using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
  public  class SPReqestDTO
    {
        public int SPReqID { get; set; }
        public Nullable<int> ReqByUserFk { get; set; }
        public string ReqByUserName { get; set; }

        public Nullable<int> SPStatusFK { get; set; }
        public string SPStatusName { get; set; }

        public Nullable<int> ReqFK { get; set; }
        public Nullable<int> EntrityFK { get; set; }
        public string EntrityName { get; set; }

        public string ReqByUserNote { get; set; }
        public bool IsLoadedByUser { get; set; }
        public System.DateTime CreationDate { get; set; }


    }
}
