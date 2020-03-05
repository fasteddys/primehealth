using System;
using System.IO;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Configuration;
using Microsoft.Exchange.WebServices.Data;

namespace CallCenterNotesApp.Portal
{
    public partial class ManageClaimsApprove : System.Web.UI.Page
    {

        string RequestedUsernamestr = "", CreationTimestr = "", MedicalIDstr = "", Reqtypestr = "", RequestTitlestr = "", base64String = "";
        string RequestSubjectstr = "", UserImagepathstr = "", UserImage2pathstr = "", UserImage3pathstr = "", UserImage4pathstr = "", UserImage5pathstr = "";
        string CallCenterCommentstr = "", CallCenterImagepathstr = "", StatusStr = "", CallCenterImageRealPath = "", CallCenterCommentPersonstr = "", CallCenterCommentDatestr = "";
        int id;
        string User_Name;
        string ClientUsername = "", ClientEmail = "", ClientType = "";

        PhNetworkEntities db = new PhNetworkEntities();
        ClaimsApprovalClass mdb = new ClaimsApprovalClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string RequestedUsernamestr = "";
            string CreationTimestr = "";
            string MedicalIDstr = "";
            string Reqtypestr = "";
            string RequestTitlestr = "";
            string RequestSubjectstr = "";
            string UserImagepathstr = "";
            string UserImage2pathstr = "";
            string UserImage3pathstr = "";
            string UserImage4pathstr = "";
            string UserImage5pathstr = "";

            string CallCenterCommentstr = "";
            string CallCenterImagepathstr = "";
            string StatusStr = "";
            string CallCenterImageRealPath = "";
            string CallCenterCommentPersonstr = "";
            string CallCenterCommentDatestr = "";

            if (!IsPostBack)
            {
                div_Error_Download.Visible = false; 
                div_Error_update.Visible = false;
                div_Success_update.Visible = false;
                CallCenterCommentAddedByDiv.Visible = false;
                CallCenterCommentAddedTimeDiv.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                TicketNum.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();
                string UserName = Request.Cookies["UserName"].Value.Trim();

                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr,ref CreationTimestr , ref MedicalIDstr,ref Reqtypestr, ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr,
                                    ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath, ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);

                Username.InnerText = RequestedUsernamestr;
                CreationTime.InnerText = CreationTimestr;
                MedicalID.InnerText = MedicalIDstr;
                Reqtype.InnerText = Reqtypestr;
                ReqTitle.InnerText = RequestTitlestr;
                RequestSubject.InnerText = RequestSubjectstr;
                CallCenterComment.InnerText = CallCenterCommentstr;
                CallCenterCommentAddedBy.InnerText = CallCenterCommentPersonstr;
                CallCenterCommentAddedTime.InnerText = CallCenterCommentDatestr;

                btn_Approve.Visible = false;
                btn_Reject.Visible = false;
                /////////////////////////////////////////////////////////
                if (type == "CallCenterManager" || type == "CallCenterDoctor" || type == "CallCenterUser")
                {
                    if (StatusStr == "PendingApproval")
                    {
                            btn_Approve.Visible = true;
                            btn_Reject.Visible = true;
                            CallCentgerImageDiv.Visible = false;
                        
                    }
                    else if (StatusStr == "Rejected" || StatusStr == "Approved" )
                    {
                        approveimageDiv.Visible = false;
                        CallCenterCommentAddedByDiv.Visible = true;
                        CallCenterCommentAddedTimeDiv.Visible = true;
                    }
                }
                else if (type == "DirectorUser")
                {
                    if (StatusStr == "PendingApproval")
                    {
                        btn_Approve.Visible = false;
                        btn_Reject.Visible = false;
                        approveimageDiv.Visible = false;
                        CallCentgerImageDiv.Visible = false;
                        CallCenterCommentDiv.Visible = false;
                    }
                    else if (StatusStr == "Rejected" || StatusStr == "Approved")
                    {
                        CallCenterCommentAddedByDiv.Visible = true;
                        CallCenterCommentAddedTimeDiv.Visible = true;
                        approveimageDiv.Visible = false;

                    }
                }

