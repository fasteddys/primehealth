//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRMS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class FailedTransaction
    {
        public int FailedTransactionsID { get; set; }
        public string LogDetails { get; set; }
        public string LogType { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public string ErrorSource { get; set; }
    }
}