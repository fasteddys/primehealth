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
    public class LeaveTypeEntitlementTypeDIMBLL : Repository<LeaveTypeEntitlementTypeDIM>
    {
        DateTime creationDate;
        public LeaveTypeEntitlementTypeDIMBLL(HRMSEntities Context) : base(Context)
        {
        }

        public LeaveTypeEntitlementTypeDIMBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public List<LeaveTypeEntitlementTypeDTO> GetAllLeaveTypeEntitlementTypes()
        {
            List<LeaveTypeEntitlementTypeDTO> leaveTypeEntitlementTypeDTO = new List<LeaveTypeEntitlementTypeDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true).ToList())
            {
                LeaveTypeEntitlementTypeDTO tempDTO = new LeaveTypeEntitlementTypeDTO()
                {

                    LeaveTypeEntitlementTypeID = item.LeaveTypeEntitlementTypeID,
                    LeaveTypeEntitlementTypeName = item.LeaveTypeEntitlementTypeName
                };
                leaveTypeEntitlementTypeDTO.Add(tempDTO);
            }
            return leaveTypeEntitlementTypeDTO;
        }
    }
}
