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
    public class LeaveTypeMonthlyAaccuralDaysDIMBLL : Repository<LeaveTypeMonthlyAaccuralDaysDIM>
    {
        public LeaveTypeMonthlyAaccuralDaysDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public List<LeaveTypeMonthlyAaccuralDaysDTO> GetLeaveTypeMonthlyAaccuralDays()
        {
            List<LeaveTypeMonthlyAaccuralDaysDTO> leaveTypeEntitlementSourceDTOs = new List<LeaveTypeMonthlyAaccuralDaysDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true).ToList())
            {
                LeaveTypeMonthlyAaccuralDaysDTO temp = new LeaveTypeMonthlyAaccuralDaysDTO()
                {
                     LeaveTypeMonthlyAaccuralDaysID = item.MonthlyAaccuralDaysID,
                     LeaveTypeMonthlyAaccuralDaysName = item.MonthlyAaccuralDaysName,
                      LeaveTypeMonthlyAaccuralDaysValue= item.Value,
                       LeaveTypeMonthlyAaccuralDaysDescription= item.MonthlyAaccuralDaysDescription

                };

                leaveTypeEntitlementSourceDTOs.Add(temp);
            }

            return leaveTypeEntitlementSourceDTOs;
        }
    }
}
