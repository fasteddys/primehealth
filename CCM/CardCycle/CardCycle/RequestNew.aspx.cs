using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class RequestNew : System.Web.UI.Page
    {
        Request mdb = new Request();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Bind();
            }

        }
        protected void Refresh(object sender, EventArgs e)
        {

            Bind();

        }
        public void Bind()
        {
            var data = mdb.GetAllReqNewNotAssigned();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();
        }
        // call data every 10000 mss
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }
}