using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class LockRequestDTO
    {
        public int RequestID { get; set; }
        public string EntityName { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
    }
}
