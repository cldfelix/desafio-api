using Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSave;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class DeleteSalesRequestProfile: Profile
{
    public DeleteSalesRequestProfile()
    {
        CreateMap<GetSalesResult, DeleteSalesCommand>();
        CreateMap<DeleteSalesResult, DeleteSalesResponse>();
        
    }
    
}