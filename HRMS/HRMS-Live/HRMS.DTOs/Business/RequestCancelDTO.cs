using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class RequestCancelDTO
    {
        public int RequestID { get; set; }
        public int UserDeleterID { get; set; }
        public string CancelReason { get; set; }



    }
}
