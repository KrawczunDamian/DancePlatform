using DancePlatform.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DancePlatform.Application.Interfaces.Chat;
using DancePlatform.Application.Models.Chat;

namespace DancePlatform.Infrastructure.Models.Identity
{
    public class DanceFairAndSquareUser : IdentityUser<string>, IChatUser, IAuditableEntity<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string CreatedBy { get; set; }

        [Column(TypeName = "text")]
        public string ProfilePictureDataUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<ChatHistory<DanceFairAndSquareUser>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<DanceFairAndSquareUser>> ChatHistoryToUsers { get; set; }

        public DanceFairAndSquareUser()
        {
            ChatHistoryFromUsers = new HashSet<ChatHistory<DanceFairAndSquareUser>>();
            ChatHistoryToUsers = new HashSet<ChatHistory<DanceFairAndSquareUser>>();
        }
    }
}