using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CraftIQ.Inventory.Core.Features.Categories.Commands.Models
{
    public class ADDCategoryCommand : IRequest<Response<CategoriesContract>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
