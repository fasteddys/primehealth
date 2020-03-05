using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
   
    public class SpecialApprovalAuditCategViewDTO
    {
        public int SPReqID { get; set; }
        public Nullable<int> ReqFK { get; set; }
        public string SystemEntityName { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string ReqByUserNote { get; set; }
        public Nullable<int> SPStatusFK { get; set; }
        public string SPStatusName { get; set; }
        public Nullable<int> NumberOfPendingClaims { get; set; }
        public int BatchAuditRequestFK { get; set; }
        public int BatchingRequestFK { get; set; }
        public string BatchNumber { get; set; }
        public int BatchSystemFK { get; set; }
        public string SystemName { get; set; }
        public int BatchingFilterationDetailsFK { get; set; }
        public int FilterationRequestFK { get; set; }
        public int ReceivingRequestFK { get; set; }
        public int BillingYear { get; set; }
        public int BilllingMonth { get; set; }
        public int ProviderID { get; set; }
        public string ProviderName { get; set; }
        public Nullable<int> IbnSinaProviderPin { get; set; }
        public Nullable<int> MCAProviderPin { get; set; }
        public Nullable<int> ReqByUserFk { get; set; }
        public string UserName { get; set; }
        public Nullable<int> TeamFK { get; set; }
        public string TeamName { get; set; }
        public List<string> Reasons { get; set; }

        public string SpUserName { get; set; }

        public DateTime ActionOn { get; set; }

        public string spUserActionComment { get; set; }

    }
}
