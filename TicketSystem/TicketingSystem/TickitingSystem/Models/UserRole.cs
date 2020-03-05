using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TickitingSystem.Models
{
    public class UserRole
    {
      public  User users { get; set; }
      public List<Role> Roles { get; set; }
    }
}