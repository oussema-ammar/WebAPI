using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreationDate { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
    }
}
