//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailRequestAttachmentsDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Nullable<bool> IsDoctorAttachment { get; set; }
        public Nullable<bool> IsAuditAttachment { get; set; }
        public Nullable<bool> IsTicketAttachment { get; set; }
        public Nullable<bool> IsOtherAttachment { get; set; }
        public Nullable<bool> IsTransferToAuditAttach { get; set; }
        public Nullable<bool> IsFaxAttachment { get; set; }
        public Nullable<bool> IsReopeningAttachment { get; set; }
        public Nullable<int> TicketNumber { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual EmailApprovalRequest EmailApprovalRequest { get; set; }
    }
}
