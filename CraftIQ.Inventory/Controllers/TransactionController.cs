using CraftIQ.Inventory.Base;
using CraftIQ.Inventory.Core.Features.Transactions.Commands.Models;
using CraftIQ.Inventory.Core.Features.Transactions.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : AppControllerBase
    {
        [HttpGet(Routes.ProductsRoutes.GetById, Name = "TransactionDetailsRoute")]
        public async Task<IActionResult> GetTransactionByID([FromRoute] Guid id)
        {
            var Result = await Mediator.Send(new GetTransactionsByIdQuery(id));
            return NewResult(Result);
        }

        [HttpGet(Routes.OrdersRoutes.GetAll)]
        public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsQuery query)
        {
            var results = await Mediator.Send(query);
            return NewResult(results);
        }
        [HttpPost(Routes.TransactionsRoutes.ADD)]
        public async Task<IActionResult> ADDTransaction([FromBody] ADDTransactionsCommand command)
        {
            var result = await Mediator.Send(command);
            string url = Url.Link("TransactionDetailsRoute", new { id = result.Data.TransactionId });
            return NewResult(result, url);
        }
        [HttpPut(Routes.TransactionsRoutes.Update)]
        public async Task<IActionResult> UpdateTransaction([FromRoute] Guid id, [FromBody] UpdateTransactionsCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);

        }
        [HttpDelete(Routes.TransactionsRoutes.Delete)]
        public async Task<IActionResult> DeleteTransaction([FromRoute] Guid id)
        {
            var result = await Mediator.Send(new DeleteTransactionsCommand(id));
            return NewResult(result);

        }
    }
}
