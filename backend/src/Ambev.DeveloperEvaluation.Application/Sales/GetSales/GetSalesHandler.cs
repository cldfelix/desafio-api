using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesHandler: IRequestHandler<GetSalesCommand, GetSalesResult>
{
    private readonly ISalesRepository _repository;
    private readonly IMapper _mapper;
    
    public GetSalesHandler(ISalesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var sale = await _repository.GetByIdAsync(request.SaleId,cancellationToken);
        return _mapper.Map<GetSalesResult>(sale);
        
    }
    

    
}