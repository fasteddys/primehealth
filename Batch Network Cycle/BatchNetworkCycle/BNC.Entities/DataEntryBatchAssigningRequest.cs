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
    
    public partial class DataEntryBatchAssigningRequest
    {
        public int DataEntryBatchAssigningRequestID { get; set; }
        public int DataEntryAdministrationRequestFK { get; set; }
        public Nullable<int> DataEntryBatchAssigningStatusFK { get; set; }
        public int DataEntryOfficerFK { get; set; }
        public string DataEntryOfficerReceivingComment { get; set; }
        public Nullable<System.DateTime> ConfirmRecievingByOfficerTime { get; set; }
        public Nullable<System.DateTime> AssignedByAdminTime { get; set; }
        public Nullable<System.DateTime> DataEntryOfficerFinishedOnSystemTime { get; set; }
        public string DataEntryOfficerFinishedComment { get; set; }
        public int AssignedClaimsNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual DataEntryAdminstrationRequest DataEntryAdminstrationRequest { get; set; }
        public virtual StatusDIM StatusDIM { get; set; }
        public virtual User User { get; set; }
    }
}
