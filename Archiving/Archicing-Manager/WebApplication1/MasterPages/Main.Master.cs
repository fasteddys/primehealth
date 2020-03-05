using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.MasterPages
{
    public partial class Main : System.Web.UI.MasterPage
    {
        Requests mdb = new Requests();
        subRequests mdd = new subRequests();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    //Li1.Visible = false;
                    if (Request.Cookies["nameark"].Value.Trim() != null)
                    {
                        string name = Request.Cookies["nameark"].Value.Trim();
                        string type = Request.Cookies["typeark"].Value.Trim();
                        UserNameLbl.InnerHtml = name;
                        if (type == "Archiving")
                        {

                            
                            dashboardspan.InnerText = mdb.GetDataCount().ToString();
                            subdash.InnerHtml = mdd.GetDataCount_ForArchiving().ToString();        
                            newsub.InnerHtml = mdd.GetDataCountforNewin().ToString();
                            recsub.InnerHtml = mdd.GetDataCountforrecivingforarchiving(name).ToString();
                            pendingsub.InnerHtml = mdd.GetDataCountforpending_Archiving().ToString();
                            confrimedsub.InnerHtml = mdd.GetDataCountforclosedForEnterpriseAdmin().ToString();
                            closedsub.InnerHtml = mdd.GetDataCountforclosedtickets().ToString();
                            arcspan.InnerHtml = mdb.GetDataArchivingForArchiving(name).ToString();
                            pendspan.InnerText = mdb.GetDataPending().ToString();
                            closedspan.InnerHtml = mdb.GetDataClosed().ToString();
                            newspan.InnerHtml = mdb.GetDataNew().ToString();
                            Span155.InnerHtml = mdd.TPA().ToString();
                            rejectedspan.InnerHtml = mdb.GetDataRejected().ToString();
                            submitreqs.InnerHtml = mdb.GetDataSubmitted().ToString();
                            UsersAdminLi.Visible = false;
                            AddRequestLi.Visible = false;
                            BatchMgrLi.Visible = true;
                            claimspan.InnerHtml = mdb.GetDataPendingClaims().ToString();
                            PendingDataEntry.InnerHtml = mdb.GetDataPendingDataEntry().ToString();
                            rejectedsub.InnerHtml = mdd.GetDataCountforRejected().ToString();
                            Li2.Visible = true;
                            Li3.Visible = true;
                            Li5.Visible = false;
                            Rempend.Visible = false;
                            Li7.Visible = false;
                            adminsearch.Visible = true;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            Li8.Visible = false;
                            TPA.Visible = true;
                        }

                        else if (type == "TPAAdmin")
                        {            
                            subdash.InnerText = mdd.GetDataCountTPA().ToString();
                            newsub.InnerHtml = mdd.GetDataCountforNewout().ToString();
                            recsub.InnerHtml = mdd.GetDataCountforrecivingforTPAAdmin().ToString();
                            pendingsub.InnerHtml = mdd.GetDataCountforpending_TPA().ToString();
                            confrimedsub.InnerHtml = mdd.GetDataCountforclosedForTPA().ToString();
                            closedsub.InnerHtml = mdd.GetDataCountforclosedtickets_out().ToString();
                            rejectedsub.InnerHtml = mdd.GetDataCountforRejected().ToString();
                            Span155.InnerHtml = mdd.TPA().ToString();
                            UsersAdminLi.Visible = false;
                            AddRequestLi.Visible = false;
                            BatchMgrLi.Visible = true;
                            AddOutPatient.Visible = false;
                            Li3.Visible = false;
                            Li4.Visible = false;
                            Li5.Visible = false;
                            Rempend.Visible = false;
                            Li7.Visible = false;
                            adminsearch.Visible = false;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            Li8.Visible = false;
                            Li1.Visible = false;
                            search.Visible = false;
                            NewRequestsLi.Visible = false;
                            aaa.Visible = false;
                            pp.Visible = false;
                            Rempend.Visible = false;
                            PendDE.Visible = false;
                            rembsubmit.Visible = false;
                            claimspend.Visible = false;
                            Rejected.Visible = false;
                            closed.Visible = false;
                            AddInPatient.Visible = false;
                            AddRemb.Visible = false;
                            Li2.Visible = false;
                            BatchMgrLi.Visible = false;
                            TPA.Visible = true;
                        }
                        else if (type == "TPA")
                        {
                            subdash.InnerText = mdd.GetDataCountTPA().ToString();
                            newsub.InnerHtml = mdd.GetDataCountforNewout().ToString();
                            recsub.InnerHtml = mdd.GetDataCountforrecivingforTPA(name).ToString();
                            pendingsub.InnerHtml = mdd.GetDataCountforpending_TPA().ToString();
                            confrimedsub.InnerHtml = mdd.GetDataCountforclosedForTPA().ToString();
                            closedsub.InnerHtml = mdd.GetDataCountforclosedtickets_out().ToString();
                            rejectedsub.InnerHtml = mdd.GetDataCountforRejected().ToString();
                            Span155.InnerHtml = mdd.TPA().ToString();
                            UsersAdminLi.Visible = false;
                            AddRequestLi.Visible = false;
                            BatchMgrLi.Visible = true;
                            AddOutPatient.Visible = false;
                            Li3.Visible = false;
                            Li4.Visible = false;
                            Li5.Visible = false;
                            Rempend.Visible = false;
                            Li7.Visible = false;
                            adminsearch.Visible = false;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            Li8.Visible = false;
                            Li1.Visible = false;
                            search.Visible = false;
                            NewRequestsLi.Visible = false;
                            aaa.Visible = false;
                            pp.Visible = false;
                            Rempend.Visible = false;
                            PendDE.Visible = false;
                            rembsubmit.Visible = false;
                            claimspend.Visible = false;
                            Rejected.Visible = false;
                            closed.Visible = false;
                            AddInPatient.Visible = false;
                            AddRemb.Visible = false;
                            Li2.Visible = false;
                            BatchMgrLi.Visible = false;
                            TPA.Visible = true;
                        }


                        else if (type == "User")
                        {
                            UsersAdminLi.Visible = false;
                            BatchMgrLi.Visible = false;
                            NewRequestsLi.Visible = false;
                            AddOutPatient.Visible = false;
                            AddInPatient.Visible = false;
                            AddRemb.Visible = false;
                            Li2.Visible = false;
                            Li3.Visible = false;
                            Li4.Visible = false;
                            aaa.Visible = true;
                            Li5.Visible = false;
                            Li8.Visible = false;
                            Li13.Visible = false;
                            Li9.Visible = false;
                            Li10.Visible = false;
                            Li14.Visible = false;
                            Li11.Visible = false;
                            Li12.Visible = false;
                            Li15.Visible = false;
                            Li16.Visible = false;
                            rembsubmit.Visible = false;
                            Rempend.Visible = false;
                            dashboardspan.InnerText = mdb.GetDataCountForUsers(name).ToString();
                            arcspan.InnerHtml = mdb.GetDataArchivingForUsers(name).ToString();
                            pendspan.InnerText = mdb.GetDataPendingForUsers(name).ToString();
                            closedspan.InnerHtml = mdb.GetDataClosedForusers(name).ToString();
                            rejectedspan.InnerHtml = mdb.GetDataRejectedusers(name).ToString();
                            unassignedspan.InnerHtml = mdb.GetDataUnassigned(name).ToString();
                            claimspan.InnerHtml = mdb.GetDataPendingClaimsforUser(name).ToString();
                            adminsearch.Visible = true;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            PendDE.Visible = false;
                            TPA.Visible = false;


                        }

                        else if (type == "Data Entry")
                        {
                            UsersAdminLi.Visible = false;
                            BatchMgrLi.Visible = false;
                            NewRequestsLi.Visible = false;
                            AddOutPatient.Visible = false;
                            AddInPatient.Visible = false;
                            AddRemb.Visible = false;
                            Li2.Visible = false;
                            Li3.Visible = false;
                            Li4.Visible = false;
                            aaa.Visible = true;
                            Li5.Visible = false;
                            Rempend.Visible = false;
                            dashboardspan.InnerText = mdb.GetDataCountForUsers(name).ToString();
                            subdash.InnerText = mdd.GetDataCountforuser(name).ToString();
                            recsub.InnerHtml = mdd.GetDataCountforrecforUser(name).ToString();
                            arcspan.InnerHtml = mdb.GetDataArchivingForUsers(name).ToString();
                            pendspan.InnerText = mdb.GetDataPendingForUsers(name).ToString();
                            closedspan.InnerHtml = mdb.GetDataClosedForusers(name).ToString();
                            rejectedspan.InnerHtml = mdb.GetDataRejectedusers(name).ToString();
                            unassignedspan.InnerHtml = mdb.GetDataUnassigned(name).ToString();
                            newsub.InnerHtml = mdd.GetDataCountforNewforUser(name).ToString();
                            submitreqs.InnerHtml = mdb.GetDataSubmittedforuser(name).ToString();
                            confrimedsub.InnerHtml = mdd.GetDataCountforclosedforuser(name).ToString();
                            closedsub.InnerHtml = mdd.GetDataCountforclosedticketsforuser(name).ToString();
                            pendingsub.InnerHtml = mdd.GetDataCountforpendingforuser(name).ToString();
                            rejectedsub.InnerHtml = mdd.GetDataCountforRejectedforuser(name).ToString();
                            PendingDataEntry.InnerHtml = mdb.GetDataPendingDataEntry().ToString();
                            claimspan.InnerHtml = mdb.GetDataPendingClaims().ToString();
                            adminsearch.Visible = true;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            TPA.Visible = false;

                        }

                        else if(type=="Remb")
                        {
                            UsersAdminLi.Visible = false;
                            BatchMgrLi.Visible = false;
                            NewRequestsLi.Visible = false;
                            AddOutPatient.Visible = false;
                            AddInPatient.Visible = false;
                            AddRemb.Visible = false;
                            Li2.Visible = false;
                            Li3.Visible = false;
                            Li4.Visible = false;
                            aaa.Visible = true;
                            Li5.Visible = false;
                            Rempend.Visible = false;
                            Li8.Visible = false;
                            Li13.Visible = false;
                            Li9.Visible = false;
                            Li10.Visible = false;
                            Li14.Visible = false;
                            Li11.Visible = false;
                            Li12.Visible = false;
                            Li15.Visible = false;
                            Li16.Visible = false;
                            dashboardspan.InnerText = mdb.GetDataCountForUsers(name).ToString();
                            arcspan.InnerHtml = mdb.GetDataArchivingForUsers(name).ToString();
                            pendspan.InnerText = mdb.GetDataPendingForUsers(name).ToString();
                            closedspan.InnerHtml = mdb.GetDataClosedForusers(name).ToString();
                            rejectedspan.InnerHtml = mdb.GetDataRejectedusers(name).ToString();
                            unassignedspan.InnerHtml = mdb.GetDataUnassigned(name).ToString();
                            submitreqs.InnerHtml = mdb.GetDataSubmittedforuser(name).ToString();
                            PendingDataEntry.InnerHtml = mdb.GetDataPendingDataEntryForUser(name).ToString();
                            adminsearch.Visible = true;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            claimspend.Visible=false;
                            TPA.Visible = false;

                        }

                      
                        else if (type == "Admin")
                        {
                            dashboardspan.InnerText = mdb.GetDataCount().ToString();
                            subdash.InnerHtml = mdd.GetDataCount_ForArchiving().ToString();
                            arcspan.InnerHtml = mdb.GetDataArchivingForArchivingforadmin().ToString();//update
                            pendspan.InnerText = mdb.GetDataPending().ToString();
                            newsub.InnerHtml = mdd.GetDataCountforNewin().ToString();
                            newspan.InnerHtml = mdb.GetDataNew().ToString();
                            AddRequestLi.Visible = false;
                            Li5.Visible = true;
                            Li8.Visible = false;
                            Rempend.Visible = true;
                            rembspan.InnerText = mdb.GetAllReqRembPConfirmCount().ToString();
                            recsub.InnerHtml = mdd.GetDataCountforrecivingforarchiving(name).ToString();
                            pendingsub.InnerHtml = mdd.GetDataCountforpending_Archiving().ToString();
                            closedsub.InnerHtml = mdd.GetDataCountforclosedtickets().ToString();
                            confrimedsub.InnerHtml = mdd.GetDataCountforclosedForEnterpriseAdmin().ToString();
                            closedspan.InnerHtml = mdb.GetDataClosed().ToString();
                            rejectedspan.InnerHtml = mdb.GetDataRejected().ToString();
                            claimspan.InnerHtml = mdb.GetDataPendingClaims().ToString();
                            submitreqs.InnerHtml = mdb.GetDataSubmitted().ToString();
                            PendingDataEntry.InnerHtml = mdb.GetDataPendingDataEntry().ToString();
                            rejectedsub.InnerHtml = mdd.GetDataCountforRejected().ToString();
                            Li7.Visible = false;
                            adminadduser.Visible = true;
                            manageuseradmin.Visible = true;
                            manageuserenterprise.Visible = false;
                            enterpriseadminadduser.Visible = false;
                            adminsearch.Visible = true;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            TPA.Visible = true;
                            Span155.InnerText = mdd.TPA().ToString();
                           
                           

                        }
                        else if (type == "ArcAdmin")
                        {
                            dashboardspan.InnerText = mdb.GetDataCount().ToString();
                            subdash.InnerHtml = mdd.GetDataCount_ForArchiving().ToString(); 
                            AddRequestLi.Visible = false;
                            Li5.Visible = false;
                            UsersAdminLi.Visible = false;
                            Li8.Visible = false;
                            Rempend.Visible = true;
                            recsub.InnerHtml = mdd.GetDataCountforrecivingforarchiving(name).ToString();
                            arcspan.InnerHtml = mdb.GetDataArchivingForArchiving(name).ToString();
                            confrimedsub.InnerHtml = mdd.GetDataCountforclosedForEnterpriseAdmin().ToString();
                            pendingsub.InnerHtml = mdd.GetDataCountforpending_Archiving().ToString();
                            pendspan.InnerText = mdb.GetDataPending().ToString();
                            newsub.InnerHtml = mdd.GetDataCountforNewin().ToString();
                            rembspan.InnerText = mdb.GetAllReqRembPConfirmCount().ToString();
                            closedspan.InnerHtml = mdb.GetDataClosed().ToString();
                            closedsub.InnerHtml = mdd.GetDataCountforclosedtickets().ToString();
                            newspan.InnerHtml = mdb.GetDataNew().ToString();
                            rejectedspan.InnerHtml = mdb.GetDataRejected().ToString();
                            claimspan.InnerHtml = mdb.GetDataPendingClaims().ToString();
                            PendingDataEntry.InnerHtml = mdb.GetDataPendingDataEntry().ToString();
                            submitreqs.InnerHtml = mdb.GetDataSubmitted().ToString();
                            rejectedsub.InnerHtml = mdd.GetDataCountforRejected().ToString();
                            Li7.Visible = false;
                            adminsearch.Visible = true;
                            searchbydate.Visible = false;
                            Li6.Visible = false;
                            TPA.Visible = true;
                            Span155.InnerHtml = mdd.TPA().ToString();
                           
                           

                        }

                        else if (type == "EnterpriseAdmin")
                        {
                            dashboardspan.InnerText = mdb.GetDataCount().ToString();
                            subdash.InnerText = mdd.GetDataCount().ToString();
                            arcspan.InnerHtml = mdb.GetDataArchivingCount().ToString();
                            pendspan.InnerText = mdb.GetDataPending().ToString();
                            confrimedsub.InnerHtml = mdd.GetDataCountforclosedForEnterpriseAdmin().ToString();
                            newsub.InnerHtml = mdd.GetDataCountforNew().ToString();
                            recsub.InnerHtml = mdd.GetDataCountforreciving().ToString();
                            pendingsub.InnerHtml = mdd.GetDataCountforpending().ToString();
                            closedsub.InnerHtml = mdd.GetDataCountforclosedtickets().ToString();
                            newspan.InnerHtml = mdb.GetDataNew().ToString();
                            AddRequestLi.Visible = false;
                            Li5.Visible = false;
                            BatchMgrLi.Visible = true;
                            Li2.Visible = true;
                            Li3.Visible = true;
                            Li4.Visible = false;
                            Rempend.Visible = true;
                            rembspan.InnerText = mdb.GetAllReqRembPConfirmCount().ToString();
                            closedspan.InnerHtml = mdb.GetDataClosed().ToString();
                            rejectedspan.InnerHtml = mdb.GetDataRejected().ToString();
                            claimspan.InnerHtml = mdb.GetDataPendingClaims().ToString();
                            submitreqs.InnerHtml = mdb.GetDataSubmitted().ToString();
                            PendingDataEntry.InnerHtml = mdb.GetDataPendingDataEntry().ToString();
                            rejectedsub.InnerHtml = mdd.GetDataCountforRejected().ToString();
                            Li7.Visible = false;
                            adminadduser.Visible = false;
                            manageuseradmin.Visible = false;
                            manageuserenterprise.Visible = true;
                            enterpriseadminadduser.Visible = true;
                            Li8.Visible = false;
                            AddInPatient.Visible = false;
                            AddOutPatient.Visible = false;
                            AddRemb.Visible = false;
                            TPA.Visible = true;
                            Span155.InnerHtml = mdd.TPA().ToString();

                        }

                    }
                    else
                    {
                        Response.Redirect("../login.aspx");
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("../login.aspx");
            }
        }


        protected void LogOUt_ServerClick(object sender, EventArgs e)
        {
            Response.Cookies["nameark"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["typeark"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["uIDark"].Expires = DateTime.Now.AddDays(-1);
            Accounts ac = new Accounts();
            int id = int.Parse(Request.Cookies["uIDark"].Value.Trim());
            ac.updateOFF(id);
            Response.Redirect("../login.aspx");
        }
    }
}