using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Orders.Commands.Models
{
    public class ADDOrderCommand:IRequest<Response<OrdersContract>>
    {
        public int TotalAmount { get; set; }
        public int Status { get; set; }
        public int OrderType { get; set; }
        public DateTimeOffset Expecteddeliverydate { get; set; }
        public DateTimeOffset Receivedrate { get; set; }
    }
}
