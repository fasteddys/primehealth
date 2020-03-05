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
    public class LeaveTypeProrateMethodDIMBLL : Repository<LeaveTypeProrateMethodDIM>
    {
        public LeaveTypeProrateMethodDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public List<LeaveTypeProrateMethodDTO> GetAllLeaveTypeProrateMethods()
        {
            List<LeaveTypeProrateMethodDTO> leaveTypeProrateMethodDTOs = new List<LeaveTypeProrateMethodDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true).ToList())
            {
                LeaveTypeProrateMethodDTO temp = new LeaveTypeProrateMethodDTO() {
                    ProrateMethodID=item.ProrateMethodID,
                    ProrateMethodName=item.ProrateMethodName
                };

                leaveTypeProrateMethodDTOs.Add(temp);
            }

            return leaveTypeProrateMethodDTOs;
        }


    }
}
