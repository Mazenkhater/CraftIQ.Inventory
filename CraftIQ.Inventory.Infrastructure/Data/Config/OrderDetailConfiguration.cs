using CraftIQ.Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Infrastructure.Data.Config
{
    internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(o=>o.OrderDetailId);
            builder.Property(od => od.OrderDetailId).ValueGeneratedOnAdd();
            builder.HasOne(x=>x.Order).WithMany(o=>o.OrderDetails).HasForeignKey(od=>od.OrderId);
            builder.HasOne(k=>k.Product).WithMany(p=>p.OrderDetails).HasForeignKey(od=>od.ProductId);
        }
    }
}
