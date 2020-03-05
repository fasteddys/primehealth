using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class DoctorActivityLogViewModel
    {
        public string DoctorAsignee { get; set; }
        public int? NumberOfTickets { get; set; }
        public int? AverageTicketTime { get; set; }
        public int? ApprovalCategoryID { get; set; }

    }
}