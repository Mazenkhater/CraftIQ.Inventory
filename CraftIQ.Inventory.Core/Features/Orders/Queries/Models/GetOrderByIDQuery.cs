using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Orders.Queries.Models
{
    public class GetOrderByIDQuery : IRequest<Response<OrdersContract>>
    {
        public int Id { get; set; }
        public GetOrderByIDQuery(int id)
        {
            Id = id;
        }
    }
}
