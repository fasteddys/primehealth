using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class ClosingReqestes : System.Web.UI.Page
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
            string user = Request.Cookies["name"].Value;
            string type = Request.Cookies["type"].Value;
            if (type == "Account Manager")
            {
                var data = mdb.GetAllReqClosingBYACM(user);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "Issuance" || type == "It" || type == "QualityControl")
            {
                var data = mdb.GetAllReqClosing();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
          
        }
        // call data every 20 scond
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }
}