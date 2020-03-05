using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardCycle.BLL;
using System.IO;
using CardCycle.DAL;

namespace CardCycle
{
    public partial class ManageClient : System.Web.UI.Page
    {
        Request mdb = new Request();
        Client cl = new Client();
        Account act = new Account();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            requestTB req = new requestTB();
            btn_Change_Status.Visible = false;
            StatusDropDown.Visible = false;
            string type = Request.Cookies["type"].Value;
            #region Introduction
            if (!IsPostBack)
            {
                var data = act.getQualityUser();
                if (type == "QualityControl")
                {
                    drop_quality.Visible = true;
                    foreach (var x in data)
                    {
                        drop_quality.Items.Add(x.name);
                    }
                }

                string src = "";
                string sub = "";
                string body = "";
                string attch = "";
                string acManger = "";
                string ISSComment = "";
                string AMC = "";
                string UWC = "";
                string requestType = "";
                string CAsign = "";
                string asignPrint = "";
                string asignQC = "";
                string closeNotes = "";
                string ClientName = "";
                string clientnotes = "";
                string pricingcomments = "";
                string accountcomments = "";
                string Qualitycomments = "";
                string NumberOfCards = "";
                string IssNumOfCards = "";
                string ExceptionComm = "";
                string AMExceptionConf = "";
                string UWExceptionConf = "";
                string AccSrc = "";
                string rColor = "";
                string CComment = "";
                string CAction = "";
                string NumberOfBooklet = "";
                acceptComplain.Visible = false;
                rejectComplain.Visible = false;
                AddComplain.Visible = false;
                EnterCompComments.Visible = false;
                ComplainComments.Visible = false;
                btn_Complain.Visible = false;
                btn_reject_Seen.Visible = false;
                div1.Visible = false;
                text2.ReadOnly = true;
                btn_Close.Visible = false;
                btn_asignPrint.Visible = false;
                btn_AsignQC.Visible = false;
                div_Error.Visible = false;
                btn_Close.Disabled = true;
                btn_asign.Visible = false;
                btn_transfare.Visible = false;
                DropDownList1.Visible = false;
                btn_PTechnical.Visible = false;
                FileUpload1.Visible = false;
                btn_fw.Visible = false;
                btn_back_to_Issuance.Visible = false;
                // txtrBody.Disabled = true;
                btn_iss_print.Visible = false;
                // btn_reject.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                Label1.InnerText = id.ToString();
                string state = Request.QueryString["state"];
                TextBox1.ReadOnly = true;
                IssCardsNum.ReadOnly = true;
                ////////////////////////////////////
                pricingcom.ReadOnly = true;
                accountcom.ReadOnly = true;
                QCcom.ReadOnly = true;
                ExcepCarea.Visible = false;
                btn_IssExceptionConfirm.Visible = false;
                btn_AMExceptionConfirm.Visible = false;
                ////////////////////////////////////
                btn_trnsfare_print.Visible = false;
                btn_transfare_Qc.Visible = false;

                mdb.GetNumberOfBooklet(id ,ref NumberOfBooklet);

                mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

                //if(NumberOfBooklet!=null || NumberOfBooklet=="")
                //{
                //    txtNumberOfBookelt.Text = NumberOfBooklet;
                //    txtNumberOfBookelt.ReadOnly = true;
                //}


                text2.Text = closeNotes;
                lbl_Sub.InnerHtml = sub;
                txtrBody.InnerHtml = body;
                NumOfCardsByACC.InnerHtml = NumberOfCards;
                Label2.InnerHtml = requestType.ToString();
                ClientTxt.InnerHtml = ClientName;
                TextBox1.Text = ISSComment;
                IssCardsNum.Text = IssNumOfCards;
                accountcom.Text = accountcomments;
                pricingcom.Text = pricingcomments;
                QCcom.Text = Qualitycomments;
                ExcepComm.Value = ExceptionComm;
                Complaintext.InnerHtml = CComment;

                if (requestType.ToString() == "Exceptional-Transfer" || requestType.ToString() == "Exceptional-Addition"
                    || requestType.ToString() == "Exception-Cancellation" || requestType.ToString() == "Exception-Modification")
                {
                    ExcepCarea.Visible = true;
                }
                try
                {
                    cl.GetClientNotesByName(ClientName, ref clientnotes);
                    if (clientnotes != null)
                    {
                        spn_close.InnerHtml = clientnotes;
                    }
                    else
                    {
                        spn_close.InnerHtml = "No Notes To Display";
                    }
                }
                catch
                {
                    spn_close.InnerHtml = "No Notes To Display";
                }

                //string type = Request.Cookies["type"].Value;
                string user = Request.Cookies["name"].Value;
                btn_reject.Visible = false;
                btn_reject.Disabled = true;

                if (attch == "")
                {
                    btn_download.Disabled = true;
                    btn_Original_download.Disabled = true;

                }
            #endregion

                #region AccountManager
                if (type == "Account Manager")
                {
                    TextBox1.Text = ISSComment;
                    TextBox1.ReadOnly = true;
                    btn_issue.Visible = false;
                    btn_print.Visible = false;
                    btn_QC.Visible = false;
                    btn_UWcconfirm.Visible = false;
                    btn_Mang.Disabled = true;
                    btn_AMConfirm.Disabled = true;
                    // txtrBody.Disabled = false;
                    //if (attch != "")
                    //    btn_download.Disabled = false;

                    if (state == "Exception Pending")
                    {
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        if (AMExceptionConf == "true")
                        {
                            btn_AMExceptionConfirm.Disabled = true;
                            btn_AMExceptionConfirm.Visible = true;
                            btn_reject.Disabled = true;
                            btn_reject.Visible = true;
                        }
                        else
                        {
                            btn_AMExceptionConfirm.Disabled = false;
                            btn_AMExceptionConfirm.Visible = true;
                            btn_reject.Disabled = false;
                            btn_reject.Visible = true;
                        }

                    }

                    if (state == "pending confirmation")
                    {
                        btn_back_to_Issuance.Visible = true;
                        accountcom.ReadOnly = false;
                        if (AMC == "true")
                        {
                            btn_AMConfirm.Disabled = true;

                        }
                        else
                        {
                            btn_AMConfirm.Disabled = false;
                        }

                    }

                    if (state == "Closing")
                    {
                        btn_Mang.Disabled = false;
                        text2.ReadOnly = false;
                        text2.Text = closeNotes;
                    }
                    if (state == "Rejected" && rColor == "#ffd966")
                    {

                        btn_fw.Visible = true;
                        btn_reject_Seen.Visible = true;
                    }
                    if (state == "Closed" && CAction != "true")
                    {

                        btn_Complain.Visible = true;
                    }
                    if (state == "AccountComplain")
                    {
                        EnterCompComments.Visible = true;
                        ComplainComments.Visible = true;
                        btn_transfare.Visible = false;
                        btn_trnsfare_print.Visible = false;
                        btn_transfare_Qc.Visible = false;
                        btn_asign.Visible = false;
                        btn_asignPrint.Visible = false;
                        btn_AsignQC.Visible = false;
                        btn_Complain.Visible = false;
                        ExcepCarea.Visible = false;
                        IssuanceCommentsArea.Visible = false;
                        AccountBackCommentsArea.Visible = false;
                        PricingBackCommentsArea.Visible = false;
                        QualityControlCommentsArea.Visible = false;
                        ClosingCommentsArea.Visible = false;
                        btn_iss_print.Visible = false;
                        btn_issue.Visible = false;
                        btn_print.Visible = false;
                        btn_PTechnical.Visible = false;
                        btn_QC.Visible = false;
                        btn_back_to_Issuance.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        btn_AMExceptionConfirm.Visible = false;
                        btn_IssExceptionConfirm.Visible = false;
                        btn_Mang.Visible = false;
                        btn_reject.Visible = false;
                        btn_reject_Seen.Visible = false;
                        btn_Close.Visible = false;
                        btn_fw.Visible = false;
                        AccountManagerCommentsArea.Visible = false;
                        FinalNumberOfMembersArea.Visible = false;
                        downloadarea.Visible = false;
                        rejectComplain.Visible = false;
                        acceptComplain.Visible = false;
                        AddComplain.Visible = true;
                        acceptComplain.Visible = true;
                        //rejectComplain.Visible = true;

                    }

                }
                #endregion
                #region Issuance
                else if (type == "Issuance" || type == "It")
                {
                    //here//
                    btn_Close.Visible = true;
                    btn_print.Disabled = true;
                    btn_QC.Disabled = true;
                    btn_issue.Disabled = true;
                    btn_iss_print.Visible = true;
                    btn_iss_print.Disabled = true;
                    btn_reject.Visible = true;
                    btn_reject.Disabled = true;
                    btn_download.Disabled = true;
                    btn_Original_download.Disabled = true;
                    //div_NumberOfBookelt.Visible = true;
                    //if (requestType.ToString() == "Lost Card" || requestType.ToString() == "Modification" || requestType.ToString() == "Exception-Modification" || requestType.ToString() == "Transfer" || requestType.ToString() == "Exceptional-Transfer" || requestType.ToString() == "Cancellation" || requestType.ToString() =="reprint card")
                    //{
                    //    div_NumberOfBookelt.Visible = false;
                    //}
                    string str = requestType.ToString().Trim();
                    if (requestType.ToString().Trim() == "Addition" || requestType.ToString().Trim() == "Renewal" || requestType.ToString().Trim() == "New Company" || requestType.ToString().Trim() == "Missing Photo" || requestType.ToString().Trim() == "Exceptional-Addition" || requestType.ToString().Trim() == "Discount Card")
                    {
                        div_NumberOfBookelt.Visible = true;
                    }
                    bool bol = mdb.Isissuing(id);
                    if (bol == false )
                    {
                        txtNumberOfBookelt.ReadOnly = true;
                    }
                   
                       else if (type == "It")
                        {
                            mdb.GetNumberOfBooklet(id, ref NumberOfBooklet);
                            if (NumberOfBooklet != "" || NumberOfBooklet != null || NumberOfBooklet != string.Empty)
                            {
                                txtNumberOfBookelt.Text = NumberOfBooklet;
                            }

                           // txtNumberOfBookelt.ReadOnly = true;
                        }

                    
                    #region State Issuing
                    if (state == "Exception Pending")
                    {
                        btn_iss_print.Visible = false;
                        btn_issue.Visible = false;
                        btn_Close.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        btn_print.Visible = false;
                        btn_QC.Visible = false;
                        btn_Mang.Visible = false;
                        if (UWExceptionConf == "true")
                        {
                            btn_IssExceptionConfirm.Disabled = true;
                            btn_IssExceptionConfirm.Visible = true;
                            btn_reject.Disabled = true;
                            btn_reject.Visible = true;
                        }
                        else
                        {
                            btn_IssExceptionConfirm.Disabled = false;
                            btn_IssExceptionConfirm.Visible = true;
                            btn_reject.Disabled = false;
                            btn_reject.Visible = true;
                        }

                    }
                    if (state == "Issuing")
                    {
                        btn_PTechnical.Visible = true;
                        btn_PTechnical.Disabled = true;
                        btn_print.Visible = false;
                        btn_QC.Visible = false;
                        btn_asign.Visible = true;
                        TextBox1.ReadOnly = false;
                        IssCardsNum.ReadOnly = false;
                        btn_Mang.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        btn_AMConfirm.Visible = false;
                        FileUpload1.Visible = true;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;

                        string issperson = Request.Cookies["name"].Value;
                        if (CAsign != null)
                        {
                            if (CAsign.Equals(issperson))
                            {
                                btn_asign.Visible = false;
                                btn_Close.Disabled = false;
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;
                                btn_iss_print.Disabled = false;
                                btn_issue.Disabled = false;
                                btn_reject.Disabled = false;
                                btn_transfare.Disabled = false;
                                btn_transfare.Visible = true;
                                DropDownList1.Visible = true;
                               
                               
                                btn_PTechnical.Disabled = false;
                            }
                            else
                            {
                                btn_asign.Visible = false;
                            }
                        }
                    }
                    #endregion
                    #region State Printing

                    if (state == "Printing")
                    {
                        btn_asignPrint.Visible = true;
                        btn_print.Disabled = true;
                        btn_download.Disabled = true;
                        btn_Original_download.Disabled = true;
                        FileUpload1.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        btn_issue.Visible = false;
                        btn_iss_print.Visible = false;
                        btn_reject.Visible = false;
                        btn_Close.Visible = false;
                        btn_QC.Visible = false;
                        //btn_reject.Visible = false;
                        //////////////////////////////////////////////////////////
                        string printperson = Request.Cookies["name"].Value;
                        if (asignPrint != null)
                        {
                            if (asignPrint.Equals(printperson))
                            {
                                btn_asignPrint.Visible = false;
                                btn_print.Disabled = false;
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;
                                btn_trnsfare_print.Visible = true;
                              
                               
                            }
                            else
                            {
                                btn_asignPrint.Visible = false;
                            }
                        }
                    }
                    #endregion
                    #region State Pending Technical

                    if (state == "Pending Technical")
                    {
                        btn_back_to_Issuance.Visible = true;
                        btn_back_to_Issuance.Disabled = true;
                        btn_asignPrint.Visible = false;
                        btn_print.Visible = false;
                        btn_download.Disabled = true;
                        btn_Original_download.Disabled = true;
                        FileUpload1.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        btn_issue.Visible = false;
                        btn_iss_print.Visible = false;
                        btn_reject.Visible = false;
                        btn_Close.Visible = false;
                        btn_QC.Visible = false;
                        //btn_reject.Visible = false;
                        //////////////////////////////////////////////////////////
                        string penTech = Request.Cookies["name"].Value;
                        if (CAsign != null)
                        {
                            if (CAsign.Equals(penTech))
                            {
                                btn_back_to_Issuance.Disabled = false;
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;
                            }
                            else
                            {
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;

                            }
                        }
                    }
                    #endregion

                    #region Quality Control
                    if (state == "Quality Control")
                    {
                        btn_AsignQC.Visible = true;
                        btn_download.Disabled = true;
                        btn_Original_download.Disabled = true;
                        btn_QC.Disabled = true;
                        FileUpload1.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        IssCardsNum.ReadOnly = false;
                        QCcom.ReadOnly = false;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        btn_reject.Visible = false;
                        btn_iss_print.Visible = false;
                        btn_issue.Visible = false;
                        btn_Close.Visible = false;
                        btn_print.Visible = false;

                        string QCperson = Request.Cookies["name"].Value;
                        if (asignQC != null)
                        {
                            if (asignQC.Equals(QCperson))
                            {
                                btn_AsignQC.Visible = false;
                                btn_QC.Disabled = false;
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;
                                btn_transfare_Qc.Visible = true;
                               
                                DropDownList1.Visible = true; 
                            }
                            else
                            {
                                btn_AsignQC.Visible = false;
                                btn_QC.Disabled = true;
                                btn_transfare_Qc.Visible = false;
                            }
                        }


                    }
                    #endregion
                    #region Closing
                    if (state == "Closing")
                    {
                        FileUpload1.Visible = false;
                        btn_issue.Disabled = true;
                        btn_print.Disabled = true;
                        btn_QC.Disabled = true;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;

                    }
                    #endregion
                    #region closed
                    if (state == "Closed")
                    {
                        FileUpload1.Visible = false;
                        btn_issue.Disabled = true;
                        btn_print.Disabled = true;
                        btn_QC.Disabled = true;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;

                    }
                    #endregion
                    #region Pending Cnformation
                    if (state == "pending confirmation")
                    {
                        // FileUpload1.Visible = false;
                        btn_issue.Disabled = true;
                        btn_print.Disabled = true;
                        btn_UWcconfirm.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_Mang.Visible = false;
                        btn_QC.Disabled = true;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        /////////////////////////////////////////////////////
                        string Pending_by_issperson = Request.Cookies["name"].Value;
                        if (CAsign != null)
                        {
                            if (CAsign.Equals(Pending_by_issperson))
                            {
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;

                            }

                        }
                    }
                    #endregion
                    #region Rejected
                    if (state == "Rejected")
                    {
                        // FileUpload1.Visible = false;
                        btn_issue.Disabled = true;
                        btn_print.Disabled = true;
                        btn_UWcconfirm.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_Mang.Visible = false;
                        btn_QC.Disabled = true;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;

                        string Rejected_by_issperson = Request.Cookies["name"].Value;
                        if (CAsign != null)
                        {
                            if (CAsign.Equals(Rejected_by_issperson))
                            {
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;

                            }

                        }
                    }
                    #endregion
                    #region IssuanceComplain
                   
                    if (state == "IssuanceComplain")
                    {
                        //   mdb.GetAllReqComplain();
                        bool found = mdb.GetAllReqComplainiss(id , user);
                         if(found == true)
                        {

                            FileUpload1.Visible = false;
                            TextBox1.Text = ISSComment;
                            TextBox1.ReadOnly = true;
                            acceptComplain.Visible = true;
                            btn_reject.Visible = false;
                            btn_iss_print.Visible = false;
                            EnterCompComments.Visible = true;
                            ComplainComments.Visible = true;
                            btn_transfare.Visible = false;
                            btn_trnsfare_print.Visible = false;
                            btn_transfare_Qc.Visible = false;
                            btn_asign.Visible = false;
                            btn_asignPrint.Visible = false;
                            btn_AsignQC.Visible = false;
                            btn_Complain.Visible = false;
                            ExcepCarea.Visible = false;
                            IssuanceCommentsArea.Visible = false;
                            AccountBackCommentsArea.Visible = false;
                            PricingBackCommentsArea.Visible = false;
                            QualityControlCommentsArea.Visible = false;
                            ClosingCommentsArea.Visible = false;
                            btn_issue.Visible = false;
                            btn_print.Visible = false;
                            btn_PTechnical.Visible = false;
                            btn_QC.Visible = false;
                            btn_back_to_Issuance.Visible = false;
                            btn_AMConfirm.Visible = false;
                            btn_UWcconfirm.Visible = false;
                            btn_AMExceptionConfirm.Visible = false;
                            btn_IssExceptionConfirm.Visible = false;
                            btn_Mang.Visible = false;
                            btn_reject_Seen.Visible = false;
                            btn_Close.Visible = false;
                            btn_fw.Visible = false;
                            AccountManagerCommentsArea.Visible = false;
                            FinalNumberOfMembersArea.Visible = false;
                            downloadarea.Visible = false;
                            rejectComplain.Visible = true;
                            AddComplain.Visible = false;
                        }
                         else
                        {
                            FileUpload1.Visible = false;
                            TextBox1.Text = ISSComment;
                            TextBox1.ReadOnly = false;
                            acceptComplain.Visible = false;
                            btn_reject.Visible = false;
                            btn_iss_print.Visible = false;
                            EnterCompComments.Visible = false;
                            ComplainComments.Visible = false;
                            btn_transfare.Visible = false;
                            btn_trnsfare_print.Visible = false;
                            btn_transfare_Qc.Visible = false;
                            btn_asign.Visible = false;
                            btn_asignPrint.Visible = false;
                            btn_AsignQC.Visible = false;
                            btn_Complain.Visible = false;
                            ExcepCarea.Visible = false;
                            IssuanceCommentsArea.Visible = false;
                            AccountBackCommentsArea.Visible = false;
                            PricingBackCommentsArea.Visible = false;
                            QualityControlCommentsArea.Visible = false;
                            ClosingCommentsArea.Visible = false;
                            btn_issue.Visible = false;
                            btn_print.Visible = false;
                            btn_PTechnical.Visible = false;
                            btn_QC.Visible = false;
                            btn_back_to_Issuance.Visible = false;
                            btn_AMConfirm.Visible = false;
                            btn_UWcconfirm.Visible = false;
                            btn_AMExceptionConfirm.Visible = false;
                            btn_IssExceptionConfirm.Visible = false;
                            btn_Mang.Visible = false;
                            btn_reject_Seen.Visible = false;
                            btn_Close.Visible = false;
                            btn_fw.Visible = false;
                            btn_Change_Status.Visible  = false ;
                            StatusDropDown.Visible = false;
                            AccountManagerCommentsArea.Visible = false;
                            FinalNumberOfMembersArea.Visible = false;
                            downloadarea.Visible = false;
                            rejectComplain.Visible = false;
                            AddComplain.Visible = false;
                        }
                       
                    }
                    #endregion
                    #region IssuanceComplain
                    if (state == "AccountComplain")
                    {

                        bool fo = mdb.GetAllReqComplainAC(id, user);

                        if(fo == true)
                        {
                            btn_Close.Visible = false;
                            btn_print.Visible = false;
                            btn_QC.Visible = false;
                            btn_issue.Visible = false;
                            btn_iss_print.Visible = false;
                            btn_reject.Visible = false;
                            btn_download.Disabled = true;
                            btn_Original_download.Disabled = true;
                            btn_AMConfirm.Visible = false;
                            btn_UWcconfirm.Visible = false;
                            btn_Mang.Visible = false;
                        }
                        else
                        {
                            btn_Close.Visible = false;
                            btn_print.Visible = false;
                            btn_QC.Visible = false;
                            btn_issue.Visible = false;
                            btn_iss_print.Visible = false;
                            btn_reject.Visible = false;
                            btn_download.Disabled = false;
                            btn_Original_download.Disabled = false;
                            btn_AMConfirm.Visible = false;
                            btn_UWcconfirm.Visible = false;
                            btn_Mang.Visible = false;
                        }

                        

                    }
                    #endregion
                }
                #endregion
                #region Quakity Control
                else if (type == "QualityControl")
                {
                    if (requestType.ToString().Trim() == "Addition" || requestType.ToString().Trim() == "Renewal" || requestType.ToString().Trim() == "New Company" || requestType.ToString().Trim() == "Missing Photo" || requestType.ToString().Trim() == "Exceptional-Addition" || requestType.ToString().Trim() == "Discount Card")
                    {
                        div_NumberOfBookelt.Visible = true;
                        mdb.GetNumberOfBooklet(id, ref NumberOfBooklet);
                        txtNumberOfBookelt.Text = NumberOfBooklet;
                    }
               
                    txtNumberOfBookelt.ReadOnly = true;
                  
                    if (state == "Quality Control")
                    {
                        FileUpload1.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        QCcom.ReadOnly = false;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        btn_reject.Visible = false;
                        btn_iss_print.Visible = false;
                        btn_issue.Visible = false;
                        btn_Close.Visible = false;
                        btn_print.Visible = false;
                        DropDownList1.Visible = false;
                        btn_transfare_Qc.Visible = true;

                        string QCperson = Request.Cookies["name"].Value;
                        if (asignQC != null)
                        {
                            if (asignQC.Equals(QCperson))
                            {
                                btn_AsignQC.Visible = false;
                                btn_QC.Disabled = false;
                                btn_download.Disabled = false;
                                btn_Original_download.Disabled = false;
                                drop_quality.Visible = true;

                            }
                            else
                            {
                                
                                btn_QC.Disabled = true;
                                btn_transfare_Qc.Visible = false;
                                drop_quality.Visible = false;
                            }
                        }
                        else
                        {
                            btn_AsignQC.Visible = true;
                            btn_QC.Disabled = true;
                            btn_transfare_Qc.Visible = false;
                            drop_quality.Visible = false;
                        }
                    }
                    #region Printing
                    if (state == "Printing")
                    {
                        FileUpload1.Visible = false;
                        btn_issue.Visible = false;
                        btn_print.Visible = false;
                        btn_QC.Visible = false;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        drop_quality.Visible = false;
                        btn_transfare_Qc.Visible = false;
                        btn_Original_download.Visible = true;
                        btn_Original_download.Disabled = false;

                    }
                    #endregion
                    #region Closing
                    if (state == "Closing")
                    {
                        FileUpload1.Visible = false;
                        btn_issue.Visible = false;
                        btn_print.Visible = false;
                        btn_QC.Visible = false;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        drop_quality.Visible = false;
                        btn_transfare_Qc.Visible = false;
                        btn_Original_download.Visible = true;
                        btn_Original_download.Disabled = false;


                    }
                    #endregion
                    #region closed
                   if (state == "Closed")
                    {
                        FileUpload1.Visible = false;
                        btn_issue.Visible = false;
                        btn_print.Visible = false;
                        btn_QC.Visible = false;
                        btn_Mang.Visible = false;
                        btn_AMConfirm.Visible = false;
                        btn_UWcconfirm.Visible = false;
                        TextBox1.Text = ISSComment;
                        TextBox1.ReadOnly = true;
                        drop_quality.Visible = false;
                        btn_transfare_Qc.Visible = false;
                        btn_Original_download.Visible = true;
                        btn_Original_download.Disabled = false;

                    }
                    #endregion
                    //if (state == "Quality Control" && asignQC != null)
                    //{
                    //    btn_transfare_Qc.Visible = true;
                    //    btn_transfare_Qc.Disabled = false;
                    //    btn_QC.Disabled = false;
                    //    DropDownList1.Visible = true;
                    //}
                }
                #endregion
                #region underwritting
                else if (type == "Underwriting")
                {
                    TextBox1.ReadOnly = true;
                    TextBox1.Text = ISSComment;
                    btn_issue.Visible = false;
                    btn_print.Visible = false;
                    btn_QC.Visible = false;
                    btn_AMConfirm.Visible = false;
                    btn_Mang.Visible = false;
                    btn_UWcconfirm.Disabled = true;
                    TextBox1.ReadOnly = true;
                    // txtrBody.Disabled = false;
                    if (state == "pending confirmation")
                    {
                        btn_back_to_Issuance.Visible = true;
                        pricingcom.ReadOnly = false;
                        if (UWC == "true")
                        {
                            btn_UWcconfirm.Disabled = true;
                            btn_back_to_Issuance.Visible = false;
                        }
                        else
                        {
                            btn_UWcconfirm.Disabled = false;
                            mdb.updateAssignUnderWriting(id, user);
                        }

                    }
                }
                #endregion
                string adm = Request.Cookies["name"].Value;
                string SpState = Request.QueryString["state"];

                if ((adm == "Meriam.Mikhael" || adm == "admin" || adm == "Mohamed.Abdelsattar"))
                {
                    if (attch != null)
                    {
                        btn_download.Disabled = false;
                    }
                    if (AccSrc != "")
                        btn_Original_download.Disabled = false;

                    if (SpState == "Issuing" && CAsign != null)
                    {
                        btn_transfare.Visible = true;
                        btn_transfare.Disabled = false;
                        DropDownList1.Visible = true;
                    }
                    if (SpState == "Printing" && asignPrint != null)
                    {
                        btn_trnsfare_print.Visible = true;
                        btn_trnsfare_print.Disabled = false;
                        DropDownList1.Visible = true;
                    }
                    if (SpState == "Quality Control" && asignQC != null)
                    {
                        btn_transfare_Qc.Visible = true;
                        btn_transfare_Qc.Disabled = false;
                        DropDownList1.Visible = true;
                    }
                    btn_Change_Status.Visible = true;
                    StatusDropDown.Visible = true;
                }
                // if attached empty
                if (attch == "")
                {
                    btn_download.Disabled = true;
                }
                if (AccSrc == "")
                {
                  btn_Original_download.Disabled = true;
                }

                if (attch != "")
                {
                    if (CAsign == user || asignPrint == user || asignQC == user || state != "Issuing")
                    {
                        btn_download.Disabled = false;
                    }
                }
                if (AccSrc != "")
                {
                    if (CAsign == user || asignPrint == user || asignQC == user || state != "Issuing")
                    {
                        btn_Original_download.Disabled = false;
                    }
                }

                if (CAction == "true" || CAction == "ReComp")
                {
                    ComplainComments.Visible = true;

                }
            }


        }          //finished
        protected void DownloadFile(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            //Response.ContentType = "file/rar";
            Response.ContentType = ".rar";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + lbl_Sub.InnerHtml + " " + user +" "+DateTime.Now.ToString()+".rar");
            // Response.AppendHeader("Content-Disposition", "attachment; filename=" + lbl_Sub.InnerHtml + " " + user +" "+DateTime.Now.ToString()+".rar");
            //string attach = lbl_Sub.InnerHtml;
            // attach.Replace(" ", "_");
            string closeNotes = "";
            int id = int.Parse(Request.QueryString["id"]);
            string src = "";
            string sub = "";
            string body = "";
            string attch = "";
            string acManger = "";
            string ISSComment = "";
            string AMC = "";
            string UWC = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";


            mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + id + ".rar");
            // Response.AddHeader("Content-Disposition", "attachment; filename=\"" + lbl_Sub.InnerHtml + "\"");
            Response.TransmitFile(Server.MapPath(src));
            Response.End();

        }
        protected void DownloadOriginalFile(object sender, EventArgs e)
        {
            try
            {
                string user = Request.Cookies["name"].Value;
                //Response.ContentType = "file/rar";
                Response.ContentType = ".rar";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + lbl_Sub.InnerHtml + " " + user +" "+DateTime.Now.ToString()+".rar");
                // Response.AppendHeader("Content-Disposition", "attachment; filename=" + lbl_Sub.InnerHtml + " " + user +" "+DateTime.Now.ToString()+".rar");
                //string attach = lbl_Sub.InnerHtml;
                // attach.Replace(" ", "_");
                string closeNotes = "";
                int id = int.Parse(Request.QueryString["id"]);
                string src = "";
                string sub = "";
                string body = "";
                string attch = "";
                string acManger = "";
                string ISSComment = "";
                string AMC = "";
                string UWC = "";
                string requestType = "";
                string CAsign = "";
                string asignPrint = "";
                string asignQC = "";
                string ClientName = "";
                string pricingcomments = "";
                string accountcomments = "";
                string NumberOfCards = "";
                string IssNumOfCards = "";
                string Qualitycomments = "";
                string ExceptionComm = "";
                string AMExceptionConf = "";
                string UWExceptionConf = "";
                string AccSrc = "";
                string rColor = "";
                string CComment = "";
                string CAction = "";


                mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);
               Response.AppendHeader("Content-Disposition", "attachment; filename=" + id + ".rar");
                // Response.AddHeader("Content-Disposition", "attachment; filename=\"" + lbl_Sub.InnerHtml + "\"");

                Response.TransmitFile(Server.MapPath(AccSrc));
                Response.End();


                bool isExists = System.IO.Directory.Exists(Server.MapPath(AccSrc));
                if(isExists==true)
                {
                    Response.TransmitFile(Server.MapPath(AccSrc));
                    Response.End();
                }
                //else
                //{ Response.Write("<script LANGUAGE='JavaScript'>alert('File  Not Found ')</script>"); }

                
             
            }
            catch( Exception ex)
            {
                //lbmessage.Visible = true;
                //lbmessage.Text= ex.Message;
            }
        }
        protected void ApvIssue(object sender, EventArgs e)
        {
            if (IssCardsNum.Text != "")
            {
                string user = Request.Cookies["name"].Value;
                int id = int.Parse(Request.QueryString["id"]);
             
                if (txtNumberOfBookelt.Text != null && txtNumberOfBookelt.Text != "" && txtNumberOfBookelt.Text != string.Empty && string.IsNullOrWhiteSpace(txtNumberOfBookelt.Text)==false&& div_NumberOfBookelt.Visible==true)
                {
                    mdb.AddNumberOfBooklet(id, txtNumberOfBookelt.Text);

                    string mm = TextBox1.Text;
                    string src = "";
                    string sub = "";
                    string body = "";
                    string attch = "";
                    string acManger = "";
                    string ISSComment = "";
                    string AMC = "";
                    string UWC = "";
                    string requestType = "";
                    string CAsign = "";
                    string asignPrint = "";
                    string asignQC = "";
                    string closeNotes = "";
                    string ClientName = "";
                    string pricingcomments = "";
                    string accountcomments = "";
                    string NumberOfCards = "";
                    string IssNumOfCards = "";
                    string Qualitycomments = "";
                    string ExceptionComm = "";
                    string AMExceptionConf = "";
                    string UWExceptionConf = "";
                    string AccSrc = "";
                    string rColor = "";
                    string CComment = "";
                    string CAction = "";


                    mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

                    if (requestType != "Cancellation" && requestType != "Suspend" && requestType != "Unsuspend" && requestType != "Exception-Cancellation")
                    {
                        if (requestType == "Renewal" || requestType == "New Company")
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);


                                }
                                catch
                                {
                                    mdb.SendEmailBackupFail();
                                }


                                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssing(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                            else
                            {

                                // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                                //string src = "~/Issuance/" + FileUpload1.FileName;
                                mdb.UpdateIssing2(id, user, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }

                        }
                        else
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                                }
                                catch
                                {
                                    mdb.SendEmailBackupFail();
                                }

                                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssingNoTAPV(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                            else
                            {

                                mdb.UpdateIssing2NOTAPV(id, user, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                        }

                    }
                    else
                    {
                        if (FileUpload1.HasFile)
                        {
                            int PATHid = int.Parse(Request.QueryString["id"]);
                            Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                            FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                            try
                            {
                                Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                            }
                            catch
                            {
                                mdb.SendEmailBackupFail();
                            }

                            src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;
                            mdb.UpdateCancel2Issuning(id, user, src, TextBox1.Text, IssCardsNum.Text);
                            btn_issue.Disabled = true;
                            TextBox1.ReadOnly = true;
                        }
                        else
                        {
                            mdb.UpdateCancelIssuning(id, user, TextBox1.Text, IssCardsNum.Text);
                            btn_issue.Disabled = true;
                            TextBox1.ReadOnly = true;
                        }
                    }
                    btn_issue.Disabled = true;
                    btn_iss_print.Disabled = true;
                    btn_reject.Disabled = true;
                    btn_Close.Disabled = true;
                    btn_transfare.Disabled = true;
                    btn_download.Disabled = true;
                    btn_Original_download.Disabled = true;
                    DropDownList1.Enabled = false;
                    btn_PTechnical.Visible = false;
                    div1.Visible = false;
                    Response.Redirect("RequestNew.aspx", false);

                }
               else if (div_NumberOfBookelt.Visible == false)
                {
                    lbl_num_booklet.Visible = false;

                    string mm = TextBox1.Text;
                    string src = "";
                    string sub = "";
                    string body = "";
                    string attch = "";
                    string acManger = "";
                    string ISSComment = "";
                    string AMC = "";
                    string UWC = "";
                    string requestType = "";
                    string CAsign = "";
                    string asignPrint = "";
                    string asignQC = "";
                    string closeNotes = "";
                    string ClientName = "";
                    string pricingcomments = "";
                    string accountcomments = "";
                    string NumberOfCards = "";
                    string IssNumOfCards = "";
                    string Qualitycomments = "";
                    string ExceptionComm = "";
                    string AMExceptionConf = "";
                    string UWExceptionConf = "";
                    string AccSrc = "";
                    string rColor = "";
                    string CComment = "";
                    string CAction = "";


                    mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

                    if (requestType != "Cancellation" && requestType != "Suspend" && requestType != "Unsuspend" && requestType != "Exception-Cancellation")
                    {
                        if (requestType == "Renewal" || requestType == "New Company")
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                                }
                                catch
                                {
                                    mdb.SendEmailBackupFail();
                                }


                                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssing(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                            else
                            {

                                // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                                //string src = "~/Issuance/" + FileUpload1.FileName;
                                mdb.UpdateIssing2(id, user, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }

                        }
                        else
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                                }
                                catch
                                {
                                    mdb.SendEmailBackupFail();
                                }

                                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssingNoTAPV(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                            else
                            {

                                mdb.UpdateIssing2NOTAPV(id, user, TextBox1.Text, IssCardsNum.Text);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                        }

                    }
                    else
                    {
                        if (FileUpload1.HasFile)
                        {
                            int PATHid = int.Parse(Request.QueryString["id"]);
                            Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                            FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                            try
                            {
                                Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                            }
                            catch
                            {
                                mdb.SendEmailBackupFail();
                            }

                            src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;
                            mdb.UpdateCancel2Issuning(id, user, src, TextBox1.Text, IssCardsNum.Text);
                            btn_issue.Disabled = true;
                            TextBox1.ReadOnly = true;
                        }
                        else
                        {
                            mdb.UpdateCancelIssuning(id, user, TextBox1.Text, IssCardsNum.Text);
                            btn_issue.Disabled = true;
                            TextBox1.ReadOnly = true;
                        }
                    }
                    btn_issue.Disabled = true;
                    btn_iss_print.Disabled = true;
                    btn_reject.Disabled = true;
                    btn_Close.Disabled = true;
                    btn_transfare.Disabled = true;
                    btn_download.Disabled = true;
                    btn_Original_download.Disabled = true;
                    DropDownList1.Enabled = false;
                    btn_PTechnical.Visible = false;
                    div1.Visible = false;
                    Response.Redirect("RequestNew.aspx", false);

                }
                else
                {
                    lbl_num_booklet.Visible = true;
                }
              
            }
            else
            {
                div1.Visible = true;
            }
            //Server.Transfer("RequestNew.aspx");


        }          //finished
        protected void ApvIssueAndPrint(object sender, EventArgs e)
        {
            if (IssCardsNum.Text != "")
            {
                string user = Request.Cookies["name"].Value;
                int id = int.Parse(Request.QueryString["id"]);
                bool bol1 = string.IsNullOrWhiteSpace(txtNumberOfBookelt.Text);
                bool bol2 = div_NumberOfBookelt.Visible == true;
                
                if (txtNumberOfBookelt.Text != null && txtNumberOfBookelt.Text != "" && txtNumberOfBookelt.Text != string.Empty && string.IsNullOrWhiteSpace(txtNumberOfBookelt.Text)==false && div_NumberOfBookelt.Visible == true)
                {
                    mdb.AddNumberOfBooklet(id, txtNumberOfBookelt.Text);
                    string mm = TextBox1.Text;
                    string src = "";
                    string sub = "";
                    string body = "";
                    string attch = "";
                    string acManger = "";
                    string ISSComment = "";
                    string AMC = "";
                    string UWC = "";
                    string requestType = "";
                    string CAsign = "";
                    string asignPrint = "";
                    string asignQC = "";
                    string closeNotes = "";
                    string ClientName = "";
                    string pricingcomments = "";
                    string accountcomments = "";
                    string NumberOfCards = "";
                    string IssNumOfCards = "";
                    string Qualitycomments = "";
                    string ExceptionComm = "";
                    string AMExceptionConf = "";
                    string UWExceptionConf = "";
                    string AccSrc = "";
                    string rColor = "";
                    string CComment = "";
                    string CAction = "";
                    mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

                    if (requestType != "Cancellation" && requestType != "Suspend" && requestType != "Unsuspend" && requestType != "Exception-Cancellation")
                    {
                        if (requestType == "Renewal" || requestType == "New Company")
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    
                                        FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                                }
                                catch
                                {
                                    mdb.SendEmailBackupFail();
                                }

                                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssing(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;

                            }
                            else
                            {

                                // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                                //string src = "~/Issuance/" + FileUpload1.FileName;
                                mdb.UpdateIssing2(id, user, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }

                        }
                        else
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                            }
                                catch
                            {
                                mdb.SendEmailBackupFail();
                            }

                            src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssingNoTAPV(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                            else
                            {

                                // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                                //string src = "~/Issuance/" + FileUpload1.FileName;
                                mdb.UpdateIssing2NOTAPV(id, user, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                        }

                    }
                    else
                    {
                        mdb.UpdateCancelIssuning(id, user, TextBox1.Text, IssCardsNum.Text);
                        btn_issue.Disabled = true;
                        TextBox1.ReadOnly = true;

                    }
                    btn_issue.Disabled = true;
                    btn_iss_print.Disabled = true;
                    btn_reject.Disabled = true;
                    btn_Close.Disabled = true;
                    btn_transfare.Disabled = true;
                    btn_download.Disabled = true;
                    btn_Original_download.Disabled = true;
                    DropDownList1.Enabled = false;
                    btn_PTechnical.Visible = false;
                    div1.Visible = false;
                      Response.Redirect("RequestNew.aspx", false);
                }
                else if (div_NumberOfBookelt.Visible == false)
                {
                    lbl_num_booklet.Visible = false;
                    mdb.AddNumberOfBooklet(id, txtNumberOfBookelt.Text);
                    string mm = TextBox1.Text;
                    string src = "";
                    string sub = "";
                    string body = "";
                    string attch = "";
                    string acManger = "";
                    string ISSComment = "";
                    string AMC = "";
                    string UWC = "";
                    string requestType = "";
                    string CAsign = "";
                    string asignPrint = "";
                    string asignQC = "";
                    string closeNotes = "";
                    string ClientName = "";
                    string pricingcomments = "";
                    string accountcomments = "";
                    string NumberOfCards = "";
                    string IssNumOfCards = "";
                    string Qualitycomments = "";
                    string ExceptionComm = "";
                    string AMExceptionConf = "";
                    string UWExceptionConf = "";
                    string AccSrc = "";
                    string rColor = "";
                    string CComment = "";
                    string CAction = "";
                    mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

                    if (requestType != "Cancellation" && requestType != "Suspend" && requestType != "Unsuspend" && requestType != "Exception-Cancellation")
                    {
                        if (requestType == "Renewal" || requestType == "New Company")
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                  Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                                }
                                catch
                                {
                                    mdb.SendEmailBackupFail();
                                }

                                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssing(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;

                            }
                            else
                            {

                                // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                                //string src = "~/Issuance/" + FileUpload1.FileName;
                                mdb.UpdateIssing2(id, user, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }

                        }
                        else
                        {

                            //  string yy = Textarea1.Value;
                            //    string yy = TextBox1.Text.Trim();
                            if (FileUpload1.HasFile)
                            {
                                int PATHid = int.Parse(Request.QueryString["id"]);
                                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                                try
                                {
                                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                                }
                                catch
                                {
                                    mdb.SendEmailBackupFail();
                                }

                                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                                mdb.UpdateIssingNoTAPV(id, user, src, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                            else
                            {

                                // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                                //string src = "~/Issuance/" + FileUpload1.FileName;
                                mdb.UpdateIssing2NOTAPV(id, user, TextBox1.Text, IssCardsNum.Text);
                                user = Request.Cookies["name"].Value;
                                id = int.Parse(Request.QueryString["id"]);
                                mdb.Updateprinting(id, user);
                                btn_issue.Disabled = true;
                                TextBox1.ReadOnly = true;
                            }
                        }

                    }
                    else
                    {
                        mdb.UpdateCancelIssuning(id, user, TextBox1.Text, IssCardsNum.Text);
                        btn_issue.Disabled = true;
                        TextBox1.ReadOnly = true;

                    }
                    btn_issue.Disabled = true;
                    btn_iss_print.Disabled = true;
                    btn_reject.Disabled = true;
                    btn_Close.Disabled = true;
                    btn_transfare.Disabled = true;
                    btn_download.Disabled = true;
                    btn_Original_download.Disabled = true;
                    DropDownList1.Enabled = false;
                    btn_PTechnical.Visible = false;
                    div1.Visible = false;
                    Response.Redirect("RequestNew.aspx", false);
                }
                else
                {
                    lbl_num_booklet.Visible = true;
                }
            }
           
            else
            {
                div1.Visible = true;
            }

        }  //finished
        protected void Apv_Issu_Close(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
         
            
            if (IssCardsNum.Text != "")
            {
                string user = Request.Cookies["name"].Value;
               
                if (txtNumberOfBookelt.ReadOnly == false)
                {
                    mdb.AddNumberOfBooklet(id, txtNumberOfBookelt.Text);
                }
                string mm = TextBox1.Text;
                //  string yy = Textarea1.Value;
                if (FileUpload1.HasFile)
                {
                    int PATHid = int.Parse(Request.QueryString["id"]);
                    Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                    FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                    try
                    {
                        Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                        FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                        Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                        FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                    }
                    catch
                    {
                        mdb.SendEmailBackupFail();
                    }

                    string src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

                    mdb.UpdateIssingClose(id, user, src, TextBox1.Text, IssCardsNum.Text);
                    btn_Close.Disabled = true;
                    btn_issue.Disabled = true;
                    btn_reject.Disabled = true;
                    TextBox1.ReadOnly = true;
                }
                else
                {

                    // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                    //string src = "~/Issuance/" + FileUpload1.FileName;
                    mdb.UpdateCancelIssuning(id, user, TextBox1.Text, IssCardsNum.Text);
                    btn_Close.Disabled = true;
                    btn_issue.Disabled = true;
                    btn_reject.Disabled = true;
                    TextBox1.ReadOnly = true;
                }

                btn_issue.Disabled = true;
                btn_iss_print.Disabled = true;
                btn_reject.Disabled = true;
                btn_Close.Disabled = true;
                btn_transfare.Disabled = true;
                btn_download.Disabled = true;
                btn_Original_download.Disabled = true;
                DropDownList1.Enabled = false;
                btn_PTechnical.Visible = false;
                div1.Visible = false;
                Response.Redirect("RequestNew.aspx", false);

            }
            else
            {
                div1.Visible = true;
            }
        }    //finished
        protected void ApvACconfirm(object sender, EventArgs e)
        {
            // code here for confirm
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateACM(id);
            btn_AMConfirm.Disabled = true;
            btn_back_to_Issuance.Disabled = true;
            string AMC = "";
            string UWC = "";
            string src = "";
            string sub = "";
            string body = "";
            string attch = "";
            string acManger = "";
            string ISSComment = "";

            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string closeNotes = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";


            mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

            mdb.GetConfirmByid(id, ref AMC, ref UWC);
            if (requestType == "Renewal" || requestType == "New Company")
            {
                if (AMC == "true" && UWC == "true")
                {
                    mdb.UpdateConfirmTWO(id, user);
                    Response.Redirect("RequestNew.aspx", false);

                }
            }

        }
        protected void ApUWconfrim(object sender, EventArgs e)
        {
            // code here for confirm
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            string AMC = "";
            string UWC = "";
            mdb.UpdateUWM(id, user);
            string src = "";
            string sub = "";
            string body = "";
            string attch = "";
            string acManger = "";
            string ISSComment = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string closeNotes = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            btn_back_to_Issuance.Disabled = true;
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";

            mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);
            mdb.GetConfirmByid(id, ref AMC, ref UWC);
            btn_UWcconfirm.Disabled = true;
            if (requestType == "Renewal" || requestType == "New Company")
            {
                if (AMC == "true" && UWC == "true")
                {
                    mdb.UpdateConfirmTWO(id, user);
                    Response.Redirect("RequestNew.aspx", false);

                }
            }
        }

        ///////////////////////////////////////jjjjjjjjjjjjjjjjj/////////////////////////////////
        protected void AMExceptionConfirm(object sender, EventArgs e)
        {
            // code here for confirm
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateExcACM(id, user);
            btn_AMExceptionConfirm.Disabled = true;
            btn_reject.Disabled = true;
            string AMC = "";
            string UWC = "";
            string src = "";
            string sub = "";
            string body = "";
            string attch = "";
            string acManger = "";
            string ISSComment = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string closeNotes = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";
            mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

            mdb.GetConfirmExcByid(id, ref AMExceptionConf, ref UWExceptionConf);
            if (requestType == "Exceptional-Addition" || requestType == "Exceptional-Transfer" || requestType == "Exception-Cancellation" || requestType == "Exception-Modification")
            {
                if (AMExceptionConf == "true" && UWExceptionConf == "true")
                {
                    mdb.UpdateExcConfirmTWO(id, user);
                    Response.Redirect("RequestNew.aspx", false);

                }
            }

        }
        protected void IssExceptionConfirm(object sender, EventArgs e)
        {
      

            // code here for confirm
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.AddNumberOfBooklet(id, txtNumberOfBookelt.Text);
            string AMC = "";
            string UWC = "";
            mdb.UpdateExcUWM(id, user);
            string src = "";
            string sub = "";
            string body = "";
            string attch = "";
            string acManger = "";
            string ISSComment = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string closeNotes = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";

            mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);
            mdb.GetConfirmExcByid(id, ref AMExceptionConf, ref UWExceptionConf);
            btn_IssExceptionConfirm.Disabled = true;
            btn_reject.Disabled = true;
            if (requestType == "Exceptional-Addition" || requestType == "Exceptional-Transfer" || requestType == "Exception-Cancellation" || requestType == "Exception-Modification")
            {
                if (AMExceptionConf == "true" && UWExceptionConf == "true")
                {
                    mdb.UpdateExcConfirmTWO(id, user);
                    Response.Redirect("RequestNew.aspx", false);

                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        protected void BackToIssuance(object sender, EventArgs e)
        {
            // code here for confirm
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            btn_AMConfirm.Disabled = true;
            btn_UWcconfirm.Disabled = true;
            btn_back_to_Issuance.Disabled = true;
            string AMC = "";
            string UWC = "";
            string src = "";
            string sub = "";
            string body = "";
            string attch = "";
            string acManger = "";
            string ISSComment = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string closeNotes = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";

            mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);
            accountcomments = accountcom.Text;
            pricingcomments = pricingcom.Text;
            mdb.UpdateRequest(id, accountcomments, pricingcomments);
            Response.Redirect("RequestNew.aspx", false);

        }
        protected void ApvPTech(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
          
            string mm = TextBox1.Text;
            string src = "";
            string sub = "";
            string body = "";
            string attch = "";
            string acManger = "";
            string ISSComment = "";
            string AMC = "";
            string UWC = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string closeNotes = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";


            mdb.GetDetailByid(id, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

            if (FileUpload1.HasFile)
            {
                int PATHid = int.Parse(Request.QueryString["id"]);
                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                try
                {
                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                }
                catch
                {
                    mdb.SendEmailBackupFail();
                }

                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;
                mdb.UpdatePTech(id, user, src, TextBox1.Text);
                btn_issue.Disabled = true;
                TextBox1.ReadOnly = true;
            }
            else
            {

                // FileUpload1.SaveAs(Server.MapPath("~/Issuance/") + FileUpload1.FileName);
                //string src = "~/Issuance/" + FileUpload1.FileName;
                mdb.UpdatePTech2(id, user, TextBox1.Text);
                btn_issue.Disabled = true;
                TextBox1.ReadOnly = true;
            }

            btn_issue.Disabled = true;
            btn_PTechnical.Disabled = true;
            btn_iss_print.Disabled = true;
            btn_reject.Disabled = true;
            btn_Close.Disabled = true;
            btn_transfare.Disabled = true;
            btn_download.Disabled = true;
            btn_Original_download.Disabled = true;
            DropDownList1.Enabled = false;
            div1.Visible = false;
            Response.Redirect("P_Technical.aspx", false);
        }

        ////////////////////////////////////////////////////////////
        protected void ApvPrint(object sender, EventArgs e)
        {
            string state = Request.QueryString["state"];
            if (state == "Printing")
            {
                string user = Request.Cookies["name"].Value;
                int id = int.Parse(Request.QueryString["id"]);
             
                mdb.Updateprinting(id, user);
                btn_print.Disabled = true;
                btn_download.Disabled = true;
                btn_Original_download.Disabled = true;
                btn_trnsfare_print.Visible = false;
                DropDownList1.Visible = false;
                Response.Redirect("RequestNew.aspx", false);

            }
            //Response.ContentType = "file/rar";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + lbl_Sub.InnerHtml + "" + user + ".rar");
            //Response.TransmitFile(Server.MapPath(src));
            //Response.End();
        }
        protected void ApvQC(object sender, EventArgs e)
        {
            if (IssCardsNum.Text != "")
            {
                string type = Request.Cookies["type"].Value;
                string state = Request.QueryString["state"];
                if (state == "Quality Control")
                {
                    string user = Request.Cookies["name"].Value;
                    int id = int.Parse(Request.QueryString["id"]);
                    mdb.UpdateQC(id, user, IssCardsNum.Text, QCcom.Text);
                    btn_QC.Disabled = true;
                    btn_download.Disabled = true;
                    btn_Original_download.Disabled = true;
                    btn_transfare_Qc.Visible = false;
                    DropDownList1.Visible = false;

                    if (type == "QualityControl")
                    {
                        drop_quality.Visible = false;
                        //Response.Redirect("QCRequetes.aspx", false);
                    }
                    else { Response.Redirect("RequestNew.aspx", false);
                      
                    }

                }
            }
            else
            {
                div1.Visible = true;
            }

        }
        protected void ApvColse(object sender, EventArgs e)
        {
            int idM = int.Parse(Request.QueryString["id"]);
            string sub = "";
            string body = "";
            string state = Request.QueryString["state"];
            string attch = "";
            string acManger = "";
            string closeNotes = "";
            string src = "";
            string ISSComment = "";
            string AMC = "";
            string UWC = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string asignQC = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";
            mdb.GetDetailByid(idM, ref sub, ref body, ref src, ref attch, ref acManger, ref ISSComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            string comment2 = text2.Text;
            mdb.UpdateClose(id, user, comment2);
            btn_Mang.Disabled = true;
            Response.Redirect("RequestNew.aspx", false);


        }
        protected void ApvColse_ByISS(object sender, EventArgs e)
        {
            int idM = int.Parse(Request.QueryString["id"]);
            string sub = "";
            string body = "";
            string state = Request.QueryString["state"];
            string attch = "";
            string acManger = "";
            string isssuComment = "";
            string closeNotes = "";
            string asignQC = "";
            string src = "";
            string AMC = "";
            string UWC = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";
            mdb.GetDetailByid(idM, ref sub, ref body, ref src, ref attch, ref acManger, ref isssuComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);

            mdb.UpdateClose(id, user, text2.Text);
            btn_Close.Disabled = true;

            Response.Redirect("RequestNew.aspx", false);

        }
        protected void ApvReject(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
        
            string mm = TextBox1.Text;
            string src = "";
            if (FileUpload1.HasFile)
            {
                int PATHid = int.Parse(Request.QueryString["id"]);
                Directory.CreateDirectory(Server.MapPath("~/Issuance/" + PATHid + "/"));
                FileUpload1.SaveAs(Server.MapPath("~/Issuance/" + PATHid + "/") + FileUpload1.FileName);

                try
                {
                    Directory.CreateDirectory("E:\\Issuance\\" + PATHid + "\\");
                    FileUpload1.SaveAs("E:\\Issuance\\" + PATHid + "\\" + FileUpload1.FileName);

                    Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/");
                    FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Issuance\" + PATHid + "/" + FileUpload1.FileName);
                }
                catch
                {
                    mdb.SendEmailBackupFail();
                }

                src = "~/Issuance/" + PATHid + "/" + FileUpload1.FileName;

            }
            else
            {
                src = "";
            }

            mdb.updateReject(id, mm, src);
            btn_issue.Disabled = true;
            btn_iss_print.Disabled = true;
            btn_reject.Disabled = true;
            btn_Close.Disabled = true;
            btn_transfare.Disabled = true;
            btn_download.Disabled = true;
            btn_Original_download.Disabled = true;
            DropDownList1.Enabled = false;
            btn_PTechnical.Visible = false;
            div1.Visible = false;
            Response.Redirect("RequestNew.aspx", false);

        }
        protected void ApvRejectSeen(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.updateRejectSeen(id);
            btn_reject_Seen.Disabled = true;
            btn_fw.Disabled = true;
            Response.Redirect("RequestNew.aspx", false);
        }

        protected void AssignToMe(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            btn_Close.Disabled = true;
            btn_reject.Visible = true;
            btn_reject.Disabled = true;
            btn_issue.Disabled = true;
            btn_iss_print.Disabled = true;
            //btn_print.Disabled = false;
            mdb.updateAssign(id);
            mdb.updateAssignPerson(id, user);
            btn_asign.Disabled = true;
            Server.Transfer("ManageCycle.aspx");
        }
        protected void Transfare(object sender, EventArgs e)
        {
            string user = DropDownList1.SelectedValue;
            int id = int.Parse(Request.QueryString["id"]);
            btn_Close.Visible = false;
            btn_reject.Visible = false;
            btn_issue.Visible = false;
            btn_iss_print.Visible = false;
            btn_asign.Visible = false;
            btn_download.Visible = false;
            btn_Original_download.Visible = false;
            DropDownList1.Visible = false;
            btn_transfare.Visible = false;
            btn_PTechnical.Visible = false;
            mdb.updateAssign(id);
            mdb.updateAssignPerson(id, user);
            Server.Transfer("ManageCycle.aspx");
        }
        protected void AssignToMePrint(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.updateAssign(id);
            mdb.updateAssignPrint(id, user);
            btn_asignPrint.Disabled = true;
            Server.Transfer("ManageCycle.aspx");
            // btn_print.Disabled = false;
            // btn_download.Disabled = false;
        }
        protected void btn_Change_StatusServerClick(object sender, EventArgs e)
        {
            string user1 = Request.Cookies["name"].Value;
            string user = DropDownList1.SelectedValue;
            int id = int.Parse(Request.QueryString["id"]);
            bool bol = mdb.Check_status(id);
            if (bol == true)
            {
                if (StatusDropDown.SelectedItem.Text == "Printing" || StatusDropDown.SelectedItem.Text == "Quality Control")
                {
                    if (IssCardsNum.Text != "")
                    {
                        if (div_NumberOfBookelt.Visible == true)
                        {
                            if (txtNumberOfBookelt.Text == "")
                            {
                                lbl_num_booklet.Visible = true;
                                btn_Change_Status.Visible = true;
                                StatusDropDown.Visible = true;
                            }
                            else
                            {
                                mdb.updateIssuanceStatus(id, StatusDropDown.SelectedItem.ToString(), txtNumberOfBookelt.Text, int.Parse(IssCardsNum.Text));
                                Response.Redirect("~/RequestNew.aspx");
                            }
                        }
                        else
                        {
                            mdb.changeStatusNumOfCard(id, StatusDropDown.SelectedItem.ToString(), int.Parse(IssCardsNum.Text));
                            Response.Redirect("~/RequestNew.aspx");
                        }
                    }
                    else
                    {
                        div1.Visible = true;
                        btn_Change_Status.Visible = true;
                        StatusDropDown.Visible = true;
                    }
                }
                else
                {
                    mdb.changeStatus(id, StatusDropDown.SelectedItem.ToString());
                    Response.Redirect("~/RequestNew.aspx");
                }
            }
            else
            {
                mdb.changeStatus(id, StatusDropDown.SelectedItem.ToString());
                Response.Redirect("~/RequestNew.aspx");
            }
        }

        protected void Transfare_print(object sender, EventArgs e)
        {
            string user = DropDownList1.SelectedValue;
            int id = int.Parse(Request.QueryString["id"]);
            btn_asignPrint.Visible = false;
            btn_download.Visible = false;
            btn_Original_download.Visible = false;
            DropDownList1.Visible = false;
            btn_trnsfare_print.Visible = false;
            btn_print.Visible = false;
            mdb.updateAssign(id);
            mdb.updateAssignPrint(id, user);
            Server.Transfer("ManageCycle.aspx");
        }
        protected void AssignToMeQC(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.updateAssign(id);
            mdb.updateAssignQc(id, user);
            btn_AsignQC.Disabled = true;
            Server.Transfer("ManageCycle.aspx");
        }
        protected void Transfare_QC(object sender, EventArgs e)
        {
            string type = Request.Cookies["type"].Value;
            string user = DropDownList1.SelectedValue;
            if (type == "QualityControl")
            {
                user = drop_quality.SelectedItem.Text;
            }
                
            int id = int.Parse(Request.QueryString["id"]);
            btn_AsignQC.Visible = false;
            btn_download.Visible = false;
            btn_Original_download.Visible = false;
            DropDownList1.Visible = false;
            btn_transfare_Qc.Visible = false;
            btn_QC.Visible = false;
            mdb.updateAssign(id);
            mdb.updateAssignQc(id, user);
            Server.Transfer("ManageCycle.aspx");
        }
        protected void btn_fw_ServerClick(object sender, EventArgs e)
        {
            int idM = int.Parse(Request.QueryString["id"]);
            string sub = "";
            string bodyx = "";
            string state = Request.QueryString["state"];
            string attch = "";
            string acManger = "";
            string isssuComment = "";
            string closeNotes = "";
            string asignQC = "";
            string src = "";
            string AMC = "";
            string UWC = "";
            string requestType = "";
            string CAsign = "";
            string asignPrint = "";
            string ClientName = "";
            string pricingcomments = "";
            string accountcomments = "";
            string NumberOfCards = "";
            string IssNumOfCards = "";
            string Qualitycomments = "";
            string ExceptionComm = "";
            string AMExceptionConf = "";
            string UWExceptionConf = "";
            string AccSrc = "";
            string rColor = "";
            string CComment = "";
            string CAction = "";


            mdb.GetDetailByid(idM, ref sub, ref bodyx, ref src, ref attch, ref acManger, ref isssuComment, ref AMC, ref UWC, ref requestType, ref CAsign, ref asignPrint, ref asignQC, ref closeNotes, ref ClientName, ref accountcomments, ref pricingcomments, ref NumberOfCards, ref IssNumOfCards, ref Qualitycomments, ref ExceptionComm, ref AMExceptionConf, ref UWExceptionConf, ref AccSrc, ref rColor, ref CComment, ref CAction);

            try
            {
                mdb.updateRejectSeen(idM);
                btn_reject_Seen.Disabled = true;
                btn_fw.Disabled = true;

                string file = attch;
                string file2 = AccSrc;
                string cards = NumberOfCards;
                Response.Cookies["txtBody"].Expires = DateTime.Now.AddDays(-1);
                HttpCookie body = new HttpCookie("txtBody");
                body.Value = txtrBody.InnerText;
                Response.Cookies.Add(body);
                Response.Redirect("AcManagerPanel.aspx?id=" + Label1.InnerText + " &lbl=" + lbl_Sub.InnerText + "&lbl2=" + Label2.InnerText + "&attach=" + attch + "&Cardsss=" + NumOfCardsByACC.InnerText + "&accattach=" + AccSrc + "");

            }
            catch (Exception)
            {

            }
        }
        protected void Complain_Action(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);

            EnterCompComments.Visible = true;
            ComplainComments.Visible = true;
            btn_transfare.Visible = false;
            btn_trnsfare_print.Visible = false;
            btn_transfare_Qc.Visible = false;
            btn_asign.Visible = false;
            btn_asignPrint.Visible = false;
            btn_AsignQC.Visible = false;
            btn_Complain.Visible = false;
            ExcepCarea.Visible = false;
            IssuanceCommentsArea.Visible = false;
            AccountBackCommentsArea.Visible = false;
            PricingBackCommentsArea.Visible = false;
            QualityControlCommentsArea.Visible = false;
            ClosingCommentsArea.Visible = false;
            btn_iss_print.Visible = false;
            btn_issue.Visible = false;
            btn_print.Visible = false;
            btn_PTechnical.Visible = false;
            btn_QC.Visible = false;
            btn_back_to_Issuance.Visible = false;
            btn_AMConfirm.Visible = false;
            btn_UWcconfirm.Visible = false;
            btn_AMExceptionConfirm.Visible = false;
            btn_IssExceptionConfirm.Visible = false;
            btn_Mang.Visible = false;
            btn_reject.Visible = false;
            btn_reject_Seen.Visible = false;
            btn_Close.Visible = false;
            btn_fw.Visible = false;
            AccountManagerCommentsArea.Visible = false;
            FinalNumberOfMembersArea.Visible = false;
            downloadarea.Visible = false;
            rejectComplain.Visible = false;
            acceptComplain.Visible = false;
            AddComplain.Visible = true;
        }
        protected void ApvAddComplain(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.AddAccountComplain(id, user, ComplaintextEntrance.Value);
            AddComplain.Visible = false;


        }
        protected void ApvrejectComplain(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.AddIssuanceComplain(id, user, ComplaintextEntrance.Value);
            rejectComplain.Visible = false;
            acceptComplain.Visible = false;

        }
        protected void ApvAcceptComplain(object sender, EventArgs e)
        {
            string user = Request.Cookies["name"].Value;
            int id = int.Parse(Request.QueryString["id"]);
            mdb.CloseComplain(id, user);
            rejectComplain.Visible = false;
            acceptComplain.Visible = false;

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drop_quality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}