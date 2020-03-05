using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.DB.Models
{
    public class Employee: Base
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public string Phone { get; set; }

        public long JobId { get; set; }

        public virtual Job Job { get; set; }

        public long EmployeeRoleId { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        public DateTime HireDate { get; set; }

        public bool LoggedIn { get; set; }

        public virtual ICollection<ProcessTransaction> ProcessTransactions { get; set; }


    }
}
