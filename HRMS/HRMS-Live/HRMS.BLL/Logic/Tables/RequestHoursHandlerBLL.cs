using HRMS.DAL.Repositories;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
 public   class RequestHoursHandlerBLL : Repository<RequestHoursHandler>
    {
        DateTime creationDate;

        public RequestHoursHandlerBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
             
        }
        }
}
