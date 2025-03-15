using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

public class CreateSalesProfile : Profile
{
    public CreateSalesProfile()
    {
        CreateMap<CreateSalesCommand, Domain.Entities.Sales>();
        CreateMap<Domain.Entities.Sales, CreateSalesResult>();
    }
}