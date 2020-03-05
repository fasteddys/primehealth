using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs
{
    public class ShowAuditRequestDTO
    {
        public int RequestID { get; set; }
        public int BatchAuditRequestID { get; set; }
        public int? NumberOfApprovedClaims { get; set; }
        public int? NumberOfPendingClaims { get; set; }
        public int? NumberOfRejectedClaims { get; set; }
        public string OfficerName { get; set; }       
        public string Action { get; set; }
    }
}
