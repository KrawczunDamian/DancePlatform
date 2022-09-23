using AutoMapper;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Commands.AddEdit
{
    public partial class AddEditDancerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Height { get; set; }

        internal class AddEditDancerCommandHandler : IRequestHandler<AddEditDancerCommand, Result<int>>
        {
            private readonly IMapper _mapper;
            private readonly IStringLocalizer<AddEditDancerCommandHandler> _localizer;
            private readonly IUnitOfWork<int> _unitOfWork;

            public AddEditDancerCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditDancerCommandHandler> localizer)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _localizer = localizer;
            }

            public async Task<Result<int>> Handle(AddEditDancerCommand command, CancellationToken cancellationToken)
            {
                if (command.Id == 0)
                {
                    var dancer = _mapper.Map<Dancer>(command);
                    await _unitOfWork.Repository<Dancer>().AddAsync(dancer);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(dancer.Id, _localizer["Dancer Saved"]);
                }
                else
                {
                    var dancer = await _unitOfWork.Repository<Dancer>().GetByIdAsync(command.Id);
                    if (dancer != null)
                    {
                        dancer.Nickname = command.Nickname ?? dancer.Nickname;
                        dancer.Weight = command.Weight;
                        dancer.Height = command.Height;
                        await _unitOfWork.Repository<Dancer>().UpdateAsync(dancer);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(dancer.Id, _localizer["Dancer Updated"]);
                    }
                    else
                    {
                        return await Result<int>.FailAsync(_localizer["Dancer Not Found!"]);
                    }
                }
            }
        }
    }
}