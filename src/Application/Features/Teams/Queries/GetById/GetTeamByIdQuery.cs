using AutoMapper;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Queries.GetById
{
    public class GetTeamByIdQuery : IRequest<Result<GetTeamByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Result<GetTeamByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetTeamByIdResponse>> Handle(GetTeamByIdQuery query, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<Team>().GetByIdAsync(query.Id);
            var mappedTeam = _mapper.Map<GetTeamByIdResponse>(team);
            return await Result<GetTeamByIdResponse>.SuccessAsync(mappedTeam);
        }
    }
}