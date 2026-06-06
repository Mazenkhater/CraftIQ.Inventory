using CraftIQ.Inventory.Core.Features.Orders.Commands.Models;
using FluentValidation;

namespace CraftIQ.Inventory.Core.Features.Orders.Commands.Validations
{
    public class ADDOrderValidator : AbstractValidator<ADDOrderCommand>
    {
        public ADDOrderValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
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
