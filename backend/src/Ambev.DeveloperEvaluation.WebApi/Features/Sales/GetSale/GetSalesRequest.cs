namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSalesRequest
{
    public Guid SaleId { get; }
    
    public GetSalesRequest(Guid saleId)
    {
        SaleId = saleId;
    }
}