using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IPasswordHasher
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string CreateToken(User user);
    }
}
