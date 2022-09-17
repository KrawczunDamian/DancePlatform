using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Application.Interfaces.Services.Account;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.Relations;
using DancePlatform.Domain.Entities.Relations.Photos;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using LazyCache;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Queries.GetGallery
{
    public partial class GetTeamGalleryQuery : IRequest<Result<List<string>>>
    {
        [Required]
        public int TeamId { get; set; }
    }

    internal class GetTeamGalleryQueryHandler : IRequestHandler<GetTeamGalleryQuery, Result<List<string>>>
    {
        private readonly IStringLocalizer<GetTeamGalleryQueryHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IAccountService _accountService;
        private readonly IAppCache _cache;

        public GetTeamGalleryQueryHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<GetTeamGalleryQueryHandler> localizer, IAccountService accountService, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _accountService = accountService;
            _cache = cache;
        }

        public async Task<Result<List<string>>> Handle(GetTeamGalleryQuery request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(request.TeamId);
            if (team != null)
            {
                var allTeamPhotos = await _unitOfWork.Repository<TeamPhoto>().GetAllAsync();
                var teamPhotos = new List<string>();
                foreach (var photo in allTeamPhotos)
                {
                    if (photo.TeamId != team.Id || photo.IsDeleted == true)
                    {
                    }
                    else
                    {
                        teamPhotos.Add(photo.PictureURL);
                    }
                }
                return await Result<List<string>>.SuccessAsync(data: teamPhotos);
            }
            return await Result<List<string>>.FailAsync(_localizer["Team Not Found!"]);
        }
    }
}