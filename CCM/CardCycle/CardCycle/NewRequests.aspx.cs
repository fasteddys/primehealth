using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardCycle.BLL;


namespace CardCycle
{
    public partial class NewRequests : System.Web.UI.Page
    {
        Request mdb = new Request();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Bind();
            }
            
        }
        protected void Refresh(object sender, EventArgs e)
        {

            Bind();

        }
        public void Bind()
        { 
        var data= mdb.GetAllReqNew();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
        }
        // call data every 10000 mss
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
        //private Timer timer1;
        //public void InitTimer()
        //{
        //    //Timer timer1 = new Timer();
        //    timer1 = new Timer();
        //    timer1.Tick += new EventHandler(timer1_Tick);
        //    timer1.Interval = 2000; // in miliseconds
        //    timer1.Start();
        //}
        
    }
}