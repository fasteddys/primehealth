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
    public class LeaveTypeDurationUnitDIMBLL : Repository<LeaveTypeDurationUnitDIM>
    {
        DateTime creationDate;

        public LeaveTypeDurationUnitDIMBLL(HRMSEntities Context) : base(Context)
        {

        }
        public LeaveTypeDurationUnitDIMBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }

        public List<LeaveTypeDurationUnitDTO> GetActiveLeaveTypeDurationUnits()
        {
            List<LeaveTypeDurationUnitDTO> AllActiveLeaveDurationUnits = new List<LeaveTypeDurationUnitDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true).ToList())
            {
                LeaveTypeDurationUnitDTO temp = new LeaveTypeDurationUnitDTO
                {

                    LeaveTypeDurationUnitID = item.LeaveDurationUnitID,
                    LeaveTypeDurationUnitName = item.LeaveDurationUnitName
                };
                AllActiveLeaveDurationUnits.Add(temp);
            }

            return AllActiveLeaveDurationUnits;
        }
    }
}
