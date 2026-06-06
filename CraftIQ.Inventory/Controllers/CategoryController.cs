using CraftIQ.Inventory.Base;
using CraftIQ.Inventory.Core.Features.Categories.Commands.Models;
using CraftIQ.Inventory.Core.Features.Categories.Queries.Models;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Controllers
{
    [ApiController]
    [Authorize]
    public class CategoryController : AppControllerBase
    {
        [HttpGet(Routes.CategoriesRoutes.GetById, Name = "CategoryDetailsRoute")]
        public async Task<IActionResult> GetCategoryByID([FromRoute] int id)
        {
            var Result = await Mediator.Send(new GetCategoryByIDQuery(id));
            return NewResult(Result);
        }
        [HttpGet(Routes.CategoriesRoutes.GetAll)]
        public async Task<IActionResult> GetCategories([FromQuery]GetCategoriesQuery query)
        {
            var results = await Mediator.Send(query);

            return NewResult(results);
        }
        [HttpPost(Routes.CategoriesRoutes.ADD)]
        public async Task<IActionResult> ADDCategory([FromBody] ADDCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            string url = Url.Link("CategoryDetailsRoute", new { id = result.Data.id });
            return NewResult(result, url);

        }
        [HttpPut(Routes.CategoriesRoutes.Update)]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] ADDCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);

        }
        [HttpDelete(Routes.CategoriesRoutes.Delete)]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand(id));
            return NewResult(result);

        }

    }
}
