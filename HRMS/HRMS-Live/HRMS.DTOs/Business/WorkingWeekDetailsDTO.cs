using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class WorkingWeekDetailsDTO
    {
        public int DayID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal WorkingDuration { get; set; }
        public string DayName { get; set; }
        public int GraceIn { get; set; }
        public int GraceOut { get; set; }
        public bool IsActive { get; set; }




    }
}
