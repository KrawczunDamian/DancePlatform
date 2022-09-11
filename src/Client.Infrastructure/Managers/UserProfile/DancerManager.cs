using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Client.Infrastructure.Extensions;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.UserProfile
{
    public class DancerManager : IDancerManager
    {
        private readonly HttpClient _httpClient;

        public DancerManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /*public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.DancersEndpoints.Export
                : Routes.DancersEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.DancersEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }*/

        public async Task<IResult<List<GetAllDancersResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DancersEndpoints.GetAll);
            return await response.ToResult<List<GetAllDancersResponse>>();
        }
        /*public async Task<IResult<GetDancerByIdResponse>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{Routes.DancersEndpoints.GetById}/{id}");
            return await response.ToResult<GetDancerByIdResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditDancerCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DancersEndpoints.Save, request);
            return await response.ToResult<int>();
        }*/
    }
}