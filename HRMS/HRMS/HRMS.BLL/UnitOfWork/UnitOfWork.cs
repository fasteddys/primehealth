using System;
using HRMS.BLL.DbObjects;
using HRMS.BLL.Logic.Tables;
using HRMS.BLL.Logic.Shared_Logic;
using HRMS.Entities;
using System.Reflection;
using HRMS.Utilities;
using System.Data.Entity.Validation;
using System.IO;

namespace HRMS.BLL.UnitOfWork
{
    public class UnitOfWork
    {
        //Sample Data
        private static int LanguageId;
        private int UserId;
        private HRMSEntities Context;
        private DateTime CreationDate;
        public UnitOfWork(int languageId, DateTime creationDate, HRMSEntities context)
        {
            LanguageId = languageId;
            // UserId = userId;
            Context = context;
            CreationDate = creationDate;
        }
        #region Functions
        public bool Submit(out Exception exResult)
        {
            try
            {
                exResult = new Exception();
                return Context.SaveChanges() > 0 /*false*/;

            }
            //catch (DbEntityValidationException dbEx)
            //{
                
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            //            if (!Directory.Exists(path))
            //            {

            //                Directory.CreateDirectory(path);

            //            }

            //            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\DbEntityValidationExceptionServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            //            if (!File.Exists(filepath))
            //            {

            //                // Create a file to write to.

            //                using (StreamWriter sw = File.CreateText(filepath))
            //                {

            //                    sw.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

            //                }

            //            }
            //            else
            //            {

            //                using (StreamWriter sw = File.AppendText(filepath))
            //                {

            //                    sw.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

            //                }

            //            }
            //        }
            //    }
            //    exResult = dbEx;
            //    return false;

            //}
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                exResult = ex;
                return false;
            }

        }
        #endregion

        private StoredFunctions storedFunctions;
        public StoredFunctions StoredFunctions
        {
            get
            {

                if (this.storedFunctions == null)
                {
                    this.storedFunctions = new StoredFunctions(LanguageId);
                }
                return storedFunctions;
            }
        }
        private TimeAttendanceBLL timeAttendanceBLL;

        public TimeAttendanceBLL TimeAttendanceBLL
        {
            get
            {

                if (this.timeAttendanceBLL == null)
                {
                    this.timeAttendanceBLL = new TimeAttendanceBLL(Context, CreationDate);
                }
                return timeAttendanceBLL;
            }
        }
        private UserBLL userBLL;

        public UserBLL UserBLL
        {
            get
            {

                if (this.userBLL == null)
                {
                    this.userBLL = new UserBLL(Context, CreationDate);
                }
                return userBLL;
            }
        }

        private AccessControlUserBLL accessControlUserBLL;

        public AccessControlUserBLL AccessControlUserBLL
        {
            get
            {

                if (this.accessControlUserBLL == null)
                {
                    this.accessControlUserBLL = new AccessControlUserBLL(Context, CreationDate);
                }
                return accessControlUserBLL;
            }
        }
        private PositionBLL positionBLL;

        public PositionBLL PositionBLL
        {
            get
            {

                if (this.positionBLL == null)
                {
                    this.positionBLL = new PositionBLL(Context, CreationDate);
                }
                return positionBLL;
            }
        }


        private ServiceBLL serviceBLL;

        public ServiceBLL ServiceBLL
        {
            get
            {

                if (this.serviceBLL == null)
                {
                    this.serviceBLL = new ServiceBLL(Context, CreationDate);
                }
                return serviceBLL;
            }
        }


        private DepartmentBLL departmentBLL;

        public DepartmentBLL DepartmentBLL
        {
            get
            {

                if (this.departmentBLL == null)
                {
                    this.departmentBLL = new DepartmentBLL(Context, CreationDate);
                }
                return departmentBLL;
            }
        }




        private SubDepartmentBLL subDepartmentBLL;

        public SubDepartmentBLL SubDepartmentBLL
        {
            get
            {

                if (this.subDepartmentBLL == null)
                {
                    this.subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
                }
                return subDepartmentBLL;
            }
        }

        private ManagerBLL managerBLL;

        public ManagerBLL ManagerBLL
        {
            get
            {

                if (this.managerBLL == null)
                {
                    this.managerBLL = new ManagerBLL(Context, CreationDate);
                }
                return managerBLL;
            }
        }
        private ApprovalFlowBLL approvalFlowBLL;

        public ApprovalFlowBLL ApprovalFlowBLL
        {
            get
            {

                if (this.approvalFlowBLL == null)
                {
                    this.approvalFlowBLL = new ApprovalFlowBLL(Context, CreationDate);
                }
                return approvalFlowBLL;
            }
        }

