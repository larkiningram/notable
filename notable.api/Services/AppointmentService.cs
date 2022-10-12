using System;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using notable.Models;

namespace notable.Services
{
    public class AppointmentService : IAppointmentService
    {
        public AppointmentService()
        {
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            var data = ReadData();
            return data.Doctors;
        }

        public Doctor GetDoctor(string id)
        {
            var data = ReadData();
            var doctor = data!.Doctors.Find(d => d.id == id);
            if (doctor != null) return doctor;
            return new Doctor();
        }

        public bool AddDoctor(Doctor doctor)
        {
            var data = ReadData();
            var exists = data.Doctors.FindAll(d => (d.firstName == doctor.firstName && d.lastName == doctor.lastName));
            if (exists.Count() == 0)
            {
                doctor.id = Guid.NewGuid().ToString();
                data.Doctors.Add(doctor);
                WriteData(data);
                return true;
            }
            return false;
        }

        public List<Appointment> GetAppointments(string doctorId)
        {
            var data = ReadData();
            return data!.Appointments.FindAll(a => a.doctorId == doctorId);
        }

        public List<Appointment> GetAppointmentsByDate(string doctorId, string date)
        {
            var data = ReadData();
            var appointmentsByDoctor = data!.Appointments.FindAll(a => a.doctorId == doctorId);
            return appointmentsByDoctor.FindAll(a => a.visit.date == date);
        }

        public bool AddAppointment(string doctorId, Appointment appointment)
        {
            var data = ReadData();
            appointment.id = Guid.NewGuid().ToString();
            if (CheckIfValidAppointment(doctorId, appointment, data))
            {
                data!.Appointments.Add(appointment);
                WriteData(data);
                return true;
            }
            return false;
        }

        public bool DeleteAppointment(string doctorId, string appointmentId)
        {
            var data = ReadData();
            var keep = data.Appointments.FindAll(a => a.id != appointmentId);
            if (keep.Count() != data.Appointments.Count())
            { 
                data.Appointments = keep;
                WriteData(data);
                return true;
            }
            return false;
        }

        private Data ReadData()
        {
            StreamReader r = new StreamReader("data.json");
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Data>(json);
        }

        private void WriteData(Data data)
        {
            var jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText("data.json", jsonString);
        }

        private bool CheckIfValidAppointment(string doctorId, Appointment appointment, Data data)
        {
            var times = appointment.visit.time.Split(":");
            if (int.Parse(times[1]) % 15 != 0) return false;
            var otherAppointments = data.Appointments.FindAll(a => a.doctorId == doctorId);
            var sameDay = otherAppointments.FindAll(a => a.visit.date == appointment.visit.date);
            var sameTime = otherAppointments.FindAll(a => a.visit.time == appointment.visit.time).Count();
            if (sameTime > 2) return false;
            if (appointment.kind != "New Patient" && appointment.kind != "Follow-up") return false;
            return true;
        }
    }
}

