using DancePlatform.Domain.Contracts;
using DancePlatform.Domain.Entities.Organisations;
using DancePlatform.Domain.Entities.UserProfile;
using System;

namespace DancePlatform.Domain.Entities.Relations.Photos
{
    public class TeamPhoto : AuditableEntity<int>
    {
        public int TeamId { get; set; }
        public string PictureURL { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual Team Team { get; set; }
    }
}