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

            //-----------------------------------------------------------------------
            bool userHaveResetPassword = helpers.CheckResetPasswordUser(UserID);
            bool userHaveDeActivate = helpers.CheckDeActivateUser(UserID);
            if(userHaveResetPassword)
            {
                btnResetPasswordUser.Visible = false;

            }
            else
            {
                btnResetPasswordUser.Visible = true;

            }
            if (userHaveDeActivate)
            {
                btnDeActivateUser.Visible = false;
                btnActiveUser.Visible = true;
            }
            else
            {
                btnDeActivateUser.Visible = true;
                btnActiveUser.Visible = false;

            }
            //-----------------------------------------------------------------------

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

        protected void ResetPasswordUser_Click(object sender, EventArgs e)
        {
            var result = helpers.ResetPasswordUser(UserID);
            if(result==true)
            {
                btnResetPasswordUser.Visible = false;
            }
            else
            {
                btnResetPasswordUser.Visible = true;

            }
        }

        protected void DeActivateUser_Click(object sender, EventArgs e)
        {
            var result = helpers.DeActivateUser(UserID);
            if (result == true)
            {
                btnDeActivateUser.Visible = false;
                btnActiveUser.Visible = true;
            }
            else
            {
                btnDeActivateUser.Visible = true;
                btnActiveUser.Visible = false;

            }
        }

        protected void ActiveUser_Click(object sender, EventArgs e)
        {
            var result = helpers.ActivateUser(UserID);
            if (result == true)
            {
                btnDeActivateUser.Visible = true;
                btnActiveUser.Visible = false;
            }
            else
            {
                btnDeActivateUser.Visible = false;
                btnActiveUser.Visible = true;

            }
        }
    }
}