﻿using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal
{
    class Helper
    {
        static public class Print
        {
            static public byte[] PrintFile(string Path, DataSet data,List< string> DataSetName, List<string> TableName, string ReportType)
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

                for (int i=0; i< TableName.Count; i++)
                {
                    DataTable Request = data.Tables[TableName[i]];
                    ReportDataSource rd = new ReportDataSource(DataSetName[i], Request);
                    lr.DataSources.Add(rd);
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

}