        private LeaveTypeBLL leaveTypeBLL;

        public LeaveTypeBLL LeaveTypeBLL
        {
            get
            {

                if (this.leaveTypeBLL == null)
                {
                    this.leaveTypeBLL = new LeaveTypeBLL(Context, CreationDate);
                }
                return leaveTypeBLL;
            }
        }


        private RequestBLL requestBLL;

        public RequestBLL RequestBLL
        {
            get
            {

                if (this.requestBLL == null)
                {
                    this.requestBLL = new RequestBLL(Context, CreationDate);
                }
                return requestBLL;
            }
        }

        private UserPositionBLL userPositionBLL;

        public UserPositionBLL UserPositionBLL
        {
            get
            {

                if (this.userPositionBLL == null)
                {
                    this.userPositionBLL = new UserPositionBLL(Context, CreationDate);
                }
                return userPositionBLL;
            }
        }

        private SharedUserProfileBLL sharedUserProfileBLL;

        public SharedUserProfileBLL SharedUserProfileBLL
        {
            get
            {

                if (this.sharedUserProfileBLL == null)
                {
                    this.sharedUserProfileBLL = new SharedUserProfileBLL(Context, CreationDate);
                }
                return sharedUserProfileBLL;
            }
        }

        private LeaveTypeConsiderationAsDIMBLL leaveCalculationTypeDIMBLL;

        public LeaveTypeConsiderationAsDIMBLL LeaveCalculationTypeDIMBLL
        {
            get
            {

                if (this.leaveCalculationTypeDIMBLL == null)
                {
                    this.leaveCalculationTypeDIMBLL = new LeaveTypeConsiderationAsDIMBLL(Context, CreationDate);
                }
                return leaveCalculationTypeDIMBLL;
            }
        }

        private LeaveTypeDurationUnitDIMBLL leaveDurationUnitDIMBLL;

        public LeaveTypeDurationUnitDIMBLL LeaveDurationUnitDIMBLL
        {
            get
            {

                if (this.leaveDurationUnitDIMBLL == null)
                {
                    this.leaveDurationUnitDIMBLL = new LeaveTypeDurationUnitDIMBLL(Context, CreationDate);
                }
                return leaveDurationUnitDIMBLL;
            }
        }
        private RequestStatusBLL requesStatusBLL;

        public RequestStatusBLL RequesStatusBLL
        {
            get
            {

                if (this.requesStatusBLL == null)
                {
                    this.requesStatusBLL = new RequestStatusBLL(Context, CreationDate);
                }
                return requesStatusBLL;
            }
        }



        private UserTimeAttendanceBLL userTimeAttendanceBLL;

        public UserTimeAttendanceBLL UserTimeAttendanceBLL
        {
            get
            {

                if (this.userTimeAttendanceBLL == null)
                {
                    this.userTimeAttendanceBLL = new UserTimeAttendanceBLL(Context, CreationDate);
                }
                return userTimeAttendanceBLL;
            }
        }
        private RequestStatusBLL requestStatusBLL;

        public RequestStatusBLL RequestStatusBLL
        {
            get
            {

                if (this.requestStatusBLL == null)
                {
                    this.requestStatusBLL = new RequestStatusBLL(Context, CreationDate);
                }
                return requestStatusBLL;
            }
        }


        private UserDetailsProfileBLL userDetailsProfileBLL;

        public UserDetailsProfileBLL UserDetailsProfileBLL
        {
            get
            {

                if (this.userDetailsProfileBLL == null)
                {
                    this.userDetailsProfileBLL = new UserDetailsProfileBLL(Context, CreationDate);
                }
                return userDetailsProfileBLL;
            }
        }

        private RequestDetailsBLL requestDetailsBLL;

        public RequestDetailsBLL RequestDetailsBLL
        {
            get
            {

                if (this.requestDetailsBLL == null)
                {
                    this.requestDetailsBLL = new RequestDetailsBLL(Context, CreationDate);
                }
                return requestDetailsBLL;
            }
        }
        private WorkingWeekBLL workingWeekBLL;
        public WorkingWeekBLL WorkingWeekBLL
        {
            get
            {

                if (this.workingWeekBLL == null)
                {
                    this.workingWeekBLL = new WorkingWeekBLL(Context, CreationDate);
                }
                return workingWeekBLL;
            }
        }
        private WorkingWeekDetailsBLL workingWeekDetailsBLL;
        public WorkingWeekDetailsBLL WorkingWeekDetailsBLL
        {
            get
            {

                if (this.workingWeekDetailsBLL == null)
                {
                    this.workingWeekDetailsBLL = new WorkingWeekDetailsBLL(Context, CreationDate);
                }
                return workingWeekDetailsBLL;
            }
        }

