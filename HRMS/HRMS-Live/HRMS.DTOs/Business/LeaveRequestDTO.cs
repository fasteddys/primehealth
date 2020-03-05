using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class LeaveRequestDTO
    {
        public int LeaveTypeID { get; set; }
        public DateTime RequestStartTime { get; set; }
        public DateTime RequestEndTime { get; set; }
        public string RequestDurationUnit { get; set; }
        public List<DateTime> OffDays { get; set; }
        public DateTime BackToWorkDate { get; set; }
        public decimal NumberOfDays { get; set; }
    }
}
