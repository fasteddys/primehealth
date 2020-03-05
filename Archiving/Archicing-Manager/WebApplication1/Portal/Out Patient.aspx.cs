using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;
namespace WebApplication1.Portal
{
    public partial class OutPaitient : System.Web.UI.Page
    {
        outpatient mdb = new outpatient();

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            if (type == "User" || type == "Remb")
            {

                Response.Redirect("/Login.aspx");
            }

            else
            {
                div_Error.Visible = false;
                div_success.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span1")).Attributes["class"] = "icon-thumbnail bg-success";
                if (!IsPostBack)
                {
                    //bind();
                }

                if (type == "EnterpriseAdmin")
                {
                    addout.Visible = false;
                }

                if (type == "Archiving")
                {
                    divsearch.Visible = false;
                }
                if (type == "ArcAdmin")
                {
                    divsearch.Visible = true;
                }

            }
        }
        protected void AddBatchBtn_ServerClick(object sender, EventArgs e)
        {
            if (Text1.Value == "" || Text2.Value == "")
            {
                div_Error.Visible = true;
            }
            else
            {
                string name = Request.Cookies["nameark"].Value;
                bool add = mdb.addRecord(BatchNumTxt.Value, BoxNumTxt.Value, Text1.Value, Text2.Value, name);
                if (add)
                {
                    clear();
                    //bind();
                    div_success.Visible = true;
                }
                else
                {
                    div_Error.Visible = true;
                }
            }
        }
        public void clear()
        {
            BatchNumTxt.Value = "";
            BoxNumTxt.Value = "";
            Text1.Value = "";
            Text2.Value = "";

        }
        //public void bind()
        //{
        //    var data = mdb.GetAllTransactions();
        //    lstNewReq.DataSource = data;
        //    lstNewReq.DataBind();
        //}

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    div_Error.Visible = false;
                    string path = string.Concat(Server.MapPath("/outpatientexcels/" + FileUpload1.FileName));
                    FileUpload1.SaveAs(path);

                    // Connection String to Excel Workbook
                    string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    OleDbConnection connection = new OleDbConnection();
                    connection.ConnectionString = excelConnectionString;
                    OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
                    connection.Open();
                    // Create DbDataReader to Data Worksheet
                    DbDataReader dr = command.ExecuteReader();

                    // SQL Server Connection String
                    string sqlConnectionString = "Data Source=ANDROIDDB;Initial Catalog=Archiving;Integrated Security=True";

                    // Bulk Copy to SQL Server 
                    SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
                    bulkInsert.DestinationTableName = "[Archiving].[dbo].[Outpatient]";
                    bulkInsert.WriteToServer(dr);
                    div_success.Visible = true;
                }
                catch (Exception ex)
                {
                    div_Error.Visible = true;
                }

            }
            else
            {
                div_Error.Visible = true;
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            outpatient outt = new outpatient();
            outt.GetAllTransactions();

        }
    }
}

