using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.IRepositoryBases;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ.Inventory.Services.Implementations
{
    public class InventoryServices : IGenericServices<InventoriesOperationsContract, InventoriesContract>
    {
        private readonly IGenericRepository<Core.Entities.Inventory> repository;
        private readonly ICurrentUserService currentUserService;

        public InventoryServices(IGenericRepository<Core.Entities.Inventory> repository, ICurrentUserService currentUser)
        {
            currentUserService = currentUser;
            this.repository = repository;
        }
        public async Task<PaginatedResult<List<InventoriesContract>>> GetAll(int pageNumber, int pageSize, string? search, string? orderBy)
        {
            var Query = repository.GetTableNoTracking();
            if (!string.IsNullOrEmpty(search))    //search!="" && search!="null") 
            {
                search = search.ToLower();

                Query = Query.Where(c =>
                    c.Location.ToLower().Contains(search)
                );
            }
            Query = orderBy?.ToLower() switch
            {
                "lastupdated" => Query.OrderBy(x => x.LastUpdated),
                "location" => Query.OrderBy(x => x.Location),
                "date" => Query.OrderBy(x => x.CreatedOn),


                _ => Query.OrderBy(x => x.InventoryId)
            };
            var TotalCount = await Query.CountAsync();
            var Inventories = await Query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (TotalCount > 0)
            {
                var result = Inventories.Select(i => new InventoriesContract
                                                                         (
                                                                              i.InventoryId,
                                                                              i.Quantity,
                                                                              i.ReorderLevel,
                                                                              i.Location,
                                                                              i.LastUpdated,
                                                                              i.CreatedBy,
                                                                              i.ModifiedBy,
                                                                              i.CreatedOn,
                                                                              i.ModifiedOn
                                                                         )).ToList();
                return new PaginatedResult<List<InventoriesContract>>(result, TotalCount,pageNumber,pageSize);
            }
            else
                return new PaginatedResult<List<InventoriesContract>>(new List<InventoriesContract>(), TotalCount, pageNumber, pageSize);
        }
        
        public async Task<InventoriesContract> GetById(int id)
        {
            var Inventory = await repository.GetTableNoTracking().FirstOrDefaultAsync(x => x.InventoryId == id);
            if (Inventory != null)
            {
                return new InventoriesContract(Inventory.InventoryId,
                                                 Inventory.Quantity,
                                                 Inventory.ReorderLevel,
                                                 Inventory.Location,
                                                 Inventory.LastUpdated,
                                                 Inventory.CreatedBy,
                                                 Inventory.ModifiedBy,
                                                 Inventory.CreatedOn,
                                                 Inventory.ModifiedOn);
            }
            else
                throw new Exception("No Inventory found");
        }

        public async Task<InventoriesContract> Add(InventoriesOperationsContract contract)
        {
            var existingInventory = await repository.GetTableNoTracking().FirstOrDefaultAsync(x => x.Location == contract.Location);
            if (existingInventory == null)
            {
                var inventory = new Core.Entities.Inventory();
                inventory.Quantity = contract.Quantity;
                inventory.ReorderLevel = contract.ReorderLevel;
                inventory.Location = contract.Location;
                inventory.LastUpdated = DateTimeOffset.UtcNow;
                inventory.CreatedBy = currentUserService.UserId;
                inventory.ModifiedBy = currentUserService.UserId;
                inventory.CreatedOn = DateTimeOffset.UtcNow;
                inventory.ModifiedOn = DateTimeOffset.UtcNow;
                var result = await repository.AddAsync(inventory);
                if (result != null)
                {
                    return new InventoriesContract(result.InventoryId,
                                                    result.Quantity,
                                                    result.ReorderLevel,
                                                    result.Location,
                                                    result.LastUpdated,
                                                    result.CreatedBy,
                                                    result.ModifiedBy,
                                                    result.CreatedOn,
                                                    result.ModifiedOn);
                }
                else
                    throw new Exception("Failed to add Inventory");
            }
            else
                throw new Exception("Inventory with the same location already exists");

        }

        public async Task Update(int id, InventoriesOperationsContract contract)
        {
            var inventory = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.InventoryId == id);
            if (inventory != null)
            {
                inventory.Quantity = contract.Quantity;
                inventory.ReorderLevel = contract.ReorderLevel;
                inventory.Location = contract.Location;
                inventory.LastUpdated = DateTimeOffset.UtcNow;
                inventory.ModifiedBy = currentUserService.UserId;
                inventory.ModifiedOn = DateTimeOffset.UtcNow;
                inventory.CreatedOn = inventory.CreatedOn;
                inventory.CreatedBy = inventory.CreatedBy;
                await repository.UpdateAsync(inventory);
            }
            else
                throw new Exception("Inventory Not Found");
        }

        public async Task Delete(int id)
        {
            var trans = repository.BeginTransaction();
            try
            {
                var inventory = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.InventoryId == id);
                if (inventory != null)
                {
                    await repository.DeleteAsync(inventory);
                    await trans.CommitAsync();
                }
                else 
                    throw new Exception("Inventory Not Found");
            }
            catch
            {
                await trans.RollbackAsync();
                throw new Exception("Failed to delete Inventory");
            }

        }
    }
}
