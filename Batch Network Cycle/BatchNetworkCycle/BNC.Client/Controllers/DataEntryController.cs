using BNC.BLL.StaticData;
using BNC.Client.Custom_Action_Filters;
using BNC.DTOs.Business;
using BNC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BNC.Client.Controllers
{
    public class DataEntryController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AddDataEntryRequest()
        {
            ViewBag.DataEntryUsers = new SelectList(UnitOfWork.UserBLL.getUserData(1), "UserID", "UserName");
            return View();
        }
        
        public ActionResult ViewDataEntryRequests()
        {            
            return View();
        }
        public ActionResult ViewMyDataEntryRequests()
        {
            return View();
        }
        public ActionResult DataEntryActions(int ID)
        {
            return View();
        }
        public ActionResult DataEntryFinish(int ID)
        {
            return View();
        }
        public ActionResult ViewAllDataEntryRequests()
        {
            return View();
        }
        [HttpPost]
        [CheckRequestLockedobj("DataEntryAdministrationRequestFK", (int)StaticEnums.Entity.DataEntryAdminstrationRequest, "dataEntryBatchAssigningRequestDTO")]

        public JsonResult AddDataEntryBatchAssigningRequest(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.AddDataEntryBatchAssigningRequest(dataEntryBatchAssigningRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Claims Assigned To " + dataEntryBatchAssigningRequestDTO.DataEntryOfficerName + " Successfully",
                        Success = true
                    };
                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(outException, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = outException.Message,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDataEntryBatchAssigningRequest(int UserID)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedDataEntryBatchBLL.GetDataEntryBatchAssigningRequest(UserID),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CheckRequestLockedobj("DataEntryBatchAssigningRequestID", (int)StaticEnums.Entity.DataEntryBatchAssigningRequest, "dataEntryBatchAssigningRequestDTO")]

        public JsonResult ConfirmRecievingByOfficer(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.ConfirmRecievingByOfficer(dataEntryBatchAssigningRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Reciving Batch Confirmed",
                        Success = true
                    };
                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(outException, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = outException.Message,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DataEntryOfficerFinished(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.DataEntryOfficerFinished(dataEntryBatchAssigningRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Batch Is Finished",
                        Success = true
                    };
                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(outException, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = outException.Message,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDataEntryBatchAssigningRequestByID(int RequestID)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedDataEntryBatchBLL.GetDataEntryBatchAssigningRequestByID(RequestID),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMyDataEntryBatchAssigningRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.Search(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDataEntryBatchAssigningRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.GetMyDataEntryBatchAssigningRequest(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRequestData(int ID)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.DataEntryBatchAssigningRequestBLL.GetRequestData(ID),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}