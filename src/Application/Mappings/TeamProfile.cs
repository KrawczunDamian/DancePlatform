using AutoMapper;
using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Application.Features.Teams.Queries.GetGallery;
using DancePlatform.Domain.Entities.Organisations;

namespace DancePlatform.Application.Mappings
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<AddEditTeamCommand, Team>().ReverseMap();
            CreateMap<GetTeamByIdResponse, Team>().ReverseMap();
            CreateMap<GetAllTeamsResponse, Team>().ReverseMap();
        }
    }
}