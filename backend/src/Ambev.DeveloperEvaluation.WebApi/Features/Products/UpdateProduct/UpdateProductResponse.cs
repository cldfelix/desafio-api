namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductResponse
{
    public Guid Id { get; set; }
    public string Name { get;  set; }
    public string? Description { get;  set; }
    public decimal Price { get;  set; }
    public uint Stock { get;  set; }
}

