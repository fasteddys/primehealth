using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardCycle.BLL;


namespace CardCycle
{
    public partial class Login : System.Web.UI.Page
    {
        //DataContextDataContext db = new DataContextDataContext();
        Account mdb = new Account();
        protected void Page_Load(object sender, EventArgs e)
        {
            //int userId = Convert.ToInt32(request.session("userId"));
            //ReportBrowserClosed(userId);
            divError.Visible=false;
        }
        protected void Login_Auth(object sender, EventArgs e)
        {
            string type = "";
            int id = 0;
            bool b = mdb.LogiAuth(txtName.Value,txtPass.Value,ref type,ref id);
            if (b == true)
            {
                if (type == "Account Manager")
                {
                    Response.Cookies["name"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["type"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uID"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("name");
                    cName.Value = txtName.Value.Trim();
                    Response.Cookies.Add(cName);
                    //  Application["name"] = txtName.Value;
                    HttpCookie ctype = new HttpCookie("type");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);
                    //Application["type"] = type;
                    HttpCookie cID = new HttpCookie("uID");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);
                    // Application["uID"] = id;
                    mdb.updateON(id);
                    Response.Redirect("AcManagerPanel.aspx");

                }
                else if (type == "Underwriting")
                {
                    Response.Cookies["name"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["type"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uID"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("name");
                    cName.Value = txtName.Value.Trim();
                    Response.Cookies.Add(cName);
                    //  Application["name"] = txtName.Value;
                    HttpCookie ctype = new HttpCookie("type");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);
                    //Application["type"] = type;
                    HttpCookie cID = new HttpCookie("uID");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);
                    // Application["uID"] = id;
                    mdb.updateON(id);
                    Response.Redirect("PConfirm.aspx");
                    // Response.Redirect("");
                    //Server.Transfer("AcManagerPanel.aspx");
                }
                else if (type == "QualityControl")
                {
                    Response.Cookies["name"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["type"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uID"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("name");
                    cName.Value = txtName.Value.Trim();
                    Response.Cookies.Add(cName);
                    //  Application["name"] = txtName.Value;
                    HttpCookie ctype = new HttpCookie("type");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);
                    //Application["type"] = type;
                    HttpCookie cID = new HttpCookie("uID");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);
                    // Application["uID"] = id;
                    mdb.updateON(id);
                    Response.Redirect("QCRequetes.aspx");
                }
                else
                {
                    Response.Cookies["name"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["type"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uID"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("name");
                    cName.Value = txtName.Value.Trim();
                    Response.Cookies.Add(cName);
                    //  Application["name"] = txtName.Value;
                    HttpCookie ctype = new HttpCookie("type");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);
                    //Application["type"] = type;
                    HttpCookie cID = new HttpCookie("uID");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);
                    // Application["uID"] = id;
                    mdb.updateON(id);
                    Response.Redirect("ITCotrolPanel.aspx");
                }
                
            }
            else
            {
                divError.Visible = true;
            }

        }
    }
}