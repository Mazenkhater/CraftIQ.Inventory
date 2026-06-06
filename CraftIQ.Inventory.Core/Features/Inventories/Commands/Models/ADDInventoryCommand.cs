using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Inventories.Commands.Models
{
    public class ADDInventoryCommand : IRequest<Response<InventoriesContract>>
    {
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public string Location { get; set; }
    }
}
