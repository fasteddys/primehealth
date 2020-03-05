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

namespace WebApplication1.Users
{
    public partial class Portal : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error.Visible = false;
            div_Error1.Visible = false;
            div_success.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddReqLi")).Attributes["class"] = "icon-thumbnail bg-success";
                FileUpload1.Visible = false;
        }

        protected void SubmitBtn_ServerClick(object sender, EventArgs e)
        {
            try
            {
                        Requests md = new BLL.Requests();
                        string src = "";        
                        string Fuser = Request.Cookies["nameark"].Value;
                        string added = Request.Cookies["typeark"].Value;
                        int proNumber= 0 ;

                           //Clienttype , Cname , month , year , name 
                        mdb.addrequest(ClientTypeDropDownList.SelectedItem.ToString(), DropDownList5.SelectedItem.ToString(), DropDownList4.SelectedItem.ToString(), DropDownList3.SelectedItem.ToString(),DropDownList1.SelectedItem.ToString(), src, Fuser, added, Fuser);
                        //mdb.addrequest(Provider Name, Select Month, Select Year, src, Fuser, added, DataEntry Name, Client Name);
                        div_success.Visible = true;
                        clean();
            }
            catch (Exception)
            {
                div_Error1.Visible = true;
            }
        }

        public void clean()
        {
            FileUpload1.Attributes.Clear();
        }
        protected void ItemsList_ProviderDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("IDTxt") as Label;
            int id = int.Parse(lblTemp.Text);
            mdb.DeleteProvider(id);
        }


    }
}