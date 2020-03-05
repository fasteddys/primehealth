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
    public class OperatingCountriesBLL : Repository<OperatingCountry>
    {
        DateTime creationDate;

        public OperatingCountriesBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public List<OperatingCountryDTO> GetAllOperatingCountries(int LanguageFK)
        {

            List<OperatingCountryDTO> ListOperatingCountry = new List<OperatingCountryDTO>();
           foreach (var item in     Find(x => x.LanguageFK == LanguageFK&&x.IsActive==true).ToList())
            {
                ListOperatingCountry.Add(new OperatingCountryDTO
                {
                    LanguageFK = item.LanguageFK,
                    OperatingCountryID = item.CountryID,
                    OperatingCountryName = item.ConutryName
                });
            }
            return ListOperatingCountry;

        }

   

    }

}
