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
    
    public partial class ProjectsTask
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Nullable<System.DateTime> TaskFrom { get; set; }
        public Nullable<System.DateTime> TaskTo { get; set; }
        public string TaskAssignee { get; set; }
        public string TaskBackup { get; set; }
        public string TaskCreator { get; set; }
        public Nullable<System.DateTime> TaskCreationDate { get; set; }
        public string Notification { get; set; }
    }
}
