using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TasksMonitoringSystem
{
    public class UserDTO
    {

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdminLevel { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }


    }
}
