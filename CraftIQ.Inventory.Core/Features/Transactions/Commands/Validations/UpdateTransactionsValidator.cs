using CraftIQ.Inventory.Core.Features.Transactions.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Features.Transactions.Commands.Validations
{
    public class UpdateTransactionsValidator : AbstractValidator<UpdateTransactionsCommand>
    {
        public UpdateTransactionsValidator() 
        {
            ApplyValidationsRules(); 
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.TransactionType)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.Notes)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(500)
                .WithMessage("{PropertyName} must not exceed 500 characters.");
        }
    }
}
