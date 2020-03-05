using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;


namespace WebApplication1.Portal
{
    public partial class SearchClaimNum : System.Web.UI.Page
    {

        outpatient mdb = new outpatient();
        trans mddb = new trans();
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            if (type == "User" || type == "Remb")
            {
                Response.Redirect("/Login.aspx");
            }

            else
            {
                div_Error.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span5")).Attributes["class"] = "icon-thumbnail bg-success";
                divsuccess.Visible = false;
                div_Error.Visible = false;
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownList1.SelectedValue == "" || DropDownList2.SelectedValue == "" || txtsearch.Value == "")
                {
                    div_Error.Visible = true;
                }
                else if (DropDownList1.SelectedValue == "OutPatient" && DropDownList2.SelectedValue == "Claim Code OR Pre_AuthID")
                {
                    string x = mdb.numofclaimsbybatchid(txtsearch.Value);
                    sp1.InnerHtml = x;
                    divsuccess.Visible = true;


                }
                else if (DropDownList1.SelectedValue == "OutPatient" && DropDownList2.SelectedValue == "Set ID")
                {
                    string x = mdb.numofSetIDbybatchid(txtsearch.Value);
                    sp1.InnerHtml = x;
                    divsuccess.Visible = true;


                }
                else if (DropDownList1.SelectedValue == "InPatient" && DropDownList2.SelectedValue == "Claim Code OR Pre_AuthID")
                {
                    string x = mddb.numofSetIDbybatchid(txtsearch.Value);
                    sp1.InnerHtml = x;
                    divsuccess.Visible = true;

                }
                else if (DropDownList1.SelectedValue == "InPatient" && DropDownList2.SelectedValue == "Set ID")
                {
                    string x = mddb.numofSetIDbybatchid(txtsearch.Value);
                    sp1.InnerHtml = x;
                    divsuccess.Visible = true;

                }

                else
                {
                    div_Error.Visible = true;
                }

            }

            catch (Exception ex)
            {
                div_Error.Visible = true;

            }

        }

    }
}