using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;

public class UpdateSalesCommand: IRequest<UpdateSalesResult>{
    public Guid SaleId { get; set; }
    public Guid Customer { get; set; } 
    public string? CustomerName { get; set; }
    public string? Branch { get; set; }
    public Guid Product { get; set; }
    public int Quantity { get; set; }
    public HandleItem Action { get; set; } = HandleItem.Add;
    

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSalesCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}