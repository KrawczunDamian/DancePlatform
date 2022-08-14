using System.Collections.Generic;
using System.Threading.Tasks;
using DancePlatform.Application.Interfaces.Common;
using DancePlatform.Application.Requests.Identity;
using DancePlatform.Application.Responses.Identity;
using DancePlatform.Shared.Wrapper;

namespace DancePlatform.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}