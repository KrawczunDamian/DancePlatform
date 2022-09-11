using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.Relations;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Commands.AddMember
{
    public class AddTeamMemberCommand : IRequest<Result<int>>
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int DancerId { get; set; }
    }

    internal class AddTeamMemberCommandHandler : IRequestHandler<AddTeamMemberCommand, Result<int>>
    {
        private readonly IStringLocalizer<AddTeamMemberCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddTeamMemberCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddTeamMemberCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddTeamMemberCommand command, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(command.TeamId);
            var dancer = await _unitOfWork.Repository<Dancer>().GetByIdAsync(command.DancerId);
            if (team != null && dancer != null)
            {
                var teamMember = new TeamDancer()
                {
                    Team = team,
                    Dancer = dancer
                };
                await _unitOfWork.Repository<TeamDancer>().AddAsync(teamMember);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(team.Id, _localizer["Team Member Added"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Member Not Found!"]);
            }
        }
    }
}