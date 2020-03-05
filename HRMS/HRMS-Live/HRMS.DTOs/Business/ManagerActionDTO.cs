using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public  class ManagerActionDTO
    {
        public int UserID { get; set; }
        public int? ModifiedByUserId { get; set; }

        public int RequestID { get; set; }

        public string RejectionReason { get; set; }


    }
}
