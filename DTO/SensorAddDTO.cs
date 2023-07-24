using WebAPI.Services;

namespace WebAPI.DTO
{
    public class SensorAddDTO
    {
        public string Name { get; set; }
        public string ReferenceCode { get; set; }
        [UserIsClient]
        public int UserId { get; set; }
    }
}
