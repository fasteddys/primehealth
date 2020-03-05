using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
  public  class TripDTO
    {
        public int TripID { get; set; }
        public int? LocationID { get; set; }
        public int? CountryID { get; set; }
        public string LocationName { get; set; }
        public string CountryName { get; set; }
        public int? CurrencyID { get; set; }
        public int? LangugeID { get; set; }

        public string TripName { get; set; }
        public string TripShortDesc { get; set; }
        public string TripDetailedDesc { get; set; }     
        //public string Price { get; set; }
        public List< PhotoDTO> Photos { get; set; }
        public string CurrencyName { get; set; }
        public List<TripCurrencyDTO> TripCurrency { get; set; }

    }
}
