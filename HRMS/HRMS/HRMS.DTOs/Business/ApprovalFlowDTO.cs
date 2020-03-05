using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
public    class ApprovalFlowDTO
    {
        public int ApprovalRoleID { get; set; }
        public int? ApprovalRoleLeaveID { get; set; }
        public List<ApprovalFlowStepDTO> Steps { get; set; }


    }
}
