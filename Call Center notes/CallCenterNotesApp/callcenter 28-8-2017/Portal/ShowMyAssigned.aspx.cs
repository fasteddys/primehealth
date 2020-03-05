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
    public partial class ShowMyAssigned : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();
        PhNetworkEntities Db = new PhNetworkEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowMyAssigned")).Attributes["class"] = "active";
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
          
                Panel1.Controls.Clear();


                Helpers helper = new Helpers();

                string user = Request.Cookies["UserName"].Value;
                string type = Request.Cookies["UserType"].Value;
                var data = helper.GetAssignedTicketByUserName(user, Type, SubType, asc).Take(20).ToList();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();

                //create pageing

                DashboardSearchAndPaging pageing = new DashboardSearchAndPaging();
                int NumberOfPages = pageing.PageNumbers(20, helper.GetAssignedTicketByUserNameCount(user));
                ViewState["ViewAllPageNumber"] = NumberOfPages;
                for (int i = 0; i < NumberOfPages; i++)
                {
                    //if (i < 10)
                    //{
                    Button ButtonChange = new Button();

                    ButtonChange.Text = i.ToString();
                    ButtonChange.ID = "change_" + i.ToString();
                    ButtonChange.Font.Size = FontUnit.Point(7);
                    ButtonChange.ControlStyle.CssClass = "button";
                    ButtonChange.Click += ButtonChange_Click; ;
                    Panel1.Controls.Add(ButtonChange);
                    //}
                    //else
                    //{
                    //    Button ButtonChange = new Button();

                    //    ButtonChange.Text = i.ToString();
                    //    ButtonChange.ID = "change_" + i.ToString();
                    //    ButtonChange.Font.Size = FontUnit.Point(7);
                    //    ButtonChange.ControlStyle.CssClass = "button";
                    //    ButtonChange.Visible = false;
                    //    ButtonChange.Click += ButtonChange_Click; ;
                    //    Panel2.Controls.Add(ButtonChange);


                    //}

                }
            
            







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
                helper.GetAssignedTicketByUserName(Request.Cookies["UserName"].Value, 
                DropDownType.SelectedValue,DropDownSubType.SelectedValue 
                ,Convert.ToInt16(DropDownAsc.SelectedValue)));
            lstNewReq.DataBind();





        }




        protected void UpdateColors_Click(object sender, EventArgs e)
        {
            //ClaimApprovalDBDataContext db = new ClaimApprovalDBDataContext();

            //List<CallCenterEmailApproval> data = (from p in db.CallCenterEmailApprovals
            //                                      where p.ReqStatues != "Closed" ||
            //                                      (p.IsFirstNotifySent == 1 && p.IsScenodNotifySent == 1 && p.IsThirdNotifySent == 1)
            //                                      orderby p.ID descending
            //                                      select p).ToList();


            //foreach (var ItemEmailApproval in data)
            //{
            //    Int64 LateMenits =Convert.ToInt64( (DateTime.Now - Convert.ToDateTime(ItemEmailApproval.CallcenterlTicketCraetionTime)).TotalMinutes);
            //    //string ClintType = ItemEmailApproval.TicketTypeID;
            //    //int Level = (int)ItemEmailApproval.IsFirstNotifySent + (int)ItemEmailApproval.IsScenodNotifySent + (int)ItemEmailApproval.IsThirdNotifySent;
            //    foreach (var ColorItem in db.ColorAlerts)

            //    {
            //        if (ColorItem.BeginTime <= LateMenits && (LateMenits < ColorItem.EndTime || ColorItem.EndTime == null) && 
            //            ItemEmailApproval.TicketTypeID== ColorItem.TicketType)

            //        {
            //            if (ColorItem.TimeLevel == 0)
            //            {
            //                //Send Mail First Level
            //            }

            //           else if (ColorItem.TimeLevel==1)
            //            {
            //                ItemEmailApproval.IsFirstNotifySent = 1;
            //                //Send Mail First Level
            //            }
            //            else if (ColorItem.TimeLevel == 2)
            //            {
            //                ItemEmailApproval.IsScenodNotifySent = 1;

            //            }
            //            else if (ColorItem.TimeLevel == 3)
            //            {
            //                ItemEmailApproval.IsThirdNotifySent = 1;

            //            }
            //            ItemEmailApproval.ColorID = ColorItem.ID;
            //            db.SaveChanges();

            //        }
            //        //else    if (ColorItem.BeginTime >= LateMenits && (LateMenits < ColorItem.EndTime || ColorItem.EndTime == null) &&
            //        //    ItemEmailApproval.TicketTypeID == Convert.ToInt32(Enums.Enums.TicketType.Special))

            //        //{
            //        //    if (ColorItem.TimeLevel == 0)
            //        //    {
            //        //        //Send Mail First Level
            //        //    }

            //        //    else if (ColorItem.TimeLevel == 1)
            //        //    {
            //        //        ItemEmailApproval.IsFirstNotifySent = 1;
            //        //        //Send Mail First Level
            //        //    }
            //        //    else if (ColorItem.TimeLevel == 2)
            //        //    {
            //        //        ItemEmailApproval.IsScenodNotifySent = 1;

            //        //    }
            //        //    else if (ColorItem.TimeLevel == 3)
            //        //    {
            //        //        ItemEmailApproval.IsThirdNotifySent = 1;

            //        //    }
            //        //    ItemEmailApproval.ColorID = ColorItem.ID;
            //        //    db.SaveChanges();

            //        //}

            //    }


            //}



        }



        protected void Next_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(ViewState["currentpage"]) < 0)
            //{
            //    ViewState["currentpage"] = 0;
            //}

            //ViewState["currentpage"]=  Convert.ToInt32(ViewState["currentpage"]) + 1;
            //ChangePage((Convert.ToInt32(ViewState["currentpage"]) ).ToString());




            //Button btn = (Button)Panel2.FindControl("Change_" + ViewState["currentpage"]);
            //Button btnnext = (Button)Panel2.FindControl("Change_" + (Convert.ToInt32( ViewState["currentpage"]) + 1).ToString());
            //Button btnbefor = (Button)Panel2.FindControl("Change_" + (Convert.ToInt32(ViewState["currentpage"]) - 1).ToString());

            //btn.BackColor = Color.Blue;
            //btnbefor.BackColor = default(Color);
            //btnnext.BackColor = default(Color);



            //if (!Panel2.FindControl("Change_" + ViewState["currentpage"]).Visible)
            //{
            //    for(int i=0;i< Convert.ToInt32( ViewState["ViewAllPageNumber"]);i++)
            //    {
            //        Panel2.FindControl("Change_" + i).Visible = false;




            //    }
            //    for (int i = Convert.ToInt32(ViewState["currentpage"]); i < Convert.ToInt32(ViewState["currentpage"])+10; i++)
            //    {

            //        Panel2.FindControl("Change_" + i).Visible = true;


            //    }



            //}
        }

        protected void Befor_Click(object sender, EventArgs e)
        {
            //try {
            //    ViewState["currentpage"] = Convert.ToInt32(ViewState["currentpage"]) - 1;

            //    ChangePage((Convert.ToInt32(ViewState["currentpage"])).ToString());

            //    //Button btnnext = (Button)Panel2.FindControl("Change_" + ViewState["currentpage"]);


            //    Button btnnext = (Button)Panel2.FindControl("Change_" + (Convert.ToInt32(ViewState["currentpage"]) + 1).ToString());
            //    btnnext.BackColor = default(Color);

            //    Button btn = (Button)Panel2.FindControl("Change_" + (Convert.ToInt32(ViewState["currentpage"])).ToString());
            //    btn.BackColor = Color.Blue;



            //    Button btnbefor = (Button)Panel2.FindControl("Change_" + (Convert.ToInt32(ViewState["currentpage"]) - 1).ToString());
            //    btnbefor.BackColor = default(Color);



            //    if (!Panel2.FindControl("Change_" + ViewState["currentpage"]).Visible)
            //    {
            //        for (int i = 0; i < Convert.ToInt32(ViewState["ViewAllPageNumber"]); i++)
            //        {
            //            Panel2.FindControl("Change_" + i).Visible = false;


            //        }
            //        for (int i = Convert.ToInt32(ViewState["currentpage"]);
            //            i > Convert.ToInt32(ViewState["currentpage"]) - 10; i--)
            //        {
            //            Panel2.FindControl("Change_" + i).Visible = true;


            //        }



            //    }
            //}
            //catch { }
            //}

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
