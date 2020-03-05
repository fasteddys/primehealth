using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{
  
        public class AttachmentDTO
    {
        public int ID { get; set; }
        public int? RequestDetailsID { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsDeleted { get; set; }
        public int AttachmentTypeID { get; set; }
        public int RequestID { get; set; }
    }
   

}