                if (UserImagepathstr==null)    {UserImage.Visible=false; }
                if (UserImage2pathstr == null) { UserImage2.Visible = false; }
                if (UserImage3pathstr == null) { UserImage3.Visible = false; }
                if (UserImage4pathstr == null) { UserImage4.Visible = false; }
                if (UserImage5pathstr == null) { UserImage5.Visible = false; }

            }


        }

        protected void btn_Approve_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Label1.Visible = false;
                id = int.Parse(Request.QueryString["id"]);


                string UserName = Request.Cookies["UserName"].Value.Trim();


                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr,
                                                   ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr,
                                                  ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath,
                                                  ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                ///////////////////////////////////////////////////////////////////////////////////////
                mdb.GetUserDataByMedicalID(MedicalIDstr, ref ClientUsername, ref ClientEmail, ref ClientType);
                if (FileUpload1.HasFile)
                {
                    if (this.UploadFile())
                    {

                        btn_Approve.Visible = false;
                        btn_Reject.Visible = false;
                        mdb.UpdateRequestWithCallCenterResponse(id, UserName, CallCenterComment.Value, base64String, CallCenterImageRealPath, "Approved");
                    this.SendMail("Approved");

                    Server.Transfer("~/Portal/ShowAllApprovals.aspx");

                    }
                    else
                    {
                        div_Error_update.Visible = true;
                        div_Error_update.InnerHtml = "Pdf Dont Uploaded";
                    }
                }
                else
                {
                    Label1.Visible = true;
                }










            }


            catch (Exception ex)
            {
                // div_Error_update.Visible = true;

            }

            /////////////////////////////////////////////////////////////////////////////   

        }
        protected void btn_Reject_ServerClick(object sender, EventArgs e)
        {

            try
            {
                Label1.Visible = false;
             
                int id = int.Parse(Request.QueryString["id"]);
                string UserName = Request.Cookies["UserName"].Value.Trim();


                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr,
                                                   ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr,
                                                  ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath,
                                                  ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                ///////////////////////////////////////////////////////////////////////////////////////
                mdb.GetUserDataByMedicalID(MedicalIDstr, ref ClientUsername, ref ClientEmail, ref ClientType);
                if (FileUpload1.HasFile)
                {
                    if (this.UploadFile())
                    {

                        btn_Approve.Visible = false;
                        btn_Reject.Visible = false;
                        mdb.UpdateRequestWithCallCenterResponse(id, UserName, CallCenterComment.Value, base64String, CallCenterImageRealPath, "Rejected");
                        this.SendMail("Rejected");

                        Server.Transfer("~/Portal/ShowAllApprovals.aspx");

                    }
                    else
                    {
                        div_Error_update.Visible = true;
                        div_Error_update.InnerHtml = "Pdf Dont Uploaded";
                    }
                }
                else
                {
                    Label1.Visible = true;
                }










            }


            catch (Exception ex)
            {
                // div_Error_update.Visible = true;

            }

            /////////////////////////////////////////////////////////////////////////////   


        }
        protected void DownloadClientImage(object sender, EventArgs e)
        {
            try
            {


                string UserName = Request.Cookies["UserName"].Value.Trim();
                // Response.ContentType = ".jpg";

                string RequestedUsernamestr = "", CreationTimestr = "", MedicalIDstr = "", Reqtypestr = "", RequestTitlestr = "";
                string RequestSubjectstr = "", UserImagepathstr = "", UserImage2pathstr = "", UserImage3pathstr = "", UserImage4pathstr = "", UserImage5pathstr = "", CallCenterCommentstr = "", CallCenterImagepathstr = "", StatusStr = "", CallCenterImageRealPath = "",
                       CallCenterCommentPersonstr = "", CallCenterCommentDatestr = "";
                int id = int.Parse(Request.QueryString["id"]);
                // int id = 94;

                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr, ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr, ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath, ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                Response.ContentType = "application/octet-stream";

                Response.AppendHeader("Content-Disposition", "attachment; filename=" + UserImagepathstr + ".jpg");
                //Response.TransmitFile(Server.MapPath(UserImagepathstr));
                Response.TransmitFile(@"\\Mobile-System\inetpub\wwwroot\MobileApi\TransientStorage\" + UserImagepathstr + ".jpg");
                Response.Flush();
            }
            catch (Exception ex)
            {
                div_Error_Download.Visible = true;

            }

        }
        protected void DownloadClientImage2(object sender, EventArgs e)
        {
            try
            {
                string UserName = Request.Cookies["UserName"].Value.Trim();

                string RequestedUsernamestr = "", CreationTimestr = "", MedicalIDstr = "", Reqtypestr = "", RequestTitlestr = "";
                string RequestSubjectstr = "", UserImagepathstr = "", UserImage2pathstr = "", UserImage3pathstr = "", UserImage4pathstr = "", UserImage5pathstr = "", CallCenterCommentstr = "", CallCenterImagepathstr = "", StatusStr = "", CallCenterImageRealPath = "",
                       CallCenterCommentPersonstr = "", CallCenterCommentDatestr = "";
                int id = int.Parse(Request.QueryString["id"]);
                // int id = 94;

                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr, ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr, ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath, ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + UserImage2pathstr + ".jpg");
                //Response.TransmitFile(Server.MapPath(UserImagepathstr));
                Response.TransmitFile(@"\\Mobile-System\inetpub\wwwroot\MobileApi\TransientStorage\" + UserImage2pathstr + ".jpg");
                Response.Flush();
            }
            catch (Exception ex)
            {
                div_Error_Download.Visible = true;

            }
         }
        protected void DownloadClientImage3(object sender, EventArgs e)
        {
            try
            {
                string UserName = Request.Cookies["UserName"].Value.Trim();

                string RequestedUsernamestr = "", CreationTimestr = "", MedicalIDstr = "", Reqtypestr = "", RequestTitlestr = "";
                string RequestSubjectstr = "", UserImagepathstr = "", UserImage2pathstr = "", UserImage3pathstr = "", UserImage4pathstr = "", UserImage5pathstr = "", CallCenterCommentstr = "", CallCenterImagepathstr = "", StatusStr = "", CallCenterImageRealPath = "",
                       CallCenterCommentPersonstr = "", CallCenterCommentDatestr = "";
                int id = int.Parse(Request.QueryString["id"]);
                // int id = 94;

                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr, ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr, ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath, ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + UserImage3pathstr + ".jpg");
                //Response.TransmitFile(Server.MapPath(UserImagepathstr));
                Response.TransmitFile(@"\\Mobile-System\inetpub\wwwroot\MobileApi\TransientStorage\" + UserImage3pathstr + ".jpg");
                Response.Flush();
            }
            catch (Exception ex)
            {
                div_Error_Download.Visible = true;

            }
           
        }
        protected void DownloadClientImage4(object sender, EventArgs e)
        {
            try
            {
                string UserName = Request.Cookies["UserName"].Value.Trim();

                string RequestedUsernamestr = "", CreationTimestr = "", MedicalIDstr = "", Reqtypestr = "", RequestTitlestr = "";
                string RequestSubjectstr = "", UserImagepathstr = "", UserImage2pathstr = "", UserImage3pathstr = "", UserImage4pathstr = "", UserImage5pathstr = "", CallCenterCommentstr = "", CallCenterImagepathstr = "", StatusStr = "", CallCenterImageRealPath = "",
                       CallCenterCommentPersonstr = "", CallCenterCommentDatestr = "";
                int id = int.Parse(Request.QueryString["id"]);
                // int id = 94;

                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr, ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr, ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath, ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + UserImage4pathstr + ".jpg");
                //Response.TransmitFile(Server.MapPath(UserImagepathstr));
                Response.TransmitFile(@"\\Mobile-System\inetpub\wwwroot\MobileApi\TransientStorage\" + UserImage4pathstr + ".jpg");
                Response.Flush();
            }
            catch (Exception ex)
            {
                div_Error_Download.Visible = true;

            }
        }
        protected void DownloadClientImage5(object sender, EventArgs e)
        {
            try
            {
                string UserName = Request.Cookies["UserName"].Value.Trim();

                string RequestedUsernamestr = "", CreationTimestr = "", MedicalIDstr = "", Reqtypestr = "", RequestTitlestr = "";
                string RequestSubjectstr = "", UserImagepathstr = "", UserImage2pathstr = "", UserImage3pathstr = "", UserImage4pathstr = "", UserImage5pathstr = "", CallCenterCommentstr = "", CallCenterImagepathstr = "", StatusStr = "", CallCenterImageRealPath = "",
                       CallCenterCommentPersonstr = "", CallCenterCommentDatestr = "";
                int id = int.Parse(Request.QueryString["id"]);
                // int id = 94;

                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr, ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr, ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath, ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + UserImage5pathstr + ".jpg");
                //Response.TransmitFile(Server.MapPath(UserImagepathstr));
                Response.TransmitFile(@"\\Mobile-System\inetpub\wwwroot\MobileApi\TransientStorage\" + UserImage5pathstr + ".jpg");
                Response.Flush();
            }
            catch (Exception ex)
            {
                div_Error_Download.Visible = true;

            }
        }

        protected void DownloadCallCenterImage(object sender, EventArgs e)
        {
            try
            {

                string UserName = Request.Cookies["UserName"].Value.Trim();
                Response.ContentType = ".pdf";

                string RequestedUsernamestr = "", CreationTimestr = "", MedicalIDstr = "", Reqtypestr = "", RequestTitlestr = "";
                string RequestSubjectstr = "", UserImagepathstr = "", UserImage2pathstr = "", UserImage3pathstr = "", UserImage4pathstr = "", UserImage5pathstr = "", CallCenterCommentstr = "", CallCenterImagepathstr = "", StatusStr = "", CallCenterImageRealPath = "",
                CallCenterCommentPersonstr = "", CallCenterCommentDatestr = "";
                int id = int.Parse(Request.QueryString["id"]);

                mdb.GetApprovalRequestDetailsByid(id, ref RequestedUsernamestr, ref CreationTimestr, ref MedicalIDstr, ref Reqtypestr, ref RequestTitlestr, ref RequestSubjectstr, ref UserImagepathstr, ref CallCenterCommentstr, ref CallCenterImagepathstr, ref StatusStr, ref CallCenterImageRealPath, ref CallCenterCommentPersonstr, ref CallCenterCommentDatestr, ref UserImage2pathstr, ref UserImage3pathstr, ref UserImage4pathstr, ref UserImage5pathstr);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + CallCenterImageRealPath);
                // Response.TransmitFile(Server.MapPath(CallCenterImageRealPath));
             var z=   Server.MapPath(@"\\PDFReply\\") + id.ToString() + @"\\" + CallCenterImageRealPath;

                Response.TransmitFile(Server.MapPath(@"\\CallCenter System\\PDFReply\\" + id.ToString()+@"\"+ CallCenterImageRealPath));
                Response.End();
        }
            catch (Exception ex)
            {
                div_Error_Download.Visible = true;

            }
}

        public bool UploadFile()
        {
            
                Label1.Visible = false;
                if (CallCenterComment.Value != "")
                {
                    Random RandomApprovalNum = new Random();
                    int FileName = RandomApprovalNum.Next() + int.Parse(MedicalIDstr);
                    CallCenterImageRealPath = FileName.ToString() + ".pdf";
                    string UploadUserPDF = ConfigurationManager.AppSettings["PdfForUsers"] + FileName + ".pdf";

                    base64String = ConfigurationManager.AppSettings["pdflocation"]  + FileName + ".pdf";

                    //Upload PDF To Users
                    FileUpload1.SaveAs(UploadUserPDF);

                  if( ! File.Exists(UploadUserPDF))
                    {

                    return false;
                    }

                    //Upload PDF For Call Center

                    Directory.CreateDirectory(Server.MapPath(@"\\CallCenter System\\PDFReply\\") + id + " /");
                    FileUpload1.SaveAs(Server.MapPath(@"\\CallCenter System\\PDFReply\\") + "/" + id + " /" + FileName + ".pdf");

                    
                   
                return true;

                }
                else
                {
                    div_Error_update.Visible = true;
                return false;  
                }
            }

        public void SendMail(string RjectedOrApproved)
        {

            if (ClientEmail != null)
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("noreply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Your Request has been " + RjectedOrApproved;
                message.Body = " Dear " + ClientUsername + " Your Request Number " + id + " \r \t has been " + RjectedOrApproved;
                message.From = "noreply@Prime-Health.org";
                message.ToRecipients.Add(ClientEmail);

                if (ClientType == "Normal")
                {
                    message.CcRecipients.Add("CallCenter@Prime-Health.org");
                }
                else if (ClientType == "Special")
                {
                    message.CcRecipients.Add("SP.CallCenter@Prime-Health.org");

                }
                message.Save();
                message.SendAndSaveCopy();
            }
        }
        }


    }

