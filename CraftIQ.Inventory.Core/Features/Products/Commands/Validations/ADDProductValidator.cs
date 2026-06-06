using CraftIQ.Inventory.Core.Features.Products.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Products.Commands.Validations
{
    public class ADDProductValidator : AbstractValidator<ADDProductCommand>
    {
        public ADDProductValidator() 
        {
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(x => x.UnitPrice)
                .InclusiveBetween(0.01m, 10000m).WithMessage("{PropertyName} must be between 0.01 and 10000.");

            RuleFor(x => x.Weight)
                .InclusiveBetween(0.01f, 1000f).WithMessage("{PropertyName} must be between 0.01 and 1000.");

            RuleFor(x => x.Length)
                .InclusiveBetween(0.01f, 1000f).WithMessage("{PropertyName} must be between 0.01 and 1000.");

            RuleFor(x => x.Width)
                .InclusiveBetween(0.01f, 1000f).WithMessage("{PropertyName} must be between 0.01 and 1000.");

            RuleFor(x => x.Height)
                .InclusiveBetween(0.01f, 1000f).WithMessage("{PropertyName} must be between 0.01 and 1000.");

            RuleFor(x => x.TaxCost)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(x => x.ProfitPreUnit)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(x => x.ProductionCost)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.InventoryId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
