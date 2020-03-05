using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class UserEntitlementsDTO
    {
        public int UserEntitlementID { get; set; }
        public decimal EntitlementModifiedQty { get; set; }
        public int UserFK { get; set; }

        public int? ModifiedByUserId { get; set; }

        public int LeaveTypeFK { get; set; }
        public int LeaveDurationUnitFK { get; set; }
        public string ArName { get; set; }
        public Nullable<int> Year { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ModificationDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public bool ISAddition { get; set; }
        public string Comment { get; set; }
        public int ModifiedBy { get; set; }
        public string DurationUnit { get; set; }
        public int? Years { get; set; }
        public int? Month { get; set; }
    }
}
