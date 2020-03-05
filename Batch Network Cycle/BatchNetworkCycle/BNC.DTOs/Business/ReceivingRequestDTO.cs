using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs
{
   public class ReceivingRequestDTO
    {
        public int ReceivingRequestID { get; set; }
        public DateTime ReceivingDate { get; set; }
        public int BilllingMonth { get; set; }
        public int BillingYear { get; set; }
        public int ClaimsCount { get; set; }
        public int ReceivingOfficerID { get; set; }
        public int ProviderID { get; set; }
        public int StatusID { get; set; }
        public string ReceivingOfficerComment { get; set; }
        public decimal? ExpectedAmount { get; set; }
        public decimal? ReceivedClaimsCount { get; set; }
        public bool IsTransferdToFiltrationTeam { get; set; }
        public string AgentName { get; set; }
        public string ProviderName { get; set; }
        public string Action { get; set; }
        public int? CompanyFK { get; set; }
        public int? IbnSinaProviderPin { get; set; }
        public object MCAProviderPin { get; set; }
        public int? GovernmentFK { get; set; }
    }
}
