using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs
{
   public class FinancialAuditDTO
    {
        public int FinancialAuditRequestID { get; set; }
        public int BatchAuditRequestFK { get; set; }
        public int BatchRequestFK { get; set; }

        public Nullable<int> NumberOfApprovedClaims { get; set; }
        public Nullable<decimal> ApprovedClaimsAmount { get; set; }
        public Nullable<int> NumberOfPendingClaims { get; set; }
        public Nullable<decimal> PendingClaimsAmount { get; set; }
        public Nullable<int> NumberOfRejectedClaims { get; set; }
        public Nullable<decimal> RejectedClaimsAmount { get; set; }
        public string FinancialAuditOfficerComment { get; set; }
        public Nullable<int> FinancialAuditOfficerFK { get; set; }
        public string OfficerName { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> AssignedTime { get; set; }
        public Nullable<System.DateTime> CompilationDate { get; set; }
        public int AuditOrder { get; set; }
        public Nullable<int> TotalNumberOfApprovedClaims { get; set; }
        public string ReqByUserNote { get; set; }



    }
}
