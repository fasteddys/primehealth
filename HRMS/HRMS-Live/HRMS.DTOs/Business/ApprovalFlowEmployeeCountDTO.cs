using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class ApprovalFlowEmployeeCountDTO
    {

        public int ApprovalFlowID { get; set; }
        public string ApprovalFlowName { get; set; }


        public int UsersCount { get; set; }
    }
}
