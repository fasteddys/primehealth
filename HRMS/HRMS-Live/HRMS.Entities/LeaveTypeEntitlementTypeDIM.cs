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
    
    public partial class LeaveTypeEntitlementTypeDIM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveTypeEntitlementTypeDIM()
        {
            this.LeaveTypes = new HashSet<LeaveType>();
        }
    
        public int LeaveTypeEntitlementTypeID { get; set; }
        public string LeaveTypeEntitlementTypeName { get; set; }
        public string ArName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveType> LeaveTypes { get; set; }
    }
}
