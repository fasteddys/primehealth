using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class RejectionReasonInputDTO
    {
        public int? RequestUserID { get; set; }
        public int? RequestLeaveTypeID { get; set; }
        public string RequestForm { get; set; }
        public string RequestTo { get; set; }
        public int RequestStatus { get; set; }
    }
}
