using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TickitingSystem.Models
{
    public class RoleManagment
    {
         public Role role { get; set; }
        public List<Permission> permission { get; set; }

    }
}