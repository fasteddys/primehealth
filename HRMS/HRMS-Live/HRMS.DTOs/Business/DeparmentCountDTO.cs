using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class DeparmentCountDTO
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int SubDepartmentCount { get; set; }
       

    }
}
