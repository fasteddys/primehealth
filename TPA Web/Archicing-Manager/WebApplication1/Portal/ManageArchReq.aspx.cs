using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;
using WebApplication1.DLL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.Portal
{
    public partial class ManageArchReq : System.Web.UI.Page
    {
        TPASystemDataContext db = new TPASystemDataContext();
        Requests mdb = new Requests();
        string CName = "";
        string Month = "";
        string Year = "";
        string status = "";
        string Creator = "";
        string TottalProviders = "";
        string TottalComm = "";
        string SerType = "";
        string PathFile = "";
        string TotExcel = "";
        string TotClaims = "";
        string Reciver = "";
        string Assign = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ////////////// Buttons Visibility ///////////////////////////////
                    TottalComments.ReadOnly = true;
                    btn_AcceptForReview.Visible = false;
                    btn_GoToPending.Visible = false;
                    btn_Closed.Visible = false;
                    NewTottalProviders.Visible = false;
                    NewTottalExcel.Visible = false;
                    NewTottalClaims.Visible = false;
                    btn_UpdateTotArch.Visible = false;
                    btn_BackToArch.Visible = false;

                    ////////////// Loading Data Visibility ///////////////////////////////

                    int id = int.Parse(Request.QueryString["id"]);
                    IDTicket.InnerText = id.ToString();
                    mdb.GetDetailByidArchiving(id, ref CName, ref Month, ref Year, ref status, ref Creator, ref TottalProviders, ref SerType, ref PathFile, ref TotExcel, ref Reciver, ref Assign, ref TotClaims, ref TottalComm);
                    ClientName_lbl.InnerHtml = CName;
                    Month_lbl.InnerHtml = Month;
                    Year_lbl.InnerHtml = Year;
                    CreatorName_lbl.InnerHtml = Creator;
                    TottalProviders_lbl.InnerHtml = TottalProviders;
                    TottalExcel_lbl.InnerHtml = TotExcel;
                    TottalClaims_lbl.InnerHtml = TotClaims;
                    TottalComments.Text = TottalComm;

                    /////////////////////////////////////////////////////////////////////////////////
                    string type = Request.Cookies["typeark"].Value;
                    string user = Request.Cookies["nameark"].Value;
                    /////////////DataEntry privlages///////////////////////////
                    if (type == "Admin" || type == "DataEntry")
                    {
                        if (status == "NewArch")
                        {
                            btn_AcceptForReview.Visible = true;
                        }

                        if (status == "ReviewArch" && Reciver == user)
                        {
                            btn_GoToPending.Visible = true;
                            btn_Closed.Visible = true;
                        }

                    }
                    ///////////// Archiving privlages///////////////////////////
                    if (type == "Admin" || type == "ArchAdmin")
                    {
                        if (status == "PendingArch" && Creator == user)
                        {
                            btn_BackToArch.Visible = true;
                            btn_UpdateTotArch.Visible = true;
                        }
                    }
                    if (status == "ClosedArch" && user == "Mohamed.Abdelsattar")
                    {
                        btn_GoToPending.Visible = true;
                    }
                }
            }
            catch
            {
            
}
            }
              
        
        #region New Arch Functions

        protected void btn_AcceptForReview_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateReviewPerson(id, user);
            Server.Transfer("ManageArchReq.aspx");
        } // btn_accept 
        protected void btn_GoToPending_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updatePendingTicket(id, user, EntranceArch_Comments.Text);
            Server.Transfer("ManageArchReq.aspx");
        }   // btn goto pending
        protected void btn_Closed_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateClosedPerson(id, user);
            Server.Transfer("ManageArchReq.aspx");
        }
        protected void btn_BackToArch_ServerClick(object sender, EventArgs e)
        {

            btn_UpdateTotArch.Visible = false;
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateBackToNew(id, user, NewTottalProviders_lbl.Text, NewTottalExcel_lbl.Text, NewTottalClaims_lbl.Text, EntranceArch_Comments.Text);
            Server.Transfer("ManageArchReq.aspx");
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string user = Request.Cookies["nameark"].Value;
            Response.ContentType = ".rar";
            int id = int.Parse(Request.QueryString["id"]);
            string CName = "";
            string Month = "";
            string Year = "";
            string status = "";
            string Creator = "";
            string TottalProviders = "";
            string ArchivingComments = "";
            string DataEntryarchComments = "";
            string SerType = "";
            string PathFile = "";
            string TotExcel = "";
            string Reciver = "";
            string Assign = "";
            string TotClaims = "";
            string TottalComm = "";

            mdb.GetDetailByidArchiving(id, ref CName, ref Month, ref Year, ref status, ref Creator, ref TottalProviders, ref SerType, ref PathFile, ref TotExcel, ref Reciver, ref Assign, ref TotClaims, ref TottalComm);


            string inputString = CName + " " + SerType + " " + Month + " " + Year;
            Regex re = new Regex(" ");
            string outputString = re.Replace(inputString, "-");

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + outputString + ".rar");
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + folderpath + "-" + Path.GetFileName(src) + ".xlsx");
            Response.TransmitFile(PathFile);
            Response.End();

        }
        protected void btn_UpdateTottalProviders_ServerClick(object sender, EventArgs e)
        {
            NewTottalProviders.Visible = true;
            NewTottalExcel.Visible = true;
            NewTottalClaims.Visible = true;
        }

        #endregion





        
    }
}