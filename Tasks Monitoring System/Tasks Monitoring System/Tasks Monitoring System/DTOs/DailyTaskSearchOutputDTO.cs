using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TasksMonitoringSystem.DTOs
{
    public class DailyTaskSearchOutputDTO
    {
        public int UserDailyTasksID { get; set; }
        public string CompanyName { get; set; }
        public string TaskName { get; set; }
        public string TaskPriority { get; set; }
        public string TaskStatusName { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> DateOfTask { get; set; }
        public Nullable<System.DateTime> AssignTime { get; set; }
        public Nullable<System.DateTime> CompletedOn { get; set; }
        public string ClosingComment { get; set; }
    }
}