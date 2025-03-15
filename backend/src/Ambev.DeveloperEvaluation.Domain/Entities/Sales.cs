using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public partial class Sales : BaseEntity
{
    public Sales(Guid customer, string customerName, string branch)
    {
        Id = Guid.NewGuid();
        SaleDate = DateTime.Now;
        Customer = customer;
        CustomerName = customerName;
        TotalSaleAmount = 0;
        Branch = branch;
        Items = [];
        TotalAmount = 0;
        IsCancelled = false;
    }

    /// <summary>
    /// Represents the date of the sale
    /// </summary>
    public DateTime SaleDate { get; set; }


    /// <summary>
    /// Represents the customer who made the sale
    /// Must be a valid customer id
    /// </summary>
    public Guid Customer { get; set; }

    /// <summary>
    /// Represents the name of the customer
    /// Must be string with max length of 50
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Represents the total amount of the sale
    /// </summary>
    public decimal TotalSaleAmount { get; private set; }

    /// <summary>
    /// Represents the branch where the sale was made
    /// Must be string with max length of 50
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// Represents the items of the sale
    /// </summary>
    public List<Item>? Items { get; set; }

    private void RemoveItemWithQuantityZero() =>
        Items?.RemoveAll(i => i.Quantity == 0);

    public void CalculateAmmount()
    {
        foreach (var item in Items)
            item.UpdateTotalAmount();

        TotalAmount = Items.Sum(i => i.TotalAmountItem);
        TotalSaleAmount = TotalAmount > 0
            ? Items.Sum(i => i.TotalAmountItem) - Items.Sum(i => i.Discount)
            : 0;

        RemoveItemWithQuantityZero();
    }

    public async Task AddItems(Item item, HandleItem action = HandleItem.Add)
    {
        var it = Items.Find(i => i.ProductId == item.ProductId);

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

        item.UpdateTotalAmount();
        await Task.CompletedTask;
    }

    public void SetItems(List<Item> items) => Items = items;

    /// <summary>
    /// Represents the total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; private set; }

    public void SetTotalAmount(decimal totalAmount) => TotalAmount = totalAmount;

    /// <summary>
    /// Represents if the sale was cancelled
    /// Must be boolean
    /// </summary>
    public bool IsCancelled { get; private set; }

    public void SetIsCancelled(bool isCancelled) => IsCancelled = isCancelled;
}