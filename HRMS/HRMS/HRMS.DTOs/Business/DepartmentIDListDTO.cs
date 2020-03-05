using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class DepartmentIDListDTO
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int? ModifiedByUserId { get; set; }

    }
}
