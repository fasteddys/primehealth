//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stock_System2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Activity = new HashSet<Activity>();
            this.Internet_Request = new HashSet<Internet_Request>();
            this.Printer = new HashSet<Printer>();
        }
    
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string user_type { get; set; }
        public string department { get; set; }
        public string user_email { get; set; }
        public string user_role { get; set; }
    
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Internet_Request> Internet_Request { get; set; }
        public virtual ICollection<Printer> Printer { get; set; }
    }
}