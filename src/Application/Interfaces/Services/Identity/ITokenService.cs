using DancePlatform.Application.Interfaces.Common;
using DancePlatform.Application.Requests.Identity;
using DancePlatform.Application.Responses.Identity;
using DancePlatform.Shared.Wrapper;
using System.Threading.Tasks;

namespace DancePlatform.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}