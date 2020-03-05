using CallCenterNotesApp.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class RequestDetailsViewModel
    {
        public int ID
        {
            get;
            set;
        }


        public int MailSubject
        {
            get;
            set;
        }
        public int PatientName
        {
            get;
            set;
        }
        public int Medical_ID
        {
            get;
            set;
        }

        public int CompanyName
        {
            get;
            set;
        }
        public int CreatedByNotes
        {
            get;
            set;
        }
        public int CreatedBy
        {
            get;
            set;
        }
        public int CreationDate
        {
            get;
            set;
        }
        public int DoctorAssignee
        {
            get;
            set;
        }
        public int ClosedTime
        {
            get;
            set;
        }
        public int DoctorNotes
        {
            get;
            set;
        }
        public int OperatorNotes
        {
            get;
            set;
        }
        public int AuditNotes
        {
            get;
            set;
        }
        public int IsInquiryTicket
        {
            get;
            set;
        }
        public int AuditAssignee
        {
            get;
            set;
        }
        public int TechnicalApproveByAuditNote
        {
            get;
            set;
        }



        public int TransferedToAuditComment
        {
            get;
            set;
        }

        public int TechnicalApproveByDoctorNote
        {
            get;
            set;
        }

        public int MailSource
        {
            get;
            set;
        }

        public int OperatorAssignee
        {
            get;
            set;
        }
        public List<EmailRequestAttachmentsDetail> ListEmailRequestAttachments
        {
            get;
            set;
        }
        public List<EmailRequestMailingDetail> ListRequestMailingDetail
        {
            get;
            set;
        }



































    }
}