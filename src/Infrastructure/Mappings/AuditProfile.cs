using AutoMapper;
using DancePlatform.Infrastructure.Models.Audit;
using DancePlatform.Application.Responses.Audit;

namespace DancePlatform.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}