//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineApprovals.Entities
{
    using System;
    
    public partial class SP_Select_OnlineApprovalsAttachment_Result
    {
        public int ID { get; set; }
        public Nullable<int> RequestDetailsID { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsDeleted { get; set; }
        public int AttachmentTypeID { get; set; }
        public int RequestID { get; set; }
        public bool IsBackUpTaken { get; set; }
    }
}
