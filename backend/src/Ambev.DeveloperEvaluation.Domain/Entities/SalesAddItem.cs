using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public partial class SalesAddItem
{
    public Guid SaleId { get; set; }
    public Guid Customer { get; set; } 
    public Guid Product { get; set; }
    public string? CustomerName { get; set; }
    public string? Branch { get; set; }
    public HandleItem Action { get; set; } = HandleItem.Add;
    public int Quantity { get; set; }
}