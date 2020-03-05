using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class AdminSearch : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        string word = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                word = String.Format("{0}", Request.Form["txt_search_Admin"]);

                Bind();
            }

        }
        public void Bind()
        {
            int num;
            bool isNum = int.TryParse(word, out num);
            if (word == "")
            {

            }
            else if (isNum)
            {
                var data = mdb.GetAllSearchBID(word);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else
            {
                var data = mdb.GetAllSearch(word);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
        }
    }
}