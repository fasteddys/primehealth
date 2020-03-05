using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.Entities.Business
{
    class RequestDTO
    {
        public int ID { get; set; }

        public int ProviderID { get; set; }
        
        public int ProviderType { get; set; }
        public int MedicalNumber { get; set; }
        public String MembderName { get; set; }
        public String ClinteName { get; set; }
        public int ClaimNumber { get; set; }
        public int ClaimDate { get; set; }
        public int CoInsurancePerc { get; set; }
        public String Diagnose { get; set; }
        public String Note { get; set; }
        public DateTime CreationTime { get; set; }
        public int SatatusID { get; set; }
        public bool FinnacialApproval { get; set; }
        public int MedicalApproval { get; set; }
        public int IVRNumber { get; set; }
        public bool Delivered { get; set; }
        public bool Seen { get; set; }
        public bool SeenByUserID { get; set; }
        public bool Deleted { get; set; }
        public int RequestTypeID { get; set; }

        


    }
}
