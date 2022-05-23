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
        [Required(ErrorMessage = "* Enter patient id")]
        public int PatientId { get; set; }
        [Required(ErrorMessage ="* Enter specialization")]
        public string Specialization { get; set; }
        [Required(ErrorMessage = "* Enter doctor name")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "* Must not have special charecters")]
        public string Doctor { get; set; }
        [Required(ErrorMessage = "* enter visiting date")]
        [RegularExpression("2[0-9]{1}/[0-9]{2}/[0-9]{4}", ErrorMessage = "* Values must be in proper format")]
        public string VisitDate { get; set; }
        [Required(ErrorMessage = "* enter appointment time")]
        public string AppointmentTime { get; set; }
    }
}
