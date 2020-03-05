using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.ModelBinding;
using System.ComponentModel;
using Excel = Microsoft.Office.Interop.Excel;
using WebApplication1.BLL;
using WebApplication1.DLL;
namespace WebApplication1.Portal
{
    public partial class ExcelSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            string type = Request.Cookies["typeark"].Value;
            if (type == "Admin" || type == "EnterpriseAdmin")
            {
                ErrorDiv.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span6")).Attributes["class"] = "icon-thumbnail bg-success";
                ErrorDiv.Visible = false;
                if (!IsPostBack)
                {
                    ErrorDiv.Visible = false;

                }
            }

            else
            {
                Response.Redirect("/Login.aspx");
            }

        }
        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;
        outpatient outt = new outpatient();
        trans ts = new trans();
        outpatient ot = new outpatient();
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    ErrorDiv.Visible = false;
                    string path = string.Concat(Server.MapPath("/searchexcel/" + FileUpload1.FileName));
                    FileUpload1.SaveAs(path);
                    MyApp = new Excel.Application();
                    MyApp.Visible = false;
                    string ConStr = "";
                    string ext = Path.GetExtension(path).ToLower();
                    //checking that extantion is .xls or .xlsx  
                    if (ext.Trim() == ".xls")
                    {
                        //connection string for that file which extantion is .xls  
                        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (ext.Trim() == ".xlsx")
                    {
                        //connection string for that file which extantion is .xlsx  
                        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }

                    OleDbConnection conn = new OleDbConnection(ConStr);
                    MyBook = MyApp.Workbooks.Open(path);
                    MySheet = (Excel.Worksheet)MyBook.Sheets[1];
                    var lastCell = MySheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
                    List<string> claimcode = new List<string>();
                    List<string> setID = new List<string>();

                    for (int index = 1; index <= lastCell; index++)
                    {
                        string MyValues = MySheet.get_Range("A" + index.ToString()).Cells.Text;

                        claimcode.Add(MyValues);

                        if (MyValues == "")
                        {
                            claimcode.Remove(MyValues);


                        }
                    }

                    for (int index = 1; index <= lastCell; index++)
                    {
                        string MyValuesss = MySheet.get_Range("B" + index.ToString()).Cells.Text;

                        setID.Add(MyValuesss);
                        if (MyValuesss == "")
                        {
                            setID.Remove(MyValuesss);


                        }
                    }


                    List<Transaction> tr = new List<Transaction>();
                    List<Transaction> trr = new List<Transaction>();
                    List<Outpatient> outt = new List<Outpatient>();
                    List<Outpatient> outtt = new List<Outpatient>();


                    if (DropDownList1.SelectedValue == "OutPatient")
                    {
                        outtt = ot.GetTransactionByClaimCode(claimcode);
                        outt.AddRange(outtt);
                        outtt = ot.GetTransactionBSetID(setID);
                        outt.AddRange(outtt);
                        GridView1.DataSource = outt;
                        GridView1.DataBind();

                    }

                    else if (DropDownList1.SelectedValue == "InPatient")
                    {

                        trr = ts.GetTransactionByClaimCode(claimcode);
                        tr.AddRange(trr);
                        trr = ts.GetTransactionBSetIDList(setID);
                        tr.AddRange(trr);
                        GridView1.DataSource = tr;
                        GridView1.DataBind();

                    }

                    GridView1.Visible = true;
                    Response.AddHeader("content-disposition", "attachment; filename=" + Path.GetFileNameWithoutExtension(path) + ".xls");
                    Response.ContentType = "application/excel";
                    StringWriter sWriter = new StringWriter();
                    HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
                    GridView1.RenderControl(hTextWriter);
                    Response.Write(sWriter.ToString());
                    MyApp.Quit();
                    Response.TransmitFile(path);
                    Response.End();
                }
                else
                {
                    ErrorDiv.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}