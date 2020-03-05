using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.Entities.Business
{
  
        public class Drug
        {
            public int ID { get; set; }
            public int DrugID { get; set; }
        public int RequestID { get; set; }
        public String DrugName { get; set; }
        public int UnitQuantity { get; set; }
        public int UnitPrice { get; set; }
        public int DemandedQuantity { get; set; }
        public double TotalPrice { get; set; }
        public bool IsDeleted { get; set; }



    }


}
