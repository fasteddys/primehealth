using HRMS.BLL.Logic.Tables;
using HRMS.BLL.StaticData;
using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Shared_Logic
{
    public class SharedUserEntitlementBLL
    {
        UserEntitlementBLL userEntitlementBLL;
        UserEntitlementChangesBLL userEntitlementChangesBLL;
        UserBLL userBLL;
        LeaveTypeBLL leaveTypeBLL;
        EntitlementChangedByDIMBLL entitlementChangedByDIMBLL;
        MonthlyEffectiveAccuredLeaveTypesBLL monthlyEffectiveAccuredLeaveTypesBLL;
        DateTime creationDate;

        public SharedUserEntitlementBLL(HRMSEntities Context, DateTime CreationDate)
        {
            userEntitlementChangesBLL = new UserEntitlementChangesBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, creationDate);
            leaveTypeBLL = new LeaveTypeBLL(Context, creationDate);
            userEntitlementBLL = new UserEntitlementBLL(Context, creationDate);
            entitlementChangedByDIMBLL = new EntitlementChangedByDIMBLL(Context, creationDate);
            monthlyEffectiveAccuredLeaveTypesBLL = new MonthlyEffectiveAccuredLeaveTypesBLL(Context, creationDate);
            creationDate = CreationDate;
        }

        public List<UserEntitlement> GetMonthlyOrNotEntitlement(HRViewRequestsFilterDTO viewRequestsFilterDTO)
        {
            List<UserEntitlement> UserEntitlement = new List<UserEntitlement>();

            foreach (var items in viewRequestsFilterDTO.ListLeaveType)
            {
                bool IsMonthly = leaveTypeBLL.IsMonthlyLeaveType(items);

                if (IsMonthly)
                    UserEntitlement.AddRange(userEntitlementBLL.Find(x => viewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == viewRequestsFilterDTO.Year && x.Month == viewRequestsFilterDTO.Month && x.IsActive == true).OrderByDescending(x => x.CreationDate).ToList());
                else
                    UserEntitlement.AddRange(userEntitlementBLL.Find(x => viewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == viewRequestsFilterDTO.Year && x.IsActive == true).OrderByDescending(x => x.CreationDate).ToList());
            }
            return UserEntitlement;
        }

        public void ChangeQuantityOfUserEntitlement(UserEntitlementsDTO UserEntitlementsDTO)
        {
            UserEntitlement UserEntitlementForParentLeaveObj = null;
            UserEntitlement UserEntitlementObj = null;
            bool CreateUserEntitlementChangeForParent = false;
            decimal OldParentEntitlementTotal = 0;
            decimal OldEntitlementTotal = 0;
            bool IsNew = false;
            string ModificationDateString = UserEntitlementsDTO.ModificationDate.ToString();

            //Monthly Entitlement
            if(UserEntitlementsDTO.Month != null)
                UserEntitlementObj = userEntitlementBLL.Find(x => x.LeaveTypeFK == UserEntitlementsDTO.LeaveTypeFK && x.UserFK == UserEntitlementsDTO.UserFK && x.Year == UserEntitlementsDTO.Years.Value && x.Month == UserEntitlementsDTO.Month.Value).FirstOrDefault();
            else
                UserEntitlementObj = userEntitlementBLL.Find(x => x.LeaveTypeFK == UserEntitlementsDTO.LeaveTypeFK && x.UserFK == UserEntitlementsDTO.UserFK && x.IsActive == true).FirstOrDefault();

            LeaveType LeaveTypeObj = leaveTypeBLL.Find(X => X.LeaveTypeID == UserEntitlementsDTO.LeaveTypeFK && X.IsActive == true).FirstOrDefault();
            LeaveType ParentLeaveTypeObj = leaveTypeBLL.Find(X => X.LeaveTypeID == LeaveTypeObj.ParentLeaveTypeFK && X.IsActive == true).SingleOrDefault();
            bool CheckMonthlyLeaveType = leaveTypeBLL.IsMonthlyLeaveType(LeaveTypeObj.LeaveTypeID);

            //make sure the leavetype isn't a parent
            if (ParentLeaveTypeObj != null)
                UserEntitlementForParentLeaveObj = userEntitlementBLL.Find(x => x.LeaveTypeFK == ParentLeaveTypeObj.LeaveTypeID && x.UserFK == UserEntitlementsDTO.UserFK && x.IsActive == true).FirstOrDefault();// != null ? userEntitlementBLL.Find(x => x.LeaveTypeFK == ParentLeaveTypeObj.LeaveTypeID && x.UserFK == UserEntitlementsDTO.UserFK && x.IsActive == true)?.FirstOrDefault() : null;

            if (LeaveTypeObj.ParentLeaveTypeFK != null && UserEntitlementForParentLeaveObj == null)
            {
                throw new NullReferenceException("This user haven't parent entitlement.");
            }

            UserEntitlementForParentLeaveObj = null;

            if (UserEntitlementObj == null )
            {
                if(UserEntitlementsDTO.ISAddition == true)
                {
                    UserEntitlement NewUserEntitlement = new UserEntitlement
                    {
                        LeaveTypeFK = UserEntitlementsDTO.LeaveTypeFK,
                        EntitlementTotal = UserEntitlementsDTO.EntitlementModifiedQty,
                        UserFK = UserEntitlementsDTO.UserFK,
                        LeaveDurationUnitFK = LeaveTypeObj.DurationUnitFK.Value,
                        IsActive = true,
                        IsDeleted = false,
                        Year = creationDate.Year,
                        CreationDate = creationDate,
                    };
                    if (CheckMonthlyLeaveType)
                    {
                        MonthlyEffectiveAccuredLeaveType monthlyEffectiveAccuredLeaveTypes = monthlyEffectiveAccuredLeaveTypesBLL.Find(x => x.LeaveTypeFK == UserEntitlementsDTO.LeaveTypeFK && x.IsActive == true).FirstOrDefault();
                        NewUserEntitlement.EffectiveDateFrom = monthlyEffectiveAccuredLeaveTypes.EffectiveDateFrom;
                        NewUserEntitlement.EffectiveDateTo = monthlyEffectiveAccuredLeaveTypes.EffectiveDateTo;
                        NewUserEntitlement.Month = monthlyEffectiveAccuredLeaveTypes.Month;
                        NewUserEntitlement.Year = monthlyEffectiveAccuredLeaveTypes.Year;
                    }
                    UserEntitlementObj = NewUserEntitlement;

                    userEntitlementBLL.AddEntitlmentForLeaveTypeIfNotExist(NewUserEntitlement);
                    IsNew = true;
                }
                else
                {
                    throw new Exception("Invalid Operation");
                }
                
            }

            //Monthly Entitlement
            if (UserEntitlementObj?.UserEntitlementID != 0)
           {
                if (UserEntitlementsDTO.Month != null)
                {
                    UserEntitlementObj = userEntitlementBLL.Find(x => x.LeaveTypeFK == UserEntitlementsDTO.LeaveTypeFK && x.UserFK == UserEntitlementsDTO.UserFK && x.Year == UserEntitlementsDTO.Years.Value && x.Month == UserEntitlementsDTO.Month.Value).FirstOrDefault();
                }

                else
                {
                    UserEntitlementObj = userEntitlementBLL.Find(x => x.LeaveTypeFK == UserEntitlementsDTO.LeaveTypeFK && x.UserFK == UserEntitlementsDTO.UserFK && x.IsActive == true).FirstOrDefault();
                }
            }
            
            var oldUserEntitlementObj = XMLObjectConverter.Serialize(UserEntitlementObj) ;

            //Oring to make sure the child when created on first time shlould update it's parent
            if (!IsNew || (IsNew && ParentLeaveTypeObj != null))
            {
                if (ParentLeaveTypeObj != null)
                {
                    UserEntitlementForParentLeaveObj = userEntitlementBLL.Find(x => x.LeaveTypeFK == ParentLeaveTypeObj.LeaveTypeID && x.UserFK == UserEntitlementsDTO.UserFK && x.IsActive == true).FirstOrDefault();
                    OldParentEntitlementTotal = UserEntitlementForParentLeaveObj.EntitlementTotal;
                }

                OldEntitlementTotal = IsNew ? 0 : UserEntitlementObj.EntitlementTotal;

                if (UserEntitlementsDTO.ISAddition == true)
                {
                    if(!IsNew)
                        UserEntitlementObj.EntitlementTotal += UserEntitlementsDTO.EntitlementModifiedQty;

                    if(UserEntitlementForParentLeaveObj != null)
                    {
                        var oldUserEntitlementForParentLeaveObj =XMLObjectConverter.Serialize(UserEntitlementForParentLeaveObj) ;

                        UserEntitlementForParentLeaveObj.EntitlementTotal += UserEntitlementsDTO.EntitlementModifiedQty * (LeaveTypeObj.DeductionFromParentLeaveDurationPercentage / 100).Value;

                        UserEntitlementForParentLeaveObj.ModificationDate = creationDate;
                        Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData,(int) UserEntitlementsDTO.ModifiedByUserId,oldUserEntitlementForParentLeaveObj , XMLObjectConverter.Serialize(UserEntitlementForParentLeaveObj), "Change User Entitlement");

                        userEntitlementBLL.Edit(UserEntitlementForParentLeaveObj);

                        CreateUserEntitlementChangeForParent = true;
                    }
                }

                else if(UserEntitlementsDTO.ISAddition == false && UserEntitlementsDTO.EntitlementModifiedQty > UserEntitlementObj.EntitlementTotal)
                {
                    throw new Exception("Invalid Operation");
                }

                else if (UserEntitlementsDTO.ISAddition == false && UserEntitlementsDTO.EntitlementModifiedQty <= UserEntitlementObj.EntitlementTotal)
                {
                    if (!IsNew)
                        UserEntitlementObj.EntitlementTotal -= UserEntitlementsDTO.EntitlementModifiedQty;

                    if(UserEntitlementForParentLeaveObj != null)
                    {
                        var oldUserEntitlementForParentLeaveObj =  XMLObjectConverter.Serialize(UserEntitlementForParentLeaveObj);
                        UserEntitlementForParentLeaveObj.EntitlementTotal -= UserEntitlementsDTO.EntitlementModifiedQty * (LeaveTypeObj.DeductionFromParentLeaveDurationPercentage / 100).Value;

                        UserEntitlementForParentLeaveObj.ModificationDate = creationDate;
                        Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserEntitlementsDTO.ModifiedByUserId, oldUserEntitlementForParentLeaveObj, XMLObjectConverter.Serialize(UserEntitlementForParentLeaveObj), "Change User Entitlement");

                        userEntitlementBLL.Edit(UserEntitlementForParentLeaveObj);
                        CreateUserEntitlementChangeForParent = true;
                    }
                }

                /*else if (UserEntitlementsDTO.ISAddition == true && UserEntitlementForParentLeaveObj != null)
                {
                    UserEntitlementObj.EntitlementTotal += UserEntitlementsDTO.EntitlementModifiedQty;
                }

                else if (UserEntitlementsDTO.ISAddition == false && UserEntitlementForParentLeaveObj != null && UserEntitlementsDTO.EntitlementModifiedQty <= UserEntitlementObj.EntitlementTotal)
                {
                    UserEntitlementObj.EntitlementTotal -= UserEntitlementsDTO.EntitlementModifiedQty;
                    
                }*/

                UserEntitlementObj.ModificationDate = creationDate;
                userEntitlementBLL.Edit(UserEntitlementObj);
                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UserEntitlementsDTO.ModifiedByUserId, oldUserEntitlementObj, XMLObjectConverter.Serialize(UserEntitlementObj), "Change User Entitlement");

            }

            User UserObj = userBLL.Find(x => x.UserID == UserEntitlementsDTO.ModifiedBy).SingleOrDefault();

            int ChangedBy;
            if (UserObj.IsAdmin == true)
                ChangedBy = (int)StaticEnums.EntitlementChangedBy.Admin;
            else
                ChangedBy = (int)StaticEnums.EntitlementChangedBy.HR;

            UserEntitlementChange AddedChange = new UserEntitlementChange();
            if (UserEntitlementObj?.UserEntitlementID != 0)
                AddedChange.UserEntitlementFK = UserEntitlementObj.UserEntitlementID;
            else
                AddedChange.UserEntitlement = UserEntitlementObj;
            AddedChange.Comment = UserEntitlementsDTO.Comment;
            AddedChange.CreationDate = creationDate;
            AddedChange.LeaveDurationUnitFK = LeaveTypeObj.DurationUnitFK.Value;
            AddedChange.IsActive = true;
            AddedChange.EntitlementBefore = OldEntitlementTotal;
            AddedChange.EntitlementAfter = UserEntitlementObj.EntitlementTotal;
            AddedChange.EntitlementChangedByFK = ChangedBy;
            AddedChange.ActionDate = DateTime.ParseExact(ModificationDateString, @"dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
            AddedChange.IsDeleted = false;
            AddedChange.UserFK = UserEntitlementObj.UserFK;
            AddedChange.UserChangeEntitlementFK = UserEntitlementsDTO.ModifiedBy;
            AddedChange.Year = creationDate.Year;
            //ModificationDate= UserEntitlementObj.ModificationDate
            userEntitlementChangesBLL.Add(AddedChange);

            if (CreateUserEntitlementChangeForParent)
            {
                UserEntitlementChange AddedChangeForParent= new UserEntitlementChange
                {
                    UserEntitlementFK = UserEntitlementForParentLeaveObj.UserEntitlementID,
                    Comment = UserEntitlementsDTO.Comment,
                    CreationDate = creationDate,
                    LeaveDurationUnitFK = ParentLeaveTypeObj.DurationUnitFK.Value,
                    IsActive = true,
                    EntitlementBefore = OldParentEntitlementTotal,
                    EntitlementAfter = UserEntitlementForParentLeaveObj.EntitlementTotal,
                    EntitlementChangedByFK = ChangedBy,
                    ActionDate = DateTime.ParseExact(ModificationDateString, @"dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    UserFK = UserEntitlementForParentLeaveObj.UserFK,
                    UserChangeEntitlementFK = UserEntitlementsDTO.ModifiedBy,
                    Year = creationDate.Year
                    //ModificationDate = UserEntitlementForParentLeaveObj.ModificationDate
                };
                userEntitlementChangesBLL.Add(AddedChangeForParent);
            }

        }

        public List<UserEntitlementChangesSearchResultDTO> FilterUserEntitlementChanges(UserEntitlementChangesFilterDTO UserEntitlementsSearchDTO)
        {
            DateTime CreationFrom, CreationTo;
            bool IsMonthly = false;
            List<int> UserEntitlementID = new List<int>();
            List<UserEntitlementChange> UserEntitlementChanges = new List<UserEntitlementChange>();
            List<UserEntitlementChangesSearchResultDTO> UserEntitlementChangesResult = new List<UserEntitlementChangesSearchResultDTO>();

            TimeSpan UpdateCreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan UpdateCreationToTime = new TimeSpan(23, 59, 59);

            if (true)
            {
                if(UserEntitlementsSearchDTO.LeaveTypeID == 0 || UserEntitlementsSearchDTO.LeaveTypeID == null)
                {
                    foreach (var items in leaveTypeBLL.GetAll().Where(x => x.IsActive == true).ToList())
                    {
                        IsMonthly = leaveTypeBLL.IsMonthlyLeaveType(items.LeaveTypeID);
                        if (IsMonthly)
                            UserEntitlementID.AddRange(userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.LeaveTypeFK == items.LeaveTypeID).Select(x => x.UserEntitlementID).ToList());
                        else
                            UserEntitlementID.AddRange(userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.LeaveTypeFK == items.LeaveTypeID && x.IsActive == true).Select(x => x.UserEntitlementID).ToList());
                    }
                }
                else
                {
                    IsMonthly = leaveTypeBLL.IsMonthlyLeaveType(UserEntitlementsSearchDTO.LeaveTypeID.Value);
                    if (IsMonthly)
                        UserEntitlementID = userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.LeaveTypeFK == UserEntitlementsSearchDTO.LeaveTypeID).Select(x => x.UserEntitlementID).ToList();
                    else
                        UserEntitlementID = userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.LeaveTypeFK == UserEntitlementsSearchDTO.LeaveTypeID && x.IsActive == true).Select(x => x.UserEntitlementID).ToList();
                }
                

                if (UserEntitlementID.Count == 0)
                    return UserEntitlementChangesResult;
            }

            if (UserEntitlementsSearchDTO.From != "" && UserEntitlementsSearchDTO.To == "" && UserEntitlementsSearchDTO.LeaveTypeID == 0)
            {
                CreationFrom = DateTime.ParseExact(UserEntitlementsSearchDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID &&  x.CreationDate.Value.CompareTo(CreationFrom) >= 0).ToList();
            }
            else if (UserEntitlementsSearchDTO.From == "" && UserEntitlementsSearchDTO.To != "" && UserEntitlementsSearchDTO.LeaveTypeID == 0)
            {
                CreationTo = DateTime.ParseExact(UserEntitlementsSearchDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.CreationDate.Value.CompareTo(CreationTo) <= 0).ToList();
            }
            else if (UserEntitlementsSearchDTO.From == "" && UserEntitlementsSearchDTO.To == "" && UserEntitlementsSearchDTO.LeaveTypeID != 0)
            {
                //var UserEntitlementID = userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.LeaveTypeFK == UserEntitlementsSearchDTO.LeaveTypeID && x.IsActive == true).FirstOrDefault().UserEntitlementID;
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && UserEntitlementID.Contains(x.UserEntitlementFK.Value)).ToList();
            }
            else if (UserEntitlementsSearchDTO.From != "" && UserEntitlementsSearchDTO.To != "" && UserEntitlementsSearchDTO.LeaveTypeID != 0)
            {
                CreationFrom = DateTime.ParseExact(UserEntitlementsSearchDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(UserEntitlementsSearchDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                //var UserEntitlementID = userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.LeaveTypeFK == UserEntitlementsSearchDTO.LeaveTypeID && x.IsActive == true).FirstOrDefault()?.UserEntitlementID;
                UserEntitlementChanges = UserEntitlementID.Count != 0 ? userEntitlementChangesBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID &&  UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).ToList() : new List<UserEntitlementChange>();
            }
            else if (UserEntitlementsSearchDTO.From != "" && UserEntitlementsSearchDTO.To != "")
            {
                CreationFrom = DateTime.ParseExact(UserEntitlementsSearchDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(UserEntitlementsSearchDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                //var EntitlementID = userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.IsActive == true).FirstOrDefault()?.UserEntitlementID;
                UserEntitlementChanges = UserEntitlementID.Count != 0 ? userEntitlementChangesBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).ToList() : new List<UserEntitlementChange>();
            }
            else if (UserEntitlementsSearchDTO.From == "" && UserEntitlementsSearchDTO.To == "" && UserEntitlementsSearchDTO.LeaveTypeID == 0)
            {
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID).ToList();
            }
            UserEntitlementChanges= UserEntitlementChanges.OrderByDescending(ue => ue.CreationDate).ToList();
            foreach (var items in UserEntitlementChanges)
            {
                var LeaveTypeID = userEntitlementBLL.Find(x => x.UserFK == UserEntitlementsSearchDTO.UserID && x.UserEntitlementID == items.UserEntitlementFK).FirstOrDefault().LeaveTypeFK;
                UserEntitlementChangesResult.Add(new UserEntitlementChangesSearchResultDTO {
                    UserEntitlementChangeID = items.UserEntitlementChangesID,
                    EntitlementChangedBy = entitlementChangedByDIMBLL.Find(x => x.EntitlementChangedByDIMID == items.EntitlementChangedByFK && x.IsActive == true).FirstOrDefault().EntitlementChangedByName,
                    Request = items.RequestFK != null ? items.RequestFK.Value.ToString() : "None",
                    UserChangeEntitlement = items.UserChangeEntitlementFK != null ? userBLL.Find(x => x.UserID == items.UserChangeEntitlementFK && x.IsActive == true).FirstOrDefault().UserName : "None",
                    LeaveTypeName = leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveTypeID && x.IsActive == true).FirstOrDefault().LeaveTypeName,
                    Comment = items.Comment,
                    EntitlementBefore = items.EntitlementBefore,
                    EntitlementTo = items.EntitlementAfter,
                    EntitlementBeforeTo = items.EntitlementBefore + "->" + items.EntitlementAfter,
                    RequestDuration = items.RequestDuration,
                    ActionDate = items.ActionDate
                });
            }
            return UserEntitlementChangesResult;
        }

        public List<UserEntitlementChangesSearchResultDTO> HRFilterUserEntitlementChanges(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            DateTime CreationFrom, CreationTo;
            List<UserEntitlementChange> UserEntitlementChanges = new List<UserEntitlementChange>();
            List<UserEntitlementChangesSearchResultDTO> UserEntitlementChangesResult = new List<UserEntitlementChangesSearchResultDTO>();

            TimeSpan UpdateCreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan UpdateCreationToTime = new TimeSpan(23, 59, 59);

            if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.EntitlementChangedByID != 0 && ViewRequestsFilterDTO.ListUsers.Count() != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.IsActive == true).Select(x => x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => x.EntitlementChangedByFK == ViewRequestsFilterDTO.EntitlementChangedByID && UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();
            }

            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListUsers.Count() != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.IsActive == true).Select(x=> x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();
            }

            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListDepartment.Count() != 0 && ViewRequestsFilterDTO.ListSubDepartment.Count() != 0 && ViewRequestsFilterDTO.EntitlementChangedByID != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> SubDepartmentUsers = userBLL.Find(x => ViewRequestsFilterDTO.ListSubDepartment.Contains(x.SubDepartmentFK.Value)).Select(x => x.UserID).ToList();
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => SubDepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.IsActive == true).Select(x => x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.EntitlementChangedByFK == ViewRequestsFilterDTO.EntitlementChangedByID).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();
            }

            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListDepartment.Count() != 0 && ViewRequestsFilterDTO.ListSubDepartment.Count() != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> SubDepartmentUsers = userBLL.Find(x => ViewRequestsFilterDTO.ListSubDepartment.Contains(x.SubDepartmentFK.Value)).Select(x => x.UserID).ToList();
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => SubDepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.IsActive == true).Select(x => x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();
            }

            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListDepartment.Count() != 0 && ViewRequestsFilterDTO.EntitlementChangedByID != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> DepartmentUsers = userBLL.Find(x => ViewRequestsFilterDTO.ListDepartment.Contains(x.DepartmentFK.Value)).Select(x => x.UserID).ToList();
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => DepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.IsActive == true).Select(x => x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.EntitlementChangedByFK == ViewRequestsFilterDTO.EntitlementChangedByID).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();
            }

            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListDepartment.Count() != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> DepartmentUsers = userBLL.Find(x => ViewRequestsFilterDTO.ListDepartment.Contains(x.DepartmentFK.Value)).Select(x => x.UserID).ToList();
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => DepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.IsActive == true).Select(x => x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();
            }

            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.EntitlementChangedByID != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.IsActive == true).Select(x => x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => x.EntitlementChangedByFK == ViewRequestsFilterDTO.EntitlementChangedByID && UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();
            }
           
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.ListLeaveType.Count != 0)
            {
                CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
                List<int> UserEntitlementID = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK)).Select(x => x.UserEntitlementID).ToList();
                UserEntitlementChanges = userEntitlementChangesBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0).OrderByDescending(ue => ue.UserEntitlementChangesID).ToList();                //UserEntitlementChanges = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.IsActive == true).ToList();
            }
            UserEntitlementChanges = UserEntitlementChanges.OrderByDescending(e => e.CreationDate).ToList();
            foreach (var items in UserEntitlementChanges)
            {

                var LeaveTypeID = userEntitlementBLL.Find(x => x.UserEntitlementID == items.UserEntitlementFK).FirstOrDefault().LeaveTypeFK;

                UserEntitlementChangesResult.Add(new UserEntitlementChangesSearchResultDTO
                {
                    UserEntitlementChangeID = items.UserEntitlementChangesID,
                    EntitlementChangedBy = entitlementChangedByDIMBLL.Find(x => x.EntitlementChangedByDIMID == items.EntitlementChangedByFK && x.IsActive == true).FirstOrDefault().EntitlementChangedByName,
                    UserChangeEntitlement = items.UserChangeEntitlementFK != null ? userBLL.Find(x => x.UserID == items.UserChangeEntitlementFK && x.IsActive == true).FirstOrDefault().UserName : "None",
                    LeaveTypeName = leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveTypeID && x.IsActive == true).FirstOrDefault().LeaveTypeName,
                    UserName = userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault().UserName,
                    AccessControlID = userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault()?.AccessControlUserFK,
                    //AccessControlID = userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault().AccessControlUserFK != null ? userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault().AccessControlUserFK.Value.ToString() : "",
                    Comment = items.Comment,
                    EntitlementBefore = items.EntitlementBefore,
                    EntitlementTo = items.EntitlementAfter,
                    Request = items.RequestFK != null ? items.RequestFK.Value.ToString() : "None",
                    RequestDuration = items.RequestDuration,
                    ActionDate = items.ActionDate
                });
            }

            return UserEntitlementChangesResult;
        }

        public List<UserEntitlementSearchResultDTO> HRFilterUserEntitlement(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            //DateTime CreationFrom, CreationTo;
            List<UserEntitlement> UserEntitlement = new List<UserEntitlement>();
            List<UserEntitlementSearchResultDTO> UserEntitlementResult = new List<UserEntitlementSearchResultDTO>();

            TimeSpan UpdateCreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan UpdateCreationToTime = new TimeSpan(23, 59, 59);

            if (ViewRequestsFilterDTO.Year != 0 && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListUsers.Count() != 0)
            {
                if(ViewRequestsFilterDTO.Month != null)
                {
                    foreach (var items in ViewRequestsFilterDTO.ListLeaveType)
                    {
                        bool IsMonthly = leaveTypeBLL.IsMonthlyLeaveType(items);

                        if (IsMonthly)
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month).OrderByDescending(x => x.CreationDate).ToList());
                        else
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year).OrderByDescending(x => x.CreationDate).ToList());
                    }

                    //UserEntitlement = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month && x.IsActive == true).OrderByDescending(x => x.CreationDate).ToList();
                }
                else
                {
                    UserEntitlement = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.IsActive == true).OrderByDescending(x => x.CreationDate).ToList();
                }
                //UserEntitlementChanges = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.IsActive == true).ToList();
            }

            else if (ViewRequestsFilterDTO.Year != 0 && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListDepartment.Count() != 0)
            {
                List<int> DepartmentUsers = userBLL.Find(x => ViewRequestsFilterDTO.ListDepartment.Contains(x.DepartmentFK.Value)).Select(x => x.UserID).ToList();
                if (ViewRequestsFilterDTO.Month != null)
                {
                    foreach (var items in ViewRequestsFilterDTO.ListLeaveType)
                    {
                        bool IsMonthly = leaveTypeBLL.IsMonthlyLeaveType(items);

                        if (IsMonthly)
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => DepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month).OrderByDescending(x => x.CreationDate).ToList());
                        else
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => DepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year).OrderByDescending(x => x.CreationDate).ToList());
                    }

                    //UserEntitlement = userEntitlementBLL.Find(x => DepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month && x.IsActive == true).OrderByDescending(x => x.CreationDate).ToList();
                }
                else
                {
                    UserEntitlement = userEntitlementBLL.Find(x => DepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year).OrderByDescending(x => x.CreationDate).ToList();
                }
                //UserEntitlementChanges = userEntitlementBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.IsActive == true).ToList();
            }

            else if (ViewRequestsFilterDTO.Year != 0 && ViewRequestsFilterDTO.ListLeaveType.Count != 0 && ViewRequestsFilterDTO.ListDepartment.Count() != 0 && ViewRequestsFilterDTO.ListSubDepartment.Count() != 0)
            {
                List<int> SubDepartmentUsers = userBLL.Find(x => ViewRequestsFilterDTO.ListSubDepartment.Contains(x.SubDepartmentFK.Value)).Select(x => x.UserID).ToList();
                if (ViewRequestsFilterDTO.Month != null)
                {
                    foreach (var items in ViewRequestsFilterDTO.ListLeaveType)
                    {
                        bool IsMonthly = leaveTypeBLL.IsMonthlyLeaveType(items);

                        if (IsMonthly)
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => SubDepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month).OrderByDescending(x => x.CreationDate).ToList());
                        else
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => SubDepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year).OrderByDescending(x => x.CreationDate).ToList());
                    }
                    //UserEntitlement = userEntitlementBLL.Find(x => SubDepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month && x.IsActive == true).OrderByDescending(x => x.CreationDate).ToList();
                }
                else
                {
                    UserEntitlement = userEntitlementBLL.Find(x => SubDepartmentUsers.Contains(x.UserFK) && ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year).OrderByDescending(x => x.CreationDate).ToList();
                }
                //UserEntitlementChanges = userEntitlementChangesBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.IsActive == true).ToList();
            }

            else if (ViewRequestsFilterDTO.Year != 0 && ViewRequestsFilterDTO.ListLeaveType.Count != 0)
            {
                if (ViewRequestsFilterDTO.Month != null)
                {
                    foreach (var items in ViewRequestsFilterDTO.ListLeaveType)
                    {
                        bool IsMonthly = leaveTypeBLL.IsMonthlyLeaveType(items);

                        if (IsMonthly)
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month).OrderByDescending(x => x.CreationDate).ToList());
                        else
                            UserEntitlement.AddRange(userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year).OrderByDescending(x => x.CreationDate).ToList());
                    }
                    //UserEntitlement = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year && x.Month == ViewRequestsFilterDTO.Month && x.IsActive == true).OrderByDescending(x => x.CreationDate).ToList();
                }
                else
                {
                    UserEntitlement = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListLeaveType.Contains(x.LeaveTypeFK) && x.Year == ViewRequestsFilterDTO.Year).OrderByDescending(x => x.CreationDate).ToList();
                }
                //UserEntitlementChanges = userEntitlementBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains(x.UserFK) && UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.IsActive == true).ToList();
            }

            //else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "" && ViewRequestsFilterDTO.LeaveTypeID != null && ViewRequestsFilterDTO.ListDepartment.Count() != 0 && ViewRequestsFilterDTO.ListUsers.Count() != 0)
            //{
            //    CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
            //    CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;
            //    List<int> DepartmentUsers = userBLL.Find(x => ViewRequestsFilterDTO.ListDepartment.Contains(x.DepartmentFK.Value)).Select(x => x.UserID).ToList();
            //    UserEntitlement = userEntitlementBLL.Find(x => DepartmentUsers.Contains(x.UserFK) && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.IsActive == true).ToList();
            //    //UserEntitlementChanges = userEntitlementBLL.Find(x => UserEntitlementID.Contains(x.UserEntitlementFK.Value) && x.CreationDate.Value.CompareTo(CreationFrom) >= 0 && x.CreationDate.Value.CompareTo(CreationTo) <= 0 && x.IsActive == true).ToList();
            //}

            foreach (var items in UserEntitlement)
            {
                var LeaveTypeID = userEntitlementBLL.Find(x => x.UserEntitlementID == items.UserEntitlementID).FirstOrDefault().LeaveTypeFK;

                UserEntitlementResult.Add(new UserEntitlementSearchResultDTO
                {
                    UserEntitlementID = items.UserEntitlementID,
                    EntitlementTotal = items.EntitlementTotal,
                    Year = items.Year.Value.ToString(),
                    Month = items.Month != null ? items.Month.Value.ToString() : "---",
                    LeaveTypeName = leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveTypeID && x.IsActive == true).FirstOrDefault().LeaveTypeName,
                    UserName = userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault().UserName,
                    AccessControlID = userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault()?.AccessControlUserFK
                //AccessControlID = userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault().AccessControlUserFK != null ? userBLL.Find(x => x.UserID == items.UserFK).FirstOrDefault().AccessControlUserFK.Value.ToString() : "",

            });
            }

            return UserEntitlementResult;
        }

        public List<int> GetAllEntiltementMonths(int LeaveTypeID)
        {
            List<int> Months = new List<int>();
            Months = userEntitlementBLL.Find(x => x.Month != null).OrderByDescending(x => x.Month.Value).Select(x => x.Month.Value).Distinct().ToList();
            return Months;
        }

        public List<int> GetAllEntiltementMonths(int LeaveTypeID, int UserID, int Year)
        {
            List<int> Months = new List<int>();
            Months = userEntitlementBLL.Find(x => x.Month != null && x.LeaveTypeFK == LeaveTypeID && x.UserFK == UserID && x.Year == Year).OrderByDescending(x => x.Month.Value).Select(x => x.Month.Value).Distinct().ToList();
            return Months;
        }

        public decimal GetMonthlyEntitlementQuantity(int LeaveTypeID, int UserID, int Year,int Month)
        {
            if (leaveTypeBLL.IsMonthlyLeaveType(LeaveTypeID))
            {
                decimal entitlementQuantity = userEntitlementBLL.Find(x => x.LeaveTypeFK == LeaveTypeID && x.UserFK == UserID && x.Year == Year && x.Month == Month).Select(x => x.EntitlementTotal).FirstOrDefault();
                return entitlementQuantity;
            }
            return 0;
        }

    }
}
