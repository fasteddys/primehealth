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
    
    public partial class Internet_Request
    {
        public int internet_billing_request_id { get; set; }
        public string Service_Provider_Name { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public System.DateTime warning_date { get; set; }
        public string email { get; set; }
        public System.DateTime date_request { get; set; }
        public int user_id { get; set; }
    
        public virtual User User { get; set; }
    }
}
