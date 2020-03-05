using CallCenterNotesApp.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class ModelViewSearchTable
    {
        public int ID
        {
            get;
            set;
        }
        public  int? MedicalID
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public  int? RequstStatusID
        {
            get;
            set;
        }

        public string DoctorAssignee
        {
            get;
            set;
        }

        public string AuditAssignee
        {
            get;
            set;
        }
        public string MailSubject
        {
            get;
            set;
        }
        public  int? TicketTypeID
        {
            get;
            set;
        }
        public string RequestStatusName { get; set; }

        public string RequestTypeName { get; set; }
        public string OperatorAssignee { get; set; }
        public string Priority { get; set; }
        public string CategoryName { get; set; }
        public int? PriorityID{ get; set; }
        public int? ApprovalCategoryID { get; set; }
        public DateTime InsertionDate { get; set; }



    }
}