using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.AspNetCore.DataProtection;
using static Ambev.DeveloperEvaluation.Domain.Entities.User;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public partial class Item : BaseEntity
{
    public Item(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalAmountItem = 0;
    }

    private void DiscountCalculator()
    {
        var d = Quantity switch
        {
            <= 10 and >= 4 => TotalAmountItem * 0.1m,
            >= 11 => TotalAmountItem * 0.2m,
            _ => Discount
        };
        Discount = d;
        
        // entre 4 e 10 produtos 10%
        // entre 11 e 20 produtos 20%
    }

    private void AddQuantity(int quantity)
    {
        Quantity += quantity;
        TotalAmountItem = Quantity * UnitPrice;
    }

    private void RemoveQuantity(int quantity)
    {
        if (Quantity > quantity)
            Quantity -= quantity;

        TotalAmountItem = Quantity * UnitPrice;
    }
    
    public void UpdateQuantity(int quantity, HandleItem action)
    {
        if (action == HandleItem.Add)
            AddQuantity(quantity);
        else
            RemoveQuantity(quantity);
        
        
        DiscountCalculator();
    }


    /// <summary>
    /// Product id
    /// Must be valid Product Guid
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Sale id
    /// Must be valid Sale Guid
    /// </summary>
    public Guid? SaleId { get; private set; }

    /// <summary>
    /// Product name
    /// Must be string with max length of 50
    /// </summary>
    public string ProductName { get; private set; }

    /// <summary>
    /// Product
    /// </summary>
    public virtual Product? Product { get; private set; }

    /// <summary>
    /// Quantity of product
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Unit price of product
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Discount applied to producs price
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Total amount of product
    /// </summary>
    public decimal TotalAmountItem { get; set; }


    
    public void UpdateTotalAmount()
    {
        TotalAmountItem = UnitPrice * Quantity;
    }
}