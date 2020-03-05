using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Tables
{
    public class ConfigurationBLL : Repository<Configuration>
    {
        DateTime creationDate;
        public ConfigurationBLL(HRMSEntities Context,DateTime CreationDate) : base(Context) 
        {
            creationDate = CreationDate;

        }
    }
}
