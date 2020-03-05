using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Common.ViewModels
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public long JobId { get; set; }

        public string JobName { get; set; }

        public long EmployeeRoleId { get; set; }

        public string EmployeeRoleName { get; set; }

        public DateTime HireDate { get; set; }

        public bool LoggedIn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
