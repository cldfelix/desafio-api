using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class UpdateSalesRequestProfile : Profile
{
    public UpdateSalesRequestProfile()
    {
        CreateMap<UpdateSalesRequest, UpdateSalesCommand>();
        CreateMap<SalesAddItem, UpdateSalesCommand>();
  
        CreateMap<UpdateSalesResult, UpdateSalesResponse>();
    }
}