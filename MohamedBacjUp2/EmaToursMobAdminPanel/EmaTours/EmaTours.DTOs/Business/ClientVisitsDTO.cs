using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
    public class ClientVisitsDTO
    {
        public int ClientID { get; set; }
        public List<VisitsDTO> ListVisits { get; set; }
    }
}
