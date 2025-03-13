using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetListProduct;

public class GetListProductResponse
{
    public IEnumerable<GetProductResponse>? ProductsResponses { get; set; }
}