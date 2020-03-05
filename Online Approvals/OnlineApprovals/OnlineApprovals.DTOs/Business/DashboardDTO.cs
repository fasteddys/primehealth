using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs.Business
{
  public  class DashboardDTO
    {
        public int? TotalTickets { get; set; }
        public int? PendingApprovalTickets { get; set; }
        public int? ApprovedTickets{ get; set; }
        public int? RejectedTickets{ get; set; }
        public int? TerminateTickets { get; set; }
        public int? PendingProviderApprovalTickets { get; set; }
        public int? TotalMembers { get; set; }
        public int? ClaimsPerCurrentMonth{ get; set; }
        public int? AssignedTickets { get; set; }
        public int? PendingProvidersTickets { get; set; }
        public int? ReopendTickets { get; set; }
        public int? NewTickets { get; set; }

        public string  UserName { get; set; }

        public string Location { get; set; }






    }
}
