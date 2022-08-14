using DancePlatform.Application.Requests.Mail;
using System.Threading.Tasks;

namespace DancePlatform.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}