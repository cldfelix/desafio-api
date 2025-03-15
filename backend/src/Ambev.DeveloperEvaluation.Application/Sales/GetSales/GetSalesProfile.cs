using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesProfile: Profile
{
    public GetSalesProfile()
    {
        CreateMap<Domain.Entities.Sales, GetSalesResult>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));

    }
    
    
}