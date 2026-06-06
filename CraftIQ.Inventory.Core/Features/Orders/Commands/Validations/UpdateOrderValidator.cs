using CraftIQ.Inventory.Core.Features.Orders.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Orders.Commands.Validations
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x=> x.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");

            RuleFor(x => x.Status)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.OrderType)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.Expecteddeliverydate)
                .NotEqual(default(DateTimeOffset)).WithMessage("{PropertyName} must be a valid date.");

            RuleFor(x => x.Receivedrate)
                .NotEqual(default(DateTimeOffset)).WithMessage("{PropertyName} must be a valid date.");
        }
    }
}
