using OnlineApprovals.BLL.UnitOfWork;
using OnlineApprovals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineApprovals.Entities;
using System.IO;
using OnlineApprovals.BLL.CommonFunctions;
using static OnlineApprovals.BLL.StaticData.StaticEnums;

namespace OnlineApprovals.Client.Controllers
{
    public class RequestController : BaseController
    {
        // GET: Request

         [ClientAuthorization]
        public ActionResult OpenNewRequest()
        {
            return View();
        }
        [ClientAuthorization]
        [HttpPost]
        public JsonResult OpenNewRequest(ALLRequestDataDTO RequestData)
        {
            var CheckClaimExist = UnitOfWork.OnlineApprovalRequestBLL.CheckClaimExistence(RequestData.Request.ClaimNumber);

            if (CheckClaimExist== true)
            {
                OnlineApprovals_Requests RequestObj = new OnlineApprovals_Requests();
                List<OnlineApprovals_DemandedDrugsDetails> DrugDetailsObj = new List<OnlineApprovals_DemandedDrugsDetails>();

                DTOMapper Mapper = new DTOMapper();
                RequestObj = Mapper.MapRequestDTO(RequestData.Request);
                DrugDetailsObj = Mapper.MapDrugListDTOToDrugList(RequestData.DemandedDrugs);

                UnitOfWork.OnlineApprovalRequestBLL.InsertNewRequest(RequestObj, DrugDetailsObj);

                UnitOfWork.OnlineApprovalsInvoicesBLL.AddNewInvoice(RequestObj, DrugDetailsObj);
                var success = UnitOfWork.Submit();
                if (success == true)
                {
                    return Json(RequestObj.RequestID, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(-1, JsonRequestBehavior.AllowGet);

            }
                 
        }

        [ClientAuthorization]
        public JsonResult SearchDrugList()
        {
            var SearchDrug = UnitOfWork.OnlineApprovalsDrugsListDIMBLL.GetDrugsListDTO().Where(p=>p.IsDeleted==false).ToList();
            return Json(SearchDrug, JsonRequestBehavior.AllowGet);
        }
        [ClientAuthorization]
        [HttpPost]
        public JsonResult ListDrugsInTable(OnlineApprovals_DrugsListDIM DrugsListDIM, int id)
        {

            var Listdrug = UnitOfWork.OnlineApprovalsDrugsListDIMBLL.GetDruglistByID(id);

            OnlineApprovals_DemandedDrugsDetails DrugDetails = new OnlineApprovals_DemandedDrugsDetails
            {
                DrugName = Listdrug.DrugName,
                UnitQuantity = Listdrug.UnitQuaninty,
            };

            return Json(DrugDetails, JsonRequestBehavior.AllowGet);
        }
        [ClientAuthorization]
        public ActionResult ManageRequest(int RequestID)
        {
            var Request = UnitOfWork.OnlineApprovalRequestBLL.GetAllRequestData(RequestID);
            return View(Request);
        }
        [ClientAuthorization]
        public PartialViewResult _ManageRequest(int id)
        {
            var Request = UnitOfWork.OnlineApprovalRequestBLL.GetAllRequestData(id);
            return PartialView(Request);
        }
        [ClientAuthorization]
        [ValidateInput(false)]
        public JsonResult AddReply(RequestDetailsDTO RequestDetails)
        {
            string BasePath = UnitOfWork.OnlineApprovalsConfigrationBLL.GetAll().Where(p => p.ConfigrationKey == "AttachmentsPath").Select(p => p.Configration).ToString();

            try
            {
                DTOMapper Mapper = new DTOMapper();
                var RequestDetailsOpj = Mapper.MapRequestDetailsDTO(RequestDetails);
                RequestDetailsOpj.DetailsDate = DateTime.Now;
                
                UnitOfWork.OnlineApprovalRequestDetailsBLL.Add(RequestDetailsOpj);
                OnlineApprovals_Requests RequestObj = UnitOfWork.OnlineApprovalRequestBLL.GetById(RequestDetailsOpj.RequestID);
                RequestObj.RequestStatusID =(int) RequestStatus.PendingCallCenterAction;
                UnitOfWork.OnlineApprovalRequestBLL.Edit(RequestObj);
                bool success = UnitOfWork.Submit();
                if (success == true)
                {
                    return Json(RequestDetailsOpj.RequestDetailsID, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception)
            {

                throw;
            }


        }
        [ClientAuthorization]
        [HttpPost]
        public JsonResult AddReplyAttach(string tt, string tx)
        {
            try
            {
                string BasePath = UnitOfWork.OnlineApprovalsConfigrationBLL.GetAll().Where(p => p.ConfigrationKey == "AttachmentsPath").FirstOrDefault().Configration;
                AjaxCallObjectDTO AjaxObj = new AjaxCallObjectDTO();

                string Path1 = BasePath + "/" + tt + "/" + tx + "/";
                string SavePath = Path1.Replace("/", @"\");

                if (!Directory.Exists(Path1))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path1);
                }

                for (int i = 0; i < Request.Files.Count; i++)
                {

                    OnlineApprovals_RequestAttachment Temp = new OnlineApprovals_RequestAttachment();
                    var file = Request.Files[i];
                    Temp.AttachmentName = Path.GetFileName(file.FileName);
                    Temp.AttachmentPath = SavePath + file.FileName;
                    string Path2 = Path1 + file.FileName;
                    Temp.RequestID = int.Parse(tt);
                    Temp.AttachmentTypeID = 2;
                    Temp.IsDeleted = false;
                    Temp.RequestDetailsID = int.Parse(tx);
                    file.SaveAs(Path2);
                    UnitOfWork.OnlineApprovalRequestAttachmentBLL.Add(Temp);
                    bool success = UnitOfWork.Submit();


                }

                AjaxObj.ValidationID = 1;
                AjaxObj.Message = "Your Comment has been submitted successfully";
                return Json(AjaxObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                AjaxCallObjectDTO AjaxObj = new AjaxCallObjectDTO();
                AjaxObj.ValidationID = 0;
                AjaxObj.Message = "Sorry ...Your Attachment(s) has not been uploded ,please try to upload it again";
                return Json(AjaxObj, JsonRequestBehavior.AllowGet);
                throw;
            }

        }
        [ClientAuthorization]
        public JsonResult AddRequestAttach(string tt)
        {
             AjaxCallObjectDTO AjaxObj = new AjaxCallObjectDTO();
            string BasePath = UnitOfWork.OnlineApprovalsConfigrationBLL.GetAll().Where(p => p.ConfigrationKey == "AttachmentsPath").FirstOrDefault().Configration;

            string Path1 = BasePath + "/" + tt + "/" ;
            string SavePath = Path1.Replace("/", @"\");

            if (!Directory.Exists(Path1))
            {
                DirectoryInfo di = Directory.CreateDirectory(Path1);
            }

            for (int i = 0; i < Request.Files.Count; i++)
            {

                OnlineApprovals_RequestAttachment Temp = new OnlineApprovals_RequestAttachment();
                var file = Request.Files[i];
                Temp.AttachmentName = Path.GetFileName(file.FileName);
                Temp.AttachmentPath = SavePath + file.FileName;
                string Path2 = Path1 + file.FileName;
                Temp.RequestID = int.Parse(tt);
                Temp.AttachmentTypeID = 2;
                Temp.IsDeleted = false;            
                file.SaveAs(Path2);
                UnitOfWork.OnlineApprovalRequestAttachmentBLL.Add(Temp);
                bool success = UnitOfWork.Submit();
            }


            string message = "success";
            ViewBag.message = "success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [ClientAuthorization]
        public FileResult FileDownload(string path, string name)
        {
           string FileDownloadPath = path ;
            if (!string.IsNullOrEmpty(FileDownloadPath))
            {
                //var vFullFileName = HostingEnvironment.MapPath(path);

                var file = new FileInfo(FileDownloadPath);
                if (file.Exists)
                {
                    return File(FileDownloadPath, "content-disposition", name);
                }
            }

            //file is empty, so return null
            return null;
        }
        [ClientAuthorization]
        public ActionResult ShowRequestList()
        {


            var Request = UnitOfWork.OnlineApprovalRequestBLL.GetRequestsByprovider();
            foreach(var item in Request)
            {
            var Color    = (StatusColor)Enum.Parse(typeof(StatusColor), item.StatusID.ToString());

                item.Color = Color.ToString();

                var Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), item.StatusID.ToString());

                item.Status = Status.ToString();


            }



            return View(Request);
        }
        [ClientAuthorization]
        public ActionResult ShowRequestLists(int SID)
        {
            var Request = UnitOfWork.OnlineApprovalRequestBLL.GetRequestsByStatusAndProvider(SID);
            foreach (var item in Request)
            {
                var Color = (StatusColor)Enum.Parse(typeof(StatusColor), item.StatusID.ToString());

                item.Color = Color.ToString();

                var Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), item.StatusID.ToString());

                item.Status = Status.ToString();


            }



            return View("ShowRequestList", Request);
        }
        [ClientAuthorization]
        public JsonResult ReopenRequest(int ID ,string tz)
        {
            bool Success = UnitOfWork.OnlineApprovalRequestBLL.ReopenRequest(ID,tz);
            AjaxCallObjectDTO AjaxObj = new AjaxCallObjectDTO();
            if (Success==true)
            {
                UnitOfWork.Submit();
                AjaxObj.ValidationID = 1;
                AjaxObj.Message = "Request has been Reopened Successfuly";
                return Json(AjaxObj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AjaxObj.ValidationID = 0;
                AjaxObj.Message = "Failed To Reopen Request...,please Try again";
                return Json(AjaxObj, JsonRequestBehavior.AllowGet);
            }
            
        }
        [ClientAuthorization]
        public JsonResult TerminateRequest(int ID, string tq )
        {
            AjaxCallObjectDTO AjaxObj = new AjaxCallObjectDTO();

            bool Success = UnitOfWork.OnlineApprovalRequestBLL.TerminateRequest(ID, tq, out string ValidationMessage);
            if (Success == true)
            {
                UnitOfWork.Submit();
                AjaxObj.ValidationID = 1;
                AjaxObj.Message = ValidationMessage;
                return Json(AjaxObj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AjaxObj.ValidationID = 0;
                AjaxObj.Message = ValidationMessage;
                return Json(AjaxObj, JsonRequestBehavior.AllowGet);
            }

        }
        [ClientAuthorization]
        public PartialViewResult _ViewInvoice (int ID)
        {
            Invoice_DrugDetailsDTO InvoiceDtails = new Invoice_DrugDetailsDTO();

            InvoiceDtails.Invoice = UnitOfWork.OnlineApprovalsInvoicesBLL.GetInvoiceByRequest(ID);
            InvoiceDtails.ListDrugsDetail = new List<DrugsDetailDTO>();
            InvoiceDtails.ListDrugsDetail = UnitOfWork.OnlineApprovalsDrugsDetailBLL.GetDrugDetailsList(ID);


            return PartialView (InvoiceDtails);

        }
    }
}
