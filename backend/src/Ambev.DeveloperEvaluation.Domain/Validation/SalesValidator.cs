using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public partial class UserValidator
{
    public partial class SalesValidator : AbstractValidator<Sales>{
            public SalesValidator()
            {
                RuleFor(sales => sales.Customer)
                    .NotEmpty()
                    .WithMessage("Customer id must be valid Customer Guid.");
                
                RuleFor(sales => sales.CustomerName)
                    .NotEmpty()
                    .MinimumLength(3).WithMessage("Customer name must be at least 3 characters long.")
                    .MaximumLength(50).WithMessage("Customer name cannot be longer than 50 characters.");
                
                RuleFor(sales => sales.Branch)
                    .NotEmpty()
                    .MinimumLength(3).WithMessage("Branch name must be at least 3 characters long.")
                    .MaximumLength(50).WithMessage("Branch name cannot be longer than 50 characters.");
            }
        }

}
