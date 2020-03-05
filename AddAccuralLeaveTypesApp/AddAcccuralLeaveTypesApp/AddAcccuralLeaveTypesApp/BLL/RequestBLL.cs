using AddAcccuralLeaveTypesApp.DTO;
using AddAcccuralLeaveTypesApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AddAcccuralLeaveTypesApp.BLL.Enums.StaticEnums;

namespace AddAcccuralLeaveTypesApp.BLL
{
    public class RequestBLL
    {
        HRMSEntities Context;
        DateTime CreationDate;
        public RequestBLL(HRMSEntities Context, DateTime CreationDate)
        {
            this.Context = Context;
            this.CreationDate = CreationDate;
        }
        ~RequestBLL()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }
        public void AddNewRequestDetails(RequestDetailsDTO RequestDetail)
        {
            Context.RequestDetails.Add(new RequestDetail
            {
                CreationDate = RequestDetail.CreationDate,
                IsActive = true,
                IsDeleted = false,
                RequestFK = RequestDetail.RequestID,
                RequestDetailsTypeID = RequestDetail.RequestDetailsTypeID,
                RequestDetailsComment = RequestDetail.RequestDetailsComment,
                RequestDetailsDate = (DateTime)RequestDetail.CreationDate,
                UserFK = RequestDetail.UserID,
                
            });
        }

        public void RequestChangeEntitlmentOfUser(RequestEntitlmentDTO NewEntitlmentDTO)
        {
            UserEntitlement EditUserEntitlement = Context.UserEntitlements.Where(x => x.LeaveTypeFK == NewEntitlmentDTO.LeaveTypeID && x.UserFK == NewEntitlmentDTO.UserID && x.IsActive == true).FirstOrDefault();
            //var OldEditUserEntitlement = XMLObjectConverter.Serialize(EditUserEntitlement);
            decimal OldEntitlementTotal = EditUserEntitlement.EntitlementTotal;

            EditUserEntitlement.EntitlementTotal = EditUserEntitlement.EntitlementTotal - NewEntitlmentDTO.RequestDuration;

            Context.UserEntitlements.AddOrUpdate(EditUserEntitlement);
            //Logger.LogCypressEvent(NewEntitlmentDTO.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)NewEntitlmentDTO.ModifiedByUserId, OldEditUserEntitlement, XMLObjectConverter.Serialize(EditUserEntitlement), "Edit User Entitlement");

            Context.UserEntitlementChanges.Add(new UserEntitlementChange
            {
                UserEntitlementFK = EditUserEntitlement.UserEntitlementID,
                Comment = "System Approved Request" + " " + NewEntitlmentDTO.LeaveTypeName,
                CreationDate = CreationDate,
                LeaveDurationUnitFK = NewEntitlmentDTO.UnitID,
                IsActive = true,
                EntitlementBefore = OldEntitlementTotal,
                EntitlementAfter = EditUserEntitlement.EntitlementTotal,
                EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                ActionDate = CreationDate,
                RequestFK = NewEntitlmentDTO.RequestID,
                RequestDuration = NewEntitlmentDTO.RequestDuration,
                IsDeleted = false,
                DurationFrom = NewEntitlmentDTO.DurationFrom,
                DurationTo = NewEntitlmentDTO.DurationTo,
                BackToWork = NewEntitlmentDTO.RequestBackToworkDate,
                UserFK = NewEntitlmentDTO.UserID,
                Year = CreationDate.Year
            });

        }

