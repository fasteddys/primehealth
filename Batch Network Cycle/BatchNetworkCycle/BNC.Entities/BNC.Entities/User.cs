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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.BatchClosingRequests = new HashSet<BatchClosingRequest>();
            this.BatchingRequests = new HashSet<BatchingRequest>();
            this.ClosedBatchReAdministrationRequests = new HashSet<ClosedBatchReAdministrationRequest>();
            this.DataEntryAdminstrationRequests = new HashSet<DataEntryAdminstrationRequest>();
            this.DataEntryBatchAssigningRequests = new HashSet<DataEntryBatchAssigningRequest>();
            this.FilterationRequestDetails = new HashSet<FilterationRequestDetail>();
            this.FinancialAuditRequests = new HashSet<FinancialAuditRequest>();
            this.LockLogges = new HashSet<LockLogge>();
            this.MedicalAuditRequests = new HashSet<MedicalAuditRequest>();
            this.MIAuditRequests = new HashSet<MIAuditRequest>();
            this.MIAuditRequests1 = new HashSet<MIAuditRequest>();
            this.MIAuditRequests2 = new HashSet<MIAuditRequest>();
            this.ReceivingRequests = new HashSet<ReceivingRequest>();
            this.ReconciliationAuditRequests = new HashSet<ReconciliationAuditRequest>();
            this.SPRequests = new HashSet<SPRequest>();
            this.SPUsers = new HashSet<SPUser>();
            this.TransferRequestHistories = new HashSet<TransferRequestHistory>();
            this.TransferRequestHistories1 = new HashSet<TransferRequestHistory>();
            this.TransferRequestHistories2 = new HashSet<TransferRequestHistory>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public Nullable<int> TeamFK { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchClosingRequest> BatchClosingRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchingRequest> BatchingRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClosedBatchReAdministrationRequest> ClosedBatchReAdministrationRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataEntryAdminstrationRequest> DataEntryAdminstrationRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataEntryBatchAssigningRequest> DataEntryBatchAssigningRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilterationRequestDetail> FilterationRequestDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinancialAuditRequest> FinancialAuditRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LockLogge> LockLogges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalAuditRequest> MedicalAuditRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MIAuditRequest> MIAuditRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MIAuditRequest> MIAuditRequests1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MIAuditRequest> MIAuditRequests2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceivingRequest> ReceivingRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReconciliationAuditRequest> ReconciliationAuditRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPRequest> SPRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPUser> SPUsers { get; set; }
        public virtual Team Team { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferRequestHistory> TransferRequestHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferRequestHistory> TransferRequestHistories1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferRequestHistory> TransferRequestHistories2 { get; set; }
    }
}
