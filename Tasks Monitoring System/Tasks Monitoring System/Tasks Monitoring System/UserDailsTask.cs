//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TasksMonitoringSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserDailsTask
    {
        public int UserDailyTasksID { get; set; }
        public Nullable<int> TaskFK { get; set; }
        public Nullable<int> AssignedByFK { get; set; }
        public Nullable<System.DateTime> DateOfTask { get; set; }
        public Nullable<int> StatusFK { get; set; }
        public Nullable<System.DateTime> CompletedOn { get; set; }
        public string ClosingComment { get; set; }
        public Nullable<System.DateTime> AssignTime { get; set; }
    
        public virtual Task Task { get; set; }
        public virtual TasksStatusDIM TasksStatusDIM { get; set; }
        public virtual User User { get; set; }
    }
}
