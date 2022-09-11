using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.Relations;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Queries.GetProfilePicture
{
    public partial class GetTeamMembersQuery : IRequest<Result<IList<Dancer>>>
    {
        [Required]
        public int TeamId { get; set; }
    }

    internal class GetTeamMembersQueryHandler : IRequestHandler<GetTeamMembersQuery, Result<IList<Dancer>>>
    {
        private readonly IStringLocalizer<GetTeamMembersQueryHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetTeamMembersQueryHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<GetTeamMembersQueryHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<IList<Dancer>>> Handle(GetTeamMembersQuery command, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(command.TeamId);
            if (team != null)
            {
                var allTeamMembers = await _unitOfWork.Repository<TeamDancer>().GetAllAsync();
                var teamMembers = new List<Dancer>();
                foreach (var member in allTeamMembers)
                {
                    if (member.TeamId != team.Id || member.IsDeleted == true)
                    {
                        break;
                    }
                    var dancer = await _unitOfWork.Repository<Dancer>().GetByIdAsync(member.DancerId);
                    if (dancer.IsDeleted != true)
                    {
                        teamMembers.Add(dancer);
                    }
                }
                return await Result<IList<Dancer>>.SuccessAsync(data: teamMembers);
            }
            return await Result<IList<Dancer>>.FailAsync(_localizer["Team Not Found!"]);
        }
    }
}