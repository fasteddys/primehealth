﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
    public class TripCurrencyDTO
    {
     public int TripCurrencyID { get; set; }
        public string Price { get; set; }
        public int TripID { get; set; }
        public int CurrencyID { get; set; }


    }
}
