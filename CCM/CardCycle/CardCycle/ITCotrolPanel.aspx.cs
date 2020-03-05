using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class ITCotrolPanel : System.Web.UI.Page
    {
        Request mdb = new Request();
        protected void Page_Load(object sender, EventArgs e)
        {
            //div_success.Visible = false;
            //div_Error.Visible = false;
            if (!IsPostBack)
            {
                int x1 = mdb.New_req();
                spn_New.InnerHtml = x1.ToString();
                int x2 = mdb.total_req();
                spn_total.InnerHtml = x2.ToString();
                int x3 = mdb.close_req();
                spn_close.InnerHtml = x3.ToString();
                int x4 = mdb.pending_req();
                spn_pend.InnerHtml = x4.ToString();
            }
        }
    }
}