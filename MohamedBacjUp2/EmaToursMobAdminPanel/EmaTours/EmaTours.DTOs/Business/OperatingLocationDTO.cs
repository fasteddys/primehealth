using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
  public  class OperatingLocationDTO
    {
        public int OperatingLocationID { get; set; }
        public string OperatingLocationName { get; set; }
        public int? LanguageID { get; set; }
        public int? CountryID { get; set; }
        public List<LanguageDTO> Languges { get; set; }
        public List<OperatingCountryDTO> Countries { get; set; }


    }
}
