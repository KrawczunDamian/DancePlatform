using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Application.Interfaces.Services.Account;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.Relations;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Models;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Queries.GetProfilePicture
{
    public partial class GetTeamMembersQuery : IRequest<Result<IList<GetDancersWithProfileInfoResponse>>>
    {
        [Required]
        public int TeamId { get; set; }
    }

    internal class GetTeamMembersQueryHandler : IRequestHandler<GetTeamMembersQuery, Result<IList<GetDancersWithProfileInfoResponse>>>
    {
        private readonly IStringLocalizer<GetTeamMembersQueryHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IAccountService _accountService;

        public GetTeamMembersQueryHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<GetTeamMembersQueryHandler> localizer, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _accountService = accountService;
        }

        public async Task<Result<IList<GetDancersWithProfileInfoResponse>>> Handle(GetTeamMembersQuery command, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(command.TeamId);
            if (team != null)
            {
                var allTeamMembers = await _unitOfWork.Repository<TeamDancer>().GetAllAsync();
                var teamMembers = new List<GetDancersWithProfileInfoResponse>();
                foreach (var member in allTeamMembers)
                {
                    if (member.TeamId != team.Id || member.IsDeleted == true)
                    {
                        break;
                    }
                    var dancer = await _unitOfWork.Repository<Dancer>().GetByIdAsync(member.DancerId);
                    var dancerProfileInfo = await _accountService.GetUserProfileInfo(dancer.CreatedBy);
                    var dancerWithProfileInfo = new GetDancersWithProfileInfoResponse()
                    {
                        Id = dancer.Id,
                        Nickname = dancer.Nickname,
                        Height = dancer.Height,
                        Weight = dancer.Weight,
                        CreatedById = dancer.CreatedBy,
                        FirstName = dancerProfileInfo.Data.FirstName,
                        LastName = dancerProfileInfo.Data.LastName,
                        ProfilePictureDataUrl = dancerProfileInfo.Data.ProfilePictureDataUrl
                    };
                    if (dancer.IsDeleted != true)
                    {
                        teamMembers.Add(dancerWithProfileInfo);
                    }
                }
                return await Result<IList<GetDancersWithProfileInfoResponse>>.SuccessAsync(data: teamMembers);
            }
            return await Result<IList<GetDancersWithProfileInfoResponse>>.FailAsync(_localizer["Team Not Found!"]);
        }
    }
}