using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
  public  class FeedBackDTO
    {
        public int ServiceID { get; set; }
        public int ClientID { get; set; }
        public int ClientVisitID { get; set; }

        public int RatingScale { get; set; }
        public string ClientComment { get; set; }


    }
}
