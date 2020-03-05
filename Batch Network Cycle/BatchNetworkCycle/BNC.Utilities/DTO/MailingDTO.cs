using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.Utilities.DTO
{
   public class MailingDTO
    {
        public int RequestID { get; set; }
        public int ActionTypeID { get; set; }
        public string Action{ get; set; }
        public string ActionBy{ get; set; }
        public string Actionto{ get; set; }
        public string NextActionto { get; set; }
        public int? DurationUnitID { get; set; }

        public List<string> To{ get; set; }
        public List<string> CC{ get; set; }
        public string MailDomain{ get; set; }
        public string MailUserName{ get; set; }
        public string LeaveTypeName{ get; set; }
        public string MailPassword{ get; set; }
        public decimal Duration{ get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndtDate{ get; set; }
        public DateTime ComeBackDate { get; set; }
        public string LeaveStartDate { get; set; }
        public string LeaveEndtDate { get; set; }
        public string LeaveComeBackDate { get; set; }
        public int? Gender{ get; set; }
        public string MailHeader{ get; set; }
        public string MailFooter{ get; set; }
        public string MailPictureLogo{ get; set; }
        public string MailURL{ get; set; }
        public string AutodiscoverUrl{ get; set; }
        public string Unit{ get; set; }
        public string ManagerName { get; set; }
        public string RejectionReason { get; set; }

        public bool IsFinalApproved { get; set; }

    }
}
