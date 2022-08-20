using AutoMapper;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Commands.AddEdit
{
    public partial class AddEditTeamCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Tax { get; set; }
    }

    internal class AddEditTeamCommandHandler : IRequestHandler<AddEditTeamCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditTeamCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditTeamCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditTeamCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditTeamCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var team = _mapper.Map<Team>(command);
                await _unitOfWork.Repository<Team>().AddAsync(team);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllTeamsCacheKey);
                return await Result<int>.SuccessAsync(team.Id, _localizer["Team Saved"]);
            }
            else
            {
                var team = await _unitOfWork.Repository<Team>().GetByIdAsync(command.Id);
                if (team != null)
                {
                    team.Name = command.Name ?? team.Name;
                    team.Description = command.Description ?? team.Description;
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
}