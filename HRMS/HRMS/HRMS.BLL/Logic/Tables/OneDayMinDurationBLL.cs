using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Tables
{
    public class OneDayMinDurationBLL : Repository<LeaveTypeMinOneDayDurationDIM>
    {
        DateTime creationDate;
        public OneDayMinDurationBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }
        public List< LeaveTypeMinOneDayDurationDTO> GetOneDayMinDurationByLeaveTypeUnitID(int DurationUnitID )
        {
            List<LeaveTypeMinOneDayDurationDTO> LeaveTypeMinOneDayDuration = new List<LeaveTypeMinOneDayDurationDTO>();
          foreach (var item in   Find(x => x.DurationUnitFK == DurationUnitID))
            {
                LeaveTypeMinOneDayDuration.Add(new LeaveTypeMinOneDayDurationDTO
                { MinOneDayDurationID=item.MinOneDayDurationID,
                 MinOneDayDurationName=item.MinOneDayDurationName,
                  MinOneDayDurationValue=(decimal) item.MinOneDayDurationValue



                });

            }
            return LeaveTypeMinOneDayDuration;
        }


    }
}



