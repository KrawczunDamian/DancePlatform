using DancePlatform.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DancePlatform.Domain.Entities.Organisations
{
    public class Federation : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "text")]
        public string ProfilePictureURL { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}