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
        public List<OperatingCountryDTO> GetAllOperatingCountries()
        {

            List<OperatingCountryDTO> ListOperatingCountry = new List<OperatingCountryDTO>();
            foreach (var item in Find(x =>  x.IsActive == true).ToList())
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

        public void GetAllOperatingCountries(OperatingCountryDTO OperatingCountry)
        {
            Add(new OperatingCountry
            {
                ConutryName = OperatingCountry.OperatingCountryName,
                IsActive = true,
                CreationDate = creationDate,
                LanguageFK = OperatingCountry.LanguageFK,
            });

        }
        public OperatingCountryDTO GetOperatingCountryByID(int OperatingCountryID)
        {
            OperatingCountry OperatingCountry = Find(x => x.CountryID == OperatingCountryID).FirstOrDefault();

           return new  OperatingCountryDTO
            {
                 OperatingCountryName = OperatingCountry.ConutryName,
                 OperatingCountryID = OperatingCountry.CountryID,
                  LanguageFK= OperatingCountry.LanguageFK,
                  


           };

        }


        public void EditOperatingCountry(OperatingCountryDTO OperatingCountry)
        {
            OperatingCountry EditOperatingCountry = Find(x => x.CountryID == OperatingCountry.OperatingCountryID).FirstOrDefault();
            EditOperatingCountry.IsActive = true;
            EditOperatingCountry.CreationDate = creationDate;
            EditOperatingCountry.ConutryName = OperatingCountry.OperatingCountryName;
            EditOperatingCountry.LanguageFK = OperatingCountry.LanguageFK;
            EditOperatingCountry.CountryID = OperatingCountry.OperatingCountryID;

            Edit(EditOperatingCountry);
        }




    }

}
