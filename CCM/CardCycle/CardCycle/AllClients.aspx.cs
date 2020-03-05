using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class AllClients : System.Web.UI.Page
    {
        Client cl = new Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            var data = cl.GetAllClients();
            ClientsList.DataSource = data;
            ClientsList.DataBind();
        }
        protected void Refresh(object sender, EventArgs e)
        {

            Bind();

        }
        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            var data = cl.GetAllSearchByCName(PName.Text);
            ClientsList.DataSource = data;
            ClientsList.DataBind();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }
}