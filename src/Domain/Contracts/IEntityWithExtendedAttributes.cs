using System.Collections.Generic;

namespace DancePlatform.Domain.Contracts
{
    public interface IEntityWithExtendedAttributes<TExtendedAttribute>
    {
        public ICollection<TExtendedAttribute> ExtendedAttributes { get; set; }
    }
}