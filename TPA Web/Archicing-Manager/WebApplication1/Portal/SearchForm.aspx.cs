using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class SearchForm : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        string word = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                word = String.Format("{0}", Request.Form["txt_search"]);
                Bind();
            }
        }

        protected void Refresh(object sender, EventArgs e)
        {

            Bind();

        }
        public void Bind()
        {
            
            int num;
            bool isNum = int.TryParse(word, out num);
            if (isNum)  // search by ticket number
            {
                var data = mdb.GetAllSearchBID(word);
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
            else if (word!="")       // search by name from text view
            {
                var data = mdb.GetAllSearch(word);
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
           

            #region comments
            //string user = Request.Cookies["name"].Value;
            //string type = Request.Cookies["type"].Value;
            //if (type == "Account Manager")
            //{
            //    int num;
            //    bool isNum = int.TryParse(word, out num);
            //    if (isNum)
            //    {
            //        var data = mdb.GetAllByACMSearchBID(user, word);
            //        lstNewReq.DataSource = data;
            //        lstNewReq.DataBind();
            //    }
            //    else
            //    {
            //        var data = mdb.GetAllByACMSearch(user, word);
            //        lstNewReq.DataSource = data;
            //        lstNewReq.DataBind();
            //    }

            //}
            //else if (type == "Issuance" || type == "It" || type == "Underwriting" || type=="Quality Control")
            //{
            #endregion

        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            var data = mdb.GetAllSearchByMonth(ClientType.SelectedItem.ToString(),ClientName.SelectedItem.ToString(), PName.Text, MonthNum.SelectedItem.ToString(), YearNum.SelectedItem.ToString());
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }
    }
}