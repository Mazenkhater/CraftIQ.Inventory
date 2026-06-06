using CraftIQ.Inventory.Base;
using CraftIQ.Inventory.Core.Features.OrderDetail.Commands.Models;
using CraftIQ.Inventory.Core.Features.OrderDetail.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Controllers
{
    [ApiController]
    [Authorize]
    public class OrderDetailController : AppControllerBase
    {

        [HttpGet(Routes.OrderDetailsRoutes.GetById, Name = "OrderDetailRoute")]
        public async Task<IActionResult> GetOrderDetailsByID([FromRoute] int id)
        {
            var Result = await Mediator.Send(new GetOrderDetailsByIdQuery(id));
            return NewResult(Result);
        }
        [HttpGet(Routes.OrderDetailsRoutes.GetAll)]
        public async Task<IActionResult> GetOrderDetails([FromQuery]GetOrderDetailsQuery query)
        {
            var results = await Mediator.Send(query);

            return NewResult(results);
        }
        [HttpPost(Routes.OrderDetailsRoutes.ADD)]
        public async Task<IActionResult> ADDOrderDetails([FromBody] ADDOrderDetailsCommand command)
        {
            var result = await Mediator.Send(command);
            string url = Url.Link("OrderDetailRoute", new { id = result.Data.OrderDetailId });
            return NewResult(result, url);
        }

        [HttpPut(Routes.OrderDetailsRoutes.Update)]
        public async Task<IActionResult> UpdateOrderDetails([FromRoute] int id, [FromBody] UpdateOrderDetailsCommand command)
        {
            var result = await Mediator.Send(command);

            return NewResult(result);

        }
        [HttpDelete(Routes.OrderDetailsRoutes.Delete)]
        public async Task<IActionResult> DeleteOrderDetails([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteOrderDetailsCommand(id));

            return NewResult(result);

        }
    }
}
