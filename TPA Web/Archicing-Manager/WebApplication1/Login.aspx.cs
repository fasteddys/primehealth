using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        Accounts mdb = new Accounts();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                divError.Visible = false;
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string name = UserTxt.Text;
            string pass = PassTxt.Text;
            int id = 0;
            string type="";
            bool login = mdb.LogiAuth(name, pass, ref type , ref id);

            if(login == true)
            {
                if (type == "Admin")
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
                else if (type == "User" || type == "TBAUser")
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
                    if (type == "User")
                    {
                        Response.Redirect("Portal/NewRequest.aspx");
                    }
                    else if (type == "TBAUser")
                    {
                        Response.Redirect("Portal/PendingConfirmation.aspx");
                    }


                }
                else if(type == "DataEntry")
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
                else if (type == "ArchAdmin")
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
                    Response.Redirect("Portal/ArchivingPanel.aspx");

                }
            }
            else
            {
                divError.Visible = true;
            }
        }
    }
}