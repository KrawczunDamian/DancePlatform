using AutoMapper;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Wrapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Dancers.Queries.GetById
{
    public class GetDancerByAccountIdQuery : IRequest<Result<GetDancerByAccountIdResponse>>
    {
        public string AccountId { get; set; }
    }

    internal class GetDancerByAccountIdQueryHandler : IRequestHandler<GetDancerByAccountIdQuery, Result<GetDancerByAccountIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetDancerByAccountIdQueryHandler (IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetDancerByAccountIdResponse>> Handle(GetDancerByAccountIdQuery query, CancellationToken cancellationToken)
        {
            var allDancers = await _unitOfWork.Repository<Dancer>().GetAllAsync();
            var dancer = allDancers.FirstOrDefault(x => x.CreatedBy == query.AccountId && x.IsDeleted != true);
            if (dancer == null)
            {
                return await Result<GetDancerByAccountIdResponse>.SuccessAsync(new GetDancerByAccountIdResponse() { Id = 0 });
            }
            var mappedDancer = new GetDancerByAccountIdResponse()
            {
                Id = dancer.Id,
                Nickname = dancer.Nickname,
                Weight = dancer.Weight,
                Height = dancer.Height
            };
            return await Result<GetDancerByAccountIdResponse>.SuccessAsync(mappedDancer);
        }
    }
}