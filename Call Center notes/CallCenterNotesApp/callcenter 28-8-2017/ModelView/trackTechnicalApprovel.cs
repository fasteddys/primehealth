using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class trackTechnicalApprovel
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
        public bool IsDone { get; set; }
        public string DepartmentName { get; set; }

    }
}