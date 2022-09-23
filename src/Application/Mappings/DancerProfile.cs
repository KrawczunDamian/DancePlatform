using AutoMapper;
using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Domain.Entities.UserProfile;

namespace DancePlatform.Application.Mappings
{
    public class DancerProfile : Profile
    {
        public DancerProfile()
        {
            CreateMap<GetDancersWithProfileInfoResponse, Dancer>().ReverseMap();
            CreateMap<AddEditDancerCommand, Dancer>().ReverseMap();
        }
    }
}