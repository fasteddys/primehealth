using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{

    public class WorkingDayDetailsDTO
    {
      public    TimeSpan DayEnd { get; set; }
       public TimeSpan DayStart { get; set; }
        public bool IsBetweenTwoDaysRequest { get; set; }



    }
}
