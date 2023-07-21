using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly SqlServerDataContext _context;
        public TicketRepository(SqlServerDataContext context)
        {
            _context = context;
        }

        public void AddTicket(TicketAddDTO ticket)
        {
            Ticket t = new Ticket
            {
                Subject = ticket.Subject,
                Content = ticket.Content,
                CreationDate = DateTime.Now,
                UserId = ticket.UserId
            };
            _context.Tickets.Add(t);
            _context.SaveChanges();
        }

        public void DeleteTicket(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id)
            ?? throw new Exception("Ticket doesn't exist.");
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }

        public Ticket GetTicket(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id)
            ?? throw new Exception("Ticket doesn't exist.");
            return ticket;
        }

        public ICollection<TicketDisplayDTO> GetTickets()
        {
            ICollection<Ticket> fullTickets = _context.Tickets.OrderBy(t => t.Id).ToList();
            ICollection<TicketDisplayDTO> tickets = new List<TicketDisplayDTO>();
            foreach (var fullTicket in fullTickets)
            {
                TicketDisplayDTO ticket = new TicketDisplayDTO
                {
                    Id = fullTicket.Id,
                    UserId = fullTicket.UserId,
                    Subject = fullTicket.Subject,
                    CreationDate = fullTicket.CreationDate,
                };
                tickets.Add(ticket);
            }
            return tickets;
        }

        public ICollection<TicketDisplayDTO> GetUserTickets(int id)
        {
            ICollection<Ticket> fullTickets = _context.Tickets
                .Where(c => c.UserId == id)
                .OrderBy(c => c.Id).ToList();
            ICollection<TicketDisplayDTO> tickets = new List<TicketDisplayDTO>();
            foreach (var fullTicket in fullTickets)
            {
                TicketDisplayDTO ticket = new TicketDisplayDTO
                {
                    Id = fullTicket.Id,
                    UserId = fullTicket.UserId,
                    Subject = fullTicket.Subject,
                    CreationDate = fullTicket.CreationDate,
                };
                tickets.Add(ticket);
            }
            return tickets;
        }
    }
}
