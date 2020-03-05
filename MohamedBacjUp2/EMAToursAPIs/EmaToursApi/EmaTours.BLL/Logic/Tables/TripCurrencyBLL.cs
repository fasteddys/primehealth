using EmaTours.DAL.Repositories;
using EmaTours.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.BLL.Logic
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

