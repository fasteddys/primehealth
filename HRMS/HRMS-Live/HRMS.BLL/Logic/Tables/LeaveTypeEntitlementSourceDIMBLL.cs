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
    public class LeaveTypeEntitlementSourceDIMBLL : Repository<LeaveTypeEntitlementSourceDIM>
    {
        public LeaveTypeEntitlementSourceDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public List<LeaveTypeEntitlementSourceDTO> GetLeaveTypeEntitlementSources()
        {
            List<LeaveTypeEntitlementSourceDTO> leaveTypeEntitlementSourceDTOs = new List<LeaveTypeEntitlementSourceDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true).ToList())
            {
                LeaveTypeEntitlementSourceDTO temp = new LeaveTypeEntitlementSourceDTO() {
                    EntitlementSourceID=item.EntitlementSourceID,
                    EntitlementSourceName=item.EntitlementSourceName
                };

                leaveTypeEntitlementSourceDTOs.Add(temp);
            }

            return leaveTypeEntitlementSourceDTOs;
        }
    }
}
