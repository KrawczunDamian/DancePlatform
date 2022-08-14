using System.Collections.Generic;
using System.Threading.Tasks;
using DancePlatform.Application.Requests.Identity;
using DancePlatform.Application.Responses.Identity;
using DancePlatform.Shared.Wrapper;

namespace DancePlatform.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}