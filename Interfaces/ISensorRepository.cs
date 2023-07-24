using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ISensorRepository
    {
        public void AddSensor(SensorAddDTO sensor);
        public Sensor GetSensor(int id);
        public ICollection<SensorDisplayDTO> GetSensors();
        public ICollection<SensorDisplayDTO> GetUserSensors(int userId);
        public void DeleteSensor(int id);
        public void UpdateSensor(Sensor sensor);
        public bool AddSensorToCategory(int sensorId, int  categoryId);
        public void RemoveSensorFromCategory(int sensorId, int categoryId);
        
    }
}
