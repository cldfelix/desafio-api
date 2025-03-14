using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class UpdateSaleResponse
{
    public string SaleId { get;  set; }
    public DateTime SaleDate { get;  set; }
    public Guid Customer { get;  set; }
    public string CustomerName { get;  set; }
    public decimal TotalSaleAmount { get; private set; }
    public string Branch { get;  set; }
    public virtual List<Item> Items { get;  set; }
    public decimal TotalAmount { get;  set; }
    public bool IsCancelled { get; set; }
    
    public void CalculateAmmount()
    {
        foreach (var item in Items)
        {
           item.UpdateTotalAmount();
        }
        
        var v =  Items.Sum(i => i.TotalAmountItem);
        TotalAmount = v;
        
        TotalSaleAmount = Items.Sum(i => i.TotalAmountItem) - Items.Sum(i => i.Discount);
        
    }
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