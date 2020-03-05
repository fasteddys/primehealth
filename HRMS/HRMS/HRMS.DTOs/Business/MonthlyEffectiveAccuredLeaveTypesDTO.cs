using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class MonthlyEffectiveAccuredLeaveTypesDTO
    {
        public int MonthlyEffectiveAccuredLeaveTypesID { get; set; }
        public int LeaveTypeID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime EffectiveDateTo { get; set; }
    }
}
