using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
   public class OperatingCountryDTO
    {
       public int OperatingCountryID { get; set; }
        public string OperatingCountryName { get; set; }
        public int? LanguageFK { get; set; }
       public List<LanguageDTO> Languges { get; set; }

    }
}
