using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public  class RequestAttachmentDTO
    {
       public int RequestID { get; set; }
        public List<string> AttachmentPath { get; set; } 


    }
}
