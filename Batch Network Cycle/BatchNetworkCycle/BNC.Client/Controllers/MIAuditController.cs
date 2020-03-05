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
    public class MIAuditController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult ShowBatchingAuditRequestsMI()
        {
            return View();
        }
        //------------------------------------------------------------------------------------------
        public ActionResult AddMIAuditRequest()
        {

            return View();
        }
        [HttpPost]
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "MIAuditInputDTO")]

        public JsonResult AddMIAuditRequest(MIAuditDTO MIAuditInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //1) add new Medical Audit Request
                MIAuditRequest newMIAuditRequest = UnitOfWork.SharedAuditBatchBLL.AddMIAuditRequest(MIAuditInputDTO);
                var BatchAuditRequest = UnitOfWork.BatchAuditingRequestBLL.Find(x => x.BatchingRequestFK == MIAuditInputDTO.BatchRequestFK).FirstOrDefault();
                UnitOfWork.BatchingRequestBLL.UpdateBatchAuditStatusInMIAudit(BatchAuditRequest);

                //AuditFlowBatchDetail AuditFlowBatchDetail = UnitOfWork.AuditFlowBLL.CompletedNextAudtingStep(MIAuditInputDTO.BatchRequestFK);
                //if (UnitOfWork.AuditFlowBLL.IsTheLastAudtingStep(MIAuditInputDTO.BatchRequestFK))
                //{
                //    UnitOfWork.SharedDataEntryBatchBLL.AddDataEntryAdminstrationRequest(MIAuditInputDTO.BatchRequestFK);
                //}

                //  UnitOfWork.AuditFlowBLL.EditBatchAuditRequestStatusOnChangeOfFlow(BatchAuditRequest, AuditFlowBatchDetail);


                //2)save changes
                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Success Add New MI Audit Request With ID : " + newMIAuditRequest.MIAuditRequestID,
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
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "MIAuditInputDTO")]
        public JsonResult AssignMIAuditRequest(MIAuditDTO MIAuditInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //1) add new Medical Audit Request
                UnitOfWork.SharedAuditBatchBLL.AssignMIAuditRequest(MIAuditInputDTO);

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
        
        //------------------------------------------------------------------------------------------
        public JsonResult GetMIComment(int BatchID)
        {
            ResultViewModel Result;
            try
            {

                Result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedAuditBatchBLL.GetMIComment(BatchID),
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