using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;

namespace EmaTours.BLL.Logic.Tables
{
    public class ConfigurationsBLL : Repository<Configuration>
    {
        DateTime creationDate;

        public ConfigurationsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public string GetConfigrationByKey(string ConfigrationKey)
        {
           return Find(x => x.ConfigurationKey == ConfigrationKey).FirstOrDefault().ConfigurationValue;


        }

    }

}
