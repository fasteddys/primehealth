using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;

namespace EmaTours.BLL.Logic.Tables
{
    public class TripCurrencyBLL : Repository<TripCurrency>
    {
        DateTime creationDate;
        public TripCurrencyBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

 

    }

}
