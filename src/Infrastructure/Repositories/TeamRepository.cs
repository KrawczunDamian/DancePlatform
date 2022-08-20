using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Organisations;

namespace DancePlatform.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IRepositoryAsync<Team, int> _repository;

        public TeamRepository(IRepositoryAsync<Team, int> repository)
        {
            _repository = repository;
        }
    }
}