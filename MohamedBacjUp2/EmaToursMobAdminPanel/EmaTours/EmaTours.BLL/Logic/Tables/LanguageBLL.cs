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
    public class LanguageBLL : Repository<LanguageDIM>
    {
        DateTime creationDate;

        public LanguageBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
      public List<LanguageDTO> GetAllLanguage()
        {
            List<LanguageDTO> ListLanguage = new List<LanguageDTO>();
            foreach (var item in GetAll())
            {
                ListLanguage.Add(new LanguageDTO
                {
                    LanguageID = item.LanaguageID,
                    LanguageName = item.LanguageName
                });

            }
            return ListLanguage;
        }

    }

}
