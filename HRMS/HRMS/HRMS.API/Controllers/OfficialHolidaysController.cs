using HRMS.BLL.UnitOfWork;
using HRMS.DTOs;
using HRMS.DTOs.Business;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HRMS.API.Controllers
{
    public class OfficialHolidaysController : BaseController
    {
        // GET: OfficialHolidays
        public ResultViewModel GetAllOfficialHolidays()
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.OfficialHolidayBLL.GetAllOfficialHolidays(),
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
        //------------------------------------------------------
        // GET: OfficialHolidays\
        [HttpDelete]
        public ResultViewModel DeleteOfficialHolidaysById(int id)
        {

            try
            {
                UnitOfWork.OfficialHolidayBLL.DeleteOfficialHolidaysById(id);
                Exception exOutputSubmit;
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
        //------------------------------------------------------
        [HttpPost]
        public ResultViewModel AddNewOfficialHolidays(OfficialHolidaysInputDTO NewOfficialHolidays)
        {

            try
            {
                UnitOfWork.OfficialHolidayBLL.AddNewOfficialHolidays(NewOfficialHolidays);
                Exception exOutputSubmit;
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
        //-----------------------------------------------------------------------
        [HttpPost]
        public int CheckIfThisDayHaveOfficialHoldays(OfficialHolidaysInputDTO NewOfficialHolidays)
        {

          return    UnitOfWork.OfficialHolidayBLL.CheckIfThisDayHaveOfficialHoldays(NewOfficialHolidays);


        }
        //-----------------------------------------------------------------------
        [HttpGet]
        public ResultViewModel GetOfficialHolidayById(int id)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.OfficialHolidayBLL.GetOfficialHolidayById(id),
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
        //------------------------------------------------------
        [HttpGet]
        public ResultViewModel EditOfficialHolidays(OfficialHolidaysInputDTO NewOfficialHolidays)
        {

            try
            {
                UnitOfWork.OfficialHolidayBLL.EditOfficialHolidays(NewOfficialHolidays);
                Exception exOutputSubmit;
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
        //------------------------------------------------------
    }
}