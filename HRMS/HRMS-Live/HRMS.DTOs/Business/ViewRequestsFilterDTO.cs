using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class ViewRequestsFilterDTO
    {
        public int UserID { get; set; }

        public int StatusID { get; set; }
        public int? LeaveTypeID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
       
   

    }
}
