using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSalesResponse
{
    public CreateSalesResponse(Guid customer, string customerName, string branch)
    {
        SaleId = Guid.NewGuid();
        SaleDate = DateTime.Now;
        Customer = customer;
        CustomerName = customerName;
        TotalSaleAmount = 0;
        Branch = branch;
        Items = [];
        TotalAmount = 0;
        IsCancelled = false;
    }

    public Guid SaleId { get; private set; }
    public DateTime SaleDate { get; private set; }
    public Guid Customer { get; private set; }
    public string CustomerName { get; private set; }
    public decimal TotalSaleAmount { get; private set; }
    public string Branch { get; private set; }
    public virtual List<Item> Items { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    
    
    public void AddItem(Item item, HandleItem action)
    {
        var it  = Items.Find(i=> i.ProductId == item.ProductId);

        if (it == null)
        {
            Items.Add(item);
            TotalAmount += item.Quantity;
        }
        else
        {
            if (action == HandleItem.Add)
            {
                Items.Add(item);
                TotalAmount += item.Quantity;
            }
            else
            {
                Items.Remove(item);
                TotalAmount -= item.Quantity;
            
            }
        }
        
        
    }
}