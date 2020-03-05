using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class BatchManager : System.Web.UI.Page
    {
        Batches mdb = new Batches();

        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error.Visible = false;
            div_success.Visible = false;
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddBaLi")).Attributes["class"] = "icon-thumbnail bg-success";
            if(!IsPostBack)
            {
                bind();
            }
        }

        protected void AddBatchBtn_ServerClick(object sender, EventArgs e)
        {
            if (BatchNumTxt.Value == "" || BoxNumTxt.Value == "" )
            {
                div_Error.Visible = true;
            }
            else
            {
                string name = Request.Cookies["nameark"].Value;
                bool add = mdb.addRecord(BatchNumTxt.Value, BoxNumTxt.Value, name);
                if (add)
                {
                    clear();
                    bind();
                    div_success.Visible = true;
                }
                else
                {
                    div_Error.Visible = true;
                }
            }
        }
        public void clear()
        {
            BatchNumTxt.Value = "";
            BoxNumTxt.Value = "";

        }
        public void bind()
        {
            var data = mdb.GetAllBatches();
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }
    }
}