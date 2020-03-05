using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.IO.Compression;
using Microsoft.Reporting.WebForms;
using System.Security;
using System.Security.Permissions;
using static CallCenterNotesApp.Enums.Enums;
using System.Net;
using System.Configuration;
using Microsoft.Exchange.WebServices.Data;
using HttpClientSample;
using CallCenterNotesApp.ModelView;

namespace CallCenterNotesApp.Portal
{
    public partial class OnlineApprovalManageRequest : System.Web.UI.Page
    {

        List<string> PersonreceiveEmail = new List<string>();
        List<string> PersonIccEmail = new List<string>();
        List<string> PersonInBccEmail = new List<string>();
        PhNetworkEntities db = new PhNetworkEntities();
        OnlineApprovals_Requests OnlineApprovalObj = new OnlineApprovals_Requests();
        CallCenterAppUser user;
        OnlineApprovalHelper Helper = new OnlineApprovalHelper();
        int RequestID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            string userName = Request.Cookies["UserName"].Value.Trim();
          user =db.CallCenterAppUsers.Where(x=>x.UserName== userName).FirstOrDefault();
            div_Error.Visible = false;
            div_Success.Visible = false;

            RequestID = Convert.ToInt32(Request.QueryString["ID"]);
            OnlineApprovalObj= db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            TicketID.InnerText = OnlineApprovalObj.RequestID.ToString();
            UserMedicalID.Value = OnlineApprovalObj.MedicalID.ToString();
            Member.Value = OnlineApprovalObj.MemberName.ToString();
            MobileNumber.Value = OnlineApprovalObj.MobileNumber.ToString();
            IVRNumber.Value = OnlineApprovalObj.IVRNumber.ToString();
            RequestCreationTimeID.Value = OnlineApprovalObj.RequestCreationTime.ToString();
            Diagnose.Value = OnlineApprovalObj.Diagnose.ToString();
            CloseTime.Value = OnlineApprovalObj.CloseTime.ToString();
            DoctorAssignee.Value = OnlineApprovalObj.CallCenterAssignee;
            Note.Value = OnlineApprovalObj.Notes;
            MedicalApprovalCheck.Checked =Convert.ToBoolean( OnlineApprovalObj.IsMedicalApproval);
            FinancialApprovalCheck.Checked = Convert.ToBoolean(OnlineApprovalObj.IsFinnacialApproval);
            CoInsurancePercentage.Value = OnlineApprovalObj.CoInsurancePercentage.ToString();
            CompanyNameID.Value = OnlineApprovalObj.ClientName.ToString();
            TotalApprovalPrice.Value= OnlineApprovalObj.TotalApprovalPrice.ToString();
            TotalMemberpays.Value = OnlineApprovalObj.TotalMemberpays.ToString();
            TotalPayments.Value = OnlineApprovalObj.TotalPayments.ToString();


            try
            {
                ProviderName.Value = db.PharmacyTBs.Where(x => x.ID == OnlineApprovalObj.ProviderID).FirstOrDefault().PharmName.ToString();
            }
            catch { }

            RequestStatusID.Value = db.OnlineApprovals_RequestStatusDIM.Where(x=>x.RequestSatsusID== OnlineApprovalObj.RequestStatusID).FirstOrDefault().RequestStatusName.ToString();
            TicketAttachment.DataSource = OnlineApprovalObj.OnlineApprovals_RequestAttachment.Where(x => x.RequestID == RequestID &&x.RequestDetailsID==null);
            TicketAttachment.DataBind();


            DrugList.DataSource = db.OnlineApprovals_DemandedDrugsDetails.Where(x => x.RequestID == RequestID).ToList();
            DrugList.DataBind();


            var ListOfDetails =
    from s in OnlineApprovalObj.OnlineApprovals_RequestDetails
    join r in db.OnlineApprovals_RequestDetailsTypeDIM on s.RequestDetailsTypeID equals r.RequestDetailsTypeID

    select
   new { s.Delivered,s.DetailsDate,s.IsDeleted,s.Notes,s.RequestDetailsID,s.RequestDetailsTypeID,s.RequestID,s.Seen,s.UserID,s.UserTypeID,r.RequestDetailsTypeName, //m .UserName
   };

          List<  OlineApprovalsDetailsViewModel> OlineApprovalsDetailsViewModelList = new List<OlineApprovalsDetailsViewModel>();


