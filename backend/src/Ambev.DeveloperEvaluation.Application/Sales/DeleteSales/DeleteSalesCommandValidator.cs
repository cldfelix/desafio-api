using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

public class DeleteSalesCommandValidator: AbstractValidator<DeleteSalesCommand>
{
    public DeleteSalesCommandValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }

}