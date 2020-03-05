
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
    public class AuditController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowBatchingAuditRequests()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DirectBatchToAuditBage(int BatchID)
        {
            var AuditDirection = UnitOfWork.AuditFlowBLL.GetNextAuditStep(BatchID);
            return RedirectToAction(AuditDirection.AuditCategoriesActionName, AuditDirection.AuditCategoriesControllerName, new { BatchID= BatchID });

        }
        [HttpGet]
        public JsonResult CheckAuditRequestStatus(int BatchID)
        {
            ResultViewModel Result;
            try
            {
                var AuditDirection = UnitOfWork.AuditFlowBLL.CheckAuditRequestStatus(BatchID);
                Result = new ResultViewModel
                {
                    Data = AuditDirection,
                    Success = true,
                    Message = "Success!!"
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
            return Json(Result, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public JsonResult GetBatchingAuditRequests()
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedAuditBatchBLL.GetBatchingAuditRequests(),
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
        public JsonResult GetBatchingAuditRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {

            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedAuditBatchBLL.GetBatchingAuditRequestsByStatus(SearchCriteria),
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