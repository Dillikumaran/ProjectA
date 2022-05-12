using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class Appointments
    {
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "Enter PatientId")]
        public int PatientId { get; set; }
        [Required(ErrorMessage ="Enter Specialization")]
        public string Specialization { get; set; }
        [Required(ErrorMessage = "Enter DoctorName")]
        public string Doctor { get; set; }
        [Required(ErrorMessage = "Enter visiting Date")]
        public string VisitDate { get; set; }
        [Required(ErrorMessage = "Enter Appointment Time")]
        public string AppointmentTime { get; set; }
    }
}
