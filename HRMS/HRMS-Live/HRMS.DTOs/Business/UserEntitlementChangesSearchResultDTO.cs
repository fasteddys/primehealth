using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class UserEntitlementChangesSearchResultDTO
    {
        public int UserEntitlementChangeID { get; set; }
        public string Comment { get; set; }
        public decimal EntitlementBefore { get; set; }
        public decimal EntitlementTo { get; set; }
        public decimal RequestDuration { get; set; }
        public string EntitlementChangedBy { get; set; }
        public string UserChangeEntitlement { get; set; }
        public string LeaveTypeName { get; set; }
        public Nullable<DateTime> ActionDate { get; set; }
        public string UserName { get; set; }
        public string Request { get; set; }
        public string EntitlementBeforeTo { get; set; }
        public int? AccessControlID { get; set; }

        //public string AccessControlID { get; set; }
        //public Nullable<DateTime> DurationTo { get; set; }
        //public Nullable<DateTime> BackToWork { get; set; }
    }
}