           foreach (var item in ListOfDetails.ToList())
            {
                OlineApprovalsDetailsViewModel OlineApprovalsDetailsViewModel = new OlineApprovalsDetailsViewModel
                {  Notes=item.Notes,
                DetailTypeName=item.Notes,
                  Delivered=item.Delivered,
                   DetailsDate=item.DetailsDate,
                    RequestDetailsID= item.RequestDetailsID,
                     UserTypeID= item.UserTypeID,
                    UserID= item.UserID,
                     IsDeleted= item.IsDeleted,
                      RequestDetailsTypeID= item.RequestDetailsTypeID,
                       RequestID= item.RequestID,
                        Seen= item.Seen,
                 RequestDetailsTypeName=item.RequestDetailsTypeName};
                if (OlineApprovalsDetailsViewModel.UserTypeID ==(int) UserType.CallCeneter)
                {
                    OlineApprovalsDetailsViewModel.UserName = db.CallCenterAppUsers.Where(x => x.UserID == OlineApprovalsDetailsViewModel.UserID).FirstOrDefault().UserName;
                }
                else
                {
                    OlineApprovalsDetailsViewModel.UserName = db.PharmacyTBs.Where(x => x.ID == OlineApprovalsDetailsViewModel.UserID).FirstOrDefault().PharmName;

                }
                OlineApprovalsDetailsViewModelList.Add(OlineApprovalsDetailsViewModel);

            }

            RequestDetails.DataSource = OlineApprovalsDetailsViewModelList;

            RequestDetails.DataBind();
            

                foreach (var iteminlistview in RequestDetails.Items)
                {

                HiddenField hidden = (HiddenField)(iteminlistview.FindControl("hiddin"));

          var attaches=      OnlineApprovalObj.OnlineApprovals_RequestAttachment.Where(x => x.RequestDetailsID == Convert.ToInt32(hidden.Value)).ToList();
               foreach (var Attachment in attaches)
                    {
                    LinkButton link = new LinkButton();
                    link.Attributes.Add("FileName", Attachment.AttachmentName);
                    link.Attributes.Add("Path", Attachment.AttachmentPath);
                    //link.OnClientClick= LinkButton2_Click(link);
                    link.Click += (s, r) => {
                        LinkButton clickedButton = link;
                        var FileName = clickedButton.Attributes["FileName"].ToString();
                        var FilePath = clickedButton.Attributes["Path"].ToString();

                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName.Replace(" ", string.Empty));
                        Response.WriteFile(FilePath);
                        Response.End();
                    };
                    link.Text = Attachment.AttachmentName;
                        var x = iteminlistview;
                 var NoteController=   x.FindControl("Note");

                    NoteController.Controls.Add(link);
                    Label peace = new Label();
                    peace.Text = "  ";
                    NoteController.Controls.Add(peace);


                }
            }
            Replybtn.Visible = false;
            RejectBtn.Visible = false;
            ApproveBtn.Visible = false;
            TermenateBtn.Visible = false;
            OnholdBtn.Visible = false;
            AssignBtn.Visible = false;
            ReasonComment.Visible = false;
            OnholdBtn.Visible = false;
            EndOnhodBtn.Visible = false;

