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
    public class WorkingShiftUserDailyBLL : Repository<WorkingShiftUserDaily>
    {
        DateTime creationDate;
        WorkingShiftBLL workingShiftBLL;

        public WorkingShiftUserDailyBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            workingShiftBLL = new WorkingShiftBLL(Context, CreationDate);
        }

        public List<String> CheckUserShiftExist(int UserID, DateTime ShiftStartDate, DateTime ShiftEndDate)
        {
            List<String> ExistShiftNames = new List<string>();

            while (ShiftStartDate.CompareTo(ShiftEndDate) <= 0)
            {
                WorkingShiftUserDaily workingShiftUserDaily = Find(x => x.UserFK == UserID && x.ShiftStartDate == ShiftStartDate && x.IsActive == true).FirstOrDefault();

                if (workingShiftUserDaily != null)
                    ExistShiftNames.Add(workingShiftBLL.Find(x => x.ShiftID == workingShiftUserDaily.ShiftFK).FirstOrDefault().ShiftName);
                else
                    ExistShiftNames.Add("");

                ShiftStartDate = ShiftStartDate.AddDays(1);
            }
            return ExistShiftNames;
        }

        public string GetUserShiftName(int UserID, string ShiftDate)
        {
            var NewShiftDateFormate = DateTime.ParseExact(ShiftDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date;

            WorkingShiftUserDaily workingShiftUserDaily = Find(x => x.UserFK == UserID && x.ShiftStartDate == NewShiftDateFormate && x.IsActive == true).FirstOrDefault();

            if (workingShiftUserDaily != null)
                return workingShiftBLL.Find(x => x.ShiftID == workingShiftUserDaily.ShiftFK).FirstOrDefault().ShiftName;
            else
                return "";
        }
        
        public bool CheckIfInsertNewShift(List<NewShiftForUserDTO> newShiftForUsers)
        {
            foreach (var items in newShiftForUsers)
            {
                WorkingShift workingShift = workingShiftBLL.Find(x => x.ShiftName == items.ShiftName).FirstOrDefault();
                WorkingShiftUserDaily workingShiftUserDaily = Find(x => x.ShiftFK == workingShift.ShiftID && x.UserFK == items.UserID && x.ShiftStartDate == items.ShiftDate).FirstOrDefault();

                if (workingShiftUserDaily == null)
                    return true;
            }
            return false;
        }

        public void AddNewShiftForUser(List<NewShiftForUserDTO> newShiftForUsers)
        {
            foreach (var items in newShiftForUsers)
            {
                DateTime ShiftEndDate = new DateTime();
                
                WorkingShift workingShift = workingShiftBLL.Find(x => x.ShiftName == items.ShiftName).FirstOrDefault();
                WorkingShiftUserDaily workingShiftUserDaily = Find(x => x.ShiftFK == workingShift.ShiftID && x.UserFK == items.UserID && x.ShiftStartDate == items.ShiftDate).FirstOrDefault();
                if (workingShiftUserDaily != null)
                    continue;

                //if(userbll)

                if (workingShift.IsOverNight == true)
                    ShiftEndDate = items.ShiftDate.AddDays(1);
                else
                    ShiftEndDate = items.ShiftDate;

                WorkingShiftUserDaily newWorkingShiftUserDaily = new WorkingShiftUserDaily
                {
                    UserFK = items.UserID,
                    ShiftFK = workingShift.ShiftID,
                    ShiftStartDate = items.ShiftDate,
                    ShiftEndDate = ShiftEndDate,
                    ShiftStartTime = workingShift.ShiftStart,
                    ShiftEndTime = workingShift.ShiftEnd,
                    GraceIn = workingShift.GraceIn,
                    GraceOut = workingShift.GraceOut,
                    AddedByUserFK = items.LoggedUserID,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now
                };
                Add(newWorkingShiftUserDaily);
            }            
        }

        public void CheckAssignedShifts(int ShiftID)
        {
            WorkingShiftUserDaily workingShiftUserDaily = Find(x => x.ShiftFK == ShiftID && x.IsActive == true).FirstOrDefault();

            if (workingShiftUserDaily != null)
            {
                throw new Exception("You can't edit this shift because it assigned to users");
            }
        }

        public void SwapShift(SwapShiftDTO swapShiftDTO)
        {           
            WorkingShift NewShift = workingShiftBLL.Find(x => x.ShiftID == swapShiftDTO.NewShiftID).FirstOrDefault();
            var NewShiftDateFormate = DateTime.ParseExact(swapShiftDTO.ShiftDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date;
            DateTime EndDate = new DateTime();

            if (swapShiftDTO.OldShiftName != "Off")
            {
                WorkingShift OldShift = workingShiftBLL.Find(x => x.ShiftName == swapShiftDTO.OldShiftName).FirstOrDefault();
                WorkingShiftUserDaily workingShiftUserDaily = Find(x => x.ShiftFK == OldShift.ShiftID && x.UserFK == swapShiftDTO.UserID && x.ShiftStartDate == NewShiftDateFormate).FirstOrDefault();
                workingShiftUserDaily.IsActive = false;
                workingShiftUserDaily.IsDeleted = true;
                Edit(workingShiftUserDaily);
            }           

            if (NewShift.IsOverNight == true)
                EndDate = NewShiftDateFormate.AddDays(1);
            else
                EndDate = NewShiftDateFormate;

            WorkingShiftUserDaily newWorkingShiftUserDaily = new WorkingShiftUserDaily
            {
                UserFK = swapShiftDTO.UserID,
                ShiftFK = NewShift.ShiftID,
                ShiftStartDate = NewShiftDateFormate,
                ShiftEndDate = EndDate,
                ShiftStartTime = NewShift.ShiftStart,
                ShiftEndTime = NewShift.ShiftEnd,
                GraceIn = NewShift.GraceIn,
                GraceOut = NewShift.GraceOut,
                AddedByUserFK = swapShiftDTO.LoggedUserID,
                IsActive = true,
                IsDeleted = false,
                CreationDate = DateTime.Now
            };
            Add(newWorkingShiftUserDaily);
        }
    }
}
