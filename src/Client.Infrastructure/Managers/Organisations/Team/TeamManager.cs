using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Commands.AddMember;
using DancePlatform.Application.Features.Teams.Commands.UpdateProfilePicture;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Client.Infrastructure.Extensions;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.Organisations.Team
{
    public class TeamManager : ITeamManager
    {
        private readonly HttpClient _httpClient;

        public TeamManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.TeamsEndpoints.Export
                : Routes.TeamsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.TeamsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllTeamsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.TeamsEndpoints.GetAll);
            return await response.ToResult<List<GetAllTeamsResponse>>();
        }
        public async Task<IResult<GetTeamByIdResponse>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{Routes.TeamsEndpoints.GetById}/{id}");
            return await response.ToResult<GetTeamByIdResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditTeamCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TeamsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
        public async Task<IResult<string>> GetProfilePictureAsync(int teamId)
        {
            var response = await _httpClient.GetAsync($"{Routes.TeamsEndpoints.GetProfilePicture}/{teamId}");
            return await response.ToResult<string>();
        }
        public async Task<IResult<int>> UpdateProfilePictureAsync(UpdateProfilePictureTeamCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TeamsEndpoints.UpdateProfilePicture, request);
            return await response.ToResult<int>();
        }
        public async Task<IResult<int>> AddTeamMemberAsync(AddTeamMemberCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TeamsEndpoints.AddTeamMember, request);
            return await response.ToResult<int>();
        }
        public async Task<IResult<List<GetDancersWithProfileInfoResponse>>> GetTeamMembersAsync(int teamId)
        {
            var response = await _httpClient.GetAsync($"{Routes.TeamsEndpoints.GetTeamMembers}/{teamId}");
            return await response.ToResult<List<GetDancersWithProfileInfoResponse>>();
        }
        public async Task<IResult<int>> RemoveMemberAsync(int teamId, int dancerId)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.TeamsEndpoints.RemoveMember}/{teamId}/{dancerId}");
            return await response.ToResult<int>();
        }
        public async Task<IResult<int>> UploadTeamPicutreAsync(UploadPictureTeamCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TeamsEndpoints.UploadTeamPicture, request);
            return await response.ToResult<int>();
        }
        public async Task<IResult<List<string>>> GetTeamGalleryAsync(int teamId)
        {
            var response = await _httpClient.GetAsync($"{Routes.TeamsEndpoints.GetTeamGallery}/{teamId}");
            return await response.ToResult<List<string>>();
        }
    }
}