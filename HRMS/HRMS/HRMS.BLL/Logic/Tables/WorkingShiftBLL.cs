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
    public class WorkingShiftBLL : Repository<WorkingShift>
    {
        DateTime creationDate;
        UserBLL userBLL;
        //WorkingShiftUserDailyBLL workingShiftUserDailyBLL;
        public WorkingShiftBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            userBLL = new UserBLL(Context, CreationDate);
            //workingShiftUserDailyBLL = new WorkingShiftUserDailyBLL(Context, CreationDate);
        }

        public List<WorkingShiftDTO> GetAllWorkingShift()
        {
            List<WorkingShiftDTO> workingShiftDTOList = new List<WorkingShiftDTO>();
            List<WorkingShift> workingShiftsList = GetAll().ToList();

            foreach (var items in workingShiftsList)
            {
                workingShiftDTOList.Add(new WorkingShiftDTO
                {
                    ShiftID = items.ShiftID,
                    ShiftName = items.ShiftName,
                    ShiftStart = items.ShiftStart,
                    ShiftEnd = items.ShiftEnd,
                    GraceIn = items.GraceIn,
                    GraceOut = items.GraceOut,
                    IsActive = items.IsActive.Value
                });
            }
            return workingShiftDTOList;
        }
        public List<WorkingShiftDTO> GetAllActiveWorkingShift()
        {
            List<WorkingShiftDTO> workingShiftDTOList = new List<WorkingShiftDTO>();
            List<WorkingShift> workingShiftsList = Find(x => x.IsActive == true).ToList();

            foreach (var items in workingShiftsList)
            {
                workingShiftDTOList.Add(new WorkingShiftDTO
                {
                    ShiftID = items.ShiftID,
                    ShiftName = items.ShiftName,
                    ShiftStart = items.ShiftStart,
                    ShiftEnd = items.ShiftEnd,
                    GraceIn = items.GraceIn,
                    GraceOut = items.GraceOut,
                    IsActive = items.IsActive.Value
                });
            }
            return workingShiftDTOList;
        }
        public List<WorkingShiftDTO> GetAllActiveWorkingShift(int LoggedUserID)
        {
            int departmentID = userBLL.Find(x => x.UserID == LoggedUserID).FirstOrDefault().DepartmentFK.Value;
            List<WorkingShiftDTO> workingShiftDTOList = new List<WorkingShiftDTO>();
            List<WorkingShift> workingShiftsList = Find(x => x.DepartmentFK == departmentID && x.IsActive == true).ToList();

            foreach (var items in workingShiftsList)
            {
                workingShiftDTOList.Add(new WorkingShiftDTO
                {
                    ShiftID = items.ShiftID,
                    ShiftName = items.ShiftName,
                    ShiftStart = items.ShiftStart,
                    ShiftEnd = items.ShiftEnd,
                    GraceIn = items.GraceIn,
                    GraceOut = items.GraceOut,
                    IsActive = items.IsActive.Value
                });
            }
            return workingShiftDTOList;
        }
        public void AddNewWorkingShift(WorkingShiftDTO workingShiftDTO)
        {
            WorkingShift newWorkingShift = new WorkingShift
            {
                ShiftName = workingShiftDTO.ShiftName,
                ShiftStart = workingShiftDTO.ShiftStart,
                ShiftEnd = workingShiftDTO.ShiftEnd,
                GraceIn = workingShiftDTO.GraceIn,
                GraceOut = workingShiftDTO.GraceOut,
                DepartmentFK = workingShiftDTO.DepartmentID,
                AddedByUserFK = workingShiftDTO.AddedByUser,
                IsActive = true,
                IsDeleted = false,
                CreationDate = DateTime.Now
            };
            Add(newWorkingShift);
        }
        public WorkingShiftDTO GetWorkingShift(int ID)
        {
            WorkingShift workingShift = Find(x => x.ShiftID == ID).FirstOrDefault();

            WorkingShiftDTO workingShiftDTO = new WorkingShiftDTO {
                ShiftID = workingShift.ShiftID,
                ShiftName = workingShift.ShiftName,
                ShiftStart = workingShift.ShiftStart,
                ShiftEnd = workingShift.ShiftEnd,
                GraceIn = workingShift.GraceIn,
                GraceOut = workingShift.GraceOut,
                DepartmentID = workingShift.DepartmentFK.Value
            };

            return workingShiftDTO;
        }
        public void EditWorkingShift(WorkingShiftDTO workingShiftDTO)
        {
            WorkingShift workingShift = Find(x => x.ShiftID == workingShiftDTO.ShiftID).FirstOrDefault();
            workingShift.ShiftName = workingShiftDTO.ShiftName;
            workingShift.ShiftStart = workingShiftDTO.ShiftStart;
            workingShift.ShiftEnd = workingShiftDTO.ShiftEnd;
            workingShift.GraceIn = workingShiftDTO.GraceIn;
            workingShift.GraceOut = workingShiftDTO.GraceOut;
            workingShift.DepartmentFK = workingShiftDTO.DepartmentID;

            Edit(workingShift);
        }
        public void DisapleWorkingShift(int ID)
        {
            WorkingShift workingShift = Find(x => x.ShiftID == ID).FirstOrDefault();
            workingShift.IsActive = false;
            workingShift.IsDeleted = true;

            Edit(workingShift);
        }
    }
}
