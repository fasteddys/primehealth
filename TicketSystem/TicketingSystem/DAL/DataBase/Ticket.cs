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
    
    public partial class Ticket
    {
        public int ID { get; set; }
        public Nullable<int> Ticket_ID { get; set; }
        public string Subject { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> User_ID { get; set; }
        public Nullable<int> Ticket_type_ID { get; set; }
        public Nullable<int> Status_ID { get; set; }
        public Nullable<int> Assign_Person_ID { get; set; }
        public Nullable<int> TicketSubtypeFK { get; set; }
    }
}
