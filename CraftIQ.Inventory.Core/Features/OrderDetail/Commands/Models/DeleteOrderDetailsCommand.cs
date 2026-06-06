using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.OrderDetail.Commands.Models
{
    public class DeleteOrderDetailsCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteOrderDetailsCommand(int id)
        {
            Id = id;
        }
    }
}
