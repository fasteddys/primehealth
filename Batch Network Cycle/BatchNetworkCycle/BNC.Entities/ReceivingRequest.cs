//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BNC.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivingRequest
    {
        public int ReceivingRequestID { get; set; }
        public Nullable<System.DateTime> ReceivingDate { get; set; }
        public int BilllingMonth { get; set; }
        public int BillingYear { get; set; }
        public Nullable<decimal> ExpectedAmount { get; set; }
        public Nullable<int> ReceivedClaimsCount { get; set; }
        public System.DateTime TransferredToFIlterationDate { get; set; }
        public Nullable<int> ReceivingOfficerCalimsCount { get; set; }
        public int ReceivingOfficerFK { get; set; }
        public string ReceivingOfficerComment { get; set; }
        public int ProviderFK { get; set; }
        public int ReceivingStatusFK { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    }
}