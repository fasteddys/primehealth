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
    public partial class ManageRequest : System.Web.UI.Page
    {
        TPASystemDataContext db = new TPASystemDataContext();
        Requests mdb = new Requests();

        string CType = "";
        string CName = "";
        string Pname = "";
        string acManger = "";
        string src = "";
        string AssignedPerson = "";
        string Status="";
        string CAsign = "";
        string addedbytype;
        string NClaims = "";
        string DubClaims = "";
        string misspath = "";
        string foundpath = "";
        string folderpath = "";
        string DisplayAllComments = "";
        string PolicyNumber = "";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Display_All_Comments.ReadOnly = true;
                    uploaddiv1.Visible = false;
                    uploaddiv2.Visible = false;
                    NClaimdiv.Visible = false;
                    TMoneydiv.Visible = false;
                    NDubClaimdiv.Visible = false;
                    NInpClaimdiv.Visible = false;
                    NWrongClaimdiv.Visible = false;
                    NRejClaimdiv.Visible = false;
                    NRecClaimdiv.Visible = false;

                    btn_asign.Visible = false;
                    btn_issue.Visible = false;
                    btn_issue2.Visible = false;
                    btn_download.Visible = false;
                    btn_download_found.Visible = false;
                    btn_download_missing.Visible = false;
                    btn_pending_missing.Visible = false;
                    btn_Mang.Visible = false;
                    btn_Closed_Quality.Visible = false;
                    btn_AcceptAndClose.Visible = false;
                    btn_Mang_prime.Visible = false;
                    btn_Mang_PendingQC.Visible = false;
                    div_success.Visible = false;
                    div_Error.Visible = false;
                    div_QualityBackComments.Visible = false;
                    div_QualityBackComments2.Visible = false;
                    btn_BackToSearching.Visible = false;
                    btn_BackTo_Quality.Visible = false;
                    btn_DeleteRequest.Visible = false;
                    int id = int.Parse(Request.QueryString["id"]);
                    Label1.InnerText = id.ToString();
                    mdb.GetDetailByidnew(id, ref CType ,ref CName, ref Pname, ref acManger, ref src, ref AssignedPerson, ref Status, ref CAsign, ref addedbytype, ref NClaims, ref misspath, ref foundpath, ref folderpath, ref DisplayAllComments, ref PolicyNumber);
                    lbl_Sub.InnerHtml = CName;
                    ClientType.InnerHtml = CType;
                    txtrBody.InnerHtml = Pname + " " + PolicyNumber;
                    NClaimslb.InnerHtml = NClaims;
                    Display_All_Comments.Text = DisplayAllComments;
                    txtrAssPer.InnerHtml = AssignedPerson;
                    string type = Request.Cookies["typeark"].Value;
                    string user = Request.Cookies["nameark"].Value;
                    EnterComDiv.Visible = false;

        /////////////Quality privlages///////////////////////////

                    if (type == "User" || type == "Admin")
                    {
                        if (Status == "New")
                        {
                            EnterComDiv.Visible = false;
                            DisplayComDiv.Visible = false;
                        }
                        if (Status == "New" || Status == "Searching" || Status == "Pending Missing")
                        {
                            btn_DeleteRequest.Visible = true;
                        }
                        if (Status == "Pending Quality Control")
                        {
                            btn_Mang_PendingQC.Visible = true;
                            btn_download.Visible = true;
                            btn_download_found.Visible = true;
                            btn_download_missing.Visible = true;

                        }
                        if (Status == "Quality Control")
                        {
                            btn_Mang_prime.Visible = true;
                            btn_download.Visible = true;
                            btn_download_found.Visible = true;
                            btn_download_missing.Visible = true;
                            btn_BackToSearching.Visible = true;
                            btn_DeleteRequest.Visible = true;
                            EnterComDiv.Visible = true;
                            btn_Closed_Quality.Visible = true;

                        }
                       

                       
                       
                        
                    }
          /////////////TBAUser privlages///////////////////////////

                    if (type == "TBAUser" || type == "Admin")
                    {

                        if (Status == "Pending Confirmation")
                        {
                            // btn_Mang_prime.Visible = false;
                            btn_download.Visible = true;
                            btn_download_found.Visible = true;
                            btn_download_missing.Visible = true;
                            btn_Mang.Visible = true;
                        }
                        if (Status == "Accept")
                        {
                            // btn_Mang_prime.Visible = false;
                            btn_download.Visible = true;
                            btn_download_found.Visible = true;
                            btn_download_missing.Visible = true;
                            btn_AcceptAndClose.Visible = true;
                            btn_BackTo_Quality.Visible = true;
                            EnterComDiv.Visible = true;

                        }
                      
                            
                    }
         /////////////Data Entry privlages///////////////////////////

                     if (type == "DataEntry" || type == "Admin")
                    {
                        if (CAsign == "false" && Status == "New")
                        {
                            btn_asign.Visible = true;
                            btn_download.Visible = true;
                            btn_DeleteRequest.Visible = true;
                            EnterComDiv.Visible = false;
                            DisplayComDiv.Visible = false;
                        }

                        if (CAsign == "true" && Status == "Searching")
                        {
                            btn_pending_missing.Visible = true;
                            btn_issue.Visible = true;
                            btn_download.Visible = true;
                            uploaddiv2.Visible = true;
                            NDubClaimdiv.Visible = true;
                            NInpClaimdiv.Visible = true;
                            NWrongClaimdiv.Visible = true;
                            NRejClaimdiv.Visible = true;
                            NRecClaimdiv.Visible = true;
                            TMoneydiv.Visible = true;
                            btn_DeleteRequest.Visible = true;
                            EnterComDiv.Visible = true;


                        }

                        if (CAsign == "true" && Status == "Pending Missing")
                        {
                            btn_issue2.Visible = true;
                            uploaddiv1.Visible = true;
                            uploaddiv2.Visible = true;
                            NClaimdiv.Visible = true;
                            NDubClaimdiv.Visible = true;
                            NInpClaimdiv.Visible = true;
                            NWrongClaimdiv.Visible = true;
                            NRejClaimdiv.Visible = true;
                            NRecClaimdiv.Visible = true;
                            btn_BackToSearching.Visible = true;
                            TMoneydiv.Visible = true;
                            btn_DeleteRequest.Visible = true;
                            EnterComDiv.Visible = true;


                        }
                    
                    }
                }
                #region Exceptions

                if (Status == "Closed")
                {
                    btn_download.Visible = true;
                    btn_download_found.Visible = true;
                    btn_download_missing.Visible = true;
                    string type = Request.Cookies["typeark"].Value;
                    string user = Request.Cookies["nameark"].Value;
                    if (type == "User" || type == "Admin")
                    {
                        btn_BackTo_Quality.Visible = true;
                    }

                }
                if (misspath == null)
                {
                    btn_download_missing.Disabled = true;
                }
                if (foundpath == null)
                {
                    btn_download_found.Disabled = true;
                }

                #endregion
            }
            catch
            {

            }
           
        }


        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        #region Actions
                    //(Accpet btn_Assign ----> btn_asign)
        protected void btn_asign_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateAssignPerson(id, user);
            Server.Transfer("ManageRequest.aspx");
        }

                    //(Move_QC_with_found ----> btn_issue)
        protected void btn_issue_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.GetDetailByidnew(id, ref CType, ref CName, ref Pname, ref acManger, ref src, ref AssignedPerson, ref Status, ref CAsign, ref addedbytype, ref NClaims, ref misspath, ref foundpath, ref folderpath, ref DisplayAllComments, ref PolicyNumber);
            try
            {
                if (FileUpload1.HasFile && Dublicatedcounter.Text != "" && InPcounter.Text != "" && Wrongcounter.Text != "" && Moneycounter.Text != "" && Rejectedtecounter.Text != "" && Receivedcounter.Text != "")
                {
                    string user = Request.Cookies["nameark"].Value;
                    Directory.CreateDirectory(Server.MapPath("~/FoundClaims/" + folderpath + "/"));
                    FileUpload1.SaveAs(Server.MapPath("~/FoundClaims/" + folderpath + "/") + FileUpload1.FileName);
                    foundpath = "~/FoundClaims/" + folderpath + "/" + FileUpload1.FileName;
                    //string Fuser = Request.Cookies["nameark"].Value;
                    mdb.UpdateQualityControlFound(id, txtrAssPer.InnerHtml, foundcounter.Text, Dublicatedcounter.Text, InPcounter.Text, Wrongcounter.Text, misspath, foundpath, Moneycounter.Text, Enter_Your_Comments.Text, user, Rejectedtecounter.Text, Receivedcounter.Text);
                    div_success.Visible = true;
                    Server.Transfer("ManageRequest.aspx");
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

                    //(Move_QC_with_found_missing ----> btn_issue2)
        protected void btn_issue2_ServerClick(object sender, EventArgs e)
        {
            string user = Request.Cookies["nameark"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.GetDetailByidnew(id, ref CType ,ref CName, ref Pname, ref acManger, ref src, ref AssignedPerson, ref Status, ref CAsign, ref addedbytype, ref NClaims, ref misspath, ref foundpath, ref folderpath, ref DisplayAllComments, ref PolicyNumber);
            try
            {
                if (FileUpload1.HasFile && FileUpload2.HasFile && Dublicatedcounter.Text != "" && InPcounter.Text != "" && Wrongcounter.Text != "" && Moneycounter.Text != "" && Rejectedtecounter.Text != "" && Receivedcounter.Text != "")
                {
                    ///////////////////////////////////map ecxel found to path/////////////////////////////////////////
                    Directory.CreateDirectory(Server.MapPath("~/FoundClaims/" + folderpath + "/"));
                    FileUpload1.SaveAs(Server.MapPath("~/FoundClaims/" + folderpath + "/") + FileUpload1.FileName);
                    foundpath = "~/FoundClaims/" + folderpath + "/" + FileUpload1.FileName;
                    ///////////////////////////////////map ecxel miss to path/////////////////////////////////////////
                    Directory.CreateDirectory(Server.MapPath("~/MissingClaims/" + folderpath + "/"));
                    FileUpload2.SaveAs(Server.MapPath("~/MissingClaims/" + folderpath + "/") + FileUpload2.FileName);
                    misspath = "~/MissingClaims/" + folderpath + "/" + FileUpload2.FileName;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                   // string Fuser = Request.Cookies["name"].Value;
                    mdb.UpdateQualityControlFoundAndMiss(id, txtrAssPer.InnerHtml, foundcounter.Text, Dublicatedcounter.Text, InPcounter.Text, Wrongcounter.Text, misspath, foundpath, Moneycounter.Text, Enter_Your_Comments.Text, user, Rejectedtecounter.Text, Receivedcounter.Text);
                    div_success.Visible = true;
                   // Server.Transfer("ManageRequest.aspx");
                    Server.Transfer("ManageRequest.aspx");

                }
                else
                {
                    div_Error.Visible = true;
                    //mdb.UpdatePending(id, txtrAssPer.InnerHtml, foundcounter.Text, misspath, foundpath);
                    //Server.Transfer("ManageRequest.aspx");
                }
            }

            catch
            {
                div_Error.Visible = true;
            }

        }

                    //(Pending Missing ----> btn_pending_missing)
        protected void btn_pending_missing_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdatePendingMissing(id);
            Server.Transfer("ManageRequest.aspx");
        }

        protected void btn_DeleteRequest_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.deletereq(id);
            Server.Transfer("AllNewRequests.aspx");
        }

        //(BackToSearching  ----> btn_pending_missing)
        protected void btn_BackToSearching_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            if (Enter_Your_Comments.Text != "")
            {
                mdb.BackToSearching(id, user, Enter_Your_Comments.Text);
                Server.Transfer("ManageRequest.aspx");
            }
            else
            {
                div_QualityBackComments.Visible = true;
                div_QualityBackComments2.Visible = true;
            }
        }

        protected void Back_To_Quality_Control(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            if (Enter_Your_Comments.Text != "")
            {
                mdb.BackToQuality(id, user, Enter_Your_Comments.Text);
                Server.Transfer("ManageRequest.aspx");
            }
            else
            {
                div_QualityBackComments.Visible = true;
                div_QualityBackComments2.Visible = true;
            }
        }

                    //(Download Original file ----> btn_download)
        protected void DownloadFile(object sender, EventArgs e)
        {
            string user = Request.Cookies["nameark"].Value;
            Response.ContentType = ".xlsx";
            int id = int.Parse(Request.QueryString["id"]);
            string CName = "";
            string CType = "";
            string Pname = "";
            string acManger = "";
            string src = "";
            string AssignedPerson = "";
            string status = "";
            string CAsign = "";
            string addedtype = "";
            string NClaims = "";
            string misspath = "";
            string foundpath = "";
            string folderpath = "";
            mdb.GetDetailByidnew(id, ref CType, ref CName, ref Pname, ref acManger, ref src, ref AssignedPerson, ref status, ref CAsign, ref addedtype, ref NClaims, ref misspath, ref foundpath, ref folderpath, ref DisplayAllComments, ref PolicyNumber);

            string inputString = Pname+PolicyNumber;
            Regex re = new Regex(" ");
            string outputString = re.Replace(inputString, "-");

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + outputString + ".xlsx");
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + folderpath + "-" + Path.GetFileName(src) + ".xlsx");
            Response.TransmitFile(src);
            Response.End();

        }

                    //(Download found file ----> btn_download_found)
        protected void DownloadFoundFile(object sender, EventArgs e)
        {
            string user = Request.Cookies["nameark"].Value;
            Response.ContentType = ".xlsx";
            int id = int.Parse(Request.QueryString["id"]);
            string CName = "";
            string CType = "";
            string Pname = "";
            string acManger = "";
            string src = "";
            string AssignedPerson = "";
            string status = "";
            string CAsign = "";
            string addedtype = "";
            string NClaims = "";
            string misspath = "";
            string foundpath = "";
            string folderpath = "";
            mdb.GetDetailByidnew(id, ref CType , ref CName, ref Pname, ref acManger, ref src, ref AssignedPerson, ref status, ref CAsign, ref addedtype, ref NClaims, ref misspath, ref foundpath, ref folderpath, ref DisplayAllComments, ref PolicyNumber);

            string inputString = Pname;
            Regex re = new Regex(" ");
            string outputString = re.Replace(inputString, "-");

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + outputString + ".xlsx");
            Response.TransmitFile(foundpath);
            Response.End();

        }
                    
                    //(Download missing file ----> btn_download_missing)
        protected void DownloadMissingFile(object sender, EventArgs e)
        {
            string user = Request.Cookies["nameark"].Value;
            Response.ContentType = ".xlsx";
            int id = int.Parse(Request.QueryString["id"]);
            string CName = "";
            string CType = "";
            string Pname = "";
            string acManger = "";
            string src = "";
            string AssignedPerson = "";
            string status = "";
            string CAsign = "";
            string addedtype = "";
            string NClaims = "";
            string misspath = "";
            string foundpath = "";
            string folderpath = "";
            mdb.GetDetailByidnew(id, ref CType , ref CName, ref Pname, ref acManger, ref src, ref AssignedPerson, ref status, ref CAsign, ref addedtype, ref NClaims, ref misspath, ref foundpath, ref folderpath, ref DisplayAllComments, ref PolicyNumber);

            string inputString = Pname;
            Regex re = new Regex(" ");
            string outputString = re.Replace(inputString, "-");


            Response.AppendHeader("Content-Disposition", "attachment; filename=" + outputString + ".xlsx");
            Response.TransmitFile(misspath);
            Response.End();

        }
                    //(Confirmed To TBA ----> btn_Mang_prime)           
        protected void btn_PendingQC_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.UpdatePendingQC(id, user);
            Server.Transfer("ManageRequest.aspx");
        }

        protected void btn_prime_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.UpdatePrimeConfirmation(id, user);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_Quality_Closing_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.UpdateQualityClosing(id, user);
            Server.Transfer("ManageRequest.aspx");
        }

                    //(Approve Request ----> btn_Mang)
        protected void btn_Mang_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;

            mdb.UpdateAcceptArchiving(id, user);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_AcceptAndClose_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;

            mdb.UpdateClosedArchiving(id, user);
            Server.Transfer("ManageRequest.aspx");
        }

        #endregion
    }
}