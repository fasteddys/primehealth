using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class LeaveTypeCarryOverRuleBLL : Repository<LeaveTypeCarryOverRule>
    {
        DateTime creationDate;

        public LeaveTypeCarryOverRuleBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }
    }
}
