using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;
using WebApplication1.DLL;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        Accounts mdb = new Accounts();
        userTB user = new userTB();
        string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["UserType"] = type;
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
            bool login = mdb.LogiAuth(name, pass, ref type, ref id);

            if (login == true)
            {
                if (type == "Admin" || type == "ArcAdmin")
                {
                    
                    

                    Response.Cookies["nameark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["typeark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uIDark"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("nameark");
                    cName.Value = UserTxt.Text.Trim();
                    Response.Cookies.Add(cName);

                    HttpCookie ctype = new HttpCookie("typeark");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);

                    HttpCookie cID = new HttpCookie("uIDark");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);

                    mdb.updateON(id);
                    Response.Redirect("Portal/AllNewRequests.aspx");
                }

               else if (type == "TPAAdmin")
                {


                    Response.Cookies["nameark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["typeark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uIDark"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("nameark");
                    cName.Value = UserTxt.Text.Trim();
                    Response.Cookies.Add(cName);

                    HttpCookie ctype = new HttpCookie("typeark");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);

                    HttpCookie cID = new HttpCookie("uIDark");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);

                    mdb.updateON(id);
                    Response.Redirect("Portal/NewSubmiReq.aspx");
                }
                else if (type == "User" || type == "Remb" || type=="Data Entry")
                {
                    HttpContext.Current.Session["UserType"] = type;
                    Response.Cookies["nameark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["typeark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uIDark"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("nameark");
                    cName.Value = UserTxt.Text.Trim();
                    Response.Cookies.Add(cName);

                    HttpCookie ctype = new HttpCookie("typeark");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);

                    HttpCookie cID = new HttpCookie("uIDark");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);

                    mdb.updateON(id);
                    Response.Redirect("Portal/NewRequest.aspx");

                }
                else if (type == "Archiving")
                {
                    HttpContext.Current.Session["UserType"] = type;
                    Response.Cookies["nameark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["typeark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uIDark"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("nameark");
                    cName.Value = UserTxt.Text.Trim();
                    Response.Cookies.Add(cName);

                    HttpCookie ctype = new HttpCookie("typeark");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);

                    HttpCookie cID = new HttpCookie("uIDark");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);

                    mdb.updateON(id);
                    Response.Redirect("Portal/AllNewRequests.aspx");

                }
                    else if(type=="TPA")
                {

                    Session["UserType"] = type;
                    
                    Response.Cookies["nameark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["typeark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uIDark"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("nameark");
                    cName.Value = UserTxt.Text.Trim();
                    Response.Cookies.Add(cName);

                    HttpCookie ctype = new HttpCookie("typeark");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);

                    HttpCookie cID = new HttpCookie("uIDark");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);

                    mdb.updateON(id);
                    Response.Redirect("Portal/NewSubmiReq.aspx");
                
                }

                else if (type == "EnterpriseAdmin")
                {
                    HttpContext.Current.Session["UserType"] = type;
                    Response.Cookies["nameark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["typeark"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uIDark"].Expires = DateTime.Now.AddDays(-1);
                    HttpCookie cName = new HttpCookie("nameark");
                    cName.Value = UserTxt.Text.Trim();
                    Response.Cookies.Add(cName);

                    HttpCookie ctype = new HttpCookie("typeark");
                    ctype.Value = type.Trim();
                    Response.Cookies.Add(ctype);

                    HttpCookie cID = new HttpCookie("uIDark");
                    cID.Value = id.ToString().Trim(); ;
                    Response.Cookies.Add(cID);

                    mdb.updateON(id);
                    Response.Redirect("Portal/Reports.aspx");
                }
            }
            else
            {
                divError.Visible = true;
            }
        }
    }
}