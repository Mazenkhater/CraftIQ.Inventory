using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.IRepositoryBases;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Products;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ.Inventory.Services.Implementations
{
    public class ProductsServices : IGenericServices<ProductsOperationsContract, ProductsContract> 
    {
        private readonly IGenericRepository<Product> repository;
        private readonly ICurrentUserService currentUser;

        public ProductsServices(IGenericRepository<Product> repository, ICurrentUserService currentUser)
        {
            this.repository = repository;
            this.currentUser = currentUser;
        }
        public async Task<PaginatedResult<List<ProductsContract>>> GetAll(int pageNumber, int pageSize, string? search, string? orderBy)
        {
            var Query = repository.GetTableNoTracking();
            if (!string.IsNullOrEmpty(search))    //search!="" && search!="null") 
            {
                search = search.ToLower();

                Query = Query.Where(c =>
                    c.Name.ToLower().Contains(search)
                );
            }
            Query = orderBy?.ToLower() switch
            {
                "name" => Query.OrderBy(x => x.Name),
                "weight" => Query.OrderBy(x => x.Weight),
                "date" => Query.OrderBy(x => x.CreatedOn),


                _ => Query.OrderBy(x => x.ProductId)
            };
            var TotalCount = await Query.CountAsync();
            var products = await Query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (products.Any())
            {
                var result = products.Select(p => new ProductsContract
                (
                                            p.ProductId,
                                            p.Name,
                                            p.Description,
                                            p.UnitPrice,
                                            p.Weight,
                                            p.Length,
                                            p.Width,
                                            p.Height,
                                            p.TaxCost,
                                            p.ProfitPreUnit,
                                            p.ProductionCost
                )).ToList();
                return new PaginatedResult<List<ProductsContract>>(result, TotalCount, pageNumber, pageSize);
            }
            else
                return new PaginatedResult<List<ProductsContract>>(new List<ProductsContract>(), TotalCount, pageNumber, pageSize);
        }

        public async Task<ProductsContract> GetById(int id)
        {
            var product = await repository.GetTableNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null)
            {
                return new ProductsContract(product.ProductId,
                                            product.Name,
                                            product.Description,
                                            product.UnitPrice,
                                            product.Weight,
                                            product.Length,
                                            product.Width,
                                            product.Height,
                                            product.TaxCost,
                                            product.ProfitPreUnit,
                                            product.ProductionCost);
            }
            else
                throw new Exception("No Product found");
        }

        public async Task<ProductsContract> Add(ProductsOperationsContract contract)
        {
            var existingProduct = await repository.GetTableNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower().Equals(contract.Name.ToLower()));
            if (existingProduct == null)
            {
                var product = new Product();
                product.Name = contract.Name;
                product.Description = contract.Description;
                product.UnitPrice = contract.UnitPrice;
                product.Weight = contract.Weight;
                product.Length = contract.Length;
                product.Width = contract.Width;
                product.Height = contract.Height;
                product.TaxCost = contract.TaxCost;
                product.ProfitPreUnit = contract.ProfitPreUnit;
                product.ProductionCost = contract.ProductionCost;
                product.CategoryId = contract.CategoryId;
                product.InventoryId = contract.InventoryId;
                product.CreatedBy = currentUser.UserId;
                product.ModifiedBy = currentUser.UserId;
                product.CreatedOn = DateTimeOffset.UtcNow;
                product.ModifiedOn = DateTimeOffset.UtcNow;
                var result = await repository.AddAsync(product);
                if (result != null)
                {
                    return new ProductsContract(product.ProductId,
                                                product.Name,
                                                product.Description,
                                                product.UnitPrice,
                                                product.Weight,
                                                product.Length,
                                                product.Width,
                                                product.Height,
                                                product.TaxCost,
                                                product.ProfitPreUnit,
                                                product.ProductionCost);
                }
                else
                    throw new Exception("Failed to add Product");
            }
            else
                throw new Exception("Product with the same name already exists");

        }

        public async Task Update(int id, ProductsOperationsContract contract)
        {
            var product = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null)
            {
                product.Name = contract.Name;
                product.Description = contract.Description;
                product.UnitPrice = contract.UnitPrice;
                product.Weight = contract.Weight;
                product.Length = contract.Length;
                product.Width = contract.Width;
                product.Height = contract.Height;
                product.TaxCost = contract.TaxCost;
                product.ProfitPreUnit = contract.ProfitPreUnit;
                product.ProductionCost = contract.ProductionCost;
                product.CategoryId = contract.CategoryId;
                product.InventoryId = contract.InventoryId;
                product.ModifiedBy = currentUser.UserId;
                product.ModifiedOn = DateTimeOffset.UtcNow;
                product.CreatedBy = product.CreatedBy;
                product.CreatedOn = product.CreatedOn;
                await repository.UpdateAsync(product);
            }
            else
                throw new Exception("Product Not Found");
        }

        public async Task Delete(int id)
        {
            var trans = repository.BeginTransaction();
            try
            {
                var product = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.ProductId == id);
                if (product != null)
                {
                    await repository.DeleteAsync(product);
                    await trans.CommitAsync();
                }
                else
                    throw new Exception("Product Not Found");
            }
            catch
            {
                await trans.RollbackAsync();
                throw new Exception("Failed to delete Product");
            }

        }
    }
}
