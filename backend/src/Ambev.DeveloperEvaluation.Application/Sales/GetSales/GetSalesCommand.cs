using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesCommand :  IRequest<GetSalesResult>
{
    public Guid SaleId { get; }

    public GetSalesCommand(Guid saleId)
    {
        SaleId = saleId;
    }
    
}