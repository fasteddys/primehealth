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
    public class MedicalAuditController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AddMedicalAuditRequest()
        {

            return View();
        }
        public ActionResult ShowBatchingAuditRequestsMedical()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "MedicalAuditInputDTO")]
        [HttpPost]
        public JsonResult AddMedicalAuditRequest(MedicalAuditDTO MedicalAuditInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            UserDTO User = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value);

            try
            {
                //1) add new Medical Audit Request
                MedicalAuditRequest newMedicalAuditRequest = UnitOfWork.SharedAuditBatchBLL.AddMedicalAuditRequest(MedicalAuditInputDTO);
               var BatchAuditRequest= UnitOfWork.BatchingRequestBLL.EditNumberOfBatchingClaimsOfMedicalAudit(MedicalAuditInputDTO);
               //var AuditFlowBatchDetail= UnitOfWork.AuditFlowBLL.CompletedNextAudtingStep(MedicalAuditInputDTO.BatchRequestFK);
                UnitOfWork.BatchingRequestBLL.UpdateBatchAuditStatusInMidecalAudit(BatchAuditRequest);

               //UnitOfWork.AuditFlowBLL.EditBatchAuditRequestStatusOnChangeOfFlow(BatchAuditRequest, AuditFlowBatchDetail);

                //2)save changes
                if (UnitOfWork.Submit(out outException))
                {

                    if (MedicalAuditInputDTO.NumberOfPendingClaims > 0)
                    {
                        SPReqestDTO newSPReqestDTO = new SPReqestDTO
                        {
                            ReqByUserFk = User.UserID,
                            ReqFK = newMedicalAuditRequest.MedicalAuditRequestID,
                            EntrityFK = (int)StaticEnums.Entity.MedicalAuditRequest,
                            SPStatusFK = (int)SPSTATUS.Pending,//change static enums,
                            ReqByUserNote = MedicalAuditInputDTO.ReqByUserNote

                        };
                        SPRequest spNewRequest = UnitOfWork.SharedAuditBatchBLL.addSpecialApprovalRequest(newSPReqestDTO);
                        if (UnitOfWork.Submit(out outException))
                        {
                            UnitOfWork.SpReqReasonBLL.AddSpReqReason((int)SPREASONS.Pending, spNewRequest.SPReqID);
                            SPAuditRequest sPAuditRequest = UnitOfWork.SPAuditRequestBLL.AddSPAuditRequest(spNewRequest.SPReqID);
                            if (UnitOfWork.Submit(out outException))
                            {
                               
                                Result = new ResultViewModel
                                {
                                    Data = null,
                                    Message = "Success Add New Medical Audit & speciall approval & Reasons &sp Audit  Request With ID : " + newMedicalAuditRequest.MedicalAuditRequestID,
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
                    else
                    {
                        Result = new ResultViewModel
                        {
                            Data = null,
                            Message = "Success Add New Medical Audit Request With ID : " + newMedicalAuditRequest.MedicalAuditRequestID,
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

        [HttpPost]
        [CheckRequestLockedobj("BatchAuditRequestFK", (int)StaticEnums.Entity.AuditRequest, "MedicalAuditInputDTO")]
        public JsonResult AssignMedicalAuditRequest(MedicalAuditDTO MedicalAuditInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
              UnitOfWork.SharedAuditBatchBLL.AssignMedicalAuditRequest(MedicalAuditInputDTO);
             
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

        //------------------------------------------------------------------------------------------
        public JsonResult GetMedicalAuditRequestData(int BatchID)
        {
            ResultViewModel Result;
            try
            {
                    Result = new ResultViewModel
                    {
                        Data = UnitOfWork.SharedAuditBatchBLL.GetMedicalAuditRequestData(BatchID),
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