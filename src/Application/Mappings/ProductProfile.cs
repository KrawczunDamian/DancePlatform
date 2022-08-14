using AutoMapper;
using DancePlatform.Application.Features.Products.Commands.AddEdit;
using DancePlatform.Domain.Entities.Catalog;

namespace DancePlatform.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}