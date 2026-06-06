

namespace CraftIQ.Inventory.Core.Entities
{
    public class Inventory: BaseEntity
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public string Location { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public List<Product> Products { get; set; } = new();

    }
}
