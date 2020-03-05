using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class AccountManger : System.Web.UI.Page
    {
        Account mdb = new Account();

        protected void Page_Load(object sender, EventArgs e)
        {
            div_success.Visible = false;
            div_Error.Visible = false;
            if (!IsPostBack)
            {
               mdb.BindDDLUser(DropDownList2);
               filData();
               DropDownList2.Text = "";
            }
           
        }
        protected void Adduser(object sender, EventArgs e)
        {
            try
            {
                // mdb.BindDDLUser(DropDownList2);
                //if (DropDownList2.Text == "")
                //{
                    mdb.adduser(txt_name.Value, txt_pass.Text, DropDownList1.Text);
                    div_success.Visible = true;
                    clear();
                    mdb.BindDDLUser(DropDownList2);
                //}
           

            }
            catch (Exception)
            { 
            
            }

        }
        protected void updateuser(object sender, EventArgs e)
        {
            try { 
         //   mdb.BindDDLUser(DropDownList2);
            if (DropDownList2.Text != "")
            {
                mdb.UpdateUser(DropDownList2.Text,txt_name.Value, txt_pass.Text, DropDownList1.Text);
                div_success.Visible = true;
                clear();
                mdb.BindDDLUser(DropDownList2);
            }
            }
            catch (Exception)
            {

            }
        }
        protected void DeleteUser(object sender, EventArgs e)
        {
            try
            { 
           // mdb.BindDDLUser(DropDownList2);
            if (DropDownList2.Text != "")
            {
                mdb.DeleteUser(DropDownList2.Text);
                div_success.Visible = true;

                clear();
                mdb.BindDDLUser(DropDownList2);
            }
            }
            catch (Exception)
            {

            }
        }
        public void filData()
        {
            string name = "";
            string pass = "";
            string type = "";
            //txt_name.Value = "";
            //txt_pass.Value = "";
            mdb.BAckAllUser(DropDownList2.Text,ref name, ref pass, ref type);
            txt_name.Value = name;
            txt_pass.Text = pass.ToString();
            DropDownList1.Text = type;
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filData();
        }
        public void clear()
        {
            txt_name.Value = "";
            txt_pass.Text = "";
        }
    }
}