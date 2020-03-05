using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class SearchForm : System.Web.UI.Page
    {
        Request mdb = new Request();
        string word = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //word = Request.QueryString["word"];
            
            if (!IsPostBack)
            {
                word = String.Format("{0}", Request.Form["txt_search"]);

                Bind();
            }

        }

        public void Bind()
        {

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
                 int num;
                 bool isNum = int.TryParse(word, out num);
                 if (isNum)
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
               
            //}
            //else if ()
            //{
            //    var data = mdb.GetAll();
            //    lstNewReq.DataSource = data;
            //    lstNewReq.DataBind();
            //}

        }
        // call data every 10000 mss
        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    Bind();
        //}
    }
}