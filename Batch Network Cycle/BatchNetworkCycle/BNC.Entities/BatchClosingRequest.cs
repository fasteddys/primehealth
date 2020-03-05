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
    
    public partial class BatchClosingRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BatchClosingRequest()
        {
            this.ClosedBatchReAdministrationRequests = new HashSet<ClosedBatchReAdministrationRequest>();
        }
    
        public int BatchClosingRequestID { get; set; }
        public int DataEntryAdminstrationRequestFK { get; set; }
        public Nullable<int> ClosingOfficerAssigneeFK { get; set; }
        public Nullable<System.DateTime> ClosingOfficerAssignedTime { get; set; }
        public string ConfirmReceivingComment { get; set; }
        public Nullable<System.DateTime> ReceviedTime { get; set; }
        public string FinishedReviewingComment { get; set; }
        public Nullable<System.DateTime> FinishedReviewingTime { get; set; }
        public Nullable<System.DateTime> ClosedOnSystemTime { get; set; }
        public int BatchClosingStatusFK { get; set; }
        public Nullable<System.DateTime> TransferredBackToAdminTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual DataEntryAdminstrationRequest DataEntryAdminstrationRequest { get; set; }
        public virtual StatusDIM StatusDIM { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClosedBatchReAdministrationRequest> ClosedBatchReAdministrationRequests { get; set; }
    }
}
