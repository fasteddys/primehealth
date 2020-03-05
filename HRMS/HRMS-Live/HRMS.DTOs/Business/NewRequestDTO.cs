using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
 public   class NewRequestDTO
    {
        public int LeaveTypeID { get; set; }
        public int UserID { get; set; }
        public int? OnBehalfOfRequesterID { get; set; }
        public string RequestStart { get; set; }
        public string RequestEnd { get; set; }
        public int RequestPartialUnitID { get; set; }
        public string Reason { get; set; }
        public int? Substitute { get; set; }
        public string Comment { get; set; }
        

    }
}
