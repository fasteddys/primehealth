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
    
    public partial class Event
    {
        public Event()
        {
            this.EmpEvents = new HashSet<EmpEvent>();
        }
    
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string EventFrom { get; set; }
        public string EventTo { get; set; }
        public Nullable<System.DateTime> EventDay { get; set; }
    
        public virtual ICollection<EmpEvent> EmpEvents { get; set; }
    }
}