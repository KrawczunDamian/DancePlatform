using AutoMapper;
using DancePlatform.Application.Features.DocumentTypes.Commands.AddEdit;
using DancePlatform.Application.Features.DocumentTypes.Queries.GetAll;
using DancePlatform.Application.Features.DocumentTypes.Queries.GetById;
using DancePlatform.Domain.Entities.Misc;

namespace DancePlatform.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}