using CraftIQ.Inventory.Shared.Contracts.Products;

namespace CraftIQ.Inventory.Shared.Contracts.Categories
{
    public record CategoriesContract(int id,
                                     string name,
                                     string description,
                                     Guid createdBy,
                                     Guid modifiedBy,
                                     DateTimeOffset createdOn,
                                     DateTimeOffset modifiedOn)
    {  
        public List<ProductsContract> Products { get; set; } = new(); 
    }
}
