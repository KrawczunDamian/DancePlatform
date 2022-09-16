using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Commands.AddMember;
using DancePlatform.Application.Features.Teams.Commands.UpdateProfilePicture;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.Organisations.Team
{
    public interface ITeamManager : IManager
    {
        Task<IResult<List<GetAllTeamsResponse>>> GetAllAsync();
        Task<IResult<GetTeamByIdResponse>> GetByIdAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditTeamCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
        Task<IResult<string>> GetProfilePictureAsync(int teamId);
        Task<IResult<int>> UpdateProfilePictureAsync(UpdateProfilePictureTeamCommand request);
        Task<IResult<int>> AddTeamMemberAsync(AddTeamMemberCommand request);
        Task<IResult<List<GetDancersWithProfileInfoResponse>>> GetTeamMembersAsync(int teamId);
        Task<IResult<int>> RemoveMemberAsync(int teamId, int dancerId);
    }
}