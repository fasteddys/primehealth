using BNC.BLL.StaticData;
using BNC.Client.Authorize;
using BNC.Client.Custom_Action_Filters;
using BNC.DTOs;
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
    public class FilterationController : BaseController
    {
       // [CheckRequestLockedparameter("RecievingRequestID", (int)StaticEnums.Entity.ReceivingRequest)]

        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
         //[BNCAuthorizeAttribute]
        public ActionResult CreateFilterationRequest(int ReceivingRequestID)
        {
            ViewBag.FilterationRequest = UnitOfWork.FilterationRequestBLL.Find(x=>x.ReceivingRequestFK== ReceivingRequestID).FirstOrDefault();
            return View();
        }
        
        public ActionResult ShowFilterationRequest()
        {
            return View();
        }
        public ActionResult UnderFilterationRequestProcess()
        {
            return View();
        }
        [BNCAuthorizeAttribute]
        [HttpPost]
        public JsonResult CreateFilterationRequestDetial(List<FilterationRequestDetialsDTO> ListFilterationRequestDetials)
        {
            Exception exOutputSubmit;
            ResultViewModel Result;
            try
            {
               // UserDTO User = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value);


                foreach (var item in ListFilterationRequestDetials)
                {
                    UnitOfWork.FilterationRequestBLL.CreateFilterationRequestDetial(item);
                }
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Success = true,
                        Message = "Success"
                    };

                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(exOutputSubmit, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    Result =  new ResultViewModel
                    {
                        Data = null,
                        Success = false,
                        Message = exOutputSubmit.Message

                    };
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
            return Json(Result);
        }
        [HttpGet]
        public JsonResult GetAllCategoryies()
        {
             
               ResultViewModel Result;
            try
            {
                List<FilterationCategoryDTO> FilterationCategories = new List<FilterationCategoryDTO>();
                foreach (var item in UnitOfWork.FilterationCategoriesBLL.Find(x => x.IsActive == true && x.IsDeleted == false).ToList())
                {
                    FilterationCategories.Add(new FilterationCategoryDTO
                    {
                        FilterationCategoryID = item.FilterationCategoriesID,
                        FilterationCategoryName = item.FilterationCategoryName

                    });
                }
                    Result = new ResultViewModel
                    {
                        Data = FilterationCategories,
                        Success = true,
                        Message = "Success"
                    };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
            return Json(Result,JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------
        public JsonResult GetCountOfClaimsOfFilterationRequest(int FilterationRequestID)
        {
            ResultViewModel result;
            try
            {
             var val = UnitOfWork.FilterationRequestBLL.GetCountOfClaimsOfFilterationRequest(FilterationRequestID);
                result = new ResultViewModel
                {
                    Data = val,
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
        //------------------------------------------------------------------------------
        public JsonResult getNumberOfClaimsAtFilterationRequestDetail(int FilterationRequestDetailID)
        {
            ResultViewModel result;
            try
            {
                var val = UnitOfWork.FilterationRequestBLL.getNumberOfClaimsAtFilterationRequestDetail(FilterationRequestDetailID);
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
                        Message = "No Filteration Request Detail For This " + FilterationRequestDetailID,
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
        //------------------------------------------------------------------------------

        public JsonResult CloseFiltrationRequest( List<FilterationRequestDetialsDTO> ListFilterationRequestDetials)
        {
            
            Exception exOutputSubmit;
            ResultViewModel Result;

            try
            {
                foreach (var item in ListFilterationRequestDetials)
                {
                    UnitOfWork.FilterationRequestBLL.CreateFilterationRequestDetial(item);
                }
                UnitOfWork.SharedReceivingFilterationBLL.CloseFiltrationRequest(ListFilterationRequestDetials.FirstOrDefault().FilterationRequestID);

                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Success = true,
                        Message = "Success"
                    };

                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(exOutputSubmit, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    Result = new ResultViewModel
                    {
                        Data = null,
                        Success = false,
                        Message = exOutputSubmit.Message

                    };
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
            return Json(Result);
        }
         [CheckRequestLockedparameter("FilterationRequestID", (int)StaticEnums.Entity.FilterationRequest)]

        public JsonResult SendFiltrationRequestToBatching(int FilterationRequestID)
        {
            Exception exOutputSubmit;
            ResultViewModel Result;
            try
            {
                UnitOfWork.SharedReceivingFilterationBLL.SendFiltrationRequestToBatching(FilterationRequestID);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Success = true,
                        Message = "Success!!"
                    };

                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(exOutputSubmit, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    Result = new ResultViewModel
                    {
                        Data = null,
                        Success = false,
                        Message = exOutputSubmit.Message

                    };
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
            return Json(Result,JsonRequestBehavior.AllowGet);
        }
        //------------------------------------------------------------------------------------------
        [HttpGet]
        public JsonResult GetBatchingFilterationDetails()
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.BatchingRequestBLL.GetBatchingFilterationDetails(),
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
        public JsonResult GetFilterationRequestStatus(int FilterationRequestID)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.FilterationRequestBLL.GetFilterationRequestStatus(FilterationRequestID),
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
        public JsonResult GetFilterationRequestDetails(int FilterationRequestID)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.FilterationRequestBLL.GetFilterationRequestDetails(FilterationRequestID),
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
        public JsonResult GetFilterationRequestBeforeBatching(int FilterationRequestID)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.FilterationRequestBLL.GetFilterationRequestBeforeBatching(FilterationRequestID),
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

        public JsonResult GetFilterationRequests(SearchCriteriaDTO searchCriteriaDTO)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.GetFilterationRequestsByStatus(searchCriteriaDTO),
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
        public ActionResult GetAllFilterationRequests()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetFilterationRequestByStatus(SearchCriteriaDTO SearchCriteriaDTO)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedSearchBLL.GetFilterationRequestsByStatus(SearchCriteriaDTO),
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
               


    }
}