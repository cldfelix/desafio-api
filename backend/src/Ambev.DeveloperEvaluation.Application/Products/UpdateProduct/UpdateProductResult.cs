namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}