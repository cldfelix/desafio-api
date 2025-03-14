using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

    /// <summary>
    /// Represents a product in the system.
    /// </summary>

    public partial class Product: BaseEntity {
        public Product(string name, string description, decimal price, uint stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        /// <summary>
        /// The name of the product.
        /// Must be string with max length of 50 characters. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the product.
        /// Must be string with max length of 200 characters.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The price of the product.
        /// Must be a decimal number.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The stock of the product.
        /// Must be an unsigned integer.
        /// </summary>
        public uint Stock { get;  set; }

        /// <summary>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public uint UpdateStock(HandleItem action, uint amount)
        {
            if (action == HandleItem.Remove && Stock < amount)
                return Stock;
            
            return action == HandleItem.Add ?  Stock += amount : Stock -= amount;
        }
    }