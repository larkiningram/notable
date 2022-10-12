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
            _service.AddDoctor(doctor);
            return Ok();
        }

        [HttpGet("{id}/appointments/{date}")]
        public IEnumerable<Appointment> GetAppointments(string id, string date)
        {
            return _service.GetAppointments(id, date);
        }

        [HttpPost("{id}/appointments")]
        public IActionResult AddAppointment(string id, Appointment appointment)
        {
            _service.AddAppointment(id, appointment);
            return Ok();
        }

        [HttpDelete("{doctorId}/appointments/{appointmentId}")]
        public IActionResult DeleteAppointment(string doctorId, string appointmentId)
        {
            _service.DeleteAppointment(doctorId, appointmentId);
            return Ok();
        }
    }

}

