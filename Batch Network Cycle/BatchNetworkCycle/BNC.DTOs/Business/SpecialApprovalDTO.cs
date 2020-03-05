using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
   public class SpecialApprovalDTO
    {
        public int SPStatusID { get; set; }
        public int UserID { get; set; }
        public string From { get; set; }
        public  string To { get; set; }
        public string BatchNumber { get; set; }
        public int ProviderFK { get; set; }
        public int SPReasonFK { get; set; }

    }
}
