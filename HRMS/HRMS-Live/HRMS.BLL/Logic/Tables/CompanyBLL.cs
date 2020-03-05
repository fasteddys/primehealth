using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class CompanyBLL : Repository<Company>
    {
        DateTime creationDate;
        public CompanyBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }
    }
}
