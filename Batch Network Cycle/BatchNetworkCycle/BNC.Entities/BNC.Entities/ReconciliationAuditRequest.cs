//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BNC.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReconciliationAuditRequest
    {
        public int ReconciliationAuditRequestID { get; set; }
        public int BatchAuditRequestFK { get; set; }
        public Nullable<int> ReconciliationOfficerFK { get; set; }
        public Nullable<System.DateTime> AssignedTime { get; set; }
        public Nullable<System.DateTime> ReconciliationAuditRequestClosedDate { get; set; }
        public Nullable<int> NumberOfApprovedClaims { get; set; }
        public Nullable<int> NumberOfPendingClaims { get; set; }
        public Nullable<int> NumberOfRejectedClaims { get; set; }
        public Nullable<decimal> ApprovedClaimsAmount { get; set; }
        public Nullable<decimal> PendingClaimsAmount { get; set; }
        public Nullable<decimal> RejectedClaimsAmount { get; set; }
        public Nullable<int> NumberOfAutoApprovedClaims { get; set; }
        public Nullable<int> NumberOfApprovedClaimsBySP { get; set; }
        public Nullable<int> NumberOfRejectedClaimsBySP { get; set; }
        public Nullable<int> TotalNumberOfApprovedClaims { get; set; }
        public Nullable<int> TotalNumberOfRejectedClaims { get; set; }
        public string ReconciliationOfficerComment { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual BatchAuditingRequest BatchAuditingRequest { get; set; }
        public virtual User User { get; set; }
    }
}
