using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using HRMS.DTOs.Business;
using System.Web.Http;
using HRMS.DTOs;
using System.IO;
using System.Net.Http;
using HRMS.Entities;
using HRMS.Utilities;
using System.Reflection;

namespace HRMS.API.Controllers
{
    public class WorkingWeekController : BaseController
    {
        public ResultViewModel AddNewWorkingWeek(WorkingWeekDTO WorkingWeek)
        {

            try
            {
                //Every Working Week Has 7 Days in DataBase
                var workingweek = UnitOfWork.WorkingWeekBLL.AddNewWorkingWeek(WorkingWeek.WorkingWeekName);
                //UnitOfWork.Submit();
                UnitOfWork.WorkingWeekDetailsBLL.AddNewWorkingWeekDetails(WorkingWeek.ListWorkingWeekDetails, workingweek);
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
        public ResultViewModel EditWorkingWeekWorkingWeekDetail(WorkingWeekDTO WorkingWeek)
        {
            try
            {
                UnitOfWork.WorkingWeekBLL.EditWorkingWeek(WorkingWeek);
                UnitOfWork.WorkingWeekDetailsBLL.EditWorkingWeekDetails(WorkingWeek);
                //UnitOfWork.Submit(out Exception exOutputSubmit);
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
        public ResultViewModel LinkUserToWorkingWeek(UserWorkingWeekDTO UserWorkingWeekDTO)
        {

            try
            {

                UnitOfWork.WorkingWeekDetailsBLL.LinkUserToWorkingWeekDetails( UserWorkingWeekDTO);
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
        public ResultViewModel EditWorkingWeek(WorkingWeekDTO WorkingWeekDTO)
        {
            try
            {

                UnitOfWork.WorkingWeekDetailsBLL.EditWorkingWeekDetails(WorkingWeekDTO);
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
        [HttpGet]
        public ResultViewModel SelectWorkingWeek(int WorkingWeekID)
        {
            try
            {

                

                return new ResultViewModel
                {
                    Data = UnitOfWork.WorkingWeekDetailsBLL.SelectWorkingWeekDetails(WorkingWeekID),
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

        public ResultViewModel GetAllWorkingWeek()
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetWorkingWeekView().ToList(),
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
        public ResultViewModel GetWorkingWeekByID(int WorkingWeekID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.WorkingWeekBLL.GetWorkingWeekByID(WorkingWeekID),
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

        public ResultViewModel GetUsersByWorkingWeek(int WorkingWeekID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.WorkingWeekDetailsBLL.GetAllUserByWorkingWeek(WorkingWeekID),
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