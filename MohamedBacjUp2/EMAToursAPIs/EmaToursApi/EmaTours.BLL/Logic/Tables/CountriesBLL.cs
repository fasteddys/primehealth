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
    public class CountriesBLL : Repository<CountriesDIM>
    {
        DateTime creationDate;

        public CountriesBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<CountryDTO> GetAllCountries()
        {
            List<CountryDTO> Countries = new List<CountryDTO>();
            foreach (var item in GetAll())
            {

                Countries.Add(new CountryDTO
                {
                    CountryID = item.CountryID,
                    CountryCode = item.CountryCode,
                    CountryName = item.CountryName
                });

            }
            return Countries;
        }
 


    }

}
