using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
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
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRedisService _redisService;

    public SalesController(
        ILogger<SalesController> logger, 
        IMediator mediator, 
        IMapper mapper, 
        IRedisService redisService, 
        IUserRepository userRepository, 
        IProductRepository productRepository)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _redisService = redisService;
        _userRepository = userRepository;
        _productRepository = productRepository;
    }


    [HttpPost("/start")]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartSale([FromBody] CreateSaleRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale started successfully",
            Data = await CreateSaleRedis(request)
        });
    }
    [HttpGet("/close-sale/{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloseSale([FromRoute] Guid saleId,
        CancellationToken cancellationToken)
    {
        var saleDb =  await _redisService.GetAsync<UpdateSaleResponse>(saleId.ToString(), cancellationToken);
        if (saleDb is null)
            return BadRequest("Sale not found");

        foreach (var produto in saleDb.Items)
        {
            
        }


        return Created(string.Empty, new ApiResponseWithData<UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale closed successfully",
            Data = saleDb
        });
    }

    [HttpPost("/add-item")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        

        var commandProduct = _mapper.Map<GetProductCommand>(request.Product);
        var product = await _mediator.Send(commandProduct, cancellationToken);
        if (product is null || product.Stock < request.Quantity)
            return BadRequest("Invalid Product or Insufficient Stock");
        
        var saleDb =  await _redisService.GetAsync<UpdateSaleResponse>(request.SaleId.ToString(), cancellationToken);
        if (saleDb is null)
            return BadRequest("Sale not found");
        
        var item = saleDb.Items.Find(i => i.ProductId == request.Product);    
        
        if (item is null)
        {
            item = new Item(request.Product, product.Name, request.Quantity, product.Price);
            saleDb.AddItem(item, request.Action);
        }
        else
        {
            if((item.Quantity + request.Quantity) > 20 && request.Action == HandleItem.Add)
                return BadRequest("Maximum limit: 20 items per product");
            item.UpdateQuantity(request.Quantity, request.Action);
        }

        saleDb.CalculateAmmount();
        var result  =  await _redisService.UpdateAsync<UpdateSaleResponse>(request.SaleId.ToString(), saleDb, cancellationToken);
      
        return Created(string.Empty, new ApiResponseWithData<UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = result
        });
        
    }
    

    private async Task<CreateSaleResponse> CreateSaleRedis(CreateSaleRequest request)
    {
        var sale = new CreateSaleResponse(request.Customer, request.CustomerName, request.Branch);
        await _redisService.SetAsync<CreateSaleResponse>(sale.SaleId.ToString(), sale, TimeSpan.FromHours(6));
        return sale;
    }


}