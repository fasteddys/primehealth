using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal.DTO
{
    public class UserDTO
    {
        public int CypressID { get; set; }
        public string UserName { get; set; }

        public string BirthDate { get; set; }
        public string HireDate { get; set; }
        public List<int> UserEntitlementIDS { get; set; }
        public int AccID { get; internal set; }
        public string CompanyName { get; internal set; }
    }
}
