using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IUserRepository
    {
        public bool RegisterUser(User user);
        public User Login(UserLoginDTO user);
        public User GetUser(int id);
        public ICollection<UserDisplayDTO> GetUsers();
        public void UpdateUser(User user);
        public void DeleteUser(int id);
    }
}
