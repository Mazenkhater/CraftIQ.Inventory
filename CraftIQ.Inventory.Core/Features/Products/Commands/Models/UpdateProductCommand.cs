using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Products.Commands.Models
{
    public class UpdateProductCommand : IRequest<Response<string>>
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
        public int InventoryId { get; set; }
    }
}
