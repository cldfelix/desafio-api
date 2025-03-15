using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;

public class UpdateSalesCommandValidator:  AbstractValidator<UpdateSalesCommand>
{
    private bool BeAValidGuid(Guid guid) => guid != Guid.Empty;
    public UpdateSalesCommandValidator()
    {
        RuleFor(s=> s.Customer).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid customer guid");
        RuleFor(s => s.Branch).NotEmpty().Length(3, 100);
        RuleFor(s => s.CustomerName).NotEmpty().Length(3, 100);
        RuleFor(s => s.SaleId).NotEmpty()
          .Must(BeAValidGuid).WithMessage("Invalid sales guid");
        RuleFor(s => s.Product).NotNull()
            .Must(BeAValidGuid).WithMessage("Invalid product guid");
        RuleFor(s => s.Action).IsInEnum().WithMessage("Invalid action");
        RuleFor(s => s.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        
    }
}