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
    public class LeaveTypeGainingEligibilityMethodDIMBLL : Repository<LeaveTypeGainingEligibilityMethodDIM>
    {
        DateTime creationDate;
        public LeaveTypeGainingEligibilityMethodDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public LeaveTypeGainingEligibilityMethodDIMBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public List<LeaveTypeGainingEligibilityMethodDTO> GetAllLeaveTypeGainingEligibilityMethods()
        {
            List<LeaveTypeGainingEligibilityMethodDTO> leaveTypeGainingEligibilityMethodDTOs = new List<LeaveTypeGainingEligibilityMethodDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true).ToList())
            {
                LeaveTypeGainingEligibilityMethodDTO tempDTO = new LeaveTypeGainingEligibilityMethodDTO()
                {

                    GainingEligibilityMethodID = item.GainingEligibilityMethodID,
                    GainingEligibilityMethodName = item.GainingEligibilityMethodName
                };
                leaveTypeGainingEligibilityMethodDTOs.Add(tempDTO);
            }
            return leaveTypeGainingEligibilityMethodDTOs;
        }
    }
}
