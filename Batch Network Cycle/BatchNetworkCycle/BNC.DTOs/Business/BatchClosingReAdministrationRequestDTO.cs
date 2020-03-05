using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class BatchClosingReAdministrationRequestDTO
    {
        public int BatchClosingReAdministrationRequestID { get; set; }
        public int BatchClosingRequestFK { get; set; }
        public Nullable<DateTime> ReceivedFromClosingOn { get; set; }
        public Nullable<int> AssignedByAdminFK { get; set; }
        public string ConfirmedReceivingComment { get; set; }
        public Nullable<DateTime>  ConfirmedReceivingOn { get; set; }
        public int ReAdministrationStatusFK { get; set; }
        public string ReAdministrationStatusName { get; set; }
        public int ArchivingSystemTicketNo { get; set; }
        public string FinalClosureComment { get; set; }
        public Nullable<DateTime> FinalClosureTime { get; set; }
        public string AssignedByAdminComment { get; set; }
        public string ReAdministrationOfficerName { get; set; }
        public Nullable<DateTime> ReAdministrationOfficerAssignedTime { get; set; }
        public int? TotalClaimsCount { get; set; }
    }
}
