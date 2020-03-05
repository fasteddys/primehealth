using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Tables
{
    public class ApprovalFlowBLL : Repository<ApprovalFlow> 
    {

        ApprovalFlowDetailsBLL approvalFlowDetailsBLL;
        UserBLL userBLL;
        ApprovalFlowRequestDetailsBLL approvalFlowRequestDetailsBLL;
        ApprovalFlowUserDetailsBLL approvalFlowDetailsUserBLL;
        ManagerBLL managerBLL;
        DateTime creationDate;
        SubDepartmentBLL subDepartmentBLL;
        //RequestBLL RequestBLL;
        public ApprovalFlowBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            approvalFlowDetailsBLL = new ApprovalFlowDetailsBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, CreationDate);
            approvalFlowRequestDetailsBLL = new ApprovalFlowRequestDetailsBLL(Context, CreationDate);
            approvalFlowDetailsUserBLL = new ApprovalFlowUserDetailsBLL(Context, CreationDate);
            creationDate = CreationDate;
            managerBLL = new ManagerBLL(Context, CreationDate);
            subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
           // RequestBLL = new RequestBLL(Context, CreationDate);

        }
        public void AddNewApprovalFlow(ApprovalFlow NewApprovalFlow, ApprovalFlowDTO ListManagerDto)
        {
            
            foreach (var item in ListManagerDto.Steps)
            {
                if (item.UserID == -1)
                {
                    approvalFlowDetailsBLL.Add(new ApprovalFlowDetail
                    {
                        Order = item.Order,
                       // ApprovalFlowFK = NewApprovalFlow.ApprovalFlowID,
                        IsActive = true,
                        IsDeleted = false,
                        IsDirectManager = true,
                        ApprovalFlow= NewApprovalFlow



                    });
                }
                else if (item.UserID == -2)
                {
                    approvalFlowDetailsBLL.Add(new ApprovalFlowDetail
                    {
                        Order = item.Order,
                        //ApprovalFlowFK = NewApprovalFlow.ApprovalFlowID
                        
                        IsActive = true,
                        IsDeleted = false
                        ,
                        IsTeamManager = true,
                        ApprovalFlow = NewApprovalFlow


                    });
                }
                else if (item.UserID != null)
                {

                    var addedApprovalflowmanager = new ApprovalFlowDetail
                    {
                        Order = item.Order,
                      //  ApprovalFlowFK = NewApprovalFlow.ApprovalFlowID,
                        IsActive = true,
                        IsDeleted = false,
                        ApprovalFlowPersonFK = item.UserID,
                        ApprovalFlow = NewApprovalFlow

                    };
                    approvalFlowDetailsBLL.Add(addedApprovalflowmanager);
                    approvalFlowDetailsUserBLL.Add(new ApprovalFlowUserDetail
                    {
                        UserFK = item.UserID,
                       // ApprovalFlowDetailsFK = addedApprovalflowmanager.ApprovalFlowDetailsID,
                        ApprovalFlowDetail= addedApprovalflowmanager
                    });
                }
               // 

            }
        }
        public void AddNewAlternativeFlow(ApprovalFlow ApprovalFlow, ApprovalFlowDTO ListManagerDto)
        {
            List<ApprovalFlowDetail> ListApprovalFlowManagers = new List<ApprovalFlowDetail>();
            List<ApprovalFlowUserDetail> ListApprovalFlowDetailsUser = new List<ApprovalFlowUserDetail>();
            foreach (var item in ListManagerDto.Steps)
            {
                if (item.UserID == -1)
                {
                    approvalFlowDetailsBLL.Add(new ApprovalFlowDetail
                    {
                        Order = item.Order,
                        ApprovalFlow = ApprovalFlow,
                        IsActive = true,
                        IsDeleted = false,
                        //ParentApprovalFlowFK = ParentApprovalflow.ApprovalFlowID,
                        IsDirectManager = true,
                        LeaveTypeFK = ListManagerDto.ApprovalRoleLeaveID

                    });
                }
                else if (item.UserID == -2)
                {
                    approvalFlowDetailsBLL.Add(new ApprovalFlowDetail
                    {
                        Order = item.Order,
                        ApprovalFlow = ApprovalFlow,
                        IsActive = true,
                        IsDeleted = false
                        ,
                        // ParentApprovalFlowFK   = ParentApprovalflow.ApprovalFlowID,
                        IsTeamManager = true,
                        LeaveTypeFK = ListManagerDto.ApprovalRoleLeaveID

                    });
                }
                else if (item.UserID != null)
                {



                    var addedApprovalflowmanager = new ApprovalFlowDetail
                    {
                        Order = item.Order,
                        ApprovalFlow = ApprovalFlow,
                        IsActive = true,
                        IsDeleted = false,
                        //ParentApprovalFlowFK = ParentApprovalflow.ApprovalFlowID,
                        ApprovalFlowPersonFK = item.UserID
                        ,
                        LeaveTypeFK = ListManagerDto.ApprovalRoleLeaveID

                    };
                    approvalFlowDetailsBLL.Add(addedApprovalflowmanager);
                    //  
                    approvalFlowDetailsUserBLL.Add(new ApprovalFlowUserDetail
                    {
                        UserFK = item.UserID,
                        ApprovalFlowDetailsFK = addedApprovalflowmanager.ApprovalFlowDetailsID,



                    });






                }
              //  


            }
            // 

        }
        public void EditApprovalFlow(ApprovalFlow ApprovalFlow, ApprovalFlowDTO ListManagerDto)
        {
            ApprovalFlow EditApprovalflow = Find(x => x.ApprovalFlowID == ApprovalFlow.ApprovalFlowID).FirstOrDefault();
            EditApprovalflow.ApprovalFlowName = ApprovalFlow.ApprovalFlowName;

            foreach (var item in ApprovalFlow.ApprovalFlowDetails)
            {
                item.IsActive = false;
            }
            foreach (var item in ListManagerDto.Steps)
            {
                if (item.UserID == -1)
                {
                    approvalFlowDetailsBLL.Add(new ApprovalFlowDetail
                    {
                        Order = item.Order,
                        ApprovalFlowFK = ApprovalFlow.ApprovalFlowID,
                        IsActive = true,
                        IsDeleted = false,
                        IsDirectManager = true,



                    });
                }
                else if (item.UserID == -2)
                {
                    approvalFlowDetailsBLL.Add(new ApprovalFlowDetail
                    {
                        Order = item.Order,
                        ApprovalFlowFK = ApprovalFlow.ApprovalFlowID
                        ,
                        IsActive = true,
                        IsDeleted = false
                        ,
                        IsTeamManager = true

                    });
                }
                else if (item.UserID != null)
                {

                    var addedApprovalflowmanager = new ApprovalFlowDetail
                    {
                        Order = item.Order,
                        ApprovalFlowFK = ApprovalFlow.ApprovalFlowID,
                        IsActive = true,
                        IsDeleted = false,
                        ApprovalFlowPersonFK = item.UserID
                    };
                    approvalFlowDetailsBLL.Add(addedApprovalflowmanager);
                    //  
                    approvalFlowDetailsUserBLL.Add(new ApprovalFlowUserDetail
                    {
                        UserFK = item.UserID,
                        ApprovalFlowDetailsFK = addedApprovalflowmanager.ApprovalFlowDetailsID,
                    });
                }
            }
        }
        public List<User> GetManagerApprovalFlow(Request Leave, User User)
        {
            ApprovalFlow ApprovalFlow = User.ApprovalFlow;
            List<ApprovalFlowDetail> ListApprovalFlowDetails = ApprovalFlow.ApprovalFlowDetails.ToList();
            List<ApprovalFlowRequestDetail> listApprovalFlowManager_Leave = new List<ApprovalFlowRequestDetail>();
            foreach (var item in ListApprovalFlowDetails)
            {
                listApprovalFlowManager_Leave.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == Leave.RequestID && x.ApprovalFlowDetailsFK == item.ApprovalFlowDetailsID).FirstOrDefault());
            }
            int? ApprovalFlowDetailsID = 0;
            foreach (var item in listApprovalFlowManager_Leave)
            {
                if (item.RequestActionFK == null)
                {

                    ApprovalFlowDetailsID = item.ApprovalFlowDetailsFK;

                }

            }
            List<Manager> ListManager = new List<Manager>();
            List<User> users = new List<User>();
            foreach (var item in ListManager)
            {
                users.Add(userBLL.Find(x => x.UserID == item.ManagerUserFK).FirstOrDefault());
            }


            List<ApprovalFlowUserDetail> ApprovalFlowDetailsUsers = new List<ApprovalFlowUserDetail>();
            foreach (var item in ListApprovalFlowDetails)
            {
                ApprovalFlowDetailsUsers.AddRange(approvalFlowDetailsUserBLL.Find(x => x.ApprovalFlowDetailsFK == item.ApprovalFlowDetailsID));


            }

            var userids = ApprovalFlowDetailsUsers.Select(x => x.UserFK).ToList();
            foreach (var item in userids)
            {
                users.Add(userBLL.GetAll().Where(x => x.UserID == item).FirstOrDefault());
            }
            return users;

        }

        public List<UserDTO> GetUserApprovedByManager(int UserIDOfManager)
        {
            //if (userBLL.Find(x => x.UserID == UserIDOfManager).FirstOrDefault()?.IsHR != null)
            //{
            //    List<UserDTO> ListOfUsers = new List<UserDTO>();
            //    foreach (var item in userBLL.GetAll().Where(x=>x.IsDeleted==false))
            //    {
            //        ListOfUsers.Add(new UserDTO
            //        {
            //            UserName = item.UserName,
            //            UserID = item.UserID

            //        });
            //    }
            //    return ListOfUsers;
            //}
            //else
            //{
                List<User> ListUsers = new List<User>();
                List<UserDTO> ListOfUsers = new List<UserDTO>();
                int? ManagerID = managerBLL.Find(x => x.ManagerUserFK == UserIDOfManager).FirstOrDefault()?.ManagerID;
                ListUsers.AddRange(userBLL.Find(x => x.DirectManagerFK == ManagerID && x.DirectManagerFK != null));
                List<int> SubDepartmentIDs = subDepartmentBLL.Find(x => x.TeamManagerFK == ManagerID && x.TeamManagerFK != null).Select(x => x.SubDepartmentID).ToList();
                ListUsers.AddRange(userBLL.Find(x => SubDepartmentIDs.Contains((int)x.SubDepartmentFK)));
                List<int> ListApprovalFlowIDs = approvalFlowDetailsBLL.Find(x => x.ApprovalFlowPersonFK == UserIDOfManager && x.LeaveTypeFK == null).Select(x => x.ApprovalFlowFK).ToList();
                ListUsers.AddRange(userBLL.Find(x => ListApprovalFlowIDs.Contains((int)x.ApprovalFlowFK)));
                foreach (var item in ListUsers.Where(x => x.IsDeleted == false&&x.UserID!= UserIDOfManager))
                {
                if (ListOfUsers.Where(x => x.UserID == item.UserID).Count() == 0)
                {
                    ListOfUsers.Add(new UserDTO
                    {
                        UserName = item.UserName,
                        UserID = item.UserID

                    });
                }
                }
                return ListOfUsers;
            //}
        }
        public bool CheckUserApprovedByManager(int UserIDOfManager,int UserID)
        {
            User user = userBLL.Find(x => x.UserID ==
              UserIDOfManager && x.IsDeleted == false).FirstOrDefault();
            if (user?.IsHR == true|| user?.IsAdmin == true)
            {

                return true;
            }
            else {
                List<int> ListUsers = new List<int>();
                int? ManagerID = managerBLL.Find(x => x.ManagerUserFK == UserIDOfManager).FirstOrDefault()?.ManagerID;
                ListUsers.AddRange(userBLL.Find(x => x.DirectManagerFK == ManagerID && x.DirectManagerFK != null && x.IsDeleted == false).Select(x => x.UserID));
                List<int> SubDepartmentIDs = subDepartmentBLL.Find(x => x.TeamManagerFK == ManagerID && x.TeamManagerFK != null).Select(x => x.SubDepartmentID).ToList();
                ListUsers.AddRange(userBLL.Find(x => SubDepartmentIDs.Contains((int)x.SubDepartmentFK) && x.IsDeleted == false).Select(x => x.UserID));
                List<int> ListApprovalFlowIDs = approvalFlowDetailsBLL.Find(x => x.ApprovalFlowPersonFK == UserIDOfManager && x.LeaveTypeFK == null).Select(x => x.ApprovalFlowFK).ToList();
                ListUsers.AddRange(userBLL.Find(x => ListApprovalFlowIDs.Contains((int)x.ApprovalFlowFK) && x.IsDeleted == false).Select(x => x.UserID));
                if (ListUsers.Where(x => x == UserID).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
     }


        public ApprovalFlowContainerDTO GetApprovalFlowDetails(int ApprovalFlowID)
        {

            ApprovalFlowContainerDTO ApprovalFlowContaner = new ApprovalFlowContainerDTO();
            ApprovalFlow ApprovalFlow = Find(x => x.ApprovalFlowID == ApprovalFlowID).FirstOrDefault();
            ApprovalFlowContaner.ApprovalFlowName = ApprovalFlow.ApprovalFlowName;
            List<ApprovalFlowDetail> ApprovalFlowDetail = approvalFlowDetailsBLL.Find(x => x.ApprovalFlowFK == ApprovalFlowID && x.IsActive == true).ToList();
            ApprovalFlowContaner.ApprovalFlow = new List<ApprovalFlowDTO>();
            for (int i = 0; i < ApprovalFlowDetail.Count(); i++)
            {
                if (ApprovalFlowDetail[i].LeaveTypeFK == null)
                {
                    List<ApprovalFlowStepDTO> Steps = new List<ApprovalFlowStepDTO>();
                    if (ApprovalFlowDetail[i].IsDirectManager)
                    {
                        Steps.Add(new ApprovalFlowStepDTO
                        {
                            Order = ApprovalFlowDetail[i].Order,
                            UserID = -1
                        });
                    }
                    else if (ApprovalFlowDetail[i].IsTeamManager)
                    {
                        Steps.Add(new ApprovalFlowStepDTO
                        {
                            Order = ApprovalFlowDetail[i].Order,
                            UserID = -2
                        });
                    }
                    else
                    {
                        Steps.Add(new ApprovalFlowStepDTO
                        {
                            Order = ApprovalFlowDetail[i].Order,
                            UserID = ApprovalFlowDetail[i].ApprovalFlowPersonFK
                        });
                    }
                    if (ApprovalFlowContaner.ApprovalFlow.Where(x => x.ApprovalRoleLeaveID == ApprovalFlowDetail[i].LeaveTypeFK).Count() == 0)
                    {
                        ApprovalFlowContaner.ApprovalFlow.Add(new ApprovalFlowDTO {
                             ApprovalRoleLeaveID= ApprovalFlowDetail[i].LeaveTypeFK,
                             Steps=new List<ApprovalFlowStepDTO>()
                              
                        }


                      );
                    }
                    ApprovalFlowContaner.ApprovalFlow.Where(x => x.ApprovalRoleLeaveID == ApprovalFlowDetail[i].LeaveTypeFK).FirstOrDefault().Steps.AddRange(Steps);


                }
                else
                {
                    List<ApprovalFlowStepDTO> Steps = new List<ApprovalFlowStepDTO>();
                    if (ApprovalFlowDetail[i].IsDirectManager)
                    {
                        Steps.Add(new ApprovalFlowStepDTO
                        {
                            Order = ApprovalFlowDetail[i].Order,
                            UserID = -1
                        });
                    }
                    else if (ApprovalFlowDetail[i].IsTeamManager)
                    {
                        Steps.Add(new ApprovalFlowStepDTO
                        {
                            Order = ApprovalFlowDetail[i].Order,
                            UserID = -2
                        });
                    }
                    else
                    {
                        Steps.Add(new ApprovalFlowStepDTO
                        {
                            Order = ApprovalFlowDetail[i].Order,
                            UserID = ApprovalFlowDetail[i].ApprovalFlowPersonFK
                        });
                    }
                    if (ApprovalFlowDetail[i - 1].Order == ApprovalFlowDetail[i].Order)
                    {
                        ApprovalFlowContaner.ApprovalFlow[i].Steps = Steps;

                    }
                    else
                    {
                        ApprovalFlowContaner.ApprovalFlow.Add(new ApprovalFlowDTO
                        {
                            ApprovalRoleID = i,
                            ApprovalRoleLeaveID = ApprovalFlowDetail[i].LeaveTypeFK,
                            Steps = Steps
                        });
                    }
                    if (ApprovalFlowContaner.ApprovalFlow.Where(x => x.ApprovalRoleLeaveID == ApprovalFlowDetail[i].LeaveTypeFK).Count() == 0)
                    {
                        ApprovalFlowContaner.ApprovalFlow.Add(new ApprovalFlowDTO
                        {
                            ApprovalRoleLeaveID = ApprovalFlowDetail[i].LeaveTypeFK,
                            Steps = new List<ApprovalFlowStepDTO>()

                        }


                      );
                    }
                    ApprovalFlowContaner.ApprovalFlow.Where(x => x.ApprovalRoleLeaveID == ApprovalFlowDetail[i].LeaveTypeFK).FirstOrDefault().Steps.AddRange(Steps);



                }
            }
            foreach (var item in ApprovalFlowContaner.ApprovalFlow.SelectMany(x => x.Steps))
            {
                if (item.UserID != -1 || item.UserID != -2)
                {
                    item.UserName = userBLL.Find(x => x.UserID == item.UserID)?.FirstOrDefault()?.UserName;
                }

            }
            return ApprovalFlowContaner;
        }

        public List<ApprovalFlowEmployeeCountDTO> GetApprovalFlowView()
        {
            List<ApprovalFlowEmployeeCountDTO> Approval = new List<ApprovalFlowEmployeeCountDTO>();

            var Approvalist = GetAll().ToList();

            foreach (var item in Approvalist)
            {
                ApprovalFlowEmployeeCountDTO Approvalitem = new ApprovalFlowEmployeeCountDTO();
                Approvalitem.ApprovalFlowName = item.ApprovalFlowName;
                Approvalitem.ApprovalFlowID = item.ApprovalFlowID;

                Approvalitem.UsersCount = userBLL.Find(x => x.ApprovalFlowFK == item.ApprovalFlowID&&x.IsDeleted==false).Count();
                Approval.Add(Approvalitem);
            }
            return Approval;
        }

        public void CheckIfApprovalFlowVaildForUser(int UserID, int ApprovalFlowID)
        {
            User User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            //ApprovalFlow ApprovalFlow = Find(x => x.ApprovalFlowID == ApprovalFlowID && x.IsActive == true).FirstOrDefault();
            List<ApprovalFlowDetail> ApprovalFlowDetails = approvalFlowDetailsBLL.Find(x => x.ApprovalFlowFK == ApprovalFlowID && x.IsActive == true).ToList();

            if (User.SubDepartmentFK == null)
            {
                foreach (var items in ApprovalFlowDetails)
                {
                    if (items.IsTeamManager == true)
                    {
                        throw new Exception("User Hasn't Team Manager");
                    }
                }
            }

            if (User.DirectManagerFK == null)
            {
                foreach (var items in ApprovalFlowDetails)
                {
                    if (items.IsDirectManager == true)
                    {
                        throw new Exception("User Hasn't Direct Manager");
                    }
                } 
            }
            

            //if (User.DirectManagerFK != null && User.SubDepartmentFK != null)
            //{
            //    if(ApprovalFlowDetail.IsDirectManager == false || ApprovalFlowDetail.IsTeamManager == false)
            //    {
            //        return false;
            //    }
        }
    }
}
