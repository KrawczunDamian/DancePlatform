using AutoMapper;
using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Domain.Entities.UserProfile;

namespace DancePlatform.Application.Mappings
{
    public class DancerProfile : Profile
    {
        public DancerProfile()
        {
            CreateMap<GetAllDancersResponse, Dancer>().ReverseMap();
        }
    }
}