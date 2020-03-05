using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSystemReports.DTOs
{
   public class TicketsAverageTimeBySattusDTO
    {
        public int TicketTypeID { get; set; }
        public int AverageTime { get; set; }
        public string AverageTicketTimeSatsus { get; set; }

    }
}
