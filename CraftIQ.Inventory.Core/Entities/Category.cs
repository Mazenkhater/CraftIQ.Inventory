namespace CraftIQ.Inventory.Core.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
