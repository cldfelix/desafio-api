namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public Guid Customer { get; set; }
    public string? CustomerName { get; set; }
    public string? Branch { get; set; }
}