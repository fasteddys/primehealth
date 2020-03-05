using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypressPendingRequestsReminder.DTOs
{
  public  class RequestDTO
    {
        public int RequestID { get; set; }
        public int Order { get; set; }
        public Nullable<int> LeaveTypeFK { get; set; }
        public Nullable<int> UserFK { get; set; }
        public Nullable<int> RequesStatusFK { get; set; }
        public System.DateTime DurationFrom { get; set; }
        public System.DateTime DurationTo { get; set; }
        public System.DateTime BackToWork { get; set; }
        public decimal RequestDuration { get; set; }
        public Nullable<int> Level { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public Nullable<int> LeaveDurationUnitFK { get; set; }
        public Nullable<int> PartialDurationUnitFK { get; set; }
        public Nullable<int> Substitute { get; set; }
        public string Reason { get; set; }
        public string Comment { get; set; }
        public Nullable<int> OnBehalfOfRequesterID { get; set; }
        public bool IncluedHR { get; set; }

    }
}
