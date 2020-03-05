using HRMS.BLL.Logic.Tables;
using HRMS.BLL.StaticData;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Shared_Logic
{
    public class UserDetailsProfileBLL
    {
        UserBLL UserBLL;
        SubDepartmentBLL subDepartmentBLL;
        PositionBLL PositionBLL;
        DepartmentBLL departmentBLL;
        ApprovalFlowBLL approvalFlowBLL;
        ManagerBLL managerBLL;
        DateTime creationDate;
        public UserDetailsProfileBLL(HRMSEntities Context,DateTime CreationDate) 
        {
            UserBLL = new UserBLL(Context, CreationDate);
            subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
            PositionBLL = new PositionBLL(Context, CreationDate);
            departmentBLL = new DepartmentBLL(Context, CreationDate);
            approvalFlowBLL = new ApprovalFlowBLL(Context, CreationDate);
            managerBLL = new ManagerBLL(Context, CreationDate);
            creationDate = CreationDate;

        }


        public UserDTO UserDetails(int UserID)
        {
            User User = UserBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            SubDepartment SubDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK).FirstOrDefault();

            Manager DirectManager = managerBLL.Find(x => x.ManagerID == User.DirectManagerFK).FirstOrDefault();
            Manager TeamManager = managerBLL.Find(x => x.ManagerID == SubDepartment.TeamManagerFK).FirstOrDefault();



            return new UserDTO
            {
                UserID = User.UserID,
                ApprovalFlowName = approvalFlowBLL.Find(x=>x.ApprovalFlowID== User.ApprovalFlowFK).FirstOrDefault().ApprovalFlowName,
                AccessControlUserID = User.AccessControlUserFK,
                BirthDate = User.BirthDate,
                DepartmentName = departmentBLL.Find(x=>x.DepartmentID== User.DepartmentFK).FirstOrDefault().DepartmentName,
                HireDate = User.HireDate,
                SeniorityBeforeHireMonth = User.SeniorityBeforeHireMonth,
                SeniorityBeforeHireYears = User.SeniorityBeforeHireYears,
                SubDepartmentName = SubDepartment.SubDepartmentName,
                UserEmail = User.UserEmail,
                UserName = User.UserName,
                PositionName = PositionBLL.Find(x=>x.PositionID== User.PositionFK).FirstOrDefault().PositionName,
                TerminationDate = User.TerminationDate,
                ContractTypeName = ((StaticEnums.ContractTypes)Enum.Parse(typeof(StaticEnums.ContractTypes), User.ContractTypeFK.ToString())).ToString(),
                Gender =((StaticEnums.Gender)Enum.Parse(typeof(StaticEnums.Gender), User.GenderFK.ToString())).ToString(),
                ProbationDate = User.ProbationDate,
                PhoneNumber = User.PhoneNumber,
                CustomNote = User.CustomNote,
                DirectManagerName = DirectManager.ManagerName,
                TeamManagerName = TeamManager.ManagerName,
                

            }
            ;
        }







    }
}
