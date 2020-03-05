using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class LoginDTO
    {
        public string UserName{ get; set; }
        public string UserPassword { get; set; }
        public string IPAddress { get; set; }
        public int UserID { get; set; }
    }
}
