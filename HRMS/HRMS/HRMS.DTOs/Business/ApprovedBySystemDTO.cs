using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class ApprovedBySystemDTO
    {
        public int RequestID { get; set; }
        public int RequestStatusID { get; set; }
        public int EpmlyeeID { get; set; }
        public string Exception { get; set; }
        public bool HasException { get; set; }





    }
}
