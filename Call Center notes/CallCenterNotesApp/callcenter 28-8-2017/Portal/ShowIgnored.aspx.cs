using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Drawing;

namespace CallCenterNotesApp.Portal
{
    public partial class ShowIgnored : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();
        PhNetworkEntities Db = new PhNetworkEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowIgnoredApprovals")).Attributes["class"] = "active";
            if (!IsPostBack)
            { ViewState["ViewPageNo"] = 0;
                Bind(DropDownType.SelectedValue, DropDownSubType.SelectedValue, Convert.ToInt16(DropDownAsc.SelectedValue));

            }
            else
            {

                Bind(DropDownType.SelectedValue, DropDownSubType.SelectedValue, Convert.ToInt16(DropDownAsc.SelectedValue));
            }
        }
        public void Bind(string Type,string SubType, int? asc)
        {
            try
            {
                Panel1.Controls.Clear();


                Helpers helper = new Helpers();

                string user = Request.Cookies["UserName"].Value;
                string type = Request.Cookies["UserType"].Value;
                var data = helper.GetIgnoredRequestsByUserName(user, Type, SubType, asc).Take(20).ToList();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();

                //create pageing

                DashboardSearchAndPaging pageing = new DashboardSearchAndPaging();
                int NumberOfPages = pageing.PageNumbers(20, helper.GetIgnoredRequestsByUserNameCount(user));
                ViewState["ViewAllPageNumber"] = NumberOfPages;
                for (int i = 0; i < NumberOfPages; i++)
                {
                   
                    Button ButtonChange = new Button();

                    ButtonChange.Text = i.ToString();
                    ButtonChange.ID = "change_" + i.ToString();
                    ButtonChange.Font.Size = FontUnit.Point(7);
                    ButtonChange.ControlStyle.CssClass = "button";
                    ButtonChange.Click += ButtonChange_Click; ;
                    Panel1.Controls.Add(ButtonChange);
                  
                }
            }
            catch { }







        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Button btn = (Button)sender;
            ChangePage(btn.Text);
            ViewState["currentpage"] = btn.Text;

        }
        private void ChangePage(string PageNumber)
        {
            DashboardSearchAndPaging pageing = new DashboardSearchAndPaging();
            Helpers helper = new Helpers();
            lstNewReq.DataSource = pageing.PageItems(20, int.Parse(PageNumber),
                helper.GetIgnoredRequestsByUserName(Request.Cookies["UserName"].Value, 
                DropDownType.SelectedValue,DropDownSubType.SelectedValue 
                ,Convert.ToInt16(DropDownAsc.SelectedValue)));
            lstNewReq.DataBind();
        }




        protected void UpdateColors_Click(object sender, EventArgs e)
        {
          



        }



        protected void Next_Click(object sender, EventArgs e)
        {
           
        }

        protected void Befor_Click(object sender, EventArgs e)
        {
           

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            if(DropDownType.SelectedValue == "N/A")
            {
                List<int> table = new List<int>();
                DropDownSubType.DataSource = table;
                DropDownSubType.DataBind();
            }

            if (DropDownType.SelectedValue == "Priority" )
            {
                DropDownSubType.DataTextField = "Priority";
                DropDownSubType.DataValueField = "ID";

                DropDownSubType.DataSource= Db.EmailApprovalsPriorityDIMs.Select(x => x).ToList();
                DropDownSubType.DataBind();

                ListItem item = new ListItem("N/A", string.Empty);
                DropDownSubType.Items.Add(item);
                item.Selected = true;
            }


            if (DropDownType.SelectedValue == "ApprovalCategoty")
            {
                DropDownSubType.DataTextField = "CategoryName";
                DropDownSubType.DataValueField = "ID";

                DropDownSubType.DataSource = Db.EmailApprovalsCategoryDIMs.Select(x => x).ToList();
                DropDownSubType.DataBind();
                ListItem item = new ListItem("N/A", string.Empty);
                DropDownSubType.Items.Add(item);
                item.Selected = true;

            }


            if (DropDownType.SelectedValue == "Time" )
            {
                List<int> table = new  List<int>();
                DropDownSubType.DataSource= table;
                DropDownSubType.DataBind();
            }


            if (DropDownType.SelectedValue == "TicketType" )
            {
                DropDownSubType.DataTextField = "Name";
                DropDownSubType.DataValueField = "ID";
                
                DropDownSubType.DataSource = Db.TicketTypes.Select(x => x).ToList();
                DropDownSubType.DataBind();
                ListItem item = new ListItem("N/A", string.Empty);
                DropDownSubType.Items.Add(item);
                item.Selected=true;
            }



















            Bind(DropDownType.SelectedValue, DropDownSubType.SelectedValue, Convert.ToInt16(DropDownAsc.SelectedValue));

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind(DropDownType.SelectedValue, DropDownSubType.SelectedValue, Convert.ToInt16(DropDownAsc.SelectedValue));

        }

        protected void DropDownSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind(DropDownType.SelectedValue,DropDownSubType.SelectedValue, Convert.ToInt16(DropDownAsc.SelectedValue));

        }
    }
}
