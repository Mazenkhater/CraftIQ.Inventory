using CraftIQ.Inventory.Core.Features.Inventories.Commands.Models;
using FluentValidation;

namespace CraftIQ.Inventory.Core.Features.Inventories.Commands.Validations
{
    public class ADDInventoryValidator:AbstractValidator<ADDInventoryCommand>
    {
        public ADDInventoryValidator() 
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");
            RuleFor(x => x.ReorderLevel)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
        }
    }
}
