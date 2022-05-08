using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class Appointments
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string Specialization { get; set; }
        public string Doctor { get; set; }
        public string VisitDate { get; set; }
        public string AppointmentTime { get; set; }
    }
}
