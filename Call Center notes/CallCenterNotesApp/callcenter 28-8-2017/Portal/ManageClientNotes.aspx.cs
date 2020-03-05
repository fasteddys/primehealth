using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.CustomExceptions;
using CallCenterNotesApp.DLL;


namespace CallCenterNotesApp.Portal
{
    public partial class ManageClientNotes : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        ClientNotesClass mdb = new ClientNotesClass();
        Helpers Helper = new Helpers();
        static List<int> DeletedList = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {

            string Creatorstr = "";
            string CreationTimestr = "";
            string ClientNamestr = "";
            string Notesstr = "";

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
               // IDLable.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();
                string Uname = Request.Cookies["UserName"].Value.Trim();

                mdb.GetDetailByid(id, ref CreationTimestr, ref Creatorstr, ref ClientNamestr, ref Notesstr);

                ClientNotesCreator.InnerText = Creatorstr;
                CreationTime.InnerText = CreationTimestr;
                ClientName.Value = ClientNamestr;
                Notes.InnerText = Notesstr;

                btn_DeleteClientNotes.Visible = false;
                btn_UpdateClientNotes.Visible = false;
                ListViewManager.Visible = false;
                FileUpload1.Visible = false;
                ListViewNormal.Visible = true;
                /////////////////////////////////////////////////////////
                if (type == "CallCenterManager")
                {
                    btn_DeleteClientNotes.Visible = true;
                    btn_UpdateClientNotes.Visible = true;
                    ListViewManager.Visible = true;
                    FileUpload1.Visible = true;
                    ListViewNormal.Visible = false;
                }
                ListViewManager.DataSource = Helper.GetAllAttachmentByNoteId(id);
                ListViewManager.DataBind();
                ListViewNormal.DataSource = Helper.GetAllAttachmentByNoteId(id);
                ListViewNormal.DataBind();
            }


        }
        //protected void ListViewBound(object sender, ListViewItemEventArgs e)
        //{
        //    string type = Request.Cookies["UserType"].Value.Trim();

        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        if (type == "CallCenterManager")
        //        {
        //            HtmlGenericControl hyperlink = (HtmlGenericControl)FindControl("DeleteButton");
        //            hyperlink.Visible = true;
        //        }
        //        else
        //        {
        //            HtmlGenericControl hyperlink = (HtmlGenericControl)FindControl("DeleteButton");
        //            hyperlink.Visible = false;
        //        }
                    
        //    }
        //}
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                EmailApprovalsConfiguration Configuration = new EmailApprovalsConfiguration();

                using (PhNetworkEntities Db = new PhNetworkEntities())
                {
                    Configuration = Db.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "SaveTicketAttachmentPath").FirstOrDefault();
                }

                LinkButton clickedButton = sender as LinkButton;
                var FileName = clickedButton.Attributes["FileName"].ToString();
                var FilePath = clickedButton.Attributes["Path"].ToString();

                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName.Replace(" ", string.Empty));
                Response.TransmitFile(Configuration.ConfigurationValue + FilePath);
                Response.Flush();
                Response.End();



            }

            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "CcEmailRequestManageRequest.aspx.cs", "LinkButton2_Click", "CallCenterSystemApp");

            }
        }
        protected void DeleteAttachment_Click(object sender, EventArgs e)
        {
            string IsDeleted;
            HtmlButton button = (HtmlButton)sender;
            IsDeleted = button.Attributes["isDeleted"].ToString();
            if (button.Attributes["isDeleted"] == "False")
            {
                string x = button.Attributes["AttachmentID"].ToString();
                DeletedList.Add(Convert.ToInt32(button.Attributes["AttachmentID"]));
                button.Attributes["isDeleted"] = "True";
                button.InnerHtml = "<span class='btn btn-success fa fa-undo'>";

            }
            else
            {

                DeletedList.Remove(Convert.ToInt32(button.Attributes["AttachmentID"]));
                button.Attributes["isDeleted"] = "False";
                //button.Value = "Button";
                button.InnerHtml = "<span class='btn btn-danger fa fa-times'>";

            }

        }
        protected void DeleteClientNotes_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteClient(id);
            Response.Redirect("~/Portal/ViewNotes.aspx");
        }
        protected void UpdateClientNotes_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateClientNotes(id, ClientName.Value, Notes.Value, FileUpload1.PostedFile, FileUpload1);
            Helper.DeleteNoteAttach(DeletedList);
            div_Success_update.Visible = true;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}