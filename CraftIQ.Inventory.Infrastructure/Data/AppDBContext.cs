using CraftIQ.Inventory.Core.AuthModels;
using CraftIQ.Inventory.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CraftIQ.Inventory.Infrastructure.Data
{
    public class AppDBContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Core.Entities.Inventory> Inventories { get; set; }  // namespace مش هيقدر يقرا اسم الكلاس عشان نفس اسم ال 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // عشان تربط تلقائى بين ال configurations وال entities
        }

    }
}
