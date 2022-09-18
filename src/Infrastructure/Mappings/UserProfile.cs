using AutoMapper;
using DancePlatform.Infrastructure.Models.Identity;
using DancePlatform.Application.Responses.Identity;

namespace DancePlatform.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, DanceFairAndSquareUser>().ReverseMap();
            CreateMap<ChatUserResponse, DanceFairAndSquareUser>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}