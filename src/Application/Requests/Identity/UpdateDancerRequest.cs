using System.ComponentModel.DataAnnotations;

namespace DancePlatform.Application.Requests.Identity
{
    public class UpdateDancerRequest
    {
        public string Weight { get; set; }
        public string Height { get; set; }
        [Required]
        public string Nickname { get; set; }
    }
}