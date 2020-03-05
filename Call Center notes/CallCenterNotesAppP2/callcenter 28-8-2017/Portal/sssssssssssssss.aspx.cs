using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class sssssssssssssss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadMultipleFiles(object sender, EventArgs e)
        {
            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);

                //Directory.CreateDirectory(Server.MapPath("~/Attached/" + pathID + "/"));
                //FileUpload1.SaveAs(Server.MapPath("~/Attached/" + pathID + "/") + FileUpload1.FileName);

                Directory.CreateDirectory(Server.MapPath("~/AttachedCallcenterEmailReq/" + "123" + "/"));
                postedFile.SaveAs(Server.MapPath("~/AttachedCallcenterEmailReq/" + "123" + "/") + fileName);
            }
            lblSuccess.Text = string.Format("{0} files have been uploaded successfully.", FileUpload1.PostedFiles.Count);
        }

    }
}