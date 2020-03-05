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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                   //////////////////// Notification //////////////////
                    int NewArchCounter = mdb.GetAllReqNewArchCounter();
                    spn_NewArchTick.InnerHtml = NewArchCounter.ToString();

                    int ReviewArchCounter = mdb.GetAllReqReviewArchCounter();
                    spn_ReviewArchTick.InnerHtml = ReviewArchCounter.ToString();

                    int PendingArchCounter = mdb.GetAllReqPendingArchCounter();
                    spn_PendingArchTick.InnerHtml = PendingArchCounter.ToString();

                    int ClosedArchCounter = mdb.GetAllReqClosedArchCounter();
                    spn_ClosedArchTick.InnerHtml = ClosedArchCounter.ToString();
                    /////////////////////////////////////////////////////////

                    Li1.Visible = false;
                    AddBatchLi.Visible = false;
                    AddClaimLi.Visible = false;
                    BatchMgrLi.Visible = false;
                    AddRequestArchLi.Visible = false;
                    if (Request.Cookies["nameark"].Value.Trim() != null)
                    {
                        string name = Request.Cookies["nameark"].Value.Trim();
                        string type = Request.Cookies["typeark"].Value.Trim();
                        UserNameLbl.InnerHtml = name;
                        if (type == "DataEntry")
                        {
                            UsersAdminLi.Visible = false;
                            AddRequestLi.Visible = true;
                            BatchMgrLi.Visible = false;
                            Reports.Visible = false;
                            int NewCounter = mdb.GetAllReqNewCounterByDataEntry(name);
                            spn_NewTick.InnerHtml = NewCounter.ToString();
                            int SearchingCounter = mdb.GetAllReqSearchingCounterByDataEntry(name);
                            spn_SearchingTick.InnerHtml = SearchingCounter.ToString();
                            int PMissingCounter = mdb.GetAllReqPMissingCounterByDataEntry(name);
                            spn_PMissingTick.InnerHtml = PMissingCounter.ToString();


                            int PendingQControlCounter = mdb.GetAllReqPQControlCounterByDataEntry(name);
                            spn_PQControlTick.InnerHtml = PendingQControlCounter.ToString();

                            int QControlCounter = mdb.GetAllReqQControlCounterByDataEntry(name);
                            spn_QControlTick.InnerHtml = QControlCounter.ToString();
                           

                            int PConfirmationCounter = mdb.GetAllReqPConfirmationCounterByDataEntry(name);
                            spn_TBAConfirmTick.InnerHtml = PConfirmationCounter.ToString();
                            int AcceptCounter = mdb.GetAllReqAcceptCounterByDataEntry(name);
                            spn_AcceptTick.InnerHtml = AcceptCounter.ToString();
                            int ClosedCounter = mdb.GetAllReqClosedCounterByDataEntry(name);
                            spn_ClosedlTick.InnerHtml = ClosedCounter.ToString();
                            int QualityClosedCounter = mdb.GetAllReqQualityClosedCounter();
                            spn_QualityClosedlTick.InnerHtml = QualityClosedCounter.ToString();

                        }

                        else if (type == "User")
                        {
                            TBADailyReport.Visible = false;
                            UsersAdminLi.Visible = false;
                            BatchMgrLi.Visible = false;
                            AddBatchLi.Visible = false;
                            int NewCounter = mdb.GetAllReqNewCounterByUser(name);
                            spn_NewTick.InnerHtml = NewCounter.ToString();
                            int SearchingCounter = mdb.GetAllReqSearchingCounterByUser(name);
                            spn_SearchingTick.InnerHtml = SearchingCounter.ToString();
                            int PMissingCounter = mdb.GetAllReqPMissingCounterByUser(name);
                            spn_PMissingTick.InnerHtml = PMissingCounter.ToString();

                            int PendingQControlCounter = mdb.GetAllReqPQControlCounterByUser(name);
                            spn_PQControlTick.InnerHtml = PendingQControlCounter.ToString();

                            int QControlCounter = mdb.GetAllReqQControlCounterByUser(name);
                            spn_QControlTick.InnerHtml = QControlCounter.ToString();
                            

                            int PConfirmationCounter = mdb.GetAllReqPConfirmationCounterByUser(name);
                            spn_TBAConfirmTick.InnerHtml = PConfirmationCounter.ToString();
                            int AcceptCounter = mdb.GetAllReqAcceptCounterByUser(name);
                            spn_AcceptTick.InnerHtml = AcceptCounter.ToString();
                            int ClosedCounter = mdb.GetAllReqClosedCounterByUser(name);
                            spn_ClosedlTick.InnerHtml = ClosedCounter.ToString();
                            int QualityClosedCounter = mdb.GetAllReqQualityClosedCounter();
                            spn_QualityClosedlTick.InnerHtml = QualityClosedCounter.ToString();
                            //NewRequestsLi.Visible = false;

                        }

                        else if (type == "TBAUser")
                        {
                            UsersAdminLi.Visible = false;
                            BatchMgrLi.Visible = false;
                            AddBatchLi.Visible = false;
                            NewRequestsLi.Visible = false;
                            AddClaimLi.Visible = false;
                            AddRequestLi.Visible = false;
                            NewRequestsLi.Visible = false;
                            SearchingLi.Visible = false;
                            missingLi.Visible = false;
                            QualityLi.Visible = false;
                            PQualityLi.Visible = false;
                            DataEntry.Visible = false;
                            NewRequestsArchLi.Visible = false;
                            ReviewRequestsArchLi.Visible = false;
                            PendingRequestsArchLi.Visible = false;
                            ClosedRequestsArchLi.Visible = false;
                            QualityClosedRequestsLi.Visible = false;

                            int NewCounter = mdb.GetAllReqNewCounter();
                            spn_NewTick.InnerHtml = NewCounter.ToString();
                            int SearchingCounter = mdb.GetAllReqSearchingCounter();
                            spn_SearchingTick.InnerHtml = SearchingCounter.ToString();
                            int PMissingCounter = mdb.GetAllReqPMissingCounter();
                            spn_PMissingTick.InnerHtml = PMissingCounter.ToString();


                            int PendingQControlCounter = mdb.GetAllReqPQControlCounter();
                            spn_PQControlTick.InnerHtml = PendingQControlCounter.ToString();
                            int QControlCounter = mdb.GetAllReqQControlCounter();
                            spn_QControlTick.InnerHtml = QControlCounter.ToString();


                            int PConfirmationCounter = mdb.GetAllReqPConfirmationCounter();
                            spn_TBAConfirmTick.InnerHtml = PConfirmationCounter.ToString();
                            int AcceptCounter = mdb.GetAllReqAcceptCounter();
                            spn_AcceptTick.InnerHtml = AcceptCounter.ToString();
                            int ClosedCounter = mdb.GetAllReqClosedCounter();
                            spn_ClosedlTick.InnerHtml = ClosedCounter.ToString();
                            



                        }
                        else if (type == "ArchAdmin")
                        {
                            AddRequestArchLi.Visible = true;
                            UsersAdminLi.Visible = false;
                            BatchMgrLi.Visible = false;
                            AddBatchLi.Visible = false;
                            NewRequestsLi.Visible = false;
                            AddClaimLi.Visible = false;
                            AddRequestLi.Visible = false;
                            NewRequestsLi.Visible = false;
                            SearchingLi.Visible = false;
                            missingLi.Visible = false;
                            QualityLi.Visible = false;
                            PQualityLi.Visible = false;
                            DataEntry.Visible = false;
                            ClosedRequestsLi.Visible = false;
                            QualityClosedRequestsLi.Visible = false;
                            Reports.Visible = false;
                            TBALi.Visible = false;
                            SearchLi.Visible = false;
                            AcceptLi.Visible = false;

                        }
                        else if (type == "Admin")
                        {
                            AddRequestLi.Visible = true;
                            AddClaimLi.Visible = false;
                            int NewCounter = mdb.GetAllReqNewCounter();
                            spn_NewTick.InnerHtml = NewCounter.ToString();
                            int SearchingCounter = mdb.GetAllReqSearchingCounter();
                            spn_SearchingTick.InnerHtml = SearchingCounter.ToString();
                            int PMissingCounter = mdb.GetAllReqPMissingCounter();
                            spn_PMissingTick.InnerHtml = PMissingCounter.ToString();

                            int PendingQControlCounter = mdb.GetAllReqPQControlCounter();
                            spn_PQControlTick.InnerHtml = PendingQControlCounter.ToString();
                            
                            int QControlCounter = mdb.GetAllReqQControlCounter();
                            spn_QControlTick.InnerHtml = QControlCounter.ToString();


                            int PConfirmationCounter = mdb.GetAllReqPConfirmationCounter();
                            spn_TBAConfirmTick.InnerHtml = PConfirmationCounter.ToString();
                            int AcceptCounter = mdb.GetAllReqAcceptCounter();
                            spn_AcceptTick.InnerHtml = AcceptCounter.ToString();

                            int ClosedCounter = mdb.GetAllReqClosedCounter();
                            spn_ClosedlTick.InnerHtml = ClosedCounter.ToString();

                            int QualityClosedCounter = mdb.GetAllReqQualityClosedCounter();
                            spn_QualityClosedlTick.InnerHtml = QualityClosedCounter.ToString();
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

        protected void GoSearch(object sender, EventArgs e)
        {
            //string s = txt_search.Value;
            //Response.Redirect("SearchForm.aspx?word="+txt_search.Value+"");
        }
    }
}