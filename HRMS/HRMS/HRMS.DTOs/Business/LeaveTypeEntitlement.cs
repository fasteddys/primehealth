using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class LeaveTypeEntitlementDTO
    {
        public int? LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public decimal? UserEntitlementAmount { get; set; }
        public string DurationUnit { get; set; }
        public string Message { get; set; }
    }
}
