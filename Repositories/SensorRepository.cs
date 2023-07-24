using InfluxDB.Client.Api.Domain;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly SqlServerDataContext _context;
        public SensorRepository(SqlServerDataContext context)
        {
            _context = context;
        }

        public void AddSensor(SensorAddDTO sensor)
        {
            Sensor s = new Sensor
            {
                Name = sensor.Name,
                ReferenceCode = sensor.ReferenceCode,
                UserId = sensor.UserId
            };
            _context.Sensors.Add(s);
            _context.SaveChanges();
        }

        public bool AddSensorToCategory(int sensorId, int categoryId)
        {
            var sensor = _context.Sensors.Find(sensorId);
            var category = _context.Categories.Find(categoryId);

            if (sensor != null && category != null)
            {
                SensorCategory sensorCategory = new SensorCategory
                {
                    SensorId = sensorId,
                    CategoryId = categoryId,
                    Sensor = sensor,
                    Category = category
                };
                _context.SensorCategories.Add(sensorCategory);
                return true;
            }
            return false;
        }

        public void DeleteSensor(int id)
        {
            var sensor = _context.Sensors.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sensor doesn't exist.");
            _context.Sensors.Remove(sensor);
            _context.SaveChanges();
        }

        public Sensor GetSensor(int id)
        {
            var sensor = _context.Sensors.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sensor doesn't exist.");
            return sensor;
        }

        public ICollection<SensorDisplayDTO> GetSensors()
        {
            ICollection<Sensor> fullSensors = _context.Sensors.OrderBy(s => s.Id).ToList();
            ICollection<SensorDisplayDTO> sensors = new List<SensorDisplayDTO>();
            foreach (var fullSensor in fullSensors)
            {
                SensorDisplayDTO sensor = new SensorDisplayDTO
                {
                    Id = fullSensor.Id,
                    UserId = fullSensor.UserId,
                    ReferenceCode = fullSensor.ReferenceCode,
                    Name = fullSensor.Name,
                };
                sensors.Add(sensor);
            }
            return sensors;
        }

        public ICollection<SensorDisplayDTO> GetUserSensors(int userId)
        {
            ICollection<Sensor> fullSensors = _context.Sensors
                .Where(s => s.UserId == userId)
                .OrderBy(s => s.Id).ToList();
            ICollection<SensorDisplayDTO> sensors = new List<SensorDisplayDTO>();
            foreach (var fullSensor in fullSensors)
            {
                SensorDisplayDTO sensor = new SensorDisplayDTO
                {
                    Id = fullSensor.Id,
                    UserId = fullSensor.UserId,
                    Name = fullSensor.Name,
                    ReferenceCode = fullSensor.ReferenceCode,
                };
                sensors.Add(sensor);
            }
            return sensors;
        }

        public void RemoveSensorFromCategory(int sensorId, int categoryId)
        {
            var sensorCategory = _context.SensorCategories.FirstOrDefault(sc => sc.SensorId == sensorId && sc.CategoryId == categoryId)
            ?? throw new Exception("No such relation exists.");
            _context.SensorCategories.Remove(sensorCategory);
            _context.SaveChanges();
        }

        public void UpdateSensor(Sensor sensor)
        {
            // Retrieve the existing sensor from the database
            var existingSensor = _context.Sensors.FirstOrDefault(s => s.Id == sensor.Id);

            // If the sensor exists, update its properties with the new values
            if (existingSensor != null)
            {
                existingSensor.Id = sensor.Id;
                existingSensor.Name = sensor.Name;
                existingSensor.ReferenceCode = sensor.ReferenceCode;
                _context.Update(existingSensor);
                _context.SaveChanges();
            }
        }
    }
}
