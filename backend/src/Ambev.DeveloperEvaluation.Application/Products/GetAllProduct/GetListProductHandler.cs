using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetListProductHandler : IRequestHandler<GetListProductCommand, IEnumerable<GetProductResult>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    
    public GetListProductHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetProductResult>> Handle(GetListProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAllAsync(cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Products not found");
        
        return _mapper.Map<IEnumerable<GetProductResult>>(product);
        
    }

}