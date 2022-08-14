using DancePlatform.Application.Features.Documents.Commands.AddEdit;
using DancePlatform.Application.Features.Documents.Queries.GetAll;
using DancePlatform.Application.Requests.Documents;
using DancePlatform.Shared.Wrapper;
using System.Threading.Tasks;
using DancePlatform.Application.Features.Documents.Queries.GetById;

namespace DancePlatform.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}