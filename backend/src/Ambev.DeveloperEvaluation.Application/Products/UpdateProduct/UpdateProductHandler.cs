using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler: IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    
    public UpdateProductHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var mappedProduct = _mapper.Map<Product>(request);
        
        var product = await _repository.UpdateByIdAsync(mappedProduct, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");
        
        return _mapper.Map<UpdateProductResult>(product);
    }
    

    
}