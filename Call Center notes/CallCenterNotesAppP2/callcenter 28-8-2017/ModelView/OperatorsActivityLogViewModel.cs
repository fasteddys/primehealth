using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class OperatorsActivityLogViewModel
    {
        public string OperatorAsignee { get; set; }
        public int? NumberOfTickets { get; set; }
        public string AverageTicketTime { get; set; }
    }
}