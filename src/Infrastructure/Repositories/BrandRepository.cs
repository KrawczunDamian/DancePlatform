using DancePlatform.Application.Interfaces.Repositories;
using DancePlatform.Domain.Entities.Catalog;

namespace DancePlatform.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}