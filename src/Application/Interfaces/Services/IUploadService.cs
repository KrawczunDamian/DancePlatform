using DancePlatform.Application.Requests;

namespace DancePlatform.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}