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
    
    public partial class RequestHandler
    {
        public int ID { get; set; }
        public string RequestType { get; set; }
        public string RequestSubType { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> Offday { get; set; }
        public string ReqStatus { get; set; }
        public Nullable<decimal> ReqDuration { get; set; }
        public string HalfDayOrFullDay { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public Nullable<int> OriginalRequestID { get; set; }
    }
}
