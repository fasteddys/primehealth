using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class AlertReportListvie
    {

       public int? OnTimeTickets { get; set; }
        public int? FirstWarningTickets { get; set; }

        public int? SecondWarningTickets { get; set; }
        public int? ThirdWarningTickets { get; set; }
        public int? TotalTickets { get; set; }
        public int? NormalTicketsViolatesTime { get; set; }
        public int? SpTicketsViolatesTime { get; set; }


        

    }
}