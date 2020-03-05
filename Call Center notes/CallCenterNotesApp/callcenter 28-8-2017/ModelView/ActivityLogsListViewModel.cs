using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class ActivityLogsListViewModel
    {
        public List<OperatorsActivityLogViewModel> OperatorsActivityLog { get; set; }       
        public List<DoctorAuditActivityViewModel> DoctorsActivityLog { get; set; }       
        public List<DoctorAuditActivityViewModel> AuditActivityLog { get; set; }
    }
}