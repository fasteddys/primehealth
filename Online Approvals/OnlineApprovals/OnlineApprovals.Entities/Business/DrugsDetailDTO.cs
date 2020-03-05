using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.Entities.Business
{
  
        public class DrugsDetail
        {
            public int ID { get; set; }
            public string DrugName { get; set; }
        public int RequestID { get; set; }
        public int UnitQuaninty { get; set; }
        public int PriceForUnit { get; set; }
      
        public bool IsDeleted { get; set; }



    }


}
