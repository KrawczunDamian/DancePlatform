using AutoMapper;
using DancePlatform.Application.Requests.Identity;
using DancePlatform.Application.Responses.Identity;
using DancePlatform.Infrastructure.Models.Identity;

namespace DancePlatform.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, DanceFairAndSquareRoleClaim>()
                .ForMember(nameof(DanceFairAndSquareRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(DanceFairAndSquareRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, DanceFairAndSquareRoleClaim>()
                .ForMember(nameof(DanceFairAndSquareRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(DanceFairAndSquareRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}