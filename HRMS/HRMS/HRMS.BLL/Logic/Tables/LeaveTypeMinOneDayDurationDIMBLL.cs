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
    public class LeaveTypeMinOneDayDurationDIMBLL : Repository<LeaveTypeMinOneDayDurationDIM>
    {
        public LeaveTypeMinOneDayDurationDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public List<LeaveTypeMinOneDayDurationDTO> GetAllLeaveTypeMinOneDayDurations(int DurationUnitID)
        {
            List<LeaveTypeMinOneDayDurationDTO> leaveTypeMinOneDayDurationDTOs = new List<LeaveTypeMinOneDayDurationDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true&&p.DurationUnitFK== DurationUnitID).ToList())
            {
                LeaveTypeMinOneDayDurationDTO tempDTO = new LeaveTypeMinOneDayDurationDTO() {

                    MinOneDayDurationID = item.MinOneDayDurationID,
                   MinOneDayDurationName=item.MinOneDayDurationName 
                };

                leaveTypeMinOneDayDurationDTOs.Add(tempDTO);
            }
            return leaveTypeMinOneDayDurationDTOs;
        }
    }
}
