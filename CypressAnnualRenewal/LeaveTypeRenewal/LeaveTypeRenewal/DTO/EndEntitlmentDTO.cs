using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal.DTO
{
    public class EndEntitlmentDTO
    {
        public List<EntitlmentDTO> LeaveEntitlmentOfEndYear { get; set; }
        public decimal TotalEndEntitlment { get; set; }


    }
}
