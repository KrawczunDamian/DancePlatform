using DancePlatform.Application.Models.Chat;
using DancePlatform.Application.Responses.Identity;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using DancePlatform.Application.Interfaces.Chat;

namespace DancePlatform.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}