//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddAcccuralLeaveTypesForNewEmployees_WS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestHistory
    {
        public int RequestHistoryID { get; set; }
        public System.DateTime Date { get; set; }
        public string Action { get; set; }
        public int FromUser { get; set; }
        public int RequestFK { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual User User { get; set; }
    }
}
