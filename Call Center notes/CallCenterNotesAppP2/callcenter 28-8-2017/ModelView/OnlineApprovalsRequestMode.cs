using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class OnlineApprovalsRequestMode
    {
        public int RequestID { get; set; }
        public string Provider { get; set; }
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
        public string RequestStatus { get; set; }
      
        public Nullable<long> IVRNumber { get; set; }
       
      
   
        public Nullable<bool> IsDeleted { get; set; }
        public string RequestType { get; set; }
        public string CallCenterAssignee { get; set; }
        public Nullable<System.DateTime> CloseTime { get; set; }
    }
}