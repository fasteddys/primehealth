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
   public class LeaveTypeFieldsDIMBLL : Repository<LeaveTypeFieldsDIM>
    {
        public LeaveTypeFieldsDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public List<LeaveTypeFieldDTO> GetAllLeaveTypeFieldsDIM()
        {
            List<LeaveTypeFieldDTO> ListLeaveTypeField = new List<LeaveTypeFieldDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true))
            {
                ListLeaveTypeField.Add(new LeaveTypeFieldDTO
                {
                     LeaveTypeFieldID = item.LeaveFieldID,
                     LeaveTypeFieldName = item.LeaveTypeFieldName

                });

            }
            
            return ListLeaveTypeField;
        }
    }
}
