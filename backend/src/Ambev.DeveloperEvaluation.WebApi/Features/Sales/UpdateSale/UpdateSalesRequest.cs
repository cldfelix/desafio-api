using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSalesRequest
{
    public Guid SaleId { get; set; }
    public Guid Customer { get; set; } 
    public string? CustomerName { get; set; }
    public string? Branch { get; set; }
    public Guid Product { get; set; }
    public int Quantity { get; set; }
    public HandleItem Action { get; set; } = HandleItem.Add;
}