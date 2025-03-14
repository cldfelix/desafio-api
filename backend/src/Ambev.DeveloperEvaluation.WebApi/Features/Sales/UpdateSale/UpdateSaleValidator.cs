using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleRequest>
{

    public UpdateSaleValidator()
    {
        RuleFor(sale => sale.Customer).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid guid");
        RuleFor(sale => sale.Product).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid guid");
        RuleFor(sale => sale.Quantity).NotEmpty().GreaterThan(0);
    }
    
    private bool BeAValidGuid(Guid guid) => guid != Guid.Empty;
    
}