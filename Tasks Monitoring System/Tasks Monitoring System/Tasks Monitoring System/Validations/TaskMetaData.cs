using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TasksMonitoringSystem
{
    [MetadataType(typeof(UsersMetaData))]
    public partial class User
    {

    }
    public class UsersMetaData
    {
        [Required(ErrorMessage = "User Name  is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


    }
}