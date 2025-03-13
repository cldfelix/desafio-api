using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequest{
    
    public string? Name { get; set; }
    public string? Description{ get; set;}
    public decimal Price { get; set; }
    public int Stock { get; set; }

}