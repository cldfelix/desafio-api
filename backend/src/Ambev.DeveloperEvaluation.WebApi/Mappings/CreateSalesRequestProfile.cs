using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateSalesRequestProfile : Profile
{
    public CreateSalesRequestProfile()
    {
        CreateMap<CreateSalesRequest, CreateSalesCommand>();
        CreateMap<CreateSalesResult, CreateSalesResponse>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));
    }
    
}