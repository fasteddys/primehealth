using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenterProviderApprovals.DTO;
using System.IO;
using CallCenterProviderApprovals.Enum;
using CallCenterProviderApprovals.Helper;
using static CallCenterProviderApprovals.Enum.TechnicalDestinationEnums;
using System.Net.Http;
using ExcelDataReader;
using System.Data;
using System.Net;
using ClosedXML.Excel;
using System.Net.Http.Headers;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelNumberFormat;

namespace CallCenterProviderApprovals.Controllers
{
    public class RequestController : Controller
    {
        PhNetworkEntities db = new PhNetworkEntities();
        // GET: Request


        [Authorize]

        public ActionResult ManageRequest(int ID)
        {
            int UserTypeID = int.Parse(@Request.Cookies["UserTypeCookie"].Value);
            AllRequestDataDTO RequestToView = new AllRequestDataDTO();
            var OriginalRequest = (from r in db.EmailApprovalRequests where r.ID == ID select r).SingleOrDefault();
            var Attachments = (from a in db.EmailRequestAttachmentsDetails where a.TicketNumber == ID select a).ToList();
            RequestToView.Request = OriginalRequest;
            RequestToView.Attachments = Attachments;

            var TechnicalApprovalRequest = (from t in db.EmailRequestRequest_TechnicalDestination where t.RequestID == ID && t.TechnicalDestinationID == UserTypeID select t).SingleOrDefault();
            RequestToView.TechnicalApprovalRequest = TechnicalApprovalRequest;
            if (TechnicalApprovalRequest.AssignTime == null && TechnicalApprovalRequest.ActionTime == null)
            {
                RequestToView.TechnicalApprovalStatus = (int)TechnicalDestinationEnums.TechnicalAprrovalRequestStatus.PendingAssign;
            }
            if (TechnicalApprovalRequest.AssignTime != null && TechnicalApprovalRequest.ActionTime == null)
            {
                RequestToView.TechnicalApprovalStatus = (int)TechnicalDestinationEnums.TechnicalAprrovalRequestStatus.Assigned;
            }
            if (TechnicalApprovalRequest.AssignTime != null && TechnicalApprovalRequest.ActionTime != null)
            {
                RequestToView.TechnicalApprovalStatus = (int)TechnicalDestinationEnums.TechnicalAprrovalRequestStatus.Done;
            }
            ViewBag.Location = db.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "SaveTicketAttachmentPath").FirstOrDefault().ConfigurationValue;


            return View(RequestToView);
        }
        public ActionResult ClientResponseTime()
        {
            return View();
        }
        [AcceptVerbs("Post")]
        public FileContentResult MyFileUpload()
        {
          //  HttpResponseMessage Result = new HttpResponseMessage();
            List<Int32> ListOfMedicalId = new List<Int32>();
            //var fi = Request.Files[0];
            ListOfMedicalId = GetMedicalIdsFromExcel(Request);
            var ExtractedData = new List< EmailApprovalRequest>();
             using (PhNetworkEntities Ph=new PhNetworkEntities())
            {
             ExtractedData=    Ph.EmailApprovalRequests.Where(x => ListOfMedicalId.Contains((int)x.Medical_ID)).ToList();
            }
            //Result=
              return  GetAllRejectionReasonExcel(ExtractedData);
          //  return Result;

        }


