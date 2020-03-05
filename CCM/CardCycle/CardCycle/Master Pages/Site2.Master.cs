using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle.Master_Pages
{
    public partial class Site2 : System.Web.UI.MasterPage
    {
        Request mdb = new Request();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!IsPostBack)
                {
                    int num1=mdb.Tot_NewREQ();
                    int num2=mdb.Tot_REQ();
                    int num3 = mdb.Tot_IssREQ();
                    int num4 = mdb.Tot_PCREQ();
                    int num5 = mdb.Tot_PrintREQ();
                    int num6 = mdb.Tot_QCREQ();
                    int num7 = mdb.Tot_PAREQ();
                    int num8 = mdb.Tot_ClosedREQ();
                    int num9 = mdb.Tot_REGREQ();
                    int num25 = mdb.Tot_REGREQSeen();
                    int num10 = mdb.Tot_PTREQ();
                    int exceptions = mdb.Tot_ExcepREQ();
                   
                    ///////////
                    spn_Excep.Visible = false;
                    spn_exc.Visible = false;
                    spn_IssuanceComplain.Visible = false;
                    spn_AccountComplain.Visible = false;
                    Con.Visible = false;


                    ///////////
                    Li3.Visible = false;
                    Li5.Visible = false;
                    Lireport.Visible = false;
                    Li6.Visible = false;
                    if (Request.Cookies["Name"].Value.Trim() != null)
                    {
                        string user_ac = Request.Cookies["Name"].Value.Trim();

                        Label1.Text = Request.Cookies["Name"].Value.Trim();
                        string type = Request.Cookies["type"].Value.Trim();
                        int ConfirmedUW = mdb.Tot_Con_UW(user_ac);
                        int UWPending = mdb.Tot_PCREQ_ac(user_ac);
                        Li_account.Visible = false;
                        Li2.Visible = false;
                        if (type == "Account Manager")
                        {

                            if (Label1.Text == "David.Maher"|| Label1.Text == "Riham.Morcos")
                            {
                                spn_Excep.Visible = true;
                                spn_exc.Visible = true;
                            }
                            //spn_num1.Visible = false;
                            //Spn_num2.Visible = false;
                            //spn_num3.Visible = false;
                            //Span1.Visible = false;
                            //Span2.Visible = false;
                            //Span3.Visible = false;
                            //Span4.Visible = false;
                            //Span5.Visible = false;
                            //Span6.Visible = false;
                            //AddClientLi.Visible = false;
                            
                            int tnr = mdb.Tot_NewREQ_ac(user_ac);
                            spn_num1.InnerHtml = tnr.ToString();
                            int trzc = mdb.Tot_REQ_ac(user_ac);
                            Spn_num2.InnerHtml = trzc.ToString();
                            spn_num3.InnerHtml = num3.ToString();
                           int tpa =mdb.Tot_PCREQ_ac(user_ac);
                            
                            Span1.InnerHtml = tpa.ToString();
                            

                            //add
                            spn_AccountComplain.Visible = true;
                            spn_IssuanceComplain.Visible = false;
                            spn_num11.InnerHtml = num10.ToString();
                            spn_exc.InnerHtml = exceptions.ToString();

                            int ComplainAcc = mdb.Tot_Complain_ac(user_ac);
                            spn_AccComp.InnerHtml = ComplainAcc.ToString();


                            ///////

                            Span2.InnerHtml = num5.ToString();
                            Span3.InnerHtml = num6.ToString();
                            int tpac = mdb.Tot_PAREQ_ac(user_ac);
                            Span4.InnerHtml = tpac.ToString();
                            int tpacz = mdb.Tot_ClosedREQ_ac(user_ac);
                            Span5.InnerHtml = tpacz.ToString();
                            int tpaczk = mdb.Tot_REGREQ_ac(user_ac);
                            Span6.InnerHtml = tpaczk.ToString();
                            int ReqSeenAcc = mdb.Tot_REGREQSeen_ac(user_ac);
                            Span7.InnerHtml = ReqSeenAcc.ToString();
                            ClientsLi.Visible = false;
                            ////////////////////
                            spn_das.InnerHtml = "Add Request";
                            spn_new.Visible = false;
                            //spn_All.Visible = false;
                            spn_Issue.Visible = false;
                            spn_print.Visible = false;
                            spn_qc.Visible = false;
                            Li3.Visible = true;
                            Li5.Visible = true;
                            
                            //btn_issue.Visible = false;
                            //btn_print.Visible = false;
                            //btn_QC.Visible = false;
                            //btn_Mang.Disabled = true;
                            //if (state == "Closing")
                            //{
                            //    btn_Mang.Disabled = false;
                            //}
                        }
                        else if (type == "Underwriting")
                        {
                            // spn_das.InnerHtml = "Add Request";
                            //added
                            spn_PTech.Visible = false;
                            Span8.InnerHtml = ConfirmedUW.ToString();
                            Span1.InnerHtml = UWPending.ToString();
                            spn_num11.InnerHtml = num10.ToString();
                            spn_dash.Visible = false;
                            spn_new.Visible = false;
                            spn_pend.Visible = false;
                            //spn_All.Visible = false;
                            spn_Issue.Visible = false;
                            spn_print.Visible = false;
                            spn_qc.Visible = false;
                            spn_All.Visible = false;
                            spn_close.Visible = false;
                            AddClientLi.Visible = false;
                            ClientsLi.Visible = false;
                            Con.Visible = true;
                           // Server.Transfer("AcManagerPanel.aspx");
                           // Response.Redirect("");
                        }
                      
                        else if (type == "Issuance" || type == "It")
                        {
                            Li3.Visible = true;
                            Li5.Visible = true;
                            Li2.Visible = true;

                            if (Label1.Text == "admin" )
                            {
                                spn_Excep.Visible = true;
                                spn_exc.Visible = true;
                            }
                            if (Label1.Text == "Meriam.Mikhael"|| Label1.Text== "A.Magdi")
                            {
                                Lireport.Visible = true;
                                spn_Excep.Visible = true;
                                spn_exc.Visible = true;
                                Li6.Visible = true;
                            }


                          //  Li1.Visible = false;
                            string user_Iss = Request.Cookies["Name"].Value.Trim();
                            int ComplainIss = mdb.Tot_Complain_Iss(user_Iss);
                            spn_IssComp.InnerHtml = ComplainIss.ToString();
                            spn_num1.InnerHtml = num1.ToString();
                            Spn_num2.InnerHtml = num2.ToString();
                            spn_num3.InnerHtml = num3.ToString();
                            Span1.InnerHtml = num4.ToString();
                            Span2.InnerHtml = num5.ToString();
                            Span3.InnerHtml = num6.ToString();
                            Span4.InnerHtml = num7.ToString();
                            Span5.InnerHtml = num8.ToString();
                            Span6.InnerHtml = num9.ToString();
                            Span7.InnerHtml = num25.ToString();
                            //added
                            spn_AccountComplain.Visible = false;
                            spn_IssuanceComplain.Visible = true;

                            spn_num11.InnerHtml = num10.ToString();
                            spn_exc.InnerHtml = exceptions.ToString();

                        }

                        else if (type == "QualityControl")
                        {
                            spn_num1.InnerHtml = num1.ToString();
                            Spn_num2.InnerHtml = num2.ToString();
                            spn_num3.InnerHtml = num3.ToString();
                            Span1.InnerHtml = num4.ToString();
                            Span2.InnerHtml = num5.ToString();
                            Span3.InnerHtml = num6.ToString();
                            Span4.InnerHtml = num7.ToString();
                            Span5.InnerHtml = num8.ToString();
                            Span6.InnerHtml = num9.ToString();
                            Span7.InnerHtml = num25.ToString();


                            //btn_QC.Disabled = true;
                            //q-spn_qc   spn_close   Li4
                            //if (state == "Quality Control")
                            //{
                            //    btn_QC.Disabled = false;
                            //}
                            spn_dash.Visible = false;
                            spn_das.Visible = false;
                            spn_All.Visible = false;
                            spn_Excep.Visible = false;
                            spn_new.Visible = false;
                            spn_Issue.Visible = false;
                            spn_PTech.Visible = false;
                            Li1.Visible = false;
                            Con.Visible = false;
                           
                        
                         
                            Li3.Visible = false;
                            Li5.Visible = false;
                            spn_AccountComplain.Visible = false;
                            spn_IssuanceComplain.Visible = false;
                            ClientsLi.Visible = false;
                            AddClientLi.Visible = false;
                            Li2.Visible = false;

                            Li4.Visible = false;
                            Lireport.Visible = false;
                            Li_account.Visible = false;

                            spn_pend.Visible = true;
                            spn_qc.Visible = true;
                            spn_print.Visible = true;

                            spn_close.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                    if (Label1.Text == "admin" || Label1.Text == "Mohamed.Abdelsattar")
                    {
                        Li_account.Visible = true;
                        Li2.Visible = true;
                        Lireport.Visible = true;
                        Li4.Visible = false;
                    }

                   
                }
            }
            catch (Exception)
            {
                Response.Redirect("login.aspx");
            }
  
            

        }
        protected void Choose_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            string txt = Request.Cookies["type"].Value.Trim();
                if (txt == "Account Manager")
                {
                    //Application["name"] = txtName.Value;
                    //Application["type"] = type;
                    //mdb.updateON(id);
                    Response.Redirect("AcManagerPanel.aspx");

                }
            else if (txt == "Underwriting")
                {
                    Server.Transfer("AcManagerPanel.aspx");
                }
                else
                {
                    //Application["name"] = txtName.Value;
                    //Application["type"] = type;
                    //mdb.updateON(id);
                    Response.Redirect("ITCotrolPanel.aspx");
                }
            //}


        }
        protected void Log_out(object sender, EventArgs e)
        {
            Response.Cookies["name"].Expires = DateTime.Now.AddSeconds(1.00);
            Response.Cookies["type"].Expires = DateTime.Now.AddSeconds(1.00);
            Response.Cookies["uID"].Expires = DateTime.Now.AddSeconds(1.00);
            Account ac = new Account();
            int id = int.Parse(Request.Cookies["uID"].Value.Trim());
            ac.updateOFF(id);
            //HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            //HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            //HttpContext.Current.Response.AddHeader("Expires", "0");

            Response.Redirect("login.aspx");
        }
        protected void GoSearch(object sender, EventArgs e)
        {
            //string s = txt_search.Value;
            //Response.Redirect("SearchForm.aspx?word="+txt_search.Value+"");
        }
    }
}