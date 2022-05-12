using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage ="* enter first name")]
        [RegularExpression("^[A-Za-z0-9]*$",ErrorMessage ="* must not have special charecters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="* enter last name")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "* must not have special charecters")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="* enter gender")]
        public string Sex { get; set; }
        [Required]
        [Range(0,120,ErrorMessage ="* age must be less than or equal to 120")]
        public int Age { get; set; }
        [Required(ErrorMessage ="* enter date of birth")]
        [RegularExpression("[0-9]{2}/[0-9]{2}/[0-9]{4}",ErrorMessage ="* values must be in proper format")]
        public string DateOfBirth { get; set;}
    }
}
