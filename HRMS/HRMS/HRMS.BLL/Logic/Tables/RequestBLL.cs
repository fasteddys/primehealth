using HRMS.BLL.StaticData;
using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;
using HRMS.Utilities;
using HRMS.Utilities.DTO;
using System.IO;
using System.Reflection;
using HRMS.BLL.Logic.Shared_Logic;
using System.Text.RegularExpressions;

namespace HRMS.BLL.Logic.Tables
{
    public class RequestBLL : Repository<Request>
    {
        ApprovalFlowBLL approvalFlowBLL;
        ManagerBLL managerBLL;
        UserBLL userBLL;
        SubDepartmentBLL subDepartmentBLL;
        ApprovalFlowRequestDetailsBLL approvalFlowRequestDetailsBLL;
        UserEntitlementBLL userEntitlementBLL;
        RequestDetailsBLL requestDetailsBLL;
        ApprovalFlowDetailsBLL approvalFlowDetailsBLL;
        DepartmentBLL departmentBLL;
        LeaveTypeBLL leaveTypeBLL;
        PositionBLL PositionBLL;
        ConfigurationBLL configurationBLL;
        DateTime creationDate;
        RequestAttachmentBLL requestAttachmentBLL;
        OfficialHolidayBLL officialHolidayBLL;
        LeaveTypeAccuralRuleBLL leaveTypeAccuralRuleBLL;
        LeaveTypeCarryOverRuleBLL leaveTypeCarryOverRuleBLL;
        LeaveTypePartialDurationBLL leaveTypePartialDurationBLL;
        LeaveTypeRestrictedContractTypeBLL leaveTypeRestrictedContractTypeBLL;
        RequestHandlerBLL requestHandlerBLL;
        LeaveTypeRestrictionBLL leaveTypeRestrictionBLL;
       // CompanyOfficialHolidayBLL companyOfficialHolidayBLL;
        RequestHoursHandlerBLL requestHoursHandlerBLL;
        CompanyBLL companyBLL;
        LeaveTypeDurationUnitDIMBLL leaveTypeDurationUnitDIMBLL;
        UserEntitlementChangesBLL userEntitlementChangesBLL;
        RequestHistoryBLL requestHistoryBLL;
        LeaveTypeMinOneDayDurationDIMBLL leaveTypeMinOneDayDurationBLL;
        RequestStatusBLL requestStatusBLL;
        WorkingWeekBLL workingWeekBLL;
        WorkingWeekDetailsBLL workingWeekDetailsBLL;
        WeekDaysBLL weekDaysBLL;
        WorkingShiftUserDailyBLL workingShiftUserDailyBLL;

        public RequestBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
           
