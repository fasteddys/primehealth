using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.DTOs.Business;

namespace HRMS.BLL.Logic.Tables
{
    public class MonthlyEffectiveAccuredLeaveTypesBLL : Repository<MonthlyEffectiveAccuredLeaveType>
    {
        DateTime creationDate;
        public MonthlyEffectiveAccuredLeaveTypesBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public List<MonthlyEffectiveAccuredLeaveTypesDTO> GetAllMonthlyEffectiveAccuredLeaveTypes()
        {
            List<MonthlyEffectiveAccuredLeaveTypesDTO> AllEffectiveAccuredLeave = new List<MonthlyEffectiveAccuredLeaveTypesDTO>();
            foreach(var items in GetAll().Where(x => x.IsActive == true).ToList())
            {
                AllEffectiveAccuredLeave.Add(new MonthlyEffectiveAccuredLeaveTypesDTO {
                    MonthlyEffectiveAccuredLeaveTypesID = items.MonthlyEffectiveAccuredLeaveTypesID,
                    LeaveTypeID = items.LeaveTypeFK.Value,
                    EffectiveDateFrom = items.EffectiveDateFrom.Value,
                    EffectiveDateTo = items.EffectiveDateTo.Value,
                    Year = items.Year.Value,
                    Month = items.Month.Value
                });
            }

            return AllEffectiveAccuredLeave;
        }
    }
}