        public List<ApprovedBySystemDTO> ApprovedBySystem(int LeaveTypeID)
        {
            List<Request> Requests = Context.Requests.Where(x => x.IsActive == true
                      && x.RequesStatusFK == (int)RequestStatus.Pending
                      && x.IsDeleted == false && x.LeaveTypeFK == LeaveTypeID
                   ).ToList();

            List<ApprovedBySystemDTO> RequestApprovedBySystem = new List<ApprovedBySystemDTO>();

            foreach (var item in Requests)
            {
                try
                {
                    LeaveType leavetype = Context.LeaveTypes.Where(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault();
                    LeaveTypeAccuralRule leaveTypeAccuralRule = Context.LeaveTypeAccuralRules.Where(x => x.LeaveTypeFK == item.LeaveTypeFK).FirstOrDefault();
                    LeaveTypeAccuralRule ParentleaveTypeAccuralRule = Context.LeaveTypeAccuralRules.Where(x => x.LeaveTypeFK == leavetype.ParentLeaveTypeFK).FirstOrDefault();
                    List<ApprovalFlowRequestDetail> ApprovalFlowDetailRequests = Context.ApprovalFlowRequestDetails.Where(x => x.RequestFK == item.RequestID).ToList();
                    DateTime DurationFrom = item.DurationFrom;
                    DateTime DurationTo = item.DurationTo;
                    DateTime DurationFromDate = item.DurationFrom.Date;
                    DateTime DurationToDate = item.DurationTo.Date;
                    decimal? TotalLeaveTypeEntitlement;
                    bool ISMonthlyUnlimitedEntitlementType = false;

                    if (leavetype.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Accured
                      && (leaveTypeAccuralRule.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.RepeatEveryMonthAt
                      || ParentleaveTypeAccuralRule.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.RepeatEveryMonthAt
                      ))
                    {
                        ISMonthlyUnlimitedEntitlementType = true;
                    }


                    if (ISMonthlyUnlimitedEntitlementType)
                    {


                        if (leavetype.ParentLeaveTypeFK != null)
                        {
                            TotalLeaveTypeEntitlement =
                            Context.UserEntitlements.Where(x =>
                               x.UserFK == item.UserFK
                            && x.LeaveTypeFK == leavetype.ParentLeaveTypeFK
                            && x.IsActive == true
                            && x.IsDeleted == false
                            && x.Year == DurationFrom.Year
                            && x.Year == DurationTo.Year)?.FirstOrDefault()?.EntitlementTotal;
                        }
                        else
                        {
                            TotalLeaveTypeEntitlement = Context.UserEntitlements.Where(x =>
                               x.UserFK == item.UserFK
                            && x.LeaveTypeFK == leavetype.LeaveTypeID
                            && x.IsActive == true
                            && x.IsDeleted == false
                            && x.Year == DurationFrom.Year
                            && x.Year == DurationTo.Year)?.FirstOrDefault()?.EntitlementTotal;
                        }


                        //System Auto Approve Request
                        if (TotalLeaveTypeEntitlement >= item.RequestDuration)
                        {
                            item.RequesStatusFK = (int)RequestStatus.SystemApproved;
                            item.Order = ApprovalFlowDetailRequests.Last().Order;
                            Context.Requests.AddOrUpdate(item);

                            foreach (var ApprovalFlowDetailRequestsItems in ApprovalFlowDetailRequests)
                            {
                                ApprovalFlowDetailRequestsItems.IsActive = false;
                                ApprovalFlowDetailRequestsItems.RequestActionFK = (int)ActionType.SystemApprove;
                                Context.ApprovalFlowRequestDetails.AddOrUpdate(ApprovalFlowDetailRequestsItems);
                            }

                            //Change Entitlment Of User
                            if (leavetype.ParentLeaveTypeFK != null)
                            {
                                if (leavetype.TakeMaxAmountFromParent == null)
                                {
                                    RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                                    {
                                        UserID = (int)item.UserFK,
                                        RequestID = item.RequestID,
                                        DurationTo = item.DurationTo,
                                        DurationFrom = item.DurationFrom,
                                        LeaveTypeID = leavetype.ParentLeaveTypeFK,
                                        RequestDuration = (decimal)(leavetype.DeductionFromParentLeaveDurationPercentage / 100) * item.RequestDuration,
                                        UnitID = (int)item.LeaveDurationUnitFK,
                                        RequestBackToworkDate = item.BackToWork,
                                        ModifiedByUserId = 0,// ManagerActionDTO.ModifiedByUserId,
                                        LeaveTypeName = Context.LeaveTypes.Where(p => p.LeaveTypeID == leavetype.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                                        TotalOffDays = item.RequestDuration

                                    };
                                    RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);


                                }
                                else
                                {
                                    LeaveType ParentLeaveType = Context.LeaveTypes.Where(x => x.LeaveTypeID == leavetype.ParentLeaveTypeFK).FirstOrDefault();
                                    decimal RequestDuration = (decimal)(leavetype.DeductionFromParentLeaveDurationPercentage / 100) * item.RequestDuration;
                                    if (Context.UserEntitlements.Where(x => x.LeaveTypeFK == item.LeaveTypeFK).Count() > 0)
                                    {
                                        RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                                        {
                                            UserID = (int)item.UserFK,
                                            RequestID = item.RequestID,
                                            DurationTo = item.DurationTo,
                                            DurationFrom = item.DurationFrom,
                                            LeaveTypeID = leavetype.ParentLeaveTypeFK,
                                            RequestDuration = RequestDuration,
                                            UnitID = (int)item.LeaveDurationUnitFK,
                                            RequestBackToworkDate = item.BackToWork,
                                            //ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                                        };
                                        RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);
                                        RequestEntitlmentDTO RequestParentEntitlmentDTO = new RequestEntitlmentDTO
                                        {
                                            UserID = (int)item.UserFK,
                                            RequestID = item.RequestID,
                                            DurationTo = item.DurationTo,
                                            DurationFrom = item.DurationFrom,
                                            LeaveTypeID = item.LeaveTypeFK,
                                            RequestDuration = item.RequestDuration,
                                            UnitID = (int)item.LeaveDurationUnitFK,
                                            RequestBackToworkDate = item.BackToWork,
                                            // ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                                        };
                                        RequestChangeEntitlmentOfUser(RequestParentEntitlmentDTO);
                                    }

                                    else if (Context.UserEntitlements.Where(x => x.LeaveTypeFK == item.LeaveTypeFK).Count() == 0
                                        && leavetype.TakeMaxAmountFromParent == null && leaveTypeAccuralRule == null)
                                    {
                                        RequestEntitlmentDTO RequestParentEntitlmentDTO = new RequestEntitlmentDTO
                                        {
                                            UserID = (int)item.UserFK,
                                            RequestID = item.RequestID,
                                            DurationTo = item.DurationTo,
                                            DurationFrom = item.DurationFrom,
                                            LeaveTypeID = ParentLeaveType.LeaveTypeID,
                                            RequestDuration = item.RequestDuration,
                                            UnitID = (int)item.LeaveDurationUnitFK,
                                            RequestBackToworkDate = item.BackToWork,
                                            //  ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                                        };
                                        RequestChangeEntitlmentOfUser(RequestParentEntitlmentDTO);

                                    }
                                }
                            }
                            else
                            {

                                decimal RequestDuration = item.RequestDuration;
                                RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                                {
                                    UserID = (int)item.UserFK,
                                    RequestID = item.RequestID,
                                    DurationTo = item.DurationTo,
                                    DurationFrom = item.DurationFrom,
                                    LeaveTypeID = item.LeaveTypeFK,
                                    RequestDuration = RequestDuration,
                                    UnitID = (int)item.LeaveDurationUnitFK,
                                    RequestBackToworkDate = item.BackToWork,
                                    // ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                                };

                                RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);

                            }

                            //Add new Request Details
                            RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO()
                            {
                                RequestID = item.RequestID,
                                CreationDate = CreationDate,
                                RequestDetailsComment = "System Approved Request",
                                RequestDetailsTypeID = (int)RequestDetailsTypes.SystemApproveRequest,
                                RequestDetailsTypeName = RequestDetailsTypes.SystemApproveRequest.ToString(),
                                // UserID = UserManager.UserID,

                            };


                            AddNewRequestDetails(RequestDetailsDTO);


                        foreach( var handleritem in   Context.RequestHandlers.Where(x => x.RequestFK == item.RequestID).ToList())
                            {
                                handleritem.RquestStatusFK =(int) RequestStatus.SystemApproved;
                            }
                        }
                        //System Auto Reject Request
                        else
                        {
                            item.RequesStatusFK = (int)RequestStatus.SystemRejected;
                            Context.Requests.AddOrUpdate(item);
                            foreach (var ApprovalFlowDetailRequestsItems in ApprovalFlowDetailRequests)
                            {
                                ApprovalFlowDetailRequestsItems.IsActive = false;
                                ApprovalFlowDetailRequestsItems.RequestActionFK = (int)ActionType.SystemReject;
                                Context.ApprovalFlowRequestDetails.AddOrUpdate(ApprovalFlowDetailRequestsItems);
                            }
                            //Add new Request Details

                            RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO()
                            {
                                RequestID = item.RequestID,
                                CreationDate = CreationDate,
                                RequestDetailsComment = "System Rejected Request",
                                RequestDetailsTypeID = (int)RequestDetailsTypes.SystemRejectRequest,
                                RequestDetailsTypeName = RequestDetailsTypes.SystemRejectRequest.ToString(),
                                //UserID = UserManager.UserID
                                 

                            };

                            AddNewRequestDetails(RequestDetailsDTO);

                            RequestDetailsDTO RequestDetailsRejetionReason = new RequestDetailsDTO()
                            {
                                RequestID = item.RequestID,
                                CreationDate = CreationDate,
                                RequestDetailsComment = "No enough Entitlement To Approve This Request",
                                RequestDetailsTypeID = (int)RequestDetailsTypes.SystemRejectionReason,
                                RequestDetailsTypeName = RequestDetailsTypes.SystemRejectionReason.ToString(),
                                //UserID = UserManager.UserID


                            };

                            AddNewRequestDetails(RequestDetailsRejetionReason);

                            foreach (var handleritem in Context.RequestHandlers.Where(x => x.RequestFK == item.RequestID).ToList())
                            {
                                handleritem.RquestStatusFK = (int)RequestStatus.SystemRejected;
                            }


                        }

                        RequestApprovedBySystem.Add(new ApprovedBySystemDTO
                        {
                            EmployeeID = (int)item.UserFK,
                            EmployeeName = Context.Users.Where(x => x.UserID == item.UserFK).Select(x => x.UserName).FirstOrDefault(),
                            AccessControlID = Context.Users.Where(x => x.UserID == item.UserFK).FirstOrDefault().AccessControlUserFK != null ? Context.Users.Where(x => x.UserID == item.UserFK).FirstOrDefault().AccessControlUserFK.ToString() : "",
                            RequestID = item.RequestID,
                            RequestStatusName = Context.RequestStatus.Where(x => x.RequestStatusID == item.RequesStatusFK.Value).FirstOrDefault().RequestStatusName
                        });


                      
                    }
                }
                catch (Exception ex)
                {
                    RequestApprovedBySystem.Add(new ApprovedBySystemDTO
                    {
                        EmployeeID = (int)item.UserFK,
                        EmployeeName = Context.Users.Where(x => x.UserID == item.UserFK).Select(x => x.UserName).FirstOrDefault(),
                        AccessControlID = Context.Users.Where(x => x.UserID == item.UserFK).FirstOrDefault().AccessControlUserFK != null ? Context.Users.Where(x => x.UserID == item.UserFK).FirstOrDefault().AccessControlUserFK.ToString() : "",
                        RequestID = item.RequestID,
                        RequestStatusName = Context.RequestStatus.Where(x => x.RequestStatusID == item.RequesStatusFK.Value).FirstOrDefault().RequestStatusName,//((RequestStatus)Enum.Parse(typeof(RequestStatus), item.RequesStatusFK.ToString())).ToString(),
                        HasException = true,
                        Exception = ex.InnerException.ToString()
                    });

                }

            }
            Context.SaveChanges();
            return RequestApprovedBySystem;

        }


    }
}
