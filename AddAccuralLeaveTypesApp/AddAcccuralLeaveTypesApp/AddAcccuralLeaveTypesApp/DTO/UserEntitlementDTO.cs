using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAcccuralLeaveTypesApp.DTO
{
    class UserEntitlementDTO
    {
        public int CypressID { get; set; }
        public string AccessControlID { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveTypeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime HiringDate { get; set; }
        public string ContractType { get; set; }
        public int RequestID { get; set; }
        public string RequestStatus { get; set; }
    }
}
