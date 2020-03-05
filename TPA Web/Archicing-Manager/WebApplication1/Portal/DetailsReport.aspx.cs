using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;
using WebApplication1.DLL;

namespace WebApplication1.Portal
{
    public partial class DetailsReport : System.Web.UI.Page
    {
        Requests mdb = new Requests();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.Cookies["typeark"].Value;
                if (type == "User")
                {
                    btn_ClientReport_TBA.Visible = false;
                }
                if (type == "TBAUser")
                {
                    btn_ClientReport_QC.Visible = false;
                }
            }
        }

        protected void btn_ClientReport_QC_ServerClick(object sender, EventArgs e)
        {
            var data = mdb.GetClientsReportyMonth_QC(ClientName.SelectedItem.ToString(), MonthNum.SelectedItem.ToString(), YearNum.SelectedItem.ToString());
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }

        protected void btn_ClientReport_TPA_ServerClick(object sender, EventArgs e)
        {
            var data = mdb.GetClientsReportyMonth_TBA(ClientName.SelectedItem.ToString(), MonthNum.SelectedItem.ToString(), YearNum.SelectedItem.ToString());
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }

        protected void btn_ClientReport_Report_ServerClick(object sender, EventArgs e)
        {
            List<requestTB> tr = new List<requestTB>();
            List<requestTB> trr = new List<requestTB>();

            trr = mdb.GetClientsReportyMonth_TBA(ClientName.SelectedItem.ToString(), MonthNum.SelectedItem.ToString(), YearNum.SelectedItem.ToString());
            tr.AddRange(trr);
            GridView1.DataSource = tr;

            //GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment; filename=" + "teeeest"+ ".xls");
            Response.ContentType = "application/excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            //tr.f(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
            GridView1.Visible = false;

        }

    }
}