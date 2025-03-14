using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>

public class CreateSaleHandler : IRequestHandler< CreateSaleCommand,  CreateSaleResult>
{
    private readonly ISalesRepository _repository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(IMapper mapper, ISalesRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Domain.Entities.Sales>(command);
        var createdSale = await _repository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }

}