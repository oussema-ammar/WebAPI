using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.DTO;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("v1/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpPost, Authorize(Roles ="Client")]
        public IActionResult SendTicket(string subject, string content)
        {
            try
            {
                TicketAddDTO ticket = new TicketAddDTO()
                {
                    Subject = subject,
                    Content = content,
                    UserId = GetMe(),
                };
                _ticketRepository.AddTicket(ticket);
                return Ok("Ticket sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles ="Admin")]
        public IActionResult GetTickets()
        {
            var tickets = _ticketRepository.GetTickets();
            return Ok(tickets);
        }

        [HttpGet("{ticketId}"), Authorize(Roles ="Admin")]
        public IActionResult GetTicket(int ticketId)
        {
            var ticket = _ticketRepository.GetTicket(ticketId);
            return Ok(ticket);
        }

        [HttpDelete,Authorize(Roles ="Admin")]
        public IActionResult DeleteTicket(int id)
        {
            _ticketRepository.DeleteTicket(id);
            return Ok();
        }

        private int GetMe()
        {
            var id = User.FindFirstValue("Id");
            return int.Parse(id);
        }
    }
}
