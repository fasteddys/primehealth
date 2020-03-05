using System.Web;
using System.Web.Http;
using HRMS.Entities;
using HRMS.DTOs.Business;
using static HRMS.BLL.StaticData.StaticEnums;
using HRMS.DTOs;
using System;
using HRMS.Utilities;
using System.Reflection;

namespace HRMS.API.Controllers
{
    public class LeaveTypeController : BaseController
    {
        public ResultViewModel GetAllLeaveTypes(int UserID)
        {


            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypeBLL.GetLeavesTypeForUser(UserID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }
        public ResultViewModel GetAllLeaveTypes()
        {


            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypeBLL.GetAllLeavesType(),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }
        public ResultViewModel GetLeaveTypeDropDowns()
        {
            LeaveTypeDropDownsDTO LeaveTypeDropDownsDTO = new LeaveTypeDropDownsDTO() {
                LeaveDurationUnit = UnitOfWork.LeaveDurationUnitDIMBLL.GetActiveLeaveTypeDurationUnits(),
                LeaveTypeAccuralPeriod = UnitOfWork.LeaveTypeAccuralPeriodDIMBLL.GetAllLeaveTypeAccuralPeriods(),
                LeaveTypeConsiderationType = UnitOfWork.LeaveCalculationTypeDIMBLL.GetActiveLeaveTypeConsiderationAs(),
                LeaveTypeEntitlementSource = UnitOfWork.LeaveTypeEntitlementSourceDIMBLL.GetLeaveTypeEntitlementSources(),
                LeaveTypeEntitlementType = UnitOfWork.LeaveTypeEntitlementTypeDIMBLL.GetAllLeaveTypeEntitlementTypes(),
                LeaveTypeFirstAccuralMethod = UnitOfWork.LeaveTypeFirstAccuralMethodDIMBLL.GetAllLeaveTypeFirstAccuralMethods(),
                LeaveTypeGainingEligibilityMethod = UnitOfWork.LeaveTypeGainingEligibilityMethodDIMBLL.GetAllLeaveTypeGainingEligibilityMethods(),
                LeaveTypeProrateCalculationMode = UnitOfWork.LeaveTypeProrateCalculationModeDIMBLL.GetAllProrateCalculationModes(),
                LeaveTypeProrateMethod = UnitOfWork.LeaveTypeProrateMethodDIMBLL.GetAllLeaveTypeProrateMethods(),
                LeaveTypes = UnitOfWork.LeaveTypeBLL.GetAllLeavesType(),
                ContractType = UnitOfWork.ContractTypeBLL.GetAllContractTypes(),
                GenderType = UnitOfWork.GenderTypeBLL.GetAllGenderTypes(),
                LeaveTypeFields = UnitOfWork.LeaveTypeFieldsDIMBLL.GetAllLeaveTypeFieldsDIM(),
                LeaveTypeFieldsVisibility = UnitOfWork.LeaveTypeFieldsVisibilityDIMBLL.GetAllLeaveTypeFieldsVisibility(),
                LeaveTypeRestriction= UnitOfWork.LeaveTypeRestrictionTypeBLL.GetAllRestriction(),
                LeaveTypeMonthlyAaccuralDays= UnitOfWork.LeaveTypeMonthlyAaccuralDaysDIMBLL.GetLeaveTypeMonthlyAaccuralDays()
            };



            try
            {
                return new ResultViewModel
                {
                    Data = LeaveTypeDropDownsDTO,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }

        public ResultViewModel GetAllLeaveTypePartialDurationUnits(int DurationUnitID,int MinOneDayDurationID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypePartialDurationUnitDIMBLL.GetAllLeaveTypePartialDurationUnits(DurationUnitID, MinOneDayDurationID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
      

        public ResultViewModel AddLeaveType(LeaveTypeAdditionContainerDTO LeaveTypeContainer)
        {
            Exception exOutputSubmit;
            try
            {
                UnitOfWork.LeaveTypeBLL.AddLeavesType(LeaveTypeContainer);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }

        public ResultViewModel GetLeavesTypeFields(int LeaveTypeID)
        {


            try
            {
                
                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypeBLL.GetLeavesTypeFields(LeaveTypeID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }


        public ResultViewModel GetLeaveTypeMinOneDayDuration(int DurationUnitID)
        {
            try
            {
      
                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypeMinOneDayDurationDIMBLL.GetAllLeaveTypeMinOneDayDurations(DurationUnitID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }

        public ResultViewModel GetOneDayMinDurationByLeaveTypeUnitID(int DurationUnitID)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.OneDayMinDurationBLL.GetOneDayMinDurationByLeaveTypeUnitID(DurationUnitID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        public ResultViewModel GetPartialDurationUnit(int MinOneDayDuratioID)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypePartialDurationUnitDIMBLL.GetPartialDurationUnit(MinOneDayDuratioID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }


        public ResultViewModel GetAllTypesForEntitlmentChange(int UserID)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypeBLL.GetLeavesTypeForUserForEntitlmentChanges(UserID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        [HttpGet]
        public ResultViewModel IsMonthlyLeaveType(int LeaveTypeID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.LeaveTypeBLL.IsMonthlyLeaveType(LeaveTypeID),
                    Message = "Success",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }

    }
}