using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftIQ.Inventory.Infrastructure.Data.Config
{
    public class Inventoryconfiguration : IEntityTypeConfiguration<Core.Entities.Inventory>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Inventory> builder)
        {
            builder.HasKey(i => i.InventoryId);
            builder.Property(i => i.InventoryId)
                .ValueGeneratedOnAdd();
            builder.Property(i => i.Location)
                .HasMaxLength(200);
        }
    }
}
