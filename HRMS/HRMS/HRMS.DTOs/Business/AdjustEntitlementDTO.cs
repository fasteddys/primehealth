using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class AdjustEntitlementDTO
    {
        public decimal EntitlementTotal { get; set; }
        public int LeaveTypeFK { get; set; }
        public DateTime CreationDate { get; set; }
        public string comment { get; set; }

    }
}
