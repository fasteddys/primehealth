using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class Stop : System.Web.UI.Page
    {
        public static Stopwatch sw;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sw = new Stopwatch();
                sw.Start();
            }

        }
        protected void tm1_Tick(object sender, EventArgs e)
        {
            long sec = 60;
            sec = sw.Elapsed.Seconds;
            long min = 5;
            min = sw.Elapsed.Minutes;
            Label2.Text = sec.ToString();
            Label3.Text = min.ToString();
           
        }
    }
}