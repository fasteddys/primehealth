using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{
    public class RequestDTO
    {
        public int ID { get; set; }
        public int? ProviderID { get; set; }
        public int? ProviderType { get; set; }
        public long? MedicalID { get; set; }
        public string MemberName { get; set; }
        public string MobileNumber { get; set; }
        public string ClientName { get; set; }
        public long? ClaimNumber { get; set; }
        public DateTime ClaimDate { get; set; }
        public int? CoInsurancePerc { get; set; }
        public string Diagnose { get; set; }
        public string Note { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? StatusID { get; set; }
        public bool? FinnacialApproval { get; set; }
        public bool? MedicalApproval { get; set; }
        public long? IVRNumber { get; set; }
        public bool? Delivered { get; set; }
        public bool? Seen { get; set; }
        public int? SeenByUserID { get; set; }
        public bool? Deleted { get; set; }
        public int? RequestTypeID { get; set; }

        public string CallCenterAssignee { get; set; }
        public DateTime? CloseTime { get; set; }
        public string ProviderName { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalApprovalPrice { get; set; }
        public decimal TotalMemberpays { get; set; }




    }
}
