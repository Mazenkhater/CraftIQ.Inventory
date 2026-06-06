using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.OrderDetail.Commands.Models
{
    public class ADDOrderDetailsCommand : IRequest<Response<OrderDetailsContract>>
    {
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
