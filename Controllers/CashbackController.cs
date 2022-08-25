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
    public class CashbackController : ControllerBase
    {
        [HttpGet("v1/cashbacks")]
        public async Task<IActionResult> GetAsync(
            [FromServices] GCSDataContext context,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                if (pageSize > 10) pageSize = 10;
                if (page < 1) page = 1;

                var cashbacks = await context
                    .Cashbacks
                    .AsNoTracking()
                    .OrderByDescending(x => x.CreateAt)
                    .Skip(page * pageSize - pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new ResultViewModel<List<Cashback>>(cashbacks));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Cashback>>("ERROR: 763815 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/cashbacks")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CashbackRequestViewModel request,
            [FromServices] GCSDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Cashback>(ModelState.GetErros()));

            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);
                if (product == null)
                    return NotFound(new ResultViewModel<string>($"ERROR: 582865 - O produto {request.ProductId} não foi encontrado"));


                var cashback = new Cashback
                {
                    Product = product,
                    DayOfWeek = request.DayOfWeek,
                    Percent = request.Percent,
                };

                await context.Cashbacks.AddAsync(cashback);
                await context.SaveChangesAsync();

                return Created($"v1/cashbacks/{cashback.Id}", new ResultViewModel<Cashback>(cashback));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Cashback>("ERROR: 376479 - Não foi possível incluir o cashback"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Cashback>("ERROR: 140534 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/cashbacks/{id:guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] CashbackRequestViewModel request,
            [FromServices] GCSDataContext context)
        {
            try
            {
                var cashback = await context
                    .Cashbacks
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (cashback == null)
                    return NotFound(new ResultViewModel<Cashback>("Id não encontrado"));

                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == request.ProductId);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Produto não encontrado"));

                cashback.DayOfWeek = request.DayOfWeek;
                cashback.Percent = request.Percent;
                cashback.Product = product;


                context.Cashbacks.Update(cashback);

                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Cashback>(cashback));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Cashback>("ERROR: 228816 - Não foi possível alterar o cashback"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Cashback>("ERROR: 244244 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/cashbacks/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] GCSDataContext context)
        {
            try
            {
                var cashback = await context.Cashbacks.FirstOrDefaultAsync(x => x.Id == id);

                if (cashback == null)
                    return NotFound(new ResultViewModel<Cashback>("Id não encontrado"));

                context.Cashbacks.Remove(cashback);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Cashback>("ERROR: 995966 - Não foi possível excluir o cashback"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Cashback>("ERROR: 785107 - Falha interna no servidor"));
            }
        }
    }
}
