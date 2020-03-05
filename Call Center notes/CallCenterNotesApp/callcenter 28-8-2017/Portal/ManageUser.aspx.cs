using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace CallCenterNotesApp.Portal
{
    public partial class ManageUser : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        ClientNotesClass mdb = new ClientNotesClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string UserNamestr = "";

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                div_Error_update.Visible = false;
                UserPassword1.Value = "";
                UserPassword2.Value = "";

                string type = Request.Cookies["UserType"].Value.Trim();
                string UName = Request.Cookies["UserName"].Value.Trim();
                int UID = int.Parse(Request.Cookies["UserID"].Value.Trim());

                if (type == "CallCenterDoctor" || type == "CallCenterUser") 
                {
                    mdb.GetUserDataByid(UID, ref UserNamestr);
                    btn_DeleteUserData.Visible=false;
                }
                else if (type == "CallCenterManager")
                {
                    mdb.GetUserDataByid(UID, ref UserNamestr);
                    btn_DeleteUserData.Visible=false;
                }
                else
                {
                    try
                    {
                        int IDs = int.Parse(Request.QueryString["id"]);
                        mdb.GetUserDataByid(IDs, ref UserNamestr);
                    }
                    catch
                    {
                        mdb.GetUserDataByid(UID, ref UserNamestr);
                        btn_DeleteUserData.Visible = false;
                    }
                }
                UserName.InnerText = UserNamestr;

            }


        }
        protected void UpdateUserData_ServerClick(object sender, EventArgs e)
        {
            string type = Request.Cookies["UserType"].Value.Trim();
            string UName = Request.Cookies["UserName"].Value.Trim();
            int UID = int.Parse(Request.Cookies["UserID"].Value.Trim());
            if (type == "CallCenterUser" || type == "CallCenterDoctor")
            {
                if (UserPassword1.Value == UserPassword2.Value)
                {
                    mdb.UpdateUserData(UID, UserPassword1.Value);
                    div_Success_update.Visible = true;
                    div_Error_update.Visible = false;

                }
                else
                {
                    div_Error_update.Visible = true;
                    div_Success_update.Visible = false;

                }
            }
            else
            {
                try
                {
                    int IDs = int.Parse(Request.QueryString["id"]);
                    if (UserPassword1.Value == UserPassword2.Value)
                    {
                        mdb.UpdateUserData(IDs, UserPassword1.Value);
                        div_Success_update.Visible = true;
                        div_Error_update.Visible = false;

                    }
                    else
                    {
                        div_Error_update.Visible = true;
                        div_Success_update.Visible = false;
                    }
                }
                catch
                {
                    if (UserPassword1.Value == UserPassword2.Value)
                    {
                        mdb.UpdateUserData(UID, UserPassword1.Value);
                        div_Success_update.Visible = true;
                        div_Error_update.Visible = false;

                    }
                    else
                    {
                        div_Error_update.Visible = true;
                        div_Success_update.Visible = false;

                    }
                }
            }

        }
        protected void DeleteUserData_ServerClick(object sender, EventArgs e)
        {
             int IDs = int.Parse(Request.QueryString["id"]);
             mdb.DeleteUserData(IDs);
             Response.Redirect("~/Portal/ViewNotes.aspx");
        }

    }
}