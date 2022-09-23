using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Dancers.Queries.GetById;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.UserProfile
{
    public interface IDancerManager : IManager
    {
        Task<IResult<List<GetDancersWithProfileInfoResponse>>> GetAllAsync();
        Task<IResult<GetDancerByAccountIdResponse>> GetByAccountIdAsync(string id);
    }
}