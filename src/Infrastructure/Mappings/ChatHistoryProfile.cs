using AutoMapper;
using DancePlatform.Application.Interfaces.Chat;
using DancePlatform.Application.Models.Chat;
using DancePlatform.Infrastructure.Models.Identity;

namespace DancePlatform.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}