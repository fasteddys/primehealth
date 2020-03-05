using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
  public  class TransactionRequestDTO
    {
        public int RequestTransactionSummaryID { get; set; }
        public int RequestFK { get; set; }
        public int EntityFK { get; set; }
        public Nullable<int> PersonFK { get; set; }
        public Nullable<int> TeamFK { get; set; }
        public int StatusFK { get; set; }
        public string TransactionComment { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }

    }
}
