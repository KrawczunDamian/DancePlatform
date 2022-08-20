using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.Organisations.Team
{
    public interface ITeamManager : IManager
    {
        Task<IResult<List<GetAllTeamsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditTeamCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}