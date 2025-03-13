using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public partial class UserValidator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Product name cannot be longer than 50 characters.");
            
            RuleFor(product => product.Description)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Product description must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Product description cannot be longer than 100 characters.");
            
            RuleFor(product => product.Price)
                .GreaterThan(0)
                .WithMessage("Product price must be greater than 0.");
            
            RuleFor(product => (int)product.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product stock must be greater than or equal to 0.");
        }
        
    }
    
}
