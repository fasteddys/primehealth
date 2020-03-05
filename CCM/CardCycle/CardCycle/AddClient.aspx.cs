using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class AddClient : System.Web.UI.Page
    {
        Client CL = new Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            div_success.Visible = false;
            div_Error.Visible = false;
        }
        protected void AddClient_ServerClick(object sender, EventArgs e)
        {
            string ActiveValue = "";
            if (ActivateClients.Checked) { ActiveValue = "1"; }
            else { ActiveValue = "0"; }

            bool add = CL.AddNewClient(input_text.Value, txtrBody.Value, ActiveValue , DropDownType.SelectedItem.ToString());
            if(add)
            {
                div_success.Visible = true;
                Clean();
            }
            else
            {
                div_Error.Visible = true;
            }
        }

        protected void Clean()
        {
            input_text.Value = "";
            txtrBody.Value = "";
            ActivateClients.Checked = false;
        }
    }
}