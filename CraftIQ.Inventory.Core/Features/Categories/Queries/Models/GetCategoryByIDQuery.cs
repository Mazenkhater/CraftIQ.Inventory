using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Categories.Queries.Models
{
    public class GetCategoryByIDQuery : IRequest<Response<CategoriesContract>>
    {
        public int Id { get; set; }
        public GetCategoryByIDQuery(int id)
        {
            Id = id;
        }
    }
}
