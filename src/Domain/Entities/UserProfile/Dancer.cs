using DancePlatform.Domain.Contracts;
using DancePlatform.Domain.Entities.General;
using DancePlatform.Domain.Entities.Organisations;
using System;
using System.Collections.Generic;

namespace DancePlatform.Domain.Entities.UserProfile
{
    public class Dancer : AuditableEntity<int>
    {
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Nickname { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual ICollection<DanceStyle> DanceStyles { get; set; }

        public Dancer()
        {
            DanceStyles = new HashSet<DanceStyle>();
        }
    }
}