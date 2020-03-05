//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DispatchingMVCVer1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int ReqID { get; set; }
        public Nullable<System.DateTime> CreationRequestDate { get; set; }
        public string Creator { get; set; }
        public string ProviderType { get; set; }
        public string ProviderName { get; set; }
        public string ClaimsType { get; set; }
        public Nullable<int> NumOfBocklets { get; set; }
        public string Notes { get; set; }
        public string ReqStatues { get; set; }
        public string AgreementPerson { get; set; }
        public Nullable<System.DateTime> AgreementDate { get; set; }
        public string AssignPerson { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<int> StartClaims { get; set; }
        public Nullable<int> EndClaims { get; set; }
        public string PreparingPerson { get; set; }
        public Nullable<System.DateTime> PreparationData { get; set; }
        public string ClosedPerson { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public string ReqjectComment { get; set; }
        public string ApprovalType { get; set; }
        public string TransferedFrom { get; set; }
        public Nullable<System.DateTime> WasAssignedAT { get; set; }
        public string TransferPerson { get; set; }
        public string OriginalProviderName { get; set; }
        public Nullable<int> OriginalProviderID { get; set; }
    }
}
