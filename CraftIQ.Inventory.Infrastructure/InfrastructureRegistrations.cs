using CraftIQ.Inventory.Core.AuthModels;
using CraftIQ.Inventory.Core.IRepositoryBases;
using CraftIQ.Inventory.Infrastructure.Authentication;
using CraftIQ.Inventory.Infrastructure.Data;
using CraftIQ.Inventory.Infrastructure.InfrastructureBases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace CraftIQ.Inventory.Infrastructure
{
    public static class InfrastructureRegistrations
    {
        public static void AddInfrastructureDbContext(this IServiceCollection services, string connectionString)
        {
            //لازم انزل باكدج entitysql عشان اقدر استخدم UseSqlServer
            //وهنزل ال package بتاع ال tools عشان اقدر اعمل migrations
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(connectionString));
        }
        public static void AddInfrastructureRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();
        }
    }
}
