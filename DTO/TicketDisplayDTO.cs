using WebAPI.Services;

namespace WebAPI.DTO
{
    public class TicketDisplayDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; }
        public DateTime CreationDate { get; set; }
    }
}