        private LeaveTypeAccuralPeriodDIMBLL leaveTypeAccuralPeriodDIMBLL;
        public LeaveTypeAccuralPeriodDIMBLL LeaveTypeAccuralPeriodDIMBLL
        {
            get
            {

                if (this.leaveTypeAccuralPeriodDIMBLL == null)
                {
                    this.leaveTypeAccuralPeriodDIMBLL = new LeaveTypeAccuralPeriodDIMBLL(Context);
                }
                return leaveTypeAccuralPeriodDIMBLL;
            }
        }

        private LeaveTypeEntitlementTypeDIMBLL leaveTypeEntitlementTypeDIMBLL;
        public LeaveTypeEntitlementTypeDIMBLL LeaveTypeEntitlementTypeDIMBLL
        {
            get
            {

                if (this.leaveTypeEntitlementTypeDIMBLL == null)
                {
                    this.leaveTypeEntitlementTypeDIMBLL = new LeaveTypeEntitlementTypeDIMBLL(Context);
                }
                return leaveTypeEntitlementTypeDIMBLL;
            }
        }


        private LeaveTypeFirstAccuralMethodDIMBLL leaveTypeFirstAccuralMethodDIMBLL;
        public LeaveTypeFirstAccuralMethodDIMBLL LeaveTypeFirstAccuralMethodDIMBLL
        {
            get
            {

                if (this.leaveTypeFirstAccuralMethodDIMBLL == null)
                {
                    this.leaveTypeFirstAccuralMethodDIMBLL = new LeaveTypeFirstAccuralMethodDIMBLL(Context);
                }
                return leaveTypeFirstAccuralMethodDIMBLL;
            }
        }


        private LeaveTypeGainingEligibilityMethodDIMBLL leaveTypeGainingEligibilityMethodDIMBLL;
        public LeaveTypeGainingEligibilityMethodDIMBLL LeaveTypeGainingEligibilityMethodDIMBLL
        {
            get
            {

                if (this.leaveTypeGainingEligibilityMethodDIMBLL == null)
                {
                    this.leaveTypeGainingEligibilityMethodDIMBLL = new LeaveTypeGainingEligibilityMethodDIMBLL(Context);
                }
                return leaveTypeGainingEligibilityMethodDIMBLL;
            }
        }

        private LeaveTypeMinOneDayDurationDIMBLL leaveTypeMinOneDayDurationDIMBLL;
        public LeaveTypeMinOneDayDurationDIMBLL LeaveTypeMinOneDayDurationDIMBLL
        {
            get
            {

                if (this.leaveTypeMinOneDayDurationDIMBLL == null)
                {
                    this.leaveTypeMinOneDayDurationDIMBLL = new LeaveTypeMinOneDayDurationDIMBLL(Context);
                }
                return leaveTypeMinOneDayDurationDIMBLL;
            }
        }

        private LeaveTypePartialDurationUnitDIMBLL leaveTypePartialDurationUnitDIMBLL;
        public LeaveTypePartialDurationUnitDIMBLL LeaveTypePartialDurationUnitDIMBLL
        {
            get
            {

                if (this.leaveTypePartialDurationUnitDIMBLL == null)
                {
                    this.leaveTypePartialDurationUnitDIMBLL = new LeaveTypePartialDurationUnitDIMBLL(Context);
                }
                return leaveTypePartialDurationUnitDIMBLL;
            }
        }

        private LeaveTypeProrateCalculationModeDIMBLL leaveTypeProrateCalculationModeDIMBLL;
        public LeaveTypeProrateCalculationModeDIMBLL LeaveTypeProrateCalculationModeDIMBLL
        {
            get
            {

                if (this.leaveTypeProrateCalculationModeDIMBLL == null)
                {
                    this.leaveTypeProrateCalculationModeDIMBLL = new LeaveTypeProrateCalculationModeDIMBLL(Context);
                }
                return leaveTypeProrateCalculationModeDIMBLL;
            }
        }


        private LeaveTypeProrateMethodDIMBLL leaveTypeProrateMethodDIMBLL;
        public LeaveTypeProrateMethodDIMBLL LeaveTypeProrateMethodDIMBLL
        {
            get
            {

                if (this.leaveTypeProrateMethodDIMBLL == null)
                {
                    this.leaveTypeProrateMethodDIMBLL = new LeaveTypeProrateMethodDIMBLL(Context);
                }
                return leaveTypeProrateMethodDIMBLL;
            }
        }

