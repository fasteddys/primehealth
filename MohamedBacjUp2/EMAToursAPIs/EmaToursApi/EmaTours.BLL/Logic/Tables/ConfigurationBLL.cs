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
    public class ConfigurationBLL : Repository<Configuration>
    {
        DateTime creationDate;

        public ConfigurationBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public string GetConfigurationValueByKey(string ConfigurationKey,int LanguageId)
        {

            return Find(x => x.ConfigurationKey == ConfigurationKey&&x.LangauageFK== LanguageId).FirstOrDefault()?.ConfigurationValue;
        }

    }

}
