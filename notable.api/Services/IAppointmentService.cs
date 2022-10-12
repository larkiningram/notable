using System;
using notable.Models;

namespace notable.Services
{
    public interface IAppointmentService
    {
        public List<Doctor> GetDoctors();
        public Doctor GetDoctor(string id);
        public void AddDoctor(Doctor doctor);
        public void AddAppointment(string doctorId, Appointment appointment);
        public void DeleteAppointment(string doctorId, string appointmentId);
        public List<Appointment> GetAppointments(string doctorId, string date);
    }
}

