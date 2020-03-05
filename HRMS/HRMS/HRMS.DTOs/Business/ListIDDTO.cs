using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class ListIDDTO
    {
        public int ID { get; set; }
        public int PositionFK { get; set; }

        public int? ModifiedByUserId { get; set; }


    }
}
