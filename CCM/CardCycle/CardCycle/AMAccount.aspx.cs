using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class AMAccount : System.Web.UI.Page
    {
        Account mdb = new Account();
        protected void Page_Load(object sender, EventArgs e)
        {
            div_success.Visible = false;
            div_Error.Visible = false;
            if (!IsPostBack)
            {
           //     mdb.BindDDLUser(DropDownList2);
                filData();
           //     DropDownList2.Text = "";
            }
        }
        public void filData()
        {
            string name = "";
            string pass = "";
            string type = "";
            //txt_name.Value = "";
            //txt_pass.Value = "";
            string user = Request.Cookies["name"].Value;
            mdb.BAckAllUser(user, ref name, ref pass, ref type);
            txt_name.Value = name;
            txt_pass.Text = pass.ToString();
        //    DropDownList1.Text = type;
        }
        protected void updateuser(object sender, EventArgs e)
        {
            try
            {
                //   mdb.BindDDLUser(DropDownList2);
                if (txt_name.Value!= "")
                {
                    string user = Request.Cookies["name"].Value;
                    mdb.UpdateUserBYUser(user, txt_name.Value, txt_pass.Text);
                    div_success.Visible = true;
                  //  clear();
                   // mdb.BindDDLUser(DropDownList2);
                }
            }
            catch (Exception)
            {

            }
        }

    }
}