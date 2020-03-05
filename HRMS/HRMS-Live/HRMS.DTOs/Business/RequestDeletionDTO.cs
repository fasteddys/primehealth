using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class RequestDeletionDTO
    {
        public int RequestID { get; set; }
        public int UserDeleterID { get; set; }
        public List<RequestHandlerDTO> DeletedDays { get; set; }
        public List<RequestHandlerDTO> DeletedTimes { get; set; }
        public string DaysDeletionReason { get; set; }
        public string HoursDeletionReason { get; set; }



    }
}
