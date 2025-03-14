using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.Customer).NotEmpty();
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.Branch).NotEmpty().Length(3, 50);
        //RuleFor(sale => sale.Items).NotEmpty();
        RuleFor(sale => sale.TotalAmount).GreaterThan(0);
    }
}