using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Commands.Delete
{
    public class DeleteTeamCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteTeamCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteTeamCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteTeamCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(command.Id);
            if (team != null)
            {
                await _unitOfWork.Repository<Team>().DeleteAsync(team);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllTeamsCacheKey);
                return await Result<int>.SuccessAsync(team.Id, _localizer["Team Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Team Not Found!"]);
            }
        }
    }
}