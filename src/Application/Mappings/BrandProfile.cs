using AutoMapper;
using DancePlatform.Application.Features.Brands.Commands.AddEdit;
using DancePlatform.Application.Features.Brands.Queries.GetAll;
using DancePlatform.Application.Features.Brands.Queries.GetById;
using DancePlatform.Domain.Entities.Catalog;

namespace DancePlatform.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}