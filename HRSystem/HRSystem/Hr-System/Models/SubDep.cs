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
    
    public partial class SubDep
    {
        public int ID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string SubDepartmentName { get; set; }
        public string DepTeamLeader { get; set; }
        public string DepName { get; set; }
        public string DepManager { get; set; }
        public string DepBackupSupervisor { get; set; }
        public string DepBackupTeamLeader { get; set; }
        public string DepSenior { get; set; }
    
        public virtual DepartmentTB DepartmentTB { get; set; }
    }
}