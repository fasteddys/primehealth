using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BNC.Client.Helpers
{
   static public class Print
    {
        static public byte[] PrintFile(string Path, DataSet data,string DataSetName,string TableName,string ReportType)
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Path;
            if (System.IO.File.Exists(Path))
            {
                lr.ReportPath = Path;
            }
            else
            {
            }
            DataTable Request = data.Tables[TableName];
            ReportDataSource rd = new ReportDataSource(DataSetName, Request);

            lr.DataSources.Add(rd);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>"+ReportType+"</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;

            byte[] ResponseStream = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

            return ResponseStream;



        }


    }
}