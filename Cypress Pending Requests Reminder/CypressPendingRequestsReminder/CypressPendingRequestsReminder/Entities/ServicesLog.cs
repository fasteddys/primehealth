//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CypressPendingRequestsReminder.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServicesLog
    {
        public int ServicesLogID { get; set; }
        public int ServicesFK { get; set; }
        public int ServiceLogTypeFK { get; set; }
        public string Value { get; set; }
        public Nullable<int> RequestFK { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual ServiceLogTypeDIM ServiceLogTypeDIM { get; set; }
    }
}
