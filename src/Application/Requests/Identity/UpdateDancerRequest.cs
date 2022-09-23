using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DancePlatform.Application.Requests.Identity
{
    public class UpdateDancerRequest
    {
        public int? Id { get; set; }
        [Required]
        public int? Weight { get; set; }
        [Required]
        public int? Height { get; set; }
        [Required]
        public string Nickname { get; set; }
    }
}