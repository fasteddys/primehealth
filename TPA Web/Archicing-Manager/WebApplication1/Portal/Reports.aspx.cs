using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class Reports : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        string word = "";
        protected void Page_Load(object sender, EventArgs e)
        {
             string type = Request.Cookies["typeark"].Value;
            if (!IsPostBack)
            {

                Calendar1.Visible = false;
                Calendar2.Visible = false;
                div_Error1.Visible = false;
                Number_of_tickets_DEntry.Visible = true;
                Number_of_Claims_DEntry.Visible = false;
                Return_tickets_Qc.Visible = false;
                Return_tickets_TPA.Visible = false;
                btn_search_Qc.Visible=false;
                btn_search_Tpa.Visible=false;
                if(type == "Admin")
                {
                    btn_search_Qc.Visible = true;
                    btn_search_Tpa.Visible = true;
                }
                
            }

        }
        protected void Refresh(object sender, EventArgs e)
        {

            //Bind();

        }
        public void Bind()
        {

            //int num;
            //bool isNum = int.TryParse(word, out num);
            //if (isNum)  // search by ticket number
            //{
            //    var data = mdb.GetAllSearchBID(word);
            //    ItemsList.DataSource = data;
            //    ItemsList.DataBind();
            //}
            //else if (word != "")       // search by name from text view
            //{
            //    var data = mdb.GetAllSearch(word);
            //    ItemsList.DataSource = data;
            //    ItemsList.DataBind();
            //}

        }

        protected void btn_Search_ServerClick(object sender, EventArgs e)
        {
            try
            {
                var data = mdb.GetAllSearchByDateEntryReport(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);

                int countertickets = mdb.Counter_Search_DataEntry_Req(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                    lbl_Tottal_tickets.InnerHtml = "( " + countertickets.ToString() + " )";
                    int counterclaims = mdb.Sum_Search_DataEntry_Req(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                    lbl_count_Found_Claims.InnerHtml = "( " + counterclaims.ToString() + " )";
                    ItemsList.DataSource = data;
                    ItemsList.DataBind();
                    div_Error1.Visible = false;
                    Number_of_Claims_DEntry.Visible = true;
                    Return_tickets_Qc.Visible = false;
                    Return_tickets_TPA.Visible = false;



            }
            catch(Exception ex)
            {
                div_Error1.Visible=true;
                lbl_count_Found_Claims.InnerText = "( 0 )";
                Return_tickets_Qc.Visible = false;
                Return_tickets_TPA.Visible = false;
            }
        }
        protected void btn_SearchQc_ServerClick(object sender, EventArgs e)
        {
            try
            {
                var data = mdb.GetAllSearchBy_QC_Tpa_Report(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                int countertickets = mdb.CounterSearch_QC_Tpa_Req(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                lbl_Tottal_tickets.InnerHtml = "( " + countertickets.ToString() + " )";
                int counterclaims = mdb.Sum_Search_QControl_Req(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                lbl_ReturnQc.InnerHtml = "( " + counterclaims.ToString() + " )";
                ItemsList.DataSource = data;
                ItemsList.DataBind();
                div_Error1.Visible = false;
                Number_of_Claims_DEntry.Visible = false;
                Return_tickets_Qc.Visible = true;
                Return_tickets_TPA.Visible = false;


            }
            catch (Exception ex)
            {
                div_Error1.Visible = true;
                lbl_ReturnQc.InnerText = "( 0 )";
                Number_of_Claims_DEntry.Visible = false;
                Return_tickets_TPA.Visible = false;
                Return_tickets_Qc.Visible = true;

            }
        }
        protected void btn_SearchTPA_ServerClick(object sender, EventArgs e)
        {
            try
            {
                var data = mdb.GetAllSearchBy_QC_Tpa_Report(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                int countertickets = mdb.CounterSearch_QC_Tpa_Req(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                lbl_Tottal_tickets.InnerHtml = "( " + countertickets.ToString() + " )";
                int counterclaims = mdb.Sum_Search_TPA_Req(Emp.SelectedItem.ToString(), Calendar1.SelectedDate, Calendar2.SelectedDate);
                lbl_ReturnTPA.InnerHtml = "( " + counterclaims.ToString() + " )";
                ItemsList.DataSource = data;
                ItemsList.DataBind();
                div_Error1.Visible = false;
                Number_of_Claims_DEntry.Visible = false;
                Return_tickets_Qc.Visible = false;
                Return_tickets_TPA.Visible = true;


            }
            catch (Exception ex)
            {
                div_Error1.Visible = true;
                lbl_ReturnTPA.InnerText = "( 0 )";
                Number_of_Claims_DEntry.Visible = false;
                Return_tickets_Qc.Visible = false;
                Return_tickets_TPA.Visible = true;

            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible==true || Calendar2.Visible==true)
                Calendar1.Visible = false;
            else
                Calendar1.Visible = true;
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = Calendar1.SelectedDate.ToLongDateString();
            Calendar1.Visible = false;
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar2.Visible == true || Calendar1.Visible == true)
                Calendar2.Visible = false;
            else
                Calendar2.Visible = true;
        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            TextBox2.Text = Calendar2.SelectedDate.ToLongDateString();
            Calendar2.Visible = false;

        }
    }
}