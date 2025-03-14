using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class UpdateProductRequestProfile : Profile
{
    public UpdateProductRequestProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
    }
}