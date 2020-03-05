using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowReceivingRequestDTO
    {
        public int RequestID { get; set; }
        public int BilllingMonth { get; set; }
        public int BillingYear { get; set; }
        public int ClaimsCount { get; set; }
        public string Action { get; set; }
    }
}
