using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.SaveSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSave;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(
        ILogger<SalesController> logger, 
        IMediator mediator, 
        IMapper mapper, 
        IRedisService redisService, 
        IUserRepository userRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userRepository = userRepository;
    }


    [HttpPost("start")]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSalesResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartSale([FromBody] CreateSalesRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<CreateSalesCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSalesResponse>
        {
            Success = true,
            Message = "Sale started successfully",
            Data = _mapper.Map<CreateSalesResponse>(response)
        });
    }
    
    
    
    [HttpGet("close-sale/{saleId}")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaveSalesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloseSale([FromRoute] Guid saleId,
        CancellationToken cancellationToken)
    {
        var saleComand = _mapper.Map<GetSalesCommand>(saleId);
        var sale = await _mediator.Send(saleComand, cancellationToken);
        if (sale is null)
            return BadRequest("Sale not found");
        
        if (sale.Items.Count() == 0)
            return BadRequest("Sale has no items");
        
        return Created(string.Empty, new ApiResponseWithData<SaveSalesResponse>
        {
            Success = true,
            Message = "Sale closed successfully",
            Data = _mapper.Map<SaveSalesResponse>(null)
        });
    }
    
        
    [HttpDelete("{saleId}")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteSalesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid saleId,
        CancellationToken cancellationToken)
    {
        var saleComand = _mapper.Map<GetSalesCommand>(saleId);
        var sale = await _mediator.Send(saleComand, cancellationToken);
        if (sale is null)
            return BadRequest("Sale not found");
        
        if (!sale.Items.Any())
            return BadRequest("Sale has no items");
            

        foreach (var item in sale.Items)
        {
            var commandProduct = _mapper.Map<GetProductCommand>(item.ProductId);
            var product = await _mediator.Send(commandProduct, cancellationToken);
            product.Stock += (uint)item.Quantity;
            var commandProductUpdate = _mapper.Map<UpdateProductCommand>(product);
            await _mediator.Send(commandProductUpdate, cancellationToken);
        }
        
        sale.IsCancelled = true;
        
        
        var deleteComand = _mapper.Map<DeleteSalesCommand>(sale);
        var deleteSale  =  await _mediator.Send(deleteComand, cancellationToken);
        
        return Created(string.Empty, new ApiResponseWithData<DeleteSalesResponse>
        {
            Success = true,
            Message = "Sale deleted successfully",
            Data = _mapper.Map<DeleteSalesResponse>(deleteSale)
        });
    }
    
    
    [HttpGet("{saleId}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSalesResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSale([FromRoute] Guid saleId, CancellationToken cancellationToken)
    {
        var saleComand = _mapper.Map<GetSalesCommand>(saleId);
        var sale = await _mediator.Send(saleComand, cancellationToken);
        if (sale is null)
            return BadRequest("Sale not found");

        return Created(string.Empty, new ApiResponseWithData<UpdateSalesResponse>
        {
            Success = true,
            Message = "Sale closed successfully",
            Data = _mapper.Map<UpdateSalesResponse>(sale)
        });
    }

    [HttpPost("add-item")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSalesResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateSale([FromBody] UpdateSalesRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSalesRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var commandProduct = _mapper.Map<GetProductCommand>(request.Product);
        var product = await _mediator.Send(commandProduct, cancellationToken);

        if (product is null) BadRequest("Product not found");
        
        if(request.Action == HandleItem.Add && product.Stock < request.Quantity)
            return BadRequest("Insufficient Stock");
        
        var saleComand = _mapper.Map<GetSalesCommand>(request.SaleId);
        var sale = await _mediator.Send(saleComand, cancellationToken);
        if (sale is null)
            return BadRequest("Sale not found");

        var item = sale.Items.FirstOrDefault(i => i.ProductId == request.Product);
        if (item is not null && item.Quantity + request.Quantity > 20  && request.Action == HandleItem.Add)
            return BadRequest("Max quantity of 20 items per product reached");

        product.Stock = request.Action switch
        {
            HandleItem.Add => product.Stock -= (uint)request.Quantity,
            HandleItem.Remove when item?.Quantity > 0 => product.Stock += (uint)request.Quantity,
            _ => product.Stock
        };

        var updateComand = _mapper.Map<UpdateSalesCommand>(request);
        var updatedSale  =  await _mediator.Send(updateComand, cancellationToken);
        
        var commandProductUpdate = _mapper.Map<UpdateProductCommand>(product);
        await _mediator.Send(commandProductUpdate, cancellationToken);
      
        return Created(string.Empty, new ApiResponseWithData<UpdateSalesResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = _mapper.Map<UpdateSalesResponse>(updatedSale)
        });
        
    }



}