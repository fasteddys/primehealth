//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRMS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestActionDIM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestActionDIM()
        {
            this.ApprovalFlowRequestDetails = new HashSet<ApprovalFlowRequestDetail>();
        }
    
        public int RequestActionID { get; set; }
        public string RequestActionName { get; set; }
        public bool IsApprovalAction { get; set; }
        public string ArName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails { get; set; }
    }
}