﻿using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [UserIsClient]
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
