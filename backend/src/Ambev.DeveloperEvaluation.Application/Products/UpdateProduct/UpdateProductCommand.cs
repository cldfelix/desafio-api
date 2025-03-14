using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    public Guid Id { get; }
    public string Name { get;  set; }
    public string? Description { get;  set; }
    public decimal Price { get;  set; }
    public int Stock { get;  set; }

   
    
}