using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("v1/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        public TicketController(ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
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
            try
            {
                var ticket = _ticketRepository.GetTicket(ticketId);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("/users/tickets/{userId}"), Authorize]
        public IActionResult GetUserTickets(int userId)
        {
            try
            {
                if (userId == GetMe() || _userRepository.GetUser(GetMe()).Role == "Admin")
                {
                    var tickets = _ticketRepository.GetUserTickets(userId);
                    return Ok(tickets);
                }
                return BadRequest("Forbidden");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}"),Authorize]
        public IActionResult DeleteTicket(int id)
        {
            var ticket = _ticketRepository.GetTicket(id);
            if (ticket.UserId == GetMe() || _userRepository.GetUser(GetMe()).Role == "Admin")
            {
                _ticketRepository.DeleteTicket(id);
                return Ok();
            }
            return BadRequest();
        }

        private int GetMe()
        {
            var id = User.FindFirstValue("Id");
            return int.Parse(id);
        }
    }
}
