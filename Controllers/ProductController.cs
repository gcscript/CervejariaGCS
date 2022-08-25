using CervejariaGCS.Data;
using CervejariaGCS.Extensions;
using CervejariaGCS.Models;
using CervejariaGCS.ViewModels;
using CervejariaGCS.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CervejariaGCS.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/products")]
        public async Task<IActionResult> GetAsync(
            [FromServices] GCSDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 5)
        {

            try
            {
                if (pageSize > 10) pageSize = 10;
                if (page < 1) page = 1;

                var products = await context
                    .Products
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Skip(page * pageSize - pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new ResultViewModel<List<Product>>(products));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Product>>("ERROR: 738612 - Falha interna no servidor"));

            }
        }

        [HttpGet("v1/products/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] GCSDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    return NotFound(new ResultViewModel<Product>("ERROR: 777885 - Conteúdo não encontrado"));
                }

                return Ok(new ResultViewModel<Product>(product));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("ERROR: 592579 - Falha interna no servidor"));
            }

        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> PostAsync(
            ProductRequestViewModel model,
            [FromServices] GCSDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Product>(ModelState.GetErros()));

            try
            {
                var product = new Product
                {
                    //Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                    Price = model.Price
                };
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Created($"v1/products/{product.Id}", new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Product>("ERROR: 428563 - Não foi possível incluir o produto"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Product>("ERROR: 477086 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/products/{id:guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] ProductRequestViewModel model,
            [FromServices] GCSDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Id não encontrado"));

                product.Name = model.Name;
                product.Slug = model.Slug;
                product.Price = model.Price;

                context.Products.Update(product);

                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Product>("ERROR: 831803 - Não foi possível alterar o produto"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Product>("ERROR: 870969 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/products/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] GCSDataContext context)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Id não encontrado"));

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Product>("ERROR: 787132 - Não foi possível excluir o produto"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Product>("ERROR: 371877 - Falha interna no servidor"));
            }
        }
    }
}
