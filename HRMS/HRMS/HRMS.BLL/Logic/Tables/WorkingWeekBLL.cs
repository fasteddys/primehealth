using HRMS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Entities;
using HRMS.DTOs.Business;
using HRMS.Utilities;

namespace HRMS.BLL.Logic.Tables
{
    public class WorkingWeekBLL : Repository<WorkingWeek>
    {
        //WorkingWeekDetailsBLL workingWeekDetailsBLL;
        //UserBLL userBLL;
        DateTime creationDate;
        public WorkingWeekBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            //workingWeekDetailsBLL = new WorkingWeekDetailsBLL(Context, CreationDate);
            creationDate = CreationDate;

        }
        public WorkingWeek AddNewWorkingWeek(string WorkingWeekName)
        {
     
            
            WorkingWeek WorkingWeek = new WorkingWeek() {

                 WorkingWeekName= WorkingWeekName,
                 CreationDate= creationDate,
                 IsActive =true,
                 IsDeleted=false,
                   
            };
            Add(WorkingWeek);
            return WorkingWeek;
        }

        public void EditWorkingWeek(WorkingWeekDTO WorkingWeek)
        {
            //WorkingWeek workingWeek = new WorkingWeek();
            //context.Configuration.ProxyCreationEnabled = false;
            WorkingWeek workingWeek = GetAll().Where(x => x.WorkingWeekID == WorkingWeek.WorkingWeekID).FirstOrDefault();
            workingWeek.WorkingWeekName = WorkingWeek.WorkingWeekName;
            Edit(workingWeek);
            //Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserprofileDTO.ModifiedByUserId, oldWorkingWeek, XMLObjectConverter.Serialize(workingWeek), "Edit User Data");
        }

        public WorkingWeekDTO GetWorkingWeekByID(int WorkingWeekID)
        {
            WorkingWeek WorkingWeek = Find(x => x.WorkingWeekID == WorkingWeekID).FirstOrDefault();

            return new WorkingWeekDTO
            {
                WorkingWeekID = WorkingWeek.WorkingWeekID,
                WorkingWeekName = WorkingWeek.WorkingWeekName,
            };

        }


       
    }
}
