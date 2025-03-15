using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

public class DeleteSalesHandler: IRequestHandler<DeleteSalesCommand, DeleteSalesResult>
{
    private readonly ISalesRepository _repository;
    private readonly IMapper _mapper;
    
    public DeleteSalesHandler(ISalesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DeleteSalesResult> Handle(DeleteSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var response = await _repository.DeleteAsync(request.SaleId,cancellationToken);
        
        return new DeleteSalesResult
        {
            Deleted = response,
            SaleId = request.SaleId
        };
        
    }
    

    
}