
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

namespace BNC.Client.Controllers
{
    public class BatchingController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {
            return View();
        }
        //------------------------------------------------------------------------------------------
        public ActionResult AddBatchingRequest()
        {
            ViewBag.InsuranceSystem = new SelectList(UnitOfWork.InsuranceSystemBLL.getAllSystemsData(), "InsuranceSystemID", "InsuranceSystemName");
            return View();
        }
        public ActionResult ViewBatchingRequest()
        {
            ViewBag.InsuranceSystem = new SelectList(UnitOfWork.InsuranceSystemBLL.getAllSystemsData(), "InsuranceSystemID", "InsuranceSystemName");
            return View();
        }
        [HttpPost]
        [CheckRequestLockedobj("BatchingFilterationDetailsFK", (int)StaticEnums.Entity.BatchingFilterationDetails, "BatchingInputDTO")]
        public JsonResult AddBatchingRequest(BatchingRequestDTO BatchingInputDTO)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                BatchingRequest newBatchingRequest = UnitOfWork.BatchingRequestBLL.AddBatchingRequest(BatchingInputDTO);
                UnitOfWork.FilterationRequestBLL.changeRemainingClaimsCount(BatchingInputDTO.BatchingFilterationDetailsFK,(int) BatchingInputDTO.NumberOfBatchClaims);
                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Success Add New Batching Request With ID : " + newBatchingRequest.BatchingRequestID,
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
            catch(Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
            
            return Json(Result,JsonRequestBehavior.AllowGet);
        }
        [CheckRequestLockedparameter("BatchID", (int)StaticEnums.Entity.BatchRequest)]
        public JsonResult TransferBatchRequestToAudit(int BatchID)
        {
            ResultViewModel Result;
            Exception outException;
            try
            {
                UnitOfWork.SharedAuditBatchBLL.AddBatchAuditFlowDetails(BatchID);
                //UnitOfWork.AuditFlowBLL.GetNextAuditStep(BatchID);
                if (UnitOfWork.Submit(out outException))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
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
        public JsonResult getBatchData(int BatchID)
        {
            ResultViewModel Result;
            try
            {
                    Result = new ResultViewModel
                    {
                        Data = UnitOfWork.BatchingRequestBLL.getBatchData(BatchID),
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
        public JsonResult getNumberOfClaimsAtBatchingRequest(int BatchingRequestID)
        {
           
            ResultViewModel result;
            try
            {
                var val = UnitOfWork.BatchingRequestBLL.getNumberOfClaimsAtBatchingRequest(BatchingRequestID);
                if (val != -1)
                {
                    result = new ResultViewModel
                    {
                        Data = val,
                        Message = "Success Get Data",
                        Success = true,
                    };
                }
                else
                {
                    result = new ResultViewModel
                    {
                        Data = val,
                        Message = "No Batching Filteration Request Detail For This " + BatchingRequestID,
                        Success = false,
                    };
                }
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
        public JsonResult getNumberOfClaimsAtBatchAuditRequest(int BatchingAuditRequestID)
        {

            ResultViewModel result;
            try
            {
                var val = UnitOfWork.BatchingRequestBLL.getNumberOfClaimsAtBatchAuditRequest(BatchingAuditRequestID);
                if (val != -1)
                {
                    result = new ResultViewModel
                    {
                        Data = val,
                        Message = "Success Get Data",
                        Success = true,
                    };
                }
                else
                {
                    result = new ResultViewModel
                    {
                        Data = val,
                        Message = "No Batching Filteration Request Detail For This " + BatchingAuditRequestID,
                        Success = false,
                    };
                }
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

        //------------------------------------------------------------------------------------------
        //[HttpGet]
        //public JsonResult GetBatchingRequests()
        //{

        //    ResultViewModel result;
        //    try
        //    {               
        //            result = new ResultViewModel
        //            {
        //                Data = UnitOfWork.BatchingRequestBLL.GetBatchingRequests(),
        //                Message = "",
        //                Success = false,
        //            };

        //    }
        //    catch (Exception ex)
        //    {
        //        result = new ResultViewModel
        //        {
        //            Data = null,
        //            Message = ex.Message,
        //            Success = false,
        //        };
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //------------------------------------------------------------------------------------------
        public ActionResult getMyBatching()
        {
            return View();
        }//view
        [HttpGet]
        public JsonResult getMyBatchingData()
        {
            UserDTO User = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value);
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getMyBatching(User.UserID),
                    Message = "sucess get My Batching",
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
        }//page1
        [HttpGet]
        public JsonResult getMyBatchingCount()
        {
            UserDTO User = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value);
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getMyBatchingCount(User.UserID),
                    Message = "sucess get count My Batching",
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
        }//count1
         //------------------------------------------------------------------------------------------
        public ActionResult ShowBatchingRequests()
        {
            return View();
        }//view
        [HttpGet]
        public JsonResult getBatchingCreatedData()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getBatchingCreated(),
                    Message = "sucess get  Batching Created",
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
        }//page2
        [HttpGet]
        public JsonResult getBatchingCreatedCount()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getBatchingCreatedCount(),
                    Message = "sucess get count Batching Created",
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
        }//count2
         //------------------------------------------------------------------------------------------
        public ActionResult getAllPendingBatchingRequestsAtFilitrationBatching()
        {
            return View();
        }//view
        [HttpGet]
        public JsonResult getAllPendingBatchingRequestsAtFilitrationBatchingData()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllPendingBatchingRequestsAtFilitrationBatching(),
                    Message = "sucess get All Pending Batching Requests At Filitration Batching",
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
        }//page3
        [HttpGet]
        public JsonResult getAllPendingBatchingRequestsAtFilitrationBatchingCount()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllPendingBatchingRequestsAtFilitrationBatchingCount(),
                    Message = "sucess get count All Pending Batching Requests At Filitration Batching",
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
        }//count3
         //------------------------------------------------------------------------------------------
        public ActionResult getAllUnderPatchingProcessRequestsAtFilitrationBatching()
        {
            return View();
        }//view
        [HttpGet]
        public JsonResult getAllUnderPatchingProcessRequestsAtFilitrationBatchingData()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllUnderPatchingProcessRequestsAtFilitrationBatching(),
                    Message = "sucess get All Under Patching Batching Requests At Filitration Batching",
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
        }//page4
        [HttpGet]
        public JsonResult getAllUnderPatchingProcessRequestsAtFilitrationBatchingCount()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllUnderPatchingProcessRequestsAtFilitrationBatchingCount(),
                    Message = "sucess get Count All UnderPatching  Requests At Filitration Batching",
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
        }//count4
         //------------------------------------------------------------------------------------------
        public ActionResult ShowBatchingFilterationDetails()
        {
            return View();
        }//view
        [HttpGet]
        public JsonResult getAllFinishedBatchingRequestsAtFilitrationBatchingData()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllFinishedBatchingRequestsAtFilitrationBatching(),
                    Message = "sucess get All Finished Batching Requests At Filitration Batching",
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
        }//page5
        [HttpGet]
        public JsonResult getAllFinishedBatchingRequestsAtFilitrationBatchingCount()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllFinishedBatchingRequestsAtFilitrationBatchingCount(),
                    Message = "sucess get count All Finished Batching Requests At Filitration Batching",
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
        }//count5
         //------------------------------------------------------------------------------------------
        public ActionResult getAllFilitrationBatchingRequests()
        {
            return View();
        }//view
        [HttpGet]
        public JsonResult getAllFilitrationBatchingRequestsData()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllFilitrationBatchingRequests(),
                    Message = "sucess get All Filitration Batching  Requests At Filitration Batching",
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
        }//page6
        [HttpGet]
        public JsonResult getAllFilitrationBatchingRequestsCount()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.getAllFilitrationBatchingRequestsCount(),
                    Message = "sucess get count All Filitration Batching Requests At Filitration Batching",
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
        }//count6
         //------------------------------------------------------------------------------------------

        public JsonResult GetBatchingRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data =
                    UnitOfWork.SharedSearchBLL.GetBatchingRequestsByStatus(SearchCriteria),
                    Message = "",
                    Success = false,
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
        public JsonResult GetBatchingFilterationDetailsByStatus(SearchCriteriaDTO SearchCriteria)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data =
                    UnitOfWork.SharedSearchBLL.GetBatchingFilterationDetailsByStatus(SearchCriteria),
                    Message = "",
                    Success = false,
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
        public ActionResult GetAllBatchingRequests()
        {
            return View();
        }
    }
}