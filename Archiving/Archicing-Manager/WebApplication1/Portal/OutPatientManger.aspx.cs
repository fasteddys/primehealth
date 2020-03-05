using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class OutPatientManger : System.Web.UI.Page
    {
        outpatient mdb = new outpatient();
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            adminsearch.Visible = false;
            normalsearch.Visible = false;
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("BatchsubmgrLi")).Attributes["class"] = "icon-thumbnail bg-success";
            
            if (type == "Admin" || type == "ArcAdmin")
            {
                adminsearch.Visible = true;
                if (!IsPostBack)
                {
                    adminsearch.Visible = true;
                   // Bind();
                }
            }
            else if (type=="Archiving" || type=="EnterpriseAdmin")
            {
                normalsearch.Visible = true;
            }

            else
            {
                Response.Redirect("/Login.aspx");
            }
        }
        public void Bind()
        {
            var data = mdb.GetAllTransactions();
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }
        public void BindForEdit(int id)
        {
            var data = mdb.GetAllTransactionsForEdit(id);
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }
        protected void ItemsList_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ItemsList.EditIndex = -1;
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("EditIDTxt") as Label;
            int id = int.Parse(lblTemp.Text);
            BindForEdit(id);
        }
        protected void ItemsList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("IDTxt") as Label;
            int id = int.Parse(lblTemp.Text);
            mdb.DeleteTrans(id);
           // Bind(id);
            clear();
        }
        protected void ItemsList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ItemsList.EditIndex = e.NewEditIndex;
            Label lbl = ItemsList.Items[e.NewEditIndex].FindControl("IDTxt") as Label;
            int id = int.Parse(lbl.Text);
            BindForEdit(id);
        }
        protected void ItemsList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("EditIDTxt") as Label;
            int id = int.Parse(lblTemp.Text);
            TextBox txtbatch = ItemsList.Items[e.ItemIndex].FindControl("BatchNumtxt") as TextBox;
            string batch = txtbatch.Text;
            TextBox txtbox = ItemsList.Items[e.ItemIndex].FindControl("boxtxt") as TextBox;
            string box = txtbox.Text;
            TextBox txtsetid = ItemsList.Items[e.ItemIndex].FindControl("SetIDtxt") as TextBox;
            string set = txtsetid.Text;
            TextBox txtclaim = ItemsList.Items[e.ItemIndex].FindControl("ClaimNumTxt") as TextBox;
            string claim = txtclaim.Text;
            TextBox numofclaims = ItemsList.Items[e.ItemIndex].FindControl("numclaimtxt") as TextBox;
            string noclaims = numofclaims.Text;
            mdb.UpdateTran(id, claim, set, batch, box, noclaims);
            ItemsList.EditIndex = -1;
            clear();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string type = Request.Cookies["typeark"].Value;
                if (DropDownList1.SelectedValue == "" || txtsearch.Value == "")
                {
                    if (type=="Admin" || type=="ArcAdmin")
                    {
                        ItemsList.DataSource = null;
                        ItemsList.DataBind();
                    }

                    else if (type=="Archiving" || type=="EnterpriseAdmin")
                    {
                        lstNewReq.DataSource = null;
                        lstNewReq.DataBind();
                    }
                  
                }


                else if (DropDownList1.SelectedValue == "Claim Code")
                {
                    if (type == "Admin" || type == "ArcAdmin")
                    {
                        var data = mdb.GetAllSearchByClaimCode(txtsearch.Value);
                        ItemsList.DataSource = data;
                        ItemsList.DataBind();
                    }
                    else if (type == "Archiving" || type == "EnterpriseAdmin")
                    {
                        var data = mdb.GetAllSearchByClaimCode(txtsearch.Value);
                        lstNewReq.DataSource = data;
                        lstNewReq.DataBind();

                    }

                }

                else if (DropDownList1.SelectedValue == "Set ID")
                {
                    if (type == "Admin" || type == "ArcAdmin")
                    {
                        var data = mdb.GetAllSearchBySetID(txtsearch.Value);
                        ItemsList.DataSource = data;
                        ItemsList.DataBind();
                    }
                    else if (type == "Archiving" || type == "EnterpriseAdmin")
                    {
                        var data = mdb.GetAllSearchBySetID(txtsearch.Value);
                        lstNewReq.DataSource = data;
                        lstNewReq.DataBind();

                    }

                }

                else if (DropDownList1.SelectedValue == "Batch Number")
                {
                    if (type == "Admin" || type == "ArcAdmin")
                    {
                        var data = mdb.GetAllSearchByBatch(txtsearch.Value);
                        ItemsList.DataSource = data;
                        ItemsList.DataBind();
                    }
                    else if (type == "Archiving" || type == "EnterpriseAdmin")
                    {
                        var data = mdb.GetAllSearchByBatch(txtsearch.Value);
                        lstNewReq.DataSource = data;
                        lstNewReq.DataBind();
                    }
                }

                else if (DropDownList1.SelectedValue == "Box Number")
                {
                    if (type == "Admin" || type == "ArcAdmin")
                    {
                        var data = mdb.GetAllSearchByBox(txtsearch.Value);
                        ItemsList.DataSource = data;
                        ItemsList.DataBind();
                    }
                    else if (type == "Archiving" || type == "EnterpriseAdmin")
                    {
                        var data = mdb.GetAllSearchByBox(txtsearch.Value);
                        lstNewReq.DataSource = data;
                        lstNewReq.DataBind();
                    }
                }

                else if (DropDownList1.SelectedValue == "Added By")
                {
                    if (type == "Admin" || type == "ArcAdmin")
                    {
                        var data = mdb.GetAllSearchByAddedBy(txtsearch.Value);
                        ItemsList.DataSource = data;
                        ItemsList.DataBind();
                    }
                    else if (type == "Archiving" || type == "EnterpriseAdmin")
                    {
                        var data = mdb.GetAllSearchByAddedBy(txtsearch.Value);
                        lstNewReq.DataSource = data;
                        lstNewReq.DataBind();
                    }

                }
               

            }

            catch (Exception ex)
            {
               

            }

        }
        protected void clear()
        {

            ItemsList.DataSource = null;
            ItemsList.DataBind();
        }

    }
}