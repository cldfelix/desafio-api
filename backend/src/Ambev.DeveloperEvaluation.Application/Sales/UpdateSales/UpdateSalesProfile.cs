using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;

public class UpdateSalesProfile : Profile
{
    public UpdateSalesProfile()
    {
        CreateMap<UpdateSalesCommand, SalesAddItem>();
        CreateMap<Domain.Entities.Sales, UpdateSalesResult>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));

    }
}