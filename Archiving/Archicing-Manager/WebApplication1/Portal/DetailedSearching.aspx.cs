using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class DetailedSearching : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error.Visible = false;
            string type = Request.Cookies["typeark"].Value;
      
           
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("spDetailed")).Attributes["class"] = "icon-thumbnail bg-success";
           

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownList1.SelectedValue == "" || txtsearch.Value == "")
                {
                    div_Error.Visible = true;
                    lstNewReq.DataSource = null;
                    lstNewReq.DataBind();
                }

                else if (DropDownList1.SelectedValue == "ID")
                {
                    var data = mdb.GetAllSearchBID(txtsearch.Value);
                    lstNewReq.DataSource = data;
                    lstNewReq.DataBind();
                }

                else if (DropDownList1.SelectedValue == "User Comments")
                {
                    var data = mdb.GetAllSearchBySubject(txtsearch.Value);
                    lstNewReq.DataSource = data;
                    lstNewReq.DataBind();
                }

                else if (DropDownList1.SelectedValue == "Subject")
                {
                    var data = mdb.GetAllSearchBySub(txtsearch.Value);
                    lstNewReq.DataSource = data;
                    lstNewReq.DataBind();
                }

                else if (DropDownList1.SelectedValue == "Creator")
                {
                    var data = mdb.GetAllSearchByCreator(txtsearch.Value);
                    lstNewReq.DataSource = data;
                    lstNewReq.DataBind();
                }

                else if (DropDownList1.SelectedValue == "Assigned Person")
                {
                    var data = mdb.GetAllSearchByCreator(txtsearch.Value);
                    lstNewReq.DataSource = data;
                    lstNewReq.DataBind();
                }

                else if (DropDownList1.SelectedValue == "Archiving Comments")
                {
                    var data = mdb.GetAllSearchByArchivingcomment(txtsearch.Value);
                    lstNewReq.DataSource = data;
                    lstNewReq.DataBind();
                }
                else if (DropDownList1.SelectedValue == "Attachment Name")
                {
                    var data = mdb.GetAllSearchByAttachment(txtsearch.Value);
                    lstNewReq.DataSource = data;
                    lstNewReq.DataBind();
                }

            }

            catch (Exception ex)
            {
                div_Error.Visible = true;

            }

        }
    }
}