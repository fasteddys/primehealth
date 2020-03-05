//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaveTypeRenewal.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserEntitlement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserEntitlement()
        {
            this.UserEntitlementChanges = new HashSet<UserEntitlementChange>();
        }
    
        public int UserEntitlementID { get; set; }
        public decimal EntitlementTotal { get; set; }
        public int UserFK { get; set; }
        public int LeaveTypeFK { get; set; }
        public int LeaveDurationUnitFK { get; set; }
        public string ArName { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<System.DateTime> EffectiveDateFrom { get; set; }
        public Nullable<System.DateTime> EffectiveDateTo { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual LeaveType LeaveType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEntitlementChange> UserEntitlementChanges { get; set; }
        public virtual User User { get; set; }
    }
}
