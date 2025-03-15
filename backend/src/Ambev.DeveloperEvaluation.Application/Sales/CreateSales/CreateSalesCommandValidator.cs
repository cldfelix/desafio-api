using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;


public class CreateSalesCommandValidator : AbstractValidator<CreateSalesCommand>
{

    private bool BeAValidGuid(Guid guid) => guid != Guid.Empty;
    public CreateSalesCommandValidator()
    {
        RuleFor(s => s.Customer).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid guid");
        RuleFor(s => s.CustomerName).NotEmpty().Length(3, 100);
        RuleFor(s => s.Branch).NotEmpty();

    }
    
}