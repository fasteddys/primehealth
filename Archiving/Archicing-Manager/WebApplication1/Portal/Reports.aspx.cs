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
    public partial class Reports : System.Web.UI.Page
    {
        Requests mdd = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView2.Visible = false;
            div_Error.Visible = false;
            if (!IsPostBack)
            {
                Accounts mdb = new Accounts();

                  var usersList = mdb.GetAllUsers().Select(u => new ListItem
                 {
                        Text = u.UserName,
                       Value = u.id.ToString()
                 }).ToList();
                   drp1.Items.Add("----");
                   drp1.Items.Add("All Users");
                   foreach (var item in usersList)
                   {
                    drp1.Items.Add(item);
                   }

            

            }
            string type = Request.Cookies["typeark"].Value;
            if (type=="EnterpriseAdmin")
            {

                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span7")).Attributes["class"] = "icon-thumbnail bg-success";


            }

            else
            {
                Response.Redirect("/Login.aspx");
            }
        }



        protected void GetReport(object sender, EventArgs e)
        {
            try
            {
                string start = Calendar1.SelectedDate.Date.ToString("yyyy/MM/dd");
                string end = Calendar2.SelectedDate.Date.ToString("yyyy/MM/dd");
                string user = drp1.SelectedItem.Text;
                if (user == "----")
                {
                    div_Error.Visible = true;

                }
                if (user == "All Users")
                {
                    List<requestTB> data = mdd.GetAllIntervalReport(start, end);
                    ItemsList.DataSource = data;
                    Label1.Text =data.Count.ToString();
                    try
                    {
                        Label2.Text = mdd.SumRequestedItemsForAllUsers(start, end).ToString();
                        Label3.Text = mdd.SumFoundItemsForAllUsers(start, end).ToString();
                    }

                    catch
                    {
                        Label2.Text = "0";
                        Label3.Text = "0";
                    }
                    ItemsList.DataBind();
                }
                else
                {
                    List<requestTB> data = mdd.GetIntervalReport(start, end, user);
                    ItemsList.DataSource = data;
                    Label1.Text = data.Count.ToString();
                    try
                    {
                        Label2.Text = mdd.SumRequestedItems(user, start, end).ToString();
                        Label3.Text = mdd.SumFoundItems(user, start, end).ToString();
                    }

                    catch
                    {
                        Label2.Text = "0";
                        Label3.Text = "0";
                    }
                    ItemsList.DataBind();
                }

              

            }

            catch
            {
                div_Error.Visible = true;

            }
            
         }


        protected void Export(object sender, EventArgs e)
        {
            try
            {
                string start = Calendar1.SelectedDate.Date.ToString("yyyy/MM/dd");
                string end = Calendar2.SelectedDate.Date.ToString("yyyy/MM/dd");
                string user = drp1.SelectedItem.Text;
                if (user == "----")
                {
                    div_Error.Visible = true;

                }
                if (user == "All Users")
                {
                    List<requestTB> data = mdd.GetAllIntervalReport(start, end);
                    GridView2.DataSource = data;
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
                    Response.ContentType = "application/excel";
                    StringWriter sWriter = new StringWriter();
                    HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
                    GridView2.RenderControl(hTextWriter);
                    Response.Write(sWriter.ToString());
                    Response.End();
                }
                else
                {
                    List<requestTB> data = mdd.GetIntervalReport(start, end, user);
                    GridView2.DataSource = data;
                    GridView2.DataBind();
                    GridView2.Visible = true;
                    Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
                    Response.ContentType = "application/excel";
                    StringWriter sWriter = new StringWriter();
                    HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
                    GridView2.RenderControl(hTextWriter);
                    Response.Write(sWriter.ToString());
                    Response.End();
                }
            }

            catch
            {
                div_Error.Visible = true;
            }
        }


        protected void GetdailyReport(object sender, EventArgs e)
        {
            try
            {
                List<requestTB> data = mdd.GetDailyReport();
                ItemsList.DataSource = data;
                Label1.Text = data.Count.ToString();
                try
                {
                    Label2.Text = mdd.SumRequestedItemsForDailyReport().ToString();
                    Label3.Text = mdd.SumFoundItemsForDailyReport().ToString();
                }
                catch
                {
                    Label2.Text = "0";
                    Label3.Text = "0";

                }
                ItemsList.DataBind();

            }

            catch
            {
                div_Error.Visible = true;
            }
        }


        protected void ExportToday(object sender, EventArgs e)
        {
            try
            {
                GridView2.DataSource = mdd.GetDailyReport();
                GridView2.DataBind();
                GridView2.Visible = true;
                Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
                Response.ContentType = "application/excel";
                StringWriter sWriter = new StringWriter();
                HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
                GridView2.RenderControl(hTextWriter);
                Response.Write(sWriter.ToString());
                Response.End();

            }

            catch(Exception ex)
            {
                div_Error.Visible = true;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
           
        }

      
    }
}