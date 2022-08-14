using AutoMapper;
using DancePlatform.Infrastructure.Models.Identity;
using DancePlatform.Application.Responses.Identity;

namespace DancePlatform.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}