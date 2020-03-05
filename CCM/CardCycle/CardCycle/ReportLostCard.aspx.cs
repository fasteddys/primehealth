using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class ReportLostCard : System.Web.UI.Page
    {
        Request mdb = new Request();
        protected void Page_Load(object sender, EventArgs e)
        {
            var data= mdb.GetAllRLostcard();
            int total = mdb.total_LostCard();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();
            Label1.Text = total.ToString();
        }
    }
}