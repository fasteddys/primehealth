using System;
using System.Collections.Generic;
namespace BNC.Entities
{
  

    public partial class SpecialApprovalFinincalReq
    {
        public List<string> Reasons { get; set; }

        public string SpUserName { get; set; }

        public DateTime ActionOn { get; set; }

        public string spUserActionComment { get; set; }
    }
}
