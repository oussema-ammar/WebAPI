using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class UserEditDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StrongPassword]
        public string Password { get; set; } = string.Empty;
        public string PhotoPath { get; set; }
    }
}
