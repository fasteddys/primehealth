//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hr_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OverTime
    {
        public int id { get; set; }
        public Nullable<System.DateTime> OverTimeDate { get; set; }
        public string OverStart { get; set; }
        public string OverEnd { get; set; }
        public string CauseOfOverTime { get; set; }
        public string OverStatus { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public string MyTeamLeader { get; set; }
        public string MySupervisor { get; set; }
        public string MyManager { get; set; }
        public Nullable<int> NoOfHours { get; set; }
        public string ArabicName { get; set; }
        public string RejectionTeamLeadercomment { get; set; }
        public string RejectionManagerComment { get; set; }
        public string RejectionSupervisorCommeent { get; set; }
        public Nullable<System.DateTime> StartOverDate { get; set; }
        public Nullable<System.DateTime> EndOverDate { get; set; }
    }
}
