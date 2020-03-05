using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Drawing;
using CallCenterNotesApp.ModelView;
using static CallCenterNotesApp.Enums.Enums;
using System.Data.SqlClient;
using System.Data;
using CallCenterNotesApp.CustomExceptions;

namespace CallCenterNotesApp.Portal
{
    public partial class ShowAllMailRequestForGroup : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();
        CallcentereMailRequest helpe = new CallcentereMailRequest();
        PhNetworkEntities db = new PhNetworkEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowAllMailRequestForGroup")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
                ViewState["ViewPageNo"] = 0;
                // Bind(mdb.SearchAllRequests());
                foreach (RequestStatus val in Enum.GetValues(typeof(RequestStatus)))
                {
                    Status.Items.Add(val.ToString());
                }

                foreach (TicketCategory val in Enum.GetValues(typeof(TicketCategory)))
                {
                    ApprovalCategory.Items.Add(val.ToString());
                }


                foreach (TicketPriority val in Enum.GetValues(typeof(TicketPriority)))
                {
                    Priority.Items.Add(val.ToString());
                }


                string SPCallCenterSignature = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString();
                string GeneralDefaultSendingMail = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString();
                string fax = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString();
                MailSource.Items.Add(SPCallCenterSignature);
                MailSource.Items.Add(GeneralDefaultSendingMail);
                MailSource.Items.Add(fax);




            }
            else
            {

            }

        }
        public void Bind(IEnumerable<ModelViewTable> Data)
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;
            var data = Data.ToList();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();













        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //Button btn = (Button)sender;
            //ChangePage(btn.Text);
            //ViewState["currentpage"] = btn.Text;

        }
      



        protected void UpdateColors_Click(object sender, EventArgs e)
        {



        }



        protected void Next_Click(object sender, EventArgs e)
        {

        }

        protected void Befor_Click(object sender, EventArgs e)
        {


        }

        protected void Search_Click(object sender, EventArgs e)
        {

            if (MedicalIDtxt.Text.ToString() !=string.Empty||
       DoctorName.Text != string.Empty ||
        AuditName.Text != string.Empty ||

        OperatorName.Text != string.Empty ||

        ProviderName.Text != string.Empty ||
        Status.SelectedItem.Value != string.Empty ||
        Type.SelectedItem.Text != string.Empty ||
        CrationEndDateTo.Value != string.Empty ||
       CreationDateFrom.Value != string.Empty ||
       ApprovalCategory.SelectedValue != string.Empty ||
       Priority.SelectedValue != string.Empty ||
       EmailContent.Text != string.Empty ||
       MailSubject.Text != string.Empty ||
       MailSource.SelectedItem.Value != string.Empty || 
       AttachmentName.Text != string.Empty ||
       recipient.Text != string.Empty)

            {






                Bind(getsearch());
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter one Value At least')", true);
            }


        }



        public List<ModelViewTable> getsearch()
        {
            List<ModelViewTable> Filter_DATA_list = new List<ModelViewTable>();
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;
            //int GroupID = db.EmailApprovalsGroup_User.Where(x => x.UserName == user).First().GroupID;
            int GroupID = Convert.ToInt32(Request.QueryString["ID"]);
            List<string> GroupEmails = db.EmailApprovalsUser_Email.Where(x => x.GroupID == GroupID).Select(x=>x.Email).ToList();

            try
            {

                foreach (var EmailItem in GroupEmails)
                {
                    var FinalResult = db.SP_SearchInMailGroup(MedicalIDtxt.Text,
           DoctorName.Text,
            AuditName.Text,

            OperatorName.Text,

            ProviderName.Text,
            Status.SelectedItem.Value,
            Type.SelectedItem.Text,
            CrationEndDateTo.Value,
           CreationDateFrom.Value,
           ApprovalCategory.SelectedValue, Priority.SelectedValue, EmailContent.Text,
           MailSubject.Text, MailSource.SelectedItem.Value, AttachmentName.Text, recipient.Text, EmailItem).ToList().OrderBy(x=>x.ID);
                    //MailSubject
                    if (FinalResult.Any())
                    {

                        foreach (var item in FinalResult)
                        {
                            ModelViewTable Search_Filter = new ModelViewTable()
                            {
                                ID = item.ID,
                                MedicalID = item.Medical_ID,
                                ProviderName = item.ProviderName,
                                CreatedBy = item.CreatedBy,
                                CreationDate = item.CreationDate,
                                DoctorAssignee = item.DoctorAssignee,
                                AuditAssignee = item.AuditAssignee,
                                RequestTypeName = item.Name,
                                RequestStatusName = item.StatusName,
                                ColorHex = item.ColorHex,
                                Priority = item.Priority,
                                CategoryName = item.CategoryName,
                                OperatorAssignee = item.OperatorAssignee,
                                ClosedTime = item.ClosedTime,
                                MailSubject = item.MailSubject.Length > 40 ? item.MailSubject.Substring(40).ToString() + "..." : item.MailSubject



                            };

                            Filter_DATA_list.Add(Search_Filter);
                        }
                    }
                }
                return Filter_DATA_list;
            
                
             

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "ShowAllMailRequest.cs", "getsearch", "CallCenterSystemApp");

                return Filter_DATA_list;
            }



        }
    }
}
