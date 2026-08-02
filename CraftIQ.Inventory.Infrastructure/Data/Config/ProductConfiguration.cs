using CraftIQ.Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftIQ.Inventory.Infrastructure.Data.Config
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public ProductConfiguration() { }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p=>p.ProductId).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.HasOne(x => x.Category).WithMany(o => o.Products).HasForeignKey(od => od.CategoryId);
            builder.HasOne(k => k.Inventory).WithMany(p => p.Products).HasForeignKey(od => od.InventoryId);
        }
    }
}
