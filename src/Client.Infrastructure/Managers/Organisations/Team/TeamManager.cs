using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Client.Infrastructure.Extensions;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;

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

        public async Task<IResult<int>> SaveAsync(AddEditTeamCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TeamsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}