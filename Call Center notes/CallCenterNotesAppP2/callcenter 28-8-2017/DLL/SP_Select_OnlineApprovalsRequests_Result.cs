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
    
    public partial class SP_Select_OnlineApprovalsRequests_Result
    {
        public int RequestID { get; set; }
        public Nullable<int> ProviderID { get; set; }
        public Nullable<int> ProviderTypeID { get; set; }
        public Nullable<long> MedicalID { get; set; }
        public string MemberName { get; set; }
        public string MobileNumber { get; set; }
        public string ClientName { get; set; }
        public Nullable<long> ClaimNumber { get; set; }
        public Nullable<System.DateTime> ClaimDate { get; set; }
        public Nullable<int> CoInsurancePercentage { get; set; }
        public string Diagnose { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> RequestCreationTime { get; set; }
        public Nullable<int> RequestStatusID { get; set; }
        public Nullable<bool> IsFinnacialApproval { get; set; }
        public Nullable<bool> IsMedicalApproval { get; set; }
        public Nullable<long> IVRNumber { get; set; }
        public Nullable<bool> Delivered { get; set; }
        public Nullable<bool> Seen { get; set; }
        public Nullable<int> SeenByUserID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> RequestTypeID { get; set; }
        public string CallCenterAssignee { get; set; }
        public Nullable<System.DateTime> CloseTime { get; set; }
    }
}
