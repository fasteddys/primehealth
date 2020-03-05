using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;
using System.Data;
namespace WebApplication1.Portal
{
    public partial class ClientsReports : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error1.Visible = false;
            string type = Request.Cookies["typeark"].Value;
            if (type == "User")
            {
                btn_TBA_Report.Visible = false;
            }
            if (type == "TBAUser")
            {
                btn_QC_Report.Visible = false;
            }
            if (!IsPostBack)
            {
                lbel1.Text = "";      //asign time
                lbel2.Text = "";      //end asign time
                lbel3.Text = "";      //asifn print time
                lbel4.Text = "";      //end asign print time
                lbel5.Text = "";      //asign q Time
                lbel6.Text = "";
                //BindGridView();
            }

        }
        protected void btn_Search_QC_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int[] results = new int[6];
                results = mdb.QualityClientSearchReq(ClientName.SelectedItem.ToString(), MonthNum.SelectedItem.ToString(), YearNum.SelectedItem.ToString());
                lbel1.Text = results[0].ToString();      //asign time
                lbel2.Text = results[1].ToString();      //end asign time
                lbel3.Text = results[2].ToString();      //asifn print time
                lbel4.Text = results[3].ToString();      //end asign print time
                lbel5.Text = results[4].ToString();      //asign q Time
                lbel6.Text = results[5].ToString();
            }
            catch (Exception es)
            {
                div_Error1.Visible = true;
            }
        }

        protected void btn_Search_TBA_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int[] results = new int[6];
                results = mdb.TBAClienSearchReq(ClientName.SelectedItem.ToString(), MonthNum.SelectedItem.ToString(), YearNum.SelectedItem.ToString());
                lbel1.Text = results[0].ToString();      //asign time
                lbel2.Text = results[1].ToString();      //end asign time
                lbel3.Text = results[2].ToString();      //asifn print time
                lbel4.Text = results[3].ToString();      //end asign print time
                lbel5.Text = results[4].ToString();      //asign q Time
                lbel6.Text = results[5].ToString();
            }
            catch (Exception es)
            {
                div_Error1.Visible = true;
            }
        }
    }
}