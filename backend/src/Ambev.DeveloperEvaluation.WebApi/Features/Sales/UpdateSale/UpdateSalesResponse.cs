using Ambev.DeveloperEvaluation.Domain.Entities;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSalesResponse
{
    public Guid SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid Customer { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalSaleAmount { get; set; }
    public string Branch { get; set; }
    public virtual List<Item> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}