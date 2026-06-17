using CraftIQ.Inventory.Core.Features.Inventories.Commands.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Inventories.Commands.Handlers
{
    public class InventoriesHandler :ResponseHandler,
                                      IRequestHandler<ADDInventoryCommand,Response<InventoriesContract>>,
                                      IRequestHandler<UpdateInventoryCommand, Response<string>>,
                                      IRequestHandler<DeleteInventoryCommand, Response<string>>
    {
        private readonly IGenericServices<InventoriesOperationsContract, InventoriesContract> inventoryServices;

        public InventoriesHandler(IGenericServices<InventoriesOperationsContract,InventoriesContract> inventoryServices)
        {
            this.inventoryServices = inventoryServices;
        }
        public async Task<Response<InventoriesContract>> Handle(ADDInventoryCommand request, CancellationToken cancellationToken)
        {
            var result = await inventoryServices.Add(new InventoriesOperationsContract(request.Quantity, request.ReorderLevel, request.Location));
            return Created(result);
        }
        public async Task<Response<string>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            await inventoryServices.Update(request.Id,new InventoriesOperationsContract(request.Quantity,request.ReorderLevel,request.Location));
            return Success("Inventory updated successfully");
        }

        public async Task<Response<string>> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            await inventoryServices.Delete(request.Id);
            return Deleted<string>("Inventory deleted successfully");
        }
        
    }
}
