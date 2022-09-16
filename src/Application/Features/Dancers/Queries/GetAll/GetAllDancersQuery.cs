using AutoMapper;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Application.Interfaces.Services.Account;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Dancers.Queries.GetAll
{
    public class GetAllDancersQuery : IRequest<Result<List<GetDancersWithProfileInfoResponse>>>
    {
        public GetAllDancersQuery()
        {
        }
    }

    internal class GetAllDancersCachedQueryHandler : IRequestHandler<GetAllDancersQuery, Result<List<GetDancersWithProfileInfoResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public GetAllDancersCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<Result<List<GetDancersWithProfileInfoResponse>>> Handle(GetAllDancersQuery request, CancellationToken cancellationToken)
        {
            var allDancers = await _unitOfWork.Repository<Dancer>().GetAllAsync();
            var dancerList = new List<GetDancersWithProfileInfoResponse>();

            foreach (var dancer in allDancers)
            {
                var dancerProfileInfo = await _accountService.GetUserProfileInfo(dancer.CreatedBy);
                var dancerWithProfileInfo = new GetDancersWithProfileInfoResponse()
                {
                    Id = dancer.Id,
                    Nickname = dancer.Nickname,
                    Height = dancer.Height,
                    Weight = dancer.Weight,
                    CreatedBy = dancer.CreatedBy,
                    FirstName = dancerProfileInfo.Data.FirstName,
                    LastName = dancerProfileInfo.Data.LastName,
                    ProfilePictureDataUrl = dancerProfileInfo.Data.ProfilePictureDataUrl ?? string.Empty,
                };
                if (dancer.IsDeleted != true)
                {
                    dancerList.Add(dancerWithProfileInfo);
                }
            }
            return await Result<List<GetDancersWithProfileInfoResponse>>.SuccessAsync(dancerList);
        }
    }
}