using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "* Enter first name")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "* Must not have special charecters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "* Enter first name")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "* Must not have special charecters")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="* Mention gender")]
        public string Sex { get; set; }
        [Required(ErrorMessage ="* Enter specialization")]
        public string Specialization { get; set; }
        [Required(ErrorMessage ="* Enter visiting time")]
        [RegularExpression("[0-12]{2}:00",ErrorMessage ="* Enter time in proper format")]
        public string VisitFrom { get; set; }
        [Required(ErrorMessage ="* Enter leaving time")]
        //[Range(1, 12, ErrorMessage = "* Enter time in hour")]
        [RegularExpression("[0-1]{1}[0-9]{1}:00", ErrorMessage = "* Enter time in proper format")]
        public string VisitTo { get; set; }

    }
}
