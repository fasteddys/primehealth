using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WarhouseSystem.DB.Models
{
   public class EmployeeRole: Base
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
