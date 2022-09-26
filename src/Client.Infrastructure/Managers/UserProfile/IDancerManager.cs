using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DancePlatform.Client.Infrastructure.Managers.UserProfile
{
    public interface IDancerManager : IManager
    {
        Task<IResult<List<GetDancersWithProfileInfoResponse>>> GetAllAsync();
        /*Task<IResult<GetTeamByIdResponse>> GetByIdAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditTeamCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");*/
    }
}