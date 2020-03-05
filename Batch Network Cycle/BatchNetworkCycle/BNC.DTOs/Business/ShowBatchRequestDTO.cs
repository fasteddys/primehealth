using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowBatchRequestDTO
    {
        public int RequestID { get; set; }
        public int ClaimsCount { get; set; }
        public string System { get; set; }
        public string BatchNumber { get; set; }
        //public DateTime CreationDate { get; set; }
        public string Action { get; set; }
    }
}
