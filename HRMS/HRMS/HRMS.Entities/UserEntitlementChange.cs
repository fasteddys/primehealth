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
    
    public partial class UserEntitlementChange
    {
        public int UserEntitlementChangesID { get; set; }
        public string Comment { get; set; }
        public decimal EntitlementBefore { get; set; }
        public decimal EntitlementAfter { get; set; }
        public Nullable<System.DateTime> DurationFrom { get; set; }
        public Nullable<System.DateTime> DurationTo { get; set; }
        public Nullable<System.DateTime> BackToWork { get; set; }
        public System.DateTime ActionDate { get; set; }
        public decimal RequestDuration { get; set; }
        public int UserFK { get; set; }
        public Nullable<int> UserEntitlementFK { get; set; }
        public Nullable<int> RequestFK { get; set; }
        public Nullable<int> EntitlementChangedByFK { get; set; }
        public Nullable<int> UserChangeEntitlementFK { get; set; }
        public int LeaveDurationUnitFK { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public Nullable<int> Year { get; set; }
    
        public virtual EntitlementChangedByDIM EntitlementChangedByDIM { get; set; }
        public virtual LeaveTypeDurationUnitDIM LeaveTypeDurationUnitDIM { get; set; }
        public virtual Request Request { get; set; }
        public virtual UserEntitlement UserEntitlement { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}