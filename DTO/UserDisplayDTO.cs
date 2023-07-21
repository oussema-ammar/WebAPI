namespace WebAPI.DTO
{
    public class UserDisplayDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreationDate { get; set; }
        public string PhotoPath { get; set; }
    }
}
