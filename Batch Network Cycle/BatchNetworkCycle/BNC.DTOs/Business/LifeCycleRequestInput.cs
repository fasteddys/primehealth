using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
  public  class LifeCycleRequestInput
    {
        public int BilllingMonth { get; set; }
        public int BillingYear { get; set; }
        public int ProviderFK { get; set; }
        public int BatchSystemFK { get; set; }
        public string BatchNumber { get; set; }
        public int serachByBitch { get; set; }
    }
}
