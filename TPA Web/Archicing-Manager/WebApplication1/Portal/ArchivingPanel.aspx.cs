using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class ArchivingPanel : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error1.Visible = false;
            div_success.Visible = false;
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddReqArchLi")).Attributes["class"] = "icon-thumbnail bg-success";
            FileUpload1.Visible = true;
        }
        protected void SubmitBtn_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (TottalProviders.Text != null && TottalExcel.Text!=null && FileUpload1.HasFile && TottalClaims.Text!=null)
                {
                    Requests md = new BLL.Requests();
                    string Fuser = Request.Cookies["nameark"].Value;
                    string added = Request.Cookies["typeark"].Value;
                    string folderpath = ClientDropDown.SelectedItem.ToString() + "/" + TypeDropDown.SelectedItem.ToString() + "/" + YearDropDown.SelectedItem.ToString() + "/" + MonthDropDown.SelectedItem.ToString();


                    Directory.CreateDirectory(Server.MapPath("~/ArchivingAttached/" + folderpath + "/"));
                    FileUpload1.SaveAs(Server.MapPath("~/ArchivingAttached/" + folderpath + "/") + FileUpload1.FileName);
                    string ArchAttachpath = "~/ArchivingAttached/" + folderpath + "/" + FileUpload1.FileName;

                    mdb.addrequestToArchPanel(ClientDropDown.SelectedItem.ToString(), MonthDropDown.SelectedItem.ToString(), YearDropDown.SelectedItem.ToString(), TottalProviders.Text, Fuser , TottalExcel.Text, TypeDropDown.SelectedItem.ToString() , ArchAttachpath , TottalClaims.Text , TottalComments.Text);
                    div_success.Visible = true;
                    clean();
                }
            }
            catch (Exception)
            {
                div_Error1.Visible = true;
            }
        }

        public void clean()
        {
            TottalProviders.Text = "";
        }
    }
}