using Ambev.DeveloperEvaluation.Domain.Common;
using static Ambev.DeveloperEvaluation.Domain.Entities.User;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


    public partial class Item: BaseEntity {
        public Item(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalAmount = 0;
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
        public decimal Discount { get; private set; }

        /// <summary>
        /// Total amount of product
        /// </summary>
        public decimal TotalAmount { get; private set; }
    
  
}