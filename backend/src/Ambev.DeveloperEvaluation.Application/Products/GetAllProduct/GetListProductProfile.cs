using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;

public class GetListProductProfile : Profile
{
    public GetListProductProfile()
    {
        CreateMap<IEnumerable<Product>, GetListProductResult>();
    }
 
}