        public FileContentResult GetAllRejectionReasonExcel(/*ExcelClientResponseTimeDTO*/ List<EmailApprovalRequest> ExtractedData)
        {
            List<EmailApprovalRequest> rejectionReasonOutputDTOList = ExtractedData;
            DataTable dtRequest = new DataTable();
            dtRequest.Columns.Add("ID", typeof(int));
            dtRequest.Columns.Add("Medical_ID", typeof(int));
            dtRequest.Columns.Add("CreationDate", typeof(DateTime));
            dtRequest.Columns.Add("ClosedTime", typeof(DateTime));
            dtRequest.Columns.Add("Elapsed Time", typeof(double));
            dtRequest.Columns.Add("OperatorAssignee");
            dtRequest.Columns.Add("DoctorAssignee");
            dtRequest.Columns.Add("AuditAssignee");
            dtRequest.Columns.Add("MailSource");
            dtRequest.Columns.Add("IsFaxMail");
            dtRequest.Columns.Add("MailSubject");
            dtRequest.Columns.Add("TicketTypeID");
            dtRequest.Columns.Add("IsAutoGenereated");

            






            foreach (var item in rejectionReasonOutputDTOList)
            {

                TimeSpan ellapsTime = (TimeSpan)(item.ClosedTime - item.CreationDate.Value);
                dtRequest.Rows.Add(item.ID, item.Medical_ID, item.CreationDate,
                     item.ClosedTime,ellapsTime.TotalMinutes
                     , item.OperatorAssignee, item.DoctorAssignee,
                      item.AuditAssignee, item.MailSource, item.IsFaxMail,
                       item.MailSubject,
                       TicketTypes.GetName(typeof(TicketTypes), item.TicketTypeID)
                       , item.IsAutoGenereated

                    );
            }

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "CallCenter");
            var sheet = wb.Worksheet("CallCenter");
           // int i = 0;
            double Total = 0;
            for (int i=0; i < sheet.Column(5).Cells().Count();i++) /*IXLCell cell in sheet.Column(5).Cells())*/
            {
                if (i != 0)
                {
                    Total = Total + Convert.ToDouble(sheet.Column(5).Cell(i).Value);
                    //  cell.Formatting = NumberFormat;
                    if (sheet.Column(5).Cells().Count() == i+1)
                    {
                        sheet.Column(5).Cell(i + 2).Value = Total;
                    }
                }
                i++;
            }
            wb.SaveAs(stream);

