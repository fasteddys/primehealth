using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowBatchAuditRequestDTO
    {
        public int RequestID { get; set; }
        public int BatchingRequestID { get; set; }
        public string StatusName { get; set; }
        public int? NumberOfApprovedClaims { get; set; }
        public int? NumberOfRejectedClaims { get; set; }
        public string Actions { get; set; }
    }
}
