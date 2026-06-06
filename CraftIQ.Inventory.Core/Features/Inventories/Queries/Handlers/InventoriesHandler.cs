using CraftIQ.Inventory.Core.Features.Inventories.Queries.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Inventories.Queries.Handlers
{
    public class InventoriesHandler :ResponseHandler,
                                        IRequestHandler<GetInventoriesQuery,Response<PaginatedResult<List<InventoriesContract>>>>,
                                        IRequestHandler<GetInventoryByIDQuery,Response<InventoriesContract>>
    {
        private readonly IGenericServices<InventoriesOperationsContract, InventoriesContract> inventoryServices;

        public InventoriesHandler(IGenericServices<InventoriesOperationsContract, InventoriesContract> inventoryServices)
        {
            this.inventoryServices = inventoryServices;
        }
        public async Task<Response<PaginatedResult<List<InventoriesContract>>>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
        {
            var inventories = await inventoryServices.GetAll(request.PageNumber,request.PageSize,request.Search, request.OrderBy);
            return Success(inventories);
        }

        public async Task<Response<InventoriesContract>> Handle(GetInventoryByIDQuery request, CancellationToken cancellationToken)
        {
            var inventory = await inventoryServices.GetById(request.Id);
            return Success(inventory);
        }
    }
}
