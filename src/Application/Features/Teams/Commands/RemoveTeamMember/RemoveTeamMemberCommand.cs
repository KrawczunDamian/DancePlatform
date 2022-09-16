using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.Relations;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Commands.Delete
{
    public class RemoveTeamMemberCommand : IRequest<Result<int>>
    {
        public int DancerId { get; set; }
        public int TeamId { get; set; }
    }

    internal class RemoveTeamMemberCommandHandler : IRequestHandler<RemoveTeamMemberCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteTeamCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public RemoveTeamMemberCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteTeamCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(RemoveTeamMemberCommand command, CancellationToken cancellationToken)
        {
            var teamDancers = await _unitOfWork.Repository<TeamDancer>().GetAllAsync();
            var dancerToRemove = teamDancers.FirstOrDefault(x => x.DancerId == command.DancerId && x.TeamId == command.TeamId && x.IsDeleted != true);
            if (dancerToRemove != null)
            {
                await _unitOfWork.Repository<TeamDancer>().DeleteAsync(dancerToRemove);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllTeamsCacheKey);
                return await Result<int>.SuccessAsync(dancerToRemove.Id, _localizer["Member Removed"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Member Not Found!"]);
            }
        }
    }
}