using System;
namespace notable.Models
{
    public class Appointment
    {
        public string id { get; set; }
        public string doctorId { get; set; }
        public Patient patient { get; set; }
        public string kind { get; set; }
        public Visit visit { get; set; }
    }

    public class Patient
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Visit
    {
        public string date { get; set; }
        public string time { get; set; }

    }
}

