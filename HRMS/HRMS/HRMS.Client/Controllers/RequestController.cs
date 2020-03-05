using HRMS.BLL.Logic.Tables;
using HRMS.DTOs;
using HRMS.DTOs.Business;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.Threading.Tasks;
using System.Configuration;

namespace HRMS.Client.Controllers
{
    public class RequestController : BaseController
    {
        // GET: Request
        [ClientAuthorization]

        public ActionResult AddNewRequest()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult ViewAllRequest()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult ViewAllApproval()
        {
            
            return View();
        }
        [ClientAuthorization]

        public ActionResult ManageRequest()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult ViewRequestsDetails()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult ViewHRReport()
        {
            return View();
        }



        //public ActionResult Report(RequestDetailsView RequestDetailsView)
        //{
        //    string type = "PDF";
        //        LocalReport lr = new LocalReport();
        //        string path = Path.Combine(Server.MapPath("~/Reports"), "Vacation.rdlc");
        //        if (System.IO.File.Exists(path))
        //        {
        //            lr.ReportPath = path;
        //        }
        //        else
        //        {
        //            return View("ManageRequest");
        //        }

        //    //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", customerBindingSource));
        //    List<RequestDetailsView> cm = new List<RequestDetailsView>();
        //    cm.Add(RequestDetailsView);
        //    ReportDataSource rd = new ReportDataSource("DataSet1", cm);



        //    lr.DataSources.Add(rd);
        //        string reportType = "PDF";
        //        string mimeType;
        //        string encoding;
        //        string fileNameExtension;
        //        string deviceInfo =

        //        "<DeviceInfo>" +
        //        "  <OutputFormat>" + type + "</OutputFormat>" +
        //        "  <PageWidth>8.5in</PageWidth>" +
        //        "  <PageHeight>11in</PageHeight>" +
        //        "  <MarginTop>0.5in</MarginTop>" +
        //        "  <MarginLeft>1in</MarginLeft>" +
        //        "  <MarginRight>1in</MarginRight>" +
        //        "  <MarginBottom>0.5in</MarginBottom>" +
        //        "</DeviceInfo>";

        //        Warning[] warnings;
        //        string[] streams;
        //        byte[] renderedBytes;

        //        renderedBytes = lr.Render(
        //            reportType,
        //            deviceInfo,
        //            out mimeType,
        //            out encoding,
        //            out fileNameExtension,
        //            out streams,
        //            out warnings);
        //        return File(renderedBytes, mimeType);


        //}
        [ClientAuthorization]

        public ActionResult Report(int ID)
        {
            RequestDetailsView RequestDetailsView = new RequestDetailsView();
            RequestDetailsView.LeaveTypeName = "My Name Is";
            string type = "PDF";
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "Vacation.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ManageRequest");
            }
            //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", customerBindingSource));
            List<RequestDetailsView> cm = new List<RequestDetailsView>();
            cm.Add(RequestDetailsView);
            ReportDataSource rd = new ReportDataSource("DataSet1", cm);



            lr.DataSources.Add(rd);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + type + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);


        }

         public ActionResult AddRequestOnBehalfOf()
        {
            return View();
        }





        //[HttpGet]
        //public ActionResult ExtractHRReportExecl(string Value /*List<ViewRequestDTO> ViewRequestList*/)
        //{
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //        DataTable dtOperator = new DataTable();
        //        dtOperator.Columns.Add("Operator Name");
        //        dtOperator.Columns.Add("Number Of Tickets");
        //        dtOperator.Columns.Add("Average Time");

        //        //foreach (var item in ViewRequestList)
        //        //{
        //        //    dtOperator.Rows.Add(item.BackToWork, item.CreationDate, item.DurationFrom);
        //        //}
        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(dtOperator, "Operators");

        //            MemoryStream MyMemoryStream = new MemoryStream();
        //            result.Content = new StreamContent(MyMemoryStream);
        //            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = "Data.xls"
        //            };
        //        }


        //    MemoryStream fs = new MemoryStream();
        //    TextWriter tx = new StreamWriter(fs);

        //    tx.WriteLine("1111");
        //    tx.WriteLine("2222");
        //    tx.WriteLine("3333");

        //    tx.Flush();
        //    fs.Flush();
        //    byte[] bytes = fs.ToArray();


        //    return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");


        //    }




        [ClientAuthorization]
        [HttpPost]
        public async Task<JsonResult> AddRequestAttachment(int RequestID)
        {
            try
            {
                List<string> DbPath = new List<string>();;
                string SavePath = ConfigurationManager.AppSettings["RequestAttachment"]+ @"\RequestsAttachments\" + RequestID + @"\";
                SavePath = SavePath.Replace(@"\", "/");

                if (!Directory.Exists(SavePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(SavePath);
                }

                for (int i = 0; i < Request.Files.Count; i++)
                {

                    var file = Request.Files[i];
                    string Path2 = SavePath + file.FileName;
                    file.SaveAs(Path2);
                    DbPath.Add("/RequestsAttachments/" + RequestID + "/" + file.FileName);

                }

                var SuccessUpload = new ResultViewModel
                {
                    Data = DbPath,
                    Message = "Files Uploaded Successfully",
                    Success = true
                };

                return Json(SuccessUpload, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                var FailedUpload = new ResultViewModel
                {
                    Data = null,
                    Message = "Sorry..error while Uploaded Files, please try again",
                    Success = false
                };

                return Json(FailedUpload, JsonRequestBehavior.AllowGet);
            }




            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(apiUrl);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = await client.GetAsync(apiUrl);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var data = await response.Content.ReadAsStringAsync();
            //        var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
            //    }
            //}           
        }

        public ActionResult DownloadFile(string FilePath)
        {
            FilePath = ConfigurationManager.AppSettings["RequestAttachment"] + FilePath;
            byte[] filedata = System.IO.File.ReadAllBytes(FilePath);
            string contentType = MimeMapping.GetMimeMapping(FilePath);
            FileInfo fi = new FileInfo(FilePath);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fi.Name,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }
        //----------------------------------------------
        // start filiter to get reject request
        public ActionResult ViewRejectionReason()
        {
            return View();
        }
        // end filiter to get reject request Report
        //----------------------------------------------

        //----------------------------------------------
        // start filiter to Pending  request Report
        public ActionResult ViewPendingRequest()
        {
            return View();
        }
        // end filiter to get reject request Report
        //----------------------------------------------
        // start filiter to Deleted  request Report
        public ActionResult ViewDeletedRequest()
        {
            return View();
        }
        // end filiter to get Deleted request Report
        //----------------------------------------------
        // start filiter to Partial Deleted  request Report
        public ActionResult ViewPartialDeletedRequest()
        {
            return View();
        }
        // end filiter to get Partial Deleted request Report
        //----------------------------------------------

      
    }
}