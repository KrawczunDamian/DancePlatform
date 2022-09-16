using DancePlatform.Application.Interfaces.Common;
using DancePlatform.Application.Requests.Identity;
using DancePlatform.Application.Requests.Organisations.Team;
using DancePlatform.Shared.Models;
using DancePlatform.Shared.Wrapper;
using System.Threading.Tasks;

namespace DancePlatform.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
        Task<IResult<User>> GetUserProfileInfo(string userId);
    }
}