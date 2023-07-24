using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReferenceCode { get; set; }
        [UserIsClient]
        public int UserId { get; set; }
        //public User User { get; set; }
        public ICollection<SensorCategory> SensorCategories { get; set; } = new List<SensorCategory>();
    }
}
