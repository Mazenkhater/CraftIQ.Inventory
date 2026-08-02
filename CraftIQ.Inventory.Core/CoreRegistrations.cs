using CraftIQ.Inventory.Core.ValidationBehavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CraftIQ.Inventory.Core
{
    public static class CoreRegistrations
    {
        public static void ADDCoreRegistrations(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
        }
    }
}
