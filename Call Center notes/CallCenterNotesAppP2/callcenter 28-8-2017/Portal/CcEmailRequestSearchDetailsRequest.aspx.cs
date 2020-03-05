using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class CcEmailRequestSearchDetailsRequest : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();
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

            int num;
            bool isNum = int.TryParse(word, out num);
            if (isNum)
            {
                var data = mdb.GetAllSearchBID(word);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            //else if (!isNum && word!="")
            //{
            //    var data = mdb.GetAllSearch(word);
            //    lstNewReq.DataSource = data;
            //    lstNewReq.DataBind();
            //}

        }
    }
}