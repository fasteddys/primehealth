using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class PositionDTO
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public int UsersCount { get; set; }
    }
}
