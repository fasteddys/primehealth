using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class TimeAttendanceParametersDTO
    {


        public int UserID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
