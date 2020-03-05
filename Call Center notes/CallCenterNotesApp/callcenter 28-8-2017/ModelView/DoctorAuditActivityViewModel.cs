using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class DoctorAuditActivityViewModel
    {
        public string DoctorAuditName { get; set; }
        public int? TotalNumberOfTickets { get; set; }
        public string TotalAvgDuration { get; set; }
        public int? TotalNumberOfInpatientRequests { get; set; }
        public int? TotalNumberOfOutPatientRequests { get; set; }
        public int? TotalNumberOfMedicationRequests { get; set; }
        public string AvgOutpatientDuration { get; set; }
        public string AvgInpatientDuration { get; set; }
        public string AvgMedicationDuration { get; set; }
    }
}