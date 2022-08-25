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
    public class OrderController : ControllerBase
    {
        [HttpGet("v1/orders")]
        public async Task<IActionResult> GetAsync(
            [FromServices] GCSDataContext context,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Order>(ModelState.GetErros()));


                startDate ??= DateTime.MinValue;
                endDate ??= DateTime.MaxValue;

                if (pageSize > 10) pageSize = 10;
                if (page < 1) page = 1;

                var orders = await context
                    .Orders
                    .AsNoTracking()
                    .Where(x => x.CreateAt >= startDate && x.CreateAt <= endDate)
                    .OrderByDescending(x => x.CreateAt)
                    .Skip(page * pageSize - pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new ResultViewModel<List<Order>>(orders));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Order>>("ERROR: 743479 - Falha interna no servidor"));

            }
        }

        [HttpGet("v1/orders/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] GCSDataContext context)
        {
            try
            {
                var orderItems = await context
                    .OrderItems
                    .AsNoTracking()
                    .Where(x => x.OrderId == id)
                    .OrderBy(x => x.ProductId)
                    .ToListAsync();

                return Ok(new ResultViewModel<List<OrderItem>>(orderItems));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("ERROR: 144616 - Falha interna no servidor"));
            }

        }

        [HttpPost("v1/orders")]
        public async Task<IActionResult> PostAsync(
            [FromBody] OrderRequestViewModel request,
            [FromServices] GCSDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Order>(ModelState.GetErros()));

            try
            {
                decimal totalOrder = 0;
                decimal totalCashbackOrder = 0;

                var order = new Order();
                var lstOrderItem = new List<OrderItem>();

                foreach (var productAndQuantityRequest in request.ProductsAndQuantityRequest)
                {
                    decimal cashbackValue = 0;

                    var product = await context.Products.FirstOrDefaultAsync(x => x.Id == productAndQuantityRequest.Product);
                    if (product == null)
                        return NotFound(new ResultViewModel<string>($"ERROR: 458385 - O produto {productAndQuantityRequest.Product} não foi encontrado"));
                    int dow = (int)DateTime.Now.DayOfWeek;
                    var cashback = await context.Cashbacks.FirstOrDefaultAsync(c => c.ProductId == productAndQuantityRequest.Product && c.DayOfWeek == dow);
                    if (cashback != null)
                        cashbackValue = (decimal)cashback.Percent;

                    var orderItem = new OrderItem();
                    orderItem.Product = product;
                    orderItem.Quantity = productAndQuantityRequest.Quantity;
                    orderItem.Price = product.Price;

                    orderItem.Cashback = product.Price * productAndQuantityRequest.Quantity * (cashbackValue / 100) / productAndQuantityRequest.Quantity;

                    totalOrder += orderItem.Price * orderItem.Quantity;
                    totalCashbackOrder += orderItem.Cashback * orderItem.Quantity;
                    lstOrderItem.Add(orderItem);
                }

                order.OrderItem = lstOrderItem;

                order.Total = totalOrder;
                order.Cashback = totalCashbackOrder;
                order.Subtotal = totalOrder - totalCashbackOrder;

                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

                return Created($"v1/orders/{order.Id}", new ResultViewModel<Order>(order));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Order>("ERROR: 106549 - Não foi possível incluir o pedido"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Order>("ERROR: 974519 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/orders/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] GCSDataContext context)
        {
            try
            {
                var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);

                if (order == null)
                    return NotFound(new ResultViewModel<Order>("Id não encontrado"));

                context.Orders.Remove(order);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Order>("ERROR: 260452 - Não foi possível excluir o pedido"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Order>("ERROR: 384640 - Falha interna no servidor"));
            }
        }
    }
}