            if (OnlineApprovalObj.RequestStatusID == (int)OnlineApprovalStatus.New &&
                 user.UserType == "CallCenterManager")
            {
                AssignBtn.Visible = true;
            }
           else if (OnlineApprovalObj.RequestStatusID == (int)OnlineApprovalStatus.New &&
               (user.UserType == "CallCenterDoctor" || user.UserType == "CallCenterAuditDoctor" ))
            {
                try
                {
                    var details = Helper.Assign(RequestID, user);

                    Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);

                    div_Success.Visible = true;
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);



                }
                catch (Exception ex)
                {
                    div_Error.Visible = true;

                }
            }

        


            else if (OnlineApprovalObj.RequestStatusID == (int)OnlineApprovalStatus.PendingCallCenterAction
                || OnlineApprovalObj.RequestStatusID == (int)OnlineApprovalStatus.Reopend
                || OnlineApprovalObj.RequestStatusID == (int)OnlineApprovalStatus.AssignedByCallCenter
                || OnlineApprovalObj.RequestStatusID == (int)OnlineApprovalStatus.PendingProviderAction

                &&
               (user.UserType == "CallCenterDoctor" || user.UserType == "CallCenterAuditDoctor" || user.UserType == "CallCenterManager"))
            {
                Replybtn.Visible = true;
                RejectBtn.Visible = true;
                ApproveBtn.Visible = true;
                TermenateBtn.Visible = true;
                OnholdBtn.Visible = true;
                ReasonComment.Visible = true;

            }



            else if (OnlineApprovalObj.RequestStatusID == (int)OnlineApprovalStatus.Onhold &&
               (user.UserType == "CallCenterDoctor" || user.UserType == "CallCenterAuditDoctor" || user.UserType == "CallCenterManager"))
            {
                EndOnhodBtn.Visible = true;
            }



        }
        protected void LinkButton_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
        private string LinkButton2_Click(LinkButton button)
        {
            LinkButton clickedButton = button;
            var FileName = clickedButton.Attributes["FileName"].ToString();
            var FilePath = clickedButton.Attributes["Path"].ToString();

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName.Replace(" ", string.Empty));
            //Response.WriteFile(FilePath);
            Response.End();
            return "";
        }

        private void OnClientClick(LinkButton link)
        {
            LinkButton clickedButton = link;
            var FileName = clickedButton.Attributes["FileName"].ToString();
            var FilePath = clickedButton.Attributes["Path"].ToString();

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName.Replace(" ", string.Empty));
            Response.WriteFile(FilePath);
            Response.End(); ;
        }

        protected void Approve(object sender, EventArgs e)
        {if (ApproveorrejectNotes.Text != "")
            {
                try
                {
                    var details = Helper.Approve(RequestID, ApproveorrejectNotes.Text, user, IVRtxt.Text);
                    Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);
                    div_Success.Visible = true;


                   // Helper.OnlineApprovalsTakeBackUp(RequestID, details.RequestDetailsID, FileUpload1);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);


                }
                catch (Exception ex)
                {
                    div_Error.Visible = true;
                }
            }
        }
        protected void Reject(object sender, EventArgs e)
        {
            if (ApproveorrejectNotes.Text != "")
            {
                try
                {
                    var details = Helper.Reject(RequestID, ApproveorrejectNotes.Text, user);

                    Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);


                    div_Success.Visible = true;
                   // Helper.OnlineApprovalsTakeBackUp(RequestID, details.RequestDetailsID, FileUpload1);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);


                }
                catch (Exception ex)
                {
                    div_Error.Visible = true;

                }
            }
        }

        protected void Assign(object sender, EventArgs e)
        {

            try
            {
                var details = Helper.Assign(RequestID, user);

                    Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);

                    div_Success.Visible = true;
               // Helper.OnlineApprovalsTakeBackUp(RequestID, details.RequestDetailsID, FileUpload1);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);



        }
                catch (Exception ex)
                {
                    div_Error.Visible = true;

                }

}

        protected void Reply(object sender, EventArgs e)
        {
            if (ApproveorrejectNotes.Text != "")
            {
                try
                {
                    var details = Helper.AddReply(RequestID, ApproveorrejectNotes.Text, user);
                    Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);
                    div_Success.Visible = true;
                    //Helper.OnlineApprovalsTakeBackUp(RequestID, details.RequestDetailsID, FileUpload1);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);



                }
                catch (Exception ex)
                {
                    div_Error.Visible = true;


                }
            }
        }
        protected void Termenate(object sender, EventArgs e)
        {
            if (ApproveorrejectNotes.Text != "")
            {
                try
                {

                    var details = Helper.Termenate(RequestID, ApproveorrejectNotes.Text, user,ReasonComment.Text);
                    Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);
                    div_Success.Visible = true;
                   // Helper.OnlineApprovalsTakeBackUp(RequestID, details.RequestDetailsID, FileUpload1);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);



                }
                catch (Exception ex)
                {
                    div_Error.Visible = true;


                }
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            var FileName = clickedButton.Attributes["FileName"].ToString();
            var FilePath =clickedButton.Attributes["Path"].ToString();

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName.Replace(" ", string.Empty));
            Response.WriteFile(FilePath);
            Response.Flush();
        }

        protected void Onhold(object sender, EventArgs e)
        {
            if (ApproveorrejectNotes.Text != "")
            {
                try
                {
                    var details = Helper.Onhold(RequestID, ApproveorrejectNotes.Text, user,ReasonComment.Text);

                    Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);

                    div_Success.Visible = true;
                  //  Helper.OnlineApprovalsTakeBackUp(RequestID, details.RequestDetailsID, FileUpload1);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);



                }
                catch (Exception ex)
                {
                    div_Error.Visible = true;

                }
            }
        }

        protected void EndOnhod(object sender, EventArgs e)
        {
            try
            {
                var details = Helper.EndOnhold(RequestID, user);

                Helper.OnlineApprovalsRequestFileUpload( RequestID, details.RequestDetailsID, user.UserID, FileUpload1);

                div_Success.Visible = true;
                //Helper.OnlineApprovalsTakeBackUp(RequestID, details.RequestDetailsID, FileUpload1);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);



            }
            catch (Exception ex)
            {
                div_Error.Visible = true;

            }
            
        }

        protected void IVRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IVRCheckBox.Checked == true)
            {
                IVRtxt.Visible = true;
            }
            else
            {
                IVRtxt.Visible = false;
            }
        }
    }
}