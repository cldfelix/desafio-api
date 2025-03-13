using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetListProductCommand : IRequest<IEnumerable<GetProductResult>>
{
}