using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public partial class UserValidator
{
    public partial class ItemValidator : AbstractValidator<Item>{
        public ItemValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEmpty()
                .WithMessage("Product id must be valid Product Guid.");
            
            RuleFor(item => item.ProductName)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Product name cannot be longer than 50 characters.");
            
            RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
            
            RuleFor(item => item.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0.");
        }

    }

}
