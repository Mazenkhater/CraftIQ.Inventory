using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Inventories.Commands.Models
{
    public class DeleteInventoryCommand :IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteInventoryCommand(int id)
        {
            Id = id;
        }
    }
}
