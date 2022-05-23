using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectA.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "* enter user name")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "* must not have special charecters")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage ="* enter passsword")]
        public string PassWord { get; set; }
    }
}
