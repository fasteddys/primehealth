using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class RejectedSub : System.Web.UI.Page
    {
        subRequests mdb = new subRequests();

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            string user = Request.Cookies["nameark"].Value;
            
            if (type == "User" || type == "Remb")
            {
                Response.Redirect("/Login.aspx");
            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span17")).Attributes["class"] = "icon-thumbnail bg-success";
                if (!IsPostBack)
                {
                    Bind();
                }
            }

        }

        private void Bind()
        {
            string type = Request.Cookies["typeark"].Value;
            string user = Request.Cookies["nameark"].Value;
            if (type == "Data Entry")
            {
                var data = mdb.GetAllReqRejectedByDataEntry(user);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
           else if (type == "EnterpriseAdmin" || type == "Admin" || type == "Archiving" || type == "ArcAdmin" || type=="TPA" || type=="TPAAdmin")
            {
                var data = mdb.GetAllReqRejected();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }

        
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }
    }