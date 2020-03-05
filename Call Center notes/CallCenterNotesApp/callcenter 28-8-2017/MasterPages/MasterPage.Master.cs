using CallCenterNotesApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CallCenterNotesApp.Enums.Enums;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Configuration;
using System.IO;
using System.Data;
using System.Globalization;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.MasterPages
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                //if (Request.Browser.Type.Contains("Chrome"))
                //{
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "cle", "windows.history.clear", true);

                    Helpers Helpers = new Helpers();
                    PhNetworkEntities db = new PhNetworkEntities();

                    CallcentereMailRequest Obj = new CallcentereMailRequest();
                    string Username = Request.Cookies["UserName"].Value.Trim();
                    AllRequestsLabel.InnerHtml = Helpers.GetAllRequestCount().ToString();
                    MyAssigendLabel.InnerHtml = Helpers.GetAssignedTicketByUserNameCount(Username).ToString();
                    PendingAssignLabel.InnerHtml = Helpers.GetUnAssignedRequestsByUserNameCount(Username).ToString();
                    ClosedRequestLabel.InnerHtml = Helpers.GetAllClosedRequestOrByUserNameCount(Username).ToString();
                    MobileRequestCount.InnerHtml = Helpers.GetPendingMobileRequestsCount().ToString();
                    IgnoredLabel.InnerHtml = Helpers.GetIgnoredRequestsByUserNameCount(Username).ToString();

                    PendingOperatorLable.InnerHtml = Helpers.GetAllPendingOperator().ToString();
                    PendingDoctorLable.InnerHtml = Helpers.GetAllPendingDoctors().ToString();
                    PendingAuditLable.InnerHtml = Helpers.GetAllPendingAudit().ToString();
                    AssignedOperatorLable.InnerHtml = Helpers.GetAllAssignedOperator().ToString();
                    AssignedDoctorsLable.InnerHtml = Helpers.GetAllAssignedDoctor().ToString();
                    AssignedAuditLable.InnerHtml = Helpers.GetAllAssignedAudit().ToString();

                    //ShowAllOnlineApprovals.InnerHtml = Helpers.ShowAllNewOnlineApprovalsCount().ToString();
                    //NewOnlineLable.InnerHtml = Helpers.ShowNewOnlineApprovals((int)OnlineApprovalStatus.New).ToString();
                    //MyOnlineLable.InnerHtml = Helpers.ShowMyAssignedOnlineApprovals( Username).ToString();
                    //RejectedOnlineApprovalsLable.InnerHtml = Helpers.ShowMyOnlineApprovals((int)OnlineApprovalStatus.Rejected, Username).ToString();
                    //TerminatedOnlineApprovalsLable.InnerHtml = Helpers.ShowMyOnlineApprovals((int)OnlineApprovalStatus.Terminated, Username).ToString();
                    //ApprovedOnlineApprovalsLable.InnerHtml = Helpers.ShowMyOnlineApprovals((int)OnlineApprovalStatus.Approved, Username).ToString();
                    List<int> GroupIDs = db.EmailApprovalsGroup_User.Where(x => x.UserName == Username).Select(x => x.GroupID).ToList();
                    List<EmailApprovalsGroup> ListGroupName = new List<EmailApprovalsGroup>();
                    foreach (var item in GroupIDs)
                    {
                        Groups.Visible = true;

                        EmailApprovalsGroup Group = new EmailApprovalsGroup();
                        Group = db.EmailApprovalsGroups.Where(x => x.Id == item).FirstOrDefault();
                        ListGroupName.Add(Group);
                    }

                    GroupList.DataSource = ListGroupName;
                    GroupList.DataBind();

                    // AllOlineApprovalsCount.InnerText = Helpers.AllOnlieApprovalsCount().ToString();

                    ActivityLogReportDiv.Visible = false;
                    AlertReportDiv.Visible = false;
                    if (!IsPostBack)
                    {
                        string name = Request.Cookies["UserName"].Value.Trim();
                        string type = Request.Cookies["UserType"].Value.Trim();
                        UserNameLbl.InnerHtml = name;
                        UserNameLbl2.InnerHtml = name;
                        imagepath.Src = "../assets/img/images.png";
                        AddCompanyNote.Visible = false;
                        ViewNotes.Visible = false;
                        ShowAllApprovals.Visible = false;
                        ShowClientData.Visible = false;
                        AddNewCCEmailRequestID.Visible = false;
                        ShowAllFeedback.Visible = false;
                        ProviderDashboard.Visible = false;
                        AddNewDoctor.Visible = false;
                        AddNewHospital.Visible = false;
                        AddNewPharm.Visible = false;
                        AddNewLab.Visible = false;
                        AddNewOpticalShop.Visible = false;
                        AddNewLocation.Visible = false;
                        AddNewSubLocation.Visible = false;
                        AddNewCategory.Visible = false;
                        CcEmailRequestShowClosedReqID.Visible = false;
                        ShowAllMailRequest.Visible = false;
                        ShowMyAssigned.Visible = false;
                        UnAssignedEmailApprovals.Visible = false;
                        ShowClosedEmailApprovals.Visible = false;
                        ShowIgnoredApprovals.Visible = false;
                        AddUser.Visible = false;
                        ShowAllUsers.Visible = false;
                        LiChangeUserTypeForManager.Visible = false;
                        FedLAbel.Visible = false;
                        ProvLabel.Visible = false;
                        LocLabel.Visible = false;
                        CatLabel.Visible = false;
                        PendingOperator.Visible = false;
                        AssignedDoctors.Visible = false;
                        AssignedOperator.Visible = false;
                        AssignedAudit.Visible = false;
                        PendingDoctor.Visible = false;
                        PendingAudit.Visible = false;
                        NoteHeader.Visible = false;
                        MobileRequestHeader.Visible = false;
                        MailRequestsHeader.Visible = false;

                        //TerminatedOnlineApprovals.Visible = false;
                        //RejectedOnlineApprovals.Visible = false;
                        //NewOnline.Visible = false;
                        //MyOnline.Visible = false;
                        //ShowAllOnlineApprovals.Visible = false;

                        //ApprovedOnlineApprovals.Visible = false;

                        //OnlineApprovalsSection.Visible = false;

                        SearchForm.Visible = false;


                        if (type == "Admin")
                        {
                            AddUser.Visible = true;
                            ShowAllUsers.Visible = true;
                        }

                        else if (type == "CallCenterManager")
                        {
                            AddCompanyNote.Visible = true;
                            ViewNotes.Visible = true;
                            ShowAllApprovals.Visible = true;
                            ShowClientData.Visible = true;
                            ShowAllMailRequest.Visible = true;
                            ShowMyAssigned.Visible = true;
                            UnAssignedEmailApprovals.Visible = true;
                            ShowClosedEmailApprovals.Visible = true;
                            LiChangeUserTypeForManager.Visible = true;
                            ShowIgnoredApprovals.Visible = true;
                            PendingOperator.Visible = true;
                            PendingDoctor.Visible = true;
                            PendingAudit.Visible = true;
                            NoteHeader.Visible = false;
                            AssignedOperator.Visible = true;
                            AssignedDoctors.Visible = true;
                            AssignedAudit.Visible = true;
                            ActivityLogReportDiv.Visible = true;
                            AlertReportDiv.Visible = true;
                            //TerminatedOnlineApprovals.Visible = true;
                            //RejectedOnlineApprovals.Visible = true;
                            //NewOnline.Visible = true;
                            //MyOnline.Visible = true;
                            //ShowAllOnlineApprovals.Visible = true;
                            //ApprovedOnlineApprovals.Visible = true;
                            //OnlineApprovalsSection.Visible = true;
                            NoteHeader.Visible = true;
                            MobileRequestHeader.Visible = true;
                            MailRequestsHeader.Visible = true;
                            SearchForm.Visible = true;


                        }

                        else if (type == "DirectorUser")
                        {
                            AddCompanyNote.Visible = true;
                            ProviderDashboard.Visible = true;
                            ShowAllApprovals.Visible = true;
                            ShowClientData.Visible = true;
                            ShowAllFeedback.Visible = true;
                            AddNewCCEmailRequestID.Visible = false;
                            CcEmailRequestShowClosedReqID.Visible = false;
                            FedLAbel.Visible = true;
                            ProvLabel.Visible = false;
                            LocLabel.Visible = false;
                            CatLabel.Visible = false;
                            ShowAllMailRequest.Visible = true;
                            PendingOperator.Visible = true;
                            PendingDoctor.Visible = true;
                            PendingAudit.Visible = true;
                            NoteHeader.Visible = false;
                            AssignedOperator.Visible = true;
                            AssignedDoctors.Visible = true;
                            AssignedAudit.Visible = true;
                            ShowClosedEmailApprovals.Visible = true;
                            ShowIgnoredApprovals.Visible = true;
                            ActivityLogReportDiv.Visible = true;
                            AlertReportDiv.Visible = true;
                            NoteHeader.Visible = true;
                            MobileRequestHeader.Visible = true;
                            MailRequestsHeader.Visible = true;
                            SearchForm.Visible = true;

                        }

                        else if (type == "CallCenterUser")
                        {
                            ViewNotes.Visible = true;
                            ShowAllApprovals.Visible = true;
                            ShowClientData.Visible = true;
                            AddNewCCEmailRequestID.Visible = true;
                            ShowAllMailRequest.Visible = true;
                            ShowMyAssigned.Visible = true;
                            UnAssignedEmailApprovals.Visible = true;
                            ShowClosedEmailApprovals.Visible = true;
                            ShowIgnoredApprovals.Visible = true;
                            NoteHeader.Visible = true;
                            MobileRequestHeader.Visible = true;
                            MailRequestsHeader.Visible = true;
                            SearchForm.Visible = true;

                        }
                        else if (type == "CallCenterAuditDoctor")
                        {
                            ViewNotes.Visible = true;
                            ShowAllApprovals.Visible = true;
                            ShowClientData.Visible = true;
                            ShowAllMailRequest.Visible = true;
                            ShowMyAssigned.Visible = true;
                            UnAssignedEmailApprovals.Visible = true;
                            ShowClosedEmailApprovals.Visible = true;
                            ShowIgnoredApprovals.Visible = true;

                            //TerminatedOnlineApprovals.Visible = true;
                            //RejectedOnlineApprovals.Visible = true;
                            //NewOnline.Visible = true;
                            //MyOnline.Visible = true;
                            //ShowAllOnlineApprovals.Visible = true;
                            //ApprovedOnlineApprovals.Visible = true;
                            //OnlineApprovalsSection.Visible = true;
                            NoteHeader.Visible = true;
                            MobileRequestHeader.Visible = true;
                            MailRequestsHeader.Visible = true;
                            SearchForm.Visible = true;

                        }


                        else if (type == "CallCenterDoctor")
                        {
                            ViewNotes.Visible = true;
                            ShowAllApprovals.Visible = true;
                            ShowClientData.Visible = true;
                            ShowAllMailRequest.Visible = true;
                            ShowMyAssigned.Visible = true;
                            UnAssignedEmailApprovals.Visible = true;
                            ShowClosedEmailApprovals.Visible = true;
                            ShowIgnoredApprovals.Visible = true;

                            //TerminatedOnlineApprovals.Visible = true;
                            //RejectedOnlineApprovals.Visible = true;
                            //NewOnline.Visible = true;
                            //MyOnline.Visible = true;
                            //ShowAllOnlineApprovals.Visible = true;
                            //ApprovedOnlineApprovals.Visible = true;
                            //OnlineApprovalsSection.Visible = true;
                            NoteHeader.Visible = true;
                            MobileRequestHeader.Visible = true;
                            MailRequestsHeader.Visible = true;
                            SearchForm.Visible = true;


                        }
                        else if (type == "ProviderUser")
                        {
                            DeleteOpticalBalack.Visible = true;
                            DeleteLabBalack.Visible = true;
                            DeletePharmsBalack.Visible = true;
                            DeleteHospitalBalack.Visible = true;
                            DeleteDoctorBalack.Visible = true;
                            AddBalackOptical.Visible = true;
                            AddLabBalack.Visible = true;
                            AddBalackOfPharm.Visible = true;
                            AddHospitalBalack.Visible = true;
                            AddBalackDoctor.Visible = true;
                            ShowAllOpticals.Visible = true;
                            showAllHospitals.Visible = true;
                            ShowAllDoctors.Visible = true;
                            ShowAllPharms.Visible = true;
                            ShowAllLabs.Visible = true;
                            ProviderDashboard.Visible = true;
                            AddNewDoctor.Visible = true;
                            AddNewHospital.Visible = true;
                            AddNewPharm.Visible = true;
                            AddNewLab.Visible = true;
                            AddNewOpticalShop.Visible = true;
                            AddNewLocation.Visible = true;
                            AddNewSubLocation.Visible = true;
                            AddNewCategory.Visible = true;
                            AddNewCCEmailRequestID.Visible = false;
                            CcEmailRequestShowClosedReqID.Visible = false;
                            FedLAbel.Visible = false;
                            ProvLabel.Visible = true;
                            LocLabel.Visible = true;
                            CatLabel.Visible = true;

                        }
                        else if (type == "ShowGroupsUser")
                        {

                        }

                    }
                    else
                    {

                    }
                //}
                //else
                //{
                //    Helpers helper = new Helpers();

                //    helper.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.FalseBrowserLogin,Convert.ToInt32(Request.Cookies["UserID"].Value.Trim().ToString()), Request.Cookies["UserName"].Value.Trim().ToString(), "Not Logged In", "Tried To Login From " + Request.Browser.Type.ToString() + " ");
                //    Response.Redirect("~/Portal/Login.aspx");
                //}

            }
            catch (Exception ex)
            {
                Response.Redirect("~/Portal/Login.aspx");
            }
        }

        protected void ChangePassword_ServerClick(object sender, EventArgs e)
        {



            var OldPassword = Request.Cookies["OldPasswordCookie"].Value;
            var NewPassword = Request.Cookies["MyNewPasswordCookie"].Value;
            var UserName = Request.Cookies["UserName"].Value;
            string messege;
            Authorization auth = new Authorization();
            if (!auth.ChangePassword(UserName, OldPassword, NewPassword, out messege))
            {
                ViewAlert.Visible = true;
                LableMessage.InnerHtml = messege;
            }
            else
            {
                ViewAlert.Visible = true;
                Request.Cookies["IsFirstLogin"].Value = "False";
                LableMessage.InnerHtml = messege;
            }
        }

        protected void ChangePassworditemClick(object sender, EventArgs e)
        {

        }
        protected void ClosedClick(object sender, EventArgs e)
        {
            ViewAlert.Visible = false;
        }

        protected void LogOut_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Helpers help = new Helpers();
                string mac = help.GetMacAdress();

                help.UpdateLoginAndLogOut(Convert.ToInt32(Request.Cookies["UserID"].Value.Trim()), (int)LogType.LogOut, mac);
                help.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.LogOut,Convert.ToInt32( Request.Cookies["UserID"].Value.Trim()), Request.Cookies["UserName"].Value.Trim().ToString(), mac, "" , "Logout");
            }
            catch { }

            //Helpers.DeleteUserActivationData(Request.Cookies["UserName"].Value.Trim());
            Request.Cookies.Clear();

            Response.Redirect("Login.aspx");
        }



        protected void BtnGenerateActivityReport(object sender, EventArgs e)
        {
            var From = ActivityFrom.Value;
            var To = ActivityTo.Value;
            DateTime DateFrom = DateTime.ParseExact(From, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime DateTo = DateTime.ParseExact(To, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
      

            Helpers helper = new Helpers();



            DataTable dtOperator = new DataTable();
            DataTable dtDoctor = new DataTable();
            DataTable dtAudit = new DataTable();

            dtOperator.Columns.Add("Operator Name");
            dtOperator.Columns.Add("Number Of Tickets");
            dtOperator.Columns.Add("Number Of Manual Tickets");
            dtOperator.Columns.Add("Average Time");

            dtDoctor.Columns.Add("Doctor Name");
            dtDoctor.Columns.Add("Number Of Inpatient Req");
            dtDoctor.Columns.Add("Inpatient Avg Time");
            dtDoctor.Columns.Add("Number Of Outpatient Req");
            dtDoctor.Columns.Add("Outpatient Avg Time");
            dtDoctor.Columns.Add("Number Of Medication Req");
            dtDoctor.Columns.Add("Medication Avg Time");
            dtDoctor.Columns.Add("Total Number Of Req");
            dtDoctor.Columns.Add("Total Average Time");

            dtAudit.Columns.Add("Audit Dr. Name");
            dtAudit.Columns.Add("Number Of Inpatient Req");
            dtAudit.Columns.Add("Inpatient Avg Time");
            dtAudit.Columns.Add("Number Of Outpatient Req");
            dtAudit.Columns.Add("Outpatient Avg Time");
            dtAudit.Columns.Add("Number Of Medication Req");
            dtAudit.Columns.Add("Medication Avg Time");
            dtAudit.Columns.Add("Total Number Of Req");
            dtAudit.Columns.Add("Total Average time");


            foreach (var item in helper.GetActivityRportByDateRange(DateFrom, DateTo).OperatorsActivityLog)
            {
                dtOperator.Rows.Add(item.OperatorAsignee, item.NumberOfTickets, item.NumberOfManualTickets, item.AverageTicketTime);
            }
            foreach (var item in helper.GetActivityRportByDateRange(DateFrom, DateTo).DoctorsActivityLog)
            {
               
                //var CategoryName = (TicketCategory)Enum.Parse(typeof(TicketCategory), item.ApprovalCategoryID.ToString());
                dtDoctor.Rows.Add(item.DoctorAuditName, item.TotalNumberOfInpatientRequests,item.AvgInpatientDuration, item.TotalNumberOfOutPatientRequests,item.AvgOutpatientDuration, item.TotalNumberOfMedicationRequests,item.AvgMedicationDuration,item.TotalNumberOfTickets, item.TotalAvgDuration);
            }
            foreach (var item in helper.GetActivityRportByDateRange(DateFrom, DateTo).AuditActivityLog)
            {

                //var CategoryName = (TicketCategory)Enum.Parse(typeof(TicketCategory), item.ApprovalCategoryID.ToString());
                dtAudit.Rows.Add(item.DoctorAuditName, item.TotalNumberOfInpatientRequests, item.AvgInpatientDuration, item.TotalNumberOfOutPatientRequests, item.AvgOutpatientDuration, item.TotalNumberOfMedicationRequests, item.AvgMedicationDuration, item.TotalNumberOfTickets, item.TotalAvgDuration);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtOperator, "Operators");
                wb.Worksheets.Add(dtDoctor, "Doctors");
                wb.Worksheets.Add(dtAudit, "Audit");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=CallCeneterActivityLog.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void BtnGenerateReport(object sender, EventArgs e)
        {
            var From = ReportFrom.Value;
            var To = ReportTo.Value;
            DateTime DateFrom = DateTime.ParseExact(From, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime DateTo = DateTime.ParseExact(To, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);



            Helpers helper = new Helpers();



            DataTable dtOperator = new DataTable();
        

            dtOperator.Columns.Add("On Time Tickets");
            dtOperator.Columns.Add("First Warning Tickets");
            dtOperator.Columns.Add("Second Warning Tickets");
            dtOperator.Columns.Add("Third Warning Tickets");
            dtOperator.Columns.Add("Total Number Tickets");
            dtOperator.Columns.Add("Number Of Sp Tickets Violates Time");
            dtOperator.Columns.Add("Number Of Normal Tickets Violates Time");



            var ReportData = helper.GetRport(DateFrom, DateTo);




                dtOperator.Rows.Add(ReportData.OnTimeTickets, 
                    ReportData.FirstWarningTickets,
                    ReportData.SecondWarningTickets,
                    ReportData.ThirdWarningTickets,
                    ReportData.OnTimeTickets+
                    ReportData.FirstWarningTickets+
                    ReportData.SecondWarningTickets+
                    ReportData.ThirdWarningTickets,
                    ReportData.SpTicketsViolatesTime,
                    ReportData.NormalTicketsViolatesTime
                    );
            
 
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtOperator, "Report");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=CallCeneterAlertLog.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void SearchInEmailRequests(object sender, EventArgs e) {
            string word = SearchText.Value;
            Response.Redirect("CcEmailRequestSearchDetailsRequest.aspx?Id=" + word);
          //  word = String.Format("{0}", Request.Form["txt_search"]);


        }
    }

}

