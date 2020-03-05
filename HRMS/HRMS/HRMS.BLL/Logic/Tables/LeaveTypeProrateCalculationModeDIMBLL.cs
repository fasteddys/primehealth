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
    public class LeaveTypeProrateCalculationModeDIMBLL : Repository<LeaveTypeProrateCalculationModeDIM>
    {
        public LeaveTypeProrateCalculationModeDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public List<LeaveTypeProrateCalculationModeDTO> GetAllProrateCalculationModes()
        {
            List<LeaveTypeProrateCalculationModeDTO> leaveTypeProrateCalculationModeDTOs = new List<LeaveTypeProrateCalculationModeDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true).ToList())
            {
                LeaveTypeProrateCalculationModeDTO temp = new LeaveTypeProrateCalculationModeDTO() {
                    ProrateCalculationModeID = item.ProrateCalculationModeID,
                    ProrateCalculationModeName = item.ProrateCalculationModeName
                };
                leaveTypeProrateCalculationModeDTOs.Add(temp);
            }

            return leaveTypeProrateCalculationModeDTOs;
        }

    }
}
