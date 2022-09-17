using DancePlatform.Application.Features.Teams.Commands.Delete;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Relations.Photos;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Commands.RemoveTeamPicture
{
    public class RemoveTeamPictureCommand : IRequest<Result<int>>
    {
        public string PictureUrl { get; set; }
        public int TeamId { get; set; }
    }

    internal class RemoveTeamPictureCommandHandler : IRequestHandler<RemoveTeamPictureCommand, Result<int>>
    {
        private readonly IStringLocalizer<DeleteTeamCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public RemoveTeamPictureCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteTeamCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(RemoveTeamPictureCommand command, CancellationToken cancellationToken)
        {
            var teamPhotos = await _unitOfWork.Repository<TeamPhoto>().GetAllAsync();
            var pictureToRemove = teamPhotos.FirstOrDefault(x => x.PictureURL == command.PictureUrl && x.TeamId == command.TeamId && x.IsDeleted != true);
            if (pictureToRemove != null)
            {
                await _unitOfWork.Repository<TeamPhoto>().DeleteAsync(pictureToRemove);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(pictureToRemove.Id, _localizer["Picture Removed"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Picture Not Found!"]);
            }
        }
    }
}