using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class CompanyOfficialHolidayBLL : Repository<CompanyOfficialHoliday>
    {
        public CompanyOfficialHolidayBLL(HRMSEntities Context) : base(Context)
        {

        }
    }
}
