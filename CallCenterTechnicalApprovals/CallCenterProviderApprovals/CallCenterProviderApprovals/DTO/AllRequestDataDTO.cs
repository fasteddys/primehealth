using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterProviderApprovals.DTO
{
    public class AllRequestDataDTO
    {
        public EmailApprovalRequest Request { get; set; }
        public List<EmailRequestAttachmentsDetail> Attachments { get; set; }

        public EmailRequestRequest_TechnicalDestination TechnicalApprovalRequest { get; set; }

        public int TechnicalApprovalStatus { get; set; }
    }
}