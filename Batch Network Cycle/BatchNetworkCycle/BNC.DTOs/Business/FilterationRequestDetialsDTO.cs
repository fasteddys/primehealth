using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
  public  class FilterationRequestDetialsDTO
    {
        public int FilterationRequestDetailID { get; set; }
        public int FilterationRequestID { get; set; }
        public int FilterationOfficerClaimsCount { get; set; }
        public string comment { get; set; }
        public int CategoryID { get; set; }
        public int OfficerID { get; set; }
        public int StatusID { get; set; }


        
    }
}
