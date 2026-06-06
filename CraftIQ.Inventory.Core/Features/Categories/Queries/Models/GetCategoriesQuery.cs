using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Categories.Queries.Models
{
    public class GetCategoriesQuery : IRequest<Response<PaginatedResult<List<CategoriesContract>>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
    }
}
