using CraftIQ.Inventory.Core.Features.OrderDetail.Commands.Models;
using FluentValidation;

namespace CraftIQ.Inventory.Core.Features.OrderDetail.Commands.Validations
{
    public class UpdateOrderDetailValidator : AbstractValidator<UpdateOrderDetailsCommand>
    {
        public UpdateOrderDetailValidator() 
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyValue} cannot be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
