using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class DailyAttendanceDTO
    {
        public string FingerPrintIn{ get; set; }
        public string FingerPrintOut { get; set; }
        public string Date { get; set; }
    }
}
