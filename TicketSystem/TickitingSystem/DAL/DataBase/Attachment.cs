//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attachment
    {
        public int ID { get; set; }
        public Nullable<int> Ticket_Details_ID { get; set; }
        public string Path { get; set; }
        public Nullable<int> Ticket_ID { get; set; }
        public string FileName { get; set; }
    }
}