            approvalFlowBLL = new ApprovalFlowBLL(Context, CreationDate);
            managerBLL = new ManagerBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, CreationDate);
            subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
            approvalFlowRequestDetailsBLL = new ApprovalFlowRequestDetailsBLL(Context, CreationDate);
            userEntitlementBLL = new UserEntitlementBLL(Context, CreationDate);
            departmentBLL = new DepartmentBLL(Context, CreationDate);
            leaveTypeBLL = new LeaveTypeBLL(Context, CreationDate);
            PositionBLL = new PositionBLL(Context, CreationDate);
            configurationBLL = new ConfigurationBLL(Context, CreationDate);
            requestDetailsBLL = new RequestDetailsBLL(Context, CreationDate);
            requestAttachmentBLL = new RequestAttachmentBLL(Context, CreationDate);
            officialHolidayBLL = new OfficialHolidayBLL(Context, CreationDate);
            creationDate = CreationDate;
            leaveTypeAccuralRuleBLL = new LeaveTypeAccuralRuleBLL(Context, CreationDate);
            leaveTypeCarryOverRuleBLL = new LeaveTypeCarryOverRuleBLL(Context, CreationDate);
            leaveTypePartialDurationBLL = new LeaveTypePartialDurationBLL(Context, CreationDate);
            leaveTypeRestrictedContractTypeBLL = new LeaveTypeRestrictedContractTypeBLL(Context, CreationDate);
            requestHandlerBLL = new RequestHandlerBLL(Context, CreationDate);
            leaveTypeRestrictionBLL = new LeaveTypeRestrictionBLL(Context, CreationDate);
           // companyOfficialHolidayBLL = new CompanyOfficialHolidayBLL(Context);
            companyBLL = new CompanyBLL(Context, CreationDate);
            requestHoursHandlerBLL = new RequestHoursHandlerBLL(Context, CreationDate);
            leaveTypeDurationUnitDIMBLL = new LeaveTypeDurationUnitDIMBLL(Context, CreationDate);
            approvalFlowDetailsBLL = new ApprovalFlowDetailsBLL(Context, CreationDate);
            userEntitlementChangesBLL = new UserEntitlementChangesBLL(Context, CreationDate);
            requestHistoryBLL = new RequestHistoryBLL(Context, CreationDate);
            leaveTypeMinOneDayDurationBLL = new LeaveTypeMinOneDayDurationDIMBLL(Context);
            requestStatusBLL = new RequestStatusBLL(Context, CreationDate);
            workingWeekBLL = new WorkingWeekBLL(Context, CreationDate);
            workingWeekDetailsBLL = new WorkingWeekDetailsBLL(Context, CreationDate);
            weekDaysBLL = new WeekDaysBLL(Context, CreationDate);
            workingShiftUserDailyBLL = new WorkingShiftUserDailyBLL(Context, CreationDate);
        }
        public string AddNewRequest(NewRequestDTO NewRequest, out bool IsValid, out string Messages,out MailingDTO MailingDTO ,
            out Request Request, out bool CheckMailEnabled, out bool IsAutoApprove)
        {
            Messages = "";

            try
            {
                User User = userBLL.Find(x => x.UserID == NewRequest.UserID).FirstOrDefault();
                int WorkingModeID = (int)User.WorkingModeFK;
                int WorkingWeekID = (int)User.WorkingWeekFK;
                Request = new Request();
                IsValid = true;
                IsAutoApprove = false;
                bool IsOffDay = false;
                decimal MaxAllowed;
                decimal MinAllowed;
                decimal NumberOfDaysMustBeRequestedBefor;
                decimal NumberOfUnit;
                decimal NumberOfDaysInThePastAllowed;
                CheckMailEnabled = false;
                Request.LeaveTypeFK = NewRequest.LeaveTypeID;
                Request.Order = 1;
                Request.UserFK = User.UserID;
                Request.CreationDate = creationDate;
                Request.Substitute = NewRequest.Substitute;
                Request.Comment = NewRequest.Comment;
                Request.Reason = NewRequest.Reason;
                Request.OnBehalfOfRequesterID = NewRequest.OnBehalfOfRequesterID;
                if (NewRequest.RequestPartialUnitID == 0) { Request.PartialDurationUnitFK = null; }
                else
                {
                    Request.PartialDurationUnitFK = NewRequest.RequestPartialUnitID;
                }
                List<DateTime> OffDays = new List<DateTime>();
                DateTime backtoworkdate = new DateTime();
                List<RequestHandlerHours> OffHours = new List<RequestHandlerHours>();
                MailingDTO = new MailingDTO();
                decimal NumberOfOffUnit=0;


                //Get All Leave Type Data
                int? leavetype = Request.LeaveTypeFK;
                LeaveType LeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == (int)leavetype).FirstOrDefault();
                LeaveTypeAccuralRule leaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == LeaveType.LeaveTypeID).FirstOrDefault();
                LeaveTypeAccuralRule ParentleaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == LeaveType.ParentLeaveTypeFK).FirstOrDefault();


                LeaveTypeCarryOverRule leaveTypeCarryOverRule = leaveTypeCarryOverRuleBLL.Find(x => x.LeaveTypeFK == LeaveType.LeaveTypeID).FirstOrDefault();
                LeaveTypePartialDuration leaveTypePartialDuration = leaveTypePartialDurationBLL.Find(x => x.LeaveTypeFK == LeaveType.LeaveTypeID).FirstOrDefault();
                LeaveTypeRestrictedContractType leaveTypeRestrictedContractType = leaveTypeRestrictedContractTypeBLL.Find(x => x.LeaveTypeFK == LeaveType.LeaveTypeID).FirstOrDefault();

                //End All Leave Type DATA

                //leaveTypeAccuralRule
                Request.LeaveDurationUnitFK = LeaveType.DurationUnitFK;
                DateTime RequestStart = new DateTime();
                if (LeaveType.DurationUnitFK == (int)LeaveDurationUnit.Days)
                {
                    RequestStart = DateTime.ParseExact(NewRequest.RequestStart, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    RequestStart = DateTime.ParseExact(NewRequest.RequestStart, @"dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                }

                Request.DurationFrom = RequestStart;

                if (NewRequest.RequestEnd?.ToString() == "")
                {
                    Request.DurationTo = RequestStart;
                }
                else
                {

                    if (LeaveType.DurationUnitFK == (int)LeaveDurationUnit.Days)
                    {
                        Request.DurationTo = DateTime.ParseExact(NewRequest.RequestEnd, @"dd/MM/yyyy",
                         System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Request.DurationTo = DateTime.ParseExact(NewRequest.RequestEnd, @"dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                    }



                }
                if (Request.DurationFrom <= Request.DurationTo)
                {
                    if ((int)WorkingMode.Regular == WorkingModeID)
                    {
                        SetRequestBackToWorkDate(Request, out backtoworkdate, out OffDays, out OffHours, out NumberOfOffUnit, LeaveType, WorkingWeekID, WorkingModeID);
                    }
                    else if ((int)WorkingMode.Shift == WorkingModeID)
                    {
                        SetRequestBackToWorkDateOfShift(Request, out backtoworkdate, out OffDays, out OffHours, out NumberOfOffUnit, LeaveType, WorkingWeekID, WorkingModeID,out IsOffDay);


                    }
                    Request.BackToWork = backtoworkdate;
                    var EmployeeTotalWorkingMonths = (DateTime.Now.Month - User.HireDate.Value.Month) + (12 * (DateTime.Now.Year - User.HireDate.Value.Year));
                    LeaveType ParentLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveType.ParentLeaveTypeFK)?.FirstOrDefault();
                    DateTime DurationFrom = Request.DurationFrom;
                    DateTime DurationTo = Request.DurationTo;
                    DateTime DurationFromDate = Request.DurationFrom.Date;
                    DateTime DurationToDate = Request.DurationTo.Date;
                    decimal? TotalParentLeavetypeEntitlement;
                    decimal? TotalLeaveTypeEntitlement;
                    if (leaveTypeAccuralRule?.AccuralPeriodFK ==(int) LeaveTypeAccuralPeriod.FirstDayOfTheYear
                        || ParentleaveTypeAccuralRule?.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.FirstDayOfTheYear

                        
                        )
                    {
                        TotalParentLeavetypeEntitlement =
                        userEntitlementBLL.Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == LeaveType.ParentLeaveTypeFK && x.IsActive == true &&
                        x.IsDeleted == false && x.Year == DurationFrom.Year && x.Year == DurationTo.Year)?.FirstOrDefault()?.EntitlementTotal;
                        TotalLeaveTypeEntitlement = userEntitlementBLL.Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == leavetype
                        && x.IsActive == true && x.IsDeleted == false &&
                        x.Year == DurationFrom.Year && x.Year == DurationTo.Year)?.FirstOrDefault()?.EntitlementTotal;

                    }
                    else
                    {
                            TotalParentLeavetypeEntitlement =
                            userEntitlementBLL.Find(x => x.UserFK == User.UserID && x.LeaveTypeFK ==
                            LeaveType.ParentLeaveTypeFK && x.IsActive == true &&
                            x.IsDeleted == false
                            && DurationFromDate .CompareTo(x.EffectiveDateFrom.Value) >=0 
                            && DurationToDate .CompareTo(x.EffectiveDateTo.Value) <=0
                            )
                            ?.FirstOrDefault()?.EntitlementTotal;


                            TotalLeaveTypeEntitlement = userEntitlementBLL.Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == leavetype
                            && x.IsActive == true && x.IsDeleted == false &&
                            DurationFromDate.CompareTo(x.EffectiveDateFrom.Value) >= 0
                             && DurationToDate.CompareTo(x.EffectiveDateTo.Value) <= 0
                            )?.FirstOrDefault()?.EntitlementTotal;
                    }

                    if (

                        ((int)LeaveTypeGainingEligibilityMethod.FromTheFirstDayAtWork == leaveTypeAccuralRule?.FirstAccuralMethodFK
                        || ((int)LeaveTypeGainingEligibilityMethod.AfterHire == leaveTypeAccuralRule?.FirstAccuralMethodFK && (leaveTypeAccuralRule?.AfterHireEligibilityMonths <= EmployeeTotalWorkingMonths)))
                        ||
                        ((int)LeaveTypeGainingEligibilityMethod.FromTheFirstDayAtWork == ParentleaveTypeAccuralRule?.FirstAccuralMethodFK
                        || ((int)LeaveTypeGainingEligibilityMethod.AfterHire == ParentleaveTypeAccuralRule?.FirstAccuralMethodFK &&
                        (ParentleaveTypeAccuralRule?.AfterHireEligibilityMonths <= EmployeeTotalWorkingMonths)
                        ))

                        ||
                       (LeaveType.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Unlimited || ParentLeaveType.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Unlimited)
                        )

                    {
                        if (LeaveType.IncludePublicHolidayInRequestLength==false)
                        {
                            if (CheckIfOfficialHolidayDayorWeekend(Request.DurationFrom, Request.DurationTo, WorkingWeekID, WorkingModeID,(int) Request.UserFK))
                            {
                                IsValid = false;
                                Messages = Messages + "-You Requested Start/End Date which is Officially Holiday or a WeekEnd##$$";
                            }
                        }
                        if (CheckIfEndDateAndStartDateIsValid(Request.DurationFrom, Request.DurationTo))
                        {
                            IsValid = false;
                            Messages = Messages + "-Please Review End Date Is Before Start Date ##$$";

                        }
                        if (CheckIfUserHasRequestInSameDay(OffDays, Request, OffHours))
                        {
                            IsValid = false;
                            Messages = Messages + "-You Requested Start/End Date which is a Pending Vacation Request or a already Approved Vacation Request ##$$";

                        }
                        if (CheckIfRequestIsMoreThanMaxAllowed(NumberOfOffUnit, Request, out MaxAllowed))
                        {
                            IsValid = false;
                            Messages = Messages + "-You Can't Request More Than " + MaxAllowed + " " +
                                 ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString() + "##$$"
                              ;

                        }
                        if (CheckIfRequestIsLessThanMinAllowed(NumberOfOffUnit, Request, out MinAllowed))
                        {
                            IsValid = false;
                            Messages = Messages + "-You Can't Request Less Than " + MinAllowed + " " +
                                 ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString() + "##$$";

                        }
                        if (CheckAbsenceRequestInThePastValidation(Request, out NumberOfDaysInThePastAllowed, WorkingWeekID, WorkingModeID))
                        {
                            IsValid = false;
                            Messages = Messages + "-You Can't Request This Leave Type More  Than " + NumberOfDaysInThePastAllowed + " Days in The Past ##$$";

                        }
                        if (CheckAbsenceLongerThanMustBeRequestedBeforeValidation(Request, NumberOfOffUnit, out NumberOfDaysMustBeRequestedBefor, out NumberOfUnit, WorkingWeekID, WorkingModeID))
                        {
                            IsValid = false;
                            Messages = Messages + "-You Can Request " + NumberOfUnit + " Or More  " +
                                 ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString() +

                                    " Only if It Requested Before " + NumberOfDaysMustBeRequestedBefor +
                                 " " + ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                               + " Of Start Date ##$$";

                        }
                        if (IsOffDay)
                        {
                            IsValid = false;
                            Messages = Messages + "-The Request Days Contains no Shift  Or You don't Have Next Assigned Shift yet.";
                        }


                    }
                    else
                    {
                        IsValid = false;
                        Messages = Messages + "-You have not exceeded " + leaveTypeAccuralRule?.AfterHireEligibilityMonths + " months !";

                    }

                    if (IsValid)
                    {
                        if (LeaveType.ParentLeaveTypeFK != null)
                        {

                            if ((TotalParentLeavetypeEntitlement >= (LeaveType.DeductionFromParentLeaveDurationPercentage / 100) * NumberOfOffUnit) || ParentLeaveType.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Unlimited)
                            {
                                if (TotalLeaveTypeEntitlement >= NumberOfOffUnit)
                                {
                                    Request.BackToWork = backtoworkdate;
                                    Request.RequestDuration = NumberOfOffUnit;
                                    Request.LeaveTypeFK = NewRequest.LeaveTypeID;
                                    Request.RequesStatusFK = (int)StaticEnums.RequestStatus.Pending;
                                    Request.LeaveDurationUnitFK = leaveTypeBLL.Find(x => x.LeaveTypeID == NewRequest.LeaveTypeID).FirstOrDefault().DurationUnitFK;
                                    Request.IsActive = true;
                                    Add(Request);

                                    foreach (var item in OffDays)
                                    {

                                          RequestHandler RequestHandler=  new RequestHandler
                                        {
                                            CreationDate = creationDate,
                                            IsActive = true,
                                            IsDeleted = false,
                                            LeaveTypeFK = (int)Request.LeaveTypeFK,
                                            RequestDuration = NumberOfOffUnit,
                                          //  RequestFK = Request.RequestID,
                                            RquestStatusFK = (int)StaticEnums.RequestStatus.Pending,
                                            UserFK = User.UserID,
                                            UserName = User.UserName,
                                            Offday = item.Date,
                                            DurationUnitFK = Request.LeaveDurationUnitFK,
                                             Request= Request

                                          };
                                       requestHandlerBLL.Add(RequestHandler);

                                        foreach (var Hoursitem in OffHours)
                                        {
                                            requestHoursHandlerBLL.Add(new RequestHoursHandler
                                            {
                                                CreationDate = creationDate,
                                                IsActive = true,
                                                IsDeleted = false,
                                                Duration = Hoursitem.TimeDuration,
                                                EndOffHour = Hoursitem.TimeEnd,
                                                StartOffHour = Hoursitem.TimeStart,
                                                UserFK = User.UserID,
                                                Request = Request,
                                               // RequestHandlerFK = RequestHandler.RequestHandlerID,
                                                 RequestHandler= RequestHandler
                                            });

                                        }

                                    }


                                    AddNewApprovalFlowForLeaveRequest(Request, User,out IsAutoApprove);
                                    try
                                    {
                                         MailingDTO = new MailingDTO
                                        {
                                            RequestID = Request.RequestID,
                                            Action = "submitted",

                                            LeaveTypeName = LeaveType.LeaveTypeName,
                                            Duration = Request.RequestDuration,
                                            StartDate = Request.DurationFrom,
                                            EndtDate = Request.DurationTo,
                                            ComeBackDate = Request.BackToWork,
                                            Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                                        };
                                        MailingDTO.To = new List<string>();
                                        MailingDTO.CC = new List<string>();
                                        //List<string> ListEmailsUserInCC = GetUserApproveTheRequest(Request.RequestID).Select(x => x.UserEmail).ToList();
                                       // List<User> UserToSendEmail = GetUserShouldApproveTheRequest(Request);
                                        MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;
                                        MailingDTO.Actionto = User.UserName;
                                       // MailingDTO.ActionBy = UserToSendEmail.FirstOrDefault().UserName;
                                        //MailingDTO.CC.AddRange(UserToSendEmail.Select(x => x.UserEmail));
                                        MailingDTO.To.Add(User.UserEmail);
                                        //for (int i = 0; i < UserToSendEmail.Count(); i++)
                                        //{
                                        //    if (i == 0)
                                        //    {
                                        //        MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                                        //    }
                                        //    else
                                        //    {
                                        //        MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                                        //    }
                                        //}
                                        if (configurationBLL.Find(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                                        {
                                            // SendMailOnRequestingVication(MailingDTO);
                                            CheckMailEnabled = true;
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                                    }
                                }

                                else if (TotalLeaveTypeEntitlement == null && LeaveType.TakeMaxAmountFromParent == null && leaveTypeAccuralRule == null)
                                {
                                    Request.BackToWork = backtoworkdate;
                                    Request.RequestDuration = NumberOfOffUnit;
                                    Request.LeaveTypeFK = NewRequest.LeaveTypeID;
                                    Request.RequesStatusFK = (int)StaticEnums.RequestStatus.Pending;
                                    Request.LeaveDurationUnitFK = leaveTypeBLL.Find(x => x.LeaveTypeID == NewRequest.LeaveTypeID).FirstOrDefault().DurationUnitFK;
                                    Request.IsActive = true;
                                    Add(Request);
                                    foreach (var item in OffDays)
                                    {

                                        RequestHandler  RequestHandler =  new RequestHandler
                                        {
                                            CreationDate = creationDate,
                                            IsActive = true,
                                            IsDeleted = false,
                                            LeaveTypeFK = (int)Request.LeaveTypeFK,
                                            RequestDuration = NumberOfOffUnit,
                                            //RequestFK = Request.RequestID,
                                            RquestStatusFK = (int)StaticEnums.RequestStatus.Pending,
                                            UserFK = User.UserID,
                                            UserName = User.UserName,
                                            Offday = item.Date,
                                            DurationUnitFK = Request.LeaveDurationUnitFK,
                                             Request= Request,
                                            // User= User


                                        };
                                        requestHandlerBLL.Add(RequestHandler);
                                       // 

                                        foreach (var Hoursitem in OffHours)
                                        {
                                            requestHoursHandlerBLL.Add(new RequestHoursHandler
                                            {
                                                CreationDate = creationDate,
                                                IsActive = true,
                                                IsDeleted = false,
                                                Duration = Hoursitem.TimeDuration,
                                                EndOffHour = Hoursitem.TimeEnd,
                                                StartOffHour = Hoursitem.TimeStart,
                                                UserFK = User.UserID,
                                                Request = Request,
                                                RequestHandler = RequestHandler,
                                            });

                                        }
                                    }

                                    AddNewApprovalFlowForLeaveRequest(Request, User, out IsAutoApprove);
                                    try
                                    {
                                         MailingDTO = new MailingDTO
                                        {
                                            RequestID = Request.RequestID,
                                            Action = "submitted",
                                            LeaveTypeName = LeaveType.LeaveTypeName,
                                            Duration = Request.RequestDuration,
                                            StartDate = Request.DurationFrom,
                                            EndtDate = Request.DurationTo,
                                            ComeBackDate = Request.BackToWork,
                                            Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                                        };
                                        MailingDTO.To = new List<string>();
                                        MailingDTO.CC = new List<string>();
                                        // List<string> ListEmailsUserInCC = GetUserApproveTheRequest(Request.RequestID).Select(x=>x.UserEmail).ToList();
                                       // List<User> UserToSendEmail = GetUserShouldApproveTheRequest(Request);
                                        MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;
                                        MailingDTO.Actionto = User.UserName;
                                       // MailingDTO.ActionBy = UserToSendEmail.FirstOrDefault().UserName;
                                        //MailingDTO.CC.AddRange(UserToSendEmail.Select(x => x.UserEmail));
                                        MailingDTO.To.Add(User.UserEmail);
                                        //for (int i = 0; i < UserToSendEmail.Count(); i++)
                                        //{
                                        //    if (i == 0)
                                        //    {
                                        //        MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                                        //    }
                                        //    else
                                        //    {
                                        //        MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                                        //    }
                                        //}
                                        if (configurationBLL.Find(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                                        {
                                            // SendMailOnRequestingVication(MailingDTO);
                                            CheckMailEnabled = true;

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");

                                    }
                                }
                                else
                                {
                                    Messages = Messages + "You Do Not Have Enough Limit";
                                    IsValid = false;
                                }
                            }

                            else
                            {
                                Messages = Messages + "You Do Not Have Enough Limit";
                                IsValid = false;
                            }
                        }


                        else if (TotalLeaveTypeEntitlement >= NumberOfOffUnit)
                        {
                            Request.BackToWork = backtoworkdate;
                            Request.RequestDuration = NumberOfOffUnit;
                            Request.LeaveTypeFK = NewRequest.LeaveTypeID;
                            Request.RequesStatusFK = (int)StaticEnums.RequestStatus.Pending;
                            Request.LeaveDurationUnitFK = leaveTypeBLL.Find(x => x.LeaveTypeID == NewRequest.LeaveTypeID).FirstOrDefault().DurationUnitFK;
                            Request.IsActive = true;
                            Add(Request);
                            foreach (var item in OffDays)
                            {

                               RequestHandler RequestHandler=     new RequestHandler
                                {
                                    CreationDate = creationDate,
                                    IsActive = true,
                                    IsDeleted = false,
                                    LeaveTypeFK = (int)Request.LeaveTypeFK,
                                    RequestDuration = NumberOfOffUnit,
                                    //RequestFK = Request.RequestID,
                                    RquestStatusFK = (int)StaticEnums.RequestStatus.Pending,
                                    UserFK = User.UserID,
                                    UserName = User.UserName,
                                    Offday = item.Date,
                                    DurationUnitFK = Request.LeaveDurationUnitFK,
                                     Request=Request,
                                    // LeaveType=LeaveType,
                               };
                                requestHandlerBLL.Add(RequestHandler);

                                foreach (var Hoursitem in OffHours)
                                {
                                    requestHoursHandlerBLL.Add(new RequestHoursHandler
                                    {
                                        CreationDate = creationDate,
                                        IsActive = true,
                                        IsDeleted = false,
                                        Duration = Hoursitem.TimeDuration,
                                        EndOffHour = Hoursitem.TimeEnd,
                                        StartOffHour = Hoursitem.TimeStart,
                                        UserFK = User.UserID,
                                        RequestFK = Request.RequestID,
                                        RequestHandler = RequestHandler,
                                    });

                                }
                            }

                           AddNewApprovalFlowForLeaveRequest(Request, User,out IsAutoApprove);
                            try
                            {
                                 MailingDTO = new MailingDTO
                                {
                                    RequestID = Request.RequestID,
                                    Action = "submitted",

                                    LeaveTypeName = LeaveType.LeaveTypeName,
                                    Duration = Request.RequestDuration,
                                    StartDate = Request.DurationFrom,
                                    EndtDate = Request.DurationTo,
                                    ComeBackDate = Request.BackToWork,
                                    Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                                };
                                MailingDTO.To = new List<string>();
                                MailingDTO.CC = new List<string>();
                                //  List<string> ListEmailsUserInCC = GetUserApproveTheRequest(Request.RequestID).Select(x => x.UserEmail).ToList();
                               // List<User> UserToSendEmail = GetUserShouldApproveTheRequest(Request);
                                MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;
                                MailingDTO.Actionto = User.UserName;
                                //MailingDTO.ActionBy = UserToSendEmail.FirstOrDefault()?.UserName;
                                //MailingDTO.CC.AddRange(UserToSendEmail.Select(x => x.UserEmail));
                                MailingDTO.To.Add(User.UserEmail);
                                //for (int i = 0; i < UserToSendEmail.Count(); i++)
                                //{
                                //    if (i == 0)
                                //    {
                                //        MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                                //    }
                                //    else
                                //    {
                                //        MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                                //    }
                                //}
                                if (configurationBLL.Find(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                                {
                                    //SendMailOnRequestingVication(MailingDTO);
                                    CheckMailEnabled = true;

                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                            }
                        }
                        else if (LeaveType.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Unlimited)

                        {
                            Request.BackToWork = backtoworkdate;
                            Request.RequestDuration = NumberOfOffUnit;
                            Request.LeaveTypeFK = NewRequest.LeaveTypeID;
                            Request.RequesStatusFK = (int)StaticEnums.RequestStatus.Pending;
                            Request.LeaveDurationUnitFK = leaveTypeBLL.Find(x => x.LeaveTypeID == NewRequest.LeaveTypeID).FirstOrDefault().DurationUnitFK;
                            Request.IsActive = true;
                            Add(Request);
                            foreach (var item in OffDays)
                            {

                                  RequestHandler RequestHandler =  new RequestHandler
                                {
                                    CreationDate = creationDate,
                                    IsActive = true,
                                    IsDeleted = false,
                                    LeaveTypeFK = (int)Request.LeaveTypeFK,
                                    RequestDuration = NumberOfOffUnit,
                                   // RequestFK = Request.RequestID,
                                    RquestStatusFK = (int)StaticEnums.RequestStatus.Pending,
                                    UserFK = User.UserID,
                                    UserName = User.UserName,
                                    Offday = item.Date,
                                    DurationUnitFK = Request.LeaveDurationUnitFK,
                                     Request= Request

                                  };
                                requestHandlerBLL.Add(RequestHandler);


                                foreach (var Hoursitem in OffHours)
                                {
                                    requestHoursHandlerBLL.Add(new RequestHoursHandler
                                    {
                                        CreationDate = creationDate,
                                        IsActive = true,
                                        IsDeleted = false,
                                        Duration = Hoursitem.TimeDuration,
                                        EndOffHour = Hoursitem.TimeEnd,
                                        StartOffHour = Hoursitem.TimeStart,
                                        UserFK = User.UserID,
                                        RequestFK = Request.RequestID,
                                        RequestHandler = RequestHandler,
                                    });

                                }
                            }

                            AddNewApprovalFlowForLeaveRequest(Request, User, out IsAutoApprove);
                            try
                            {
                                 MailingDTO = new MailingDTO
                                {
                                    RequestID = Request.RequestID,
                                    Action = "submitted",

                                    LeaveTypeName = LeaveType.LeaveTypeName,
                                    Duration = Request.RequestDuration,
                                    StartDate = Request.DurationFrom,
                                    EndtDate = Request.DurationTo,
                                    ComeBackDate = Request.BackToWork,
                                    Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                                };
                                MailingDTO.To = new List<string>();
                                MailingDTO.CC = new List<string>();
                                //   List<string> ListEmailsUserInCC = GetUserApproveTheRequest(Request.RequestID).Select(x => x.UserEmail).ToList();
                               // List<User> UserToSendEmail = GetUserShouldApproveTheRequest(Request);
                                MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;

                                MailingDTO.Actionto = User.UserName;
                                //MailingDTO.ActionBy = UserToSendEmail.FirstOrDefault().UserName;
                                //MailingDTO.CC.AddRange(UserToSendEmail.Select(x => x.UserEmail));
                                MailingDTO.To.Add(User.UserEmail);
                                //for (int i = 0; i < UserToSendEmail.Count(); i++)
                                //{
                                //    if (i == 0)
                                //    {
                                //        MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                                //    }
                                //    else
                                //    {
                                //        MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                                //    }
                                //}
                                if (configurationBLL.Find(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                                {
                                  //SendMailOnRequestingVication(MailingDTO);
                                    CheckMailEnabled = true;

                                }
                            }
                            catch(Exception ex)
                            {
                                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                            }
                        }
                        else
                        {
                            Messages = Messages + "You Do Not Have Enough Limit";
                            IsValid = false;

                        }
                    }
                }
                else
                {
                    IsValid = false;
                    CheckMailEnabled = false;

                    Messages = "End Date Is Before Start Date";
                }

                //Request Is Valid
             

                return Messages;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                MailingDTO = null;
                IsValid = false;
                Messages = "Error";
                Request = null;
                CheckMailEnabled = false;
                IsAutoApprove = false;
                return Messages;

            }
        }
        public void CheckDays(DateTime start, DateTime end, out List<DateTime> offdays, 
            out DateTime backtoworkdate, out decimal NumberOfOffUnit, int workingWeekID,int WorkingModeID)
        {
            try
            {
                var dateInterval = new List<DateTime>();
                var daysInWeek = new List<DayOfWeek>();
                var dayswithoutWeekEnd = new List<DateTime>();
                //User OffDays Working week
               // var UserOffDays = companyOfficialHolidayBLL.GetAll().Where(p => p.CompanyFK == ).ToList();
                var workingWeek = workingWeekBLL.Find(x => x.WorkingWeekID == workingWeekID).FirstOrDefault();
                var ListDays = workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == workingWeek.WorkingWeekID && x.IsActive == false).ToList();
                List<int> OffDays = new List<int>();
                foreach (var day in ListDays)
                {

                    OffDays.Add((int)weekDaysBLL.Find(x => x.WeeKDaysID == day.WeekDaysFK).FirstOrDefault().WeekDayValue); 
                }
                OffDays= OffDays.OrderBy(x=>x).ToList();
                for (var dt = DateTime.Parse(start.ToString()).Date; dt <= DateTime.Parse(end.ToString()).Date; dt = dt.AddDays(1))
                {
                    dateInterval.Add(dt);
                    daysInWeek.Add(dt.DayOfWeek);
                }
                for (int j = 0; j < daysInWeek.Count; j++)
                {
                    var z = (int)daysInWeek[j];

                    foreach (var item in OffDays)
                    {
                        if (daysInWeek.Count()-1>= j) {
                            if ((int)daysInWeek[j] == item    )
                            {
                                dateInterval.RemoveAt(j);
                                daysInWeek.RemoveAt(j);
                            }
                        }
                        else
                        {

                        }
                    }


                }

                dayswithoutWeekEnd = dateInterval;
                //If User Is Not shifts
                if ((int)WorkingMode.Regular== WorkingModeID)
                {
                    var Holidays = officialHolidayBLL.GetAll().ToList();
                    for (int i = 0; i < dayswithoutWeekEnd.Count; i++)
                    {
                        for (int j = 0; j < Holidays.Count; j++)
                        {
                            if (Holidays[j].HolidayDate.Value.Date == dayswithoutWeekEnd[i])
                            {
                                dayswithoutWeekEnd.RemoveAt(i);
                                if (dayswithoutWeekEnd.Count == 0)
                                {
                                    goto GetFinalOffDays;
                                }
                            }
                        }
                    }
                }
                GetFinalOffDays: var OffWorkDays = dayswithoutWeekEnd;
                offdays = OffWorkDays;
                DateTime backtoworkexactdate = Backtoworkdate(OffWorkDays, start, end, workingWeekID);
                NumberOfOffUnit = OffWorkDays.Count();
                if (backtoworkexactdate.Date == DateTime.MinValue.Date)
                {
                    backtoworkdate = DateTime.MinValue.Date;
                }
                else
                {
                    backtoworkdate = backtoworkexactdate;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                backtoworkdate = DateTime.MinValue.Date;
                NumberOfOffUnit = 0;
                offdays = null;
            }
        }





        public void CheckDaysIncludingPublicHolidays(DateTime start, DateTime end, out List<DateTime> offdays, out DateTime backtoworkdate, out decimal NumberOfOffUnit, int workingWeekID)
        {
            try
            {
                var dateInterval = new List<DateTime>();
                var daysInWeek = new List<DayOfWeek>();
                var dayswithWeekEnd = new List<DateTime>();

                for (var dt = DateTime.Parse(start.ToString()).Date; dt <= DateTime.Parse(end.ToString()).Date; dt = dt.AddDays(1))
                {
                    dateInterval.Add(dt);
                    daysInWeek.Add(dt.DayOfWeek);
                }

                dayswithWeekEnd = dateInterval;
                var OffWorkDays = dayswithWeekEnd;
                offdays = OffWorkDays;
                DateTime backtoworkexactdate = Backtoworkdate(OffWorkDays, start, end, workingWeekID);
                NumberOfOffUnit = OffWorkDays.Count();
                if (backtoworkexactdate.Date == DateTime.MinValue.Date)
                {
                    backtoworkdate = DateTime.MinValue.Date;
                }
                else
                {
                    backtoworkdate = backtoworkexactdate;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                backtoworkdate = DateTime.MinValue.Date;
                NumberOfOffUnit = 0;
                offdays = null;
            }
        }
        public void CheckRequestLength(DateTime start, DateTime end, out decimal NumberOfOffUnit, int workingWeekID)
        {
            try
            {
                var dateInterval = new List<DateTime>();
                var daysInWeek = new List<DayOfWeek>();
                var dayswithoutWeekEnd = new List<DateTime>();
                for (var dt = DateTime.Parse(start.ToString()).Date; dt <= DateTime.Parse(end.ToString()).Date; dt = dt.AddDays(1))
                {
                    dateInterval.Add(dt);
                    daysInWeek.Add(dt.DayOfWeek);
                }
              var   workingWeek= workingWeekBLL.GetAll().FirstOrDefault();

                var ListDays = workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == workingWeek.WorkingWeekID && x.IsActive == false) ;
                List<int> OffDays = new List<int>();
                foreach(var day in ListDays)
                {
                    OffDays.Add(day.WeekDaysFK);
                }


                for (int j = 0; j < daysInWeek.Count; j++)
                {
                    foreach (var day in OffDays)
                    {
                        if (daysInWeek[j] == (DayOfWeek)day)
                        {
                            dateInterval.RemoveAt(j);
                            daysInWeek.RemoveAt(j);
                        }
                    }
                }


                //for (int j = 0; j < daysInWeek.Count; j++)
                //{
                //    if (daysInWeek[j] == DayOfWeek.Saturday)
                //    {
                //        dateInterval.RemoveAt(j);
                //        daysInWeek.RemoveAt(j);
                //    }
                //}


                dayswithoutWeekEnd = dateInterval;

                var Holidays = officialHolidayBLL.GetAll().ToList();
                for (int i = 0; i < dayswithoutWeekEnd.Count; i++)
                {
                    for (int j = 0; j < Holidays.Count; j++)
                    {
                        if (Holidays[j].HolidayDate.Value.Date == dayswithoutWeekEnd[i])
                        {
                            dayswithoutWeekEnd.RemoveAt(i);
                            if (dayswithoutWeekEnd.Count == 0)
                            {
                                goto GetFinalOffDays;
                            }
                        }
                    }
                }

            GetFinalOffDays: var OffWorkDays = dayswithoutWeekEnd;
                DateTime backtoworkexactdate = Backtoworkdate(OffWorkDays, start, end, workingWeekID);
                NumberOfOffUnit = OffWorkDays.Count();
               

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                NumberOfOffUnit = 0;
            }
        }
        public void RequestLengthIncludingPublicHolidays(DateTime start, DateTime end, out decimal NumberOfOffUnit, int workingWeekID)
        {
            try
            {
                var dateInterval = new List<DateTime>();
                var daysInWeek = new List<DayOfWeek>();
                var dayswithWeekEnd = new List<DateTime>();
                for (var dt = DateTime.Parse(start.ToString()).Date; dt <= DateTime.Parse(end.ToString()).Date; dt = dt.AddDays(1))
                {
                    dateInterval.Add(dt);
                    daysInWeek.Add(dt.DayOfWeek);
                }

                dayswithWeekEnd = dateInterval;
                var OffWorkDays = dayswithWeekEnd;
                DateTime backtoworkexactdate = Backtoworkdate(OffWorkDays, start, end, workingWeekID);
                NumberOfOffUnit = OffWorkDays.Count();
              

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                NumberOfOffUnit = 0;
            }
        }
        public DateTime Backtoworkdate(List<DateTime> offdays, DateTime start, DateTime end, int workingWeekID)
        {
            if (end.Date < start.Date)
            {
                return DateTime.MinValue.Date;
            }
            else
            {
                try
                {
                    DateTime endofvacation = offdays.Max();
                    var Holidays = officialHolidayBLL.GetAll().ToList();
                    DateTime backtoworkdate = endofvacation.AddDays(1);
                    var workingWeek = workingWeekBLL.Find(x => x.WorkingWeekID == workingWeekID).FirstOrDefault();
                    var ListDays = workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == workingWeek.WorkingWeekID && x.IsActive == false).ToList();
                    List<int> OffDays = new List<int>();
                    foreach (var day in ListDays)
                    {

                        OffDays.Add((int)weekDaysBLL.Find(x => x.WeeKDaysID == day.WeekDaysFK).FirstOrDefault().WeekDayValue); ;
                    }
                    for (int i = 0; ; i++)
                    {
                        bool Valid = true;
                        foreach (var item in Holidays)
                        {
                            if (item.HolidayDate == backtoworkdate.Date)
                            {
                                Valid = false;
                                break;
                            }

                        }
                        foreach (var item in OffDays)
                        {
                            if (item == (int)backtoworkdate.DayOfWeek)
                            {
                                Valid = false;
                                break;
                            }
                        }

                        if (Valid == true)
                        {

                            break;
                        }
                        else
                        {

                            backtoworkdate = backtoworkdate.AddDays(1);
                            continue;
                        }

                    }
                    
            return backtoworkdate.Date;
                }

                catch
                {
                    return DateTime.MinValue.Date;
                }
            }


        }

       


        //public DateTime Backtoworkdate(List<DateTime> offdays, DateTime start, DateTime end,int workingWeekID)
        //{
        //    if (end.Date < start.Date)
        //    {
        //        return DateTime.MinValue.Date;
        //    }
        //    else
        //    {
        //        var Holidays = officialHolidayBLL.GetAll().ToList();
        //        var workingWeek = workingWeekBLL.Find(x=>x.WorkingWeekID== workingWeekID).FirstOrDefault();
        //        var ListDays = workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == workingWeek.WorkingWeekID && x.IsActive == false).ToList();
        //        List<int> OffDays = new List<int>();
        //        foreach (var day in ListDays)
        //        {

        //            OffDays.Add((int)weekDaysBLL.Find(x => x.WeeKDaysID == day.WeekDaysFK).FirstOrDefault().WeekDayValue); ;
        //        }
        //        end= end.AddDays(1);
        //        for (int i=0; ;i++)
        //        {
        //            bool Valid = true;
        //            foreach (var item in Holidays)
        //            {
        //                if(item.HolidayDate== end.Date)
        //                {
        //                    Valid = false;
        //                    break;   
        //                }

        //            }
        //            foreach (var item in OffDays)
        //            {
        //                if (item == (int)end.DayOfWeek)
        //                {
        //                    Valid = false;
        //                    break;
        //                }
        //            }

        //            if (Valid==true)
        //            {

        //                break;
        //            }
        //            else {

        //                end= end.AddDays(1);
        //                continue;
        //            }

        //        }

        //    }
        //        return end;


        //}


        public void SetRequestBackToWorkDate(Request Request, out DateTime backtoworkdate, out List<DateTime> OffDays, 
            out List<RequestHandlerHours> OffHours, out decimal NumberOfOffUnit, LeaveType LeaveType, int workingWeekID, int WorkingModeID)
        {
            OffHours = new List<RequestHandlerHours>();
            if (Request.LeaveDurationUnitFK == (int)LeaveDurationUnit.Hours)
            {
                int day = (int)Request.DurationTo.DayOfWeek;
                int DayFK=  weekDaysBLL.Find(x => x.WeekDayValue == day).FirstOrDefault().WeeKDaysID;
                TimeSpan DayEnd = (TimeSpan) workingWeekDetailsBLL.Find(x=>x.WorkingWeekFK== workingWeekID&&x.WeekDaysFK== DayFK).FirstOrDefault().EndTime;
                
                OffDays = new List<DateTime>();

                OffDays.Add(Request.DurationFrom.Date);
                NumberOfOffUnit = (Request.DurationTo - Request.DurationFrom).Hours;
                decimal RemaningMinutes =(decimal) ((Request.DurationTo - Request.DurationFrom).TotalMinutes / 60) - NumberOfOffUnit;

                if (Request.DurationTo.TimeOfDay < DayEnd)
                {
                    backtoworkdate = Request.DurationTo;
                }
                else
                {
                    Request.DurationTo = Request.DurationTo.Date + DayEnd;
                    backtoworkdate = Backtoworkdate(OffDays, Request.DurationFrom, Request.DurationFrom, workingWeekID);

                }

                TimeSpan StartOfRequestTime = Request.DurationFrom.TimeOfDay;
                for (int i = 0; i < NumberOfOffUnit; i++)
                {
                    if (i == 0)
                    {
                        //StartOfRequestTime += TimeSpan.FromHours(1);
                        TimeSpan EndOfRequestTime = StartOfRequestTime;
                        EndOfRequestTime += TimeSpan.FromHours(1);
                        OffHours.Add(
                            new RequestHandlerHours
                            {
                                TimeStart = StartOfRequestTime,
                                TimeEnd = EndOfRequestTime,
                                TimeDuration = 1
                            });
                    }
                
                    else
                    {
                        StartOfRequestTime += TimeSpan.FromHours(1);
                        TimeSpan EndOfRequestTime = StartOfRequestTime;
                        EndOfRequestTime += TimeSpan.FromHours(1);
                        OffHours.Add(
                            new RequestHandlerHours
                            {
                                TimeStart = StartOfRequestTime,
                                TimeEnd = EndOfRequestTime,
                                TimeDuration = 1
                            });
                    }
                }

                TimeSpan timespan = TimeSpan.FromHours((double)RemaningMinutes);
                if (RemaningMinutes!=0) {
                    OffHours.Add(new RequestHandlerHours {
                        TimeStart = StartOfRequestTime,
                        TimeEnd = StartOfRequestTime + timespan,
                        TimeDuration = RemaningMinutes });
                }


            }
            else
            {
                if (Request.PartialDurationUnitFK == (int)StaticEnums.LeaveTypePartialDurationUnit.AMHalfDay)
                {
                    OffDays = new List<DateTime>();
                    backtoworkdate = Request.DurationFrom;
                    OffDays.Add(Request.DurationFrom);
                    NumberOfOffUnit = (decimal)0.5;

                }
                else if (Request.PartialDurationUnitFK == (int)StaticEnums.LeaveTypePartialDurationUnit.PMHalfDay)
                {
                    OffDays = new List<DateTime>();

                    CheckDays(Request.DurationFrom, Request.DurationTo, out OffDays, out backtoworkdate, out NumberOfOffUnit, workingWeekID, WorkingModeID);
                    NumberOfOffUnit = (decimal)0.5;
                }


                else
                {
                    if (LeaveType.IncludePublicHolidayInRequestLength==false)
                    {
                        CheckDays(Request.DurationFrom, Request.DurationTo, out OffDays, out backtoworkdate, out NumberOfOffUnit, workingWeekID, WorkingModeID);
                    }
                    else
                    {
                        CheckDaysIncludingPublicHolidays(Request.DurationFrom, Request.DurationTo, out OffDays, out backtoworkdate, out NumberOfOffUnit, workingWeekID);
                    }
                }

            }
        }


    
        
        public bool CheckIfOfficialHolidayDayorWeekend(DateTime startdate, DateTime enddate,int WorkingWeekID, int WorkingModeID,int UserID)
        {
            bool isitholiday = false;
            if (WorkingModeID == (int)WorkingMode.Regular)
            {
                var holiday = officialHolidayBLL.GetAll().ToList();
                //var CompanyOffDays = companyOfficialHolidayBLL.GetAll().Where(p=>p.CompanyFK== UserCompanyFK).ToList();

                var workingWeek = workingWeekBLL.Find(x => x.WorkingWeekID == WorkingWeekID).FirstOrDefault();
                var ListDays = workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == workingWeek.WorkingWeekID && x.IsActive == false).ToList();
                List<int> OffDays = new List<int>();
                foreach (var day in ListDays)
                {

                    OffDays.Add((int)weekDaysBLL.Find(x => x.WeeKDaysID == day.WeekDaysFK).FirstOrDefault().WeekDayValue);
                }
                OffDays = OffDays.OrderBy(x => x).ToList();


                foreach (var item in OffDays)
                {
                    if ((int)startdate.DayOfWeek == item || (int)startdate.DayOfWeek == item)
                    {
                        isitholiday = true;
                    }

                    if ((int)enddate.DayOfWeek == item || (int)enddate.DayOfWeek == item)
                    {
                        isitholiday = true;
                    }
                }



                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].HolidayDate.Value.Date == startdate)
                    {
                        isitholiday = true;
                    }
                }
                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].HolidayDate.Value.Date == enddate)
                    {
                        isitholiday = true;
                    }
                }

            }
            else if(WorkingModeID == (int)WorkingMode.Shift)
            {
                //Check Shifs Days Here


                var CheckStartDate = workingShiftUserDailyBLL.Find(x =>

               x.ShiftStartDate >= startdate.Date && x.ShiftEndDate <= startdate.Date).Count() > 0;



                var CheckEndDateDate = workingShiftUserDailyBLL.Find(x =>

          x.ShiftStartDate >= enddate.Date && x.ShiftEndDate <= enddate.Date).Count() > 0;








              if (!CheckStartDate || !CheckEndDateDate) {
                    isitholiday = true;
                }
            }
            return isitholiday;
        }
        public bool CheckIfEndDateAndStartDateIsValid(DateTime startdate, DateTime enddate)
        {
            bool isitholiday = false;


            if (enddate.Date >= startdate.Date)
            {
                isitholiday = false;
            }

            else
            {
                isitholiday = true;
            }



            return isitholiday;
        }




        //public bool CheckIfUserHasRequestInSameDay(List<DateTime> ListRequestsDays, Request Request)
        //{
        //    if (ListRequestsDays?.Count > 0)
        //    {
        //        int Count = 0;
        //        foreach (var item in ListRequestsDays)
        //        {
        //            var RequestsInRequestHandler = requestHandlerBLL.Find(x => x.IsActive == true && x.IsDeleted == false & x.UserFK == Request.UserFK && x.RquestStatusFK != (int)RequestStatus.Rejected).ToList();
        //            foreach (var RequestItem in RequestsInRequestHandler)
        //            {
        //                if (RequestItem.Offday.Value == item.Date)
        //                {
        //                    if (leaveTypeBLL.Find(z => z.LeaveTypeID == RequestItem.LeaveTypeFK).FirstOrDefault().ConsiderAsFK == (int)LeaveTypeConsideration.TimeOff && (Request.PartialDurationUnitFK == null)
        //                    || (Find(x => x.RequestID == RequestItem.RequestFK).FirstOrDefault().PartialDurationUnitFK == Request.PartialDurationUnitFK)

        //                  || (Find(x => x.RequestID == RequestItem.RequestFK).FirstOrDefault().LeaveTypeFK == Request.LeaveTypeFK)


        //                   )
        //                    {
        //                        Count++;
        //                    }
        //                    else if (leaveTypeBLL.Find(z => z.LeaveTypeID == Request.LeaveTypeFK).FirstOrDefault().ConsiderAsFK == (int)LeaveTypeConsideration.RemoteWork)
        //                    {
        //                        // Count = 0;
        //                    }
        //                }

        //            }
        //            if (Count == 0)
        //            {
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }


        //}


        public bool CheckIfValidHours(Request Request, List<RequestHandler> requestHandler, List<RequestHandlerHours> OffHours)
        {
            bool Valid = true;
            foreach (var HourHandler in OffHours)
            {
                foreach (var RequestHandleritem in requestHandler)
                {
                    var requestHours = requestHoursHandlerBLL.Find(x => x.RequestHandlerFK == RequestHandleritem.RequestHandlerID).ToList();
                    foreach (var HourItem in requestHours)
                    {
                        if ((HourItem.StartOffHour <= HourHandler.TimeStart && HourHandler.TimeStart <= HourItem.EndOffHour)
                            ||
                           (HourItem.StartOffHour <= HourHandler.TimeEnd && HourHandler.TimeEnd <= HourItem.EndOffHour))
                        {
                            Valid= false;
                        }

                    }

                }
            }
            return Valid;
        }




        public bool CheckIfUserHasRequestInSameDay(List<DateTime> ListRequestsDays, Request Request, List<RequestHandlerHours> OffHours)
        {

            LeaveType RequestLeaveType = leaveTypeBLL.Find(z => z.LeaveTypeID == Request.LeaveTypeFK).FirstOrDefault();
            if (ListRequestsDays?.Count > 0)
            {
                int Count = 0;
                foreach (var item in ListRequestsDays)
                {
                    DateTime DateOfLeave = item.Date;
                    List<RequestHandler> requestHandler = requestHandlerBLL.Find
                      (x =>
                         x.IsActive == true
                      && x.IsDeleted == false
                      && x.UserFK == Request.UserFK
                      && x.RquestStatusFK != (int)RequestStatus.Rejected
                      && x.Offday.Value.Day == DateOfLeave.Day
                      && x.Offday.Value.Year == DateOfLeave.Year
                      && x.Offday.Value.Month == DateOfLeave.Month
                      ).ToList();
                    foreach (var itemrequest in requestHandler)
                    {
                        LeaveType RequestHandlerLeaveType = leaveTypeBLL.Find(z => z.LeaveTypeID == itemrequest.LeaveTypeFK).FirstOrDefault();
                        Request RequestHandler = Find(x => x.RequestID == itemrequest.RequestFK).FirstOrDefault();



                        bool RequestExist =
                                RequestHandlerLeaveType.ConsiderAsFK == (int)LeaveTypeConsideration.TimeOff
                            && (Request.PartialDurationUnitFK == null|| RequestHandler.PartialDurationUnitFK == Request.PartialDurationUnitFK)

                            && !(RequestLeaveType.ConsiderAsFK == (int)LeaveTypeConsideration.RemoteWork
                      && RequestHandlerLeaveType.ConsiderAsFK == (int)LeaveTypeConsideration. TimeOff
                       && RequestHandlerLeaveType.DurationUnitFK == (int)LeaveDurationUnit.Hours
                      && RequestLeaveType.DurationUnitFK == (int)LeaveDurationUnit.Days
                       )

                            || (RequestHandler.LeaveTypeFK == Request.LeaveTypeFK)
                            
                            || (
                                   RequestHandler.LeaveTypeFK == Request.LeaveTypeFK
                                && RequestHandlerLeaveType.ConsiderAsFK == (int)LeaveTypeConsideration.RemoteWork
                                && RequestHandlerLeaveType.DurationUnitFK== (int)LeaveDurationUnit.Days
                                && RequestHandler.PartialDurationUnitFK == Request.PartialDurationUnitFK
                                );

                        if (RequestExist)
                        {
                            Count++;
                        }
                    }
                    if (RequestLeaveType.ConsiderAsFK == (int)LeaveTypeConsideration.RemoteWork
                        && RequestLeaveType.DurationUnitFK==(int)LeaveDurationUnit.Hours
                        )
                    {

                        if (CheckIfValidHours(Request,requestHandler, OffHours))
                        {
                            Count = 0;
                        }
                    }
                    if (Count == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool CheckIfRequestIsMoreThanMaxAllowed(decimal NumberOfOffUnit, Request Request, out decimal MaxAllowed)
        {
            if (NumberOfOffUnit > 0)
            {
                MaxAllowed = 0;
                if (leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
                x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.AbsenceNotLongerThan &&
                x.IsActive == true && x.IsDeleted == false).Count() > 0)
                {
                    LeaveTypeRestriction RequestIsMoreThanMaxAllowedRestriction = leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
                 x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.AbsenceNotLongerThan &&
                 x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    if (NumberOfOffUnit > RequestIsMoreThanMaxAllowedRestriction.RestrictionUnit)
                    {
                        MaxAllowed = (decimal)RequestIsMoreThanMaxAllowedRestriction.RestrictionUnit;
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }

                return false;
            }
            else
            {
                MaxAllowed = 0;
                return false;
            }
        }
        public bool CheckIfRequestIsLessThanMinAllowed(decimal NumberOfOffUnit, Request Request, out decimal MinAllowed)
        {
            if (NumberOfOffUnit > 0)
            {
                MinAllowed = 0;
                if (leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
                 x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.AbsenceNotShorterThan &&
                 x.IsActive == true && x.IsDeleted == false).Count() > 0)
                {
                    LeaveTypeRestriction RequestIsMoreThanMinAllowedRestriction = leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
                 x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.AbsenceNotShorterThan &&
                 x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    if (NumberOfOffUnit < RequestIsMoreThanMinAllowedRestriction.RestrictionUnit )
                    {
                        MinAllowed = (decimal)RequestIsMoreThanMinAllowedRestriction.RestrictionUnit;

                        return true;

                    }

                }

                return false;
            }
            else
            {
                MinAllowed = 0;
                return false;
            }


        }
        public bool CheckAbsenceRequestInThePastValidation(Request Request, out decimal NumberOfDaysInThePastAllowed,int workingWeekID,int WorkingModeID)
        {


            NumberOfDaysInThePastAllowed = 0;
            if (leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
             x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.AbsenceRequestedUpTo &&
             x.IsActive == true && x.IsDeleted == false).Count() > 0)
            {
                LeaveTypeRestriction AbsenceInThePast = leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
             x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.AbsenceRequestedUpTo &&
             x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                List<DateTime> OffDays = new List<DateTime>();
                DateTime BackToWorkDate = new DateTime();
                decimal NumberOfOffDays = 0;
                CheckDays(Request.DurationFrom, (DateTime)Request.CreationDate, out OffDays,out BackToWorkDate, out NumberOfOffDays, workingWeekID, WorkingModeID);
                if (NumberOfOffDays > AbsenceInThePast.RestrictionUnit)
                {
                    if ((decimal)AbsenceInThePast.RestrictionUnit > 0)
                    {
                        NumberOfDaysInThePastAllowed = (decimal)AbsenceInThePast.RestrictionUnit - 1;
                    }
                    else
                    {
                        NumberOfDaysInThePastAllowed = (decimal)AbsenceInThePast.RestrictionUnit;

                    }
                    return true;

                }

            }

            return false;


        }
        public bool CheckAbsenceLongerThanMustBeRequestedBeforeValidation(Request Request, decimal NumberOfOffUnit, out decimal NumberOfDaysMustBeRequestedBefore, out decimal NumberOfUnit,int workingWeekID,int WorkingModeID)
        {
            if (NumberOfOffUnit > 0)
            {
                NumberOfDaysMustBeRequestedBefore = 0;
                NumberOfUnit = 0;
                DateTime CreationOfRequestDate = (DateTime)Request.CreationDate;
                if (leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
                 x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.IfAbsenceLongerThan &&
                 x.IsActive == true && x.IsDeleted == false).Count() > 0 && Request.DurationFrom >= CreationOfRequestDate.Date)
                {
                    LeaveTypeRestriction IfAbsenceLongerThan = leaveTypeRestrictionBLL.Find(x => x.LeaveTypeFK == Request.LeaveTypeFK &&
                 x.LeaveTypeRestrictionTypeFK == (int)RestrictionType.IfAbsenceLongerThan &&
                 x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
                    List<DateTime> OffDays = new List<DateTime>();
                    DateTime BackToWorkDays = new DateTime();
                    decimal CountOffDays = 0;
                    CheckDays(CreationOfRequestDate, Request.DurationFrom, out OffDays, out BackToWorkDays, out CountOffDays, workingWeekID, WorkingModeID);
                    if (NumberOfOffUnit>=IfAbsenceLongerThan.RestrictionUnit  && CountOffDays <= IfAbsenceLongerThan.OtherRestrictionUnit)
                    {
                        NumberOfDaysMustBeRequestedBefore = (decimal)IfAbsenceLongerThan.OtherRestrictionUnit;
                        NumberOfUnit = (decimal)IfAbsenceLongerThan.RestrictionUnit;

                        return true;

                    }

                }

                return false;
            }
            else
            {
                NumberOfDaysMustBeRequestedBefore = 0;
                NumberOfUnit = 0;
                return false;
            }
        }
        public List<Request> GetApprovedRequestByManager(int UserID)
        {
            User UserManager = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            List<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails = new List<ApprovalFlowRequestDetail>();
            ApprovalFlowRequestDetails = approvalFlowRequestDetailsBLL.Find(x => x.ApprovalPersonFK == UserManager.UserID).ToList();
            List<Request> Requests = new List<Request>();

            List<int> RequestsIDs = ApprovalFlowRequestDetails.Where(x => x.RequestActionFK == (int)ActionType.Approved && x.ApprovalPersonFK == UserManager.UserID).
              Select(x => x.RequestFK).ToList();
            foreach (var itemId in RequestsIDs)
            {
                Requests.AddRange(Find(x => x.RequestID == itemId));
            }

            return Requests;
        }
        public List<Request> GetRejectedRequestByManager(int UserID)
        {
            User UserManager = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();

            List<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails = new List<ApprovalFlowRequestDetail>();
            ApprovalFlowRequestDetails = approvalFlowRequestDetailsBLL.Find(x => x.ApprovalPersonFK == UserManager.UserID).ToList();
            List<Request> Requests = new List<Request>();

            List<int> RequestsIDs = ApprovalFlowRequestDetails.Where(x => x.RequestActionFK == (int)ActionType.Rejected && x.ApprovalPersonFK == UserManager.UserID).
              Select(x => x.RequestFK).ToList();
            foreach (var itemId in RequestsIDs)
            {
                Requests.AddRange(Find(x => x.RequestID == itemId));
            }

            return Requests;


        }
        public List<Request> GetALLRequestByManager(int UserID)
        {
            User UserManager = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();

            List<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails = new List<ApprovalFlowRequestDetail>();
            ApprovalFlowRequestDetails = approvalFlowRequestDetailsBLL.Find(x => x.ApprovalPersonFK == UserManager.UserID).ToList();
            List<Request> Requests = new List<Request>();

            List<int> RequestsIDs = ApprovalFlowRequestDetails.Where(x => x.ApprovalPersonFK == UserManager.UserID).
              Select(x => x.RequestFK).ToList();
            foreach (var itemId in RequestsIDs)
            {
                Requests.AddRange(Find(x => x.RequestID == itemId));
            }

            return Requests;


        }

        public List<Request> IfUserInFlowOfRequest(int UserID)
        {
            User User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            Manager Manager = managerBLL.Find(x => x.ManagerUserFK == UserID).FirstOrDefault();

            List<Request> Requests = new List<Request>();
            var list = approvalFlowRequestDetailsBLL.Find(x => (x.ApprovalPersonFK == User.UserID || x.IsDirectManager == true || x.IsTeamManager == true) && x.IsActive == true).ToList();
            foreach (var item in list)
            {
                Request request = Find(x => x.RequestID == item.RequestFK).FirstOrDefault();
                User user = userBLL.Find(x => x.UserID == request.UserFK).FirstOrDefault();
                SubDepartment subDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == user.SubDepartmentFK).FirstOrDefault();
                bool IsPending = false;
                if (item.RequestActionFK == null && item.ApprovalPersonFK == User.UserID && request.RequesStatusFK == (int)RequestStatus.Pending)
                {
                    IsPending = true;

                }

                if (item.RequestActionFK == null && item.IsDirectManager == true &&
                    user.DirectManagerFK == Manager.ManagerID && request.RequesStatusFK == (int)RequestStatus.Pending)
                {
                    IsPending = true;

                }
                if (item.RequestActionFK == null && item.IsTeamManager == true &&
                  subDepartment.TeamManagerFK == Manager.ManagerID && request.RequesStatusFK == (int)RequestStatus.Pending)
                {
                    IsPending = true;

                }
                if (IsPending && Requests.Where(x => x.RequestID == request.RequestID).Count() == 0)
                {
                    Requests.Add(request);

                }
            }


            return Requests;
        }
        public decimal GetUserOutStandingLeaves(int UserID, int LeaveTypeID)
        {
            decimal OutStandingValue = 0;
            User User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            List<Request> ListRequests = Find(x => x.UserFK == User.UserID).ToList();

            List<Request> Requests = new List<Request>();
            foreach (var item in ListRequests)
            {
                if (item.RequesStatusFK == (int)StaticEnums.RequestStatus.Pending)
                {
                    OutStandingValue = OutStandingValue + item.RequestDuration;
                }
            }
            return OutStandingValue;

        }
        public List<RequestDTO> GetUserProfileRequests(int UserID, DateTime StartDate, DateTime EndDate, int LeaveTypeID, int StatusID)
        {
            List<Request> request = new List<Request>();
            if (StartDate == null && EndDate == null)
            {
                request = Find(x => x.UserFK == UserID && x.CreationDate >= StartDate &&
            x.CreationDate <= EndDate && x.LeaveTypeFK == LeaveTypeID && x.RequesStatusFK == StatusID).ToList();
            }
            else
            {
                request = Find(x => x.UserFK == UserID && x.LeaveTypeFK == LeaveTypeID && x.RequesStatusFK == StatusID).ToList();

            }
            List<RequestDTO> RequestDTO = new List<Tables.RequestDTO>();

            foreach (var item in request)
            {
                RequestDTO.Add(new RequestDTO
                {
                    RequestID = item.RequestID,
                    RequestDate = item.CreationDate,
                    RequestStatus = ((StaticEnums.RequestStatus)Enum.Parse(typeof(StaticEnums.RequestStatus), item.RequesStatusFK.ToString())).ToString()

                });
            }
            return RequestDTO;

        }

        public void RequestApprove(ManagerActionDTO ManagerActionDTO,
            out string Message,
            out bool Success
            
            ,out User Requester,out Request Request,out User UserManager,out bool SendApproveMail


            )
        {
            bool IsDirectManager = false, IsTeamanager = false;
            Success = true;
            SendApproveMail = false;
            List<ApprovalFlowRequestDetail> RequestDetails = new List<ApprovalFlowRequestDetail>();
            int Order = 0;
             UserManager = userBLL.Find(x => x.UserID == ManagerActionDTO.UserID).FirstOrDefault();
            int ManagerID = UserManager.UserID;
            Manager Manager = managerBLL.Find(x => x.ManagerUserFK == ManagerID).FirstOrDefault();
             Request = Find(x => x.RequestID == ManagerActionDTO.RequestID).FirstOrDefault();
            int RequestID = Request.RequestID;
            int UserID =(int) Request.UserFK;
            var OldRequest = XMLObjectConverter.Serialize(Request);
             Requester = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            int? SubDepartmentFK = Requester?.SubDepartmentFK;
            int RequesterID = Requester.UserID;
            int LeaveTypeFK =(int) Request.LeaveTypeFK;
            SubDepartment SubDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == SubDepartmentFK)?.FirstOrDefault() == null ? new SubDepartment() : subDepartmentBLL.Find(x => x.SubDepartmentID == SubDepartmentFK)?.FirstOrDefault();
            Manager TeamManager = managerBLL.Find(x => x.ManagerID == SubDepartment.TeamManagerFK)?.FirstOrDefault() == null ? new Manager() : managerBLL.Find(x => x.ManagerID == SubDepartment.TeamManagerFK)?.FirstOrDefault();
            User TeamManagerUser = userBLL.Find(x => x.UserID == TeamManager.ManagerUserFK)?.FirstOrDefault() == null ? new User() : userBLL.Find(x => x.UserID == TeamManager.ManagerUserFK)?.FirstOrDefault();
            LeaveType RequestLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveTypeFK).FirstOrDefault();
            LeaveTypeAccuralRule LeaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == RequestLeaveType.LeaveTypeID)?.FirstOrDefault();
            LeaveTypeAccuralRule ParentleaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == RequestLeaveType.ParentLeaveTypeFK).FirstOrDefault();

            LeaveType CurrentParentLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == RequestLeaveType.ParentLeaveTypeFK).FirstOrDefault();
            UserEntitlement UserEntitlement;
            DateTime DurationFrom = Request.DurationFrom;
            DateTime DurationTo = Request.DurationTo;
            DateTime DurationFromDate = Request.DurationFrom.Date;
            DateTime DurationToDate = Request.DurationTo.Date;
            if (LeaveTypeAccuralRule?.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.FirstDayOfTheYear)
            {
                 UserEntitlement = userEntitlementBLL.Find(x =>
                 x.LeaveTypeFK ==RequestLeaveType.LeaveTypeID 
                 && x.UserFK == RequesterID && x.IsActive
                 && x.IsDeleted == false 
                 && x.Year == DurationFrom.Year && x.Year == DurationTo.Year
                ).FirstOrDefault();

            }
            else
            {
                 UserEntitlement = userEntitlementBLL.Find(x => 
                    x.LeaveTypeFK ==RequestLeaveType.LeaveTypeID 
                 && x.UserFK == RequesterID && x.IsActive
                 && x.IsDeleted == false 
                 &&DurationFromDate.CompareTo(x.EffectiveDateFrom.Value) >= 0
                 && DurationToDate.CompareTo(x.EffectiveDateTo.Value) <= 0
             ).FirstOrDefault();
            }
                decimal? TotalEntitlement = 0;
            
            if (UserEntitlement==null)
            {
                if (RequestLeaveType.ParentLeaveTypeFK != null)
                {
                    if (ParentleaveTypeAccuralRule?.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.FirstDayOfTheYear)
                    {
                        TotalEntitlement = (userEntitlementBLL.Find(x => 
                           x.LeaveTypeFK == CurrentParentLeaveType.LeaveTypeID 
                        && x.UserFK == RequesterID
                        && x.IsDeleted == false 
                        && x.Year == DurationFrom.Year 
                        && x.Year == DurationTo.Year
                        )?.FirstOrDefault()?.EntitlementTotal) * (100 / RequestLeaveType.DeductionFromParentLeaveDurationPercentage);

                    }
                    else
                    {
                        TotalEntitlement = (userEntitlementBLL.Find(x => 
                           x.LeaveTypeFK == CurrentParentLeaveType.LeaveTypeID 
                        && x.UserFK == RequesterID
                        && x.IsDeleted == false 
                        && DurationFromDate.CompareTo(x.EffectiveDateFrom.Value) >= 0
                        && DurationToDate.CompareTo(x.EffectiveDateTo.Value) <= 0
                        )?.FirstOrDefault()?.EntitlementTotal) * (100 / RequestLeaveType.DeductionFromParentLeaveDurationPercentage);

                    }
                }
            }
            else
            {
                TotalEntitlement = UserEntitlement?.EntitlementTotal;
            }
            if ((TotalEntitlement==null)&& (leaveTypeBLL.Find(x => x.LeaveTypeID == RequestLeaveType.ParentLeaveTypeFK)?.FirstOrDefault().EntitlementTypeFK== (int)LeaveTypeEntitlementType.Unlimited))
            {
                if (Requester.DirectManagerFK == Manager?.ManagerID
      && approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDirectManager == true).Count() > 0)
                {
                    IsDirectManager = true;
                    RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDirectManager == true).FirstOrDefault());
                }
                if (TeamManagerUser != null
               && approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsTeamManager == true).Count() > 0)
                {
                    IsTeamanager = true;
                    RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsTeamManager == true).FirstOrDefault());
                }

                if (approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.ApprovalPersonFK == ManagerID && x.IsTeamManager == false && x.IsDirectManager == false).Count() > 0)
                {
                    RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.ApprovalPersonFK == ManagerID).ToList().Last());
                }
                ApprovalFlowRequestDetail SelectedApprovalFlowRequestDetail = new ApprovalFlowRequestDetail();
                SelectedApprovalFlowRequestDetail = RequestDetails.OrderBy(x => x.Order).Last();
                var OldSelectedApprovalFlowRequestDetail = XMLObjectConverter.Serialize(SelectedApprovalFlowRequestDetail);
                int MaxLevel = approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID).ToList().Last().Order;
                //If Last Manager Approve Request
                if (MaxLevel == SelectedApprovalFlowRequestDetail.Order)
                {
                    Request.RequesStatusFK = (int)StaticEnums.RequestStatus.Approved;
                    Order = SelectedApprovalFlowRequestDetail.Order;
                    if (RequestLeaveType.ParentLeaveTypeFK != null)
                    {
                        if (RequestLeaveType.TakeMaxAmountFromParent == null)
                        {
                            RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                            {
                                UserID = Requester.UserID,
                                RequestID = Request.RequestID,
                                DurationTo = Request.DurationTo,
                                DurationFrom = Request.DurationFrom,
                                LeaveTypeID = RequestLeaveType.ParentLeaveTypeFK,
                                RequestDuration = (decimal)(RequestLeaveType.DeductionFromParentLeaveDurationPercentage / 100) * Request.RequestDuration,
                                UnitID = (int)Request.LeaveDurationUnitFK,
                                RequestBackToworkDate = Request.BackToWork,
                                ModifiedByUserId = ManagerActionDTO.ModifiedByUserId,
                                LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == RequestLeaveType.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                                TotalOffDays = Request.RequestDuration

                            };

                        }

                    }

                    foreach (var item in requestHandlerBLL.Find(x => x.RequestFK == RequestID))
                    {
                        var Olditem = XMLObjectConverter.Serialize(item);
                        item.RquestStatusFK = (int)RequestStatus.Approved;
                        item.IsActive = true;
                        requestHandlerBLL.Edit(item);
                        Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, Olditem, XMLObjectConverter.Serialize(item), "Change In Request Approval Flow");

                    }

                }
                //If middle Manager ( Not Last Manager and Not in The Normal Flow Manager)  Approve Request

                else if (SelectedApprovalFlowRequestDetail.Order > Request.Order)
                {
                    Order = SelectedApprovalFlowRequestDetail.Order;


                }
                // in The Normal Flow Manager)  Approve Request

                else if (SelectedApprovalFlowRequestDetail.Order == Request.Order)
                {
                    Order = Request.Order + 1;


                }

                //Add New Details To Request 
                RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO()
                {
                    RequestID = Request.RequestID,
                    CreationDate = creationDate,
                    RequestDetailsComment = "Approve Request",
                    RequestDetailsTypeID = (int)RequestDetailsTypes.Approve,
                    RequestDetailsTypeName = RequestDetailsTypes.Approve.ToString(),
                    UserID = UserManager.UserID

                };


                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);


                Message = "Request has been Approved Successfully";

                if (CheckIfRequestPendingManagerApproval(Request.RequestID, UserManager.UserID))
                {

                    if (IsDirectManager == true)
                    {
                        SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Approved;
                        SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                        SelectedApprovalFlowRequestDetail.IsActive = false;
                        SelectedApprovalFlowRequestDetail.ApprovalPersonFK = UserManager.UserID;
                        Request.Order = Order;

                    }
                    if (IsTeamanager == true)
                    {
                        SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Approved;
                        SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                        SelectedApprovalFlowRequestDetail.IsActive = false;
                        SelectedApprovalFlowRequestDetail.ApprovalPersonFK = UserManager.UserID;
                        Request.Order = Order;

                    }
                    if (IsDirectManager == false && IsTeamanager == false && SelectedApprovalFlowRequestDetail != null)
                    {
                        SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Approved;
                        SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                        SelectedApprovalFlowRequestDetail.IsActive = false;
                        Request.Order = Order;

                    }

                    approvalFlowRequestDetailsBLL.Edit(SelectedApprovalFlowRequestDetail);
                    Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, OldSelectedApprovalFlowRequestDetail, XMLObjectConverter.Serialize(SelectedApprovalFlowRequestDetail), "Change in Request ApprovalFlow");

                    //
                    Edit(Request);
                    Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, OldRequest, XMLObjectConverter.Serialize(Request), "Edit Request");

                    foreach (var item in approvalFlowRequestDetailsBLL.Find(x => x.Order < Order && x.RequestFK == ManagerActionDTO.RequestID && x.ApprovalRequestFlowDetailsID != SelectedApprovalFlowRequestDetail.ApprovalRequestFlowDetailsID))
                    {
                        var Olditem = XMLObjectConverter.Serialize(item);
                        item.IsActive = false;
                        approvalFlowRequestDetailsBLL.Edit(item);
                        Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, Olditem, XMLObjectConverter.Serialize(item), "Change In Request Approval Flow");

                    }

                }

                //Send Email
                SendApproveMail = true;

            }
          else if ((TotalEntitlement - Request.RequestDuration >= 0) || RequestLeaveType.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Unlimited)
            {
                if (Requester.DirectManagerFK == Manager?.ManagerID
                && approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDirectManager == true).Count() > 0)
                {
                    IsDirectManager = true;
                    RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDirectManager == true).FirstOrDefault());
                }
                if (TeamManagerUser != null
               && approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsTeamManager == true).Count() > 0)
                {
                    IsTeamanager = true;
                    RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsTeamManager == true).FirstOrDefault());
                }

                if (approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.ApprovalPersonFK ==ManagerID && x.IsTeamManager == false && x.IsDirectManager == false).Count() > 0)
                {
                    RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.ApprovalPersonFK == ManagerID).ToList().Last());
                }
                ApprovalFlowRequestDetail SelectedApprovalFlowRequestDetail = new ApprovalFlowRequestDetail();
                if(RequestDetails.Count()>0)
                SelectedApprovalFlowRequestDetail = RequestDetails.OrderBy(x => x.Order).Last();
                var OldSelectedApprovalFlowRequestDetail = XMLObjectConverter.Serialize(SelectedApprovalFlowRequestDetail);
                int MaxLevel = 0;
                    if(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID).ToList().Count()>0)
                    MaxLevel= approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID).ToList().Last().Order;
                //If Last Manager Approve Request
                if (MaxLevel == SelectedApprovalFlowRequestDetail.Order)
                {
                    Request.RequesStatusFK = (int)StaticEnums.RequestStatus.Approved;
                    Order = SelectedApprovalFlowRequestDetail.Order;
                    decimal RequestDuration = 0;
                    if (RequestLeaveType.ParentLeaveTypeFK != null)
                    {
                        if (RequestLeaveType.TakeMaxAmountFromParent == null)
                        {
                            RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                            {
                                UserID = Requester.UserID,
                                RequestID = Request.RequestID,
                                DurationTo = Request.DurationTo,
                                DurationFrom = Request.DurationFrom,
                                LeaveTypeID = RequestLeaveType.ParentLeaveTypeFK,
                                RequestDuration = (decimal)(RequestLeaveType.DeductionFromParentLeaveDurationPercentage / 100) * Request.RequestDuration,
                                UnitID = (int)Request.LeaveDurationUnitFK,
                                RequestBackToworkDate = Request.BackToWork,
                                ModifiedByUserId = ManagerActionDTO.ModifiedByUserId,
                                LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == RequestLeaveType.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                                TotalOffDays= Request.RequestDuration

                            };
                            userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);


                        }
                        else
                        {
                            LeaveType ParentLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == RequestLeaveType.ParentLeaveTypeFK).FirstOrDefault();
                            RequestDuration = (decimal)(RequestLeaveType.DeductionFromParentLeaveDurationPercentage / 100) * Request.RequestDuration;
                            if (userEntitlementBLL.Find(x => x.LeaveTypeFK == LeaveTypeFK).Count() > 0)
                            {
                                RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                                {
                                    UserID = Requester.UserID,
                                    RequestID = Request.RequestID,
                                    DurationTo = Request.DurationTo,
                                    DurationFrom = Request.DurationFrom,
                                    LeaveTypeID = RequestLeaveType.ParentLeaveTypeFK,
                                    RequestDuration = RequestDuration,
                                    UnitID = (int)Request.LeaveDurationUnitFK,
                                    RequestBackToworkDate = Request.BackToWork,
                                    ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                                };
                                userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);
                                RequestEntitlmentDTO RequestParentEntitlmentDTO = new RequestEntitlmentDTO
                                {
                                    UserID = Requester.UserID,
                                    RequestID = Request.RequestID,
                                    DurationTo = Request.DurationTo,
                                    DurationFrom = Request.DurationFrom,
                                    LeaveTypeID = Request.LeaveTypeFK,
                                    RequestDuration = Request.RequestDuration,
                                    UnitID = (int)Request.LeaveDurationUnitFK,
                                    RequestBackToworkDate = Request.BackToWork,
                                    ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                                };
                                userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestParentEntitlmentDTO);
                            }

                            else if (userEntitlementBLL.Find(x => x.LeaveTypeFK == LeaveTypeFK).Count() == 0
                                && RequestLeaveType.TakeMaxAmountFromParent == null && LeaveTypeAccuralRule == null)
                            {
                                RequestEntitlmentDTO RequestParentEntitlmentDTO = new RequestEntitlmentDTO
                                {
                                    UserID = Requester.UserID,
                                    RequestID = Request.RequestID,
                                    DurationTo = Request.DurationTo,
                                    DurationFrom = Request.DurationFrom,
                                    LeaveTypeID = ParentLeaveType.LeaveTypeID,
                                    RequestDuration = Request.RequestDuration,
                                    UnitID = (int)Request.LeaveDurationUnitFK,
                                    RequestBackToworkDate = Request.BackToWork,
                                    ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                                };
                                userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestParentEntitlmentDTO);

                            }
                        }
              



                    }


                    else if (RequestLeaveType.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Unlimited)
                    {
                        RequestDuration = Request.RequestDuration;

                    }
                    else
                    {

                        RequestDuration = Request.RequestDuration;
                        RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                        {
                            UserID = Requester.UserID,
                            RequestID = Request.RequestID,
                            DurationTo = Request.DurationTo,
                            DurationFrom = Request.DurationFrom,
                            LeaveTypeID = Request.LeaveTypeFK,
                            RequestDuration = RequestDuration,
                            UnitID = (int)Request.LeaveDurationUnitFK,
                            RequestBackToworkDate = Request.BackToWork,
                            ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
                        };

                        userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);

                    }
                    foreach (var item in requestHandlerBLL.Find(x => x.RequestFK ==RequestID))
                    {
                        var Olditem = XMLObjectConverter.Serialize(item);
                        item.RquestStatusFK = (int)RequestStatus.Approved;
                        item.IsActive = true;
                        requestHandlerBLL.Edit(item);
                        Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, Olditem, XMLObjectConverter.Serialize(item), "Change In Request Approval Flow");

                    }


                }
                //If middle Manager ( Not Last Manager and Not in The Normal Flow Manager)  Approve Request

                else if (SelectedApprovalFlowRequestDetail.Order > Request.Order)
                {
                    Order = SelectedApprovalFlowRequestDetail.Order;


                }
                // in The Normal Flow Manager)  Approve Request

                else if (SelectedApprovalFlowRequestDetail.Order == Request.Order)
                {
                    Order = Request.Order + 1;


                }

                //Add New Details To Request 
                RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO()
                {
                    RequestID = Request.RequestID,
                    CreationDate = creationDate,
                    RequestDetailsComment = "Approve Request",
                    RequestDetailsTypeID = (int)RequestDetailsTypes.Approve,
                    RequestDetailsTypeName = RequestDetailsTypes.Approve.ToString(),
                    UserID = UserManager.UserID

                };


                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);


                Message = "Request has been Approved Successfully";

                if (CheckIfRequestPendingManagerApproval(Request.RequestID, UserManager.UserID))
                {

                    if (IsDirectManager == true)
                    {
                        SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Approved;
                        SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                        SelectedApprovalFlowRequestDetail.IsActive = false;
                        SelectedApprovalFlowRequestDetail.ApprovalPersonFK = UserManager.UserID;
                        Request.Order = Order;

                    }
                    if (IsTeamanager == true)
                    {
                        SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Approved;
                        SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                        SelectedApprovalFlowRequestDetail.IsActive = false;
                        SelectedApprovalFlowRequestDetail.ApprovalPersonFK = UserManager.UserID;
                         
                        Request.Order = Order;

                    }
                    if (IsDirectManager == false && IsTeamanager == false && SelectedApprovalFlowRequestDetail != null)
                    {
                        SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Approved;
                        SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                        SelectedApprovalFlowRequestDetail.IsActive = false;
                        Request.Order = Order;

                    }

                    approvalFlowRequestDetailsBLL.Edit(SelectedApprovalFlowRequestDetail);
                    Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, OldSelectedApprovalFlowRequestDetail, XMLObjectConverter.Serialize(SelectedApprovalFlowRequestDetail), "Change in Request ApprovalFlow");

                   // 
                    Edit(Request);
                    Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, OldRequest, XMLObjectConverter.Serialize(Request), "Edit Request");

                    foreach (var item in approvalFlowRequestDetailsBLL.Find(x => x.Order < Order && x.RequestFK == ManagerActionDTO.RequestID && x.ApprovalRequestFlowDetailsID != SelectedApprovalFlowRequestDetail.ApprovalRequestFlowDetailsID))
                    {
                        var Olditem = XMLObjectConverter.Serialize(item);
                        item.IsActive = false;
                        approvalFlowRequestDetailsBLL.Edit(item);
                        Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, Olditem, XMLObjectConverter.Serialize(item), "Change In Request Approval Flow");

                    }

                }
                SendApproveMail = true;
                //Send Email

                //try
                //{
                //    if (Requester.UserEmail != "" && Requester.UserEmail != null)
                //    {
                //        MailingDTO MailingDTO = new MailingDTO
                //        {
                //            RequestID = Request.RequestID,
                //            Action = "Approved",

                //            LeaveTypeName = RequestLeaveType.LeaveTypeName,
                //            Duration = Request.RequestDuration,
                //            StartDate = Request.DurationFrom,
                //            EndtDate = Request.DurationTo,
                //            ComeBackDate = Request.BackToWork,
                //            Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                //        };
                //        if (Request.RequesStatusFK == (int)RequestStatus.Approved)
                //        {
                //            MailingDTO.IsFinalApproved = true;
                //        }
                //        MailingDTO.To = new List<string>();
                //        MailingDTO.CC = new List<string>();
                //        MailingDTO.To.Add(Requester.UserEmail);
                //        // MailingDTO.CC.Add(UserManager.UserEmail);
                //        List<User> ListUsersShouldApproveRequest = new List<User>();
                //        ListUsersShouldApproveRequest = GetUserShouldApproveTheRequest(Request);


                //        MailingDTO.CC.AddRange(ListUsersShouldApproveRequest.Select(x => x.UserEmail));
                //        MailingDTO.CC.AddRange(GetUsersApproveTheRequest(Request.RequestID).Select(x => x.UserEmail));
                //        MailingDTO.CC = MailingDTO.CC.Distinct().ToList();

                //        MailingDTO.ActionBy = UserManager.UserName;
                //        MailingDTO.Actionto = Requester.UserName;
                //        MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;
                //        List<User> UserToSendEmail = ListUsersShouldApproveRequest;
                //        for (int i = 0; i < UserToSendEmail.Count(); i++)
                //        {
                //            if (i == 0)
                //            {
                //                MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                //            }
                //            else
                //            {
                //                MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                //            }
                //        }

                //        MailingDTO.ActionTypeID = (int)StaticEnums.ActionType.Approved;

                //        if (configurationBLL.Find(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                //        {
                //            SendEmail(MailingDTO);
                //        }
                //    }
                //}
                ////      RequestDuration= Request.RequestDuration, 
                ////      DurationFrom= Request.DurationFrom,
                ////      DurationTo= Request.DurationTo,
                ////      LeaveTypeID= Request.LeaveTypeFK,
                ////      UserID= Requester.UserID,
                ////      RequestID= Request.RequestID,

                ////};
                ////UserEntitlementBLL.RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);
                //catch (Exception ex)
                //{
                //    ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                //}
            }
            else
            {
                Message = "Request can not be Approved .. There is no enough Limit";
                Success = false;

            }
        }
        public void RequestReject(ManagerActionDTO ManagerActionDTO, out User Requester, out Request Request, out User UserManager, out string RejectionReason)
        {
            bool IsDirectManager = false, IsTeamanager = false;
            List<ApprovalFlowRequestDetail> RequestDetails = new List<ApprovalFlowRequestDetail>();
            int Order = 0;
             UserManager = userBLL.Find(x => x.UserID == ManagerActionDTO.UserID).FirstOrDefault();
            int ManagerID = UserManager.UserID;
            Manager  Manager = managerBLL.Find(x => x.ManagerUserFK == ManagerID).FirstOrDefault();
             Request = Find(x => x.RequestID == ManagerActionDTO.RequestID).FirstOrDefault();
            int UserID = (int)Request.UserFK;

            int LeaveTypeFK = (int)Request.LeaveTypeFK;
            int RequestID = Request.RequestID;
            var OldRequest = XMLObjectConverter.Serialize(Request);
             Requester = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            int? SubDepartmentFK = Requester?.SubDepartmentFK;
            int RequesterID = Requester.UserID;

            SubDepartment SubDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == SubDepartmentFK)?.FirstOrDefault() == null ? new SubDepartment() : subDepartmentBLL.Find(x => x.SubDepartmentID ==SubDepartmentFK)?.FirstOrDefault();
            Manager TeamManager = managerBLL.Find(x => x.ManagerID == SubDepartment.TeamManagerFK)?.FirstOrDefault() == null ? new Manager() : managerBLL.Find(x => x.ManagerID == SubDepartment.TeamManagerFK)?.FirstOrDefault();
            User TeamManagerUser = userBLL.Find(x => x.UserID == TeamManager.ManagerUserFK)?.FirstOrDefault() == null ? new User() : userBLL.Find(x => x.UserID == TeamManager.ManagerUserFK)?.FirstOrDefault();
            LeaveType RequestLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveTypeFK).FirstOrDefault();

            if (Requester.DirectManagerFK == Manager?.ManagerID
            && approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDirectManager == true).Count() > 0)
            {
                IsDirectManager = true;
                RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDirectManager == true).FirstOrDefault());
            }
            if (TeamManagerUser != null
           && approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsTeamManager == true).Count() > 0)
            {
                IsTeamanager = true;
                RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK ==RequestID && x.IsActive == true && x.IsTeamManager == true).FirstOrDefault());
            }

            if (approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.ApprovalPersonFK == ManagerID && x.IsTeamManager == false && x.IsDirectManager == false).Count() > 0)
            {
                RequestDetails.Add(approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.ApprovalPersonFK == ManagerID).ToList().Last());
            }
            ApprovalFlowRequestDetail SelectedApprovalFlowRequestDetail = new ApprovalFlowRequestDetail();
            SelectedApprovalFlowRequestDetail = RequestDetails.OrderBy(x => x.Order).Last();
            var OldSelectedApprovalFlowRequestDetail = XMLObjectConverter.Serialize(SelectedApprovalFlowRequestDetail);
            Request.RequesStatusFK = (int)StaticEnums.RequestStatus.Rejected;
            Order = SelectedApprovalFlowRequestDetail.Order;


            if (CheckIfRequestPendingManagerApproval(Request.RequestID, UserManager.UserID))
            {

                if (IsDirectManager == true)
                {
                    SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Rejected;
                    SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                    SelectedApprovalFlowRequestDetail.IsActive = false;
                    SelectedApprovalFlowRequestDetail.ApprovalPersonFK = UserManager.UserID;
                    Request.Order = Order;

                }
                if (IsTeamanager == true)
                {
                    SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Rejected;
                    SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                    SelectedApprovalFlowRequestDetail.IsActive = false;
                    SelectedApprovalFlowRequestDetail.ApprovalPersonFK = UserManager.UserID;
                    Request.Order = Order;

                }
                if (IsDirectManager == false && IsTeamanager == false && SelectedApprovalFlowRequestDetail != null)
                {
                    SelectedApprovalFlowRequestDetail.RequestActionFK = (int)ActionType.Rejected;
                    SelectedApprovalFlowRequestDetail.ActionDate = creationDate;
                    SelectedApprovalFlowRequestDetail.IsActive = false;
                    Request.Order = Order;

                }

                approvalFlowRequestDetailsBLL.Edit(SelectedApprovalFlowRequestDetail);
                Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, OldSelectedApprovalFlowRequestDetail, XMLObjectConverter.Serialize(SelectedApprovalFlowRequestDetail), "Change in Request ApprovalFlow");


                Edit(Request);
                Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, OldRequest, XMLObjectConverter.Serialize(Request), "Edit Request");

                //
                foreach (var item in approvalFlowRequestDetailsBLL.Find(x => x.Order < Order && x.RequestFK == ManagerActionDTO.RequestID&&x.ApprovalRequestFlowDetailsID!= SelectedApprovalFlowRequestDetail.ApprovalRequestFlowDetailsID))
                {
                    item.IsActive = false;
                    approvalFlowRequestDetailsBLL.Edit(item);
                }
            }

            //Add New Details To Request 
            RequestDetailsDTO RejectDetailsDTO = new RequestDetailsDTO()
            {
                RequestID = Request.RequestID,
                CreationDate = creationDate,
                RequestDetailsComment = "Reject Request",
                RequestDetailsTypeID = (int)RequestDetailsTypes.Reject,
                RequestDetailsTypeName = RequestDetailsTypes.Reject.ToString(),
                UserID = UserManager.UserID

            };
            requestDetailsBLL.AddNewRequestDetails(RejectDetailsDTO);
            RejectionReason = ManagerActionDTO.RejectionReason;
            RequestDetailsDTO RejectionReasonRequestDetails = new RequestDetailsDTO()
            {
                RequestID = Request.RequestID,
                CreationDate = creationDate,
                RequestDetailsComment = "Rejection Reason:" + ManagerActionDTO.RejectionReason,
                RequestDetailsTypeID = (int)RequestDetailsTypes.RejectionReason,
                RequestDetailsTypeName = RequestDetailsTypes.RejectionReason.ToString(),
                UserID = UserManager.UserID

            };
            requestDetailsBLL.AddNewRequestDetails(RejectionReasonRequestDetails);

            foreach (var item in requestHandlerBLL.Find(x => x.RequestFK == RequestID))
            {
                var Olditem = XMLObjectConverter.Serialize(item);
                item.RquestStatusFK = (int)RequestStatus.Rejected;
                item.IsActive = false;
                requestHandlerBLL.Edit(item);
                Logger.LogCypressEvent(Request.RequestID, (int)StaticEnums.LogTypes.EditUserData, (int)ManagerActionDTO.ModifiedByUserId, Olditem, XMLObjectConverter.Serialize(item), "Edit Request Handler");


            }



            //Send Email

        }

        public RequestDetailsView ViewRequestsDetails(int RequestID)
        {
            Request Request = Find(x => x.RequestID == RequestID).FirstOrDefault();
            User User = userBLL.Find(x => x.UserID == Request.UserFK).FirstOrDefault();
            Department Department = departmentBLL.Find(x => x.DepartmentID == User.DepartmentFK).FirstOrDefault();
            SubDepartment SubDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault() == null ? new SubDepartment() : subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault();
            LeaveType LeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == Request.LeaveTypeFK).FirstOrDefault();
            Position Position = PositionBLL.Find(x => x.PositionID == User.PositionFK).FirstOrDefault();
            UserEntitlement userEntitlement;
            decimal?entitlemantTotal=-1;
            bool checkIfEntitlementIsMonthly = leaveTypeBLL.IsMonthlyLeaveType(Request.LeaveTypeFK.Value);
            if (checkIfEntitlementIsMonthly)
            {
                userEntitlement = userEntitlementBLL.Find(ue => ue.LeaveTypeFK == Request.LeaveTypeFK && ue.UserFK == Request.UserFK && ue.EffectiveDateFrom <= Request.DurationFrom && ue.EffectiveDateTo >= Request.DurationTo).FirstOrDefault();

            }
            else
            {
                userEntitlement = userEntitlementBLL.Find(ue => ue.LeaveTypeFK == Request.LeaveTypeFK && ue.UserFK == Request.UserFK).FirstOrDefault();

            }
            if (userEntitlement?.EntitlementTotal!=null)
            {
                entitlemantTotal = userEntitlement?.EntitlementTotal;
            }
            else
            {
            }
            //int SubstituteID =(int) Request.Substitute.Value;
            RequestDetailsView RequestDetailsView = new RequestDetailsView
            {
                BackToWork = Request.BackToWork,
                DurationFrom = Request.DurationFrom,
                DurationTo = Request.DurationTo,
                CreationDate = Request.CreationDate.Value,
                LeaveTypeName = LeaveType.LeaveTypeName,
                UserName = User.LDAPUserName,
                DepartmentName = Department.DepartmentName,
                SubDepartmentName = SubDepartment.SubDepartmentName,
                PositionName = Position.PositionName,
                RequestDuration = Request.RequestDuration,
                RequestStatus = requestStatusBLL.Find(x => x.RequestStatusID == Request.RequesStatusFK.Value).FirstOrDefault().RequestStatusName,
                DurationUnit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString(),
                Comment = Request.Comment != null ? Request.Comment : "",
                Reason = Request.Reason != null ? Request.Reason : "",
                SubstituteName = Request.Substitute != null ? userBLL.Find(x => x.UserID == Request.Substitute).FirstOrDefault().LDAPUserName : "",
                //entitlement
                EntitlementTotal = entitlemantTotal,
                 Order= Request.Order

            };


            return RequestDetailsView;
        }
        public bool CheckIfDay(int LeaveTypeID)
        {
            if ((int)LeaveDurationUnit.Days == leaveTypeBLL.Find(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault().EntitlementTypeFK)
            {
                return true;
            }
            return false;

        }
        public void SendEmail(MailingDTO MailingDTO)
        {

            string MailingDomain = configurationBLL.Find(x => x.ConfigurationKey == "MailingDomain" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailingUserName = configurationBLL.Find(x => x.ConfigurationKey == "MailingUserName" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailingPassword = configurationBLL.Find(x => x.ConfigurationKey == "MailingPassword" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailHeader = configurationBLL.Find(x => x.ConfigurationKey == "MailHeader" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailFooter = configurationBLL.Find(x => x.ConfigurationKey == "MailFooter" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailPictureLogo = configurationBLL.Find(x => x.ConfigurationKey == "MailPictureLogo" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailURL = configurationBLL.Find(x => x.ConfigurationKey == "MailURL" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string AutodiscoverUrl = configurationBLL.Find(x => x.ConfigurationKey == "AutodiscoverUrl" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;


            Mailing Mailing = new Mailing();
            MailingDTO.MailUserName = MailingUserName;
            MailingDTO.MailDomain = MailingDomain;
            MailingDTO.MailPassword = MailingPassword;
            MailingDTO.MailHeader = MailHeader;
            MailingDTO.MailFooter = MailFooter;
            MailingDTO.MailPictureLogo = MailPictureLogo;
            MailingDTO.MailURL = MailURL;
            MailingDTO.AutodiscoverUrl = AutodiscoverUrl;
            Mailing.SendMailForAction(MailingDTO);

        }
        public void SendMailOnRequestingVication(MailingDTO MailingDTO)
        {

            string MailingDomain = configurationBLL.Find(x => x.ConfigurationKey == "MailingDomain" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailingUserName = configurationBLL.Find(x => x.ConfigurationKey == "MailingUserName" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailingPassword = configurationBLL.Find(x => x.ConfigurationKey == "MailingPassword" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailHeader = configurationBLL.Find(x => x.ConfigurationKey == "MailHeader" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailFooter = configurationBLL.Find(x => x.ConfigurationKey == "MailFooter" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailPictureLogo = configurationBLL.Find(x => x.ConfigurationKey == "MailPictureLogo" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string MailURL = configurationBLL.Find(x => x.ConfigurationKey == "MailURL" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
            string AutodiscoverUrl = configurationBLL.Find(x => x.ConfigurationKey == "AutodiscoverUrl" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;


            Mailing Mailing = new Mailing();
            MailingDTO.MailUserName = MailingUserName;
            MailingDTO.MailDomain = MailingDomain;
            MailingDTO.MailPassword = MailingPassword;
            MailingDTO.MailHeader = MailHeader;
            MailingDTO.MailFooter = MailFooter;
            MailingDTO.MailPictureLogo = MailPictureLogo;
            MailingDTO.MailURL = MailURL;
            MailingDTO.AutodiscoverUrl = AutodiscoverUrl;
            Mailing.SendMailOnRequestingVication(MailingDTO);

        }
        public void AddComment(RequestDetailsDTO RequestDetailsDTO)
        {
            //Add Comment To Request 
            RequestDetailsDTO.CreationDate = creationDate;
            RequestDetailsDTO.RequestDetailsComment = "Add Comment";
            RequestDetailsDTO.RequestDetailsTypeID = (int)RequestDetailsTypes.Comment;
            RequestDetailsDTO.RequestDetailsTypeName = RequestDetailsTypes.Comment.ToString();

        }

        public void SaveFilesPath(RequestAttachmentDTO FilesPath)
        {

            Request Request = Find(x => x.RequestID == FilesPath.RequestID).FirstOrDefault();
            RequestDetail RequestDetail = new RequestDetail
            {
                UserFK = Request.UserFK,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate,
                Action = "Upload Attachment",
                RequestDetailsComment = "Upload Attachment",
                RequestDetailsTypeID = (int)RequestDetailsTypes.UploadAttach,
                RequestFK = Request.RequestID,
                RequestDetailsDate = DateTime.Now

            };
            requestDetailsBLL.Add(RequestDetail);
            foreach (var item in FilesPath.AttachmentPath)
                requestAttachmentBLL.Add(new RequestAttachment
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    RequestAttachmentPath = item,
                    RequestAttachmentName = Path.GetFileName(item),
                    RequestDetailsFK = RequestDetail.RequestDetailsID,
                });










        }
        public List<User> GetUserApproveTheRequest(int RequestID)
        {
            List<int?> IDS = approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == false && x.IsDeleted == false).Select(x => x.ApprovalPersonFK).ToList();
            List<User> ListUsers = new List<User>();
            foreach (var item in IDS)
            {

                ListUsers.Add(userBLL.Find(x => x.UserID == item).FirstOrDefault());
            }
            return ListUsers;
        }
        public List<User> GetUserShouldApproveTheRequest(Request Request)
        {
            List<ApprovalFlowRequestDetail> ApprovalFlowDetail = new List<ApprovalFlowRequestDetail>();
            User User = userBLL.Find(x => x.UserID == Request.UserFK).FirstOrDefault();
            List<User> ListManagers = new List<User>();
            int? TeamManagerID = subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault()?.TeamManagerFK;
            int? UserTeamManagerID = managerBLL.Find(x => x.ManagerID == TeamManagerID).FirstOrDefault()?.ManagerUserFK;

            int? ManagerID = User?.DirectManagerFK;
            int? DirectManagerID = managerBLL.Find(x => x.ManagerID == ManagerID).FirstOrDefault()?.ManagerUserFK;
            int NextOrder = 0;

            NextOrder = Request.Order;



            User Manager = new User();
            ApprovalFlowDetail = approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == Request.RequestID && x.Order == NextOrder && x.IsDeleted == false).ToList();

            foreach (var item in ApprovalFlowDetail)
            {
                if (item.ApprovalPersonFK != null)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == item.ApprovalPersonFK).ToList());

                }

                else if (item.IsDirectManager == true)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == DirectManagerID).ToList());

                }

                else if (item.IsTeamManager == true)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == UserTeamManagerID).ToList());

                }

            }
            return ListManagers;
        }
        public List<User> GetUserShowRejectedRequest(Request Request)
        {
            List<ApprovalFlowRequestDetail> ApprovalFlowDetail = new List<ApprovalFlowRequestDetail>();
            User User = userBLL.Find(x => x.UserID == Request.UserFK).FirstOrDefault();
            List<User> ListManagers = new List<User>();
            int? TeamManagerID = subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault()?.TeamManagerFK;
            int? UserTeamManagerID = managerBLL.Find(x => x.ManagerID == TeamManagerID).FirstOrDefault()?.ManagerUserFK;

            int? ManagerID = User?.DirectManagerFK;
            int? DirectManagerID = managerBLL.Find(x => x.ManagerID == ManagerID).FirstOrDefault()?.ManagerUserFK;
            int NextOrder = 0;

            NextOrder = Request.Order;



            User Manager = new User();
            ApprovalFlowDetail = approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == Request.RequestID && x.Order == NextOrder).ToList();

            foreach (var item in ApprovalFlowDetail)
            {
                if (item.ApprovalPersonFK != null)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == item.ApprovalPersonFK).ToList());

                }

                else if (item.IsDirectManager == true)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == DirectManagerID).ToList());

                }

                else if (item.IsTeamManager == true)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == UserTeamManagerID).ToList());

                }

            }
            return ListManagers;
        }
        public List<User> GetUsersApproveTheRequest(int RequestID)
        {
            List<ApprovalFlowRequestDetail> ApprovalFlowDetail = new List<ApprovalFlowRequestDetail>();
            Request request = Find(x => x.RequestID == RequestID).FirstOrDefault();
            User User = userBLL.Find(x => x.UserID == request.UserFK).FirstOrDefault();
            List<User> ListManagers = new List<User>();
            int? TeamManagerID = subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault()?.TeamManagerFK;
            int? UserTeamManagerID = managerBLL.Find(x => x.ManagerID == TeamManagerID).FirstOrDefault()?.ManagerUserFK;

            int? ManagerID = User?.DirectManagerFK;
            int? DirectManagerID = managerBLL.Find(x => x.ManagerID == ManagerID).FirstOrDefault()?.ManagerUserFK;

            User Manager = new User();
            ApprovalFlowDetail = approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestID && x.IsActive == false && x.IsDeleted == false).ToList();

            foreach (var item in ApprovalFlowDetail)
            {
                if (item.ApprovalPersonFK != null)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == item.ApprovalPersonFK).ToList());

                }

                else if (item.IsDirectManager == true)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == DirectManagerID).ToList());

                }

                else if (item.IsTeamManager == true)
                {
                    ListManagers.AddRange(userBLL.Find(x => x.UserID == UserTeamManagerID).ToList());

                }

            }
            return ListManagers;
        }
        public List<AttachmentDTO> GetALLRequestAttachment(int RequestID)
        {
            List<AttachmentDTO> ListAttachment = new List<AttachmentDTO>();

            List<int> ListDetailsIDs = requestDetailsBLL.Find(x => x.RequestFK == RequestID && x.RequestDetailsTypeID == (int)RequestDetailsTypes.UploadAttach).Select(x => x.RequestDetailsID).ToList();
            foreach (var item in ListDetailsIDs)
            {
                var attachmet = requestAttachmentBLL.Find(x => x.RequestDetailsFK == item).ToList();
                foreach (var itemAttach in attachmet)
                    ListAttachment.Add(

                        new AttachmentDTO
                        {
                            RequestAttachmentName = itemAttach.RequestAttachmentName,
                            RequestAttachmentPath = itemAttach.RequestAttachmentPath

                        }
                        );

            }
            return ListAttachment;

        }
        //Search 
        public List<Request> HRApprovedRequests(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        { 
            IQueryable<Request> ListRequests = null;
            if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To == "") 
            {
            var List=requestHandlerBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) && (x.RquestStatusFK == (int)RequestStatus.Approved|| x.RquestStatusFK== (int)RequestStatus.SystemApproved)).Select(x => x.RequestFK).Distinct().ToList();
                ListRequests = Find(x => List.Contains((int)x.RequestID));

            }
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); ;
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
               // ListRequests = Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID 
               // && CreationFrom.Date <= x.DurationFrom && CreationTo.Date >= x.DurationTo);

                var List = requestHandlerBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK)
                && (x.RquestStatusFK == (int)RequestStatus.Approved || x.RquestStatusFK == (int)RequestStatus.SystemApproved)
                && CreationFrom.Date <= x.Offday && CreationTo.Date >= x.Offday
                ).Select(x => x.RequestFK).Distinct().ToList();
                ListRequests = Find(x => List.Contains((int)x.RequestID));


            }
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); ;
                // ListRequests = Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && CreationFrom.Date <= x.DurationFrom);

                var List = requestHandlerBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) &&
                (x.RquestStatusFK == (int)RequestStatus.Approved || x.RquestStatusFK == (int)RequestStatus.SystemApproved)
                && CreationFrom.Date <= x.Offday

                ).Select(x => x.RequestFK).Distinct().ToList();
                ListRequests = Find(x => List.Contains((int)x.RequestID) );

            }

            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
               // ListRequests = Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && CreationTo.Date >= x.DurationTo);

                var List = requestHandlerBLL.Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) 
                && (x.RquestStatusFK == (int)RequestStatus.Approved || x.RquestStatusFK == (int)RequestStatus.SystemApproved)
                  &&  CreationTo.Date >= x.Offday

                ).Select(x => x.RequestFK).Distinct().ToList();
                ListRequests = Find(x => List.Contains((int)x.RequestID) );


            }

            if (ViewRequestsFilterDTO.LeaveTypeID == 0)
            {
                int count = ListRequests.ToList().Count;
                return ListRequests.ToList();
            }
            else
            {
                return ListRequests.Where(x => x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID).ToList();

            }
        }
        public List<Request> HRRejectedRequests(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            List<Request> ListRequests = new List<Request>();

            if (ViewRequestsFilterDTO.From == "" || ViewRequestsFilterDTO.To == "")
            {
                ListRequests = Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID).ToList();

            }
            else
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); ;
                DateTime CreationTo =
                    DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ListRequests = Find(x => ViewRequestsFilterDTO.ListUsers.Contains((int)x.UserFK) && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && CreationFrom.Date <= x.DurationFrom && CreationTo.Date >= x.DurationFrom).ToList();


            }
            List<Request> Requests = new List<Request>();
            foreach (var item in ListRequests)
            {

                if (item.RequesStatusFK == (int)RequestStatus.Rejected)
                    Requests.Add(item);
            }
            return Requests;
        }
        public List<Request> GetALLUserRequests(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            User User = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();
            IQueryable<Request> ListRequests = null;

            TimeSpan UpdateCreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan UpdateCreationToTime = new TimeSpan(23, 59, 59);

            if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
            }
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.DurationFrom.CompareTo(CreationFrom) >= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.DurationFrom.CompareTo(CreationFrom) >= 0);
            }
            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.DurationTo.CompareTo(CreationTo) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.DurationTo.CompareTo(CreationTo) <= 0);
            }
            else if(ViewRequestsFilterDTO.LeaveTypeID != 0)
            {
                ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID);// && CreationFrom.Date <= x.CreationDate.Value.Date && CreationTo.Date >= x.CreationDate.Value.Date).ToList();
            }
            else
            {
                return Find(x => x.UserFK == User.UserID).ToList();
            }

            return ListRequests.ToList();

        }
        public List<Request> UserRequestsApproved(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {

            TimeSpan UpdateCreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan UpdateCreationToTime = new TimeSpan(23, 59, 59);

            if (ViewRequestsFilterDTO.From == null)
            {
                ViewRequestsFilterDTO.From = "";
            }
            if (ViewRequestsFilterDTO.To == null)
            {
                ViewRequestsFilterDTO.To = "";
            }

            User User = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();
            IQueryable<Request> ListRequests = null;
            if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
            }
            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationTo.CompareTo(CreationTo) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationTo.CompareTo(CreationTo) <= 0);
            }
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0);
            }

            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To == "")
            {
                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID);
            }
            else if (ViewRequestsFilterDTO.LeaveTypeID != 0)
            {
                ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID);// && CreationFrom.Date <= x.CreationDate.Value.Date && CreationTo.Date >= x.CreationDate.Value.Date).ToList();
            }
            else
            {
                return Find(x => x.UserFK == User.UserID).ToList();
            }
            int z = ListRequests.ToList().Count;
            return ListRequests.ToList();
        }
        public List<Request> UserRequestsRejected(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {

            TimeSpan UpdateCreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan UpdateCreationToTime = new TimeSpan(23, 59, 59);

            User User = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();
            IQueryable<Request> ListRequests = null;
            if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
            }
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
            {
                //if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0);
                /*else if(ViewRequestsFilterDTO.LeaveTypeID == 0)
                    ListRequests = Find(x => x.UserFK == User.UserID).ToList();*/
            }
            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationTo.CompareTo(CreationTo) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationTo.CompareTo(CreationTo) <= 0);
            }
            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To == "")
            {
                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID);
            }
            else if (ViewRequestsFilterDTO.LeaveTypeID != 0)
            {
                ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID);// && CreationFrom.Date <= x.CreationDate.Value.Date && CreationTo.Date >= x.CreationDate.Value.Date).ToList();
            }
            else
            {
                return Find(x => x.UserFK == User.UserID).ToList();
            }

            return ListRequests.ToList();
        }
        public List<Request> UserRequestsPending(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            TimeSpan UpdateCreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan UpdateCreationToTime = new TimeSpan(23, 59, 59);

            if (ViewRequestsFilterDTO.From == null)
            {
                ViewRequestsFilterDTO.From = "";
            }
            if (ViewRequestsFilterDTO.To == null)
            {
                ViewRequestsFilterDTO.To = "";
            }

            User User = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();
            IQueryable<Request> ListRequests = null;
            if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0 && x.DurationTo.CompareTo(CreationTo) <= 0);
            }
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
            {
                //if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                // ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID).ToList();
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationFromTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationFrom.CompareTo(CreationFrom) >= 0);

                /*else
                {
                    ListRequests = Find(x => x.UserFK == User.UserID).ToList();
                    ListRequests.OrderByDescending(x => x.CreationDate).Take(10).ToList();
                }*/
            }
            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
            {
                //if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                // ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID).ToList();
                DateTime CreationTO = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + UpdateCreationToTime;

                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationTo.CompareTo(CreationTO) <= 0);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && x.DurationTo.CompareTo(CreationTO) <= 0);

                /*else
                {
                    ListRequests = Find(x => x.UserFK == User.UserID).ToList();
                    ListRequests.OrderByDescending(x => x.CreationDate).Take(10).ToList();
                }*/
            }
            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To == "")
            {
                if (ViewRequestsFilterDTO.LeaveTypeID != 0)
                    ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID);
                else
                    ListRequests = Find(x => x.UserFK == User.UserID && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID);
            }
            else if (ViewRequestsFilterDTO.LeaveTypeID != 0)
            {
                ListRequests = Find(x => x.UserFK == User.UserID && x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID);// && CreationFrom.Date <= x.CreationDate.Value.Date && CreationTo.Date >= x.CreationDate.Value.Date).ToList();
            }
            else
            {
                return Find(x => x.UserFK == User.UserID).ToList();
            }
            int z = ListRequests.ToList().Count;
            return ListRequests.ToList();
        }
        public List<Request> GetRejectedRequests(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            User UserManager = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();



            List<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails = new List<ApprovalFlowRequestDetail>();
            if (ViewRequestsFilterDTO.From == "" || ViewRequestsFilterDTO.To == "")
            {
                ApprovalFlowRequestDetails = approvalFlowRequestDetailsBLL.Find(x => x.ApprovalPersonFK == UserManager.UserID).ToList();

            }
            else
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); ;
                DateTime CreationTo =
                    DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ApprovalFlowRequestDetails = approvalFlowRequestDetailsBLL.Find(x =>
                x.ApprovalPersonFK == UserManager.UserID && CreationFrom.Date <= x.CreationDate && CreationTo.Date >= x.CreationDate).ToList();

            }



            List<Request> Requests = new List<Request>();

            List<int> RequestsIDs = ApprovalFlowRequestDetails.Where(x => x.ApprovalPersonFK == UserManager.UserID).Select(x => x.RequestFK).ToList();
            foreach (var itemId in RequestsIDs)
            {
                Requests.AddRange(Find(x => x.RequestID == itemId && x.RequesStatusFK == (int)RequestStatus.Rejected));
            }
            if (ViewRequestsFilterDTO.LeaveTypeID == 0)
            {
                return Requests.ToList();
            }
            else
            {
                return Requests.Where(x => x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID).ToList();
            }

        }
        //pending
        public List<Request> GetApprovedRequests(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            User UserManager = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();

            List<int> ApprovalFlowRequestDetails = new List<int>();

            List<int> RequestsIDs = approvalFlowRequestDetailsBLL.Find(x => x.ApprovalPersonFK == UserManager.UserID&&x.IsActive==false).Select(x=>x.RequestFK).Distinct().ToList();

            List<Request> Requests = new List<Request>();            
            if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To == "")
            {
                foreach (var itemId in RequestsIDs)
                {
                    Requests.AddRange(Find(x => x.RequestID == itemId && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID));
                }
            }
            else if(ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
            {
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); ;
                foreach (var itemId in RequestsIDs)
                {
                    Requests.AddRange(Find(x => x.RequestID == itemId && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID&& CreationFrom.Date <= x.DurationFrom));
                }
            }
            else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationTo =DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                foreach (var itemId in RequestsIDs)
                {
                    Requests.AddRange(Find(x => x.RequestID == itemId && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && CreationTo.Date >= x.DurationTo));
                }


            }
            else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "")
            {
                DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); ;

                foreach (var itemId in RequestsIDs)
                {
                    Requests.AddRange(Find(x => x.RequestID == itemId && x.RequesStatusFK == ViewRequestsFilterDTO.StatusID && CreationTo.Date >= x.DurationTo && CreationFrom.Date <= x.DurationFrom));
                }


            }



            if (ViewRequestsFilterDTO.LeaveTypeID == 0)
            {
                return Requests.ToList();
            }
            else
            {
                return Requests.Where(x => x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID).ToList();
            }

        }
        //pending
        public List<Request> RequestsPendingManagerApproval(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            User user = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();
            Manager Manager = new Manager();
            try
            {
                Manager = managerBLL.Find(x => x.ManagerUserFK == ViewRequestsFilterDTO.UserID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");

            }
            List<Request> Requests = new List<Request>();
            // List<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails = new List<ApprovalFlowRequestDetail>();




            int? UserID = user?.UserID;

            int? SubDepartmentFK = user?.SubDepartmentFK;

           // int? DirectManagerFK = Manager?.ManagerID;
            SubDepartment subDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == SubDepartmentFK)?.FirstOrDefault();
            // int? TeamManagerFK = subDepartment?.TeamManagerFK;
            int? ManagerID = Manager?.ManagerID;

            var ApprovalFlowRequestDetails = approvalFlowRequestDetailsBLL.Find
                (x => x.ApprovalPersonFK == user.UserID && x.RequestActionFK == null&& x.IsActive == true)
                .Select(x => new { RequestActionFK = x.RequestActionFK, Order = x.Order, IsDirectManager = x.IsDirectManager, ApprovalPersonFK = x.ApprovalPersonFK
                , RequestFK = x.RequestFK, IsTeamManager = x.IsTeamManager


                }).ToList();
           
            if (userBLL.Find(x=>x.DirectManagerFK== ManagerID).Count()>0) {
                List<int> RequestedByIDs = userBLL.Find(x => x.DirectManagerFK == ManagerID).Select(x=>x.UserID).ToList();

            ApprovalFlowRequestDetails.AddRange(approvalFlowRequestDetailsBLL.Find
       (x => 
      (x.RequestActionFK == null && x.IsDirectManager == true&& RequestedByIDs.Contains((int)x.RequestedByUserFK))
      
       && x.IsActive == true)
       .Select(x => new {
           RequestActionFK = x.RequestActionFK,
           Order = x.Order,
           IsDirectManager = x.IsDirectManager,
           ApprovalPersonFK = x.ApprovalPersonFK
       ,
           RequestFK = x.RequestFK,
           IsTeamManager = x.IsTeamManager


       }).ToList());

        }
            if (subDepartmentBLL.Find(x => x.TeamManagerFK == ManagerID).Count() > 0)
            {

               List<int> SubdepartmentIDs= subDepartmentBLL.Find(x => x.TeamManagerFK == ManagerID).Select(x => x.SubDepartmentID).ToList();
                List<int> RequestedByIDs=userBLL.Find(x => SubdepartmentIDs.Contains((int)x.SubDepartmentFK)).Select(x => x.UserID).ToList();
                ApprovalFlowRequestDetails.AddRange(approvalFlowRequestDetailsBLL.Find
    (x => x.RequestActionFK == null && x.IsTeamManager == true    && x.IsActive == true && RequestedByIDs.Contains((int)x.RequestedByUserFK))
    .Select(x => new {
        RequestActionFK = x.RequestActionFK,
        Order = x.Order,
        IsDirectManager = x.IsDirectManager,
        ApprovalPersonFK = x.ApprovalPersonFK
    ,
        RequestFK = x.RequestFK,
        IsTeamManager = x.IsTeamManager


    }).ToList());

            }
            
            List<int> RequestIDs = new List<int>();
            Request request = new Request();

            try
            {
             

                    if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To == "")
                {
                    foreach (var item in ApprovalFlowRequestDetails)
                    {
                     request = Find(x => x.RequestID == item.RequestFK && x.RequesStatusFK == (int)RequestStatus.Pending&& x.Order == item.Order).FirstOrDefault();
                     Requests.Add(request);
                        
                    }
                }
                    else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
                {
                    DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    foreach (var item in ApprovalFlowRequestDetails)
                    {
                       
                            request = Find(x => x.RequestID == item.RequestFK && x.RequesStatusFK == (int)RequestStatus.Pending && CreationFrom.Date <= x.DurationFrom).FirstOrDefault();
                            Requests.Add(request);
                        

                    }

                }
                    else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
                {
                    DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    foreach (var item in ApprovalFlowRequestDetails)
                    {
                       
                     

                            request = Find(x => x.RequestID == item.RequestFK && x.RequesStatusFK == (int)RequestStatus.Pending && CreationTo.Date >= x.DurationTo).FirstOrDefault();
                            Requests.Add(request);

                        
                    }

                    }
                    else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To != "")
                {
                    DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    foreach (var item in ApprovalFlowRequestDetails)
                    {
                        request = Find(x => x.RequestID == item.RequestFK && x.RequesStatusFK == (int)RequestStatus.Pending
                        && CreationFrom.Date <=  x.DurationFrom&& CreationTo.Date >= x.DurationTo).FirstOrDefault();
                        Requests.Add(request);
                    }

                }
                
            }
            catch (Exception ex)
            { }



            if (ViewRequestsFilterDTO.LeaveTypeID == null || ViewRequestsFilterDTO.LeaveTypeID == 0)
            {
                return Requests.Where(x=> x != null).ToList();
            }
            else
            {

                return Requests.Where(x => x.LeaveTypeFK == ViewRequestsFilterDTO.LeaveTypeID&& x != null).ToList();
            }
        }
        //pending
        //public List<Request> RequestsPendingManager(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        //{


        //    User User = userBLL.Find(x => x.UserID == ViewRequestsFilterDTO.UserID).FirstOrDefault();
        //    Manager Manager = new Manager();
        //    try
        //    {
        //        Manager = managerBLL.Find(x => x.ManagerUserFK == ViewRequestsFilterDTO.UserID).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
        //    }
        //    List<Request> Requests = new List<Request>();
        //    List<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails = new List<ApprovalFlowRequestDetail>();

        //    if (ViewRequestsFilterDTO.From == null)
        //    {
        //        ViewRequestsFilterDTO.From = "";
        //    }
        //    if (ViewRequestsFilterDTO.To == null)
        //    {
        //        ViewRequestsFilterDTO.To = "";
        //    }
        //    ApprovalFlowRequestDetails =approvalFlowRequestDetailsBLL.
        //        Find(x => (x.ApprovalPersonFK == User.UserID || x.IsDirectManager == true || x.IsTeamManager == true)&& x.IsActive == true)
        //        .Select(x=> new ApprovalFlowRequestDetail { ApprovalPersonFK = x.ApprovalPersonFK, RequestFK = x.RequestFK,
        //            RequestActionFK = x.RequestActionFK, Order = x.Order, IsDirectManager = x.IsDirectManager,
        //            IsTeamManager = x.IsTeamManager
        //        }
        //         ).ToList();

        //    foreach (var item in ApprovalFlowRequestDetails)
        //    {

        //        Request request = new Request();

        //        if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To == "")
        //        {

        //            request = Find(x => x.RequestID == item.RequestFK && x.RequesStatusFK == (int)RequestStatus.Approved).FirstOrDefault();

        //        }
        //        else if (ViewRequestsFilterDTO.From != "" && ViewRequestsFilterDTO.To == "")
        //        {
        //            DateTime CreationFrom = DateTime.ParseExact(ViewRequestsFilterDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        //            request = Find(x => x.RequestID == item.RequestFK && x.RequesStatusFK == (int)RequestStatus.Approved && CreationFrom.Date <= x.DurationFrom).FirstOrDefault();


        //        }
        //        else if (ViewRequestsFilterDTO.From == "" && ViewRequestsFilterDTO.To != "")
        //        {
        //            DateTime CreationTo = DateTime.ParseExact(ViewRequestsFilterDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //            request = Find(x => x.RequestID == item.RequestFK && x.RequesStatusFK == (int)RequestStatus.Approved && CreationTo.Date >= x.DurationFrom).FirstOrDefault();


        //        }

        //        User user = userBLL.Find(x => x.UserID == request.UserFK).FirstOrDefault();
        //        int? ApprovalPersonFK = item?.ApprovalPersonFK;
        //        int? UserID = User?.UserID;
        //        int? SubDepartmentFK = user?.SubDepartmentFK;
        //        int? DirectManagerFK = user?.DirectManagerFK;
        //        SubDepartment subDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == SubDepartmentFK).FirstOrDefault();
        //        int? TeamManagerFK = subDepartment?.TeamManagerFK;

        //        bool IsPending = false;
        //        if (item.RequestActionFK == null && request.Order == item.Order && ApprovalPersonFK == UserID && request.RequesStatusFK == (int)RequestStatus.Pending)
        //        {
        //            IsPending = true;

        //        }

        //        if (item.RequestActionFK == null && request.Order == item.Order && item.IsDirectManager == true &&
        //          DirectManagerFK == Manager?.ManagerID && request.RequesStatusFK == (int)RequestStatus.Pending)
        //        {
        //            IsPending = true;

        //        }
        //        if (item.RequestActionFK == null && request.Order == item.Order && item.IsTeamManager == true &&
        //         TeamManagerFK == Manager?.ManagerID && request.RequesStatusFK == (int)RequestStatus.Pending)
        //        {
        //            IsPending = true;

        //        }
        //        if (IsPending && Requests.Where(x => x.RequestID == request.RequestID).Count() == 0)
        //        {
        //            Requests.Add(request);

        //        }
        //    }
        //    return Requests;
        //}


        public List<Request> RequestsOnTheFlowOfManager(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            List<Request> Requests = new List<Request>();
            Requests.AddRange(RequestsPendingManagerApproval(ViewRequestsFilterDTO));
            ViewRequestsFilterDTO.StatusID = (int)StaticEnums.RequestStatus.Approved;
            Requests.AddRange(GetApprovedRequests(ViewRequestsFilterDTO));
            ViewRequestsFilterDTO.StatusID = (int)StaticEnums.RequestStatus.Rejected;
            Requests.AddRange(GetApprovedRequests(ViewRequestsFilterDTO));
            ViewRequestsFilterDTO.StatusID = (int)StaticEnums.RequestStatus.Deleted;
            Requests.AddRange(GetApprovedRequests(ViewRequestsFilterDTO));
            Requests.OrderBy(x => x.RequestID);
            return Requests.Where(x=>x!=null).ToList();
        }
        public bool CheckIfRequestPendingManagerApproval(int RequestID, int ManagerID)
        {
            ViewRequestsFilterDTO ViewRequestsFilterDTO = new ViewRequestsFilterDTO();
            Request Request = Find(x => x.RequestID == RequestID).FirstOrDefault();
            User Manager = userBLL.Find(x => x.UserID == ManagerID).FirstOrDefault();
            ViewRequestsFilterDTO.UserID = (int)Manager?.UserID;
            ViewRequestsFilterDTO.StatusID =(int) RequestStatus.Pending;
            ViewRequestsFilterDTO.To = "";
            ViewRequestsFilterDTO.From = "";

            // ViewRequestsFilterDTO.From = Request.DurationFrom.Date.ToString();
            //ViewRequestsFilterDTO.To = Request.DurationTo.Date.ToString();

            //ViewRequestsFilterDTO.From= Request.DurationFrom.ToString("dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
            // ViewRequestsFilterDTO.To= Request.DurationFrom.ToString("dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
            if (RequestsPendingManagerApproval(ViewRequestsFilterDTO).Where(x => x.RequestID== Request.RequestID).ToList().Count() > 0)
            {
                return true;
            }
            return false;


        }
        //get all rejection user by id
        public List<RejectionReasonOutputDTO> GetAllRejectionReason(RejectionReasonInputDTO rejectionReasonInput)
        {
            DateTime startDate, endDate;
            string leaveTypeName, managerName;
            RejectionReasonOutputDTO tempRejectionReasonOutputDTO;
            List<RejectionReasonOutputDTO> rejectionReasonOutputDTOList = new List<RejectionReasonOutputDTO>();
            User currentUser = userBLL.Find(u => u.UserID == rejectionReasonInput.RequestUserID).FirstOrDefault();
            var allRequestForThisUser = Find(r => r.RequesStatusFK == rejectionReasonInput.RequestStatus ||r.RequesStatusFK==(int)RequestStatus.SystemRejected);
            if (rejectionReasonInput.RequestUserID != 0)
            {
                 allRequestForThisUser = allRequestForThisUser.Where(r=>r.UserFK == rejectionReasonInput.RequestUserID);
            }
            if (rejectionReasonInput.RequestForm != "" && rejectionReasonInput.RequestTo != "" && rejectionReasonInput.RequestLeaveTypeID != null && rejectionReasonInput.RequestLeaveTypeID != 0)
            {
                startDate = DateTime.ParseExact(rejectionReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(rejectionReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == rejectionReasonInput.RequestLeaveTypeID && (r.DurationFrom >= startDate && r.DurationTo <= endDate));//requestStaus=2=rejection&&leaveTypeId=1,2,3&&start Date=12/1/2018&&end Date=12/1/2018;
            }
            else if (rejectionReasonInput.RequestForm != "" && rejectionReasonInput.RequestTo == "" && (rejectionReasonInput.RequestLeaveTypeID == null || rejectionReasonInput.RequestLeaveTypeID == 0))
            {
                startDate = DateTime.ParseExact(rejectionReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationFrom >= startDate);//requestStaus=2=rejection&&start Date=12/1/2018;
            }
            else if (rejectionReasonInput.RequestForm == "" && rejectionReasonInput.RequestTo != "" && (rejectionReasonInput.RequestLeaveTypeID == null || rejectionReasonInput.RequestLeaveTypeID == 0))
            {
                endDate = DateTime.ParseExact(rejectionReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationTo <= endDate);//requestStaus=2=rejection&&end Date=12/1/2018;
            }
            else if (rejectionReasonInput.RequestForm == "" && rejectionReasonInput.RequestTo == "" && rejectionReasonInput.RequestLeaveTypeID != null && rejectionReasonInput.RequestLeaveTypeID != 0)
            {
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == rejectionReasonInput.RequestLeaveTypeID);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
            }
            else if (rejectionReasonInput.RequestForm != "" && rejectionReasonInput.RequestTo == "" && rejectionReasonInput.RequestLeaveTypeID != null && rejectionReasonInput.RequestLeaveTypeID != 0)
            {
                startDate = DateTime.ParseExact(rejectionReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var leaveType = leaveTypeBLL.Find(l => l.LeaveTypeID == rejectionReasonInput.RequestLeaveTypeID).FirstOrDefault();
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == rejectionReasonInput.RequestLeaveTypeID && r.DurationFrom >= startDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
                leaveTypeName = leaveType.LeaveTypeName;

            }
            else if (rejectionReasonInput.RequestForm != "" && rejectionReasonInput.RequestTo != "" && (rejectionReasonInput.RequestLeaveTypeID == null || rejectionReasonInput.RequestLeaveTypeID == 0))
            {
                startDate = DateTime.ParseExact(rejectionReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(rejectionReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r =>r.DurationFrom >= startDate&& r.DurationTo <= endDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
            }
            else if (rejectionReasonInput.RequestForm == "" && rejectionReasonInput.RequestTo != "" && rejectionReasonInput.RequestLeaveTypeID != null && rejectionReasonInput.RequestLeaveTypeID != 0)
            {
                endDate = DateTime.ParseExact(rejectionReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var leaveType = leaveTypeBLL.Find(l => l.LeaveTypeID == rejectionReasonInput.RequestLeaveTypeID).FirstOrDefault();
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == rejectionReasonInput.RequestLeaveTypeID && r.DurationTo <= endDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
                leaveTypeName = leaveType.LeaveTypeName;

            }
            foreach (var req in allRequestForThisUser)
            {
                RequestDetail requestDetails = requestDetailsBLL.Find(rd => rd.RequestFK == req.RequestID && (rd.RequestDetailsTypeID == 6|| rd.RequestDetailsTypeID == 12)).FirstOrDefault();//rejection reason and system rejection reason
                tempRejectionReasonOutputDTO = new RejectionReasonOutputDTO();

                User mangerThatRejectRequest = userBLL.Find(u => u.UserID == requestDetails.UserFK).FirstOrDefault();
                if(mangerThatRejectRequest==null)
                {
                    managerName = "System";
                    tempRejectionReasonOutputDTO.RejectionReasonComment = requestDetails.RequestDetailsComment;//from RequestDetail //6

                }
                else
                {
                    managerName = mangerThatRejectRequest.UserName;
                    tempRejectionReasonOutputDTO.RejectionReasonComment = requestDetails.RequestDetailsComment.Split(':')[1];//from RequestDetail //6

                }
                leaveTypeName = leaveTypeBLL.Find(l => l.LeaveTypeID == req.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                tempRejectionReasonOutputDTO.ManagerName = managerName;//from RequestDetail //1
                tempRejectionReasonOutputDTO.RequestID = req.RequestID;//2
                tempRejectionReasonOutputDTO.RequestCreateDate = req.CreationDate;//3
                tempRejectionReasonOutputDTO.LeaveTypeName = leaveTypeName;//from variable //8
                tempRejectionReasonOutputDTO.RequestDuration = req.RequestDuration;//9
                tempRejectionReasonOutputDTO.RequestDurationUnitName = leaveTypeDurationUnitDIMBLL.Find(u => u.LeaveDurationUnitID == req.LeaveDurationUnitFK).FirstOrDefault().LeaveDurationUnitName;//10
                if(req.LeaveDurationUnitFK==1)
                {
                    tempRejectionReasonOutputDTO.RequestStartDate =req.DurationFrom.ToString("dd/MM/yyyy");//4
                    tempRejectionReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy");//5
                }
                else
                {
                    tempRejectionReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy hh:mm:ss tt");//4
                    tempRejectionReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy hh:mm:ss tt");//5
                }
                if(rejectionReasonInput.RequestUserID != 0)
                {
                    tempRejectionReasonOutputDTO.UserName = currentUser.UserName;//7
                }
                else
                {
                    tempRejectionReasonOutputDTO.UserName = userBLL.Find(u => u.UserID == req.UserFK).FirstOrDefault().UserName;
                }
                tempRejectionReasonOutputDTO.AccessControlID = userBLL.Find(x => x.UserID == req.UserFK).FirstOrDefault()?.AccessControlUserFK;

                rejectionReasonOutputDTOList.Add(tempRejectionReasonOutputDTO);
            }

            return rejectionReasonOutputDTOList;
        }
        //_____________________________________________________________
        //get all Deleted Request user by id or all user
        public List<DeletedReasonOutputDTO> GetAllDeletedReason(DeletedReasonInputDTO deletedReasonInput)
        {
            DateTime startDate, endDate;
            string leaveTypeName, managerName;
            DeletedReasonOutputDTO tempDeletedReasonOutputDTO;
            List<DeletedReasonOutputDTO> DeletedReasonOutputDTOList = new List<DeletedReasonOutputDTO>();
            User currentUser = userBLL.Find(u => u.UserID == deletedReasonInput.RequestUserID).FirstOrDefault();
            var allRequestForThisUser =Find(r => r.RequesStatusFK == deletedReasonInput.RequestStatus);
            if (deletedReasonInput.RequestUserID != 0)
            {
                allRequestForThisUser = allRequestForThisUser.Where(r => r.UserFK == deletedReasonInput.RequestUserID);
            }
            if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo != "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID && (r.DurationFrom >= startDate && r.DurationTo <= endDate));//requestStaus=2=rejection&&leaveTypeId=1,2,3&&start Date=12/1/2018&&end Date=12/1/2018;
            }
            else if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo == "" && (deletedReasonInput.RequestLeaveTypeID == null || deletedReasonInput.RequestLeaveTypeID == 0))
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationFrom >= startDate);//requestStaus=2=rejection&&start Date=12/1/2018;
            }
            else if (deletedReasonInput.RequestForm == "" && deletedReasonInput.RequestTo != "" && (deletedReasonInput.RequestLeaveTypeID == null || deletedReasonInput.RequestLeaveTypeID == 0))
            {
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationTo <= endDate);//requestStaus=2=rejection&&end Date=12/1/2018;
            }
            else if (deletedReasonInput.RequestForm == "" && deletedReasonInput.RequestTo == "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
            }
            else if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo == "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var leaveType = leaveTypeBLL.Find(l => l.LeaveTypeID == deletedReasonInput.RequestLeaveTypeID).FirstOrDefault();
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID && r.DurationFrom >= startDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
                leaveTypeName = leaveType.LeaveTypeName;

            }
            else if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo != "" && (deletedReasonInput.RequestLeaveTypeID == null || deletedReasonInput.RequestLeaveTypeID == 0))
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationFrom >= startDate && r.DurationTo <= endDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
            }
            else if (deletedReasonInput.RequestForm == "" && deletedReasonInput.RequestTo != "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var leaveType = leaveTypeBLL.Find(l => l.LeaveTypeID == deletedReasonInput.RequestLeaveTypeID).FirstOrDefault();
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID && r.DurationTo <= endDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
                leaveTypeName = leaveType.LeaveTypeName;

            }
            foreach (var req in allRequestForThisUser)
            {
                
                    RequestDetail requestDetails = requestDetailsBLL.Find(rd => rd.RequestFK == req.RequestID && rd.RequestDetailsTypeID == 8).ToList().LastOrDefault();//RequestDetailsTypesDeletedReason=8 RequestDetailsTypesDim
                    User mangerThatRejectRequest = userBLL.Find(u => u.UserID == requestDetails.UserFK).FirstOrDefault();
                    managerName = mangerThatRejectRequest.UserName;
                    leaveTypeName = leaveTypeBLL.Find(l => l.LeaveTypeID == req.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                    tempDeletedReasonOutputDTO = new DeletedReasonOutputDTO();
                    tempDeletedReasonOutputDTO.ManagerName = managerName;//from RequestDetail //1
                    tempDeletedReasonOutputDTO.RequestID = req.RequestID;//2
                    tempDeletedReasonOutputDTO.RequestCreateDate = req.CreationDate;//3
                    var requestDetailsReasonsComments = getAllDeletedReasonsForSpecificRequestById(req.RequestID);
                    foreach (var reqDetailsReasoncoment in requestDetailsReasonsComments)
                    {
                        tempDeletedReasonOutputDTO.DeletedReasonComment += "[" + reqDetailsReasoncoment.RequestStartDate + "  -  " + reqDetailsReasoncoment.RequestCreateDate + "  -  " + reqDetailsReasoncoment.DeletedReasonComment + "],    ";//from RequestDetail //6

                    }
                    tempDeletedReasonOutputDTO.LeaveTypeName = leaveTypeName;//from variable //8
                                                                             //tempDeletedReasonOutputDTO.RequestDuration = req.RequestDuration;//9
                    tempDeletedReasonOutputDTO.RequestDurationUnitName = leaveTypeDurationUnitDIMBLL.Find(u => u.LeaveDurationUnitID == req.LeaveDurationUnitFK).FirstOrDefault().LeaveDurationUnitName;//10
                    var requestStartHistory = requestHistoryBLL.Find(rh => rh.RequestFK == req.RequestID).FirstOrDefault();
                    if(requestStartHistory!=null)
                    {
                        tempDeletedReasonOutputDTO.RequestDuration = requestStartHistory.RequestDuration.Value;//9
                        if (req.LeaveDurationUnitFK == 1)
                        {

                            tempDeletedReasonOutputDTO.RequestStartDate = requestStartHistory.DurationFrom?.ToString("dd/MM/yyyy");//4
                            tempDeletedReasonOutputDTO.RequestEndDate = requestStartHistory.DurationTo?.ToString("dd/MM/yyyy");//5
                                                                                                                               //tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy");//4
                                                                                                                               //tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy");//5
                        }
                        else
                        {
                            tempDeletedReasonOutputDTO.RequestStartDate = requestStartHistory.DurationFrom?.ToString("dd/MM/yyyy hh:mm:ss tt");//4
                            tempDeletedReasonOutputDTO.RequestEndDate = requestStartHistory.DurationTo?.ToString("dd/MM/yyyy hh:mm:ss tt");//5

                            //    tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy hh:mm:ss tt");//4
                            //    tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy hh:mm:ss tt");//5
                        }
                    }
                    else
                    {
                        tempDeletedReasonOutputDTO.RequestDuration = req.RequestDuration;//9
                        if (req.LeaveDurationUnitFK == 1)
                        {

                            tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy");//4
                            tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy");//5
                                                                                                                               //tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy");//4
                                                                                                                               //tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy");//5
                        }
                        else
                        {
                            tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy hh:mm:ss tt");//4
                            tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy hh:mm:ss tt");//5

                            //    tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy hh:mm:ss tt");//4
                            //    tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy hh:mm:ss tt");//5
                        }
                    }
                  
                    if (deletedReasonInput.RequestUserID != 0)
                    {
                        tempDeletedReasonOutputDTO.UserName = currentUser.UserName;//7
                    }
                    else
                    {
                        tempDeletedReasonOutputDTO.UserName = userBLL.Find(u => u.UserID == req.UserFK).FirstOrDefault().UserName;
                    }
                tempDeletedReasonOutputDTO.AccessControlID = userBLL.Find(x => x.UserID == req.UserFK).FirstOrDefault()?.AccessControlUserFK;
                DeletedReasonOutputDTOList.Add(tempDeletedReasonOutputDTO);
                

                }

            return DeletedReasonOutputDTOList;
        }
        //to show this data in pop show
        public List<DeletedReasonOutputDTO> getAllDeletedReasonsForSpecificRequestById(int reqId)
        {
            List<DeletedReasonOutputDTO> allReqDeletedReasons = new List<DeletedReasonOutputDTO>();
            List<string> AllDateOfRequests = new List<string>();
            DeletedReasonOutputDTO tempReqDeletedReason;
            bool durationUnitHours = false;
            if (Find(r => r.RequestID == reqId).FirstOrDefault().LeaveDurationUnitFK == 2)//hours
            {
                durationUnitHours = true;
            }
            var requestDetailsDelationReasonsCommants = requestDetailsBLL.Find(rd => rd.RequestFK == reqId && rd.RequestDetailsTypeID == 9).ToList();
            var requestDetailsDelationReasonsCommantsOffDate = requestDetailsBLL.Find(rd => rd.RequestFK == reqId && rd.RequestDetailsTypeID == 8).ToList();
            // serach about hours
            if (durationUnitHours)
            {
                var requestHandlarsHours = requestHoursHandlerBLL.Find(rhh => rhh.RequestFK == reqId);
                foreach (var requestHandlarHours in requestHandlarsHours)
                {
                    TimeSpan time = new TimeSpan(1,0,0);
                    string Hour = requestHandlarHours.StartOffHour?.ToString(@"hh\:mm\:ss")+" To "+ (requestHandlarHours.StartOffHour.Value+ time);//10:00:00 to 11:00:00
                    bool HourOffCommentFound = false;
                    for (int i = 0; i < requestDetailsDelationReasonsCommants.Count(); i++)
                    {
                        string comment = requestDetailsDelationReasonsCommantsOffDate[i].RequestDetailsComment;
                        //if comment conatins this Hours so delation reason comment forword to this Hours.
                        if (comment.Contains(Hour))
                        {
                            HourOffCommentFound = true;
                            tempReqDeletedReason = new DeletedReasonOutputDTO();
                            tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[i].RequestDetailsID;
                            tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[i].CreationDate;
                            tempReqDeletedReason.RequestStartDate = Hour;
                            tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[i].RequestDetailsComment.Split(':')[1];
                            allReqDeletedReasons.Add(tempReqDeletedReason);
                            break;
                        }
                    }
                    //if Hours not found so last comment forword to  this Hours.
                    if (!HourOffCommentFound && requestDetailsDelationReasonsCommants.Count() != 0)
                    {
                        tempReqDeletedReason = new DeletedReasonOutputDTO();
                        tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].RequestDetailsID;
                        tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].CreationDate;
                        tempReqDeletedReason.RequestStartDate = Hour;
                        tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].RequestDetailsComment.Split(':')[1];
                        allReqDeletedReasons.Add(tempReqDeletedReason);
                    }
                }
            }
            // serach about days
            else
            {
                var requestHandlersDays = requestHandlerBLL.Find(rhd => rhd.RequestFK == reqId);
                foreach (var requestHandlerDay in requestHandlersDays)
                {
                    string DayOff = requestHandlerDay.Offday?.ToString("dd/MM/yyyy");
                    bool DayOffCommentFound = false;
                    for (int i = 0; i < requestDetailsDelationReasonsCommants.Count(); i++)
                    {
                        string comment = requestDetailsDelationReasonsCommantsOffDate[i].RequestDetailsComment;
                        //if comment conatins this day so delation reason comment forword to this day.
                        if (comment.Contains(DayOff))
                        {
                            DayOffCommentFound = true;
                            tempReqDeletedReason = new DeletedReasonOutputDTO();
                            tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[i].RequestDetailsID;
                            tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[i].CreationDate;
                            tempReqDeletedReason.RequestStartDate = DayOff;
                            tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[i].RequestDetailsComment.Split(':')[1];
                            allReqDeletedReasons.Add(tempReqDeletedReason);
                            break;
                        }
                    }
                    //if day not found so last comment forword to  this day.
                    if (!DayOffCommentFound&& requestDetailsDelationReasonsCommants.Count()!=0)
                    {
                        tempReqDeletedReason = new DeletedReasonOutputDTO();
                        tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count-1].RequestDetailsID;
                        tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].CreationDate;
                        tempReqDeletedReason.RequestStartDate = DayOff;
                        tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].RequestDetailsComment.Split(':')[1];
                        allReqDeletedReasons.Add(tempReqDeletedReason);
                    }
                }
            
            }
            return allReqDeletedReasons;
        }
        //_____________________________________________________________
        public List<DeletedReasonOutputDTO> GetAllPartialDeletedReason(DeletedReasonInputDTO deletedReasonInput)
        {
            DateTime startDate, endDate;
            string leaveTypeName, managerName;
            DeletedReasonOutputDTO tempDeletedReasonOutputDTO;
            List<DeletedReasonOutputDTO> DeletedReasonOutputDTOList = new List<DeletedReasonOutputDTO>();
            User currentUser = userBLL.Find(u => u.UserID == deletedReasonInput.RequestUserID).FirstOrDefault();
            //var allRequestForThisUser = Find(r => r.RequesStatusFK == deletedReasonInput.RequestStatus);//5
            var allRequestForThisUser = getAllFullPartialDeletedRequests();
            if (deletedReasonInput.RequestUserID != 0)
            {
                allRequestForThisUser = allRequestForThisUser.Where(r => r.UserFK == deletedReasonInput.RequestUserID);
            }
            if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo != "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID && (r.DurationFrom >= startDate && r.DurationTo <= endDate));//requestStaus=2=rejection&&leaveTypeId=1,2,3&&start Date=12/1/2018&&end Date=12/1/2018;
            }
            else if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo == "" && (deletedReasonInput.RequestLeaveTypeID == null || deletedReasonInput.RequestLeaveTypeID == 0))
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationFrom >= startDate);//requestStaus=2=rejection&&start Date=12/1/2018;
            }
            else if (deletedReasonInput.RequestForm == "" && deletedReasonInput.RequestTo != "" && (deletedReasonInput.RequestLeaveTypeID == null || deletedReasonInput.RequestLeaveTypeID == 0))
            {
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationTo <= endDate);//requestStaus=2=rejection&&end Date=12/1/2018;
            }
            else if (deletedReasonInput.RequestForm == "" && deletedReasonInput.RequestTo == "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
            }
            else if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo == "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var leaveType = leaveTypeBLL.Find(l => l.LeaveTypeID == deletedReasonInput.RequestLeaveTypeID).FirstOrDefault();
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID && r.DurationFrom >= startDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
                leaveTypeName = leaveType.LeaveTypeName;

            }
            else if (deletedReasonInput.RequestForm != "" && deletedReasonInput.RequestTo != "" && (deletedReasonInput.RequestLeaveTypeID == null || deletedReasonInput.RequestLeaveTypeID == 0))
            {
                startDate = DateTime.ParseExact(deletedReasonInput.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allRequestForThisUser = allRequestForThisUser.Where(r => r.DurationFrom >= startDate && r.DurationTo <= endDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
            }
            else if (deletedReasonInput.RequestForm == "" && deletedReasonInput.RequestTo != "" && deletedReasonInput.RequestLeaveTypeID != null && deletedReasonInput.RequestLeaveTypeID != 0)
            {
                endDate = DateTime.ParseExact(deletedReasonInput.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var leaveType = leaveTypeBLL.Find(l => l.LeaveTypeID == deletedReasonInput.RequestLeaveTypeID).FirstOrDefault();
                allRequestForThisUser = allRequestForThisUser.Where(r => r.LeaveTypeFK == deletedReasonInput.RequestLeaveTypeID && r.DurationTo <= endDate);//requestStaus=2=rejection&&leaveTypeId=1,2,3;
                leaveTypeName = leaveType.LeaveTypeName;

            }
            foreach (var req in allRequestForThisUser)
            {
                RequestDetail requestDetails = requestDetailsBLL.Find(rd => rd.RequestFK == req.RequestID && rd.RequestDetailsTypeID == 8).ToList().LastOrDefault();//RequestDetailsTypesDeletedReason=8 RequestDetailsTypesDim
                User mangerThatRejectRequest = userBLL.Find(u => u.UserID == requestDetails.UserFK).FirstOrDefault();
                managerName = mangerThatRejectRequest.UserName;
                leaveTypeName = leaveTypeBLL.Find(l => l.LeaveTypeID == req.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                tempDeletedReasonOutputDTO = new DeletedReasonOutputDTO();
                tempDeletedReasonOutputDTO.ManagerName = managerName;//from RequestDetail //1
                tempDeletedReasonOutputDTO.RequestID = req.RequestID;//2
                tempDeletedReasonOutputDTO.RequestCreateDate = req.CreationDate;//3
                var requestDetailsReasonsComments = getAllPartialDeletedReasonsForSpecificRequestById(req.RequestID);
                foreach (var reqDetailsReasoncoment in requestDetailsReasonsComments)
                {
                    tempDeletedReasonOutputDTO.DeletedReasonComment += "["+ reqDetailsReasoncoment.RequestStartDate + "  -  " + reqDetailsReasoncoment.RequestCreateDate + "  -  " + reqDetailsReasoncoment.DeletedReasonComment + "],    ";//from RequestDetail //6

                }
                tempDeletedReasonOutputDTO.LeaveTypeName = leaveTypeName;//from variable //8
                //tempDeletedReasonOutputDTO.RequestDuration = req.RequestDuration;//9
                tempDeletedReasonOutputDTO.RequestDurationUnitName = leaveTypeDurationUnitDIMBLL.Find(u => u.LeaveDurationUnitID == req.LeaveDurationUnitFK).FirstOrDefault().LeaveDurationUnitName;//10
                var requestStartHistory = requestHistoryBLL.Find(rh => rh.RequestFK == req.RequestID).FirstOrDefault();
                tempDeletedReasonOutputDTO.RequestDuration = requestStartHistory.RequestDuration.Value;//9
                if (req.LeaveDurationUnitFK == 1)
                {

                    tempDeletedReasonOutputDTO.RequestStartDate = requestStartHistory.DurationFrom?.ToString("dd/MM/yyyy");//4
                    tempDeletedReasonOutputDTO.RequestEndDate = requestStartHistory.DurationTo?.ToString("dd/MM/yyyy");//5
                    //tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy");//4
                    //tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy");//5
                }
                else
                {
                    tempDeletedReasonOutputDTO.RequestStartDate = requestStartHistory.DurationFrom?.ToString("dd/MM/yyyy hh:mm:ss tt");//4
                    tempDeletedReasonOutputDTO.RequestEndDate = requestStartHistory.DurationTo?.ToString("dd/MM/yyyy hh:mm:ss tt");//5

                    //    tempDeletedReasonOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy hh:mm:ss tt");//4
                    //    tempDeletedReasonOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy hh:mm:ss tt");//5
                }
                if (deletedReasonInput.RequestUserID != 0)
                {
                    tempDeletedReasonOutputDTO.UserName = currentUser.UserName;//7
                }
                else
                {
                    tempDeletedReasonOutputDTO.UserName = userBLL.Find(u => u.UserID == req.UserFK).FirstOrDefault().UserName;
                }
                tempDeletedReasonOutputDTO.AccessControlID = userBLL.Find(x => x.UserID == req.UserFK).FirstOrDefault()?.AccessControlUserFK;
                DeletedReasonOutputDTOList.Add(tempDeletedReasonOutputDTO);
            }

            return DeletedReasonOutputDTOList;
        }
        //to show this data in pop show
        public List<DeletedReasonOutputDTO> getAllPartialDeletedReasonsForSpecificRequestById(int reqId)
        {
            List<DeletedReasonOutputDTO> allReqDeletedReasons = new List<DeletedReasonOutputDTO>();
            List<string> AllDateOfRequests = new List<string>();
            DeletedReasonOutputDTO tempReqDeletedReason;
            bool durationUnitHours = false;
            if (Find(r => r.RequestID == reqId).FirstOrDefault().LeaveDurationUnitFK == 2)//hours
            {
                durationUnitHours = true;
            }
            var requestDetailsDelationReasonsCommants = requestDetailsBLL.Find(rd => rd.RequestFK == reqId && rd.RequestDetailsTypeID == 9).ToList();
            var requestDetailsDelationReasonsCommantsOffDate = requestDetailsBLL.Find(rd => rd.RequestFK == reqId && rd.RequestDetailsTypeID == 8).ToList();
            // serach about hours
            if (durationUnitHours)
            {
                var requestHandlarsHours = requestHoursHandlerBLL.Find(rhh => rhh.RequestFK == reqId&&rhh.IsDeleted==true);
                foreach (var requestHandlarHours in requestHandlarsHours)
                {
                    TimeSpan time = new TimeSpan(1, 0, 0);
                    string Hour = requestHandlarHours.StartOffHour?.ToString(@"hh\:mm\:ss") + " To " + (requestHandlarHours.StartOffHour.Value + time);//10:00:00 to 11:00:00
                    bool HourOffCommentFound = false;
                    for (int i = 0; i < requestDetailsDelationReasonsCommants.Count(); i++)
                    {
                        string comment = requestDetailsDelationReasonsCommantsOffDate[i].RequestDetailsComment;
                        //if comment conatins this Hours so delation reason comment forword to this Hours.
                        if (comment.Contains(Hour))
                        {
                            HourOffCommentFound = true;
                            tempReqDeletedReason = new DeletedReasonOutputDTO();
                            tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[i].RequestDetailsID;
                            tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[i].CreationDate;
                            tempReqDeletedReason.RequestStartDate = Hour;
                            tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[i].RequestDetailsComment.Split(':')[1];
                            allReqDeletedReasons.Add(tempReqDeletedReason);
                            break;
                        }
                    }
                    //if Hours not found so last comment forword to  this Hours.
                    if (!HourOffCommentFound && requestDetailsDelationReasonsCommants.Count() != 0)
                    {
                        tempReqDeletedReason = new DeletedReasonOutputDTO();
                        tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].RequestDetailsID;
                        tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].CreationDate;
                        tempReqDeletedReason.RequestStartDate = Hour;
                        tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].RequestDetailsComment.Split(':')[1];
                        allReqDeletedReasons.Add(tempReqDeletedReason);
                    }
                }
            }
            // serach about days
            else
            {
                var requestHandlersDays = requestHandlerBLL.Find(rhd => rhd.RequestFK == reqId&&rhd.IsDeleted == true);
                foreach (var requestHandlerDay in requestHandlersDays)
                {
                    string DayOff = requestHandlerDay.Offday?.ToString("dd/MM/yyyy");
                    bool DayOffCommentFound = false;
                    for (int i = 0; i < requestDetailsDelationReasonsCommants.Count(); i++)
                    {
                        string comment = requestDetailsDelationReasonsCommantsOffDate[i].RequestDetailsComment;
                        //if comment conatins this day so delation reason comment forword to this day.
                        if (comment.Contains(DayOff))
                        {
                            DayOffCommentFound = true;
                            tempReqDeletedReason = new DeletedReasonOutputDTO();
                            tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[i].RequestDetailsID;
                            tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[i].CreationDate;
                            tempReqDeletedReason.RequestStartDate = DayOff;
                            tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[i].RequestDetailsComment.Split(':')[1];
                            allReqDeletedReasons.Add(tempReqDeletedReason);
                            break;
                        }
                    }
                    //if day not found so last comment forword to  this day.
                    if (!DayOffCommentFound && requestDetailsDelationReasonsCommants.Count() != 0)
                    {
                        tempReqDeletedReason = new DeletedReasonOutputDTO();
                        tempReqDeletedReason.RequestID = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].RequestDetailsID;
                        tempReqDeletedReason.RequestCreateDate = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].CreationDate;
                        tempReqDeletedReason.RequestStartDate = DayOff;
                        tempReqDeletedReason.DeletedReasonComment = requestDetailsDelationReasonsCommants[requestDetailsDelationReasonsCommants.Count - 1].RequestDetailsComment.Split(':')[1];
                        allReqDeletedReasons.Add(tempReqDeletedReason);
                    }
                }

            }
            return allReqDeletedReasons;
        }
        public IEnumerable<Request>getAllFullPartialDeletedRequests()
        {
            var requestsHandler = requestHandlerBLL.GetAll().Distinct();
            List<Request> allRequestParialdeleted = new List<Request>();
            foreach (var requestHandler in requestsHandler)
            {
                if(requestHandler.DurationUnitFK==1&& requestHandler.IsDeleted==true)//days and  deleted
                {
                    var req = Find( r=>r.RequestID== requestHandler.RequestFK&& r.IsDeleted == false).FirstOrDefault();
                    if(req!=null)
                    {
                        allRequestParialdeleted.Add(req);
                    }
                }
                else if(requestHandler.DurationUnitFK == 2 && requestHandler.RquestStatusFK==5)//hours and parial deleted
                {
                    var req = Find(r => r.RequestID == requestHandler.RequestFK && r.IsDeleted == false).FirstOrDefault();
                    if (req != null)
                    {
                        allRequestParialdeleted.Add(req);
                    }
                }
            }
            return allRequestParialdeleted;

        }
        //_____________________________________________________________
        //panding request report
        public List<PendingRequestOutputDTO> GetAllLateRequestMail(PendingRequestInputDTO pendingMailInputDTO)
        {
            int counter = 0;
            List<PendingRequestOutputDTO> ListLateRequests = new List<PendingRequestOutputDTO>();
            DateTime startDate = DateTime.ParseExact(pendingMailInputDTO.RequestForm, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime endDate = DateTime.ParseExact(pendingMailInputDTO.RequestTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //var ListRequest = Find(x => x.RequesStatusFK == (int)RequestStatus.Pending&& x.DurationFrom >= startDate && x.DurationTo <= endDate);
            var ListRequest = Find(x => x.RequesStatusFK == (int)RequestStatus.Pending);

            PendingRequestOutputDTO tempPendingRequestOutputDTO;
            string leaveTypeName;
            if ((pendingMailInputDTO.RequestLeaveTypeID != null&& pendingMailInputDTO.RequestLeaveTypeID!=0) && pendingMailInputDTO.RequestUserID!=null)
            {
                if(pendingMailInputDTO.RequestUserID!=0)
                {
                    ListRequest = ListRequest.Where(r => r.LeaveTypeFK == pendingMailInputDTO.RequestLeaveTypeID && r.UserFK == pendingMailInputDTO.RequestUserID);//requestStaus=2=rejection&&leaveTypeId=1,2,3&&start Date=12/1/2018&&end Date=12/1/2018;

                }
                else
                {
                    ListRequest = ListRequest.Where(r => r.LeaveTypeFK == pendingMailInputDTO.RequestLeaveTypeID);//requestStaus=2=rejection&&leaveTypeId=1,2,3&&start Date=12/1/2018&&end Date=12/1/2018;

                }
            }
            else if ((pendingMailInputDTO.RequestLeaveTypeID != null && pendingMailInputDTO.RequestLeaveTypeID != 0) && pendingMailInputDTO.RequestUserID == null)
            {
                ListRequest = ListRequest.Where(r => r.LeaveTypeFK == pendingMailInputDTO.RequestLeaveTypeID );//requestStaus=2=rejection&&leaveTypeId=1,2,3&&start Date=12/1/2018&&end Date=12/1/2018;
            }
            else if ((pendingMailInputDTO.RequestLeaveTypeID == null || pendingMailInputDTO.RequestLeaveTypeID == 0) && pendingMailInputDTO.RequestUserID!= null&& pendingMailInputDTO.RequestUserID!=0)
            {
                ListRequest = ListRequest.Where(r => r.UserFK == pendingMailInputDTO.RequestUserID);//requestStaus=2=rejection&&leaveTypeId=1,2,3&&start Date=12/1/2018&&end Date=12/1/2018;
            }
            foreach (var req in ListRequest)
            {
                var reqHndlers = requestHandlerBLL.Find(rq => rq.RequestFK == req.RequestID);
                bool isInRang = false;
                foreach (var reqHndler in reqHndlers)
                {
                    if(reqHndler.Offday>=startDate&&reqHndler.Offday<=endDate)
                    {
                        isInRang = true;
                        break;
                    }
                }
                if (isInRang)
                {
                    if (req.Order == 1)
                    {
                        User currentUser = userBLL.Find(u => u.UserID == req.UserFK).FirstOrDefault();
                       
                        leaveTypeName = leaveTypeBLL.Find(l => l.LeaveTypeID == req.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                        tempPendingRequestOutputDTO = new PendingRequestOutputDTO();
                        tempPendingRequestOutputDTO.AccessControlID = currentUser?.AccessControlUserFK;
                        tempPendingRequestOutputDTO.RequestID = req.RequestID;//1
                        tempPendingRequestOutputDTO.UserName = currentUser.UserName;//2
                        tempPendingRequestOutputDTO.RequestCreateDate = req.CreationDate;//3
                        tempPendingRequestOutputDTO.LeaveTypeName = leaveTypeName;//from variable //4
                        tempPendingRequestOutputDTO.RequestDuration = req.RequestDuration;//7
                        tempPendingRequestOutputDTO.RequestDurationUnitName = leaveTypeDurationUnitDIMBLL.Find(u => u.LeaveDurationUnitID == req.LeaveDurationUnitFK).FirstOrDefault().LeaveDurationUnitName;//8
                        if (req.LeaveDurationUnitFK == 1)
                        {
                            tempPendingRequestOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy");//5
                            tempPendingRequestOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy");//6
                        }
                        else
                        {
                            tempPendingRequestOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy hh:mm:ss tt");//5
                            tempPendingRequestOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy hh:mm:ss tt");//6
                        }
                        var Managers = GetUserShouldApproveTheRequest2(req);
                        counter = 0;
                        foreach (var manager in Managers)
                        {
                            counter++;
                            if (Managers.Count == counter)
                                tempPendingRequestOutputDTO.ManagerName += manager.UserName;//from RequestDetail //9

                            else
                                tempPendingRequestOutputDTO.ManagerName += manager.UserName + "/";//from RequestDetail //9
                        }
                        tempPendingRequestOutputDTO.PendingFrom = req.CreationDate?.ToString("dd/MM/yyyy hh:mm:ss tt");////10
                        ListLateRequests.Add(tempPendingRequestOutputDTO);


                    }
                    else
                    {
                        ApprovalFlowRequestDetail RequestDetails = approvalFlowRequestDetailsBLL.Find
                                (x => x.RequestFK == req.RequestID
                                      && x.Order == req.Order - 1
                                      && x.ActionDate != null).FirstOrDefault();
                        User currentUser = userBLL.Find(u => u.UserID == req.UserFK).FirstOrDefault();

                        leaveTypeName = leaveTypeBLL.Find(l => l.LeaveTypeID == req.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                        tempPendingRequestOutputDTO = new PendingRequestOutputDTO();
                        tempPendingRequestOutputDTO.AccessControlID = currentUser?.AccessControlUserFK;
                        tempPendingRequestOutputDTO.RequestID = req.RequestID;//1
                        tempPendingRequestOutputDTO.UserName = currentUser.UserName;//2
                        tempPendingRequestOutputDTO.RequestCreateDate = req.CreationDate;//3
                        tempPendingRequestOutputDTO.LeaveTypeName = leaveTypeName;//from variable //4
                        tempPendingRequestOutputDTO.RequestDuration = req.RequestDuration;//7
                        tempPendingRequestOutputDTO.RequestDurationUnitName = leaveTypeDurationUnitDIMBLL.Find(u => u.LeaveDurationUnitID == req.LeaveDurationUnitFK).FirstOrDefault().LeaveDurationUnitName;//8
                        if (req.LeaveDurationUnitFK == 1)
                        {
                            tempPendingRequestOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy")/*ToString().Split(' ')[0]*/;//5
                            tempPendingRequestOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy");//6
                        }
                        else
                        {
                            tempPendingRequestOutputDTO.RequestStartDate = req.DurationFrom.ToString("dd/MM/yyyy hh:mm:ss tt");//5
                            tempPendingRequestOutputDTO.RequestEndDate = req.DurationTo.ToString("dd/MM/yyyy hh:mm:ss tt");//6
                        }
                        var Managers = GetUserShouldApproveTheRequest2(req);
                        counter = 0;
                        foreach (var manager in Managers)
                        {
                            counter++;
                            if (Managers.Count == counter)
                                tempPendingRequestOutputDTO.ManagerName += manager.UserName;//from RequestDetail //9

                            else
                                tempPendingRequestOutputDTO.ManagerName += manager.UserName + "/";//from RequestDetail //9
                        }
                        if (RequestDetails != null)
                        {
                            tempPendingRequestOutputDTO.PendingFrom = RequestDetails.ActionDate?.ToString("dd/MM/yyyy hh:mm:ss tt");//10
                            ListLateRequests.Add(tempPendingRequestOutputDTO);
                        }


                    }
                }
            }
            return ListLateRequests;
        }

        public List<User> GetUserShouldApproveTheRequest2(Request Request)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                List<ApprovalFlowRequestDetail> ApprovalFlowDetail = new List<ApprovalFlowRequestDetail>();
                User User = userBLL.Find(x => x.UserID == Request.UserFK).FirstOrDefault();
                List<User> ListManagers = new List<User>();
                int? TeamManagerID = subDepartmentBLL.Find(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault()?.TeamManagerFK;
                int? UserTeamManagerID = managerBLL.Find(x => x.ManagerID == TeamManagerID).FirstOrDefault()?.ManagerUserFK;

                int? ManagerID = User?.DirectManagerFK;
                int? DirectManagerID = managerBLL.Find(x => x.ManagerID == ManagerID).FirstOrDefault()?.ManagerUserFK;
                int NextOrder = 0;

                NextOrder = Request.Order;
                User Manager = new User();
                ApprovalFlowDetail = approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == Request.RequestID && x.Order == NextOrder && x.IsDeleted == false).ToList();

                foreach (var item in ApprovalFlowDetail)
                {
                    if (item.ApprovalPersonFK != null)
                    {
                        ListManagers.AddRange(HRMS.Users.Where(x => x.UserID == item.ApprovalPersonFK).ToList());

                    }

                    else if (item.IsDirectManager == true)
                    {
                        ListManagers.AddRange(HRMS.Users.Where(x => x.UserID == DirectManagerID).ToList());

                    }

                    else if (item.IsTeamManager == true)
                    {
                        ListManagers.AddRange(HRMS.Users.Where(x => x.UserID == UserTeamManagerID).ToList());

                    }

                }
                return ListManagers;
            }
        }
        ////_____________________________________________________________
        //check about request status
        public bool CheckIfRequestIsApproved(int id)
        {
            var request = Find(r => r.RequestID == id && (r.RequesStatusFK==(int)RequestStatus.Approved|| r.RequesStatusFK == (int)RequestStatus.SystemApproved)).FirstOrDefault();
            if (request != null)
                return true;
            else
                return false;
        }

        public void AddNewApprovalFlowForLeaveRequest(Request Request, User User, out bool IsAutoApprove)
        {
            Request.UserFK = User.UserID;
            User = userBLL.Find(x => x.UserID == User.UserID).FirstOrDefault();
            ApprovalFlow ApprovalFlow = approvalFlowBLL.Find(x => x.ApprovalFlowID == User.ApprovalFlowFK).FirstOrDefault();
            List<ApprovalFlowDetail> ApprovalFlowDetails = approvalFlowDetailsBLL.Find(x => x.IsActive == true && x.ApprovalFlowFK == ApprovalFlow.ApprovalFlowID).ToList();
            if (ApprovalFlowDetails.Count() == 0)
            {
                Request.RequesStatusFK = (int)RequestStatus.Approved;
                //  ManagerActionDTO ManagerActionDTO = new ManagerActionDTO();
                string Message = "";
                bool Success = true;
                //ManagerActionDTO.UserID = User.UserID;
                //ManagerActionDTO.ModifiedByUserId = User.UserID;

                //RequestApprove( ManagerActionDTO, out Message, out Success);
                IsAutoApprove = true;

            }
            else if (ApprovalFlowDetails.Where(x => x.LeaveTypeFK == Request.LeaveTypeFK).Count() > 0)
            {

                List<ApprovalFlowDetail> LeaveFlowDetail = ApprovalFlowDetails.Where(x => x.LeaveTypeFK == Request.LeaveTypeFK).ToList();

                foreach (var item in LeaveFlowDetail)
                {
                    approvalFlowRequestDetailsBLL.Add(new ApprovalFlowRequestDetail
                    {
                        ApprovalFlowDetailsFK = item.ApprovalFlowDetailsID,
                        RequestActionFK = null,
                        //  RequestFK = Request.RequestID,
                        IsActive = true,
                        IsDeleted = false,
                        Order = item.Order,
                        SubDepartmentFK = User.SubDepartmentFK,
                        IsDirectManager = item.IsDirectManager,
                        IsTeamManager = item.IsTeamManager,
                        ApprovalPersonFK = item.ApprovalFlowPersonFK,
                        LeaveTypeFK = Request.LeaveTypeFK,
                        RequestedByUserFK = User.UserID,
                        CreationDate = creationDate,
                        Request = Request
                        // User= User


                    });
                }
                IsAutoApprove = false;

            }
            else
            {
                List<ApprovalFlowDetail> LeaveFlowDetail = ApprovalFlowDetails.ToList();

                foreach (var item in LeaveFlowDetail.Where(x => x.LeaveTypeFK == null))
                {
                    approvalFlowRequestDetailsBLL.Add(new ApprovalFlowRequestDetail
                    {
                        ApprovalFlowDetailsFK = item.ApprovalFlowDetailsID,
                        RequestActionFK = null,
                        Request = Request,
                        IsActive = true,
                        IsDeleted = false,
                        Order = item.Order,
                        SubDepartmentFK = User.SubDepartmentFK,
                        IsDirectManager = item.IsDirectManager,
                        IsTeamManager = item.IsTeamManager,
                        ApprovalPersonFK = item.ApprovalFlowPersonFK,
                        CreationDate = creationDate,
                        LeaveTypeFK = Request.LeaveTypeFK,
                        RequestedByUserFK = User.UserID

                    });
                }
                IsAutoApprove = false;


            }

        }
        public void DeletePartialPeriodFromRequestDays(RequestDeletionDTO RequestDeletion)
        {

            bool DeleteAllRequestTime = false;
            decimal RequestDurationVicationItems = 0;
            decimal RequestDurationdeductedfromEntitlment = 0;
            decimal RequestDurationdeductedfromEntitlmentChild = 0;
           // int workingWeekID = 3;
            Request request = Find(x => x.RequestID == RequestDeletion.RequestID).FirstOrDefault();
            User userDeleter = userBLL.Find(x => x.UserID == RequestDeletion.UserDeleterID).FirstOrDefault();
            LeaveType LeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == request.LeaveTypeFK).FirstOrDefault();
            LeaveType RequestLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == request.LeaveTypeFK).FirstOrDefault();
            User Requester = userBLL.Find(x => x.UserID == request.UserFK).FirstOrDefault();
            int workingWeekID =(int) Requester.WorkingWeekFK;
            int WorkingModeID = (int)Requester.WorkingModeFK;
            ManagerActionDTO ManagerActionDTO = new ManagerActionDTO();
            LeaveTypeAccuralRule LeaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == RequestLeaveType.LeaveTypeID)?.FirstOrDefault();
            LeaveTypeAccuralRule ParentLeaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == RequestLeaveType.ParentLeaveTypeFK)?.FirstOrDefault();
            List<RequestHandler> RemaningRequestHandlerDays = new List<RequestHandler>();
            decimal RequestOldDuration = request.RequestDuration;
            requestHistoryBLL.Add(
           new RequestHistory
           {
               BackToWork = request.BackToWork,
               IsActive = true,
               IsDeleted = false,
               Comment = request.Comment,
               CreationDate = creationDate,
               DurationFrom = request.DurationFrom,
               LeaveDurationUnitFK =(int) request.LeaveDurationUnitFK,
               LeaveTypeFK = (int)request.LeaveTypeFK,
               DurationTo = request.DurationTo,
               OnBehalfOfRequesterID = request.OnBehalfOfRequesterID,
               PartialDurationUnitFK = request.PartialDurationUnitFK,
               Reason = request.Reason,
               RequesStatusFK = (int)request.RequesStatusFK,
               RequestDuration = RequestOldDuration,
               RequestFK = request.RequestID,
               Substitute = request.Substitute,
               ModificationDate = request.ModificationDate
           });

            // requestHandlerBLL.Find(x=>x.RequestFK== Request.RequestID)
            if (request.RequesStatusFK == (int)RequestStatus.Approved)
            {
              
                    foreach (var item in RequestDeletion.DeletedDays)
                    {
                        RequestDurationVicationItems = RequestDurationVicationItems + item.RequestHandlerDuration;
                    }
                    var OldrequestHandlerDays = requestHandlerBLL.Find(x => x.RequestFK == RequestDeletion.RequestID&&x.IsActive==true&&x.IsDeleted==false).ToList();
                    var ListOfDeleted = RequestDeletion.DeletedDays;
                    RemaningRequestHandlerDays = OldrequestHandlerDays.Where
                        (p => !ListOfDeleted.Select(x => x.RequestHandlerID).Any(
                        p2 => p2 == p.RequestHandlerID)).ToList()
                        .OrderBy(x => x.Offday).ToList();

                
         
           
                //Delete Days
                decimal TotalVicationDays = 0;

                if (RequestDeletion.DeletedDays != null)
                {
                    foreach (var Dayitem in RequestDeletion.DeletedDays)
                    {
                        if (request.PartialDurationUnitFK != null)
                        {

                            TotalVicationDays = (decimal)leaveTypeMinOneDayDurationBLL.Find(x => x.MinOneDayDurationID == request.PartialDurationUnitFK&& x.IsActive == true && x.IsDeleted == false).FirstOrDefault().MinOneDayDurationValue;


                        }
                        else
                        {
                            foreach (var item in requestHandlerBLL.Find(x => x.RequestFK == RequestDeletion.RequestID && x.IsActive == true && x.IsDeleted == false))
                            {

                                TotalVicationDays = TotalVicationDays+ 1;

                            }
                        }

                        var requestHandleritem = requestHandlerBLL.Find(x => x.RequestHandlerID == Dayitem.RequestHandlerID).FirstOrDefault();
                        requestHandleritem.IsActive = false;
                        requestHandleritem.IsDeleted = true;
                        requestHandleritem.RquestStatusFK = (int)StaticEnums.RequestStatus.Deleted;
                        requestHandlerBLL.Edit(requestHandleritem);
                        if (TotalVicationDays == RequestDurationVicationItems)
                        {
                            request.RequesStatusFK = (int)StaticEnums.RequestStatus.Deleted;
                            DeleteAllRequestTime = true;
                            request.IsDeleted = true;
                            request.IsActive = false;
                        }


                    }
                }
              
                //Set Back To Work Day
                 if (RemaningRequestHandlerDays.Count() > 0)
                {
                    request.DurationFrom = (DateTime)RemaningRequestHandlerDays.FirstOrDefault()?.Offday;
                    request.DurationTo = (DateTime)RemaningRequestHandlerDays.LastOrDefault()?.Offday;
                    Backtoworkdate(RemaningRequestHandlerDays.Select(x => (DateTime)x.Offday).ToList(),
                    request.DurationFrom, request.DurationTo, workingWeekID);
                    DateTime backtoworkdate = new DateTime(); ;
                    List<DateTime> OffDays = new List<DateTime>();
                    List<RequestHandlerHours> OffHours = new List<RequestHandlerHours>();
                    decimal NumberOfOffUnit = 0;
                    OffDays.Add(request.DurationFrom);
                    SetRequestBackToWorkDate(request, out backtoworkdate, out OffDays, out OffHours,
                    out NumberOfOffUnit, LeaveType, workingWeekID, WorkingModeID);
                    request.BackToWork = backtoworkdate;
                    request.RequestDuration = NumberOfOffUnit;
                }
                //Edit Request DATA
                Edit(request);
                //Change Entitlment OF User
                if (RequestLeaveType.ParentLeaveTypeFK != null)
                {
                    if (ParentLeaveTypeAccuralRule == null)
                    {
                    }

                    else
                    {

                        RequestDurationdeductedfromEntitlment =

                            (decimal)(RequestLeaveType.DeductionFromParentLeaveDurationPercentage / 100) * RequestDurationVicationItems;

                        RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                        {
                            UserID = Requester.UserID,
                            RequestID = request.RequestID,
                            DurationTo = request.DurationTo,
                            DurationFrom = request.DurationFrom,
                            LeaveTypeID = RequestLeaveType.ParentLeaveTypeFK,
                            RequestDuration = RequestDurationdeductedfromEntitlment,
                            UnitID = (int)request.LeaveDurationUnitFK,
                            RequestBackToworkDate = request.BackToWork,
                            ModifiedByUserId = userDeleter.UserID,
                            LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == RequestLeaveType.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                            TotalOffDays = RequestOldDuration,
                            LeaveTypeAccuralRuleID = ParentLeaveTypeAccuralRule.LeaveTypeAccuralRuleID,
                            LeaveTypeAccuralPeriodID = ParentLeaveTypeAccuralRule.AccuralPeriodFK,


                        };
                        ReturnEntitlmentOfUser(RequestEntitlmentDTO);
                        if (LeaveType.TakeMaxAmountFromParent != null)
                        {

                            RequestDurationdeductedfromEntitlmentChild = RequestDurationVicationItems;

                            RequestEntitlmentDTO RequestEntitlmentChildDTO = new RequestEntitlmentDTO
                            {
                                UserID = Requester.UserID,
                                RequestID = request.RequestID,
                                DurationTo = request.DurationTo,
                                DurationFrom = request.DurationFrom,
                                LeaveTypeID = RequestLeaveType.LeaveTypeID,
                                RequestDuration = RequestDurationdeductedfromEntitlmentChild,
                                UnitID = (int)request.LeaveDurationUnitFK,
                                RequestBackToworkDate = request.BackToWork,
                                ModifiedByUserId = userDeleter.UserID,
                                LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == RequestLeaveType.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                                TotalOffDays = RequestOldDuration,
                                LeaveTypeAccuralRuleID = ParentLeaveTypeAccuralRule.LeaveTypeAccuralRuleID,
                                LeaveTypeAccuralPeriodID = ParentLeaveTypeAccuralRule.AccuralPeriodFK,


                            };
                            ReturnEntitlmentOfUser(RequestEntitlmentChildDTO);

                        }

                    }

                }
                else
                {
                    if (LeaveTypeAccuralRule == null)
                    {

                    }
                    else
                    {
                        RequestDurationdeductedfromEntitlment = RequestDurationVicationItems;

                        RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                        {
                            UserID = Requester.UserID,
                            RequestID = request.RequestID,
                            DurationTo = request.DurationTo,
                            DurationFrom = request.DurationFrom,
                            LeaveTypeID = RequestLeaveType.LeaveTypeID,
                            RequestDuration = RequestDurationdeductedfromEntitlment,
                            UnitID = (int)request.LeaveDurationUnitFK,
                            RequestBackToworkDate = request.BackToWork,
                            ModifiedByUserId = userDeleter.UserID,
                            LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == RequestLeaveType.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                            TotalOffDays = RequestOldDuration,
                            LeaveTypeAccuralRuleID = LeaveTypeAccuralRule.LeaveTypeAccuralRuleID,
                            LeaveTypeAccuralPeriodID = LeaveTypeAccuralRule.AccuralPeriodFK,


                        };
                        ReturnEntitlmentOfUser(RequestEntitlmentDTO);
                    }
                }
                string RequestDetailsComment="";
                if (DeleteAllRequestTime) {
                     RequestDetailsComment = "Request Is Deleted";
                
                       
                        
                         }
                else
                {
                    RequestDetailsComment = "Partially Delete From Request :  ";

                    for (int i = 0; i < RequestDeletion.DeletedDays.Count(); i++)
                    {
                        if (i == RequestDeletion.DeletedDays.Count() - 1)
                        {
                            RequestDetailsComment = RequestDetailsComment +
                                RequestDeletion.DeletedDays[i].Offday.ToShortDateString();


                        }
                        else
                        {
                            RequestDetailsComment = RequestDetailsComment + RequestDeletion.DeletedDays[i].Offday.ToShortDateString() + ",";
                        }

                    }
                    
                }
                

                RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO
                { CreationDate=creationDate,
                  DetailsCreator= userDeleter.UserName,
                  RequestDetailsComment = RequestDetailsComment,
                  RequestDetailsTypeID = (int)RequestDetailsTypes.DeleteRequest,
                  RequestDetailsTypeName = RequestDetailsTypes.DeleteRequest.ToString(),
                  RequestID= RequestDeletion.RequestID,
                  UserID= userDeleter.UserID
                };


                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);

                RequestDetailsDTO RequestDetailsDeletionReason = new RequestDetailsDTO
                {
                    CreationDate = creationDate,
                    DetailsCreator = userDeleter.UserName,
                    RequestDetailsComment = "Deletion Reason : " + RequestDeletion.DaysDeletionReason,
                    RequestDetailsTypeID = (int)RequestDetailsTypes.DeletionReason,
                    RequestDetailsTypeName = RequestDetailsTypes.DeletionReason.ToString(),
                    RequestID = RequestDeletion.RequestID,
                    UserID = userDeleter.UserID
                };
                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDeletionReason);

            }
        }
        public void DeletePartialPeriodFromRequestHours(RequestDeletionDTO RequestDeletion)
        {

            bool DeleteAllRequestTime = false;
            decimal RequestDurationVicationItems = 0;
            decimal RequestDurationdeductedfromEntitlment = 0;
            Request request = Find(x => x.RequestID == RequestDeletion.RequestID).FirstOrDefault();
            User UserDeleter = userBLL.Find(x => x.UserID == RequestDeletion.UserDeleterID).FirstOrDefault();
            LeaveType LeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == request.LeaveTypeFK).FirstOrDefault();
            LeaveType RequestLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == request.LeaveTypeFK).FirstOrDefault();
            User Requester = userBLL.Find(x => x.UserID == request.UserFK).FirstOrDefault();
            int workingWeekID =(int) Requester.WorkingWeekFK;
            int WorkingModeID = (int)Requester.WorkingModeFK;

            ManagerActionDTO ManagerActionDTO = new ManagerActionDTO();
            LeaveTypeAccuralRule LeaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == RequestLeaveType.LeaveTypeID)?.FirstOrDefault();
            LeaveTypeAccuralRule ParentLeaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == RequestLeaveType.ParentLeaveTypeFK)?.FirstOrDefault();
            List<RequestHoursHandler> RemaningRequestHandlerHours = new List<RequestHoursHandler>();
            List<RequestHoursHandler> DeletedRequestHandlerHours = new List<RequestHoursHandler>();

            // requestHandlerBLL.Find(x=>x.RequestFK== Request.RequestID)
            requestHistoryBLL.Add(
             new RequestHistory
             {
                 BackToWork = request.BackToWork,
                 IsActive = true,
                 IsDeleted = false,
                 Comment = request.Comment,
                 CreationDate = creationDate,
                 DurationFrom = request.DurationFrom,
                 LeaveDurationUnitFK = (int)request.LeaveDurationUnitFK,
                 LeaveTypeFK = (int)request.LeaveTypeFK,
                 DurationTo = request.DurationTo,
                 OnBehalfOfRequesterID = request.OnBehalfOfRequesterID,
                 PartialDurationUnitFK = request.PartialDurationUnitFK,
                 Reason = request.Reason,
                 RequesStatusFK = (int)request.RequesStatusFK,
                 RequestDuration = request.RequestDuration,
                 RequestFK = request.RequestID,
                 Substitute = request.Substitute,
                 ModificationDate = request.ModificationDate
             });

            if (request.RequesStatusFK == (int)RequestStatus.Approved)
            {
                             
                    foreach (var item in RequestDeletion.DeletedTimes)
                    {
                        RequestDurationVicationItems = RequestDurationVicationItems + item.RequestHandlerDuration;
                    DeletedRequestHandlerHours.Add(requestHoursHandlerBLL.Find(x => x.RequestHoursHandlerID == item.RequestHandlerID).FirstOrDefault());
                    }
                    var OldrequestHandlerHours = requestHoursHandlerBLL.Find(x => x.RequestFK == RequestDeletion.RequestID).ToList();
                    var ListOfDeleted = RequestDeletion.DeletedTimes;
                    RemaningRequestHandlerHours = OldrequestHandlerHours.Where(p => !ListOfDeleted.Any
                    (p2 => p2.RequestHandlerID == p.RequestHoursHandlerID)).ToList().OrderBy(x => x.StartOffHour).ToList();

                
                //Change Entitlment OF User
                if (RequestLeaveType.ParentLeaveTypeFK != null)
                {
                    if (ParentLeaveTypeAccuralRule == null)
                    {
                    }

                    else
                    {

                        RequestDurationdeductedfromEntitlment =

                            (decimal)(RequestLeaveType.DeductionFromParentLeaveDurationPercentage / 100) * RequestDurationVicationItems;

                        RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                        {
                            UserID = Requester.UserID,
                            RequestID = request.RequestID,
                            DurationTo = request.DurationTo,
                            DurationFrom = request.DurationFrom,
                            LeaveTypeID = RequestLeaveType.ParentLeaveTypeFK,
                            RequestDuration = RequestDurationdeductedfromEntitlment,
                            UnitID = (int)request.LeaveDurationUnitFK,
                            RequestBackToworkDate = request.BackToWork,
                            ModifiedByUserId = UserDeleter.UserID,
                            LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == RequestLeaveType.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                            TotalOffDays = request.RequestDuration,
                             LeaveTypeAccuralPeriodID= LeaveTypeAccuralRule.AccuralPeriodFK,
                              LeaveTypeAccuralRuleID= LeaveTypeAccuralRule.LeaveTypeAccuralRuleID,

                        };
                        ReturnEntitlmentOfUser(RequestEntitlmentDTO);


                    }

                }
                else
                {
                    if (LeaveTypeAccuralRule== null)
                    { }
                    else
                    {
                        RequestDurationdeductedfromEntitlment = RequestDurationVicationItems;
                        RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
                        {
                            UserID = Requester.UserID,
                            RequestID = request.RequestID,
                            DurationTo = request.DurationTo,
                            DurationFrom = request.DurationFrom,
                            LeaveTypeID = RequestLeaveType.LeaveTypeID,
                            RequestDuration = RequestDurationdeductedfromEntitlment,
                            UnitID = (int)request.LeaveDurationUnitFK,
                            RequestBackToworkDate = request.BackToWork,
                            ModifiedByUserId = UserDeleter.UserID,
                            LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == RequestLeaveType.LeaveTypeID).FirstOrDefault().LeaveTypeName,
                            TotalOffDays = request.RequestDuration,
                            LeaveTypeAccuralPeriodID = LeaveTypeAccuralRule.AccuralPeriodFK,
                            LeaveTypeAccuralRuleID = LeaveTypeAccuralRule.LeaveTypeAccuralRuleID

                        };
                        ReturnEntitlmentOfUser(RequestEntitlmentDTO);
                    }
                }
             
                //Delete Hours
                if (RequestDeletion.DeletedTimes != null)
                {
                    foreach (var Timeitem in RequestDeletion.DeletedTimes)
                    {
                        decimal TotalVicationHours = 0;

                        foreach (var item in requestHoursHandlerBLL.Find(x => x.RequestFK == RequestDeletion.RequestID&&x.IsActive==true&&x.IsDeleted==false))
                        {
                            TotalVicationHours = TotalVicationHours + (decimal)item.Duration;

                        }

                        var requestHandleritem = requestHoursHandlerBLL.Find(x => x.RequestHoursHandlerID == Timeitem.RequestHandlerID).FirstOrDefault();
                        requestHandleritem.IsActive = false;
                        requestHandleritem.IsDeleted = true;
                        requestHoursHandlerBLL.Edit(requestHandleritem);
                        if (TotalVicationHours == RequestDurationVicationItems)
                        {

                            var requestHandleritemDay = requestHandlerBLL.Find(x => x.RequestHandlerID == requestHandleritem.RequestHandlerFK).FirstOrDefault();
                            requestHandleritemDay.IsActive = false;
                            requestHandleritemDay.IsDeleted = true;
                            requestHandleritemDay.RquestStatusFK = (int)StaticEnums.RequestStatus.Deleted;
                            requestHandlerBLL.Edit(requestHandleritemDay);
                            request.RequesStatusFK = (int)StaticEnums.RequestStatus.Deleted;
                            request.IsDeleted = true;
                            request.IsActive = false;
                            DeleteAllRequestTime = true;

                        }
                        else
                        {

                            var requestHandleritemDay = requestHandlerBLL.Find(x => x.RequestHandlerID == requestHandleritem.RequestHandlerFK).FirstOrDefault();
                            //requestHandleritemDay.IsActive = true;
                            //requestHandleritemDay.IsDeleted = false;
                            requestHandleritemDay.RquestStatusFK = (int)StaticEnums.RequestStatus.PartialDeleted;
                            requestHandlerBLL.Edit(requestHandleritemDay);
                        }
                    }
                }
                //Set Back To Work Time
                if (RemaningRequestHandlerHours.Count() > 0)
                {
                    request.DurationFrom = request.DurationFrom.Date + (TimeSpan)RemaningRequestHandlerHours.FirstOrDefault().StartOffHour;
                    request.DurationTo = request.DurationTo.Date + (TimeSpan)RemaningRequestHandlerHours.FirstOrDefault().EndOffHour;
                    //----- Backtoworkdate()


                    DateTime backtoworkdate = new DateTime(); 
                    List<DateTime> OffDays = new List<DateTime>();
                    OffDays.Add(request.DurationFrom);
                    List<RequestHandlerHours> OffHours = new List<RequestHandlerHours>();

                    foreach (var item in RemaningRequestHandlerHours)
                    {
                        OffHours.Add(new RequestHandlerHours
                        {
                            TimeDuration = (decimal)item.Duration,
                            TimeEnd = (TimeSpan)item.EndOffHour,
                            TimeStart = (TimeSpan)item.StartOffHour
                        });
                    }
                    decimal NumberOfOffUnit;
                    SetRequestBackToWorkDate(request, out backtoworkdate, out OffDays, out OffHours, out NumberOfOffUnit, LeaveType, workingWeekID, WorkingModeID);
                    request.BackToWork = backtoworkdate;
                    request.RequestDuration = NumberOfOffUnit;

                }
          
                //Edit Request DATA
                Edit(request);
                string RequestDetailsComment;
                if (DeleteAllRequestTime)
                {
                    RequestDetailsComment = "Request Is Deleted";



                }
                else
                {
                    RequestDetailsComment = "Partially Delete From Request :  ";
                    if (DeletedRequestHandlerHours.Count > 0)
                    {
                        for (int i = 0; i < DeletedRequestHandlerHours.Count(); i++)
                        {
                            if (i == DeletedRequestHandlerHours.Count() - 1)
                            {
                                RequestDetailsComment = RequestDetailsComment
                                    + " From " + DeletedRequestHandlerHours[i].StartOffHour.ToString() + " To "
                                    + DeletedRequestHandlerHours[i].EndOffHour.ToString() + "";


                            }
                            else
                            {

                                RequestDetailsComment = RequestDetailsComment +

                                   " From " + DeletedRequestHandlerHours[i].StartOffHour.ToString() + " To " +
                                   DeletedRequestHandlerHours[i].EndOffHour.ToString() + ",";
                            }
                        }
                    }


                }


                RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO
                {
                    CreationDate = creationDate,
                    DetailsCreator = UserDeleter.UserName,
                    RequestDetailsComment = RequestDetailsComment,
                    RequestDetailsTypeID = (int)RequestDetailsTypes.DeleteRequest,
                    RequestDetailsTypeName = RequestDetailsTypes.DeleteRequest.ToString(),
                    RequestID = RequestDeletion.RequestID,
                    UserID = UserDeleter.UserID
                };


                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);

                RequestDetailsDTO RequestDetailsDeletionReason = new RequestDetailsDTO
                {
                    CreationDate = creationDate,
                    DetailsCreator = UserDeleter.UserName,
                    RequestDetailsComment = "Deletion Reason : " + RequestDeletion.HoursDeletionReason,
                    RequestDetailsTypeID = (int)RequestDetailsTypes.DeletionReason,
                    RequestDetailsTypeName = RequestDetailsTypes.DeletionReason.ToString(),
                    RequestID = RequestDeletion.RequestID,
                    UserID = UserDeleter.UserID
                };
                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDeletionReason);


            }
        }
        public void ReturnEntitlmentOfUser(RequestEntitlmentDTO NewEntitlmentDTO)
        {
             DateTime DurationFromDate=  NewEntitlmentDTO.DurationFrom.Date;
            DateTime DurationToDate = NewEntitlmentDTO.DurationTo.Date;
            UserEntitlement EditUserEntitlement;
            if (NewEntitlmentDTO.LeaveTypeAccuralPeriodID == (int)LeaveTypeAccuralPeriod.FirstDayOfTheYear)
            {
                 EditUserEntitlement = userEntitlementBLL.Find(x => x.LeaveTypeFK == NewEntitlmentDTO.LeaveTypeID &&
                x.UserFK == NewEntitlmentDTO.UserID
                && x.Year == NewEntitlmentDTO.DurationFrom.Year && x.Year == NewEntitlmentDTO.DurationTo.Year).FirstOrDefault();
            }
            else
            {
                EditUserEntitlement = userEntitlementBLL.Find(x => x.LeaveTypeFK == NewEntitlmentDTO.LeaveTypeID &&
                x.UserFK == NewEntitlmentDTO.UserID
                && DurationFromDate.CompareTo(x.EffectiveDateFrom.Value) >= 0
                && DurationToDate.CompareTo(x.EffectiveDateTo.Value) <= 0).FirstOrDefault();

            }
            var OldEditUserEntitlement = XMLObjectConverter.Serialize(EditUserEntitlement);
            decimal OldEntitlementTotal = EditUserEntitlement.EntitlementTotal;

            EditUserEntitlement.EntitlementTotal = EditUserEntitlement.EntitlementTotal + NewEntitlmentDTO.RequestDuration;

            userEntitlementBLL.Edit(EditUserEntitlement);
            Logger.LogCypressEvent(NewEntitlmentDTO.RequestID, (int)StaticEnums.LogTypes.EditUserData,NewEntitlmentDTO.ModifiedByUserId, OldEditUserEntitlement, XMLObjectConverter.Serialize(EditUserEntitlement), "Edit User Entitlement");

            userEntitlementChangesBLL.Add(new UserEntitlementChange
            {
                UserEntitlementFK = EditUserEntitlement.UserEntitlementID,
                Comment = "Delete Request" + " " + NewEntitlmentDTO.LeaveTypeName,
                CreationDate = creationDate,
                LeaveDurationUnitFK = NewEntitlmentDTO.UnitID,
                IsActive = true,
                EntitlementBefore = OldEntitlementTotal,
                EntitlementAfter = EditUserEntitlement.EntitlementTotal,
                EntitlementChangedByFK = (int)EntitlementChangedBy.Request,
                ActionDate = creationDate,
                RequestFK = NewEntitlmentDTO.RequestID,
                RequestDuration = NewEntitlmentDTO.RequestDuration,
                IsDeleted = false,
                DurationFrom = NewEntitlmentDTO.DurationFrom,
                DurationTo = NewEntitlmentDTO.DurationTo,
                BackToWork = NewEntitlmentDTO.RequestBackToworkDate,
                UserFK = NewEntitlmentDTO.UserID,
                Year = creationDate.Year,
                 UserChangeEntitlementFK= NewEntitlmentDTO.ModifiedByUserId




            });

        }
      
        public List<RequestHandlerDTO> GetRequestHandlerRecords(int RequestID)
        {
            if ((int)StaticEnums.LeaveDurationUnit.Hours == Find(x => x.RequestID == RequestID).FirstOrDefault().LeaveDurationUnitFK)
            {
                List<RequestHandlerDTO> ListRequestHandler = new List<RequestHandlerDTO>();
                DateTime requestHandler = (DateTime)requestHandlerBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().Offday;
                foreach (var item in requestHoursHandlerBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDeleted == false).ToList())
                {

                    ListRequestHandler.Add(new RequestHandlerDTO
                    {
                        IsActive = true,
                        Offday = requestHandler.Date + (TimeSpan)item.StartOffHour,
                        RequestHandlerID = item.RequestHoursHandlerID,
                        RequestHandlerDuration = (decimal)item.Duration,
                         DurationType= "Hours"

                    });

                }
                return ListRequestHandler;
            }

            else if (Find(x => x.RequestID == RequestID).FirstOrDefault().PartialDurationUnitFK!=null)
            {
                List<RequestHandlerDTO> ListRequestHandler = new List<RequestHandlerDTO>();
                foreach (var item in requestHandlerBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDeleted == false).ToList())
                {
                    ListRequestHandler.Add(new RequestHandlerDTO
                    {
                        IsActive = true,
                        Offday = (DateTime)item.Offday,
                        RequestHandlerID = item.RequestHandlerID,
                        RequestHandlerDuration =(decimal) item.RequestDuration,
                        DurationType = "Days"

                    });

                }
                return ListRequestHandler;
            }

            else {
                List<RequestHandlerDTO> ListRequestHandler = new List<RequestHandlerDTO>();
                foreach (var item in requestHandlerBLL.Find(x => x.RequestFK == RequestID && x.IsActive == true && x.IsDeleted == false).ToList())
                {
                    ListRequestHandler.Add(new RequestHandlerDTO
                    {
                        IsActive = true,
                        Offday = (DateTime)item.Offday,
                        RequestHandlerID = item.RequestHandlerID,
                        RequestHandlerDuration = 1,
                         DurationType= "Days"

                    });

                }
                return ListRequestHandler;


            }

        }
        public void SendApproveMail(User Requester, Request Request, User UserManager)
        {
           LeaveType RequestLeaveType= leaveTypeBLL.Find(x => x.LeaveTypeID == Request.LeaveTypeFK).FirstOrDefault();
            try
            {
                if (Requester.UserEmail != "" && Requester.UserEmail != null)
                {
                    MailingDTO Mail = new MailingDTO();


                    MailingDTO MailingDTO = new MailingDTO
                    {
                        RequestID = Request.RequestID,
                        Action = "Approved",

                        LeaveTypeName = RequestLeaveType.LeaveTypeName,
                        Duration = Request.RequestDuration,
                        StartDate = Request.DurationFrom,
                        EndtDate = Request.DurationTo,
                        ComeBackDate = Request.BackToWork,
                        Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                    };
                    if (Request.RequesStatusFK == (int)RequestStatus.Approved)
                    {
                        MailingDTO.IsFinalApproved = true;
                    }
                    MailingDTO.To = new List<string>();
                    MailingDTO.CC = new List<string>();
                    MailingDTO.To.Add(Requester.UserEmail);
                    // MailingDTO.CC.Add(UserManager.UserEmail);
                    List<User> ListUsersShouldApproveRequest = new List<User>();
                    ListUsersShouldApproveRequest = GetUserShouldApproveTheRequest(Request);


                    MailingDTO.CC.AddRange(ListUsersShouldApproveRequest.Select(x => x.UserEmail));
                    MailingDTO.CC.AddRange(GetUsersApproveTheRequest(Request.RequestID).Select(x => x.UserEmail));
                    MailingDTO.CC = MailingDTO.CC.Distinct().ToList();

                    MailingDTO.ActionBy = UserManager.UserName;
                    MailingDTO.Actionto = Requester.UserName;
                    MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;
                    List<User> UserToSendEmail = ListUsersShouldApproveRequest;
                    for (int i = 0; i < UserToSendEmail.Count(); i++)
                    {
                        if (i == 0)
                        {
                            MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                        }
                        else
                        {
                            MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                        }
                    }

                    MailingDTO.ActionTypeID = (int)StaticEnums.ActionType.Approved;

                    if (configurationBLL.Find(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                    {
                        SendEmail(MailingDTO);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
            }




        }

        public void SendRejectMAil(User Requester, Request Request, User UserManager,string RejectionReason)
        {
            LeaveType RequestLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == Request.LeaveTypeFK).FirstOrDefault();

            try
            {
                if (Requester.UserEmail != "" && Requester.UserEmail != null)
                {
                    MailingDTO MailingDTO = new MailingDTO
                    {
                        RequestID = Request.RequestID,
                        Action = "Rejected",

                        LeaveTypeName = RequestLeaveType.LeaveTypeName,
                        Duration = Request.RequestDuration,
                        StartDate = Request.DurationFrom,
                        EndtDate = Request.DurationTo,
                        ComeBackDate = Request.BackToWork,
                        Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                                ,

                    };

                    MailingDTO.To = new List<string>();
                    MailingDTO.CC = new List<string>();
                    MailingDTO.To.Add(Requester.UserEmail);
                    // MailingDTO.CC.Add(UserManager.UserEmail);
                    List<User> ListUsersShouldApproveRequest = new List<User>();
                    ListUsersShouldApproveRequest = GetUserShowRejectedRequest(Request);


                    MailingDTO.CC.AddRange(ListUsersShouldApproveRequest.Select(x => x.UserEmail));
                    MailingDTO.CC.AddRange(GetUsersApproveTheRequest(Request.RequestID).Select(x => x.UserEmail));
                    MailingDTO.CC = MailingDTO.CC.Distinct().ToList();
                    MailingDTO.ActionBy = UserManager.UserName;
                    MailingDTO.Actionto = Requester.UserName;
                    MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;
                    List<User> UserToSendEmail = ListUsersShouldApproveRequest;
                    for (int i = 0; i < UserToSendEmail.Count(); i++)
                    {
                        if (i == 0)
                        {
                            MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                        }
                        else
                        {
                            MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                        }
                    }


                    MailingDTO.ActionTypeID = (int)StaticEnums.ActionType.Rejected;
                    MailingDTO.RejectionReason = RejectionReason;

                    if (configurationBLL.Find(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                    {
                        SendEmail(MailingDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
            }

        }

        //public List<ApprovedBySystemDTO> ApprovedBySystem( int LeaveTypeID)
        //{
        //        List<Request> Requests = Find(x =>x.IsActive == true
        //                  && x.RequesStatusFK == (int)RequestStatus.Pending
        //                  && x.IsDeleted == false&& x.LeaveTypeFK== LeaveTypeID
        //               ).ToList();

        //        List<ApprovedBySystemDTO> RequestApprovedBySystem = new List<ApprovedBySystemDTO>();

        //        foreach (var item in Requests)
        //        {
        //        try
        //        {
        //            LeaveType leavetype = leaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault();
        //            LeaveTypeAccuralRule leaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == item.LeaveTypeFK).FirstOrDefault();
        //            LeaveTypeAccuralRule ParentleaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == leavetype.ParentLeaveTypeFK).FirstOrDefault();
        //            List<ApprovalFlowRequestDetail> ApprovalFlowDetailRequests = approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == item.RequestID).ToList();
        //            DateTime DurationFrom = item.DurationFrom;
        //            DateTime DurationTo = item.DurationTo;
        //            DateTime DurationFromDate = item.DurationFrom.Date;
        //            DateTime DurationToDate = item.DurationTo.Date;
        //            decimal? TotalLeaveTypeEntitlement;
        //            bool ISMonthlyUnlimitedEntitlementType = false;

        //            if (leavetype.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Unlimited
        //              && (leaveTypeAccuralRule.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.RepeatEveryMonthAt
        //              || ParentleaveTypeAccuralRule.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.RepeatEveryMonthAt
        //              ))
        //            {
        //                ISMonthlyUnlimitedEntitlementType = true;
        //            }


        //            if (ISMonthlyUnlimitedEntitlementType)
        //            {


        //                if (leavetype.ParentLeaveTypeFK == null)
        //                {
        //                    TotalLeaveTypeEntitlement =
        //                    userEntitlementBLL.Find(x =>
        //                       x.UserFK == item.UserFK
        //                    && x.LeaveTypeFK == leavetype.ParentLeaveTypeFK
        //                    && x.IsActive == true
        //                    && x.IsDeleted == false
        //                    && x.Year == DurationFrom.Year
        //                    && x.Year == DurationTo.Year)?.FirstOrDefault()?.EntitlementTotal;
        //                }
        //                else
        //                {
        //                    TotalLeaveTypeEntitlement = userEntitlementBLL.Find(x =>
        //                       x.UserFK == item.UserFK
        //                    && x.LeaveTypeFK == leavetype.LeaveTypeID
        //                    && x.IsActive == true
        //                    && x.IsDeleted == false
        //                    && x.Year == DurationFrom.Year
        //                    && x.Year == DurationTo.Year)?.FirstOrDefault()?.EntitlementTotal;
        //                }


        //                //System Auto Approve Request
        //                if (TotalLeaveTypeEntitlement >= item.RequestDuration)
        //                {
        //                    item.RequesStatusFK = (int)RequestStatus.SystemApproved;
        //                    item.Order = ApprovalFlowDetailRequests.Last().Order;
        //                    Edit(item);

        //                    foreach (var ApprovalFlowDetailRequestsItems in ApprovalFlowDetailRequests)
        //                    {
        //                        ApprovalFlowDetailRequestsItems.IsActive = false;
        //                        ApprovalFlowDetailRequestsItems.RequestActionFK = (int)ActionType.SystemApprove;
        //                        approvalFlowRequestDetailsBLL.Edit(ApprovalFlowDetailRequestsItems);
        //                    }

        //                    //Change Entitlment Of User
        //                    if (leavetype.ParentLeaveTypeFK != null)
        //                    {
        //                        if (leavetype.TakeMaxAmountFromParent == null)
        //                        {
        //                            RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
        //                            {
        //                                UserID = (int)item.UserFK,
        //                                RequestID = item.RequestID,
        //                                DurationTo = item.DurationTo,
        //                                DurationFrom = item.DurationFrom,
        //                                LeaveTypeID = leavetype.ParentLeaveTypeFK,
        //                                RequestDuration = (decimal)(leavetype.DeductionFromParentLeaveDurationPercentage / 100) * item.RequestDuration,
        //                                UnitID = (int)item.LeaveDurationUnitFK,
        //                                RequestBackToworkDate = item.BackToWork,
        //                                ModifiedByUserId = 0,// ManagerActionDTO.ModifiedByUserId,
        //                                LeaveTypeName = leaveTypeBLL.GetAll().Where(p => p.LeaveTypeID == leavetype.LeaveTypeID).FirstOrDefault().LeaveTypeName,
        //                                TotalOffDays = item.RequestDuration

        //                            };
        //                            userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);


        //                        }
        //                        else
        //                        {
        //                            LeaveType ParentLeaveType = leaveTypeBLL.Find(x => x.LeaveTypeID == leavetype.ParentLeaveTypeFK).FirstOrDefault();
        //                            decimal RequestDuration = (decimal)(leavetype.DeductionFromParentLeaveDurationPercentage / 100) * item.RequestDuration;
        //                            if (userEntitlementBLL.Find(x => x.LeaveTypeFK == item.LeaveTypeFK).Count() > 0)
        //                            {
        //                                RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
        //                                {
        //                                    UserID = (int)item.UserFK,
        //                                    RequestID = item.RequestID,
        //                                    DurationTo = item.DurationTo,
        //                                    DurationFrom = item.DurationFrom,
        //                                    LeaveTypeID = leavetype.ParentLeaveTypeFK,
        //                                    RequestDuration = RequestDuration,
        //                                    UnitID = (int)item.LeaveDurationUnitFK,
        //                                    RequestBackToworkDate = item.BackToWork,
        //                                    //ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
        //                                };
        //                                userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);
        //                                RequestEntitlmentDTO RequestParentEntitlmentDTO = new RequestEntitlmentDTO
        //                                {
        //                                    UserID = (int)item.UserFK,
        //                                    RequestID = item.RequestID,
        //                                    DurationTo = item.DurationTo,
        //                                    DurationFrom = item.DurationFrom,
        //                                    LeaveTypeID = item.LeaveTypeFK,
        //                                    RequestDuration = item.RequestDuration,
        //                                    UnitID = (int)item.LeaveDurationUnitFK,
        //                                    RequestBackToworkDate = item.BackToWork,
        //                                    // ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
        //                                };
        //                                userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestParentEntitlmentDTO);
        //                            }

        //                            else if (userEntitlementBLL.Find(x => x.LeaveTypeFK == item.LeaveTypeFK).Count() == 0
        //                                && leavetype.TakeMaxAmountFromParent == null && leaveTypeAccuralRule == null)
        //                            {
        //                                RequestEntitlmentDTO RequestParentEntitlmentDTO = new RequestEntitlmentDTO
        //                                {
        //                                    UserID = (int)item.UserFK,
        //                                    RequestID = item.RequestID,
        //                                    DurationTo = item.DurationTo,
        //                                    DurationFrom = item.DurationFrom,
        //                                    LeaveTypeID = ParentLeaveType.LeaveTypeID,
        //                                    RequestDuration = item.RequestDuration,
        //                                    UnitID = (int)item.LeaveDurationUnitFK,
        //                                    RequestBackToworkDate = item.BackToWork,
        //                                    //  ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
        //                                };
        //                                userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestParentEntitlmentDTO);

        //                            }
        //                        }
        //                    }
        //                    else
        //                    {

        //                        decimal RequestDuration = item.RequestDuration;
        //                        RequestEntitlmentDTO RequestEntitlmentDTO = new RequestEntitlmentDTO
        //                        {
        //                            UserID = (int)item.UserFK,
        //                            RequestID = item.RequestID,
        //                            DurationTo = item.DurationTo,
        //                            DurationFrom = item.DurationFrom,
        //                            LeaveTypeID = item.LeaveTypeFK,
        //                            RequestDuration = RequestDuration,
        //                            UnitID = (int)item.LeaveDurationUnitFK,
        //                            RequestBackToworkDate = item.BackToWork,
        //                            // ModifiedByUserId = ManagerActionDTO.ModifiedByUserId
        //                        };

        //                        userEntitlementBLL.RequestChangeEntitlmentOfUser(RequestEntitlmentDTO);

        //                    }

        //                    //Add new Request Details
        //                    RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO()
        //                    {
        //                        RequestID = item.RequestID,
        //                        CreationDate = creationDate,
        //                        RequestDetailsComment = "System Approve Request",
        //                        RequestDetailsTypeID = (int)RequestDetailsTypes.SystemApproveRequest,
        //                        RequestDetailsTypeName = RequestDetailsTypes.Approve.ToString(),
        //                        // UserID = UserManager.UserID

        //                    };


        //                    requestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);
        //                }
        //                //System Auto Reject Request
        //                else
        //                {
        //                    item.RequesStatusFK = (int)RequestStatus.SystemRejected;
        //                    Edit(item);
        //                    foreach (var ApprovalFlowDetailRequestsItems in ApprovalFlowDetailRequests)
        //                    {
        //                        ApprovalFlowDetailRequestsItems.IsActive = false;
        //                        ApprovalFlowDetailRequestsItems.RequestActionFK = (int)ActionType.SystemReject;
        //                        approvalFlowRequestDetailsBLL.Edit(ApprovalFlowDetailRequestsItems);
        //                    }
        //                    //Add new Request Details

        //                    RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO()
        //                    {
        //                        RequestID = item.RequestID,
        //                        CreationDate = creationDate,
        //                        RequestDetailsComment = "System Reject Request",
        //                        RequestDetailsTypeID = (int)RequestDetailsTypes.SystemRejectRequest,
        //                        RequestDetailsTypeName = RequestDetailsTypes.Approve.ToString(),
        //                        //UserID = UserManager.UserID
        //                    };


        //                    requestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);



        //                }


        //                RequestApprovedBySystem.Add(new ApprovedBySystemDTO
        //                {
        //                    EpmlyeeID = (int)item.UserFK,
        //                    RequestID = item.RequestID,
        //                    RequestStatusID = (int)item.RequesStatusFK
        //                });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            RequestApprovedBySystem.Add(new ApprovedBySystemDTO
        //            {
        //                EpmlyeeID = (int)item.UserFK,
        //                RequestID = item.RequestID,
        //                RequestStatusID = (int)item.RequesStatusFK,
        //                HasException = true,
        //                Exception = ex.InnerException.ToString()
        //            });

        //        }

        //    }

        //    return RequestApprovedBySystem;

        //}


        public void CancelRequest(RequestCancelDTO RequestCancel)
        {

            Request request = Find(x => x.RequestID == RequestCancel.RequestID).FirstOrDefault();
            User UserDeleter = userBLL.Find(x => x.UserID == RequestCancel.UserDeleterID).FirstOrDefault();

            if (request.RequesStatusFK == (int)RequestStatus.Pending && request.Order == 1)
            {
                //Delete Hours
                foreach (var Timeitem in requestHoursHandlerBLL.Find(x => x.RequestFK == RequestCancel.RequestID))
                {
                    Timeitem.IsActive = false;
                    Timeitem.IsDeleted = true;
                    Timeitem.DeletionDate = creationDate;

                    requestHoursHandlerBLL.Edit(Timeitem);
                }
                foreach (var item in requestHandlerBLL.Find(x => x.RequestFK == RequestCancel.RequestID))
                {
                    item.IsActive = false;
                    item.IsDeleted = true;
                    item.RquestStatusFK = (int)StaticEnums.RequestStatus.Deleted;
                    item.DeletionDate = creationDate;

                    requestHandlerBLL.Edit(item);
                }
                foreach (var item in approvalFlowRequestDetailsBLL.Find(x => x.RequestFK == RequestCancel.RequestID))
                {

                    item.IsActive = false;
                    item.IsDeleted = true;
                    item.DeletionDate = creationDate;
                    approvalFlowRequestDetailsBLL.Edit(item);
                }
                request.RequesStatusFK = (int)StaticEnums.RequestStatus.Deleted;
                request.IsDeleted = true;
                request.IsActive = false;
                request.DeletionDate = creationDate;

                //Edit Request DATA
                Edit(request);
                string RequestDetailsComment = "Request Deleted";

                RequestDetailsDTO RequestDetailsDTO = new RequestDetailsDTO
                {
                    CreationDate = creationDate,
                    DetailsCreator = UserDeleter.UserName,
                    RequestDetailsComment = RequestDetailsComment,
                    RequestDetailsTypeID = (int)RequestDetailsTypes.DeleteRequest,
                    RequestDetailsTypeName = RequestDetailsTypes.DeleteRequest.ToString(),
                    RequestID = RequestCancel.RequestID,
                    UserID = UserDeleter.UserID
                };


                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);
                //Reason
                RequestDetailsDTO RequestDetailsDeletionReason = new RequestDetailsDTO
                {
                    CreationDate = creationDate,
                    DetailsCreator = UserDeleter.UserName,
                    RequestDetailsComment = "Deletion Reason : " + RequestCancel.CancelReason,
                    RequestDetailsTypeID = (int)RequestDetailsTypes.DeletionReason,
                    RequestDetailsTypeName = RequestDetailsTypes.DeletionReason.ToString(),
                    RequestID = RequestCancel.RequestID,
                    UserID = UserDeleter.UserID
                };
                requestDetailsBLL.AddNewRequestDetails(RequestDetailsDeletionReason);

            }


        }

        public bool CheckCancelEnable(int RequestID) {

            Request request = Find(x => x.RequestID ==RequestID).FirstOrDefault();

            if (request.Order == 1&& request.RequesStatusFK== (int)RequestStatus.Pending&& DateTime.Now<request.DurationFrom)
            {
                return true;
            }
            else {
                return false;

            }
        }

     public ListWorkingDayDetailsDTO GetStartAndEndWorkingTime(int UserID,string RequestDate)
        {
            ListWorkingDayDetailsDTO WorkingDayStartEnd = new ListWorkingDayDetailsDTO();
            WorkingDayStartEnd.WorkingDayDetailsDTO = new List<WorkingDayDetailsDTO>();
            var User=userBLL.Find(x => x.UserID == UserID).FirstOrDefault();

            if (User.WorkingModeFK== (int) WorkingMode.Regular)
            {
                int WorkingWeekID =(int) User.WorkingWeekFK;
                int day = (int)DateTime.ParseExact(RequestDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).DayOfWeek;
                int DayFK = weekDaysBLL.Find(x => x.WeekDayValue == day).FirstOrDefault().WeeKDaysID;
                TimeSpan DayEnd = (TimeSpan)workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == WorkingWeekID && x.WeekDaysFK == DayFK).FirstOrDefault().EndTime;
                TimeSpan DayStart = (TimeSpan)workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == WorkingWeekID && x.WeekDaysFK == DayFK).FirstOrDefault().StartTime;
                WorkingDayStartEnd.WorkingDayDetailsDTO.Add(new  WorkingDayDetailsDTO
                {
                    DayStart = DayStart,
                    DayEnd = DayEnd,
                });
                WorkingDayStartEnd.WorkingMode = "Regular";

            }
            else if (User.WorkingModeFK == (int)WorkingMode.Shift)
            {
                var TodayDate = DateTime.ParseExact(RequestDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                var ListOfShiftsAday = workingShiftUserDailyBLL.Find(x => x.UserFK == UserID 
                                                                       && x.ShiftStartDate <= TodayDate.Date 
                                                                       && x.ShiftEndDate >= TodayDate.Date
                                                                       && x.IsActive == true && x.IsDeleted == false
                                                                       ).ToList();
                // TimeSpan DayEnd = (TimeSpan)workingShiftUserDailyBLL.Find(x => x.UserFK == UserID&&x.ShiftDate== TodayDate.Date).FirstOrDefault().ShiftEnd;
                //   TimeSpan DayStart = (TimeSpan)workingShiftUserDailyBLL.Find(x => x.UserFK == UserID && x.ShiftDate == TodayDate.Date).FirstOrDefault().ShiftStart;
         
                if (ListOfShiftsAday.Count == 0)
                {
                    WorkingDayStartEnd.IsOffDay = true;

                }
                foreach (var item in ListOfShiftsAday.ToList())
                {
                    bool IsBetweenTwoDaysRequest = false;
                    if (item.ShiftEndDate != item.ShiftStartDate)
                    {
                        IsBetweenTwoDaysRequest = true;

                    }

                    WorkingDayStartEnd.WorkingDayDetailsDTO.Add(new WorkingDayDetailsDTO
                    {
                        DayStart = item.ShiftStartTime,
                        DayEnd = item.ShiftEndTime,
                        IsBetweenTwoDaysRequest= IsBetweenTwoDaysRequest
                    });
                }
                WorkingDayStartEnd.WorkingMode = "Shift";

            }
            return WorkingDayStartEnd;
        }

        public void SetRequestBackToWorkDateOfShift(Request Request, out DateTime backtoworkdate, out List<DateTime> OffDays,
        out List<RequestHandlerHours> OffHours, out decimal NumberOfOffUnit, LeaveType LeaveType, int workingWeekID, int WorkingModeID,out bool IsOffDay)
        {
            IsOffDay = false;
            OffHours = new List<RequestHandlerHours>();
            if (Request.LeaveDurationUnitFK == (int)LeaveDurationUnit.Hours)
            {
                // int day = (int)Request.DurationTo.DayOfWeek;
                //   int DayFK = weekDaysBLL.Find(x => x.WeekDayValue == day).FirstOrDefault().WeeKDaysID;
                //TimeSpan DayEnd = (TimeSpan)workingWeekDetailsBLL.Find(x => x.WorkingWeekFK == workingWeekID && x.WeekDaysFK == DayFK).FirstOrDefault().EndTime;


                var Shift = 
                 workingShiftUserDailyBLL.Find(x => x.ShiftStartDate == Request.DurationFrom.Date
                 &&  x.IsActive == true && x.IsDeleted == false && x.UserFK == Request.UserFK
                  
                 ).FirstOrDefault();
                TimeSpan DayEnd =new TimeSpan();
                if (Shift?.ShiftEndTime == null)
                {
                }
                else
                {
                  DayEnd = (TimeSpan)Shift?.ShiftEndTime;

                }
               



                OffDays = new List<DateTime>();

                OffDays.Add(Request.DurationFrom.Date);
                NumberOfOffUnit = (Request.DurationTo - Request.DurationFrom).Hours;
                decimal RemaningMinutes = (decimal)((Request.DurationTo - Request.DurationFrom).TotalMinutes / 60) - NumberOfOffUnit;

                if (Request.DurationTo.TimeOfDay < DayEnd)
                {
                 
                        backtoworkdate = Request.DurationTo;
                    
                }
                else
                {
                    //Request.DurationTo = Request.DurationTo.Date + DayEnd;
                    backtoworkdate = BacktoworkdateShift(OffDays, Request.DurationFrom, Request.DurationFrom, workingWeekID, (int)Request.UserFK, out IsOffDay);

                }

                TimeSpan StartOfRequestTime = Request.DurationFrom.TimeOfDay;
                for (int i = 0; i < NumberOfOffUnit; i++)
                {
                    if (i == 0)
                    {
                        //StartOfRequestTime += TimeSpan.FromHours(1);
                        TimeSpan EndOfRequestTime = StartOfRequestTime;
                        EndOfRequestTime += TimeSpan.FromHours(1);
                        OffHours.Add(
                            new RequestHandlerHours
                            {
                                TimeStart = StartOfRequestTime,
                                TimeEnd = EndOfRequestTime,
                                TimeDuration = 1
                            });
                    }

                    else
                    {
                        StartOfRequestTime += TimeSpan.FromHours(1);
                        TimeSpan EndOfRequestTime = StartOfRequestTime;
                        EndOfRequestTime += TimeSpan.FromHours(1);
                        OffHours.Add(
                            new RequestHandlerHours
                            {
                                TimeStart = StartOfRequestTime,
                                TimeEnd = EndOfRequestTime,
                                TimeDuration = 1
                            });
                    }
                }

                TimeSpan timespan = TimeSpan.FromHours((double)RemaningMinutes);
                if (RemaningMinutes != 0)
                {
                    OffHours.Add(new RequestHandlerHours
                    {
                        TimeStart = StartOfRequestTime,
                        TimeEnd = StartOfRequestTime + timespan,
                        TimeDuration = RemaningMinutes
                    });
                }


            }
            else
            {
                if (Request.PartialDurationUnitFK == (int)StaticEnums.LeaveTypePartialDurationUnit.AMHalfDay)
                {
                    OffDays = new List<DateTime>();
                    backtoworkdate = Request.DurationFrom;
                    OffDays.Add(Request.DurationFrom);
                    NumberOfOffUnit = (decimal)0.5;

                }
                else if (Request.PartialDurationUnitFK == (int)StaticEnums.LeaveTypePartialDurationUnit.PMHalfDay)
                {
                    OffDays = new List<DateTime>();

                    CheckDaysshift(Request.DurationFrom, Request.DurationTo, out OffDays, out backtoworkdate, out NumberOfOffUnit, workingWeekID, WorkingModeID, (int)Request.UserFK, out IsOffDay);
                    NumberOfOffUnit = (decimal)0.5;
                }
                else
                {
                    if (LeaveType.IncludePublicHolidayInRequestLength == false)
                    {
                        CheckDaysshift(Request.DurationFrom, Request.DurationTo, out OffDays, out backtoworkdate, out NumberOfOffUnit, workingWeekID, WorkingModeID, (int)Request.UserFK,out IsOffDay);
                    }
                    else
                    {
                        CheckDaysIncludingPublicHolidaysShift(Request.DurationFrom, Request.DurationTo, out OffDays, out backtoworkdate, out NumberOfOffUnit, workingWeekID, (int)Request.UserFK,out IsOffDay);
                    }
                }

            }
        }

        public DateTime BacktoworkdateShift(List<DateTime> offdays, DateTime start, DateTime end, int workingWeekID, int UserID,out bool IsOffDay)
        {
            IsOffDay = false;
            if (end.Date < start.Date)
            {
                return DateTime.MinValue.Date;
            }
            else
            {
                try
                {
                    DateTime endofvacation = offdays.Max();              
                    var workingWeek = workingWeekBLL.Find(x => x.WorkingWeekID == workingWeekID).FirstOrDefault();
                    var NextworkingShift = workingShiftUserDailyBLL.Find(x => 
                       x.ShiftEndDate >= endofvacation.Date
                    && x.UserFK == UserID && x.IsActive == true 
                    && x.IsDeleted == false)
                    .OrderBy(x => x.ShiftStartDate).ThenBy(x => x.ShiftStartTime).ToList();
                    if (NextworkingShift.Where(x => x.ShiftStartDate == endofvacation.Date).Count() > 0

                        )
                    {
                        NextworkingShift.RemoveAt(0);
                    }
                     var NextShiftsOfUser = NextworkingShift.FirstOrDefault();
                    DateTime backtoworkdate = new DateTime();
                    if (NextShiftsOfUser != null)
                    {
                         backtoworkdate = NextShiftsOfUser.ShiftStartDate + NextShiftsOfUser.ShiftStartTime;
                    }
                    else
                    {
                        IsOffDay = true;
                    }
                    return backtoworkdate.Date;
                }

                catch
                {
                    return DateTime.MinValue.Date;
                }
            }
        }
        public void CheckDaysshift(DateTime start, DateTime end, out List<DateTime> offdays,
        out DateTime backtoworkdate, out decimal NumberOfOffUnit, int workingWeekID, int WorkingModeID, int UserID,out bool IsOffDay)
        {
            IsOffDay = false;
            try
            {
                List<DateTime> OffWorkDays = new List<DateTime>();

                var dateInterval = new List<DateTime>();
                var daysInWeek = new List<DayOfWeek>();
                var dayswithoutWeekEnd = new List<DateTime>();
                var CurrentCheckedDay = start.Date;
                int DaysDiffrent = Math.Abs(start.DayOfYear - end.DayOfYear);
                for (int i = 0; i <= DaysDiffrent; i++)
                {
                    if (i==0)
                    {

                        if (workingShiftUserDailyBLL.Find(x => x.ShiftStartDate == CurrentCheckedDay && /*x.ShiftEndDate >= CheckedDate &&*/ x.IsActive == true && x.IsDeleted == false).Count() > 0)
                        {
                            OffWorkDays.Add(CurrentCheckedDay);
                        }
                    }
                    else
                    {
                        CurrentCheckedDay = CurrentCheckedDay.AddDays(1);
                        if (workingShiftUserDailyBLL.Find(x => x.ShiftStartDate == CurrentCheckedDay /*&& x.ShiftEndDate >= CheckedDate*/ && x.IsActive == true && x.IsDeleted == false).Count() > 0)
                        {
                            OffWorkDays.Add(CurrentCheckedDay);
                        }
                    }
                }
                offdays = OffWorkDays;
                DateTime backtoworkexactdate = new DateTime();
                if (offdays.Count() == 0)
                {
                    IsOffDay = true;

                }
                else
                {
                    backtoworkexactdate = BacktoworkdateShift(OffWorkDays, start, end, workingWeekID, UserID,out IsOffDay);

                }
                NumberOfOffUnit = OffWorkDays.Count();
                if (backtoworkexactdate.Date == DateTime.MinValue.Date)
                {
                    backtoworkdate = DateTime.MinValue.Date;
                }
                else
                {
                    backtoworkdate = backtoworkexactdate;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                backtoworkdate = DateTime.MinValue.Date;
                NumberOfOffUnit = 0;
                offdays = null;
            }
        }

        public void CheckDaysIncludingPublicHolidaysShift(DateTime start, DateTime end, out List<DateTime> offdays, out DateTime backtoworkdate, out decimal NumberOfOffUnit, int workingWeekID,int UserId ,out bool IsOffDay)
        {
            IsOffDay = false;
            try
            {
                var dateInterval = new List<DateTime>();
                var daysInWeek = new List<DayOfWeek>();
                var dayswithWeekEnd = new List<DateTime>();

                for (var dt = DateTime.Parse(start.ToString()).Date; dt <= DateTime.Parse(end.ToString()).Date; dt = dt.AddDays(1))
                {
                    dateInterval.Add(dt);
                    daysInWeek.Add(dt.DayOfWeek);
                }

                dayswithWeekEnd = dateInterval;
                var OffWorkDays = dayswithWeekEnd;
                offdays = OffWorkDays;
                DateTime backtoworkexactdate = BacktoworkdateShift(OffWorkDays, start, end, workingWeekID, UserId,out IsOffDay);
                NumberOfOffUnit = OffWorkDays.Count();
                if (backtoworkexactdate.Date == DateTime.MinValue.Date)
                {
                    backtoworkdate = DateTime.MinValue.Date;
                }
                else
                {
                    backtoworkdate = backtoworkexactdate;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                backtoworkdate = DateTime.MinValue.Date;
                NumberOfOffUnit = 0;
                offdays = null;
            }
        }




    }


}


