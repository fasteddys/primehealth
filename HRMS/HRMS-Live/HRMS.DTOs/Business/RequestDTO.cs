using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
 public   class RequestDTO
    {

        public int RequestID { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestStatus { get; set; }
       //public int RequestStatus { get; set; }
       //public int RequestStatus { get; set; }
       //public int RequestStatus { get; set; }
    }
}
