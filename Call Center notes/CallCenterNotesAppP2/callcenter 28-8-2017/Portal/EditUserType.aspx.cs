using CallCenterNotesApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenterNotesApp.Portal
{
    public partial class EditUserType : System.Web.UI.Page
    {
        Helpers helpers = new Helpers();
        int UserID ;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserID = Convert.ToInt32(Request.QueryString["ID"]);
            div_Error.Visible = false;
            div_Success.Visible = false;

            var user = helpers.GetUserByID(UserID);

            UserName.Value = user.UserName;
            UserType.Value = user.UserType;
        }

        protected void SubmitEdit_Click(object sender, EventArgs e)
        {
            string ValidationResult = "";

            var result = helpers.ChangeUserTypeByUserID(UserID, Dropdownlist1.SelectedValue,out ValidationResult);
            if (result==true)
            {
                div_Success.Visible = true;
                div_Success.InnerText = ValidationResult;
            }
            if (result==false)
            {
                div_Error.Visible = true;
                div_Error.InnerText = ValidationResult;
            }
            
        }
    }
}