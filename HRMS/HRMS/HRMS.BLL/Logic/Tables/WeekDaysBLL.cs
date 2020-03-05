using HRMS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Entities;
using HRMS.DTOs.Business;

namespace HRMS.BLL.Logic.Tables
{
  public  class WeekDaysBLL : Repository<WeekDaysDIM>
    {
        DateTime creationDate;
         
        public WeekDaysBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
  


    }
}
