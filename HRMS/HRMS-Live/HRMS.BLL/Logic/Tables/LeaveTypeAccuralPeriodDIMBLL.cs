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
    public class LeaveTypeAccuralPeriodDIMBLL : Repository<LeaveTypeAccuralPeriodDIM>
    {
        public LeaveTypeAccuralPeriodDIMBLL(HRMSEntities Context) : base(Context)
        {
        }

        public List<LeaveTypeAccuralPeriodsDTO> GetAllLeaveTypeAccuralPeriods()
        {
            List<LeaveTypeAccuralPeriodsDTO> AllLeaveTypeAccuralPeriods = new List<LeaveTypeAccuralPeriodsDTO>();

            foreach (var item in GetAll().Where(p=>p.IsActive==true).ToList())
            {
                LeaveTypeAccuralPeriodsDTO leaveTypeAccuralPeriodsDTO = new LeaveTypeAccuralPeriodsDTO {
                    AccuralPeriodID=item.AccuralPeriodID,
                    AccuralPeriodName=item.AccuralPeriodName
                };
                AllLeaveTypeAccuralPeriods.Add(leaveTypeAccuralPeriodsDTO);
            }

            return AllLeaveTypeAccuralPeriods;

        }
    }
}
