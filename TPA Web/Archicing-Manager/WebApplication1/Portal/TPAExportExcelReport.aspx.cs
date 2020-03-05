using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

namespace WebApplication1.Portal
{
    public partial class TPAExportExcelReport : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            if (!IsPostBack)
            {
                txtSearch.Text = (DateTime.Now.Date).ToString("dd/MM/yyyy");

            }

        }
        protected void Refresh(object sender, EventArgs e)
        {

            //Bind();

        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtSearch.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        }
        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ArchivingConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select folderpath as [Provider Type], rBody [Provider Name], PolicyNum [Policy Num], TottalMoney[Tottal Money] ,QualityPerson[Quality Person] ,QDate[Quality Date] from dbo.requestTB where ApprovedDate LIKE '%" + txtSearch.Text + "%'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "TpaDailyReport");
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=TpaDailyReport.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

        }

    }
}