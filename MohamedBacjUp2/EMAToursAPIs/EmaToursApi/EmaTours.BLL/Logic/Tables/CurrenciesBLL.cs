using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;

namespace EmaTours.BLL.Logic.Tables
{
  public  class CurrenciesBLL : Repository<Currency>
    {
        DateTime creationDate;

        public CurrenciesBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        
    }

}
