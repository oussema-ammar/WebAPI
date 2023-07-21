﻿using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [UserIsClient]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}