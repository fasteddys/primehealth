using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class OfficialHolidaysOutputDTO
    {
        public int officialHolidaysId { get; set; }
        public string officialHolidaysName { get; set; }
        public string officialHolidaysDate { get; set; }
        public string IsOfficial { get; set; }
        public string AddedBy { get; set; }


    }
}
