using DancePlatform.Application.Extensions;
using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Application.Interfaces.Services;
using DancePlatform.Application.Specifications.Organisations;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DancePlatform.Application.Features.Teams.Queries.Export
{
    public class ExportTeamsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportTeamsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportTeamsQueryHandler : IRequestHandler<ExportTeamsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportTeamsQueryHandler> _localizer;

        public ExportTeamsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportTeamsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportTeamsQuery request, CancellationToken cancellationToken)
        {
            var teamFilterSpec = new TeamFilterSpecification(request.SearchString);
            var teams = await _unitOfWork.Repository<Team>().Entities
                .Specify(teamFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(teams, mappers: new Dictionary<string, Func<Team, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Description"], item => item.Description }
            }, sheetName: _localizer["Teams"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
