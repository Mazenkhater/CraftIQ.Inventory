using CraftIQ.Inventory.Core.Features.Categories.Commands.Models;
using FluentValidation;

namespace CraftIQ.Inventory.Core.Features.Categories.Commands.Validations
{
    public class ADDCategoryValidator:AbstractValidator<ADDCategoryCommand>
    {
        public ADDCategoryValidator()
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
                RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
        }
        public void ApplyCustomValidationsRules()
        {
        
        }


    }
}
