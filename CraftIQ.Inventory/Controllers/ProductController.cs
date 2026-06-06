using CraftIQ.Inventory.Base;
using CraftIQ.Inventory.Core.Features.Products.Commands.Models;
using CraftIQ.Inventory.Core.Features.Products.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductController : AppControllerBase
    {
        [HttpGet(Routes.ProductsRoutes.GetById, Name = "ProductDetailsRoute")]
        public async Task<IActionResult> GetProductByID([FromRoute] int id)
        {
            var Result = await Mediator.Send(new GetProductByIdQuery(id));
            return NewResult(Result);
        }

        [HttpGet(Routes.ProductsRoutes.GetAll)]
        public async Task<IActionResult> GetProducts([FromQuery]GetProductsQuery query)
        {
            var results = await Mediator.Send(query);
            return NewResult(results);
        }

        [HttpPost(Routes.ProductsRoutes.ADD)]
        public async Task<IActionResult> ADDProduct([FromBody] ADDProductCommand command)
        {
            var result = await Mediator.Send(command);
            string url = Url.Link("ProductDetailsRoute", new { id = result.Data.Id });
            return NewResult(result, url);
        }

        [HttpPut(Routes.ProductsRoutes.Update)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);

        }

        [HttpDelete(Routes.ProductsRoutes.Delete)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteProductCommand(id));
            return NewResult(result);

        }
    }
}
