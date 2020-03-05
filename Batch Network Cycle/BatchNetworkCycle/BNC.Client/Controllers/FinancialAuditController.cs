using BNC.BLL.StaticData;
using BNC.Client.Custom_Action_Filters;
using BNC.DTOs;
using BNC.DTOs.Business;
using BNC.Entities;
using BNC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.Client.Controllers
{
    public class FinancialAuditController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowBatchingAuditRequestsFinancial()
        {
            return View();
        }
        public ActionResult AddFinancialAuditRequest()
        {

            return View();
        }
        //------------------------------------------------------------------------------------------
        [HttpPost]
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "FinancialAuditInputDTO")]

        public JsonResult AddFinancialAuditRequest(FinancialAuditDTO FinancialAuditInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            UserDTO User = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value);
            try
            {
                //1) add new Financial Audit Request
                FinancialAuditRequest newFinancialAuditRequest = UnitOfWork.SharedAuditBatchBLL.AddFinancialAuditRequest(FinancialAuditInputDTO);
                var batchAuditRequest=   UnitOfWork.BatchingRequestBLL.EditNumberOfBatchingClaimsOfFinancialAudit(FinancialAuditInputDTO);
                UnitOfWork.BatchingRequestBLL.UpdateBatchAuditStatusInFindincialAudit(batchAuditRequest);
                //2)save changes
                if (UnitOfWork.Submit(out outException))
                {
                    if(FinancialAuditInputDTO.PendingClaimsAmount>0)
                    {
                        SPReqestDTO newSPReqestDTO = new SPReqestDTO
                        {
                            ReqByUserFk = User.UserID,
                            ReqFK = newFinancialAuditRequest.FinancialAuditRequestID,
                            EntrityFK =(int)StaticEnums.Entity.FinancialAuditRequest,
                            SPStatusFK = (int)SPSTATUS.Pending,//change static enums,
                            ReqByUserNote= FinancialAuditInputDTO.ReqByUserNote

                        };
                        SPRequest spNewRequest=  UnitOfWork.SharedAuditBatchBLL.addSpecialApprovalRequest(newSPReqestDTO);
                        if (UnitOfWork.Submit(out outException))
                        {
                            UnitOfWork.SpReqReasonBLL.AddSpReqReason((int)SPREASONS.Pending, spNewRequest.SPReqID);
                            SPAuditRequest sPAuditRequest = UnitOfWork.SPAuditRequestBLL.AddSPAuditRequest(spNewRequest.SPReqID);
                            if (UnitOfWork.Submit(out outException))
                            {
                              
                                Result = new ResultViewModel
                                {
                                    Data = null,
                                    Message = "Success Add Fininacial Request and  New SPecial Approval and Reasons & sp audit request : " + newFinancialAuditRequest.FinancialAuditRequestID,
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
                        else
                        {
                            Result = new ResultViewModel
                            {
                                Data = null,
                                Message = outException.Message,
                                Success = false
                            };
                        }
                        
                    }
                    else
                    {
                        Result = new ResultViewModel
                        {
                            Data = null,
                            Message = "Success Add New Financial Audit Request With ID : " + newFinancialAuditRequest.FinancialAuditRequestID,
                            Success = true
                        };
                    }

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
        [HttpPost]
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "FinancialAuditInputDTO")]

        public JsonResult AssignFinancialAuditRequest(FinancialAuditDTO FinancialAuditInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                UnitOfWork.SharedAuditBatchBLL.AssignFinancialAuditRequest(FinancialAuditInputDTO);

                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Assign Request Success",
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
        [CheckRequestLockedobj("BatchAuditingRequestID", (int)StaticEnums.Entity.AuditRequest, "BatchRequest")]
        public JsonResult TransferRequestToNextAuditStep(BatchAuditingRequestDTO BatchRequest) {
            ResultViewModel Result;
            Exception outException;
            try
            {
                //1) add new Medical Audit Request
               // FinancialAuditRequest newFinancialAuditRequest = UnitOfWork.SharedAuditBatchBLL.AddFinancialAuditRequest(FinancialAuditInputDTO);
             //   var batchAuditRequest = UnitOfWork.BatchingRequestBLL.EditNumberOfBatchingClaimsOfFinancialAudit(FinancialAuditInputDTO);

                AuditFlowBatchDetail AuditFlowBatchDetail = UnitOfWork.AuditFlowBLL.CompletedNextAudtingStep(BatchRequest.BatchingRequestFK);
                UnitOfWork.AuditFlowBLL.EditBatchAuditRequestStatusOnChangeOfFlow(
                    UnitOfWork.BatchAuditingRequestBLL
                    .Find(x=>x.BatchingRequestFK== BatchRequest.BatchingRequestFK).FirstOrDefault()
                    , AuditFlowBatchDetail);

                if (UnitOfWork.AuditFlowBLL.IsTheLastAudtingStep(BatchRequest.BatchingRequestFK))
                {
                    UnitOfWork.SharedDataEntryBatchBLL.AddDataEntryAdminstrationRequest(BatchRequest.BatchingRequestFK);
                }
                //2)save changes
                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Transfer Succed",
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
        public JsonResult GetfinancialAuditRequestData(int BatchID)
        {
            ResultViewModel Result;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedAuditBatchBLL.GetfinancialAuditRequestData(BatchID),
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