using CallCenterNotesApp.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class ModelViewTable
    {
        public int ID
        {
            get;
            set;
        }

        public string ProviderName
        {
            get;
            set;
        }

        public string PatientName
        {
            get;
            set;
        }

        public string CompanyName
        {
            get;
            set;
        }

        public  int? MedicalID
        {
            get;
            set;
        }

        public  System.DateTime? CreationDate
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public string CreatedByNotes
        {
            get;
            set;
        }

        public  int? RequstStatusID
        {
            get;
            set;
        }

        public  System.DateTime TransferedToDoctorsTime
        {
            get;
            set;
        }

        public string DoctorAssignee
        {
            get;
            set;
        }

        public  System.DateTime? DoctorAssignTime
        {
            get;
            set;
        }

        public string DoctorNotes
        {
            get;
            set;
        }

        public string DoctorAction
        {
            get;
            set;
        }

        public  System.DateTime? DoctorActionTime
        {
            get;
            set;
        }

        public string TransferedToAuditComment
        {
            get;
            set;
        }

        public string TechnicalApproveByDoctorNote
        {
            get;
            set;
        }

        public  System.DateTime? TechnicalApproveByDoctorStartTime
        {
            get;
            set;
        }

        public  System.DateTime? TechnicalApproveByDoctorEndTime
        {
            get;
            set;
        }

        public  System.DateTime? TransferedToAuditTime
        {
            get;
            set;
        }

        public string AuditAssignee
        {
            get;
            set;
        }

        public  System.DateTime? AuditAssigneeTime
        {
            get;
            set;
        }

        public string AuditNotes
        {
            get;
            set;
        }

        public string AuditAction
        {
            get;
            set;
        }

        public  System.DateTime?  AuditActionTime;

        public string TechnicalApproveByAuditNote
        {
            get;
            set;
        }

        public  System.DateTime? TechnicalApproveByAuditStartTime
        {
            get;
            set;
        }

        public  System.DateTime? TechnicalApproveByAuditEndTime
        {
            get;
            set;
        }

        public string MailSubject
        {
            get;
            set;
        }

        public  bool IsAutoGenereated
        {
            get;
            set;
        }

        public  System.DateTime? ClosedTime
        {
            get;
            set;
        }

        public  int? MailingDetailsID
        {
            get;
            set;
        }

        public  bool IsFirstNotifySent
        {
            get;
            set;
        }

        public  bool IsSecondNotifySent
        {
            get;
            set;
        }

        public  bool IsThirdNotifySent
        {
            get;
            set;
        }

        public  int? ColorID
        {
            get;
            set;
        }

        public  int? TicketTypeID
        {
            get;
            set;
        }

        public  bool IsInquiryTicket
        {
            get;
            set;
        }

        public  int? AttachmentDetailsID
        {
            get;
            set;
        }
        public string ColorHex
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



    }
}