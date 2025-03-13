namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetProductResponse
{
    public string Name { get;  set; }
    public string? Description { get;  set; }
    public decimal Price { get;  set; }
    public uint Stock { get;  set; }
}

