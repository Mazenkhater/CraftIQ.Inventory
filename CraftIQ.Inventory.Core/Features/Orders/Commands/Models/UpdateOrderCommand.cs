using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Orders.Commands.Models
{
    public class UpdateOrderCommand : IRequest<Response<string>> 
    {
        public int Id { get; set; }
        public int TotalAmount { get; set; }
        public int Status { get; set; }
        public int OrderType { get; set; }
        public DateTimeOffset Expecteddeliverydate { get; set; }
        public DateTimeOffset Receivedrate { get; set; }
    }
}
