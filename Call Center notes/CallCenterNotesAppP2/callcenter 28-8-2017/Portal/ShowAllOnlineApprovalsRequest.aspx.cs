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
    public partial class ShowAllOnlineApprovalsRequest : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();
        CallcentereMailRequest helpe = new CallcentereMailRequest();
        PhNetworkEntities db = new PhNetworkEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowAllOnlineApprovals")).Attributes["class"] = "active";
            if (!IsPostBack)
            {


                foreach (OnlineApprovalStatus r in Enum.GetValues(typeof(OnlineApprovalStatus)))
                {
                    ListItem item = new ListItem(Enum.GetName(typeof(OnlineApprovalStatus),(int) r),((int) r).ToString());



                    Status.Items.Add(item);
                }




                ViewState["ViewPageNo"] = 0;
                // Bind(mdb.SearchAllRequests());
                //foreach (OnlineApprovalStatus val in Enum.GetValues(typeof(OnlineApprovalStatus)))
                //{

                //    Status.Items.Add(val.ToString());
                //}
                foreach (RequestType val in Enum.GetValues(typeof(RequestType)))
                {
                    ListItem item = new ListItem(Enum.GetName(typeof(RequestType), (int)val), ((int)val).ToString());

                    Type.Items.Add(item);
                }








            }
            else
            {

            }

        }
        public void Bind(IEnumerable<OnlineApprovalsRequestMode> Data)
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;
            var data = Data.ToList();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();











        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
  

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

            if (MemberNametxt.Text.ToString() !=string.Empty||
         ClientName.Text != string.Empty ||
        ClaimNumber.Text != string.Empty ||

        Diagnose.Text != string.Empty ||

        Status.SelectedItem.Value != string.Empty ||
        RequestCreationTime.Value != string.Empty ||
       CloseTime.Value != string.Empty ||
       IVRNumber.Text != string.Empty ||
       Status.Text != string.Empty ||
       DrugName.Text != string.Empty )

            {
                Bind(SearchOnlineApprovalsRequest());
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter one Value At least')", true);
            }


        }



        public List<OnlineApprovalsRequestMode> SearchOnlineApprovalsRequest()
        {
            List<OnlineApprovalsRequestMode> Filter_DATA_list = new List<OnlineApprovalsRequestMode>();


            //try
            //{
                var FinalResult = db.SP_OnlineApprovalsSearch(MemberNametxt.Text,
       ClientName.Text,
        ClaimNumber.Text,       Diagnose.Text,
           RequestCreationTime.Value,
       CloseTime.Value,
IVRNumber.Text, Type.SelectedValue
      , Status.SelectedItem.Value
       
       , DrugName.Text
       ).ToList();
                //MailSubject
                if (FinalResult.Any())
                {

                    foreach (var item in FinalResult)
                    {
                        OnlineApprovalsRequestMode Search_Filter = new OnlineApprovalsRequestMode()
                        {
                        MedicalID=item.MedicalID,
                         IVRNumber=item.IVRNumber,
                          CallCenterAssignee=item.CallCenterAssignee,
                           ClaimDate=item.ClaimDate
                           , ClaimNumber=item.ClaimNumber,
                            ClientName= item.ClientName, 
                             CloseTime= item.CloseTime,
                              CoInsurancePercentage= item.CoInsurancePercentage ,
                               Diagnose= item.Diagnose,
                                IsDeleted= item.IsDeleted,
                                 MemberName= item.MemberName,
                                  MobileNumber= item.MobileNumber,
                                   Notes= item.Notes, 
                                     RequestCreationTime= item.RequestCreationTime,
                                      RequestID= item.RequestID
                                      , RequestStatus= item.RequestStatusID.ToString(),
                                       RequestType= item.RequestTypeID.ToString(),
                                        Provider= item.ProviderID.ToString()


                        };

                        Filter_DATA_list.Add(Search_Filter);
                    }
                    return Filter_DATA_list;
                }
                else
                {
                    return new List<OnlineApprovalsRequestMode>();
                }

            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandlerConstants.CreateException(ex, "ShowAllMailRequest.cs", "getsearch", "CallCenterSystemApp");

            //    return Filter_DATA_list;
            //}



        }


      
    }
}
