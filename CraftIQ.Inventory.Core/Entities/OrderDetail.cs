namespace CraftIQ.Inventory.Core.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } 
    }
}
