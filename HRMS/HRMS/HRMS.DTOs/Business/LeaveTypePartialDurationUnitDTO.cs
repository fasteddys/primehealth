using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
 public class LeaveTypePartialDurationUnitDTO
    {
        public int PartialDurationUnitID { get; set; }
        public string PartialDurationUnitName { get; set; }
        public string DurationUnitName { get; set; }
    }
}
