using System;
using Microsoft.AspNetCore.Mvc;
using notable.Models;

namespace notable.Services
{
    public interface IAppointmentService
    {
        public List<Doctor> GetDoctors();
        public Doctor GetDoctor(string id);
        public bool AddDoctor(Doctor doctor);
        public bool AddAppointment(string doctorId, Appointment appointment);
        public bool DeleteAppointment(string doctorId, string appointmentId);
        public List<Appointment> GetAppointments(string doctorId);
        public List<Appointment> GetAppointmentsByDate(string doctorId, string date);
    }
}

