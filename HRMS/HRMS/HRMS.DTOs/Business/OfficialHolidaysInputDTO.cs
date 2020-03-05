using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class OfficialHolidaysInputDTO
    {
        public string OfficialHolidaysStart { get; set; }
        public string OfficialHolidaysName { get; set; }
        public int OfficialHolidaysType { get; set; }
        public int UserId { get; set; }
        public int OfficialHolidaysId { get; set; }


    }
}
