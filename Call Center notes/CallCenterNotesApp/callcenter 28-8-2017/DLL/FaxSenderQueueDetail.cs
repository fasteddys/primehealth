//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallCenterNotesApp.DLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FaxSenderQueueDetail
    {
        public int FaxQueueDetailsID { get; set; }
        public Nullable<int> FaxQueueFK { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public Nullable<System.DateTime> SuccessSendingTime { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
    
        public virtual FaxSenderQueue FaxSenderQueue { get; set; }
    }
}