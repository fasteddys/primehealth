using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs
{
   public class CategoryAuditDTO
    {
        public int BatchRequestFK { get; set; }
        public int CategoryAuditRequestID { get; set; }

        public int BatchAuditRequestFK { get; set; }
        public Nullable<int> NumberOfApprovedClaims { get; set; }
        public Nullable<decimal> ApprovedClaimsAmount { get; set; }
        public Nullable<int> NumberOfPendingClaims { get; set; }
        public Nullable<decimal> PendingClaimsAmount { get; set; }
        public Nullable<int> NumberOfRejectedClaims { get; set; }
        public Nullable<decimal> RejectedClaimsAmount { get; set; }
        public string CategoryAuditOfficerComment { get; set; }
        public Nullable<int> CategoryAuditOfficerFK { get; set; }

        public string OfficerName { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> AssignedTime { get; set; }
        public Nullable<System.DateTime> CompilationDate { get; set; }
        public int AuditOrder { get; set; }
        public string CategoryAuditName { get; set; }
        


    }
}
