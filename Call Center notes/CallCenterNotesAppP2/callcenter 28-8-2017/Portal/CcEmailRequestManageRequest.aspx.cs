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
    public partial class CcEmailRequestManageRequest : System.Web.UI.Page
    {

        List<string> PersonreceiveEmail = new List<string>();
        List<string> PersonIccEmail = new List<string>();
        List<string> PersonInBccEmail = new List<string>();
        PhNetworkEntities db = new PhNetworkEntities();
        CallcentereMailRequest mdb = new CallcentereMailRequest();
        Helpers Helper = new Helpers();
        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error.Visible = false;

            if (!IsPostBack)
            {
                //ActiveUsers.DataTextField = "UserName";
                //ActiveUsers.DataValueField = "UserName";

                //ActiveUsers.DataSource = Helper.GetAllActiveDoctorsAndAuditUsers().Where
                //    (x =>
                //    x.UserType == Request.Cookies["UserType"].Value.Trim()
                //    && x.UserName != Request.Cookies["UserName"].Value.Trim()).ToList();
                //ActiveUsers.DataBind();
                //ListItem listItem = new ListItem("N/A", string.Empty);
                //ActiveUsers.Items.Add(listItem);
                //listItem.Selected = true;
            
                    foreach (TechnicalDestination r in Enum.GetValues(typeof(TechnicalDestination)))
                    {
                        ListItem item = new ListItem(Enum.GetName(typeof(TechnicalDestination), (int)r), ((int)r).ToString());



                    DepartmentList.Items.Add(item);
                    }


                ViewTransfer.Visible = false;
                TransfareBool.Visible = false;

                AssginToMeMGR.Visible = false;
                AsssignComments.Visible = false;
                Release.Visible = false;


                SetInitialRow();
                if (Request.QueryString["ID"] != "")
                {

                    //   SetInitialRow();
                    TicketID.InnerText = Request.QueryString["ID"];
                    var MailingResult = Helper.GetAllMailingDetailsByRequestID(Convert.ToInt32(Request.QueryString["ID"]));
                    List<EmailRequestMailingDetail> FinalMailingToResult = new List<EmailRequestMailingDetail>();
                    List<EmailRequestMailingDetail> FinalMailingCCResult = new List<EmailRequestMailingDetail>();
                    List<EmailRequestMailingDetail> FinalMailingBCCResult = new List<EmailRequestMailingDetail>();

                    foreach (var items in MailingResult.Where(p => p.IsTO == true))
                    {
                        EmailRequestMailingDetail TempTo = new EmailRequestMailingDetail()
                        {
                            Email = items.Email
                        };

                        FinalMailingToResult.Add(TempTo);
                    }

                    foreach (var items in MailingResult.Where(p => p.IsCC == true))
                    {
                        EmailRequestMailingDetail TempCC = new EmailRequestMailingDetail()
                        {
                            Email = items.Email
                        };
                        FinalMailingCCResult.Add(TempCC);

                    }

                    foreach (var items in MailingResult.Where(p => p.IsBCC == true))
                    {
                        EmailRequestMailingDetail TempBCC = new EmailRequestMailingDetail()
                        {
                            Email = items.Email
                        };
                        FinalMailingBCCResult.Add(TempBCC);
                    }

                    if (FinalMailingToResult.Count != 0)
                    {
                        AddToList.DataSource = AppendData(FinalMailingToResult, "AddToList");
                        AddToList.DataBind();
                        // AddNewRowToGrid(AddToList, "AddToList");
                    }

                    if (FinalMailingCCResult.Count != 0)
                    {

                        AddCcList.DataSource = AppendData(FinalMailingCCResult, "AddCcList");
                        AddCcList.DataBind();
                        // AddNewRowToGrid(AddCcList, "AddCcList");
                    }

                    if (FinalMailingBCCResult.Count != 0)
                    {

                        AddBccList.DataSource = AppendData(FinalMailingBCCResult, "AddBccList");
                        AddBccList.DataBind();
                        //  AddNewRowToGrid(AddBccList, "AddBccList");
                    }
                }


                var Results = mdb.GetEmailApprovalRequestsDetailByid(int.Parse(Request.QueryString["id"]));

                TicketSubjectID.Value = Results.MailSubject;
                PatientNameID.Value = Results.PatientName;
                UserMedicalID.Value = Results.Medical_ID.ToString();
                CompanyNameID.Value = Results.CompanyName;
                UserNotes.Text = Results.CreatedByNotes;
                RequestCreatorID.Value = Results.CreatedBy;
                RequestCreationTimeID.Value = Results.CreationDate.ToString();
                AssignedDoctorNameID.Value = Results.DoctorAssignee;
                CloseTime.Value = Results.ClosedTime.ToString();
                ApprovalCategory.SelectedValue = Convert.ToString(Results.ApprovalCategoryID);
                TicketTypeIDID.SelectedValue = Convert.ToString(Results.TicketTypeID);

                if (Results.DoctorNotes != null)
                { ApproveorrejectNotes.Text = Results.DoctorNotes; }
                else if (Results.IsInquiryTicket == true)
                {
                    ApproveorrejectNotes.Text = Results.OperatorNotes;

                }
                else { ApproveorrejectNotes.Text = Results.AuditNotes; }

                AuditApprovalNameID.Value = Results.AuditAssignee;
                TechnicalApproveCommentByAudit.Value = Results.TechnicalApproveByAuditNote;
                TransfareRequestToAuditNote.Value = Results.TransferedToAuditComment;
                ReopenBtn.Visible = false;
                ReopenComment.Visible = false;
                TechnicalApproveCommentByDoctor.Value = Results.TechnicalApproveByDoctorNote;

                SaveButton.Visible = false;
                CancelButton.Visible = false;
                EditButton.Visible = false;

                SaveLinkButton.Visible = false;
                CancelLinkButton.Visible = false;
                UpdateLinkButton.Visible = false;



                EditTicketTypeBTN.Visible = false;
                SaveTicketTypeBTN.Visible = false;
                CancelTicketTypeBTN.Visible = false;


                ApprovalCategory.Enabled = false;
                TicketTypeIDID.Enabled = false;

                MailSource.Value = Results.MailSource;
                OpreatorName.Value = Results.OperatorAssignee;
            }
            else
            {
                var Results = mdb.GetEmailApprovalRequestsDetailByid(int.Parse(Request.QueryString["ID"]));
                TicketSubjectID.Value = Results.MailSubject;
                PatientNameID.Value = Results.PatientName;
                //UserMedicalID.Value = Results.Medical_ID.ToString();
                CompanyNameID.Value = Results.CompanyName;
                UserNotes.Text = Results.CreatedByNotes;
                RequestCreatorID.Value = Results.CreatedBy;
                RequestCreationTimeID.Value = Results.CreationDate.ToString();
                AssignedDoctorNameID.Value = Results.DoctorAssignee;
                AuditApprovalNameID.Value = Results.AuditAssignee;

            }

            #region Visability of Div

            TransfareBool.Visible = false;
            ViewTransfer.Visible = false;

           AssginToMeMGR.Visible = false;
            AsssignComments.Visible = false;

            ApproveBtn.Visible = false;
            RejectBtn.Visible = false;
            TransferToAuditbutton.Visible = false;
            AssignBtn.Visible = false;
            // DownlaodAuiditFilesBtn.Visible = false;
            // DownlaodDoctorFilesBtn.Visible = false;
            CloseBtn.Visible = false;
            EndTechnicalApproveBtn.Visible = false;
            Buttonticnecalapproval.Visible = false;
            TechnicalApproveFromDepartmentsBtn.Visible = false;
            DepartmentList.Visible = false;
            ApproveorrejectNotes.EnableHtmlMode = false;
            FileUpload3.Visible = false;
            // ApproveOrRejectDiv.Visible = false;
            ViewEmailSection.Disabled = true;
            AddToList.Enabled = false;
            AddCcList.Enabled = false;
            AddBccList.Enabled = false;
            AddTo.Enabled = false;
            AddCc.Enabled = false;
            AddBcc.Enabled = false;
            div_Success.Visible = false;
            IgnoreBtn.Visible = false;

            #endregion

            string type = Request.Cookies["UserType"].Value.Trim();
            string UserName = Request.Cookies["UserName"].Value.Trim();
            int CCReqid = int.Parse(Request.QueryString["ID"]);
            //int CCReqid = 10;
            //Label1.InnerText = id.ToString();

            var Result = mdb.GetEmailApprovalRequestsDetailByid(CCReqid);




            //new code------------------------------------

            if ((type == "CallCenterDoctor" && Result.DoctorAssignee == UserName.ToString()))
            {
                EditTicketTypeBTN.Visible = true;
                EditButton.Visible = true;
                UpdateLinkButton.Visible = true;

            }
            else if ((type == "CallCenterAuditDoctor" || type == "CallCenterManager") && Result.AuditAssignee == UserName.ToString())
            {
                EditTicketTypeBTN.Visible = true;
                EditButton.Visible = true;
                UpdateLinkButton.Visible = true;
            }



            if (Result.RequstStatusID == (int)RequestStatus.PendingDoctorsAssign && type == "CallCenterDoctor")
            {
                //AssignBtn.Visible = true;

                Helper.AssignRequestToMe(Request.Cookies["UserName"].Value.Trim(), Request.Cookies["UserID"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]));

                Page.Response.Redirect(Page.Request.Url.ToString(), true);

            }

            if ((Result.RequstStatusID == (int)RequestStatus.AssignedByDoctor ||
                Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor ||
                Result.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor)
                && (type == "CallCenterDoctor" && Result.DoctorAssignee == UserName))

            {
                ViewTransfer.Visible = true;
                TransfareBool.Visible = true;
                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                TransferToAuditbutton.Visible = true;
                if (Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor || Result.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor)
                {
                    Buttonticnecalapproval.Visible = false;
                }
                else
                {
                    Buttonticnecalapproval.Visible = true;
                }



              if(  db.EmailRequestRequest_TechnicalDestination.Where(x=>x.RequestID== CCReqid).Count() ==0)

                {

                    TechnicalApproveFromDepartmentsBtn.Visible = true;
                    DepartmentList.Visible = true;

                }
                RequiredFieldValidator4.Enabled = false;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                FileUpload3.Visible = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
                IgnoreBtn.Visible = true;

            }


            if ((Result.RequstStatusID == (int)RequestStatus.AssignedByDoctor ||
                 Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor || Result.RequstStatusID==(int) RequestStatus.PendingTechnicalApproveByDoctor||
                 Result.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor)
                && (type == "CallCenterManager"))

            {

                ViewTransfer.Visible = true;
                TransfareBool.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.ApprovedByDoctor && type == "CallCenterDoctor" && Result.DoctorAssignee == UserName)

            {
                ApproveOrRejectDiv.Visible = true;

                CloseBtn.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.RejectedByDoctor && type == "CallCenterDoctor" && Result.DoctorAssignee == UserName)

            {
                ApproveOrRejectDiv.Visible = true;

                CloseBtn.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.PendingDoctorsAssign && type == "CallCenterManager" && Result.DoctorAssignee != null)
            {
               Release.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.PendingAuditAssign && (type == "CallCenterAuditDoctor" || type == "CallCenterManager"))
            {
                // AssignBtn.Visible = true;
                if (type == "CallCenterManager")
                {
                    if (Result.AuditAssignee != null)
                    { Release.Visible = true; }
                    else
                    {
                        AssignBtn.Visible = true;
                    }

                }
                else if (type == "CallCenterAuditDoctor")
                {
                    Helper.AssignRequestToMe(Request.Cookies["UserName"].Value.Trim(), Request.Cookies["UserID"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]));
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);

                }
            }


            if ((Result.RequstStatusID == (int)RequestStatus.AssignedByAudit ||
                 Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit ||
                 Result.RequstStatusID == (int)RequestStatus.ReOpenedByAudit)
                && ((type == "CallCenterAuditDoctor" || type == "CallCenterManager") && Result.AuditAssignee == UserName))
            {
                ViewTransfer.Visible = true;
                TransfareBool.Visible = true;
                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                Buttonticnecalapproval.Visible = true;
                TechnicalApproveFromDepartmentsBtn.Visible =true;
                DepartmentList.Visible = true;
                RequiredFieldValidator3.Enabled = false;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
                IgnoreBtn.Visible = true;
            }
            if ((Result.RequstStatusID == (int)RequestStatus.AssignedByAudit ||
                 Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit || Result.RequstStatusID == (int)RequestStatus.PendingTechnicalApproveByAudit ||
                 Result.RequstStatusID == (int)RequestStatus.ReOpenedByAudit)
                && (type == "CallCenterManager") && (Result.AuditAssignee!=UserName))

            {

                ViewTransfer.Visible = true;
                TransfareBool.Visible = true;

                //Open Assign To Me Buttons
                AssginToMeMGR.Visible = true;
                AsssignComments.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.ApprovedByAudit && (type == "CallCenterAuditDoctor" || type == "CallCenterManager") && Result.AuditAssignee == UserName)
            {
                ApproveOrRejectDiv.Visible = true;

                CloseBtn.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.RejectedByAudit && (type == "CallCenterAuditDoctor" || type == "CallCenterManager") && Result.AuditAssignee == UserName)
            {

                ApproveOrRejectDiv.Visible = true;

                CloseBtn.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit && (type == "CallCenterAuditDoctor" || type == "CallCenterManager") && Result.AuditAssignee == UserName)
            {
                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                Buttonticnecalapproval.Visible = false;
                TechnicalApproveFromDepartmentsBtn.Visible = false;
                DepartmentList.Visible = false;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
                IgnoreBtn.Visible = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor && type == "CallCenterDoctor" && Result.DoctorAssignee == UserName)
            {
                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                TransferToAuditbutton.Visible = true;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
            }
            if (Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveFromDepartments && Result.UserNameOpenTechnicalApprove == UserName &&type == "CallCenterDoctor")
            {
                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                TransferToAuditbutton.Visible = true;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
                if (Result.TechnicalApproveByDoctorStartTime == null)
                    Buttonticnecalapproval.Visible = true;
            }

            if (Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveFromDepartments && Result.UserNameOpenTechnicalApprove == UserName && type == "CallCenterAuditDoctor")
            {
                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
             if(   Result.TechnicalApproveByAuditStartTime==null)
                Buttonticnecalapproval.Visible = true;
            }
            




            if (Result.RequstStatusID == (int)RequestStatus.PendingTechnicalApproveByAudit && (type == "CallCenterAuditDoctor" || type == "CallCenterManager") && Result.AuditAssignee == UserName)
            {
                EndTechnicalApproveBtn.Visible = true;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
            }

            if (Result.RequstStatusID == (int)RequestStatus.PendingTechnicalApproveByDoctor && type == "CallCenterDoctor" && Result.DoctorAssignee == UserName)
            {
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                EndTechnicalApproveBtn.Visible = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
            }

            if (Result.IsInquiryTicket == true
                ||
                Result.IsFaxMail == true ||
                (type == "CallCenterUser" && Result.RequstStatusID == (int)RequestStatus.NewAutoGenerated) ||
                (Result.OperatorAssignee == UserName && Result.RequstStatusID == (int)RequestStatus.AssignedByOpeartor)

                )

            {
                Response.Redirect("OpenAutoGenerated.aspx?Id=" + Result.ID.ToString());
            }

            if ((Result.RequstStatusID == (int)RequestStatus.Closed || Result.RequstStatusID == (int)RequestStatus.Ignored) &&
                (type == "CallCenterDoctor" || type == "CallCenterAuditDoctor" || type == "CallCenterManager") && (Result.DoctorAssignee != null || Result.AuditAssignee != null))

            {
                ReopenBtn.Visible = true;
                ReopenComment.Visible = true;
            }


            if (Result.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor && (type == "CallCenterDoctor") && Result.DoctorAssignee == UserName)
            {
                TransfareBool.Visible = true;

                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                TransferToAuditbutton.Visible = true;
                RequiredFieldValidator3.Enabled = false;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
                ReopenBtn.Visible = false;
                ReopenComment.Visible = false;
                TransferToAuditbutton.Visible = false;
                IgnoreBtn.Visible = false;
                FileUpload2.Visible = true;
            }

            if (Result.RequstStatusID == (int)RequestStatus.ReOpenedByAudit && (type == "CallCenterAuditDoctor" || type == "CallCenterManager") && Result.AuditAssignee == UserName)
            {
                TransfareBool.Visible = true;

                ApproveOrRejectDiv.Visible = true;
                ApproveBtn.Visible = true;
                RejectBtn.Visible = true;
                RequiredFieldValidator3.Enabled = false;
                ViewEmailSection.Disabled = false;
                AddToList.Enabled = true;
                AddCcList.Enabled = true;
                AddBccList.Enabled = true;
                AddTo.Enabled = true;
                AddCc.Enabled = true;
                AddBcc.Enabled = true;
                ReopenBtn.Visible = false;
                ReopenComment.Visible = false;
            }


            DoctorAttachment.DataSource = Helper.GetAllAttachmentByRequestId(Result.ID).Where(x => x.IsDoctorAttachment == true);
            DoctorAttachment.DataBind();

            AuditAttachment.DataSource = Helper.GetAllAttachmentByRequestId(Result.ID).Where(x => x.IsAuditAttachment == true);
            AuditAttachment.DataBind();
            TicketAttachment.DataSource = Helper.GetAllAttachmentByRequestId(Result.ID).Where(x => x.IsTicketAttachment == true && x.IsDeleted != true);
            TicketAttachment.DataBind();



            RequestStatusID.Value = ((RequestStatus)Enum.Parse(typeof(RequestStatus), Result.RequstStatusID.ToString())).ToString();

            if (Result.TicketTypeID == 1)
            {
                TicketTypeIDID.SelectedItem.Value = ((int)TicketTypes.General).ToString();
            }
            else if (Result.TicketTypeID == 2)
            {
                TicketTypeIDID.SelectedItem.Value = ((int)TicketTypes.Special).ToString();
            }
            else if (Result.TicketTypeID == 3)
            {
                TicketTypeIDID.SelectedItem.Value = ((int)TicketTypes.Inquiries).ToString();
            }
            else if (Result.TicketTypeID == 4)
            {

                TicketTypeIDID.SelectedItem.Value = ((int)TicketTypes.None).ToString();
            }


            if (Result.ApprovalCategoryID == 1)
            {

                ApprovalCategory.SelectedItem.Value = ((int)TicketCategory.Inpatient).ToString();
            }
            else if (Result.ApprovalCategoryID == 2)
            {
                ApprovalCategory.SelectedItem.Value = ((int)TicketCategory.Outpatient).ToString();

            }
            else if (Result.ApprovalCategoryID == 3)
            {
                ApprovalCategory.SelectedItem.Value = ((int)TicketCategory.Medication).ToString();

            }

            if (Result.PriorityID == 1)
            {

                PriorityID.Value = TicketPriority.Normal.ToString();
            }
            else if (Result.PriorityID == 2)
            {

                PriorityID.Value = TicketPriority.High.ToString();
            }
            else if (Result.PriorityID == 3)
            {

                PriorityID.Value = TicketPriority.Emergency.ToString();
            }
            try
            {
                TechnicalApprovelToDepartmentComment.Value = db.EmailRequestRequest_TechnicalDestination.Where(x => x.RequestID == Result.ID).FirstOrDefault().CallCenterNote;
            }
            catch { }

         List<   trackTechnicalApprovel> ListTrackObject = new List<trackTechnicalApprovel>();
            foreach (var item in db.EmailRequestRequest_TechnicalDestination.Where(x => x.RequestID == Result.ID).ToList())
            {
                trackTechnicalApprovel TrackObject = new trackTechnicalApprovel {
                     ActionTime= item.ActionTime,
                      Assignee= item.Assignee,
                       AssignTime= item.AssignTime,
                        CallCenterNote= item.CallCenterNote,
                         EndTechnicalApprovalTime= item.EndTechnicalApprovalTime,
                          ID= item.ID,
                           Note= item.Note,
                            RequestID= item.RequestID,
                             StartTechnicalApprovalTime= item.StartTechnicalApprovalTime,
                              TechnicalDestinationID= item.TechnicalDestinationID

                };



                if(TrackObject.EndTechnicalApprovalTime==null)
                {
                    TrackObject.IsDone = false;


                }
                else
                {
                    TrackObject.IsDone = true;

                }                   

                TrackObject.DepartmentName = Enum.GetName(typeof(TechnicalDestination), TrackObject.TechnicalDestinationID); ;
                ListTrackObject.Add(TrackObject);

            }
            TrackOfTechnicalApproval.DataSource = ListTrackObject;
            TrackOfTechnicalApproval.DataBind();





        }
        protected void Approve(object sender, EventArgs e)
        {
            if (ApproveorrejectNotes.Text != string.Empty)
            {
                string ValidationResult = "";
                PersonreceiveEmail.AddRange(EmailResult(AddToList));
                PersonIccEmail.AddRange(EmailResult(AddCcList));
                PersonInBccEmail.AddRange(EmailResult(AddBccList));

                int RequestID = Convert.ToInt32(Request.QueryString["ID"]);

                var ALLTo = db.EmailRequestMailingDetails.Where(x => x.TicketNumber == RequestID && x.IsDeleted == false
                  && x.IsTO == true).ToList();

                var ALLCC = db.EmailRequestMailingDetails.Where(x => x.TicketNumber == RequestID && x.IsDeleted == false
               && x.IsCC == true).ToList();


                var ALLBCC = db.EmailRequestMailingDetails.Where(x => x.TicketNumber == RequestID && x.IsDeleted == false
               && x.IsBCC == true).ToList();


                List<string> newpersonrecive = Helper.UpdateEmails(int.Parse(Request.QueryString["ID"]),
                    PersonreceiveEmail, Request.Cookies["UserName"].Value.Trim(), ALLTo);
                List<string> newpersonincc = Helper.UpdateEmails(int.Parse(Request.QueryString["ID"]),
                    PersonIccEmail, Request.Cookies["UserName"].Value.Trim(), ALLCC);
                List<string> newpersonibcc = Helper.UpdateEmails(int.Parse(Request.QueryString["ID"]),
                     PersonInBccEmail, Request.Cookies["UserName"].Value.Trim(), ALLBCC);


                Helper.AddEmailRequestMailingDetailsByRequestID(int.Parse(Request.QueryString["ID"]),
                    newpersonrecive, newpersonincc, newpersonibcc);
                bool result = Helper.ApproveRequestByUserType(Convert.ToInt32(Request.QueryString["ID"]), ApproveorrejectNotes.Text, Request.Cookies["UserName"].Value.Trim(), FileUpload2.PostedFile, FileUpload2, Server.MapPath("~/"), out ValidationResult);

                if (!result)
                {
                    div_Error.Visible = true;

                    div_Error.InnerText = ValidationResult;
                }
                else
                {
                    int TicketId = Convert.ToInt32(Request.QueryString["ID"]);
                    var Ticket = db.EmailApprovalRequests.Where(p => p.ID == TicketId).FirstOrDefault();
                    //if (Ticket.IsAutoGenereated == true)
                    //{
                    //   // CallApi.EmailTracking(Ticket, true);
                    //    UpdateEmailAsRead.AutoGeneratedEmailTracking(Ticket.AutoGeneratedEmailID,Ticket.TicketTypeID, true);
                    //}
                    Helper.CreateBackup(FileUpload2.PostedFile, Convert.ToInt32(Request.QueryString["ID"]), Request.Cookies["UserType"].Value.Trim(), FileUpload2, false);

                    if (Ticket.IsAutoGenereated == true)
                    {
                        //CallApi.EmailTracking(Ticket, true);
                        UpdateEmailAsRead.AutoGeneratedEmailTracking(Ticket.AutoGeneratedEmailID, Ticket.TicketTypeID, true);

                    }
                    Helper.CloseEmailRequest(Convert.ToInt32(Request.QueryString["ID"]));

                    Page.Response.Redirect("UnAssignedEmailApprovals.aspx", true);

                }


            }
            else
            {
                div_Error.Visible = true;
            }
        }
        protected void Reject(object sender, EventArgs e)
        {

            if (ApproveorrejectNotes.Text != string.Empty)
            {
                string ValidationResult = "";
                PersonreceiveEmail.AddRange(EmailResult(AddToList));
                PersonIccEmail.AddRange(EmailResult(AddCcList));
                PersonInBccEmail.AddRange(EmailResult(AddBccList));


                int RequestID = Convert.ToInt32(Request.QueryString["ID"]);

                var ALLTo = db.EmailRequestMailingDetails.Where(x => x.TicketNumber == RequestID && x.IsDeleted == false
                  && x.IsTO == true).ToList();

                var ALLCC = db.EmailRequestMailingDetails.Where(x => x.TicketNumber == RequestID && x.IsDeleted == false
               && x.IsCC == true).ToList();


                var ALLBCC = db.EmailRequestMailingDetails.Where(x => x.TicketNumber == RequestID && x.IsDeleted == false
               && x.IsBCC == true).ToList();



                List<string> newpersonrecive = Helper.UpdateEmails(int.Parse(Request.QueryString["ID"]),
                    PersonreceiveEmail, Request.Cookies["UserName"].Value.Trim(), ALLTo);
                List<string> newpersonincc = Helper.UpdateEmails(int.Parse(Request.QueryString["ID"]),
                    PersonIccEmail, Request.Cookies["UserName"].Value.Trim(), ALLCC);
                List<string> newpersonibcc = Helper.UpdateEmails(int.Parse(Request.QueryString["ID"]),
                     PersonInBccEmail, Request.Cookies["UserName"].Value.Trim(), ALLBCC);


                Helper.AddEmailRequestMailingDetailsByRequestID(int.Parse(Request.QueryString["ID"]),
                    newpersonrecive, newpersonincc, newpersonibcc);
                bool result = Helper.RejectRequestByUserType(Convert.ToInt32(Request.QueryString["ID"]), ApproveorrejectNotes.Text, Request.Cookies["UserName"].Value.Trim(), FileUpload2.PostedFile, FileUpload2, Server.MapPath("~/"), out ValidationResult);

                if (!result)
                {
                    div_Error.Visible = true;

                    div_Error.InnerText = ValidationResult;



                }
                else
                {
                    int TicketId = Convert.ToInt32(Request.QueryString["ID"]);
                    var Ticket = db.EmailApprovalRequests.Where(p => p.ID == TicketId).FirstOrDefault();
                    //if (Ticket.IsAutoGenereated == true)
                    //{
                    //   // CallApi.EmailTracking(Ticket, true);
                    //    UpdateEmailAsRead.AutoGeneratedEmailTracking(Ticket.AutoGeneratedEmailID, Ticket.TicketTypeID, true);

                    //}
                    Helper.CreateBackup(FileUpload2.PostedFile, Convert.ToInt32(Request.QueryString["ID"]), Request.Cookies["UserType"].Value.Trim(), FileUpload2, false);

                    if (Ticket.IsAutoGenereated == true)
                    {
                        //CallApi.EmailTracking(Ticket, true);
                        UpdateEmailAsRead.AutoGeneratedEmailTracking(Ticket.AutoGeneratedEmailID, Ticket.TicketTypeID, true);

                    }
                    Helper.CloseEmailRequest(Convert.ToInt32(Request.QueryString["ID"]));

                    Page.Response.Redirect("UnAssignedEmailApprovals.aspx", true);

                }
            }
            else
            {
                div_Error.Visible = true;
            }


        }
        protected void TransferToAudit(object sender, EventArgs e)
        {
            string ValidationResult;
            bool Result = Helper.TransfareRequestToAudit(Convert.ToInt32(Request.QueryString["ID"]), TransfareRequestToAuditNote.Value,


             Request.Cookies["UserName"].Value.Trim(), FileUpload3.PostedFile, FileUpload3, Server.MapPath("~/"), out ValidationResult);




            if (!Result)
            {
                div_Error.Visible = true;

                div_Error.InnerText = "Can Not Transfer TO Audit";
            }
            else
            {
                Helper.CreateBackup(FileUpload3.PostedFile, Convert.ToInt32(Request.QueryString["ID"]), Request.Cookies["UserType"].Value.Trim(), FileUpload3, false);
                Page.Response.Redirect("UnAssignedEmailApprovals.aspx", true);

            }
        }
        protected void Close(object sender, EventArgs e)
        {
            int TicketId = Convert.ToInt32(Request.QueryString["ID"]);
            var Ticket = db.EmailApprovalRequests.Where(p => p.ID == TicketId).FirstOrDefault();
            if (Ticket.IsAutoGenereated == true)
            {
                //CallApi.EmailTracking(Ticket, true);
                UpdateEmailAsRead.AutoGeneratedEmailTracking(Ticket.AutoGeneratedEmailID, Ticket.TicketTypeID, true);

            }
            Helper.CloseEmailRequest(Convert.ToInt32(Request.QueryString["ID"]));

            Page.Response.Redirect("UnAssignedEmailApprovals.aspx", true);
        }
        protected void Assign(object sender, EventArgs e)
        {
            var Result = Helper.AssignRequestToMe(Request.Cookies["UserName"].Value.Trim(), Request.Cookies["UserID"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]));
            if (!Result)
            {

                div_Error.Visible = true;
                div_Error.InnerText = "Request Is Assigned To SomeOne else";

            }
            else
            {

                Page.Response.Redirect(Page.Request.Url.ToString(), true);

            }
        }

        protected void AssignRequestToManager(object sender, EventArgs e)
        {
            var Result = Helper.AssignRequestToMGR(Request.Cookies["UserName"].Value.Trim(), Request.Cookies["UserID"].Value.Trim()
                , Convert.ToInt32(Request.QueryString["ID"]), AssginComment.Value);

            if (!Result)
            {

                div_Error.Visible = true;
                div_Error.InnerText = "Request Is Assigned To SomeOne else";

            }
            else
            {

                Page.Response.Redirect(Page.Request.Url.ToString(), true);

            }
        }

        protected void transfer_ServerClick(object sender, EventArgs e)
        {
            string name = Request.Cookies["UserName"].Value.Trim();
            int UserId = db.CallCenterAppUsers.Where(x => x.UserName == name).FirstOrDefault().UserID;

            string message;
            bool Result = Helper.TransfareRequestToBoolOrUser(Convert.ToInt32(
                 Request.QueryString["id"])
                 , UserId
                 , TransferComment.Value, out message);
            if (Result == true)
            {
                Page.Response.Redirect("UnAssignedEmailApprovals.aspx", true);
                div_Success.Visible = true;
                div_Success.InnerText = message;
            }
            else
            {
                div_Error.Visible = true;
                div_Error.InnerText = message;
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }



        }

        protected void DownlaodAuiditFiles(object sender, EventArgs e)
        {



        }
        protected void EndTechnicalApprove(object sender, EventArgs e)
        {
            Helper.EndTechnicalApproveByUserType(Request.Cookies["UserName"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]));
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }
        protected void TechnicalApprove(object sender, EventArgs e)
        {
            string type = Request.Cookies["UserType"].Value.Trim();
            List<string> DepartmentsID = new List<string>(); ;
            foreach (ListItem item in DepartmentList.Items)
            {
                if (item.Selected)
                {
                    DepartmentsID.Add( item.Value);
                }
            }



            if (type == "CallCenterDoctor")
            {
                Helper.StartTechnicalApproveByUserType(Request.Cookies["UserName"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]), TechnicalApproveCommentByDoctor.Value);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else
            {


                Helper.StartTechnicalApproveByUserType(Request.Cookies["UserName"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]), TechnicalApproveCommentByAudit.Value);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }



        }

        protected void TechnicalApproveByDepartment(object sender, EventArgs e)
        {
            string type = Request.Cookies["UserType"].Value.Trim();
            List<int> DepartmentsID = new List<int>(); ;
            foreach (ListItem item in DepartmentList.Items)
            {
                if (item.Selected)
                {
                    DepartmentsID.Add(Convert.ToInt32( item.Value));
                }
            }

            if (DepartmentsID.Count > 0)
            {
                if (type == "CallCenterDoctor")
                {
                    Helper.TechnicalApproveWithOtherTeam(Request.Cookies["UserName"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]), DepartmentsID, TechnicalApprovelToDepartmentComment.Value);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {


                    Helper.TechnicalApproveWithOtherTeam(Request.Cookies["UserName"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]), DepartmentsID, TechnicalApprovelToDepartmentComment.Value);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
            }


        }


        protected void PrintReport(object sender, EventArgs e)
        {
            int CCReqid = int.Parse(Request.QueryString["ID"]);
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "Report1.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            List<EmailApprovalRequest> cm = new List<EmailApprovalRequest>();
            cm = db.EmailApprovalRequests.Where(a => a.ID.Equals(CCReqid)).ToList();
            ReportDataSource rd = new ReportDataSource("CallCenter", cm);
            lr.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));
            lr.DataSources.Add(rd);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.BinaryWrite(renderedBytes); // create the file
            Response.Flush();

        }
        protected void DoctorAttachment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            var FileName = clickedButton.Attributes["FileName"].ToString();
            var FilePath = clickedButton.Attributes["Path"].ToString();

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName.Replace(" ", string.Empty));
            Response.WriteFile(FilePath);
            Response.End();


            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //response.ClearContent();
            //response.Clear();
            //response.ContentType = "text/plain";
            //response.AddHeader("Content-Disposition",
            //                   "attachment; filename=" + FileName + ";");
            //response.TransmitFile(Path + "/"+FileName);
            //response.Flush();
            //response.End();


            //if (true)
            //{
            //    Response.Clear();
            //    Response.BufferOutput = false;
            //    Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Length", Path);
            //    Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            //    Response.ContentType = "image/"+FileName.Split('.')[1];
            //    Response.Buffer = true;
            //    Response.TransmitFile(Path+FileName);
            //    //Response.Flush();
            //}
            //else
            //{
            //}





        }
        protected void AddNewEmail(object sender, EventArgs e)
        {





        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            //PersonreceiveEmail.Add(EmailResult(receiveEmail);
            //PersonIccEmail = EmailResult(InCC);
            //PersonInBccEmail = EmailResult(InPCC);


        }
        private List<string> EmailResult(ListView listview)
        {
            List<string> result = new List<string>();
            //var list = listview.Controls[1];
            for (int i = 0; i < listview.Controls.Count; i++)
            {
                foreach (var item in listview.Controls[i].Controls)

                {
                    try
                    {
                        TextBox txt = (TextBox)item;
                        if (txt.Text != string.Empty)
                        {
                            result.Add(txt.Text);

                        }
                    }
                    catch { }


                }
            }

            return result;

        }
        private void AddNewRowToGrid(ListView listview, string NameOfViewState)
        {
            int rowIndex = 0;
            if (ViewState[NameOfViewState] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState[NameOfViewState];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values 
                        Button box1 = (Button)listview.Items[rowIndex].FindControl("RowNumber");
                        TextBox box2 = (TextBox)listview.Items[rowIndex].FindControl("TextBox1");
                        box1.Text = "X";
                        drCurrentRow = dtCurrentTable.NewRow();
                        // drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ID"] = "X";
                        dtCurrentTable.Rows[i - 1]["Email"] = box2.Text;
                        rowIndex++;
                    }
                    drCurrentRow["ID"] = "X";
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState[NameOfViewState] = dtCurrentTable;

                    listview.DataSource = dtCurrentTable;
                    listview.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //Set Previous Data on Postbacks 
        }
        private void DeleteRowFromGrid(ListView listview, string NameOfViewState, int OrderOfRemovedRow)
        {

            var c = ViewState[NameOfViewState];
            if (ViewState[NameOfViewState] != null && OrderOfRemovedRow != 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState[NameOfViewState];
                dtCurrentTable.Rows[OrderOfRemovedRow].Delete();

                listview.DataSource = dtCurrentTable;
                listview.DataBind();
            }
        }
        protected void ButtonAdd_Click1(object sender, EventArgs e)
        {
        }
        protected void ADDreceiveofEmail(object sender, EventArgs e)
        {
            AddNewRowToGrid(AddToList, "AddToList");
        }
        protected void ADDInCCofEmailclick(object sender, EventArgs e)
        {
            AddNewRowToGrid(AddCcList, "AddCcList");
        }
        protected void ADDInBCCofEmailclick(object sender, EventArgs e)
        {
            AddNewRowToGrid(AddBccList, "AddBccList");
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            // dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dr = dt.NewRow();
            dr["ID"] = "X";
            dr["Email"] = string.Empty;
            //dr["Column2"] = string.Empty;
            //  dr["Column3"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["AddToList"] = dt;
            AddToList.DataSource = dt;
            AddToList.DataBind();

            ViewState["AddCcList"] = dt;
            AddCcList.DataSource = dt;
            AddCcList.DataBind();

            ViewState["AddBccList"] = dt;

            AddBccList.DataSource = dt;
            AddBccList.DataBind();
        }
        //protected void receiveEmail_Click(object sender, EventArgs e)
        //{
        //    int count = 0;
        //    Button buttonsend = (Button)(sender);



        //    DeleteRowFromGrid(AddToList, "AddToList", count);

        //}
        //protected void RowNumberInCC_Click(object sender, EventArgs e)
        //{


        //    Button DeleteButton = sender as Button;
        //    int OrderNumber = Convert.ToInt32(DeleteButton.Attributes["order"].ToString());
        //    DeleteRowFromGrid(AddCcList, "AddCcList", OrderNumber);
        //}
        //protected void RowNumberInBCC_Click(object sender, EventArgs e)
        //{


        //    Button DeleteButton = sender as Button;



        //    int OrderNumber = Convert.ToInt32(DeleteButton.ToString());
        //    DeleteRowFromGrid(AddBccList, "AddBccList", OrderNumber);
        //}
        protected void receiveEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = AddToList.SelectedIndex;
        }

        protected void receiveEmail_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            AddToList.SelectedIndex = e.NewSelectedIndex;
            DeleteRowFromGrid(AddToList, "AddToList", AddToList.SelectedIndex);

        }

        protected void InBCC_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {



        }

        protected void InCC_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            AddCcList.SelectedIndex = e.NewSelectedIndex;

            DeleteRowFromGrid(AddCcList, "AddCcList", AddCcList.SelectedIndex);
        }

        protected void InBCC_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            AddBccList.SelectedIndex = e.NewSelectedIndex;

            DeleteRowFromGrid(AddBccList, "AddBccList", AddBccList.SelectedIndex);

        }

        public DataTable AppendData(List<EmailRequestMailingDetail> Final, string ViewStateName)
        {
            //  = (DataTable)ViewState[ViewStateName];
            DataTable dtCurrentTable = new DataTable();
            dtCurrentTable.Columns.Add("ID", typeof(System.String));
            dtCurrentTable.Columns.Add("Email", typeof(System.String));
            if (Final.Count != 0)
            {
                foreach (EmailRequestMailingDetail i in Final)
                {
                    dtCurrentTable.Rows.Add("X", i.Email);
                }
            }

            else
            {
                dtCurrentTable.Rows.Add("X", "");
            }
            ViewState[ViewStateName] = dtCurrentTable;
            return dtCurrentTable;

        }

        protected void ReopenTicket(object sender, EventArgs e)
        {
            string message = "";
            Helper.ReOpenEmailApprovalRequest(Convert.ToInt32(Request.QueryString["id"]),
                Convert.ToInt32(Request.Cookies["UserID"].Value.Trim()),
                ReopenComment.Text, out message);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void EditApprovalCategory(object sender, EventArgs e)
        {
            string type = Request.Cookies["UserType"].Value.Trim();
            string UserName = Request.Cookies["UserName"].Value.Trim();
            int CCReqid = int.Parse(Request.QueryString["ID"]);

            var Result = mdb.GetEmailApprovalRequestsDetailByid(CCReqid);

            if (Result.RequstStatusID == (int)RequestStatus.AssignedByDoctor || Result.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor
                                                                             || Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor)
            {
                if (type == "CallCenterDoctor" && Result.DoctorAssignee == UserName.ToString())
                {
                    if (UpdateLinkButton.Visible == true)
                    {

                        ApprovalCategory.Enabled = true;
                        ApprovalCategory.Visible = true;
                        SaveLinkButton.Visible = true;
                        CancelLinkButton.Visible = true;
                        UpdateLinkButton.Visible = false;
                    }
                    else
                    {
                        UpdateLinkButton.Visible = true;
                    }
                }
            }

            else if (Result.RequstStatusID == (int)RequestStatus.AssignedByAudit ||
                Result.RequstStatusID == (int)RequestStatus.ReOpenedByAudit ||
                Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit)

            {
                if ((type == "CallCenterAuditDoctor" || type == "CallCenterManager") && (Result.AuditAssignee == UserName.ToString()))
                {
                    if (UpdateLinkButton.Visible == true)
                    {
                        ApprovalCategory.Enabled = true;
                        ApprovalCategory.Visible = true;

                        SaveLinkButton.Visible = true;
                        CancelLinkButton.Visible = true;
                        UpdateLinkButton.Visible = false;
                    }
                    else
                    {
                        UpdateLinkButton.Visible = true;
                    }
                }
            }
            else
            {
                UpdateLinkButton.Visible = true;
            }
        }

        protected void SaveApprovalCategory(object sender, EventArgs e)
        {
            string ValidationMessage;
            var NewCateogoryValue = (int)Enum.Parse(typeof(TicketCategory), ApprovalCategory.SelectedItem.ToString());

            var Success = Helper.ChangeEmailRequestApprovalCategory(Convert.ToInt32(Request.QueryString["ID"]),
                Convert.ToInt32(Request.Cookies["UserID"].Value), NewCateogoryValue.ToString(), out ValidationMessage);

            if (Success)
            {
                div_Success.InnerText = ValidationMessage;
                div_Success.Visible = true;
            }
            else
            {
                div_Error.InnerText = ValidationMessage;
                div_Error.Visible = true;
            }
            CancelLinkButton.Visible = false;
            SaveLinkButton.Visible = false;
            UpdateLinkButton.Visible = true;
            ApprovalCategory.Enabled = false;
        }

        protected void CancelApprovalCategory(object sender, EventArgs e)
        {
            ApprovalCategory.Enabled = false;

            SaveLinkButton.Visible = false;
            CancelLinkButton.Visible = false;
            UpdateLinkButton.Visible = true;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void EditTicketType(object sender, EventArgs e)
        {
            string type = Request.Cookies["UserType"].Value.Trim();
            string UserName = Request.Cookies["UserName"].Value.Trim();
            int CCReqid = int.Parse(Request.QueryString["ID"]);

            var Result = mdb.GetEmailApprovalRequestsDetailByid(CCReqid);

            if (Result.RequstStatusID == (int)RequestStatus.AssignedByDoctor || Result.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor
                                                                             || Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor)
            {
                if (type == "CallCenterDoctor" && Result.DoctorAssignee == UserName.ToString())
                {
                    if (EditTicketTypeBTN.Visible == true)
                    {

                        TicketTypeIDID.Enabled = true;
                        TicketTypeIDID.Visible = true;
                        SaveTicketTypeBTN.Visible = true;
                        CancelTicketTypeBTN.Visible = true;
                        EditTicketTypeBTN.Visible = false;
                    }
                    else
                    {
                        EditTicketTypeBTN.Visible = true;
                    }
                }
            }

            else if (Result.RequstStatusID == (int)RequestStatus.AssignedByAudit ||
                Result.RequstStatusID == (int)RequestStatus.ReOpenedByAudit ||
                Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit)

            {
                if ((type == "CallCenterAuditDoctor" || type == "CallCenterManager") && (Result.AuditAssignee == UserName.ToString()))
                {
                    if (EditTicketTypeBTN.Visible == true)
                    {
                        TicketTypeIDID.Enabled = true;
                        TicketTypeIDID.Visible = true;

                        SaveTicketTypeBTN.Visible = true;
                        CancelTicketTypeBTN.Visible = true;
                        EditTicketTypeBTN.Visible = false;
                    }
                    else
                    {
                        EditTicketTypeBTN.Visible = true;
                    }
                }
            }
            else
            {
                EditTicketTypeBTN.Visible = true;
            }
        }

        protected void SaveTicketType(object sender, EventArgs e)
        {
            string ValidationMessage;
            var NewCategoryValue = (int)Enum.Parse(typeof(TicketTypes), TicketTypeIDID.SelectedItem.ToString());

            var Success = Helper.ChangeEmailRequestType(Convert.ToInt32(Request.QueryString["ID"]),
                Convert.ToInt32(Request.Cookies["UserID"].Value), NewCategoryValue.ToString(), out ValidationMessage);

            if (Success)
            {
                div_Success.InnerText = ValidationMessage;
                div_Success.Visible = true;
            }
            else
            {
                div_Error.InnerText = ValidationMessage;
                div_Error.Visible = true;
            }
            CancelTicketTypeBTN.Visible = false;
            SaveTicketTypeBTN.Visible = false;
            EditTicketTypeBTN.Visible = true;
            TicketTypeIDID.Enabled = false;
        }

        protected void CancelTicketType(object sender, EventArgs e)
        {
            TicketTypeIDID.Enabled = false;
            SaveTicketTypeBTN.Visible = false;
            CancelTicketTypeBTN.Visible = false;
            EditTicketTypeBTN.Visible = true;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void EditUserMedicalId(object sender, EventArgs e)
        {
            string type = Request.Cookies["UserType"].Value.Trim();
            string UserName = Request.Cookies["UserName"].Value.Trim();
            int CCReqid = int.Parse(Request.QueryString["ID"]);

            var Result = mdb.GetEmailApprovalRequestsDetailByid(CCReqid);

            if (Result.RequstStatusID == (int)RequestStatus.AssignedByDoctor ||
                Result.RequstStatusID == (int)RequestStatus.AssignedByAudit ||
                Result.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor ||
                Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor

                )
            {
                if (type == "CallCenterDoctor" && Result.DoctorAssignee == UserName.ToString())
                {
                    if (EditButton.Visible == true)
                    {
                        UserMedicalID.Attributes.Remove("readonly");
                        SaveButton.Visible = true;
                        CancelButton.Visible = true;
                        EditButton.Visible = false;
                    }
                    else
                    {
                        EditButton.Visible = true;
                    }
                }
                else if (type == "CallCenterManager" && Result.AuditAssignee == UserName.ToString())
                {
                    if (EditButton.Visible == true)
                    {
                        UserMedicalID.Attributes.Remove("readonly");
                        SaveButton.Visible = true;
                        CancelButton.Visible = true;
                        EditButton.Visible = false;
                    }
                    else
                    {
                        EditButton.Visible = true;
                    }
                }

                else if (type == "CallCenterAuditDoctor" && Result.AuditAssignee == UserName.ToString())
                {
                    if (EditButton.Visible == true)
                    {
                        UserMedicalID.Attributes.Remove("readonly");
                        SaveButton.Visible = true;
                        CancelButton.Visible = true;
                        EditButton.Visible = false;
                    }
                    else
                    {
                        EditButton.Visible = true;

                    }
                }
            }

            else if (Result.RequstStatusID == (int)RequestStatus.AssignedByAudit ||
                Result.RequstStatusID == (int)RequestStatus.ReOpenedByAudit ||
                Result.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit)

            {
                if ((type == "CallCenterAuditDoctor" || type == "CallCenterManager") && (Result.AuditAssignee == UserName.ToString()))
                {
                    if (EditButton.Visible == true)
                    {
                        UserMedicalID.Disabled = false;
                        SaveButton.Visible = true;
                        CancelButton.Visible = true;
                        EditButton.Visible = false;
                    }
                    else
                    {
                        EditButton.Visible = true;
                    }
                }
            }

            else
            {
                EditButton.Visible = false;
            }
        }

        protected void SaveUserMedicalId(object sender, EventArgs e)
        {
            string ValidationMessage;
            var Success = Helper.ChangeEmailRequestMedicalID(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(Request.Cookies["UserID"].Value), Convert.ToInt32(UserMedicalID.Value), out ValidationMessage);

            if (Success)
            {
                div_Success.InnerText = ValidationMessage;
                div_Success.Visible = true;
            }
            else
            {
                div_Error.InnerText = ValidationMessage;
                div_Error.Visible = true;
            }


            SaveButton.Visible = false;
            CancelButton.Visible = false;
            EditButton.Visible = true;
            UserMedicalID.Disabled = true;
        }

        protected void CancelUserMedicalId(object sender, EventArgs e)
        {

            UserMedicalID.Disabled = true;

            SaveButton.Visible = false;
            CancelButton.Visible = false;
            EditButton.Visible = true;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }

     
        protected void IgnoreBtn_ServerClick(object sender, EventArgs e)
        {
            string message;
            bool valid = Helper.IgnoreAndCloseEmailRequestUserName(Convert.ToInt32(Request.QueryString["ID"]),
                Request.Cookies["UserName"].Value.Trim(), out message);
            if (!valid)
            {
                div_Error.Visible = true;
                div_Error.InnerHtml = message;
                //CallApi auto = new CallApi();

            }
            else
            {
                CallcentereMailRequest getrequest = new CallcentereMailRequest();
                EmailApprovalRequest request = getrequest.GetEmailApprovalRequestsDetailByid(Convert.ToInt32(Request.QueryString["ID"]));

                //CallApi.EmailTracking(request, true);
                UpdateEmailAsRead.AutoGeneratedEmailTracking(request.AutoGeneratedEmailID, request.TicketTypeID, true);

                //div_Success.Visible = true;
                div_Success.InnerHtml = message;

                Page.Response.Redirect("UnAssignedEmailApprovals.aspx", true);

            }

        }

        protected void Release_ServerClick(object sender, EventArgs e)
        {
           Helper.ReleaseRequest(Request.Cookies["UserName"].Value.Trim(), Request.Cookies["UserID"].Value.Trim(), Convert.ToInt32(Request.QueryString["ID"]));
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}