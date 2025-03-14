using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().Length(3, 50);
        RuleFor(product => product.Description).NotEmpty().Length(3, 100);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Stock).GreaterThanOrEqualTo(0);
    }
    
}

