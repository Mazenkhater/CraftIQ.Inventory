using CraftIQ.Inventory.Base;
using CraftIQ.Inventory.Core.Features.Categories.Commands.Models;
using CraftIQ.Inventory.Core.Features.Inventories.Commands.Models;
using CraftIQ.Inventory.Core.Features.Inventories.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Controllers
{
    [ApiController]
    [Authorize]
    public class InventoryController : AppControllerBase
    {
        [HttpGet(Routes.InventoriesRoutes.GetById, Name = "InventoryDetailsRoute")]
        public async Task<IActionResult> GetInventoryByID([FromRoute] int id)
        {
            var Result = await Mediator.Send(new GetInventoryByIDQuery(id));
            return NewResult(Result);
        }
        [HttpGet(Routes.InventoriesRoutes.GetAll)]
        public async Task<IActionResult> GetInventories([FromQuery]GetInventoriesQuery query)
        {
            var results = await Mediator.Send(query);

            return NewResult(results);
        }
        [HttpPost(Routes.InventoriesRoutes.ADD)]
        public async Task<IActionResult> ADDInventory([FromBody] ADDInventoryCommand command)
        {
            var result = await Mediator.Send(command);
            string url = Url.Link("InventoryDetailsRoute", new { id = result.Data.Id });
            return NewResult(result, url);

        }
        [HttpPut(Routes.InventoriesRoutes.Update)]
        public async Task<IActionResult> UpdateInventory([FromRoute] int id, [FromBody] UpdateInventoryCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Routes.InventoriesRoutes.Delete)]
        public async Task<IActionResult> DeleteInventory([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand(id));
            return NewResult(result);

        }

    }
}
