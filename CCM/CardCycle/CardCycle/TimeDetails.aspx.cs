using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class TimeDetails : System.Web.UI.Page
    {
        Request mdb = new Request();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label5.InnerHtml = Request.QueryString["id"].ToString();
            int id =int.Parse(Request.QueryString["id"].ToString());
            txtType.Text = Request.QueryString["type"].ToString();
            string sub = "";
            string recived = "";
           
            string apviss="";
            string apvprint="";
            string apvqc="";
            string lbl1 = "";
            string lbl2 = "";
            string lbl3= "";
            string lbl4 = "";
            string lbl5 = "";
            string lbl6 = "";
            string lbl7 = "";
            string lbl8 = "";
            string lbl9 = "";
            string lbl10 = "";
            string lbl11 = "";
            string lbl12 = "";
            mdb.GetTimeData(id, ref sub, ref recived, ref apviss, ref apvprint, ref apvqc, ref lbl1, ref lbl2, ref lbl3, ref lbl4, ref lbl5, ref lbl6,ref lbl7, ref lbl8, ref lbl9, ref lbl10, ref lbl11,ref lbl12);
            txtSubject.Text = sub;
            txtAM.Text = recived;
            lbel1.Text = lbl1;//asign time
            lbel2.Text = lbl2;//end asign time
            lbel3.Text = lbl3;//asifn print time
            lbel4.Text = lbl4;//end asign print time
            lbel5.Text = lbl5;//asign q Time
            lbel6.Text = lbl6;//end a Qtime
            lbel7.Text = lbl7;//asign date
            lbel8.Text = lbl8;//end asign date
            lbel9.Text = lbl9;//a print date
            lbel10.Text = lbl10;//end asign print date
            lbel11.Text = lbl11;//asign q date
            lbel12.Text = lbl12;//end asign q date
            if (apviss != "Null")
            {
                apvissu.Text = apviss;
            }
            if (apvprint != "Null")
            {
                apvP.Text = apvprint;
            }
            if (apvqc != "Null")
            {
                txtQ.Text = apvqc;
            }
            
           
        }
    }
}