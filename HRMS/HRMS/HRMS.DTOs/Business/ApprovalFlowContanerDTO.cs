using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class ApprovalFlowContainerDTO
    {
        public int ApprovalFlowID { get; set; }
        public string ApprovalFlowName { get; set; }

        public List<ApprovalFlowDTO> ApprovalFlow { get; set; }


    }
}
