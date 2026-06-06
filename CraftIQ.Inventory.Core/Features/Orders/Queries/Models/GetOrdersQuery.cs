using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using MediatR;
namespace CraftIQ.Inventory.Core.Features.Orders.Queries.Models
{
    public class GetOrdersQuery:IRequest<Response<PaginatedResult<List<OrdersContract>>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }

    }
}
