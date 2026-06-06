using CraftIQ.Inventory.Core.ValidationBehavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CraftIQ.Inventory.Core
{
    public static class CoreRegistrations
    {
        public static void ADDCoreRegistrations(this IServiceCollection Services)
        {
            Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
