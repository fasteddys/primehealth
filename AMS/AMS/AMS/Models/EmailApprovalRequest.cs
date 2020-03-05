//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailApprovalRequest
    {
        public EmailApprovalRequest()
        {
            this.EmailRequestAttachmentsDetails = new HashSet<EmailRequestAttachmentsDetail>();
            this.EmailRequestMailingDetails = new HashSet<EmailRequestMailingDetail>();
        }
    
        public int ID { get; set; }
        public string ProviderName { get; set; }
        public string PatientName { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> Medical_ID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByNotes { get; set; }
        public Nullable<int> RequstStatusID { get; set; }
        public Nullable<System.DateTime> TransferedToDoctorsTime { get; set; }
        public string DoctorAssignee { get; set; }
        public Nullable<System.DateTime> DoctorAssignTime { get; set; }
        public string DoctorNotes { get; set; }
        public string DoctorAction { get; set; }
        public Nullable<System.DateTime> DoctorActionTime { get; set; }
        public string TransferedToAuditComment { get; set; }
        public string TechnicalApproveByDoctorNote { get; set; }
        public Nullable<System.DateTime> TechnicalApproveByDoctorStartTime { get; set; }
        public Nullable<System.DateTime> TechnicalApproveByDoctorEndTime { get; set; }
        public Nullable<System.DateTime> TransferedToAuditTime { get; set; }
        public string AuditAssignee { get; set; }
        public Nullable<System.DateTime> AuditAssigneeTime { get; set; }
        public string AuditNotes { get; set; }
        public string AuditAction { get; set; }
        public Nullable<System.DateTime> AuditActionTime { get; set; }
        public string TechnicalApproveByAuditNote { get; set; }
        public Nullable<System.DateTime> TechnicalApproveByAuditStartTime { get; set; }
        public Nullable<System.DateTime> TechnicalApproveByAuditEndTime { get; set; }
        public string MailSubject { get; set; }
        public Nullable<bool> IsAutoGenereated { get; set; }
        public Nullable<System.DateTime> ClosedTime { get; set; }
        public Nullable<bool> IsFirstNotifySent { get; set; }
        public Nullable<bool> IsSecondNotifySent { get; set; }
        public Nullable<bool> IsThirdNotifySent { get; set; }
        public Nullable<int> ColorID { get; set; }
        public Nullable<int> TicketTypeID { get; set; }
        public Nullable<bool> IsInquiryTicket { get; set; }
        public string AutoGeneratedEmailID { get; set; }
        public string OperatorAssignee { get; set; }
        public Nullable<System.DateTime> OperatorAssignTime { get; set; }
        public string OperatorAction { get; set; }
        public string OperatorNotes { get; set; }
        public Nullable<System.DateTime> OperatorActionTime { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public Nullable<int> ApprovalCategoryID { get; set; }
        public Nullable<bool> IsAutoGeneratedNotify { get; set; }
        public Nullable<bool> IsFaxMail { get; set; }
        public string MailSource { get; set; }
        public string FaxNumber { get; set; }
        public string UserNameOpenTechnicalApprove { get; set; }
        public Nullable<bool> IsReadyToBeFollowedUp { get; set; }
        public Nullable<bool> IsReadyToBeCompleted { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<System.DateTime> InsertionDate { get; set; }
    
        public virtual ColorAlert ColorAlert { get; set; }
        public virtual EmailRequestStatusDIM EmailRequestStatusDIM { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual ICollection<EmailRequestAttachmentsDetail> EmailRequestAttachmentsDetails { get; set; }
        public virtual ICollection<EmailRequestMailingDetail> EmailRequestMailingDetails { get; set; }
    }
}