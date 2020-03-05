using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InHouseSendApproval.Model
{
    public  class ClientData
    {
        public int MedicalID { get; set; }
        public string ClientName { get; set; }
        public string ClientMail { get; set; }
        public List<string> AttachmentsUrl { get; set; }

    }
}
