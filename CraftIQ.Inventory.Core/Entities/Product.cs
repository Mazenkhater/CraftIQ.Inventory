using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public decimal TaxCost { get; set; }
        public decimal ProfitPreUnit { get; set; }
        public decimal ProductionCost { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } 

        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new();

    }
}
