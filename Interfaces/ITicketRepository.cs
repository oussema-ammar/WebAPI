using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ITicketRepository
    {
        public void AddTicket(TicketAddDTO ticket);
        public Ticket GetTicket(int id);
        public ICollection<TicketDisplayDTO> GetTickets();
        public ICollection<TicketDisplayDTO> GetUserTickets(int id);
        public void DeleteTicket(int id);
    }
}
