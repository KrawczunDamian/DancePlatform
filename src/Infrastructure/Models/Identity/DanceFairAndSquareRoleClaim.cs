using System;
using DancePlatform.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace DancePlatform.Infrastructure.Models.Identity
{
    public class DanceFairAndSquareRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        public string Description { get; set; }
        public string Group { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual DanceFairAndSquareRole Role { get; set; }

        public DanceFairAndSquareRoleClaim() : base()
        {
        }

        public DanceFairAndSquareRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}