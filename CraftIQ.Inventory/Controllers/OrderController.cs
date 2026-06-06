using CraftIQ.Inventory.Base;
using CraftIQ.Inventory.Core.Features.Orders.Commands.Models;
using CraftIQ.Inventory.Core.Features.Orders.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Controllers
{
    [ApiController]
    [Authorize]
    public class OrderController : AppControllerBase
    {
        [HttpGet(Routes.OrdersRoutes.GetById, Name = "OrderDetailsRoute")]
        public async Task<IActionResult> GetOrderByID([FromRoute] int id)
        {
            var Result = await Mediator.Send(new GetOrderByIDQuery(id));
            return Ok(Result);
        }
        [HttpGet(Routes.OrdersRoutes.GetAll)]
        public async Task<IActionResult> GetOrders([FromQuery]GetOrdersQuery query)
        {
            var results = await Mediator.Send(query);

            return Ok(results);
        }
        [HttpPost(Routes.OrdersRoutes.ADD)]
        public async Task<IActionResult> ADDOrder([FromBody] ADDOrderCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                string url = Url.Link("OrderDetailsRoute", new { id = result.Data.id });
                return Created(url, result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut(Routes.OrdersRoutes.Update)]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete(Routes.OrdersRoutes.Delete)]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new DeleteOrderCommand(id));

                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
