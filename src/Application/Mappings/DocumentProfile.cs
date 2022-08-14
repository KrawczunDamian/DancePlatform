using AutoMapper;
using DancePlatform.Application.Features.Documents.Commands.AddEdit;
using DancePlatform.Application.Features.Documents.Queries.GetById;
using DancePlatform.Domain.Entities.Misc;

namespace DancePlatform.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}