using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Queries.GetProfilePicture
{
    public partial class GetProfilePictureTeamQuery : IRequest<Result<string>>
    {
        [Required]
        public int TeamId { get; set; }
    }

    internal class GetProfilePictureTeamQueryHandler : IRequestHandler<GetProfilePictureTeamQuery, Result<string>>
    {
        private readonly IStringLocalizer<GetProfilePictureTeamQueryHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetProfilePictureTeamQueryHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<GetProfilePictureTeamQueryHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(GetProfilePictureTeamQuery command, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(command.TeamId);
            if (team != null)
            {
                return await Result<string>.SuccessAsync(data: team.ProfilePictureURL);
            }
            return await Result<string>.FailAsync(_localizer["Team Not Found!"]);
        }
    }
}