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
    public class LeaveTypeFirstAccuralMethodDIMBLL : Repository<LeaveTypeFirstAccuralMethodDIM>
    {
        public LeaveTypeFirstAccuralMethodDIMBLL(HRMSEntities Context) : base(Context)
        {

        }

        public List<LeaveTypeFirstAccuralMethodsDTO> GetAllLeaveTypeFirstAccuralMethods()
        {
            List<LeaveTypeFirstAccuralMethodsDTO> leaveTypeFirstAccuralMethodsDTOs = new List<LeaveTypeFirstAccuralMethodsDTO>();
            foreach (var item in GetAll().Where(p=>p.IsActive==true).ToList())
            {
                LeaveTypeFirstAccuralMethodsDTO leaveTypeFirstAccuralMethodsDTO = new LeaveTypeFirstAccuralMethodsDTO() {
                    FirstAccuralMethodID=item.FirstAccuralMethodID,
                    FirstAccuralMethodName=item.FirstAccuralMethodName
                };
                leaveTypeFirstAccuralMethodsDTOs.Add(leaveTypeFirstAccuralMethodsDTO);
            }
            return leaveTypeFirstAccuralMethodsDTOs;
        }
    }
}
