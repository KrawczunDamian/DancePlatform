﻿using DancePlatform.Domain.Contracts;
using System;

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
    }
}