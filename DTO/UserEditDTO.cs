﻿namespace WebAPI.DTO
{
    public class UserEditDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhotoPath { get; set; }
    }
}
