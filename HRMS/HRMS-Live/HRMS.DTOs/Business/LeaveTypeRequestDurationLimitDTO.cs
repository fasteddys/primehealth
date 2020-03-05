using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class LeaveTypeRequestDurationLimitDTO
    {
        public bool IsConditionChecked { get; set; }
        public int NumberOFDaysLimit { get; set; }

        public int DaysBeforeStartdate { get; set; }

    }
}
