﻿using DancePlatform.Domain.Contracts;
using DancePlatform.Domain.Entities.UserProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DancePlatform.Domain.Entities.Organisations
{
    public class Team : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "text")]
        public string ProfilePictureURL { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}