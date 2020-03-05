using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenterNotesApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckBoxList1.Visible = false;
                CheckBoxList1.DataSource = Directory.GetFiles(Server.MapPath("~/AttachedCallcenterEmailReq/11"));
                CheckBoxList1.DataBind();
            }
            else
            {
                var files = CheckBoxList1.Items.Cast<ListItem>().Select(li => li.Value).ToList();
                var archive = Server.MapPath("~/archive.zip");
                var temp = Server.MapPath("~/temp");

                // clear any existing archive
                if (System.IO.File.Exists(archive))
                {
                    System.IO.File.Delete(archive);
                }
                // empty the temp folder
                Directory.EnumerateFiles(temp).ToList().ForEach(f => System.IO.File.Delete(f));

                // copy the selected files to the temp folder
                files.ForEach(f => System.IO.File.Copy(f, Path.Combine(temp, Path.GetFileName(f))));

                // create a new archive
                ZipFile.CreateFromDirectory(temp, archive);
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename="+"sasa"+".zip");
                Response.TransmitFile(archive);
            }

        }

        protected void CheckBoxList1_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in CheckBoxList1.Items)
            {
                item.Text = Path.GetFileName(item.Text);
            }
        }
    }
}