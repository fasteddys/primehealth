using BNC.BLL.StaticData;
using BNC.BLL.UnitOfWork;
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
    public class ReAdministrationController : BaseController
    {
        // GET: ReAdministration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAllClosingReAdministrationTeamRequest()
        {
            return View();
        }
        public ActionResult ViewClosingReAdministrationTeamRequest()
        {
            return View();
        }
        public ActionResult ViewMyClosingReAdministrationTeamRequest()
        {
            return View();
        }
        public ActionResult ClosingReAdministrationTeamRequestActions()
        {
            return View();
        }
        public ActionResult ConfirmClosingReAdministrationTeamRequest()
        {
            return View();
        }
        public ActionResult FinishClosingReAdministrationTeamRequest()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllBatchClosingReAdministrationRequest()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.ClosedBatchReAdministrationRequestBLL.GetAllBatchClosingReAdministrationRequest(),
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
        public JsonResult GetMyBatchClosingReAdministrationRequests(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;

            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.GetMyBatchClosingReAdministrationRequests(searchCriteriaDTO),
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
        [CheckRequestLockedobj("BatchClosingReAdministrationRequestID", (int)StaticEnums.Entity.ReAdministrationRequest, "batchClosingReAdministrationRequestDTO")]
        public JsonResult AssignBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.ClosedBatchReAdministrationRequestBLL.AssignBatchClosingReAdministrationRequest(batchClosingReAdministrationRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Batch Is Assigned To You Succefully",
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
        [CheckRequestLockedobj("BatchClosingReAdministrationRequestID", (int)StaticEnums.Entity.ReAdministrationRequest, "batchClosingReAdministrationRequestDTO")]
        public JsonResult ConfirmBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.ClosedBatchReAdministrationRequestBLL.ConfirmBatchClosingReAdministrationRequest(batchClosingReAdministrationRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Recive Batch Confirmed Succefully",
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
        [CheckRequestLockedobj("BatchClosingReAdministrationRequestID", (int)StaticEnums.Entity.ReAdministrationRequest, "batchClosingReAdministrationRequestDTO")]
        public JsonResult FinishBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.ClosedBatchReAdministrationRequestBLL.FinishBatchClosingReAdministrationRequest(batchClosingReAdministrationRequestDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Batch Is Finished and New Archiving Ticket is Created With ID = " + batchClosingReAdministrationRequestDTO.ArchivingSystemTicketNo,
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
        public JsonResult GetRequestData(int ID)
        {
            ResultViewModel result;

            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.ClosedBatchReAdministrationRequestBLL.GetRequestData(ID),
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