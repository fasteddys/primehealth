using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using WS.BLL;

namespace WS.Portal
{
    public partial class Dashboard : System.Web.UI.Page
    {
        //WS.BLL.Complaint mdb = new Complaint();

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            string user = Request.Cookies["nameark"].Value;
            if (!IsPostBack)
            {
                Bind();
            }

        }

        private void Bind()
        {
            //string user = Request.Cookies["nameark"].Value;
            //var data = mdb.GetAllReqNew(user);
            //lstNewReq.DataSource = data;
            //lstNewReq.DataBind();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }
}