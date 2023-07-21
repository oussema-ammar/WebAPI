using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("v1/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserController(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _UserRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO request)
        {
            if (!ModelState.IsValid)
            {
                // Model validation failed, return validation errors
                return BadRequest(ModelState);
            }
            _passwordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Role = request.Role,
                CreationDate = DateTime.UtcNow,
                PhotoPath = "",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            try
            {
                //Saving the new User to the Database
                bool t = _UserRepository.RegisterUser(user);
                return Ok("User Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO request)
        {
            try
            {
                var user = new User();
                user = _UserRepository.Login(request);
                string token = _passwordHasher.CreateToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            var user = _UserRepository.GetUsers();
            return Ok(user);
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetUserDetails(int id)
        {
            var user = _UserRepository.GetUser(id);
            return Ok(user);
        }

        [HttpGet("Me"), Authorize]
        public IActionResult GetCurrentUserDetails()
        {
            var user = _UserRepository.GetUser(GetMe());
            return Ok(user);
        }

        [HttpPut, Authorize]
        public IActionResult EditUser(UserEditDTO userUpdateDTO)
        {
            try
            {
                // Retrieve the user from the repository
                var user = _UserRepository.GetUser(GetMe());

                // Map the userDTO properties onto the retrieved user
                user.Name = userUpdateDTO.Name;
                user.Email = userUpdateDTO.Email;
                user.PhotoPath = userUpdateDTO.PhotoPath;
                _passwordHasher.CreatePasswordHash(userUpdateDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                // Save the changes to the database
                _UserRepository.UpdateUser(user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public IActionResult EditUser(int id, UserEditDTO userUpdateDTO)
        {
            try
            {
                // Retrieve the user from the repository
                var retrievedUser = _UserRepository.GetUser(id);

                // Map the userUpdateDTO properties onto the retrieved user
                retrievedUser.Id = id;
                retrievedUser.Name = userUpdateDTO.Name;
                retrievedUser.Email = userUpdateDTO.Email;
                retrievedUser.PhotoPath = userUpdateDTO.PhotoPath;
                _passwordHasher.CreatePasswordHash(userUpdateDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                retrievedUser.PasswordHash = passwordHash;
                retrievedUser.PasswordSalt = passwordSalt;

                // Save the changes to the database
                _UserRepository.UpdateUser(retrievedUser);

                return Ok(userUpdateDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Authorize]
        public IActionResult Delete()
        {
            try
            {
                int id = GetMe();
                _UserRepository.DeleteUser(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _UserRepository.DeleteUser(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetMe()
        {
            var id = User.FindFirstValue("Id");
            return int.Parse(id);
        }
    }
}
