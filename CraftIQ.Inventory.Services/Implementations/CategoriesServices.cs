using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.IRepositoryBases;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using CraftIQ.Inventory.Shared.Contracts.Products;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ.Inventory.Services.Implementations
{
    public class CategoriesServices : IGenericServices<CategoriesOperationsContract, CategoriesContract>
    {
        private readonly IGenericRepository<Category> repository;
        private readonly ICurrentUserService currentUserService;

        public CategoriesServices(IGenericRepository<Category> repository, ICurrentUserService currentUser)
        {
            currentUserService = currentUser;
            this.repository = repository;
        }
        public async Task<PaginatedResult<List<CategoriesContract>>> GetAll(int pageNumber, int pageSize, string? search , string? orderBy)
        {
            var baseQuery = repository.GetTableNoTracking();
            if (!string.IsNullOrEmpty(search))    //search!="" && search!="null") 
            {
                search = search.ToLower();

                baseQuery = baseQuery.Where(c =>
                    c.Name.ToLower().Contains(search) ||
                    c.Description.ToLower().Contains(search)
                );
            }
            IQueryable<Category> query = baseQuery.Include(c => c.Products);
            query = orderBy?.ToLower() switch
            {
                "name" => query.OrderBy(x => x.Name),

                "date" => query.OrderBy(x => x.CreatedOn),

                _ => query.OrderBy(x => x.CategoryId)
            };
            var totalCount = await baseQuery.CountAsync();
            var Categories = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (totalCount > 0)
            {
                var result = Categories.Select(c => new CategoriesContract(
                                          c.CategoryId,
                                          c.Name,
                                          c.Description,
                                          c.CreatedBy,
                                          c.ModifiedBy,
                                          c.CreatedOn,
                                          c.ModifiedOn)
                {
                    Products = c.Products.Select(p => new ProductsContract(p.ProductId,
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
                                                                           )).ToList()
                }).ToList();
                return new PaginatedResult<List<CategoriesContract>>(result, totalCount, pageNumber, pageSize);
            }
            else
                return new PaginatedResult<List<CategoriesContract>>(new List<CategoriesContract>(),totalCount,pageNumber,pageSize);

        }

        public async Task<CategoriesContract> GetById(int id)
        {
            var result = await repository.GetTableNoTracking().Include(p => p.Products).FirstOrDefaultAsync(c => c.CategoryId.Equals(id));
            if (result != null)
            {
                return new CategoriesContract(
                                        result.CategoryId,
                                        result.Name,
                                        result.Description,
                                        result.CreatedBy,
                                        result.ModifiedBy,
                                        result.CreatedOn,
                                        result.ModifiedOn)
                {
                    Products = result.Products.Select(p => new ProductsContract
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
                                            )).ToList()
                };
            }
            else
                throw new Exception("Category not found");
        }

        public async Task<CategoriesContract> Add(CategoriesOperationsContract contract)
        {
            var categ = await repository.GetTableNoTracking().FirstOrDefaultAsync(c => c.Name.ToLower().Equals(contract.Name.ToLower()));
            if (categ == null)
            {
                var category = new Category
                {
                    Name = contract.Name,
                    Description = contract.Description,
                    CreatedBy = currentUserService.UserId,
                    ModifiedBy = currentUserService.UserId,
                    CreatedOn = DateTimeOffset.UtcNow,
                    ModifiedOn = DateTimeOffset.UtcNow
                };
                var result = await repository.AddAsync(category);
                if (result != null)
                {
                    return new CategoriesContract(result.CategoryId,
                                                  result.Name,
                                                  result.Description,
                                                  result.CreatedBy,
                                                  result.ModifiedBy,
                                                  result.CreatedOn,
                                                  result.ModifiedOn);
                }
                else
                    throw new Exception("Failed to add category");
            }
            else
                throw new Exception("Category with the same name already exists");
        }

        public async Task Update(int id, CategoriesOperationsContract contract)
        {
            var result = await repository.GetTableAsTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
            if (result != null)
            {
                result.Name = contract.Name;
                result.Description = contract.Description;
                result.ModifiedBy = currentUserService.UserId;
                result.ModifiedOn = DateTimeOffset.UtcNow;
                result.CreatedBy = result.CreatedBy;// مينفعش نغيره عشان هو اللي انشأ الكاتيجوري بينضف وقت الانشاء بس
                result.CreatedOn = result.CreatedOn;// مينفعش نغيره عشان هو اللي انشأ الكاتيجوري بينضف وقت الانشاء بس
                await repository.UpdateAsync(result);
            }
            else
                throw new Exception("Category not found");
        }

        public async Task Delete(int id)
        {
            var trans = repository.BeginTransaction();
            try
            {
                var result = await repository.GetTableAsTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
                if (result != null)
                {
                    await repository.DeleteAsync(result);
                    await trans.CommitAsync();
                }
                else
                    throw new Exception("Category not found");
            }
            catch
            {
                await trans.RollbackAsync();
                throw new Exception("Failed to delete category");
            }
        }
    }
}
