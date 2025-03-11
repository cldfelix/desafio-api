using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


public partial class User
{
    public class Item: BaseEntity {
        public Item(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalAmount = 0;
        }
        public Guid ProductId { get; private set; }
        public Guid? SaleId { get; private set; }
        public string ProductName { get; private set; }
        public virtual Product? Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
    
  
    }
}