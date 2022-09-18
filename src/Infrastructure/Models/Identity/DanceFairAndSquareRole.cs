using System;
using System.Collections.Generic;
using DancePlatform.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace DancePlatform.Infrastructure.Models.Identity
{
    public class DanceFairAndSquareRole : IdentityRole, IAuditableEntity<string>
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual ICollection<DanceFairAndSquareRoleClaim> RoleClaims { get; set; }

        public DanceFairAndSquareRole() : base()
        {
            RoleClaims = new HashSet<DanceFairAndSquareRoleClaim>();
        }

        public DanceFairAndSquareRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<DanceFairAndSquareRoleClaim>();
            Description = roleDescription;
        }
    }
}