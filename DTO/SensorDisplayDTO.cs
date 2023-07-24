using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.DTO
{
    public class SensorDisplayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReferenceCode { get; set; }
        [UserIsClient]
        public int UserId { get; set; }
    }
}
