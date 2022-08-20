using DancePlatform.Application.Specifications.Base;
using DancePlatform.Domain.Entities.Organisations;

namespace DancePlatform.Application.Specifications.Organisations
{
    public class TeamFilterSpecification : HeroSpecification<Team>
    {
        public TeamFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
