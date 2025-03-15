using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

public class DeleteSalesProfile: Profile
{
    public DeleteSalesProfile()
    {
        CreateMap<DeleteSalesCommand, Domain.Entities.Sales>();

    }
    
    
}