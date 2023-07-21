using System.ComponentModel.DataAnnotations;
using WebAPI.Services;

namespace WebAPI.DTO
{
    public class TicketAddDTO
    {
        [UserIsClient]
        public int UserId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
