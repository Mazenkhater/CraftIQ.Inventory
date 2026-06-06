using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.OrderDetail.Queries.Models
{
    public class GetOrderDetailsQuery : IRequest<Response<PaginatedResult<List<OrderDetailsContract>>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }

    }
}
