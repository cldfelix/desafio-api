using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetListProductResult{
    public IEnumerable<GetProductResult>? ProductsResults { get; set; }
}