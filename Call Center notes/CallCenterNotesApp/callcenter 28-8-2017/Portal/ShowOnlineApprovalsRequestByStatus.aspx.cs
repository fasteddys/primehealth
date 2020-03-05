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
    public partial class ShowOnlineApprovalsRequestByStatus : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();
        CallcentereMailRequest helpe = new CallcentereMailRequest();
        PhNetworkEntities db = new PhNetworkEntities();
        string type;
        string UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
             type = Request.Cookies["UserType"].Value.Trim();
             UserName = Request.Cookies["UserName"].Value.Trim();
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            if (id == 1)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("NewOnline")).Attributes["class"] = "active";

                Bind(NewOnlineApprovalsRequest());
            }
            else if(id==2)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("MyOnline")).Attributes["class"] = "active";

                Bind(MyAssignedOnlineApprovalsRequest());
                
            }
            else if (id == 3)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ApprovedOnlineApprovals")).Attributes["class"] = "active";

                Bind(ApprovedOnlineApprovalsRequest());

            }
            else if (id == 4)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("RejectedOnlineApprovals")).Attributes["class"] = "active";

                Bind(RejectedOnlineApprovalsRequest());

            }
            else if (id == 5)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("TerminatedOnlineApprovals")).Attributes["class"] = "active";

                Bind(TerminatedOnlineApprovalsRequest());

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

       
            
          


        }



        public List<OnlineApprovalsRequestMode> NewOnlineApprovalsRequest()
        {
            List<OnlineApprovalsRequestMode> Filter_DATA_list = new List<OnlineApprovalsRequestMode>();


            //try
            //{
                var FinalResult = db.SP_OnlineApprovals_RequestsByStatus((int)OnlineApprovalStatus.New).ToList();
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
        public List<OnlineApprovalsRequestMode> MyOnlineApprovalsRequest()
        {
            List<OnlineApprovalsRequestMode> Filter_DATA_list = new List<OnlineApprovalsRequestMode>();


            //try
            //{
            var FinalResult = db.SP_MyOnlineApprovals_RequestsByStatus((int)OnlineApprovalStatus.AssignedByCallCenter, UserName).ToList();
            FinalResult.AddRange( db.SP_MyOnlineApprovals_RequestsByStatus((int)OnlineApprovalStatus.PendingCallCenterAction, UserName).ToList());
            FinalResult.OrderBy(x => x.RequestID);
            //MailSubject
            if (FinalResult.Any())
            {

                foreach (var item in FinalResult)
                {
                    OnlineApprovalsRequestMode Search_Filter = new OnlineApprovalsRequestMode()
                    {
                        MedicalID = item.MedicalID,
                        IVRNumber = item.IVRNumber,
                        CallCenterAssignee = item.CallCenterAssignee,
                        ClaimDate = item.ClaimDate
                       ,
                        ClaimNumber = item.ClaimNumber,
                        ClientName = item.ClientName,
                        CloseTime = item.CloseTime,
                        CoInsurancePercentage = item.CoInsurancePercentage,
                        Diagnose = item.Diagnose,
                        IsDeleted = item.IsDeleted,
                        MemberName = item.MemberName,
                        MobileNumber = item.MobileNumber,
                        Notes = item.Notes,
                        RequestCreationTime = item.RequestCreationTime,
                        RequestID = item.RequestID
                                  ,
                        RequestStatus = item.RequestStatusID.ToString(),
                        RequestType = item.RequestTypeID.ToString(),
                        Provider = item.ProviderID.ToString()


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
        public List<OnlineApprovalsRequestMode> MyAssignedOnlineApprovalsRequest()
        {
            List<OnlineApprovalsRequestMode> Filter_DATA_list = new List<OnlineApprovalsRequestMode>();


            //try
            //{
            var FinalResult = db.SP_MyAssignedOnlineApprovalsReuests( UserName).ToList();
         
            //MailSubject
            if (FinalResult.Any())
            {

                foreach (var item in FinalResult)
                {
                    OnlineApprovalsRequestMode Search_Filter = new OnlineApprovalsRequestMode()
                    {
                        MedicalID = item.MedicalID,
                        IVRNumber = item.IVRNumber,
                        CallCenterAssignee = item.CallCenterAssignee,
                        ClaimDate = item.ClaimDate
                       ,
                        ClaimNumber = item.ClaimNumber,
                        ClientName = item.ClientName,
                        CloseTime = item.CloseTime,
                        CoInsurancePercentage = item.CoInsurancePercentage,
                        Diagnose = item.Diagnose,
                        IsDeleted = item.IsDeleted,
                        MemberName = item.MemberName,
                        MobileNumber = item.MobileNumber,
                        Notes = item.Notes,
                        RequestCreationTime = item.RequestCreationTime,
                        RequestID = item.RequestID
                                  ,
                        RequestStatus = item.RequestStatusID.ToString(),
                        RequestType = item.RequestTypeID.ToString(),
                        Provider = item.ProviderID.ToString()


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
        public List<OnlineApprovalsRequestMode> ApprovedOnlineApprovalsRequest()
        {
            List<OnlineApprovalsRequestMode> Filter_DATA_list = new List<OnlineApprovalsRequestMode>();


            //try
            //{
            var FinalResult = db.SP_MyOnlineApprovals_RequestsByStatus((int)OnlineApprovalStatus.Approved, UserName).ToList();
            //MailSubject
            if (FinalResult.Any())
            {

                foreach (var item in FinalResult)
                {
                    OnlineApprovalsRequestMode Search_Filter = new OnlineApprovalsRequestMode()
                    {
                        MedicalID = item.MedicalID,
                        IVRNumber = item.IVRNumber,
                        CallCenterAssignee = item.CallCenterAssignee,
                        ClaimDate = item.ClaimDate
                       ,
                        ClaimNumber = item.ClaimNumber,
                        ClientName = item.ClientName,
                        CloseTime = item.CloseTime,
                        CoInsurancePercentage = item.CoInsurancePercentage,
                        Diagnose = item.Diagnose,
                        IsDeleted = item.IsDeleted,
                        MemberName = item.MemberName,
                        MobileNumber = item.MobileNumber,
                        Notes = item.Notes,
                        RequestCreationTime = item.RequestCreationTime,
                        RequestID = item.RequestID
                                  ,
                        RequestStatus = item.RequestStatusID.ToString(),
                        RequestType = item.RequestTypeID.ToString(),
                        Provider = item.ProviderID.ToString()


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

        public List<OnlineApprovalsRequestMode> RejectedOnlineApprovalsRequest()
        {
            List<OnlineApprovalsRequestMode> Filter_DATA_list = new List<OnlineApprovalsRequestMode>();


            //try
            //{
            var FinalResult = db.SP_MyOnlineApprovals_RequestsByStatus((int)OnlineApprovalStatus.Rejected, UserName).ToList();
            //MailSubject
            if (FinalResult.Any())
            {

                foreach (var item in FinalResult)
                {
                    OnlineApprovalsRequestMode Search_Filter = new OnlineApprovalsRequestMode()
                    {
                        MedicalID = item.MedicalID,
                        IVRNumber = item.IVRNumber,
                        CallCenterAssignee = item.CallCenterAssignee,
                        ClaimDate = item.ClaimDate
                       ,
                        ClaimNumber = item.ClaimNumber,
                        ClientName = item.ClientName,
                        CloseTime = item.CloseTime,
                        CoInsurancePercentage = item.CoInsurancePercentage,
                        Diagnose = item.Diagnose,
                        IsDeleted = item.IsDeleted,
                        MemberName = item.MemberName,
                        MobileNumber = item.MobileNumber,
                        Notes = item.Notes,
                        RequestCreationTime = item.RequestCreationTime,
                        RequestID = item.RequestID
                                  ,
                        RequestStatus = item.RequestStatusID.ToString(),
                        RequestType = item.RequestTypeID.ToString(),
                        Provider = item.ProviderID.ToString()


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

        public List<OnlineApprovalsRequestMode> TerminatedOnlineApprovalsRequest()
        {
            List<OnlineApprovalsRequestMode> Filter_DATA_list = new List<OnlineApprovalsRequestMode>();


            //try
            //{
            var FinalResult = db.SP_MyOnlineApprovals_RequestsByStatus((int)OnlineApprovalStatus.Terminated, UserName).ToList();
            //MailSubject
            if (FinalResult.Any())
            {

                foreach (var item in FinalResult)
                {
                    OnlineApprovalsRequestMode Search_Filter = new OnlineApprovalsRequestMode()
                    {
                        MedicalID = item.MedicalID,
                        IVRNumber = item.IVRNumber,
                        CallCenterAssignee = item.CallCenterAssignee,
                        ClaimDate = item.ClaimDate
                       ,
                        ClaimNumber = item.ClaimNumber,
                        ClientName = item.ClientName,
                        CloseTime = item.CloseTime,
                        CoInsurancePercentage = item.CoInsurancePercentage,
                        Diagnose = item.Diagnose,
                        IsDeleted = item.IsDeleted,
                        MemberName = item.MemberName,
                        MobileNumber = item.MobileNumber,
                        Notes = item.Notes,
                        RequestCreationTime = item.RequestCreationTime,
                        RequestID = item.RequestID
                                  ,
                        RequestStatus = item.RequestStatusID.ToString(),
                        RequestType = item.RequestTypeID.ToString(),
                        Provider = item.ProviderID.ToString(),
                        
                       


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
