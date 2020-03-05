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
    public class LeaveTypePartialDurationUnitDIMBLL : Repository<LeaveTypePartialDurationUnitDIM>
    {
        LeaveTypeDurationUnitDIMBLL leaveTypeDurationUnitDIMBLL;
        public LeaveTypePartialDurationUnitDIMBLL(HRMSEntities Context) : base(Context)
        {
            leaveTypeDurationUnitDIMBLL = new LeaveTypeDurationUnitDIMBLL(Context);
        }

        public List<LeaveTypePartialDurationUnitDTO> GetAllLeaveTypePartialDurationUnits(int DurationUnitID,int MinOneDayDurationID)
        {
            List<LeaveTypePartialDurationUnitDTO> leaveTypePartialDurationUnitDTOs = new List<LeaveTypePartialDurationUnitDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true&&p.MinOneDayDurationFK== MinOneDayDurationID).ToList())
            {
                LeaveTypePartialDurationUnitDTO tempDTO = new LeaveTypePartialDurationUnitDTO() {
                    PartialDurationUnitID = item.PartialDurationUnitID,
                    DurationUnitName = leaveTypeDurationUnitDIMBLL.GetAll().FirstOrDefault().LeaveDurationUnitName,
                    PartialDurationUnitName = item.PartialDurationUnitName ,
                    
                };
                leaveTypePartialDurationUnitDTOs.Add(tempDTO);
                
            }

            return leaveTypePartialDurationUnitDTOs;
        }
        
 public List<LeaveTypePartialDurationUnitDTO> GetPartialDurationUnit(int MinOneDayDuratioID)
        {
            List<LeaveTypePartialDurationUnitDTO> LeaveTypePartialDurationUnit = new List<LeaveTypePartialDurationUnitDTO>();
            foreach (var item in  Find(x => x.MinOneDayDurationFK == MinOneDayDuratioID))
            {
                LeaveTypePartialDurationUnit.Add(new LeaveTypePartialDurationUnitDTO
                {
                       PartialDurationUnitID= item.PartialDurationUnitID,
                     PartialDurationUnitName = item.PartialDurationUnitName,



                });

            }
            return LeaveTypePartialDurationUnit;
        }

    }
}
