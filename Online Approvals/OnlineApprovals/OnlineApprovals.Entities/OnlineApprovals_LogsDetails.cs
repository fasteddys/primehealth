//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineApprovals.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class OnlineApprovals_LogsDetails
    {
        public int ID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int LogTypeID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    
        public virtual CallCenterAppUser CallCenterAppUser { get; set; }
        public virtual OnlineApprovals_LogTypeDIM OnlineApprovals_LogTypeDIM { get; set; }
        public virtual OnlineApprovals_Requests OnlineApprovals_Requests { get; set; }
    }
}