            return File(stream.ToArray(), string.Format("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

        }







        public List<Int32> GetMedicalIdsFromExcel(HttpRequestBase Request)
        {
            List<Int32> ListOfMedicalId = new List<Int32>();
            
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    string fileExtension = System.IO.Path.GetExtension(Request.Files[0].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        IExcelDataReader excelReader;
                        if (fileExtension == ".xls")
                        {
                            using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                            {
                                var stream = new MemoryStream(binaryReader.ReadBytes(file.ContentLength));

                                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            }
                        }
                        else
                        {
                            using (var binaryReader = new BinaryReader(Request.Files[i].InputStream))
                            {
                                var stream = new MemoryStream(binaryReader.ReadBytes(file.ContentLength));

                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            }
                            //  excelReader.IsFirstRowAsColumnNames = true;
                            DataSet ds = excelReader.AsDataSet();

                            DataTable Dt = ds.Tables[0];

                            for (int j = 0; j < Dt.Rows.Count; j++)
                            {
                                if (j != 0)
                                {
                                    //var m = Dt.Rows[j].ItemArray.GetValue(0).ToString();
                                    try
                                    {
                                        ListOfMedicalId.Add(Convert.ToInt32(Dt.Rows[j].ItemArray.GetValue(0).ToString()));
                                    }
                                    catch { continue; }
                                }
                            }
                        }

                    }


                }
            }
            return ListOfMedicalId;

        }






        [Authorize]
        public PartialViewResult _ManageRequest(int ID)
        {
            int UserTypeID = int.Parse(@Request.Cookies["UserTypeCookie"].Value);
            AllRequestDataDTO RequestToView = new AllRequestDataDTO();
            var OriginalRequest = (from r in db.EmailApprovalRequests where r.ID == ID select r).SingleOrDefault();
            var Attachments = (from a in db.EmailRequestAttachmentsDetails where a.TicketNumber == ID select a).ToList();
            RequestToView.Request = OriginalRequest;
            RequestToView.Attachments = Attachments;
            var TechnicalApprovalRequest = (from t in db.EmailRequestRequest_TechnicalDestination where t.RequestID == ID && t.TechnicalDestinationID == UserTypeID select t).SingleOrDefault();
            if (TechnicalApprovalRequest.AssignTime == null && TechnicalApprovalRequest.ActionTime == null)
            {
                RequestToView.TechnicalApprovalStatus = (int)TechnicalDestinationEnums.TechnicalAprrovalRequestStatus.PendingAssign;
            }
            if (TechnicalApprovalRequest.AssignTime != null && TechnicalApprovalRequest.ActionTime == null)
            {
                RequestToView.TechnicalApprovalStatus = (int)TechnicalDestinationEnums.TechnicalAprrovalRequestStatus.Assigned;
            }
            if (TechnicalApprovalRequest.AssignTime != null && TechnicalApprovalRequest.ActionTime != null)
            {
                RequestToView.TechnicalApprovalStatus = (int)TechnicalDestinationEnums.TechnicalAprrovalRequestStatus.Done;
            }

            return PartialView(RequestToView);
        }

        public FileResult FileDownload(string path, string name)
        {

            path = path.Replace("~", "");

            //if (!string.IsNullOrEmpty(path))
            //{

            //    //var vFullFileName = HostingEnvironment.MapPath(path);

            //    var file = new FileInfo(path);
            //    //if (file.Exists)
            //    //{
            //        return File(path, "content-disposition", name);
            //    //}
            //}
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = name;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);




                  }









        [Authorize]

        [HttpPost]
        public JsonResult AssignRequest(int ID)
        {
            int UserTypeID = int.Parse(@Request.Cookies["UserTypeCookie"].Value);
            int LogTypeID = 0;
            string Team = TechnicalDestination.GetName(typeof(TechnicalDestination), UserTypeID);

           if ((int) TechnicalDestination.Providers== UserTypeID)
            {
                 LogTypeID = (int)EmailApprovalLogTypes.ProviderAssign;
            }
            else
            {
                LogTypeID = (int)EmailApprovalLogTypes.AccountManagerAssign;
            }
            string message;
            var AssignedRequest = (from r in db.EmailRequestRequest_TechnicalDestination where r.RequestID == ID && r.TechnicalDestinationID == UserTypeID select r).SingleOrDefault();
            if (AssignedRequest.Assignee == null)
            {
                AssignedRequest.Assignee = @Request.Cookies["UserNameCookie"].Value;
                AssignedRequest.AssignTime = DateTime.Now;
                message = "Success";
                Helpers helper = new Helpers();
                helper.LogEmailApprovalEvent(
                     AssignedRequest.RequestID, LogTypeID,
                     Convert.ToInt32(@Request.Cookies["UserIDCookie"].Value),
                     @Request.Cookies["UserNameCookie"].Value,
                     "","", @Request.Cookies["UserNameCookie"].Value+
                     Team
                     + " Assign Request for Technical Approve");

                db.SaveChanges();
            }
            else
            {
                message = "Ticket is Assigned with someone else";
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [Authorize]

        public JsonResult AddAttachJson(int RequestID)
        {
            int UserTypeID = int.Parse(@Request.Cookies["UserTypeCookie"].Value);
            string PathLocation = db.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "SaveTicketAttachmentPath").FirstOrDefault().ConfigurationValue;

            for (int i = 0; i < Request.Files.Count; i++)
            {
                string  strMappath = "";
                bool IsProviderAttach = false;
                bool IsAccountManagerAttach = false;

                if ((int)TechnicalDestination.Providers == UserTypeID)
                {
                    strMappath="/EmailRequestAttach/"+ RequestID + "/ProdviderAttachment/";
                    IsProviderAttach = true;
                }
                else
                {
                    strMappath = "/EmailRequestAttach/" + RequestID + "/AccountManager/";
                    IsAccountManagerAttach = true;

                }

                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                string[] FileArray = fileName.Split('.');
                string extension = FileArray[1];
                //string fname = t.ToString() + "_" + d.ToString() + "_" + i + "." + extension;
                var path = PathLocation + strMappath + fileName;
                if (!Directory.Exists(PathLocation+strMappath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(PathLocation + strMappath);
                }
                file.SaveAs(path);
                db.EmailRequestAttachmentsDetails.Add(new EmailRequestAttachmentsDetail
                {
                    Path = strMappath + fileName,
                    TicketNumber = RequestID,
                    IsAuditAttachment = false,
                    Name = fileName,
                    IsDeleted = false,
                    IsFaxAttachment = false,
                    IsDoctorAttachment = false,
                    IsOtherAttachment = false,
                    IsReopeningAttachment = false,
                    IsTicketAttachment = false,
                    IsTransferToAuditAttach = false,
                    IsAuditTechnicalApproveAttachment = false,
                    IsDoctorTechnicalApproveAttachment = false,
                    IsAccountManagerTechnicalApproveAttachment = IsAccountManagerAttach,
                    IsProviderTechnicalApproveAttachment =  IsProviderAttach
                });
                db.SaveChanges();

   
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SubmitComment(string RequestComment, int ID)
        {
            string message;
            try
            {
               
                    int UserTypeID = int.Parse(@Request.Cookies["UserTypeCookie"].Value);
                var RequestToAddComment = (from r in db.EmailRequestRequest_TechnicalDestination where r.RequestID == ID && r.TechnicalDestinationID == UserTypeID select r).SingleOrDefault();
                RequestToAddComment.Note = RequestComment;
                RequestToAddComment.ActionTime = DateTime.Now;
                int LogTypeID = 0;
                if ((int)TechnicalDestination.Providers == UserTypeID)
                {
                    LogTypeID = (int)EmailApprovalLogTypes.ProviderReply;
                }
                else
                {
                    LogTypeID = (int)EmailApprovalLogTypes.AccountManagerReply;
                }
                string Team = TechnicalDestination.GetName(typeof(TechnicalDestinationEnums.TechnicalDestination), UserTypeID);

                var TechnicalApprovals = (from r in db.EmailRequestRequest_TechnicalDestination where r.RequestID == ID select r).ToList();
                bool AllDone = false;
                foreach (var item in TechnicalApprovals)
                {
                    if (item.ActionTime != null)
                    {
                        AllDone = true;
                    }
                    else
                    {
                        AllDone = false;
                    }
                }
                if (AllDone == true)
                {
                    foreach (var item in TechnicalApprovals)
                    {
                        item.EndTechnicalApprovalTime = DateTime.Now;
                    }
                    var OriginalRequest = (from r in db.EmailApprovalRequests where r.ID == ID select r).SingleOrDefault();
                    OriginalRequest.RequstStatusID = (int)TechnicalDestinationEnums.RequestStatus.EndTechnicalApproveFromDepartments;
                    Helpers helper = new Helpers();
                    helper.LogEmailApprovalEvent(
                    RequestToAddComment.RequestID, LogTypeID,
                    Convert.ToInt32(@Request.Cookies["UserIDCookie"].Value),
                    @Request.Cookies["UserNameCookie"].Value,
                    RequestStatus.PendingTechnicalApproveFromDepartments.ToString(),
                    RequestStatus.EndTechnicalApproveFromDepartments.ToString(), @Request.Cookies["UserNameCookie"].Value + " From " +
                    Team
                    + " Add Technical Approve Comment"+ "(" + RequestComment + ")");


                }
                else
                {
                    Helpers helper = new Helpers();
                    helper.LogEmailApprovalEvent(
                 RequestToAddComment.RequestID, (int)TechnicalDestinationEnums.EmailApprovalLogTypes.ProviderAssign,
                 Convert.ToInt32(@Request.Cookies["UserIDCookie"].Value),
                 @Request.Cookies["UserNameCookie"].Value,
                 "", "", @Request.Cookies["UserNameCookie"].Value +" From "+
                 TechnicalDestinationEnums.TechnicalDestination.GetName(typeof(TechnicalDestinationEnums.TechnicalDestination), UserTypeID)
                 + " Add  Technical Approve Comment"+"("+ RequestComment + ")");

                }

                db.SaveChanges();
                message = "success";
                return Json(message,JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                message = "";
                return Json(message,JsonRequestBehavior.AllowGet);
                throw;
            }
           
        }
        [Authorize]

        public ActionResult RequestsList(int ID)
        {
            int UserTypeID = int.Parse(@Request.Cookies["UserTypeCookie"].Value);
            string UserName = @Request.Cookies["UserNameCookie"].Value;
            List<int?> RequestIDs = new List<int?>();
            if (ID == (int)TechnicalAprrovalRequestStatus.PendingAssign)
            {
                RequestIDs = (from r in db.EmailRequestRequest_TechnicalDestination where r.AssignTime == null && r.ActionTime == null && r.TechnicalDestinationID == UserTypeID select r.RequestID).ToList();
            }
            if (ID == (int)TechnicalAprrovalRequestStatus.Assigned)
            {
                RequestIDs = (from r in db.EmailRequestRequest_TechnicalDestination where r.AssignTime != null && r.ActionTime == null && r.TechnicalDestinationID == UserTypeID && r.Assignee == UserName select r.RequestID).ToList();
            }
            if (ID == (int)TechnicalAprrovalRequestStatus.Done)
            {
                RequestIDs = (from r in db.EmailRequestRequest_TechnicalDestination where r.AssignTime != null && r.ActionTime != null && r.TechnicalDestinationID == UserTypeID && r.Assignee == UserName select r.RequestID).ToList();
            }
            ViewBag.StatusID = ID;
            var Requests = db.EmailApprovalRequests.Where(p => RequestIDs.Contains(p.ID)).ToList();
            return View(Requests);
        }

    }
}