using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class UserEntitlementSearchResultDTO
    {
        public int UserEntitlementID { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string UserName { get; set; }
        public decimal EntitlementTotal { get; set; }
        public string LeaveTypeName { get; set; }
        public int? AccessControlID { get; set; }
    }
}
