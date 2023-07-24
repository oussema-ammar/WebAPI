using InfluxDB.Client.Api.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("v1/sensors")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly IUserRepository _userRepository;
        public SensorController(ISensorRepository sensorRepository, IUserRepository userRepository)
        {
            _sensorRepository = sensorRepository;
            _userRepository = userRepository;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult AddSensor(SensorAddDTO sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _sensorRepository.AddSensor(sensor);
            return Ok("Sensor Added");
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult GetSensor(int id)
        {
            try
            {
                var sensor = _sensorRepository.GetSensor(id);
                var user = _userRepository.GetUser(GetMe());
                if (user.Id == sensor.UserId || user.Role == "Admin")
                    return Ok(sensor);
                return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetSensors()
        {
            var sensors = _sensorRepository.GetSensors();
            return Ok(sensors);
        }

        [HttpGet("user/{userId}"), Authorize]
        public IActionResult GetUserSensors(int userId)
        {
            var currentUser = _userRepository.GetUser(GetMe());
            if (currentUser.Id == userId || currentUser.Role == "Admin")
            {
                var sensors = _sensorRepository.GetUserSensors(userId);
                return Ok(sensors);
            }
            else
                return BadRequest("Forbidden");
        }

        [HttpDelete, Authorize]
        public IActionResult DeleteSensor(int id)
        {
            var currentUser = _userRepository.GetUser(GetMe());
            var sensor = _sensorRepository.GetSensor(id);
            if (currentUser.Id == sensor.UserId || currentUser.Role == "Admin")
            {
                _sensorRepository.DeleteSensor(id);
                return Ok();
            }
            return BadRequest("Forbidden");
        }

        [HttpPut, Authorize]
        public IActionResult EditSensor(SensorEditDTO sensor)
        {
            var currentUser = _userRepository.GetUser(GetMe());
            var oldSensor = _sensorRepository.GetSensor(sensor.Id);
            if (currentUser.Id == oldSensor.UserId || currentUser.Role == "Admin")
            {
                Sensor s = new Sensor
                {
                    Id = sensor.Id,
                    Name = sensor.Name,
                    ReferenceCode =sensor.ReferenceCode,
                    UserId = oldSensor.UserId,
                    SensorCategories = oldSensor.SensorCategories
                };
                _sensorRepository.UpdateSensor(s);
                return Ok(s);
            }
            return BadRequest();
        }

        [HttpPost("/sensor-category"), Authorize]
        public IActionResult AddSensorToCategory(int sensorId, int categoryId)
        {
            var operation = _sensorRepository.AddSensorToCategory(sensorId, categoryId);
            if(operation)
                return Ok();
            return BadRequest("Operation failed");
        }

        [HttpDelete("/sensor-category"), Authorize]
        public IActionResult RemoveSensorCategory(int sensorId, int categoryId)
        {
            try
            {
                _sensorRepository.RemoveSensorFromCategory(sensorId, categoryId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        private int GetMe()
        {
            var id = User.FindFirstValue("Id");
            return int.Parse(id);
        }

    }
}
