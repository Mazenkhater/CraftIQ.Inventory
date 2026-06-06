using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Inventories.Commands.Models
{
    public class UpdateInventoryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public string Location { get; set; }
    }
}
