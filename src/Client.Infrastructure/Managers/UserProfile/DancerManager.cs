using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Dancers.Queries.GetById;
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
        public async Task<IResult<List<GetDancersWithProfileInfoResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DancersEndpoints.GetAll);
            return await response.ToResult<List<GetDancersWithProfileInfoResponse>>();
        }
        public async Task<IResult<GetDancerByAccountIdResponse>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{Routes.DancersEndpoints.GetByAccountId}/{id}");
            return await response.ToResult<GetDancerByAccountIdResponse>();
        }
    }
}