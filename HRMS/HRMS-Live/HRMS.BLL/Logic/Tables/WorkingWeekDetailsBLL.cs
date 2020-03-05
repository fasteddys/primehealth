using HRMS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Entities;
using HRMS.DTOs.Business;
using static HRMS.BLL.StaticData.StaticEnums;
using HRMS.BLL.StaticData;

namespace HRMS.BLL.Logic.Tables
{
    public class WorkingWeekDetailsBLL : Repository<WorkingWeekDetail>
    {
        UserBLL userBLL;
        WorkingWeekBLL workingWeekBLL;
        DateTime creationDate;

        public WorkingWeekDetailsBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            userBLL = new UserBLL(Context, CreationDate);
            workingWeekBLL = new WorkingWeekBLL(Context, CreationDate);
            creationDate = CreationDate;

        }
        public void AddNewWorkingWeekDetails(List<WorkingWeekDetailsDTO> WorkingWeekDetailsDTO,WorkingWeek WorkingWeek)
        {

            foreach(var item in WorkingWeekDetailsDTO)
            {
                Add(new WorkingWeekDetail
                {
                    CreationDate = creationDate,
                    IsActive = item.IsActive,
                    IsDeleted = false,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    WeekDaysFK = item.DayID,
                    WorkingWeekFK = WorkingWeek.WorkingWeekID,
                    WorkingDuration= item.WorkingDuration,
                    GraceIn = item.GraceIn,
                    GraceOut = item.GraceOut
                });


            }
        }

        public void EditWorkingWeekDetails(WorkingWeekDTO WorkingWeek)
        {
            foreach (var items in WorkingWeek.ListWorkingWeekDetails)
            {
                WorkingWeekDetail workingWeekDetail = Find(x => x.WorkingWeekFK == WorkingWeek.WorkingWeekID && x.WeekDaysFK == items.DayID).FirstOrDefault();
                workingWeekDetail.StartTime = items.StartTime;
                workingWeekDetail.EndTime = items.EndTime;
                workingWeekDetail.WorkingDuration = items.WorkingDuration;
                workingWeekDetail.GraceIn = items.GraceIn;
                workingWeekDetail.GraceOut = items.GraceOut;
                workingWeekDetail.IsActive = items.IsActive;

                Edit(workingWeekDetail);
            }
        }

        public void LinkUserToWorkingWeekDetails(UserWorkingWeekDTO UserWorkingWeekDTO)
        {
         User User =  userBLL.Find(x=>x.WorkingWeekFK== UserWorkingWeekDTO.UserID).FirstOrDefault();

            User.WorkingWeekFK = UserWorkingWeekDTO.WorkingWeekID;

        }
        //public void EditWorkingWeekDetails(WorkingWeekDTO WorkingWeekDTO)
        //{
        //    foreach (var item in WorkingWeekDTO.ListWorkingWeekDetails)
        //    {
        //        var WorkingWeekDetail = Find(x => x.WorkingWeekFK == WorkingWeekDTO.WorkingWeekID&&x.WeekDaysFK== item.DayID).FirstOrDefault();
        //        WorkingWeekDetail.IsActive = item.IsActive;
        //        WorkingWeekDetail.IsDeleted = false;
        //        WorkingWeekDetail.StartTime = item.StartTime;
        //        WorkingWeekDetail.EndTime = item.EndTime;
        //        WorkingWeekDetail.WeekDaysFK = item.DayID;
        //        WorkingWeekDetail.WorkingWeekFK = WorkingWeekDTO.WorkingWeekID;               
        //    }


        //}

        public WorkingWeekDetailsContanerDTO SelectWorkingWeekDetails(int WorkingWeekID)
        {
            WorkingWeekDetailsContanerDTO WorkingWeekDetailsContanerDTO = new WorkingWeekDetailsContanerDTO();

            WorkingWeek WorkingWeek = workingWeekBLL.Find(x => x.WorkingWeekID == WorkingWeekID).FirstOrDefault() ;
            WorkingWeekDetailsContanerDTO.WorkingWeekName = WorkingWeek.WorkingWeekName;
            List<WorkingWeekDetailsDTO> ListWorkingWeekDetailsDTO = new List<DTOs.Business.WorkingWeekDetailsDTO>();

            List<WorkingWeekDetail> WorkingWeekDetail = Find(x => x.WorkingWeekFK == WorkingWeekID).ToList();

            foreach (var item in WorkingWeekDetail)
            {
                ListWorkingWeekDetailsDTO.Add(new WorkingWeekDetailsDTO
                {
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    DayName= ((StaticEnums.Days)Enum.Parse(typeof(StaticEnums.Days), item.WeekDaysFK.ToString())).ToString()  ,                   
                    WorkingDuration= item.WorkingDuration ,
                     IsActive= item.IsActive
                });
            }
            WorkingWeekDetailsContanerDTO.WorkingWeekDetailsDTO = ListWorkingWeekDetailsDTO;

            return WorkingWeekDetailsContanerDTO;
        }

       

        public List<UserDTO> GetAllUserByWorkingWeek(int WorkingWeekID)
        {
            List<UserDTO> UserList = new List<UserDTO>();
            foreach (var item in userBLL.Find(x => x.WorkingWeekFK == WorkingWeekID).ToList())
            {
                UserDTO user = new UserDTO();
                user = new UserDTO
                {
                    UserID = item.UserID,
                    UserName=item.UserName
                };
                UserList.Add(user);

            }
            return UserList;
        }

    }
}
