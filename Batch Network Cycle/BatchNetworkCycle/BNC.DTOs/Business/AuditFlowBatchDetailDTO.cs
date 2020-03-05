using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
   public class AuditFlowBatchDetailDTO
    {
        public int BatchAuditFlowDetailID { get; set; }
        public int AuditOrder { get; set; }
        public int BatchID { get; set; }
        public int AuditFlowDetailID { get; set; }

    }
}
