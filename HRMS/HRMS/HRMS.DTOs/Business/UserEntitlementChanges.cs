using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
 public   class UserEntitlementChangesDTO
    {
        public int UserEntitlementChangeID { get; set; }
        public decimal LimitBeforEntitlementChange { get; set; }
        public decimal LimitAfterEntitlementChange { get; set; }
        public string EntitlementChangeComment { get; set; }
        public string EntitlementChangeMaker { get; set; }
        public DateTime EntitlementChangeDate { get; set; }



    }
}
