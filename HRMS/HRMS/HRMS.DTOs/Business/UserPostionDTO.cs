using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class UserPositionDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public string PositionName { get; set; }

        public int isActive;


    }
}
