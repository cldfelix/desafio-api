using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


public partial class User
{
    public class Product: BaseEntity {
        public Product(string name, string description, decimal price, uint stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public uint Stock { get; private set; }

        
    }
}