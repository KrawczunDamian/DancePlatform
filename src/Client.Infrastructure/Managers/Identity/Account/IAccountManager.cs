using DancePlatform.Application.Requests.Identity;
using DancePlatform.Application.Requests.Organisations.Team;
using DancePlatform.Shared.Wrapper;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.Identity.Account
{
    public interface IAccountManager : IManager
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
        Task<IResult> UpdateDancerAsync(UpdateDancerRequest request, string userId);
    }
}