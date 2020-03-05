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
  public  class CurrenciesBLL : Repository<Currency>
    {
        DateTime creationDate;

        public CurrenciesBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<CurrencyDTO> GetAllCurrencies()
        {
            List<CurrencyDTO> ListCurrency = new List<CurrencyDTO>();
            foreach (var item in GetAll())
            {
                ListCurrency.Add(new  CurrencyDTO
                {
                     CurrencyID = item.CurrencyID,
                      CurrencyName = item.CurrencyName,
                });
            }
            return ListCurrency;


        }

    }

}
