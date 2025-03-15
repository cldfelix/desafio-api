using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSalesRequestValidator : AbstractValidator<UpdateSalesRequest>
{

    public UpdateSalesRequestValidator()
    {
        RuleFor(sale => sale.SaleId).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid guid");
        RuleFor(sale => sale.Action).IsInEnum().WithMessage("Invalid action");
        RuleFor(sale => sale.Customer).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid guid");
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 100);
        RuleFor(sale => sale.Branch).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.Product).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid guid");
        RuleFor(sale => sale.Quantity).NotEmpty().GreaterThan(0);
        
        /*
         *     public Guid Customer { get; set; } 
           public string? CustomerName { get; set; }
           public string? Branch { get; set; }
         */
    }
    
    private bool BeAValidGuid(Guid guid) => guid != Guid.Empty;
    
}