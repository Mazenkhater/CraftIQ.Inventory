using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.OrderDetail.Queries.Models
{
    public class GetOrderDetailsByIdQuery : IRequest<Response<OrderDetailsContract>>
    {
        public int Id { get; set; }
        public GetOrderDetailsByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
