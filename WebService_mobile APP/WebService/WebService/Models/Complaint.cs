//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Complaint
    {
        public int ID { get; set; }
        public string rFrom { get; set; }
        public string Email { get; set; }
        public string rSubject { get; set; }
        public string Department { get; set; }
        public string rBody { get; set; }
        public string rStatus { get; set; }
        public string rStatusColor { get; set; }
        public Nullable<System.DateTime> rDate { get; set; }
        public Nullable<System.DateTime> ManagerCommentDate { get; set; }
        public string ManagerComment { get; set; }
        public string ClientName { get; set; }
        public string ManagerName { get; set; }
        public string Seen { get; set; }
        public string Replied { get; set; }
        public Nullable<System.DateTime> assigneddate { get; set; }
    }
}
