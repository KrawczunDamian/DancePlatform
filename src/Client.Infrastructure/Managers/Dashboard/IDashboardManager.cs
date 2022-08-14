using DancePlatform.Shared.Wrapper;
using System.Threading.Tasks;
using DancePlatform.Application.Features.Dashboards.Queries.GetData;

namespace DancePlatform.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}