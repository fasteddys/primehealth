using BNC.BLL.StaticData;
using BNC.Client.Custom_Action_Filters;
using BNC.DTOs.Business;
using BNC.Entities;
using BNC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BNC.Client.Controllers
{
    public class ReconciliationAuditController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult ShowBatchingAuditRequestsReconciliation()
        {
            return View();
        }
        public ActionResult AddReconciliationAuditRequest()
        {

            return View();
        }
        [HttpPost]
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "ReconciliationInputDTO")]

        public JsonResult AddReconciliationAuditRequest(ReconciliationDTO ReconciliationInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //1) add new Medical Audit Request
                ReconciliationAuditRequest newReconciliationAuditRequest = UnitOfWork.SharedAuditBatchBLL.AddReconciliationAuditRequest(ReconciliationInputDTO);
                var BatchAuditRequest = UnitOfWork.BatchAuditingRequestBLL.Find(x => x.BatchingRequestFK == ReconciliationInputDTO.BatchRequestFK).FirstOrDefault();
                UnitOfWork.BatchingRequestBLL.UpdateBatchAuditStatusInReconilitionAudit(BatchAuditRequest);
                // AuditFlowBatchDetail AuditFlowBatchDetail= UnitOfWork.AuditFlowBLL.CompletedNextAudtingStep(ReconciliationInputDTO.BatchRequestFK);
                //if (UnitOfWork.AuditFlowBLL.IsTheLastAudtingStep(ReconciliationInputDTO.BatchRequestFK))
                //{
                //    UnitOfWork.SharedDataEntryBatchBLL.AddDataEntryAdminstrationRequest(ReconciliationInputDTO.BatchRequestFK);
                //}
                // UnitOfWork.AuditFlowBLL.EditBatchAuditRequestStatusOnChangeOfFlow(BatchAuditRequest, AuditFlowBatchDetail);

                //2)save changes
                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Success Add New Reconciliation Audit Request With ID : " + newReconciliationAuditRequest.ReconciliationAuditRequestID,
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
        //------------------------------------------------------------------------------------------
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "ReconciliationInputDTO")]

        public JsonResult AssignReconciliationAuditRequest(ReconciliationDTO ReconciliationInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //1) add new Medical Audit Request
                UnitOfWork.SharedAuditBatchBLL.AssignReconciliationAuditRequest(ReconciliationInputDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Assign Success",
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

        public JsonResult GetReconciliationComment(int BatchID)
        {
            ResultViewModel Result;
            try
            {
               
                    Result = new ResultViewModel
                    {
                        Data = UnitOfWork.SharedAuditBatchBLL.GetReconciliationComment(BatchID),
                        Message = "",
                        Success = true
                    };
              

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