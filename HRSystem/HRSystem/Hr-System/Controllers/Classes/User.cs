using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HrSystem.Classes
{
    public class User
    {

        public int UserID { get; set; }
        [Required(ErrorMessage = "Please enter user name.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Name")]
        [StringLength(30)]
        public string EmpName { get; set; }


        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(10)]
        public string Password { get; set; }
    }
}