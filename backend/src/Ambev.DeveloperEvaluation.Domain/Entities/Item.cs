using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


public partial class User
{
    public class Item: BaseEntity {
        public Item(Product product, int quantity, decimal unitPrice, decimal discount)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            TotalAmount = (unitPrice * quantity) - discount;
        }
        public Product? Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
    }
}