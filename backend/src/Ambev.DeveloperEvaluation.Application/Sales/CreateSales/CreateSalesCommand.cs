using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesCommand: IRequest<CreateSalesResult>{
    public Guid Customer { get; set; } 
    public string? CustomerName { get; set; }
    public string? Branch { get; set; }
    
    

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSalesCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}