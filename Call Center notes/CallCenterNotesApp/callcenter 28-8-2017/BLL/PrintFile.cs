using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.BLL
{
    public static class PrintFile
    {
        static public byte[] Print(string Path, DataSet data, List<string> DataSetName, List<string> TableName, string ReportType)
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
            List<DataTable> Request = new List<DataTable>();//data.Tables[TableName];
            foreach(var items in TableName)
            {
                Request.Add(data.Tables[items]);
            }

            List<ReportDataSource> rd = new List<ReportDataSource>();//(DataSetName, Request);
            for(int i = 0; i < DataSetName.Count(); i++)
            {
                rd.Add(new ReportDataSource(DataSetName[i], Request[i]));
            }

            foreach (var items in rd)
            {
                lr.DataSources.Add(items);
            }
            
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + ReportType + "</OutputFormat>" +
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