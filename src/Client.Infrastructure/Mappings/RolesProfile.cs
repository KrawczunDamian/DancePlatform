using AutoMapper;
using DancePlatform.Application.Requests.Identity;
using DancePlatform.Application.Responses.Identity;

namespace DancePlatform.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}