using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetListProduct;
using AutoMapper;
using GetListProductRequest = Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser.GetListProductRequest;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class GetListProductRequestProfile : Profile
{
    public GetListProductRequestProfile()
    {
        CreateMap<GetListProductRequest, GetListProductCommand>();
        CreateMap<GetListProductResult, GetListProductResponse>();
    }
}