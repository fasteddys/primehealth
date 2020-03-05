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
    public class LeaveTypeConsiderationAsDIMBLL : Repository<LeaveTypeConsiderationAsDIM>
    {

        DateTime creationDate;
        public LeaveTypeConsiderationAsDIMBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;


        }

        public List<LeaveTypeConsiderationAsDTO> GetActiveLeaveTypeConsiderationAs()
        {
            List<LeaveTypeConsiderationAsDTO> AllActiveLeaveCalculationTypes = new List<LeaveTypeConsiderationAsDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true).ToList())
            {
                LeaveTypeConsiderationAsDTO tempDTO =new LeaveTypeConsiderationAsDTO
                {   LeaveTypeConsiderationAsID=item.LeaveTypeConsiderationAsID,

                    LeaveTypeConsiderationAsName=item.LeaveTypeConsiderationAsName
                };
                AllActiveLeaveCalculationTypes.Add(tempDTO);
            }

            return AllActiveLeaveCalculationTypes;

        }
    }
}