        private LeaveTypeEntitlementSourceDIMBLL leaveTypeEntitlementSourceDIMBLL;
        public LeaveTypeEntitlementSourceDIMBLL LeaveTypeEntitlementSourceDIMBLL
        {
            get
            {

                if (this.leaveTypeEntitlementSourceDIMBLL == null)
                {
                    this.leaveTypeEntitlementSourceDIMBLL = new LeaveTypeEntitlementSourceDIMBLL(Context);
                }
                return leaveTypeEntitlementSourceDIMBLL;
            }
        }

        private GenderTypeBLL genderTypeBLL;
        public GenderTypeBLL GenderTypeBLL
        {
            get
            {

                if (this.genderTypeBLL == null)
                {
                    this.genderTypeBLL = new GenderTypeBLL(Context, CreationDate);
                }
                return genderTypeBLL;
            }
        }
        private ContractTypeBLL contractTypeBLL;
        public ContractTypeBLL ContractTypeBLL
        {
            get
            {

                if (this.contractTypeBLL == null)
                {
                    this.contractTypeBLL = new ContractTypeBLL(Context, CreationDate);
                }
                return contractTypeBLL;
            }
        }


        private LeaveTypeFieldsDIMBLL leaveTypeFieldsDIMBLL;
        public LeaveTypeFieldsDIMBLL LeaveTypeFieldsDIMBLL
        {
            get
            {

                if (this.leaveTypeFieldsDIMBLL == null)
                {
                    this.leaveTypeFieldsDIMBLL = new LeaveTypeFieldsDIMBLL(Context);
                }
                return leaveTypeFieldsDIMBLL;
            }
        }

        private LeaveTypeFieldsVisibilityDIMBLL leaveTypeFieldsVisibilityDIMBLL;
        public LeaveTypeFieldsVisibilityDIMBLL LeaveTypeFieldsVisibilityDIMBLL
        {
            get
            {

                if (this.leaveTypeFieldsVisibilityDIMBLL == null)
                {
                    this.leaveTypeFieldsVisibilityDIMBLL = new LeaveTypeFieldsVisibilityDIMBLL(Context);
                }
                return leaveTypeFieldsVisibilityDIMBLL;
            }
        }
        private LeaveTypeRestrictionTypeBLL leaveTypeRestrictionTypeBLL;
        public LeaveTypeRestrictionTypeBLL LeaveTypeRestrictionTypeBLL
        {
            get
            {

                if (this.leaveTypeRestrictionTypeBLL == null)
                {
                    this.leaveTypeRestrictionTypeBLL = new LeaveTypeRestrictionTypeBLL(Context);
                }
                return leaveTypeRestrictionTypeBLL;
            }
        }
        private LeaveTypeRestrictionBLL leaveTypeRestrictionBLL;
        public LeaveTypeRestrictionBLL LeaveTypeRestrictionBLL
        {
            get
            {

                if (this.leaveTypeRestrictionBLL == null)
                {
                    this.leaveTypeRestrictionBLL = new LeaveTypeRestrictionBLL(Context, CreationDate);
                }
                return leaveTypeRestrictionBLL;
            }
        }
        private LeaveTypeMonthlyAaccuralDaysDIMBLL leaveTypeMonthlyAaccuralDaysDIMBLL;
        public LeaveTypeMonthlyAaccuralDaysDIMBLL LeaveTypeMonthlyAaccuralDaysDIMBLL
        {
            get
            {

                if (this.leaveTypeMonthlyAaccuralDaysDIMBLL == null)
                {
                    this.leaveTypeMonthlyAaccuralDaysDIMBLL = new LeaveTypeMonthlyAaccuralDaysDIMBLL(Context);
                }
                return leaveTypeMonthlyAaccuralDaysDIMBLL;
            }
        }

        private LeaveTypePartialDurationBLL leaveTypePartialDurationBLL;
        public LeaveTypePartialDurationBLL LeaveTypePartialDurationBLL
        {
            get
            {

                if (this.LeaveTypePartialDurationBLL == null)
                {
                    this.leaveTypePartialDurationBLL = new LeaveTypePartialDurationBLL(Context, CreationDate);
                }
                return LeaveTypePartialDurationBLL;
            }
        }
        private RequestAttachmentBLL requestAttachmentBLL;
        public RequestAttachmentBLL RequestAttachmentBLL
        {
            get
            {

                if (this.requestAttachmentBLL == null)
                {
                    this.requestAttachmentBLL = new RequestAttachmentBLL(Context, CreationDate);
                }
                return requestAttachmentBLL;
            }
        }

