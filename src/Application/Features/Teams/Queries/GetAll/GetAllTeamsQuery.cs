using AutoMapper;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Queries.GetAll
{
    public class GetAllTeamsQuery : IRequest<Result<List<GetAllTeamsResponse>>>
    {
        public GetAllTeamsQuery()
        {
        }
    }

    internal class GetAllTeamsCachedQueryHandler : IRequestHandler<GetAllTeamsQuery, Result<List<GetAllTeamsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllTeamsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllTeamsResponse>>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Team>>> getAllTeams = () => _unitOfWork.Repository<Team>().GetAllAsync();
            var brandList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllTeamsCacheKey, getAllTeams);
            var mappedTeams = _mapper.Map<List<GetAllTeamsResponse>>(brandList);
            return await Result<List<GetAllTeamsResponse>>.SuccessAsync(mappedTeams);
        }
    }
}