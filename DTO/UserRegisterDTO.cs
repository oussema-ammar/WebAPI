using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StrongPassword]
        public string Password { get; set; } = string.Empty;
        [Required]
        [AllowedRoles]
        public string Role { get; set; } = string.Empty;
    }
}
