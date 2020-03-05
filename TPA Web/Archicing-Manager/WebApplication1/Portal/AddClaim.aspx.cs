using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace WebApplication1.Portal
{
    public partial class AddBatches : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ClaimLi")).Attributes["class"] = "icon-thumbnail bg-success";
                div_Error.Visible = false;
                div_success.Visible = false;
            if(!IsPostBack)
            {
                //Bind();
            }
        }

        protected void SubmitBtn_ServerClick(object sender, EventArgs e)
        {
            string startPath = Server.MapPath(@"~/FoundClaims/" + Compressed.SelectedItem.ToString());
            string zipPath = @"C:\Users\Moustafa.Mahmoud\Desktop\te.zip";
            ZipFile.CreateFromDirectory(startPath, zipPath);
           
            
            
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + folderpath + "-" + Path.GetFileName(foundpath));
            ////Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(src));
            //Response.TransmitFile(foundpath);
            //Response.End();

            // Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Comoressed.SelectedItem.ToString()));

            //string startPath = @"C:\Users\Moustafa.Mahmoud\Desktop\files shared\september 2010";
            //"~/FoundClaims/" + folderpath + "/" + FileUpload1.FileName;
            //string extractPath = @"c:\example\extract";


            //ZipFile.ExtractToDirectory(zipPath, extractPath);

            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + folderpath + "-" + Path.GetFileName(foundpath));
            ////Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(src));
            //Response.TransmitFile(foundpath);
            //Response.End();
            
            
            
            
            
            
            
            //string name = Request.Cookies["nameark"].Value;
            //bool add = mdb.AddClaim(ClaimNumTxt.Value, BatchDLL.SelectedValue, name);
            //if (add)
            //{
            //    mdb.NumOFClaimIncrement(BatchDLL.SelectedValue);
            //    clear();
            //    Bind();
            //    div_success.Visible = true;
            //}
            //else
            //{
            //    div_Error.Visible = true;
            //}
        }
        //public void clear()
        //{
        //    //ClaimNumTxt.Value = "";
        //    //BatchDLL.SelectedValue = null;
        //}
        //public void Bind()
        //{
        //    //var data = mdb.GetAllClaims();
        //    //ItemsList.DataSource = data;
        //    //ItemsList.DataBind();
        //}
    }
}