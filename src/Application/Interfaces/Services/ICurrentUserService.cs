using DancePlatform.Application.Interfaces.Common;

namespace DancePlatform.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}