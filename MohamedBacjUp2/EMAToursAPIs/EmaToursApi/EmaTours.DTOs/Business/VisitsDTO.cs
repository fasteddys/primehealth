using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
  public  class VisitsDTO
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }

        public int VisitID { get; set; }
        public int HotelID { get; set; }
        public int?  CountryID { get; set; }
        public int? LocationID { get; set; }
        public string CountryName { get; set; }
        public string LocationName { get; set; }
        public DateTime? StartVisitDate { get; set; }
        public DateTime? EndVisitDate { get; set; }
        public string HotelName { get; set; }
        public List<ClientTripDTO> ListTrips { get; set; }
        public string ClientOverallVisitComment { get; set; }
        public int? LoggedUser { get; set; }

    }
}
