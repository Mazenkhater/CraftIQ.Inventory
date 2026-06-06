using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Inventories.Queries.Models
{
    public class GetInventoryByIDQuery : IRequest<Response<InventoriesContract>>
    {
        public int Id { get; set; }
        public GetInventoryByIDQuery(int id)
        {
            Id = id;
        }
    }
}
