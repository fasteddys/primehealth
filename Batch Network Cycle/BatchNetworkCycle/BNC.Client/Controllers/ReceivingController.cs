using BNC.BLL.Logic.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BNC.DTOs.Business;
using BNC.Entities;
using BNC.Utilities;
using System.Net.Http;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Net.Http.Headers;
using System.Data;
using BNC.Client.Reports;
using BNC.Client.Custom_Action_Filters;
using BNC.BLL.StaticData;
using System.Reflection;
using BNC.Client.Helpers;
namespace BNC.Client.Controllers
{
    public class ReceivingController : BaseController
    {
        // GET: Receiving
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddRecievingRequest()
        {
            ViewBag.ProvidersFK = new SelectList(UnitOfWork.ProvidersBLL.getProvidersData(), "ProviderID", "ProviderName");
            ViewBag.CompaniesFK = new SelectList(UnitOfWork.CompanyBLL.GetAllCompanies(), "CompanyID", "CompanyName");
            ViewBag.GovernmentFK = new SelectList(UnitOfWork.GovernmentBLL.GetAllGovernment(), "GovernmentID", "GovernmentName");
            return View();
        }
        public ActionResult MyRecievingRequest()
        {
            return View();
        }
        public ActionResult ShowRecievingRequest()
        {
            return View();
        }
        public ActionResult GetReceivingRequests()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.ReceivingRequestBLL.GetReceivingRequests(),
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
        public ActionResult ShowRecievingRequests()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddRecievingRequest(RecievingRequestDTOS addRecievingRequestInputDTOS)
        {
            Exception exOutputSubmit;
            ResultViewModel Result;
            try
            {
                ReceivingRequest receivingRequest = UnitOfWork.ReceivingRequestBLL.AddReceivingRequest(addRecievingRequestInputDTOS);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    Result = new ResultViewModel
                    {
                        Message = "Success Add New Recieving Request With ID : " + receivingRequest.ReceivingRequestID,
                        Success = true,
                        Data = null,
                    };

                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(exOutputSubmit, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");
                    Result = new ResultViewModel
                    {
                        Message = exOutputSubmit.Message,
                        Success = false,
                        Data = null,

                    };
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = null,

                };
            }
            return Json(Result);
            
        }
        [CheckRequestLockedparameter("RecievingRequestID", (int)StaticEnums.Entity.ReceivingRequest)]
        public JsonResult TransferRecievingRequestToFiltrationRequest(int RecievingRequestID)
        {
            Exception exOutputSubmit;
            ResultViewModel Result;
            UnitOfWork.ReceivingRequestBLL.TransferRecievingRequestToFiltrationRequest(RecievingRequestID);
            try
            {
                if (UnitOfWork.Submit(out exOutputSubmit))
                {

                    Result = new ResultViewModel
                    {
                        Message = "Recieving  Request ID:" + RecievingRequestID + " Transfered To Filtration Team",
                        Success = true,
                        Data = null,
                    };

                }
                else
                {

                    Result = new ResultViewModel
                    {
                        Message = exOutputSubmit.Message,
                        Success = false,
                        Data = null,

                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = null,

                };
            }

            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        [CheckRequestLockedparameter("RecievingRequestID", (int)StaticEnums.Entity.ReceivingRequest)]
        public ActionResult PrintRecievingReportPDF(int RecievingRequestID)
        {
            try
            {
                Exception exOutputSubmit;
                var PrintRecievingRequest = UnitOfWork.ReceivingRequestBLL.PrintRecievingRequest(RecievingRequestID);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    string path = "";
                    if (PrintRecievingRequest.CompanyFK == (int)StaticEnums.Company.PrimeHealth)
                    {
                        path = System.Web.Hosting.HostingEnvironment.MapPath("/Reports/RecievingRequestPrintPrimeHealth.rdlc");
                    }
                    else if (PrintRecievingRequest.CompanyFK == (int)StaticEnums.Company.MedGulf)
                    {
                        path = System.Web.Hosting.HostingEnvironment.MapPath("/Reports/RecievingRequestPrintMedGulf.rdlc");
                    }

                    RecievingRequestDataSet data = new RecievingRequestDataSet();
                    data.RecievingRequest.AddRecievingRequestRow(
                        PrintRecievingRequest.ReceivingRequestID.ToString(), PrintRecievingRequest.ProviderName.ToString(),
                        PrintRecievingRequest.BilllingMonth.ToString(), PrintRecievingRequest.BillingYear.ToString(),
                        PrintRecievingRequest.ClaimsCount.ToString(), PrintRecievingRequest.ReceivingDate.ToShortDateString(), PrintRecievingRequest.AgentName,
                       PrintRecievingRequest.IbnSinaProviderPin.ToString(), PrintRecievingRequest.MCAProviderPin.ToString()
                        );
                    return File(Print.PrintFile(path, data, "DataSet1", "RecievingRequest", "pdf"), "application/pdf");
                }
                else
                {
                    return null;

                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                return null;

            }

        }
        [CheckRequestLockedparameter("RecievingRequestID", (int)StaticEnums.Entity.ReceivingRequest)]
        public ActionResult RePrintRecievingReportPDF(int RecievingRequestID)
        {
            try
            {
                string path = "";
                LocalReport lr = new LocalReport();
                var ReceivingRequest = UnitOfWork.ReceivingRequestBLL.GetRecievingRequestData(RecievingRequestID);

                if (ReceivingRequest.CompanyFK == (int)StaticEnums.Company.PrimeHealth)
                {
                    path = System.Web.Hosting.HostingEnvironment.MapPath("/Reports/RecievingRequestPrintPrimeHealth.rdlc");
                }
                else if (ReceivingRequest.CompanyFK == (int)StaticEnums.Company.MedGulf)
                {
                    path = System.Web.Hosting.HostingEnvironment.MapPath("/Reports/RecievingRequestPrintMedGulf.rdlc");
                }
                RecievingRequestDataSet data = new RecievingRequestDataSet();

                data.RecievingRequest.AddRecievingRequestRow(
                    ReceivingRequest.ReceivingRequestID.ToString(), ReceivingRequest.ProviderName.ToString(),
                    ReceivingRequest.BilllingMonth.ToString(), ReceivingRequest.BillingYear.ToString(),
                    ReceivingRequest.ClaimsCount.ToString(), ReceivingRequest.ReceivingDate.ToShortDateString(), ReceivingRequest.AgentName,
                    ReceivingRequest.IbnSinaProviderPin.ToString(), ReceivingRequest.MCAProviderPin.ToString()
                    );

                return File(Print.PrintFile(path, data, "DataSet1", "RecievingRequest", "pdf"), "application/pdf");

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                return null;

            }

        }
        public JsonResult GetRecievingRequestData(int ReceivingRequestID)
        {
            ResultViewModel Result;
            
          

                Result = new ResultViewModel
                {
                    Message = "",
                    Success = true,
                    Data = UnitOfWork.ReceivingRequestBLL.GetRecievingRequestData(ReceivingRequestID),
                };

            return Json(Result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReceivingRequestsCount(int FilterationRequestID)

        {
           return Json( UnitOfWork.SharedReceivingFilterationBLL.ReceivingRequestsCount(FilterationRequestID),JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRecievingRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data =
                    UnitOfWork.SharedSearchBLL.GetRecievingRequestsByStatus(SearchCriteria),
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
        [HttpGet]
        public JsonResult MyReceivingRequests(int RecevingOfficerID)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.ReceivingRequestBLL.MyReceivingRequests(RecevingOfficerID),
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
        public ActionResult getRequestLifeCycle()
        {
            ViewBag.ProvidersFK = new SelectList(UnitOfWork.ProvidersBLL.getProvidersData(), "ProviderID", "ProviderName");
            ViewBag.InsuranceSystem = new SelectList(UnitOfWork.InsuranceSystemBLL.getAllSystemsData(), "InsuranceSystemID", "InsuranceSystemName");
            return View();
        }
        [HttpPost]
        public JsonResult getRequestLifeCycleDisplay(LifeCycleRequestInput lifeCycleRequestInput)
        {
            ResultViewModel result;
            if(lifeCycleRequestInput.serachByBitch==1)
            {
                LifeCycleBatchtRequestDTO LifeCycleBatchRequest = UnitOfWork.SharedBncBLL.getBatchRequestLifeCycleByBatchNumber(lifeCycleRequestInput.BatchNumber, lifeCycleRequestInput.BatchSystemFK);

                try
                {
                    if (LifeCycleBatchRequest.BatchingRequest != null)
                    {
                        result = new ResultViewModel()
                        {
                            Data = LifeCycleBatchRequest,
                            Success = true,
                            Message = "Sucess Get Data"

                        };
                    }
                    else
                    {
                        result = new ResultViewModel()
                        {
                            Data = null,
                            Success = false,
                            Message = "Batch Not Founded"

                        };
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    result = new ResultViewModel()
                    {
                        Data = null,
                        Success = false,
                        Message = ex.Message

                    };
                }
                return Json(result);
            }
            else
            {
                List<RequestsRecevingLifeCycleInfo> RecevingLifeCycleRequestsInfoList = UnitOfWork.SharedBncBLL.getRecevingLifeCycleRequests(lifeCycleRequestInput.BillingYear, lifeCycleRequestInput.BilllingMonth, lifeCycleRequestInput.ProviderFK);
                try
                {
                    if (RecevingLifeCycleRequestsInfoList.Count()>0)
                    {
                        result = new ResultViewModel()
                        {
                            Data = RecevingLifeCycleRequestsInfoList,
                            Success = true,
                            Message = "Sucess Get Data"

                        };
                    }
                    else
                    {
                        result = new ResultViewModel()
                        {
                            Data = null,
                            Success = false,
                            Message = "RecievingRequest Not Founded"

                        };
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    result = new ResultViewModel()
                    {
                        Data = null,
                        Success = false,
                        Message = ex.Message

                    };
                }
                return Json(result);
            }
        }
        [HttpGet]
        public JsonResult getRequestLifeCycleDisplayByBatchingID(int BatchingRequestID)
        {
                ResultViewModel result;
         
                LifeCycleBatchtRequestDTO LifeCycleBatchRequest = UnitOfWork.SharedBncBLL.getBatchRequestLifeCycleByBatchId(BatchingRequestID);

                try
                {
                    if (LifeCycleBatchRequest.BatchingRequest != null)
                    {
                        result = new ResultViewModel()
                        {
                            Data = LifeCycleBatchRequest,
                            Success = true,
                            Message = "Sucess Get Data"

                        };
                    }
                    else
                    {
                        result = new ResultViewModel()
                        {
                            Data = null,
                            Success = false,
                            Message = "Batch Not Founded"

                        };
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    result = new ResultViewModel()
                        {
                            Data = null,
                            Success = false,
                            Message = ex.Message

                        };
                }
                return Json(result,JsonRequestBehavior.AllowGet);
        }
    
    }
}
