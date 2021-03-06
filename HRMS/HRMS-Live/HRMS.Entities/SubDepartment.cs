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
    
    public partial class SubDepartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubDepartment()
        {
            this.ApprovalFlowRequestDetails = new HashSet<ApprovalFlowRequestDetail>();
            this.LeaveTypeRestrictedSubDeps = new HashSet<LeaveTypeRestrictedSubDep>();
            this.Users = new HashSet<User>();
        }
    
        public int SubDepartmentID { get; set; }
        public string SubDepartmentName { get; set; }
        public int DepartmentFK { get; set; }
        public Nullable<int> TeamManagerFK { get; set; }
        public string ArName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails { get; set; }
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveTypeRestrictedSubDep> LeaveTypeRestrictedSubDeps { get; set; }
        public virtual Manager Manager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
