using Azure;
using CraftIQ.Inventory.Core.IAuthServices;
using CraftIQ.Inventory.Core.ICachingServices;
using CraftIQ.Inventory.Core.IJwtTokenService;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Services.Caching;
using CraftIQ.Inventory.Services.Implementations;
using CraftIQ.Inventory.Services.JwtService;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using CraftIQ.Inventory.Shared.Contracts.Products;
using Microsoft.Extensions.DependencyInjection;

namespace CraftIQ.Inventory.Services
{
    public static class ServicesRegistrations
    {
        public static void AddServicesRegistrations (this IServiceCollection services)
        {
            services.AddScoped<IGenericServices<CategoriesOperationsContract, CategoriesContract>, CategoriesServices>();
            services.AddScoped<IGenericServices<InventoriesOperationsContract, InventoriesContract>, InventoryServices>();
            services.AddScoped<IGenericServices<OrdersOperationsContract, OrdersContract>, OrdersServices>();
            services.AddScoped<IGenericServices<OrderDetailsOperationsContract, OrderDetailsContract>, OrderDetailsServices>();
            services.AddScoped<IGenericServices<ProductsOperationsContract, ProductsContract>, ProductsServices>();
            services.AddScoped<ITransactionsServices, TransactionsServices>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IRedisCacheService,RedisCacheService>();
        }
    }
}
