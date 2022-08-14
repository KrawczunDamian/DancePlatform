using DancePlatform.Application.Features.Products.Commands.AddEdit;
using DancePlatform.Application.Features.Products.Queries.GetAllPaged;
using DancePlatform.Application.Requests.Catalog;
using DancePlatform.Shared.Wrapper;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}