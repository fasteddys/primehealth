using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using DispatchingMVCVer1.Models;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Net;

namespace DispatchingMVCVer1.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        DisPatchingDBEntities db = new DisPatchingDBEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report(string type, int ID)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "Report1.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Report");
            }
            List<Request> cm = new List<Request>();
            cm = db.Requests.Where(a => a.ReqID.Equals(ID)).ToList();
            ReportDataSource rd = new ReportDataSource("DispatchingDataSet", cm);
            lr.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));
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
            return File(renderedBytes,mimeType);
        }


        public ActionResult ReportMG(string type, int ID)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "ReportMG.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Report");
            }
            List<Request> cm = new List<Request>();
            cm = db.Requests.Where(a => a.ReqID.Equals(ID)).ToList();
            ReportDataSource rd = new ReportDataSource("DispatchingDataSet", cm);
            lr.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));
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

    }
}
    