        private RequestHandlerBLL requestHandlerBLL;
        public RequestHandlerBLL RequestHandlerBLL
        {
            get
            {

                if (this.requestHandlerBLL == null)
                {
                    this.requestHandlerBLL = new RequestHandlerBLL(Context, CreationDate);
                }
                return requestHandlerBLL;
            }
        }

        private OneDayMinDurationBLL oneDayMinDurationBLL;
        public OneDayMinDurationBLL OneDayMinDurationBLL
        {
            get
            {

                if (this.oneDayMinDurationBLL == null)
                {
                    this.oneDayMinDurationBLL = new OneDayMinDurationBLL(Context, CreationDate);
                }
                return oneDayMinDurationBLL;
            }
        }

        private UserEntitlementBLL userEntitlementBLL;
        public UserEntitlementBLL UserEntitlementBLL
        {
            get
            {

                if (this.userEntitlementBLL == null)
                {
                    this.userEntitlementBLL = new UserEntitlementBLL(Context, CreationDate);
                }
                return userEntitlementBLL;
            }
        }

        private SharedUserEntitlementBLL sharedUserEntitlementBLL;
        public SharedUserEntitlementBLL SharedUserEntitlementBLL
        {
            get
            {

                if (this.sharedUserEntitlementBLL == null)
                {
                    this.sharedUserEntitlementBLL = new SharedUserEntitlementBLL(Context, CreationDate);
                }
                return sharedUserEntitlementBLL;
            }
        }

        private RequestHoursHandlerBLL requestHoursHandlerBLL;
        public RequestHoursHandlerBLL RequestHoursHandlerBLL
        {
            get
            {

                if (this.requestHoursHandlerBLL == null)
                {
                    this.requestHoursHandlerBLL = new RequestHoursHandlerBLL(Context, CreationDate);
                }
                return requestHoursHandlerBLL;
            }
        }

        /*Official Holiday Bll Prop*/
        private OfficialHolidayBLL officialHolidayBLL;
        public OfficialHolidayBLL OfficialHolidayBLL
        {
            get
            {
                if (this.officialHolidayBLL == null)
                {
                    this.officialHolidayBLL = new OfficialHolidayBLL(Context, CreationDate);
                }
                return officialHolidayBLL;
            }
        }
        private UserLoginBLL userLoginBLL;

        public UserLoginBLL UserLoginBLL
        {
            get
            {

                if (this.userLoginBLL == null)
                {
                    this.userLoginBLL = new UserLoginBLL(Context, CreationDate);
                }
                return userLoginBLL;
            }
        }

        private RequestHistoryBLL  requestHistoryBLL;

        public RequestHistoryBLL RequestHistoryBLL
        {
            get
            {

                if (this.requestHistoryBLL == null)
                {
                    this.requestHistoryBLL = new RequestHistoryBLL(Context, CreationDate);
                }
                return requestHistoryBLL;
            }
        }

        private EntitlementChangedByDIMBLL entitlementChangedByDIMBLL;
        public EntitlementChangedByDIMBLL EntitlementChangedByDIMBLL
        {
            get
            {
                if(this.entitlementChangedByDIMBLL == null)
                {
                    this.entitlementChangedByDIMBLL = new EntitlementChangedByDIMBLL(Context, CreationDate);
                }
                return entitlementChangedByDIMBLL;
            }
        }

        private WeekDaysBLL weekDaysBLL;
        public WeekDaysBLL WeekDaysBLL
        {
            get
            {
                if (this.weekDaysBLL == null)
                {
                    this.weekDaysBLL = new WeekDaysBLL(Context, CreationDate);
                }
                return weekDaysBLL;
            }
        }
        private WorkingShiftBLL workingShiftBLL;
        public WorkingShiftBLL WorkingShiftBLL
        {
            get
            {
                if (this.workingShiftBLL == null)
                {
                    this.workingShiftBLL = new WorkingShiftBLL(Context, CreationDate);
                }
                return workingShiftBLL;
            }
        }
        private WorkingShiftUserDailyBLL workingShiftUserDailyBLL;
        public WorkingShiftUserDailyBLL WorkingShiftUserDailyBLL
        {
            get
            {
                if (this.workingShiftUserDailyBLL == null)
                {
                    this.workingShiftUserDailyBLL = new WorkingShiftUserDailyBLL(Context, CreationDate);
                }
                return workingShiftUserDailyBLL;
            }
        }

    }
}