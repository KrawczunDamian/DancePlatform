using AutoMapper;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Dancers.Queries.GetAll
{
    public class GetAllDancersQuery : IRequest<Result<List<GetAllDancersResponse>>>
    {
        public GetAllDancersQuery()
        {
        }
    }

    internal class GetAllDancersCachedQueryHandler : IRequestHandler<GetAllDancersQuery, Result<List<GetAllDancersResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllDancersCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllDancersResponse>>> Handle(GetAllDancersQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Dancer>>> getAllDancers = () => _unitOfWork.Repository<Dancer>().GetAllAsync();
            var dancerList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllDancersCacheKey, getAllDancers);
            var mappedDancers = _mapper.Map<List<GetAllDancersResponse>>(dancerList);
            return await Result<List<GetAllDancersResponse>>.SuccessAsync(mappedDancers);
        }
    }
}