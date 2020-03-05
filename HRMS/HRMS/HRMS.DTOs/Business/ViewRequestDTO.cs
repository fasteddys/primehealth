using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class ViewRequestDTO
    {
        public int RequestID { get; set; }
        public string LeaveType { get; set; }
        public string UserName { get; set; }
        public string RequestStatus { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public DateTime BackToWork { get; set; }
        public decimal RequestDuration { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Unit { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public string PartialDurationUnit { get; set; }
        public int? AccessControlID { get; set; }

    }
}
