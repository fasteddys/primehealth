using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class DailyReport : System.Web.UI.Page
    {
        Account db = new Account();
        Request mdb = new Request();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // db.BindDDLUserReport(DropDownList2);
                db.BindDDlReport(DropDownList2);
                DropDownList_stats.Items.Add("ALL");
                DropDownList_stats.Items.Add("Closed");
                DropDownList_stats.Items.Add("Rejected");
                DropDownList_stats.Items.Add("Issuing");
                DropDownList_stats.Items.Add("Printing");
                DropDownList_stats.Items.Add("Quality Control");
                DropDownList_stats.Items.Add("Pending Technical");
                DropDownList_stats.Items.Add("pending confirmation");
                DropDownList_stats.Items.Add("Exception Pending");
                //
                DropDownList_team.Items.Add("QulityControl");
                DropDownList_team.Items.Add("Issuance");
            }
            
        }
       

        protected void btn_reject_ServerClick(object sender, EventArgs e)
        {
          var data=mdb.GetAllDaiyReport(txtDate.Value,DropDownList2.Text.Trim());
          
          lstNewReq.DataSource = data;
          lstNewReq.DataBind();
          int x = mdb.CountIssuance(txtDate.Value, DropDownList2.Text.Trim());
          Label1.Text = x.ToString();
        }

        protected void btn_reject_ServerClick1(object sender, EventArgs e)
        {
            //IFormatProvider p ;
            //p.GetFormat);
           
            DateTime from = Convert.ToDateTime(txtDate.Value);

            DateTime to = Convert.ToDateTime(TextBox1.Text);

            //CultureInfo provider = CultureInfo.InvariantCulture;

            //DateTime d2 = DateTime.ParseExact(txtDate.Value.ToString(), "m/d/yyyy",provider);
            //DateTime d = DateTime.ParseExact(TextBox1.Text.ToString(), "M/d/yyyy", provider);


            var data = mdb.GetAllReports(from, to,DropDownList_stats.Text, DropDownList2.Text.Trim());
            
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();
            int x = mdb.CountAllIssuance(from, to,DropDownList_stats.Text,DropDownList2.Text.Trim());
            Label1.Text = x.ToString();
        }

        protected void btn_reject_ServerClick2(object sender, EventArgs e)
        {
            //IFormatProvider p ;
            //p.GetFormat);
            DateTime from = Convert.ToDateTime(Text1.Value);

            DateTime to = Convert.ToDateTime(TextBox2.Text);
          //  CultureInfo provider = CultureInfo.InvariantCulture;

            //DateTime from = DateTime.ParseExact(txtDate.Value, "MM/dd/yyyy", provider);
            //DateTime to = DateTime.ParseExact(TextBox1.Text, "MM/dd/yyyy", provider);


            var dat= mdb.Get_Reports(from, to, DropDownList_team.Text);



            ListView1.DataSource = dat;
            ListView1.DataBind();
            //int x = mdb.CountAllIssuance(d, d2, DropDownList_stats.Text, DropDownList2.Text.Trim());
            //Label1.Text = x.ToString();
        }


        protected void btn_reject_ServerClick3(object sender, EventArgs e)
        {
            //IFormatProvider p ;
            //p.GetFormat);
            DateTime from = Convert.ToDateTime(Text2.Value);
         
            DateTime to = Convert.ToDateTime(TextBox3.Text);
            //  CultureInfo provider = CultureInfo.InvariantCulture;

            //DateTime from = DateTime.ParseExact(txtDate.Value, "MM/dd/yyyy", provider);
            //DateTime to = DateTime.ParseExact(TextBox1.Text, "MM/dd/yyyy", provider);


            var dat = mdb.Get_cards_Report(from, to);



            ListView2.DataSource = dat;
            ListView2.DataBind();
            //int x = mdb.CountAllIssuance(d, d2, DropDownList_stats.Text, DropDownList2.Text.Trim());
            //Label1.Text = x.ToString();
        }
        //protected void btn_reject_ServerClick2(object sender, EventArgs e)
        //{
        //    DateTime d = Convert.ToDateTime(txtDate.Value);
        //    DateTime d2 = Convert.ToDateTime(TextBox1.Text);
        //    //DateTime dat1 = DateTime.ParseExact(txtDate.Value.ToString(), "MM/dd/yyyy", null);
        //    //DateTime dat2 = DateTime.ParseExact(TextBox1.Text.ToString(), "MM/dd/yyyy", null);

        //    //DateTime from = new DateTime();
        //    //from = DateTime.Parse(txtDate.Value);

        //    //DateTime to = new DateTime();
        //    //to = DateTime.Parse(TextBox1.Text);
        //    //CultureInfo provider = CultureInfo.InvariantCulture;

        //    //DateTime from = DateTime.ParseExact(txtDate.Value.ToString(), "MM/dd/yyyy", new CultureInfo("en-US"));
        //    //DateTime to = DateTime.ParseExact(TextBox1.Text.ToString(), "MM/dd/yyyy", new CultureInfo("en-US"));

        //    //IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);

        //    //DateTime from = DateTime.ParseExact(txtDate.Value, "mm/dd/yyyy", theCultureInfo);

        //    //DateTime to = DateTime.ParseExact(TextBox1.Text, "mm/dd/yyyy", theCultureInfo);

        //    var data = mdb.Get_Reports(d, d2, DropDownList_team.Text );

        //    ListView1.DataSource = data;
        //    ListView1.DataBind();
        //   // int x = mdb.CountAllIssuance(d, d2, DropDownList_stats.Text, DropDownList2.Text.Trim());
        //   // Label1.Text = x.ToString();
        //}
    }
}