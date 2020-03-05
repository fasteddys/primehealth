using BNC.BLL.StaticData;
using BNC.Client.Custom_Action_Filters;
using BNC.Client.Hubs;
using BNC.DTOs.Business;
using BNC.Entities;
using BNC.Utilities;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BNC.Client.Controllers
{
    public class DataEntryAdminstrationController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AssignDataEntryAdminstrationRequest(int ID)
        {
            return View();
        }
        public ActionResult ViewAllDataEntryAdminstrationRequests()
        {
            return View();
        }
        public ActionResult ViewDataEntryAdminstrationRequests()
        {
            return View();
        }
        public ActionResult ViewMyDataEntryAdminstrationRequests()
        {
            return View();
        }

        public ActionResult DataEntryAdminstrationRequestTransfer()
        {
            return View();
        }        
       [HttpGet]
        public JsonResult GetDataEntryAdminstrationRequest(int ID)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedDataEntryBatchBLL.GetDataEntryAdminstrationRequest(ID),
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
        public JsonResult GetNewDataEntryAdminstrationRequests()
        {
            ResultViewModel result;
            try
            {
                var res = UnitOfWork.SharedDataEntryBatchBLL.GetNewDataEntryAdminstrationRequests();
                //AppHub.Show();
                result = new ResultViewModel
                {
                    Data = res,
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
        public JsonResult GetMyDataEntryAdminstrationRequests(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.GetMyDataEntryAdminstrationRequests(searchCriteriaDTO),
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
        public JsonResult GetMyDataEntryAdminstrationUnFinishedRequests(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.GetMyDataEntryAdminstrationUnFinishedRequests(searchCriteriaDTO),
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
        public JsonResult GetAllDataEntryAdminstrationRequests(SearchCriteriaDTO searchCriteriaDTO)
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
        [CheckRequestLockedobj("DataEntryAdminstrationRequestID", (int)StaticEnums.Entity.DataEntryAdminstrationRequest, "dataEntryAdminstrationRequestDTO")]

        public JsonResult AssignDataEntryAdminstrationRequest(DataEntryAdminstrationRequestDTO dataEntryAdminstrationRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.AssigneDataEntryAdminstrationRequest(dataEntryAdminstrationRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    AppHub.Show();
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "This Batch Assigned to you Successfully",
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
        public JsonResult LockRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                UnitOfWork.SharedSearchBLL.LockRequest(searchCriteriaDTO);
                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Success Lock The Record",
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
                    Success = false,
                };
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TransferDataEntryAdminstrationRequestToClosingTeam(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.TransferDataEntryAdminstrationRequestToClosingTeam(dataEntryBatchAssigningRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Batch Is Transfered",
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
    }
}