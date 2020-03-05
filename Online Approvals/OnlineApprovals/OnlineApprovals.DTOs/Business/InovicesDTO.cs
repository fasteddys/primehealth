using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{
    public class InovicesDTO
    {
        public int ID { get; set; }
        public int RequestIDs { get; set; }
        public long MedicalID { get; set; }
        public string ClientName { get; set; }
        public string MemberName { get; set; }
        public string ProviderName { get; set; }
        public string ProviderType { get; set; }
        public string Diagnose { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal CoInsuranceValue { get; set; }
        public decimal Total { get; set; }
        public DateTime InvoiceDate { get; set; }
        public long ClaimNumber { get; set; }
        public long IVRNumber { get; set; }
        public string Notes { get; set; }
        public DateTime RequestCloseTime { get; set; }
        public string AssignedUser { get; set; }




    }
}
