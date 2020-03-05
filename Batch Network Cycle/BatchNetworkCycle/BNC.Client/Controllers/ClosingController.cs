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
    public class ClosingController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AddClosingTeamRequest()
        {

            return View();
        }
        public ActionResult ClosingTeamRequestActions()
        {

            return View();
        }
        public ActionResult ViewAllClosingTeamRequest()
        {

            return View();
        }
        public ActionResult ViewClosingTeamRequest()
        {

            return View();
        }
        public ActionResult ViewMyClosingTeamRequest()
        {

            return View();
        }
        public ActionResult ConfirmClosingTeamRequest()
        {

            return View();
        }
        public ActionResult FinishClosingTeamRequest()
        {

            return View();
        }
        [HttpGet]
        public JsonResult GetAllBatchClosingRequest()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedDataEntryBatchBLL.GetAllBatchClosingRequest(),
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
        public JsonResult GetMyBatchClosingRequests(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.GetMyBatchClosingRequest(searchCriteriaDTO),
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
        [CheckRequestLockedobj("BatchClosingRequestID", (int)StaticEnums.Entity.AuditRequest, "batchClosingRequestDTO")]

        public JsonResult AssignBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.AssignBatchClosingRequest(batchClosingRequestDTO);

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
        [CheckRequestLockedobj("BatchClosingRequestID", (int)StaticEnums.Entity.AuditRequest, "batchClosingRequestDTO")]
        public JsonResult ConfirmBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.ConfirmBatchClosingRequest(batchClosingRequestDTO);

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
        [CheckRequestLockedobj("BatchClosingRequestID", (int)StaticEnums.Entity.AuditRequest, "batchClosingRequestDTO")]

        public JsonResult FinishBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedBncBLL.FinishBatchClosingRequest(batchClosingRequestDTO);

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
        [HttpPost]
        [CheckRequestLockedobj("BatchClosingRequestID", (int)StaticEnums.Entity.AuditRequest, "batchClosingRequestDTO")]

        public JsonResult TransferBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedBncBLL.TransferBatchClosingRequest(batchClosingRequestDTO);

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
        [HttpGet]
        public JsonResult GetAllBatchClosingReAdministrationRequest()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedDataEntryBatchBLL.GetAllBatchClosingReAdministrationRequest(),
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
        [CheckRequestLockedobj("BatchClosingReAdministrationRequestID", (int)StaticEnums.Entity.DataEntryAdminstrationRequest, "batchClosingReAdministrationRequestDTO")]

        public JsonResult AssignBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.AssignBatchClosingReAdministrationRequest(batchClosingReAdministrationRequestDTO);

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
        [CheckRequestLockedobj("BatchClosingReAdministrationRequestID", (int)StaticEnums.Entity.DataEntryAdminstrationRequest, "batchClosingReAdministrationRequestDTO")]
        public JsonResult ConfirmBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.ConfirmBatchClosingReAdministrationRequest(batchClosingReAdministrationRequestDTO);

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
        [CheckRequestLockedobj("BatchClosingReAdministrationRequestID", (int)StaticEnums.Entity.DataEntryAdminstrationRequest, "batchClosingReAdministrationRequestDTO")]
        public JsonResult FinishBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK = Convert.ToInt32(Request.Cookies["ActiveUserIDCookie"].Value);
                UnitOfWork.SharedDataEntryBatchBLL.FinishBatchClosingReAdministrationRequest(batchClosingReAdministrationRequestDTO);

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
        public JsonResult GetRequestData(int ID)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchClosingRequestBLL.GetRequestData(ID),
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