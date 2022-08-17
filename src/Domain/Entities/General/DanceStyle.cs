using DancePlatform.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DancePlatform.Domain.Entities.General
{
    public class DanceStyle : AuditableEntity<int>
    {
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}