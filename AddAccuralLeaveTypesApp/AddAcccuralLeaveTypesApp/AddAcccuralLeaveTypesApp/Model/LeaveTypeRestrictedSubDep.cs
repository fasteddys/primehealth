//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddAcccuralLeaveTypesApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LeaveTypeRestrictedSubDep
    {
        public int LeaveTeamRestrictionID { get; set; }
        public Nullable<int> SubDepartmentFK { get; set; }
        public Nullable<int> LeaveTypeFK { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual LeaveType LeaveType { get; set; }
    }
}
