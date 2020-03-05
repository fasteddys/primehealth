using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
   public class ClientDTO
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPassport { get; set; }
        public int? NotificationMethodID { get; set; }

        public string HotelName { get; set; }
        public int? CountryID { get; set; }
        public string ClientDeviceID { get; set; }
        public int? LoggedUser { get; set; }


    }
}
