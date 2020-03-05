using HRMS.BLL.Logic.Tables;
using HRMS.BLL.StaticData;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Shared_Logic
{
    public class SharedUserProfileBLL
    {
        ApprovalFlowBLL approvalBLL;
        UserBLL userBLL;
        SubDepartmentBLL subDepartmentBLL;
        DepartmentBLL departmentBLL;
        TimeAttendanceBLL timeAttendanceBLL;
        UserEntitlementChangesBLL userEntitlementChangesBLL;
        UserEntitlementBLL userEntitlementBLL;
        LeaveTypeBLL leaveTypeBLL;
        PositionBLL PositionBLL;
        GenderTypeBLL GenderTypeBLL;
        ContractTypeBLL ContractTypeBLL;
        CompanyBLL companyBLL;
        ManagerBLL managerBLL;
        PositionBLL positionBLL;
        LeaveTypeEntitlementTypeDIMBLL leaveTypeEntitlementTypeDIMBLL;
        LeaveTypeGainingEligibilityMethodDIMBLL leaveTypeGainingEligibilityMethodDIMBLL;
        LeaveTypeAccuralRuleBLL leaveTypeAccuralRuleBLL;
        LeaveTypeRestrictedGenderBLL leaveTypeRestrictedGenderBLL;
        LeaveTypeRestrictedEmployeeBLL leaveTypeRestrictedEmployeeBLL;
        LeaveTypeRestrictedDepBLL leaveTypeRestrictedDepBLL;
        LeaveTypeRestrictedSubDepBLL leaveTypeRestrictedSubDepBLL;
        LeaveTypeRestrictedContractTypeBLL leaveTypeRestrictedContractTypeBLL;
        MonthlyEffectiveAccuredLeaveTypesBLL monthlyEffectiveAccuredLeaveTypesBLL;
        WorkingWeekBLL workingWeekBLL;
        WorkingModeBLL workingModeBLL;

        DateTime creationDate;

        public SharedUserProfileBLL(HRMSEntities Context, DateTime CreationDate)
        {
            userBLL = new UserBLL(Context, CreationDate);
            subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
            departmentBLL = new DepartmentBLL(Context, CreationDate);
            timeAttendanceBLL = new TimeAttendanceBLL(Context, CreationDate);
            userEntitlementChangesBLL = new UserEntitlementChangesBLL(Context, CreationDate);
            userEntitlementBLL = new UserEntitlementBLL(Context, CreationDate);
            leaveTypeBLL = new LeaveTypeBLL(Context, CreationDate);
            managerBLL = new ManagerBLL(Context, CreationDate);
            PositionBLL = new PositionBLL(Context, CreationDate);
            approvalBLL = new ApprovalFlowBLL(Context, CreationDate);
            GenderTypeBLL = new GenderTypeBLL(Context, CreationDate);
            ContractTypeBLL = new ContractTypeBLL(Context, CreationDate);
            companyBLL = new CompanyBLL(Context, CreationDate);
            leaveTypeEntitlementTypeDIMBLL = new LeaveTypeEntitlementTypeDIMBLL(Context, CreationDate);
            leaveTypeGainingEligibilityMethodDIMBLL = new LeaveTypeGainingEligibilityMethodDIMBLL(Context, CreationDate);
            leaveTypeAccuralRuleBLL = new LeaveTypeAccuralRuleBLL(Context, CreationDate);
            leaveTypeRestrictedGenderBLL = new LeaveTypeRestrictedGenderBLL(Context, CreationDate);
            leaveTypeRestrictedEmployeeBLL = new LeaveTypeRestrictedEmployeeBLL(Context, CreationDate);
            leaveTypeRestrictedDepBLL = new LeaveTypeRestrictedDepBLL(Context, CreationDate);
            leaveTypeRestrictedSubDepBLL = new LeaveTypeRestrictedSubDepBLL(Context, CreationDate);
            leaveTypeRestrictedContractTypeBLL = new LeaveTypeRestrictedContractTypeBLL(Context, CreationDate);
            monthlyEffectiveAccuredLeaveTypesBLL = new MonthlyEffectiveAccuredLeaveTypesBLL(Context, CreationDate);
            positionBLL = new PositionBLL(Context, CreationDate);
            workingWeekBLL = new WorkingWeekBLL(Context, CreationDate);
            workingModeBLL = new WorkingModeBLL(Context, CreationDate);
            creationDate = CreationDate;
        }

        private string GetValidationMessage(int LeaveTypeID, int UserID)
        {
            LeaveType CurrentLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveTypeID && x.IsActive == true).FirstOrDefault();
            LeaveType ParentLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == CurrentLeaveType.ParentLeaveTypeFK && x.IsActive == true).FirstOrDefault();
            if(ParentLeaveType != null)
            {
                UserEntitlement EntitlementForCurrentLeave = userEntitlementBLL.Find(x => x.LeaveTypeFK == CurrentLeaveType.LeaveTypeID && x.UserFK == UserID && x.IsActive == true).FirstOrDefault();
                UserEntitlement EntitlementForParentLeave = userEntitlementBLL.Find(x => x.LeaveTypeFK == ParentLeaveType.LeaveTypeID && x.UserFK == UserID && x.IsActive == true).FirstOrDefault();

                if (EntitlementForParentLeave != null)
                {
                    if(EntitlementForParentLeave.EntitlementTotal == 0 && EntitlementForCurrentLeave.EntitlementTotal > 0)
                        return "(Not Usable Due To Not Enough Limit On "+ ParentLeaveType.LeaveTypeName + " Leave)";
                }
            }
            return "";
        }
        public UserprofileDTO GetAllUserProfileData(int LoggedUserID, int UserID)
        {
            string Gender = "";
            string Contract = "";
            UserprofileDTO userProfile = new UserprofileDTO();
            int? DirectManagerID = 0, TeamManagerID = 0, PositionID = 0, DepartmentID = 0, SubDepartmentID = 0, ApprovalFlowID = 0, CompanyID, WorkingModeID, WorkingWeekID;

            if(LoggedUserID == UserID || approvalBLL.CheckUserApprovedByManager(LoggedUserID, UserID))
            {
                User User = userBLL.Find(x => x.UserID == UserID)?.FirstOrDefault();
                Manager manager = new Manager();
                SubDepartment subdepartment = new SubDepartment();
                DirectManagerID = User.DirectManagerFK;
                PositionID = User.PositionFK;
                CompanyID = User.CompanyFK;

                ApprovalFlow approval = new ApprovalFlow();
                approval = approvalBLL.Find(x => x.ApprovalFlowID == User.ApprovalFlowFK)?.FirstOrDefault();
                ApprovalFlowID = approval?.ApprovalFlowID;
                Department department = new Department();
                subdepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault();
                department = departmentBLL.Find(x => x.DepartmentID == User.DepartmentFK)?.FirstOrDefault();
                TeamManagerID = subdepartment?.TeamManagerFK;
                DepartmentID = department?.DepartmentID;
                SubDepartmentID = subdepartment?.SubDepartmentID;

                WorkingModeID = User?.WorkingModeFK;
                WorkingWeekID = User?.WorkingWeekFK;

                try
                {

                    Gender = ((StaticEnums.Gender)Enum.Parse(typeof(StaticEnums.Gender), User.GenderFK?.ToString())).ToString();
                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                }

                try
                {

                    Contract = ((StaticEnums.ContractTypes)Enum.Parse(typeof(StaticEnums.ContractTypes), User.ContractTypeFK?.ToString())).ToString();
                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                }

                userProfile = new UserprofileDTO()
                {
                    ApprovalFlowName = approval?.ApprovalFlowName,
                    UserID = User.UserID,
                    DepartmentName = department?.DepartmentName,
                    SubDepartmentName = subdepartment?.SubDepartmentName,
                    HireDate = User?.HireDate != null ? User?.HireDate.Value.Date.ToString("dd/MM/yyyy") : null,
                    UserName = User?.UserName,
                    LDAPUserName = User?.LDAPUserName,
                    ProfilePictureURL = User?.ProfilePictureURL,
                    SeniorityBeforeHireYear = User?.SeniorityBeforeHireYears,
                    SeniorityBeforeHireMonth = User?.SeniorityBeforeHireMonth,
                    TerminationDate = User?.TerminationDate != null ? User?.TerminationDate.Value.Date.ToString("dd/MM/yyyy") : null,
                    ProbationDate = User?.ProbationDate != null ? User?.ProbationDate.Value.Date.ToString("dd/MM/yyyy") : null,
                    UserEmail = User?.UserEmail,
                    BirthDate = User?.BirthDate != null ? User?.BirthDate.Value.Date.ToString("dd/MM/yyyy") : null,
                    UserArabicName = User?.ArName,
                    NationalID = User?.NationalID,
                    MedicalID = User?.MedicalID,
                    CurrentAddress = User?.Address,
                    HomeAddress = User?.HomeAddress,
                    WorkingModeID = WorkingModeID,
                    WorkingModeName = WorkingModeID != null ? workingModeBLL.Find(x => x.WorkingModeID == WorkingModeID)?.FirstOrDefault().WorkingModeName : null,
                    WorkingWeekID = WorkingWeekID,
                    WorkingWeekName = WorkingWeekID != null ? workingWeekBLL.Find(x => x.WorkingWeekID == WorkingWeekID)?.FirstOrDefault().WorkingWeekName : null,
                    IsActive = User.IsActive,
                    IsDeleted = User.IsDeleted,

                    EntitlementChanges = new List<UserEntitlementChangesDTO>(),
                    ListDailyAttendance = new List<DailyAttendanceDTO>(),
                    LeaveTypesEntitlements = new List<LeaveTypeEntitlementDTO>(),
                    BusinessPhone = User.PhoneNumber,
                    EmployeeNumber = User.AccessControlUserFK,
                    CustomeNote = User.CustomNote,
                    Gender = Gender,
                    GenderID = User.GenderFK,

                    ContractTypeName = Contract,
                    ContractTypeID = User.ContractTypeFK,

                    ApprovalFlowID = ApprovalFlowID,
                    DepartmentID = DepartmentID,
                    SubDepartmentID = SubDepartmentID,
                    DirectManagerID = managerBLL.Find(x => x.ManagerID == DirectManagerID)?.FirstOrDefault()?.ManagerUserFK,
                    DirectManagerName = managerBLL.Find(x => x.ManagerID == DirectManagerID)?.FirstOrDefault()?.ManagerName,

                    TeamManagerID = managerBLL.Find(x => x.ManagerID == TeamManagerID)?.FirstOrDefault()?.ManagerID,
                    TeamManagerName = managerBLL.Find(x => x.ManagerID == TeamManagerID)?.FirstOrDefault()?.ManagerName,

                    positionID = PositionBLL.Find(x => x.PositionID == PositionID)?.FirstOrDefault()?.PositionID,
                    positionName = PositionBLL.Find(x => x.PositionID == PositionID)?.FirstOrDefault()?.PositionName,

                    CompanyID = CompanyID,
                    CompanyName = companyBLL.Find(x => x.CompanyID == CompanyID)?.FirstOrDefault()?.CompanyName
                };
              //  userProfile.EntitlementChanges = new List<UserEntitlementChangesDTO>();

                foreach (var item in userEntitlementChangesBLL.GetUserEntitlementChangesByID(UserID))
                {
                    try
                    {
                        userProfile.EntitlementChanges.Add(new UserEntitlementChangesDTO
                        {
                            UserEntitlementChangeID = item.UserEntitlementChangesID,
                            EntitlementChangeComment = item.Comment,
                            LimitAfterEntitlementChange = item.EntitlementAfter,
                            LimitBeforEntitlementChange = item.EntitlementBefore,
                            EntitlementChangeMaker = item.EntitlementChangedByDIM.ToString(),
                            EntitlementChangeDate = item.ActionDate
                        });
                    }
                    catch (Exception ex ) {


                    }
                }

                var ListEntitlement = userEntitlementBLL.GetUserEntitlementByID(User.UserID);
                foreach (var item in ListEntitlement)
                {
                    userProfile.LeaveTypesEntitlements.Add(new LeaveTypeEntitlementDTO
                    {
                        LeaveTypeID = item?.LeaveTypeFK,
                        LeaveTypeName = leaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK)?.FirstOrDefault()?.LeaveTypeName,
                        UserEntitlementAmount = userEntitlementBLL.Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == item.LeaveTypeFK && x.IsActive == true)?.FirstOrDefault().EntitlementTotal,
                        DurationUnit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString(),
                        Message = GetValidationMessage(item.LeaveTypeFK, User.UserID)
                    });
                }

                return userProfile;
            }

            return userProfile;
        }

        public void DeactivateUser(UserprofileDTO UserData)
        {
            User User = userBLL.Find(x => x.UserID == UserData.UserID).FirstOrDefault();
            var oldUser =  XMLObjectConverter.Serialize(User);
            User.IsActive = false;
            User.ModificationDate = DateTime.Now;
            User.TerminationDate = UserData.TerminationDate != null ? DateTime.ParseExact(UserData.TerminationDate, @"dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse(null); 
            userBLL.Edit(User);

            Manager Manager = managerBLL.Find(x => x.ManagerUserFK == UserData.UserID)?.FirstOrDefault(); 
            if (Manager != null)
            {
                var oldManager = XMLObjectConverter.Serialize(Manager);
                Manager.IsActive = false;
                Manager.ModificationDate = DateTime.Now;
                managerBLL.Edit(Manager);
                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserData.ModifiedByUserId, oldManager, XMLObjectConverter.Serialize(Manager), "Edit Manager Data");
            }
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserData.ModifiedByUserId, oldUser, XMLObjectConverter.Serialize(User), "Edit User Data");
        }


        public void DeleteUser(UserprofileDTO UserData)
        {
            User User = userBLL.Find(x => x.UserID == UserData.UserID).FirstOrDefault();
           var oldUser =  XMLObjectConverter.Serialize(User);
            User.IsActive = false;
            User.IsDeleted = true;
            User.DeletionDate = DateTime.Now;
            User.TerminationDate = UserData.TerminationDate != "" ? DateTime.ParseExact(UserData.TerminationDate, @"dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            userBLL.Edit(User);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserData.ModifiedByUserId, oldUser, XMLObjectConverter.Serialize(User), "Edit User Data");

        }

        public void AddNewEntitlementChange(int EntitlementChangeCount)
        {
            List<UserEntitlementChange> UserEntitlementChange = new List<UserEntitlementChange>();
            List<UserEntitlement> UserEntitlement = userEntitlementBLL.GetAll().OrderByDescending(x => x.UserEntitlementID).Take(EntitlementChangeCount).ToList();

            foreach(var items in UserEntitlement)
            {
                UserEntitlementChange.Add(new UserEntitlementChange()
                {
                    UserEntitlementFK = items.UserEntitlementID,
                    Comment = "Entitlement For New User",
                    CreationDate = creationDate,
                    LeaveDurationUnitFK = items.LeaveDurationUnitFK,
                    IsActive = true,
                    EntitlementBefore = 0,
                    EntitlementAfter = items.EntitlementTotal,
                    EntitlementChangedByFK = (int)StaticEnums.EntitlementChangedBy.System,
                    ActionDate = DateTime.Now,
                    IsDeleted = false,
                    UserFK = items.UserFK,
                    Year = creationDate.Year
                });
            }
            userEntitlementChangesBLL.AddRange(UserEntitlementChange);
        }

        public void ReactivateUser(UserprofileDTO UserData)
        {
            User User = userBLL.Find(x => x.UserID == UserData.UserID).FirstOrDefault();
            var oldUser = XMLObjectConverter.Serialize(User);
            UserData.IsActive = true;
            EditUserData(UserData);

            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserData.ModifiedByUserId, oldUser, XMLObjectConverter.Serialize(User), "Edit User Data");

            List<int> AccuralLeaveTypes = leaveTypeBLL.Find(x => x.ParentLeaveTypeFK == null && x.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Accured && x.IsSuspended == false && x.IsActive == true).Select(x => x.LeaveTypeID).ToList();
            List<LeaveTypeAccuralRule> AccuralRuleLeaveTypes = leaveTypeAccuralRuleBLL.Find(x => AccuralLeaveTypes.Contains(x.LeaveTypeFK.Value) && x.GainingEligibilityMethodFK == (int)LeaveTypeGainingEligibilityMethod.FromTheFirstDayAtWork && x.IsActive == true).ToList();

            foreach (var items in AccuralRuleLeaveTypes)
            {
                var RestrictedGender = leaveTypeRestrictedGenderBLL.Find(x => x.LeaveTypeFK == items.LeaveTypeFK && x.GenderFK == UserData.GenderID && x.IsActive == true)?.FirstOrDefault();
                var RestrictedEmployee = leaveTypeRestrictedEmployeeBLL.Find(x => x.LeaveTypeFK == items.LeaveTypeFK && x.EmployeeFK == UserData.UserID && x.IsActive == true)?.FirstOrDefault();
                var RestrictedDep = leaveTypeRestrictedDepBLL.Find(x => x.LeaveTypeFK == items.LeaveTypeFK && x.DepartmentFK == UserData.DepartmentID && x.IsActive == true)?.FirstOrDefault();
                var RestrictedSubDep = leaveTypeRestrictedSubDepBLL.Find(x => x.LeaveTypeFK == items.LeaveTypeFK && x.SubDepartmentFK == UserData.SubDepartmentID && x.IsActive == true)?.FirstOrDefault();
                var RestrictedContractType = leaveTypeRestrictedContractTypeBLL.Find(x => x.LeaveTypeFK == items.LeaveTypeFK && x.ContractTypeFK == UserData.ContractTypeID && x.IsActive == true)?.FirstOrDefault();

                if (RestrictedGender != null || RestrictedEmployee != null || RestrictedDep != null || RestrictedSubDep != null || RestrictedContractType != null)
                {
                    continue;
                }
                else
                {
                    bool checkMonthlyLeaveType = leaveTypeBLL.IsMonthlyLeaveType(items.LeaveTypeFK.Value);
                    UserEntitlement NewUserEntitlement = new UserEntitlement()
                    {
                        LeaveTypeFK = items.LeaveTypeFK.Value,
                        EntitlementTotal = items.EmployeeEarnsNumberOfUnits.Value,//leaveTypeBLL.Find(x => x.LeaveTypeID == items.LeaveTypeFK.Value).FirstOrDefault().TakeMaxAmountFromParent.Value,
                        LeaveDurationUnitFK = leaveTypeBLL.Find(x => x.LeaveTypeID == items.LeaveTypeFK.Value).FirstOrDefault().DurationUnitFK.Value,
                        IsActive = true,
                        IsDeleted = false,
                        Year = creationDate.Year,
                        CreationDate = creationDate,
                        UserFK = UserData.UserID
                    };
                    if (checkMonthlyLeaveType)
                    {
                        MonthlyEffectiveAccuredLeaveType EffectiveLeaveType = monthlyEffectiveAccuredLeaveTypesBLL.Find(x => x.IsActive == true).FirstOrDefault();
                        NewUserEntitlement.Month = EffectiveLeaveType.Month;
                        NewUserEntitlement.EffectiveDateFrom = EffectiveLeaveType.EffectiveDateFrom;
                        NewUserEntitlement.EffectiveDateTo = EffectiveLeaveType.EffectiveDateTo;
                    }

                    userEntitlementBLL.Add(NewUserEntitlement);

                    UserEntitlementChange NewUserEntitlementChange = new UserEntitlementChange()
                    {
                        UserEntitlement = NewUserEntitlement,
                        Comment = "Entitlement For New User",
                        CreationDate = creationDate,
                        LeaveDurationUnitFK = NewUserEntitlement.LeaveDurationUnitFK,
                        IsActive = true,
                        EntitlementBefore = 0,
                        EntitlementAfter = NewUserEntitlement.EntitlementTotal,
                        EntitlementChangedByFK = (int)StaticEnums.EntitlementChangedBy.System,
                        ActionDate = DateTime.Now,
                        IsDeleted = false,
                        UserFK = UserData.UserID,
                        Year = creationDate.Year
                    };
                    userEntitlementChangesBLL.Add(NewUserEntitlementChange);
                }
            }
        }
        
        public bool AddEntitlementForNewUser(int ID)
        {
            int AccuralEntitlementID = leaveTypeEntitlementTypeDIMBLL.Find(x => x.LeaveTypeEntitlementTypeID == (int)LeaveTypeEntitlementType.Accured).FirstOrDefault().LeaveTypeEntitlementTypeID;
            List<LeaveType> AccuralLeaveType = leaveTypeBLL.Find(x => x.IsActive == true && x.IsSuspended == false && x.ParentLeaveTypeFK == null && x.EntitlementTypeFK == AccuralEntitlementID).ToList();

            int LeaveTypeGrainEligibilityMethodID = leaveTypeGainingEligibilityMethodDIMBLL.Find(x => x.GainingEligibilityMethodID == (int)LeaveTypeGainingEligibilityMethod.FromTheFirstDayAtWork).FirstOrDefault().GainingEligibilityMethodID;
            List<LeaveTypeAccuralRule> LeaveTypeAccuralRules = leaveTypeAccuralRuleBLL.Find(x => x.IsActive == true && x.LeaveTypeAccuralRuleID == LeaveTypeGrainEligibilityMethodID).ToList();



            return true;
        }

        public List<UserCardDTO> GetUserDepartmentUsers(int UserID)
        {
            try
            {
                List<UserCardDTO> DepartmentUserCards = new List<UserCardDTO>();
                var User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
                var DepartmentUsers = userBLL.Find(y => y.DepartmentFK == User.DepartmentFK && y.IsActive == true).ToList();

                foreach (var dItems in DepartmentUsers)
                {
                    //UserCardDTO Temp = new UserCardDTO();
                    //Temp.UserID = item.UserID;
                    //Temp.UserName = item.UserName;
                    //Temp.ProfilePictureURL = item.ProfilePictureURL;
                    //Temp.Position = PositionBLL.Find(z => z.PositionID == item.PositionFK).SingleOrDefault()?.PositionName;
                    //Temp.SubDepartment = subDepartmentBLL.Find(z => z.SubDepartmentID == item.SubDepartmentFK).SingleOrDefault()?.SubDepartmentName;

                    UserCardDTO Temp = new UserCardDTO();
                    Temp.UserID = dItems.UserID;
                    Temp.UserName = dItems.UserName;
                    Temp.ProfilePictureURL = dItems.ProfilePictureURL;
                    Temp.Position = PositionBLL.Find(z => z.PositionID == dItems.PositionFK).SingleOrDefault()?.PositionName;
                    var SubDepartment = subDepartmentBLL.Find(z => z.SubDepartmentID == dItems.SubDepartmentFK)?.SingleOrDefault();
                    Temp.SubDepartment = SubDepartment?.SubDepartmentName;

                    int? DirectManagerUserID = managerBLL.Find(x => x.ManagerID == dItems.DirectManagerFK)?.FirstOrDefault()?.ManagerUserFK;
                    Temp.DirectlyReportsTo = userBLL.Find(x => x.UserID == DirectManagerUserID)?.FirstOrDefault()?.UserName;

                    int? TeamManagerID = SubDepartment?.TeamManagerFK;
                    int? ManagerUserID = managerBLL.Find(x => x.ManagerID == TeamManagerID)?.FirstOrDefault()?.ManagerUserFK;
                    Temp.TeamLeader = userBLL.Find(x => x.UserID == ManagerUserID)?.FirstOrDefault()?.UserName;

                    Temp.CompanyName = companyBLL.Find(X => X.CompanyID == dItems.CompanyFK)?.FirstOrDefault()?.CompanyName;

                    if (dItems.UserName == Temp.TeamLeader || Temp.DirectlyReportsTo == Temp.TeamLeader)
                        Temp.TeamLeader = null;

                    DepartmentUserCards.Add(Temp);
                }

                return DepartmentUserCards;
            }

            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");

                return null;
                throw;
            }

        }

        public List<UserOrganizationCardDTO> GetOrganizationUsers()
        {
            try
            {
                List<UserCardDTO> UsersCards;
                List<UserOrganizationCardDTO> OrganizationUsersCards = new List<UserOrganizationCardDTO>();
                var Departments = departmentBLL.GetAll().Where(x => x.IsActive == true).ToList();
                foreach(var items in Departments)
                {
                    UsersCards = new List<UserCardDTO>();
                    UserOrganizationCardDTO temp = new UserOrganizationCardDTO();
                    temp.UserDepartmentName = items.DepartmentName;
                    temp.UsersCards = new List<UserCardDTO>();
                    var DepartmentUsers = userBLL.Find(y => y.DepartmentFK == items.DepartmentID && y.IsActive == true).ToList();

                    foreach (var dItems in DepartmentUsers)
                    { 
                        UserCardDTO Temp = new UserCardDTO();
                        Temp.UserID = dItems.UserID;
                        Temp.UserName = dItems.UserName;
                        Temp.UserEmail = dItems.UserEmail;
                        Temp.ExtNumber = dItems.ExtNumber;
                        Temp.ProfilePictureURL = dItems.ProfilePictureURL;
                        Temp.Position = PositionBLL.Find(z => z.PositionID == dItems.PositionFK).SingleOrDefault()?.PositionName;
                        var SubDepartment = subDepartmentBLL.Find(z => z.SubDepartmentID == dItems.SubDepartmentFK)?.SingleOrDefault();
                        Temp.SubDepartment = SubDepartment?.SubDepartmentName;

                        int? DirectManagerUserID = managerBLL.Find(x => x.ManagerID == dItems.DirectManagerFK)?.FirstOrDefault()?.ManagerUserFK;
                        Temp.DirectlyReportsTo = userBLL.Find(x => x.UserID == DirectManagerUserID)?.FirstOrDefault()?.UserName;

                        int? TeamManagerID = SubDepartment?.TeamManagerFK;
                        int? ManagerUserID = managerBLL.Find(x => x.ManagerID == TeamManagerID)?.FirstOrDefault()?.ManagerUserFK;
                        Temp.TeamLeader = userBLL.Find(x => x.UserID == ManagerUserID)?.FirstOrDefault()?.UserName;
                   
                        Temp.CompanyName = companyBLL.Find(X => X.CompanyID == dItems.CompanyFK)?.FirstOrDefault()?.CompanyName;

                        if (dItems.UserName == Temp.TeamLeader || Temp.DirectlyReportsTo == Temp.TeamLeader)
                            Temp.TeamLeader = null;

                        UsersCards.Add(Temp);
                    }
                    temp.UsersCards = UsersCards;
                    OrganizationUsersCards.Add(temp);
                }
                return OrganizationUsersCards;
                //User User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
                //var CompanyUsers = userBLL.Find(y => y.CompanyFK == User.CompanyFK && y.UserID != User.UserID && y.IsActive == true).ToList();
                ////var CompanyUsersByDepartments = CompanyUsers.GroupBy(p => p.DepartmentFK).Select(g => new { DepartmentID = g.Key, UserData = g.ToList() });
                //foreach (var items in CompanyUsers)
                //{
                //    UserCardDTO Temp = new UserCardDTO();
                //    Temp.UserID = items.UserID;
                //    Temp.UserName = items.UserName;
                //    Temp.ProfilePictureURL = items.ProfilePictureURL;
                //    Temp.Position = PositionBLL.Find(z => z.PositionID == items.PositionFK).SingleOrDefault()?.PositionName;
                //    Temp.SubDepartment = subDepartmentBLL.Find(z => z.SubDepartmentID == items.SubDepartmentFK).SingleOrDefault()?.SubDepartmentName;

                //    CompanyUserCards.Add(Temp);
                //}
                //return CompanyUserCards;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");

                return null;
                throw;
            }
            
        }

        public void EditUserData(UserprofileDTO UserprofileDTO)
        {
            User User = userBLL.Find(x => x.UserID == UserprofileDTO.UserID).FirstOrDefault();
            // XMLObjectConverter.Serialize(User);
            //string UserBeforeEdit= User.Serialize();
            var oldUser = XMLObjectConverter.Serialize(User);
            User.UserName = UserprofileDTO.UserName;
            User.ArName = UserprofileDTO.UserArabicName;
            User.SubDepartmentFK = UserprofileDTO.SubDepartmentID;
            User.ApprovalFlowFK = UserprofileDTO.ApprovalFlowID;
            User.ContractTypeFK = UserprofileDTO.ContractTypeID;
            User.DepartmentFK = UserprofileDTO.DepartmentID;
            User.DirectManagerFK = managerBLL.Find(x => x.ManagerUserFK == UserprofileDTO.DirectManagerID)?.FirstOrDefault()?.ManagerID;
            if (UserprofileDTO.DirectManagerID!=null && User.DirectManagerFK==null)
            {
                ManagerDTO NewAddedManager = new ManagerDTO()
                {
                    ManagerName = userBLL.Find(x => x.UserID == UserprofileDTO.DirectManagerID.Value)?.FirstOrDefault()?.UserName,
                    ManagerUserFK= UserprofileDTO.DirectManagerID.Value
                };
                managerBLL.AddManager(NewAddedManager);
                User.DirectManagerFK = NewAddedManager.ManagerID;
            }
            
            User.GenderFK = UserprofileDTO.GenderID;
            User.PositionFK = UserprofileDTO.positionID;
            User.PhoneNumber = UserprofileDTO.BusinessPhone;
            User.BirthDate = UserprofileDTO.BirthDate != "" ? DateTime.ParseExact(UserprofileDTO.BirthDate, @"dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            User.CustomNote = UserprofileDTO.CustomeNote;
            User.UserEmail = UserprofileDTO.UserEmail;
            User.HireDate = UserprofileDTO.HireDate != "" ? DateTime.ParseExact(UserprofileDTO.HireDate, @"dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            User.SeniorityBeforeHireMonth = UserprofileDTO.SeniorityBeforeHireMonth;
            User.SeniorityBeforeHireYears = UserprofileDTO.SeniorityBeforeHireYear;
            User.ProbationDate = UserprofileDTO.ProbationDate != "" ? DateTime.ParseExact(UserprofileDTO.ProbationDate, @"dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            User.TerminationDate = UserprofileDTO.TerminationDate != "" ? DateTime.ParseExact(UserprofileDTO.TerminationDate, @"dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;
            User.NationalID = UserprofileDTO.NationalID;
            User.MedicalID = UserprofileDTO.MedicalID;
            User.HomeAddress = UserprofileDTO.HomeAddress;
            User.Address = UserprofileDTO.HomeAddress;
            User.CompanyFK = UserprofileDTO.CompanyID;
            User.WorkingModeFK = UserprofileDTO.WorkingModeID;
            
            if (UserprofileDTO.WorkingWeekID != 0)
                User.WorkingWeekFK = UserprofileDTO.WorkingWeekID;
            if (User.WorkingModeFK != (int)WorkingMode.Regular)
                User.WorkingWeekFK = null;

            if (UserprofileDTO.IsActive != null)
                User.IsActive = UserprofileDTO.IsActive;

            userBLL.Edit(User);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserprofileDTO.ModifiedByUserId, oldUser, XMLObjectConverter.Serialize(User), "Edit User Data");

            //string UserAfterEdit=  User.Serialize();
            //Logger.LogCypressEvent(null, (int)LogTypes.EditUserData, -1, "", UserBeforeEdit, UserAfterEdit, "edited");
        }

        public void EditAdditionalUserData(UserprofileDTO AdditionalUserData)
        {

            try
            {
                User User = userBLL.Find(x => x.UserID == AdditionalUserData.UserID).FirstOrDefault();
                var oldUser =  XMLObjectConverter.Serialize(User);
                User.UserPersonalEmail = AdditionalUserData.PersonalEmail;
                User.HomeAddress = AdditionalUserData.HomeAddress;
                User.Address = AdditionalUserData.CurrentAddress;
                User.SeniorityBeforeHireYears = AdditionalUserData.SeniorityBeforeHireYear;
                User.SeniorityBeforeHireMonth = AdditionalUserData.SeniorityBeforeHireMonth;
                User.HasCompletedUserProfileData = true;
                userBLL.Edit(User);
                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)AdditionalUserData.ModifiedByUserId, oldUser, XMLObjectConverter.Serialize(User), "Edit User Data");

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                throw;
            }

        }

        public UserDataDropDownDTO GetUserDataDropDowns()
        {

            UserDataDropDownDTO userDataDropDownDTO = new UserDataDropDownDTO();
            userDataDropDownDTO.UserDTO = new List<UserDTO>();
            userDataDropDownDTO.ApprovalFlow = new List<ApprovalFlowContainerDTO>();
            userDataDropDownDTO.Departments = new List<DepartmentDTO>();
            userDataDropDownDTO.ManagerDTO = new List<ManagerDTO>();
            userDataDropDownDTO.SubDepartments = new List<SubDepartmentDTO>();
            userDataDropDownDTO.Positios = new List<PositionDTO>();
            userDataDropDownDTO.GenderTypes = new List<GenderTypeDTO>();
            userDataDropDownDTO.ContractTypes = new List<ContractTypeDTO>();
            userDataDropDownDTO.Companies = new List<CompanyDTO>();
            userDataDropDownDTO.WorkingMode = new List<WorkingModeDTO>();
            userDataDropDownDTO.WorkingWeek = new List<WorkingWeekDTO>();

            foreach (var UserItem in userBLL.GetAll().ToList())
            {
                userDataDropDownDTO.UserDTO.Add(new UserDTO
                {
                    UserID = UserItem.UserID,
                    UserName = UserItem.UserName

                });
            }


            var DistinctTeamManagersIDs = subDepartmentBLL.GetAll().Select(p => p.TeamManagerFK).Distinct().ToList();

            var AllTeamManagers = managerBLL.Find(x => DistinctTeamManagersIDs.Contains(x.ManagerID)).ToList();

            foreach (var ManagerItem in AllTeamManagers)
            {
                userDataDropDownDTO.ManagerDTO.Add(new ManagerDTO
                {
                    ManagerID = ManagerItem.ManagerID,
                    ManagerName = ManagerItem.ManagerName

                });
            }

            foreach (var PositionItem in PositionBLL.GetAll().ToList())
            {
                userDataDropDownDTO.Positios.Add(new PositionDTO
                {
                    PositionID = PositionItem.PositionID,
                    PositionName = PositionItem.PositionName

                });
            }


            foreach (var DepartmentItem in departmentBLL.GetAll().ToList())
            {
                userDataDropDownDTO.Departments.Add(new DepartmentDTO
                {
                    DepartmentID = DepartmentItem.DepartmentID,
                    DepartmentName = DepartmentItem.DepartmentName

                });
            }


            foreach (var subDeptItem in subDepartmentBLL.GetAll().ToList())
            {
                userDataDropDownDTO.SubDepartments.Add(new SubDepartmentDTO
                {
                    SubDepartmentID = subDeptItem.SubDepartmentID,
                    SubDepartmentName = subDeptItem.SubDepartmentName

                });
            }

            foreach (var ApprovalItem in approvalBLL.GetAll().Where(x=>x.IsActive==true&&x.IsDeleted==false).ToList())
            {
                userDataDropDownDTO.ApprovalFlow.Add(new ApprovalFlowContainerDTO
                {
                    ApprovalFlowID = ApprovalItem.ApprovalFlowID,
                    ApprovalFlowName = ApprovalItem.ApprovalFlowName

                });
            }

            foreach (var GenderItems in GenderTypeBLL.GetAll().ToList())
            {
                userDataDropDownDTO.GenderTypes.Add(new GenderTypeDTO
                {
                    GenderID =  GenderItems.GenderID,
                    GenderName = GenderItems.GenderName
                });
            }

            foreach (var ContractTypeItems in ContractTypeBLL.GetAll().ToList())
            {
                userDataDropDownDTO.ContractTypes.Add(new ContractTypeDTO
                {
                    ContractTypeID = ContractTypeItems.ContractTypeID,
                    ContractTypeName = ContractTypeItems.ContractName
                });
            }

            foreach (var Companies in companyBLL.GetAll().ToList())
            {
                userDataDropDownDTO.Companies.Add(new CompanyDTO {
                    CompanyID = Companies.CompanyID,
                    CompanyName = Companies.CompanyName
                });
            }

            foreach (var WorkingModeItem in workingModeBLL.GetAll().ToList())
            {
                userDataDropDownDTO.WorkingMode.Add(new WorkingModeDTO
                {
                    WorkingModeID = WorkingModeItem.WorkingModeID,
                    WorkingModeName = WorkingModeItem.WorkingModeName
                });
            }

            foreach (var WorkingWeekItem in workingWeekBLL.GetAll().ToList())
            {
                userDataDropDownDTO.WorkingWeek.Add(new WorkingWeekDTO
                {
                    WorkingWeekID = WorkingWeekItem.WorkingWeekID,
                    WorkingWeekName = WorkingWeekItem.WorkingWeekName
                });
            }

            return userDataDropDownDTO;
        }


        //get All users Information
        public List<UserInformationOutputDTO> UsersInformations(UsersInformationInputDTO usersInformationInputDTO)
        {
            List<UserInformationOutputDTO> UsersDataList = new List<UserInformationOutputDTO>();
            UserInformationOutputDTO tempUsersData;
            if (usersInformationInputDTO.FiliterUsersBy == 0)
            {
                int z = userBLL.GetAll().Where(u=>(u.IsActive==true|| u.IsActive == false)&&u.IsDeleted!=true ).ToList().Count;
                foreach (var user in userBLL.GetAll().Where(u => (u.IsActive == true || u.IsActive == false) && u.IsDeleted != true).ToList())
                {
                    tempUsersData = new UserInformationOutputDTO();
                    tempUsersData.UserID = user.UserID;//1
                    tempUsersData.UserName = user.UserName;//2
                    tempUsersData.Address = user.Address;//3
                    tempUsersData.HomeAddress = user.HomeAddress;//4
                    tempUsersData.ArbicName = user.ArName;//5
                    tempUsersData.BirthDate = user.BirthDate?.ToString("dd/MM/yyyy");//6
                    tempUsersData.HireDate = user.HireDate?.ToString("dd/MM/yyyy");//7
                    tempUsersData.ProbationDate = user.ProbationDate?.ToString("dd/MM/yyyy");//8
                    tempUsersData.TerminationDate = user.TerminationDate?.ToString("dd/MM/yyyy");//9
                    tempUsersData.creationDate = user.CreationDate;//10
                    tempUsersData.SeniorityBeforeHireMonth = user.SeniorityBeforeHireMonth;//11
                    tempUsersData.SeniorityBeforeHireYears = user.SeniorityBeforeHireYears;//12
                    tempUsersData.PhoneNumber = user.PhoneNumber;//13
                    tempUsersData.AccessControlUserID = user.AccessControlUserFK;//14
                    tempUsersData.NationalID = user.NationalID;//15
                    tempUsersData.MedicalID = user.MedicalID;//16
                    tempUsersData.ModificationDate = user.ModificationDate;//17
                    var company = companyBLL.Find(c => c.CompanyID == user.CompanyFK).FirstOrDefault();//18
                    if (company != null)//18
                    {
                        tempUsersData.CompanyName = company.CompanyName;
                    }
                    if (user.IsOutDomainUser == true)//19
                    {
                        tempUsersData.IsOutDomainUser = "True";
                    }
                    else
                    {
                        tempUsersData.IsOutDomainUser = "False";
                    }
                    if (user.WorkingWeekFK != null)//20
                    {
                        var workingWeek = workingWeekBLL.Find(ww => ww.WorkingWeekID == user.WorkingWeekFK).FirstOrDefault();
                        if (workingWeek != null)
                        {
                            tempUsersData.workingWeekName = workingWeek.WorkingWeekName;

                        }
                    }
                    if (user.IsActive != null)//21
                    {
                        if (user.IsActive.Value)
                        {
                            tempUsersData.IsActive = "True";

                        }
                        else
                        {
                            tempUsersData.IsActive = "False";

                        }
                    }
                    else
                    {
                        tempUsersData.IsActive = "False";
                    }
                    var depertment = departmentBLL.Find(d => d.DepartmentID == user.DepartmentFK).FirstOrDefault();//22
                    if (depertment != null)
                    {
                        tempUsersData.DepartmentName = depertment.DepartmentName;

                    }
                    if (user.SubDepartmentFK != null && user.SubDepartmentFK.ToString() != string.Empty)//team Manager 23,24
                    {
                        var subDepertment = subDepartmentBLL.Find(sd => sd.SubDepartmentID == user.SubDepartmentFK).FirstOrDefault();
                        if (subDepertment != null)//23
                        {
                            tempUsersData.SubDepartmentName = subDepertment.SubDepartmentName;

                        }
                        else
                        {
                            tempUsersData.SubDepartmentName = "---";
                        }
                        int subDepertMentManagerId = subDepertment.TeamManagerFK.Value;
                        var teamManagerName = managerBLL.Find(u => u.ManagerID == subDepertMentManagerId).FirstOrDefault();
                        if (teamManagerName != null)//24
                        {
                            tempUsersData.TeamManagerName = teamManagerName.ManagerName;

                        }
                        else
                        {
                            tempUsersData.TeamManagerName = "---";
                        }
                    }//23,24
                    var directManager = managerBLL.Find(u => u.ManagerID == user.DirectManagerFK).FirstOrDefault();//25
                    if (directManager != null)
                    {
                        tempUsersData.ManagerName = directManager.ManagerName;
                    }
                    var position = positionBLL.Find(p => p.PositionID == user.PositionFK).FirstOrDefault();//26
                    if (position != null)
                    {
                        tempUsersData.PositionName = position.PositionName;

                    }
                    var ApprovalFlow = approvalBLL.Find(af => af.ApprovalFlowID == user.ApprovalFlowFK).FirstOrDefault();//27
                    if (ApprovalFlow != null)
                    {
                        tempUsersData.ApprovalFlowName = ApprovalFlow.ApprovalFlowName;

                    }
                    var contractTypeName = ContractTypeBLL.Find(p => p.ContractTypeID == user.ContractTypeFK).FirstOrDefault();//28
                    if (contractTypeName != null)
                    {
                        tempUsersData.ContractTypeName = contractTypeName.ContractName;
                    }
                    var gander = GenderTypeBLL.Find(p => p.GenderID == user.GenderFK).FirstOrDefault();//29
                    if (gander != null)
                    {
                        tempUsersData.Gender = gander.GenderName;

                    }

                    tempUsersData.CustomNote = user.CustomNote;//30

                    UsersDataList.Add(tempUsersData);
                }
                return UsersDataList;
            }
            else if (usersInformationInputDTO.FiliterUsersBy == 1)
            {
                int z = userBLL.GetAll().Where(u => u.IsActive == true).ToList().Count;
                foreach (var user in userBLL.GetAll().Where(u => u.IsActive == true).ToList())
                {
                    tempUsersData = new UserInformationOutputDTO();
                    tempUsersData.UserID = user.UserID;//1
                    tempUsersData.UserName = user.UserName;//2
                    tempUsersData.Address = user.Address;//3
                    tempUsersData.HomeAddress = user.HomeAddress;//4
                    tempUsersData.ArbicName = user.ArName;//5
                    tempUsersData.BirthDate = user.BirthDate?.ToString("dd/MM/yyyy");//6
                    tempUsersData.HireDate = user.HireDate?.ToString("dd/MM/yyyy");//7
                    tempUsersData.ProbationDate = user.ProbationDate?.ToString("dd/MM/yyyy");//8
                    tempUsersData.TerminationDate = user.TerminationDate?.ToString("dd/MM/yyyy");//9
                    tempUsersData.creationDate = user.CreationDate;//10
                    tempUsersData.SeniorityBeforeHireMonth = user.SeniorityBeforeHireMonth;//11
                    tempUsersData.SeniorityBeforeHireYears = user.SeniorityBeforeHireYears;//12
                    tempUsersData.PhoneNumber = user.PhoneNumber;//13
                    tempUsersData.AccessControlUserID = user.AccessControlUserFK;//14
                    tempUsersData.NationalID = user.NationalID;//15
                    tempUsersData.MedicalID = user.MedicalID;//16
                    tempUsersData.ModificationDate = user.ModificationDate;//17
                    var company = companyBLL.Find(c => c.CompanyID == user.CompanyFK).FirstOrDefault();//18
                    if (company != null)//18
                    {
                        tempUsersData.CompanyName = company.CompanyName;
                    }
                    if (user.IsOutDomainUser == true)//19
                    {
                        tempUsersData.IsOutDomainUser = "True";
                    }
                    else
                    {
                        tempUsersData.IsOutDomainUser = "False";
                    }
                    if (user.WorkingWeekFK != null)//20
                    {
                        var workingWeek = workingWeekBLL.Find(ww => ww.WorkingWeekID == user.WorkingWeekFK).FirstOrDefault();
                        if (workingWeek != null)
                        {
                            tempUsersData.workingWeekName = workingWeek.WorkingWeekName;

                        }
                    }
                    if (user.IsActive != null)//21
                    {
                        if (user.IsActive.Value)
                        {
                            tempUsersData.IsActive = "True";

                        }
                        else
                        {
                            tempUsersData.IsActive = "False";

                        }
                    }
                    else
                    {
                        tempUsersData.IsActive = "False";
                    }
                    var depertment = departmentBLL.Find(d => d.DepartmentID == user.DepartmentFK).FirstOrDefault();//22
                    if (depertment != null)
                    {
                        tempUsersData.DepartmentName = depertment.DepartmentName;

                    }
                    if (user.SubDepartmentFK != null && user.SubDepartmentFK.ToString() != string.Empty)//team Manager 23,24
                    {
                        var subDepertment = subDepartmentBLL.Find(sd => sd.SubDepartmentID == user.SubDepartmentFK).FirstOrDefault();
                        if (subDepertment != null)//23
                        {
                            tempUsersData.SubDepartmentName = subDepertment.SubDepartmentName;

                        }
                        else
                        {
                            tempUsersData.SubDepartmentName = "---";
                        }
                        int subDepertMentManagerId = subDepertment.TeamManagerFK.Value;
                        var teamManagerName = managerBLL.Find(u => u.ManagerID == subDepertMentManagerId).FirstOrDefault();
                        if (teamManagerName != null)//24
                        {
                            tempUsersData.TeamManagerName = teamManagerName.ManagerName;

                        }
                        else
                        {
                            tempUsersData.TeamManagerName = "---";
                        }
                    }//23,24
                    var directManager = managerBLL.Find(u => u.ManagerID == user.DirectManagerFK).FirstOrDefault();//25
                    if (directManager != null)
                    {
                        tempUsersData.ManagerName = directManager.ManagerName;
                    }
                    var position = positionBLL.Find(p => p.PositionID == user.PositionFK).FirstOrDefault();//26
                    if (position != null)
                    {
                        tempUsersData.PositionName = position.PositionName;

                    }
                    var ApprovalFlow = approvalBLL.Find(af => af.ApprovalFlowID == user.ApprovalFlowFK).FirstOrDefault();//27
                    if (ApprovalFlow != null)
                    {
                        tempUsersData.ApprovalFlowName = ApprovalFlow.ApprovalFlowName;

                    }
                    var contractTypeName = ContractTypeBLL.Find(p => p.ContractTypeID == user.ContractTypeFK).FirstOrDefault();//28
                    if (contractTypeName != null)
                    {
                        tempUsersData.ContractTypeName = contractTypeName.ContractName;
                    }
                    var gander = GenderTypeBLL.Find(p => p.GenderID == user.GenderFK).FirstOrDefault();//29
                    if (gander != null)
                    {
                        tempUsersData.Gender = gander.GenderName;

                    }

                    tempUsersData.CustomNote = user.CustomNote;//30

                    UsersDataList.Add(tempUsersData);
                }
                return UsersDataList;
            }
            else if (usersInformationInputDTO.FiliterUsersBy == 2)
            {
                int z = userBLL.GetAll().Where(u => u.IsActive == false&&u.IsDeleted!=true).ToList().Count;
                foreach (var user in userBLL.GetAll().Where(u => u.IsActive == false && u.IsDeleted != true).ToList())
                {
                    tempUsersData = new UserInformationOutputDTO();
                    tempUsersData.UserID = user.UserID;//1
                    tempUsersData.UserName = user.UserName;//2
                    tempUsersData.Address = user.Address;//3
                    tempUsersData.HomeAddress = user.HomeAddress;//4
                    tempUsersData.ArbicName = user.ArName;//5
                    tempUsersData.BirthDate = user.BirthDate?.ToString("dd/MM/yyyy");//6
                    tempUsersData.HireDate = user.HireDate?.ToString("dd/MM/yyyy");//7
                    tempUsersData.ProbationDate = user.ProbationDate?.ToString("dd/MM/yyyy");//8
                    tempUsersData.TerminationDate = user.TerminationDate?.ToString("dd/MM/yyyy");//9
                    tempUsersData.creationDate = user.CreationDate;//10
                    tempUsersData.SeniorityBeforeHireMonth = user.SeniorityBeforeHireMonth;//11
                    tempUsersData.SeniorityBeforeHireYears = user.SeniorityBeforeHireYears;//12
                    tempUsersData.PhoneNumber = user.PhoneNumber;//13
                    tempUsersData.AccessControlUserID = user.AccessControlUserFK;//14
                    tempUsersData.NationalID = user.NationalID;//15
                    tempUsersData.MedicalID = user.MedicalID;//16
                    tempUsersData.ModificationDate = user.ModificationDate;//17
                    var company = companyBLL.Find(c => c.CompanyID == user.CompanyFK).FirstOrDefault();//18
                    if (company != null)//18
                    {
                        tempUsersData.CompanyName = company.CompanyName;
                    }
                    if (user.IsOutDomainUser == true)//19
                    {
                        tempUsersData.IsOutDomainUser = "True";
                    }
                    else
                    {
                        tempUsersData.IsOutDomainUser = "False";
                    }
                    if (user.WorkingWeekFK != null)//20
                    {
                        var workingWeek = workingWeekBLL.Find(ww => ww.WorkingWeekID == user.WorkingWeekFK).FirstOrDefault();
                        if (workingWeek != null)
                        {
                            tempUsersData.workingWeekName = workingWeek.WorkingWeekName;

                        }
                    }
                    if (user.IsActive != null)//21
                    {
                        if (user.IsActive.Value)
                        {
                            tempUsersData.IsActive = "True";

                        }
                        else
                        {
                            tempUsersData.IsActive = "False";

                        }
                    }
                    else
                    {
                        tempUsersData.IsActive = "False";
                    }
                    var depertment = departmentBLL.Find(d => d.DepartmentID == user.DepartmentFK).FirstOrDefault();//22
                    if (depertment != null)
                    {
                        tempUsersData.DepartmentName = depertment.DepartmentName;

                    }
                    if (user.SubDepartmentFK != null && user.SubDepartmentFK.ToString() != string.Empty)//team Manager 23,24
                    {
                        var subDepertment = subDepartmentBLL.Find(sd => sd.SubDepartmentID == user.SubDepartmentFK).FirstOrDefault();
                        if (subDepertment != null)//23
                        {
                            tempUsersData.SubDepartmentName = subDepertment.SubDepartmentName;

                        }
                        else
                        {
                            tempUsersData.SubDepartmentName = "---";
                        }
                        int subDepertMentManagerId = subDepertment.TeamManagerFK.Value;
                        var teamManagerName = managerBLL.Find(u => u.ManagerID == subDepertMentManagerId).FirstOrDefault();
                        if (teamManagerName != null)//24
                        {
                            tempUsersData.TeamManagerName = teamManagerName.ManagerName;

                        }
                        else
                        {
                            tempUsersData.TeamManagerName = "---";
                        }
                    }//23,24
                    var directManager = managerBLL.Find(u => u.ManagerID == user.DirectManagerFK).FirstOrDefault();//25
                    if (directManager != null)
                    {
                        tempUsersData.ManagerName = directManager.ManagerName;
                    }
                    var position = positionBLL.Find(p => p.PositionID == user.PositionFK).FirstOrDefault();//26
                    if (position != null)
                    {
                        tempUsersData.PositionName = position.PositionName;

                    }
                    var ApprovalFlow = approvalBLL.Find(af => af.ApprovalFlowID == user.ApprovalFlowFK).FirstOrDefault();//27
                    if (ApprovalFlow != null)
                    {
                        tempUsersData.ApprovalFlowName = ApprovalFlow.ApprovalFlowName;

                    }
                    var contractTypeName = ContractTypeBLL.Find(p => p.ContractTypeID == user.ContractTypeFK).FirstOrDefault();//28
                    if (contractTypeName != null)
                    {
                        tempUsersData.ContractTypeName = contractTypeName.ContractName;
                    }
                    var gander = GenderTypeBLL.Find(p => p.GenderID == user.GenderFK).FirstOrDefault();//29
                    if (gander != null)
                    {
                        tempUsersData.Gender = gander.GenderName;

                    }

                    tempUsersData.CustomNote = user.CustomNote;//30

                    UsersDataList.Add(tempUsersData);
                }
                return UsersDataList;
            }
            else
            {
                User user;
                for (int i = 0; i < usersInformationInputDTO.ListUserstIDs.Count; i++)
                {
                    int userId = usersInformationInputDTO.ListUserstIDs[i];
                    user = userBLL.Find(u => u.UserID == userId).FirstOrDefault();

                    if (user != null)
                    {
                        tempUsersData = new UserInformationOutputDTO();
                        tempUsersData.UserID = user.UserID;//1
                        tempUsersData.UserName = user.UserName;//2
                        tempUsersData.Address = user.Address;//3
                        tempUsersData.HomeAddress = user.HomeAddress;//4
                        tempUsersData.ArbicName = user.ArName;//5
                        tempUsersData.BirthDate = user.BirthDate?.ToString("dd/MM/yyyy");//6
                        tempUsersData.HireDate = user.HireDate?.ToString("dd/MM/yyyy");//7
                        tempUsersData.ProbationDate = user.ProbationDate?.ToString("dd/MM/yyyy");//8
                        tempUsersData.TerminationDate = user.TerminationDate?.ToString("dd/MM/yyyy");//9
                        tempUsersData.creationDate = user.CreationDate;//10
                        tempUsersData.SeniorityBeforeHireMonth = user.SeniorityBeforeHireMonth;//11
                        tempUsersData.SeniorityBeforeHireYears = user.SeniorityBeforeHireYears;//12
                        tempUsersData.PhoneNumber = user.PhoneNumber;//13
                        tempUsersData.AccessControlUserID = user.AccessControlUserFK;//14
                        tempUsersData.NationalID = user.NationalID;//15
                        tempUsersData.MedicalID = user.MedicalID;//16
                        tempUsersData.ModificationDate = user.ModificationDate;//17
                        var company = companyBLL.Find(c => c.CompanyID == user.CompanyFK).FirstOrDefault();//18
                        if (company!=null)//18
                        {
                            tempUsersData.CompanyName = company.CompanyName;
                        }
                        if(user.IsOutDomainUser==true)//19
                        {
                            tempUsersData.IsOutDomainUser = "True";
                        }
                        else
                        {
                            tempUsersData.IsOutDomainUser = "False";
                        }
                        if (user.WorkingWeekFK != null)//20
                        {
                            var workingWeek = workingWeekBLL.Find(ww => ww.WorkingWeekID == user.WorkingWeekFK).FirstOrDefault();
                            if (workingWeek != null)
                            {
                                tempUsersData.workingWeekName = workingWeek.WorkingWeekName;

                            }
                        }
                        if (user.IsActive != null)//21
                        {
                            if (user.IsActive.Value)
                            {
                                tempUsersData.IsActive = "True";

                            }
                            else
                            {
                                tempUsersData.IsActive = "False";

                            }
                        }
                        else
                        {
                            tempUsersData.IsActive = "False";
                        }
                        var depertment = departmentBLL.Find(d => d.DepartmentID == user.DepartmentFK).FirstOrDefault();//22
                        if (depertment != null)
                        {
                            tempUsersData.DepartmentName = depertment.DepartmentName;

                        }
                        if (user.SubDepartmentFK != null && user.SubDepartmentFK.ToString() != string.Empty)//team Manager 23,24
                        {
                            var subDepertment = subDepartmentBLL.Find(sd => sd.SubDepartmentID == user.SubDepartmentFK).FirstOrDefault();
                            if (subDepertment != null)//23
                            {
                                tempUsersData.SubDepartmentName = subDepertment.SubDepartmentName;

                            }
                            else
                            {
                                tempUsersData.SubDepartmentName = "---";
                            }
                            int subDepertMentManagerId = subDepertment.TeamManagerFK.Value;
                            var teamManagerName = managerBLL.Find(u => u.ManagerID == subDepertMentManagerId).FirstOrDefault();
                            if (teamManagerName != null)//24
                            {
                                tempUsersData.TeamManagerName = teamManagerName.ManagerName;

                            }
                            else
                            {
                                tempUsersData.TeamManagerName = "---";
                            }
                        }//23,24
                        var directManager = managerBLL.Find(u => u.ManagerID == user.DirectManagerFK).FirstOrDefault();//25
                        if (directManager != null)
                        {
                            tempUsersData.ManagerName = directManager.ManagerName;
                        }
                        var position = positionBLL.Find(p => p.PositionID == user.PositionFK).FirstOrDefault();//26
                        if (position != null)
                        {
                            tempUsersData.PositionName = position.PositionName;

                        }
                        var ApprovalFlow = approvalBLL.Find(af => af.ApprovalFlowID == user.ApprovalFlowFK).FirstOrDefault();//27
                        if (ApprovalFlow != null)
                        {
                            tempUsersData.ApprovalFlowName = ApprovalFlow.ApprovalFlowName;

                        }
                        var contractTypeName = ContractTypeBLL.Find(p => p.ContractTypeID == user.ContractTypeFK).FirstOrDefault();//28
                        if (contractTypeName != null)
                        {
                            tempUsersData.ContractTypeName = contractTypeName.ContractName;
                        }
                        var gander = GenderTypeBLL.Find(p => p.GenderID == user.GenderFK).FirstOrDefault();//29
                        if (gander != null)
                        {
                            tempUsersData.Gender = gander.GenderName;

                        }
   
                        tempUsersData.CustomNote = user.CustomNote;//30

                        UsersDataList.Add(tempUsersData);
                    }

                }
                return UsersDataList;
            }
        }
        //--------------------------------------------------------------
        public List<string> getAllUserInformationProperty()
        {
            List<string> classProperts = new List<string>();
            foreach (PropertyInfo p in typeof(UserInformationOutputDTO).GetProperties())
            {
                string propertyName = p.Name;
                classProperts.Add(propertyName);
                
            }
            return classProperts;
        }




    }
}
