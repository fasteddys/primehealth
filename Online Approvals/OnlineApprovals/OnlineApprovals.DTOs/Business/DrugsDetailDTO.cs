using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{

    public class DrugsDetailDTO
    {
        public int ID { get; set; }
        public int? DrugID { get; set; }
        public int RequestID { get; set; }
        public string DrugName { get; set; }
        public int UnitQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? DemandedQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDrugList { get; set; }


    }


}
