using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
    public class HotelDTO
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public int? LanguageFK { get; set; }
        public int? OperatingCountryFK { get; set; }
        public int? OperatingLocationFK { get; set; }
        public List<LanguageDTO> Languges { get; set; }
        public List<OperatingCountryDTO> Countries { get; set; }
        public List<OperatingLocationDTO> Locations { get; set; }

    }
}
