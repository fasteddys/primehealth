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
    public class LeaveTypeFieldsVisibilityDIMBLL : Repository<LeaveTypeFieldsVisibilityDIM>
    {
        public LeaveTypeFieldsVisibilityDIMBLL(HRMSEntities Context) : base(Context)
        {

        } 
        public List<LeaveTypeFieldsVisibilityDTO> GetAllLeaveTypeFieldsVisibility()
        {
            List<LeaveTypeFieldsVisibilityDTO> ListLeaveTypeFieldsVisibility = new List<LeaveTypeFieldsVisibilityDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true))
            {
                ListLeaveTypeFieldsVisibility.Add(new LeaveTypeFieldsVisibilityDTO
                {
                      LeaveTypeFieldsVisibilityID = item.LeaveTypeFieldsVisibilityID,
                     LeaveTypeFieldsVisibilityName = item.LeaveTypeFieldsVisibilityName

                });

            }



            return ListLeaveTypeFieldsVisibility;
        }
    }
}
