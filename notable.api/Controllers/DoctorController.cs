using System.Numerics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using notable.Models;
using notable.Services;

namespace notable.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private IAppointmentService _service;

        public DoctorsController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Doctor> GetDoctors()
        {
            return _service.GetDoctors();
        }

        [HttpGet("{id}")]
        public Doctor GetDoctor(string id)
        {
            return _service.GetDoctor(id);
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            if (_service.AddDoctor(doctor))
                return Ok();
            return new BadRequestObjectResult("that doctor already exists");
        }

        [HttpGet("{id}/appointments")]
        public IEnumerable<Appointment> GetAppointments(string id)
        {
            return _service.GetAppointments(id);
        }

        [HttpGet("{id}/appointments/{date}")]
        public IEnumerable<Appointment> GetAppointmentsByDate(string id, string date)
        {
            return _service.GetAppointmentsByDate(id, date);
        }

        [HttpPost("{id}/appointments")]
        public IActionResult AddAppointment(string id, Appointment appointment)
        {
            if (_service.AddAppointment(id, appointment))
                return Ok();
            return new BadRequestObjectResult("that appointment isn't formatted correctly");
        }

        [HttpDelete("{doctorId}/appointments/{appointmentId}")]
        public IActionResult DeleteAppointment(string doctorId, string appointmentId)
        {
            if (_service.DeleteAppointment(doctorId, appointmentId))
                return Ok();
            return new BadRequestObjectResult("that appointment doesn't exist");
        }
    }

}

