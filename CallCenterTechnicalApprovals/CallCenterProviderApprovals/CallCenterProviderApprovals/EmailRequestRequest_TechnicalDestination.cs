//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallCenterProviderApprovals
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailRequestRequest_TechnicalDestination
    {
        public int ID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> TechnicalDestinationID { get; set; }
        public string Assignee { get; set; }
        public Nullable<System.DateTime> AssignTime { get; set; }
        public Nullable<System.DateTime> ActionTime { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> StartTechnicalApprovalTime { get; set; }
        public Nullable<System.DateTime> EndTechnicalApprovalTime { get; set; }
        public string CallCenterNote { get; set; }
    
        public virtual EmailApprovalRequest EmailApprovalRequest { get; set; }
        public virtual EmailApprovalsTechnicalDestinationDim EmailApprovalsTechnicalDestinationDim { get; set; }
    }
}
