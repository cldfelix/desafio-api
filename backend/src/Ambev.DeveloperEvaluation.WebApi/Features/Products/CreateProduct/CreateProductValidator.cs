using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().Length(3, 50);
        RuleFor(product => product.Description).NotEmpty().Length(3, 100);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Stock).GreaterThan(0);
    }
}