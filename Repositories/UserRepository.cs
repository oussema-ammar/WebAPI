﻿using Microsoft.OpenApi.Writers;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerDataContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(SqlServerDataContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public bool RegisterUser(User user)
        {
            //Checking if another User with the same Email already exists
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                throw new Exception("A user with the same email already exists.");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User Login(UserLoginDTO user)
        {
            //Checking the Existence of the Email
            var FoundUser = _context.Users.Where(b => b.Email == user.Email).FirstOrDefault()
            ?? throw new Exception("No user with this email exists.");
            if (!_passwordHasher.VerifyPasswordHash(user.Password, FoundUser.PasswordHash, FoundUser.PasswordSalt))
            {
                throw new Exception("Password is wrong.");
            }
            return FoundUser;
        }

        public User GetUser(int id)
        {
            var user = _context.Users.Where(b => b.Id == id).FirstOrDefault()
            ?? throw new Exception("User doesn't exist.");
            return user;
        }

        public ICollection<UserDisplayDTO> GetUsers()
        {
            ICollection<User> fullUsers= _context.Users.OrderBy(u => u.Id).ToList();
            ICollection<UserDisplayDTO> users = new List<UserDisplayDTO>();
            foreach (var fullUser in fullUsers)
            {
                //map each fullUser onto each user in users 

                UserDisplayDTO user = new UserDisplayDTO
                {
                    Id = fullUser.Id,
                    Name = fullUser.Name,
                    Email = fullUser.Email,
                    Role = fullUser.Role,
                    PasswordHash = fullUser.PasswordHash,
                    PasswordSalt = fullUser.PasswordSalt,
                    PhotoPath = fullUser.PhotoPath,
                    CreationDate = fullUser.CreationDate,
                };
                users.Add(user);
            }
            return users;
        }

        public void UpdateUser(User user)
        {
            // Retrieve the existing user from the database
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            // If the user exists, update its properties with the new values
            if (existingUser != null)
            {
                existingUser = user;

                // Call the SaveChanges method to persist the changes to the database
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception("User doesn't exist.");
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
