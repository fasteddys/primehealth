using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    //use it for
    //1 for input to add new recieving request
    //2 print invoice for providers
    public class RecievingRequestDTOS
    {
        // data input
        public int ProviderFK { get; set; }
        public Nullable<int> ReceivedClaimsCount { get; set; }
        public int ReceivingOfficerCalimsCount { get; set; }
        public int ReceivingOfficerFK { get; set; }
        public decimal ExpectedAmount { get; set; }
        public int BilllingMonth { get; set; }
        public int BillingYear { get; set; }
        public string ReceivingOfficerComment { get; set; }
        // data print invoice
        public int ReceivingRequestID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string ProviderName{ get; set; }
        public string AgentName { get; set; }
        public int CompanyFK { get; set; }
        public int GovernmentFK { get; set; }


    }
}
