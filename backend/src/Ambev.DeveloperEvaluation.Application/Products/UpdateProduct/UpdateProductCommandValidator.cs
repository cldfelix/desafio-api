using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 3 and 50 characters
    /// - Description: Required, must be between 3 and 100 characters
    /// - Price: Required, must be greater than 0
    /// - Quantity: Required, must be greater than 0
    /// </remarks>
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Name).NotEmpty().Length(3, 50);
        RuleFor(product => product.Description).NotEmpty().Length(3, 100);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Stock).GreaterThan(0);
    }
}