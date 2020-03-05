using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{

    public class DrugsListDIMDTO
    {
        public int? DrugID { get; set; }
  
        public string DrugName { get; set; }
        public string Quaninty { get; set; }
        public int UnitQuaninty { get; set; }
        public bool IsDeleted { get; set; }
      


    }


}
