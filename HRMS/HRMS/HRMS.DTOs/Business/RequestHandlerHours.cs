using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public  class RequestHandlerHours
    {
       public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public decimal TimeDuration { get; set; }


    }
}
