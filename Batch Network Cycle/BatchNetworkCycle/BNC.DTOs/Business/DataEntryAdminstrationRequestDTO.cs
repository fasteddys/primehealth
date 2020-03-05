using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class DataEntryAdminstrationRequestDTO
    {
        public int DataEntryAdminstrationRequestID { get; set; }
        public int BatchRequestFK { get; set; }
        public Nullable<int> DataEntryAdminAssigneeFK { get; set; }
        public string DataEntryAdminComment { get; set; }
        public int DataEntryAdministrationStatusFK { get; set; }
        public int? OriginalApprovedClaimsNumber { get; set; }
        public int? RemainingUnassignedNumberOfClaims { get; set; }
        public int BatchAuditingRequestID { get; set; }
        public string DataEntryAdministrationStatusName { get; set; }
        public Nullable<System.DateTime> TransferredToClosingTime { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> AssignedTime { get; set; }
        public string OfficerName { get; set;}
        public bool? IsLocked { get; set; }
    }
}
