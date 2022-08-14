using System.ComponentModel.DataAnnotations;

namespace DancePlatform.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}