using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Categories.Queries.Models
{
    public class GetCategoriesQuery : IRequest<Response<PaginatedResult<List<CategoriesContract>>>>,ICacheableQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public string CacheKey =>$"Categories_{PageNumber}_{PageSize}_{Search}_{OrderBy}";
        public TimeSpan Expiration =>TimeSpan.FromMinutes(10);
    }
}
