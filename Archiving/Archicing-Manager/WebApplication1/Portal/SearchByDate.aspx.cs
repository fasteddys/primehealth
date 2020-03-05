using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class SearchByDate : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        string word = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            if (type == "EnterpriseAdmin")
            {
                if (!IsPostBack)
                {
                    word = String.Format("{0}", Request.Form["txt_search_date"]);

                    Bind();
                }
            }

            else
            {
                Response.Redirect("/Login.aspx");
            }



        }
        public void Bind()
        {
            DateTime num;
            bool isNum = DateTime.TryParse(word, out num);
            if (word == "")
            {

            }
            else if (isNum)
            {

                var data = mdb.GetAllSearchBDate(word);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else
            {

            }
        }
    }
}