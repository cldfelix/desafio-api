using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesHandler : IRequestHandler<CreateSalesCommand, CreateSalesResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    public CreateSalesHandler(IMapper mapper, ISalesRepository salesRepository)
    {
        _mapper = mapper;
        _salesRepository = salesRepository;
    }

    public async Task<CreateSalesResult> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = _mapper.Map<Domain.Entities.Sales>(command);
        var createdProduct = await _salesRepository.CreateAsync(product, cancellationToken);
        var result = _mapper.Map<CreateSalesResult>(createdProduct);
        return result;
    }
}