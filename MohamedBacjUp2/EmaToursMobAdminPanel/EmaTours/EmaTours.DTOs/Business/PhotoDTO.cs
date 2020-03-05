using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
   public class PhotoDTO
    {
        public int PhotoID { get; set; }
        public int TripID { get; set; }

        public string PhotoName { get; set; }
        public string PhotoPath { get; set; }
        public int? LangugeID { get; set; }
        public bool IsActive { get; set; }


    }
}
