using AutoMapper;
using DancePlatform.Application.Enums;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Application.Interfaces.Services;
using DancePlatform.Application.Requests;
using DancePlatform.Application.Requests.Organisations.Team;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Commands.UpdateProfilePicture
{
    public partial class UploadPictureTeamCommand : IRequest<Result<int>>
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public UploadType UploadType { get; set; }
        [Required]
        public byte[] Data { get; set; }
    }

    internal class UploadPictureTeamCommandHandler : IRequestHandler<UploadPictureTeamCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UploadPictureTeamCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;

        public UploadPictureTeamCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<UploadPictureTeamCommandHandler> localizer, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _uploadService = uploadService;
        }

        public async Task<Result<int>> Handle(UploadPictureTeamCommand command, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(command.TeamId);
            if (team != null)
            {
                var request = new UploadRequest()
                {
                    Data = command.Data,
                    Extension = command.Extension,
                    FileName = command.FileName,
                    UploadType = command.UploadType
                };
                var filePath = _uploadService.UploadAsync(request);
                team.ProfilePictureURL = filePath;
                await _unitOfWork.Repository<Team>().UpdateAsync(team);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllTeamsCacheKey);
                return await Result<int>.SuccessAsync(team.Id, _localizer["Team Updated"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Team Not Found!"]);
            }

        }
    }
}