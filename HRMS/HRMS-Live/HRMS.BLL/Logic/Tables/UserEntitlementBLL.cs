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
using System.Reflection;
using HRMS.Utilities;

namespace HRMS.BLL.Logic.Tables
{
    public class UserEntitlementBLL : Repository<UserEntitlement>
    {
        UserEntitlementChangesBLL userEntitlementChangesBLL;
        //UserBLL userBLL;
        //LeaveTypeBLL leaveTypeBLL;
        DateTime creationDate;
        
        public UserEntitlementBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {

            userEntitlementChangesBLL = new UserEntitlementChangesBLL(Context, CreationDate);
            //userBLL = new UserBLL(Context, creationDate);
            //leaveTypeBLL = new LeaveTypeBLL(Context, creationDate);
            creationDate = CreationDate;

        }
        
        public List<int> GetAllEntiltementYears()
        {
            List<int> Years = new List<int>();
            Years = GetAll().Select(x => x.Year.Value).Distinct().ToList();
            return Years;
        }

        public List<int> GetEntiltementYears(int LeaveTypeID, int UserID)
        {
            List<int> Years = new List<int>();
            //Years = Find(x => x.LeaveTypeFK == LeaveTypeID && x.UserFK == UserID)
            //    .ToList().OrderByDescending(x => x.Year.Value).Distinct().Select(x=>(int)x.Year).ToList();
            Years = Find(x => x.LeaveTypeFK == LeaveTypeID && x.UserFK == UserID && x.Year != null)
                .OrderByDescending(x => x.Year.Value).Select(x=> x.Year.Value).Distinct().ToList();
            return Years;
        }

        public List<UserEntitlementsDTO> GetUserEntitlementByID(int UserID)
        {
            List<UserEntitlementsDTO> ListEntitlement = new List<UserEntitlementsDTO>();
                
            foreach(var item in Find(x => x.UserFK == UserID && x.IsActive == true).ToList())
            {
                ListEntitlement.Add(new UserEntitlementsDTO
                {
                    UserFK = item.UserFK,
                    EntitlementModifiedQty = item.EntitlementTotal,
                    LeaveTypeFK = item.LeaveTypeFK,
                    UserEntitlementID = item.UserEntitlementID,
                    LeaveDurationUnitFK = item.LeaveDurationUnitFK,
                    DurationUnit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString()
                });
            }

            return ListEntitlement;
        }
        public void RequestChangeEntitlmentOfUser(RequestEntitlmentDTO NewEntitlmentDTO)
        {
            UserEntitlement EditUserEntitlement = Find(x => x.LeaveTypeFK == NewEntitlmentDTO.LeaveTypeID && x.UserFK == NewEntitlmentDTO.UserID && x.IsActive == true).FirstOrDefault();
            var OldEditUserEntitlement =  XMLObjectConverter.Serialize(EditUserEntitlement);
            decimal OldEntitlementTotal = EditUserEntitlement.EntitlementTotal;
            
            EditUserEntitlement.EntitlementTotal = EditUserEntitlement.EntitlementTotal - NewEntitlmentDTO.RequestDuration;

            Edit(EditUserEntitlement);
            Logger.LogCypressEvent(NewEntitlmentDTO.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)NewEntitlmentDTO.ModifiedByUserId, OldEditUserEntitlement, XMLObjectConverter.Serialize(EditUserEntitlement), "Edit User Entitlement");

            userEntitlementChangesBLL.Add(new UserEntitlementChange
            {
                UserEntitlementFK = EditUserEntitlement.UserEntitlementID,
                Comment = "Approved Request"+" "+ NewEntitlmentDTO.LeaveTypeName,
                CreationDate = creationDate,
                LeaveDurationUnitFK = NewEntitlmentDTO.UnitID,
                IsActive = true,
                EntitlementBefore = OldEntitlementTotal,
                EntitlementAfter = EditUserEntitlement.EntitlementTotal,
                EntitlementChangedByFK = (int)EntitlementChangedBy.Request,
                ActionDate =creationDate,
                RequestFK = NewEntitlmentDTO.RequestID,
                RequestDuration = NewEntitlmentDTO.RequestDuration,
                IsDeleted = false,
                DurationFrom = NewEntitlmentDTO.DurationFrom,
                DurationTo = NewEntitlmentDTO.DurationTo,
                BackToWork = NewEntitlmentDTO. RequestBackToworkDate,
                UserFK= NewEntitlmentDTO.UserID,
                Year=creationDate.Year                  




            });

        }

        //public void ChangeQuantityOfUserEntitlement(UserEntitlementsDTO UserEntitlementsDTO)
        //{
        //    UserEntitlement UserEntitlementObj = Find(x => x.LeaveTypeFK == UserEntitlementsDTO.LeaveTypeFK && x.UserFK == UserEntitlementsDTO.UserFK).SingleOrDefault();
        //    var OldEntitlementTotal = UserEntitlementObj.EntitlementTotal;

        //    UserEntitlementObj.ModificationDate = UserEntitlementsDTO.ModificationDate;
        //    UserEntitlementObj.EntitlementTotal = UserEntitlementsDTO.ISAddition == true ? UserEntitlementObj.EntitlementTotal + UserEntitlementsDTO.EntitlementModifiedQty : UserEntitlementObj.EntitlementTotal - UserEntitlementsDTO.EntitlementModifiedQty;
        //    Edit(UserEntitlementObj);

        //    LeaveType LeaveTypeObj = leaveTypeBLL.Find(X => X.LeaveTypeID == UserEntitlementsDTO.LeaveTypeFK && X.IsActive == true).SingleOrDefault();

        //    User UserObj = userBLL.Find(x => x.UserID == UserEntitlementsDTO.ModifiedBy).SingleOrDefault();

        //    int ChangedBy;
        //    if (UserObj.IsAdmin == true)
        //        ChangedBy = (int)StaticEnums.EntitlementChangedBy.Admin;
        //    else 
        //        ChangedBy = (int)StaticEnums.EntitlementChangedBy.HR;

        //    userEntitlementChangesBLL.Add(new UserEntitlementChange
        //    {
        //        UserEntitlementFK = UserEntitlementObj.UserEntitlementID,
        //        Comment = UserEntitlementsDTO.Comment,
        //        CreationDate = creationDate,
        //        LeaveDurationUnitFK = LeaveTypeObj.DurationUnitFK.Value,
        //        IsActive = true,
        //        EntitlementBefore = OldEntitlementTotal,
        //        EntitlementAfter = UserEntitlementObj.EntitlementTotal,
        //        EntitlementChangedByFK = ChangedBy,
        //        ActionDate = creationDate,
        //        IsDeleted = false,
        //        UserFK = UserEntitlementObj.UserFK,
        //        UserChangeEntitlementFK = UserEntitlementsDTO.ModifiedBy
        //    });

    

        //}

        public bool AddEntitlmentForLeaveTypeIfNotExist(UserEntitlement userEntitlement)
        {
            try
            {
                Add(userEntitlement);
                
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                return false;
            }
        }



    }
    
}
