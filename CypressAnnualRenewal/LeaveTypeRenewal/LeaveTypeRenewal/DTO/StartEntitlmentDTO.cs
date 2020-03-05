using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal.DTO
{
  public  class StartEntitlmentDTO
    {
        public List<EntitlmentDTO> LeaveEntitlmentOfStartYear { get; set; }
        public decimal StartEntitlmentTotal { get; set; }
    }
}
