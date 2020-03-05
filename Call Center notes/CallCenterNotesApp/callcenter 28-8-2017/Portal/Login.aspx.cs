using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using static CallCenterNotesApp.Enums.Enums;
using System.Net;
using System.Net.NetworkInformation;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.Portal
{
    public partial class Login : System.Web.UI.Page
    {
        Account mdb = new Account();
        Helpers helper = new Helpers();
        PhNetworkEntities db = new PhNetworkEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divError.Visible = false;
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string name = UserTxt.Text;
            string pass = PassTxt.Text;
            int id = 0;
            string type = "";
            bool? IsFirstLogin=false;
            bool login = mdb.LogiAuth(name, pass, ref type, ref id,ref IsFirstLogin);
            //if (Request.Browser.Type.Contains("Chrome"))
            //{
                if (login == true)
                {

                    Response.Cookies["IsFirstLogin"].Expires = DateTime.Now.AddDays(-1);

                    HttpCookie IsFirst = new HttpCookie("IsFirstLogin");
                    IsFirst.Value = IsFirstLogin.ToString();
                    Response.Cookies.Add(IsFirst);
                string IPAddress = Request.Cookies["IPAddress"].Value;
                if (type == "Administrator")
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);

                        HttpCookie cName = new HttpCookie("UserName");
                        cName.Value = UserTxt.Text.Trim().ToLower();
                        Response.Cookies.Add(cName);

                        HttpCookie ctype = new HttpCookie("UserType");
                        ctype.Value = type.Trim();
                        Response.Cookies.Add(ctype);

                        HttpCookie cID = new HttpCookie("UserID");
                        cID.Value = id.ToString().Trim();
                  
                        Response.Cookies.Add(cID);
                        try
                        {
                            helper.UpdateLoginAndLogOut(int.Parse(cID.Value), (int)LogType.LogIn, IPAddress);
                            helper.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.LogIn, id, name, "", IPAddress, "Login");
                        }
                        catch { }
                        Response.Redirect("~/Portal/AddUser.aspx");
                    }
                    else if (type == "CallCenterManager" || type == "CallCenterAuditDoctor")
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
                        HttpCookie cName = new HttpCookie("UserName");
                        cName.Value = UserTxt.Text.Trim().ToLower();
                        Response.Cookies.Add(cName);

                        HttpCookie ctype = new HttpCookie("UserType");
                        ctype.Value = type.Trim();
                        Response.Cookies.Add(ctype);

                        HttpCookie cID = new HttpCookie("UserID");
                        cID.Value = id.ToString().Trim();



                        Response.Cookies.Add(cID);
                        try
                        {
                            string IP = Request.UserHostName.ToString();

                            helper.UpdateLoginAndLogOut(int.Parse(cID.Value), (int)LogType.LogIn, IPAddress);
                            helper.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.LogIn, id, name, "", IPAddress, "Login");
                        }
                        catch { }
                        Response.Redirect("~/Portal/UnAssignedEmailApprovals.aspx");


                    }
                    else if (type == "CallCenterDoctor")
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
                        HttpCookie cName = new HttpCookie("UserName");
                        cName.Value = UserTxt.Text.Trim().ToLower();
                        Response.Cookies.Add(cName);

                        HttpCookie ctype = new HttpCookie("UserType");
                        ctype.Value = type.Trim();
                        Response.Cookies.Add(ctype);

                        HttpCookie cID = new HttpCookie("UserID");
                        cID.Value = id.ToString().Trim();
                        Response.Cookies.Add(cID);

                        try
                        {
                            string mac = helper.GetMacAdress();
                            helper.UpdateLoginAndLogOut(int.Parse(cID.Value), (int)LogType.LogIn, mac);
                            helper.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.LogIn, id, name, "", mac, "Login");
                        }
                        catch
                        {

                        }
                        Response.Redirect("~/Portal/UnAssignedEmailApprovals.aspx");



                    }
                    else if (type == "CallCenterUser")
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
                        HttpCookie cName = new HttpCookie("UserName");
                        cName.Value = UserTxt.Text.Trim().ToLower();
                        Response.Cookies.Add(cName);

                        HttpCookie ctype = new HttpCookie("UserType");
                        ctype.Value = type.Trim();
                        Response.Cookies.Add(ctype);

                        HttpCookie cID = new HttpCookie("UserID");
                        cID.Value = id.ToString().Trim();
                        Response.Cookies.Add(cID);

                        try
                        {
                            string mac = helper.GetMacAdress();
                            helper.UpdateLoginAndLogOut(int.Parse(cID.Value), (int)LogType.LogIn, mac);
                            helper.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.LogIn, id, name, "", mac, "Login");
                        }
                        catch { }

                        Response.Redirect("~/Portal/UnAssignedEmailApprovals.aspx");



                    }
                    else if (type == "DirectorUser")
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
                        HttpCookie cName = new HttpCookie("UserName");
                        cName.Value = UserTxt.Text.Trim().ToLower();
                        Response.Cookies.Add(cName);

                        HttpCookie ctype = new HttpCookie("UserType");
                        ctype.Value = type.Trim();
                        Response.Cookies.Add(ctype);

                        HttpCookie cID = new HttpCookie("UserID");
                        cID.Value = id.ToString().Trim(); ;
                        Response.Cookies.Add(cID);
                        try
                        {
                            helper.UpdateLoginAndLogOut(int.Parse(cID.Value), (int)LogType.LogIn, IPAddress);
                            helper.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.LogIn, id, name, "", IPAddress, "Login");
                        }
                        catch { }
                        Response.Redirect("~/Portal/SowALLToManager.aspx?ID=1");
                    }
                    else if (type == "ProviderUser")
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
                        HttpCookie cName = new HttpCookie("UserName");
                        cName.Value = UserTxt.Text.Trim().ToLower();
                        Response.Cookies.Add(cName);

                        HttpCookie ctype = new HttpCookie("UserType");
                        ctype.Value = type.Trim();
                        Response.Cookies.Add(ctype);

                        HttpCookie cID = new HttpCookie("UserID");
                        cID.Value = id.ToString().Trim(); ;
                        Response.Cookies.Add(cID);

                        Response.Redirect("~/Portal/ProviderDashboard.aspx");
                    }
                    else if (type == "ShowGroupsUser")
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
                        HttpCookie cName = new HttpCookie("UserName");
                        cName.Value = UserTxt.Text.Trim().ToLower();
                        Response.Cookies.Add(cName);

                        HttpCookie ctype = new HttpCookie("UserType");
                        ctype.Value = type.Trim();
                        Response.Cookies.Add(ctype);

                        HttpCookie cID = new HttpCookie("UserID");
                        cID.Value = id.ToString().Trim(); ;
                        Response.Cookies.Add(cID);
                        List<int> GroupIDs = db.EmailApprovalsGroup_User.Where(x => x.UserName == cName.Value).Select(x => x.GroupID).ToList();
                        if (GroupIDs.Count > 0)
                        {
                            Response.Redirect("~/Portal/ShowAllMailRequestForGroup.aspx?ID=" + GroupIDs.FirstOrDefault());
                        }
                        else
                        {
                            divError.Visible = true;

                        }
                    }

                }

                else
                {
                    divError.Visible = true;
                }
           // }
            //else
            //{
            //    helper.LogEmailApprovalEvent(null, (int)EmailApprovalLogTypes.FalseBrowserLogin, id, name, "Not Logged In", "Tried To Login From "+ Request.Browser.Type.ToString() + " ");

            //    divError.Visible = true;
            //    divError.InnerText = "Please Open System From Google Chrome";

            //}

        }
      

        public static string DetermineCompName(string IP)
        {
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }
        private static string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                // Machine not found...
            }
            return machineName;
        }







    }
}