using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class GetSalesRequestProfile : Profile
{
    public GetSalesRequestProfile()
    {
        CreateMap<GetSalesRequest, GetSalesCommand>();
        CreateMap<GetSalesResult, GetSalesResponse>();
        CreateMap<GetSalesResult, UpdateSalesResponse>();
        CreateMap<GetSalesResult, UpdateSalesCommand>();
        
    }
}