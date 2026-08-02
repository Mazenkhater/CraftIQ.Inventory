using CraftIQ.Inventory.Core.Cache;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Inventories.Queries.Models
{
    public class GetInventoriesQuery : IRequest<Response<PaginatedResult<List<InventoriesContract>>>>,ICacheable
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public string CacheKey => $"Inventories_{PageNumber}_{PageSize}_{Search}_{OrderBy}";
        public TimeSpan Expiration => TimeSpan.FromMinutes(10);

        public bool BypassCache => false;
    }
}
