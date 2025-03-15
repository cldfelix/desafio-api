using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

public class DeleteSalesCommand :  IRequest<DeleteSalesResult>
{
    public Guid SaleId { get; set; }

    
}