using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class AuditFlowDetailDTO
    {
        public int AuditFlowDetailID { get; set; }
        public int Order { get; set; }
        public int BatchID { get; set; }
        public int AuditFlowID { get; set; }

    }
}
