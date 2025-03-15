using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly ILogger<SalesController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRedisService _redisService;

    public SalesController(
        ILogger<SalesController> logger, 
        IMediator mediator, 
        IMapper mapper, 
        IRedisService redisService, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _redisService = redisService;
        _userRepository = userRepository;
    }


    [HttpPost("/start")]
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
    [HttpGet("/close-sale/{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSalesResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloseSale([FromRoute] Guid saleId,
        CancellationToken cancellationToken)
    {
        var saleDb =  await _redisService.GetAsync<UpdateSalesResponse>(saleId.ToString(), cancellationToken);
        if (saleDb is null)
            return BadRequest("Sale not found");

        _= await _redisService.RemoveAsync(saleId.ToString(), cancellationToken);
        
        // salvar venda em db

        return Created(string.Empty, new ApiResponseWithData<UpdateSalesResponse>
        {
            Success = true,
            Message = "Sale closed successfully",
            Data = null
        });
    }

    [HttpPost("/add-item")]
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
        if (product is null || product.Stock < request.Quantity)
            return BadRequest("Invalid Product or Insufficient Stock");
        
        var saleComand = _mapper.Map<GetSalesCommand>(request.SaleId);
        var sale = await _mediator.Send(saleComand, cancellationToken);
        if (sale is null)
            return BadRequest("Sale not found");

        var item = sale.Items.FirstOrDefault(i => i.ProductId == request.Product);
        if (item is not null && item.Quantity + request.Quantity > 20  && request.Action == HandleItem.Add)
            return BadRequest("Max quantity of 20 items per product reached");
        
       
        var updateComand = _mapper.Map<UpdateSalesCommand>(request);
        var updatedSale  =  await _mediator.Send(updateComand, cancellationToken);
      
        return Created(string.Empty, new ApiResponseWithData<UpdateSalesResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = _mapper.Map<UpdateSalesResponse>(updatedSale)
        });
        
    }



}