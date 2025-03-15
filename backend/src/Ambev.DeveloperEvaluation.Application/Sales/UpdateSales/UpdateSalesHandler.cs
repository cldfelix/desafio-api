using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;

public class UpdateSalesHandler : IRequestHandler<UpdateSalesCommand, UpdateSalesResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateSalesHandler(IMapper mapper, ISalesRepository salesRepository, IProductRepository productRepository)
    {
        _mapper = mapper;
        _salesRepository = salesRepository;
        _productRepository = productRepository;
    }

    public async Task<UpdateSalesResult> Handle(UpdateSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var sale = await _salesRepository.GetByIdAsync(command.SaleId, cancellationToken);

        if (sale == null)
        {
            sale = await _salesRepository.CreateAsync(_mapper.Map<Domain.Entities.Sales>(new CreateSalesCommand()
            {
                Branch = command.Branch,
                Customer = command.Customer,
                CustomerName = command.CustomerName
            }), cancellationToken);
        }
        
        
        var item = sale?.Items.Find(i => i.ProductId == command.Product);

        if (item is null && command.Action == HandleItem.Add)
        {
            var product = await _productRepository.GetByIdAsync(command.Product, cancellationToken);
            item = new Item(command.Product, product.Name, command.Quantity, product.Price);
            sale?.AddItems(item);
        }
        else
            item?.UpdateQuantity(command.Quantity, command.Action);
        
        sale?.CalculateAmmount();

        _= await _salesRepository.UpdateAsync(sale, cancellationToken);

        var result = _mapper.Map<UpdateSalesResult>(sale);
        return result;
    }
}