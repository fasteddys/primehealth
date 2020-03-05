using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using WebApplication1.BLL;
using System.Windows.Input;
using System.Drawing.Printing;
using System.Net;
using System.Text;
namespace WebApplication1.Portal
{
     
    public partial class ManageSubRequest : System.Web.UI.Page
    {
        subRequests mdb = new subRequests();
        Accounts mdb2 = new Accounts();
        List<ListItem> files = new List<ListItem>();
        string sub = "";
        string body = "";
        string acManger = "";
        string UserComment = "";
        string TPAcomment = "";
        string attch = "";
        string AssignedPerson = "";
        string assigned;
        string Status;
        int numberofsentbatches;
        int numberofrecbatches;
        int numberofrecclaims;
        string confirmed;
        string subtype;
        string monthofsub;
        string yearofsub;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            try
            {
                if (!IsPostBack)
                {
                    string type = Request.Cookies["typeark"].Value;
                    string user = Request.Cookies["nameark"].Value;
                    Button1.Visible = false;
                    btn_issue.Visible = false;
                    div_Error.Visible = false;
                    btn_Mang.Visible = false;
                    btn_asign.Visible = false;
                    adminassign.Visible = false;
                    btn_rec.Visible = false;
                    divclaims.Visible = false;
                    btn_Reject.Visible = false;
                    int id = int.Parse(Request.QueryString["id"]);
                    Label1.InnerText = id.ToString();
                    mdb.GetDetailByid(id, ref sub, ref body,ref attch ,ref acManger, ref UserComment, ref assigned, ref AssignedPerson, ref Status, ref numberofsentbatches, ref numberofrecbatches, ref numberofrecclaims, ref confirmed, ref subtype, ref monthofsub, ref yearofsub, ref TPAcomment);
                    lbl_Sub.InnerHtml = sub;
                    lbl_Creator.InnerHtml = acManger;
                    lbl_Assignee.InnerHtml = AssignedPerson;
                    lbl_status.InnerHtml = Status;
                    lbl_Month.InnerHtml = monthofsub;
                    lbl_Year.InnerHtml = yearofsub;                    
                    txtrBody.InnerHtml = body;
                    TextBox1.Text = UserComment;
                    TextBox2.Text = TPAcomment;
                    reqitems.Text = numberofsentbatches.ToString();
                    reqitems.Enabled = false;

                    if (type == "TPA" || type == "TPAAdmin" || type == "EnterpriseAdmin")
                    {
                        Button1.Visible = true;
                    }
                   
                    if (subtype=="InPatient")
                    {
                        TPA.Visible = false;
                    }

                    if (subtype == "OutPatient")
                    {
                        if (Status == "Closing" || Status == "Closed" || Status == "PendingTPA")
                        {
                            Archive.Visible = true;
                        }
                        else
                        {
                            Archive.Visible = false;
                        }
                        
                    }
                    if (numberofrecbatches==0)
                    {
                        RecBatches.Text ="";
                    }
                    else
                    {
                        RecBatches.Text = numberofrecbatches.ToString();
                    }

                    if (numberofrecclaims==0)
                    {
                        RecClaims.Text = "";
                    }

                    else
                    {
                        RecClaims.Text = numberofrecclaims.ToString();
                    }
                    if (subtype == null || subtype=="")
                    {
                        lbl_Batches_Type.InnerText = "";

                    }

                    else
                    {
                        lbl_Batches_Type.InnerText = subtype.ToString();
                    }


                    
                    if (type == "TPAAdmin" || type=="TPA")
                    {
                        var usersList = mdb2.GetAllUsersforadmin().Select(u => new ListItem
                        {
                            Text = u.UserName,
                            Value = u.id.ToString()
                        }).ToList();
                        drp1.Items.Add("----");

                        foreach (var item in usersList)
                        {
                            drp1.Items.Add(item);
                        }
                    }
                    else if (type == "Admin")
                    {
                       var usersList = mdb2.GetAllUsersforadminArchiving().Select(u => new ListItem
                        {
                            Text = u.UserName,
                            Value = u.id.ToString()
                        }).ToList();

                       drp1.Items.Add("----");

                       foreach (var item in usersList)
                       {
                           drp1.Items.Add(item);
                       }
                    }


                    
                    if ((type == "Admin") && (subtype == "InPatient") && (Status == "Submitted" || Status == "Receiving" || Status == "Submitting"||Status=="Closing"))
                    {
                        adminassign.Visible = true;
                        btn_Mang.Visible = false;
                        
                        btn_asign.Visible = true;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;


                    }

                    if ((type == "Admin") && (subtype == "InPatient") && (Status == "Closed" || Status == "Rejected" ))
                    {
                        adminassign.Visible = false;
                        btn_Mang.Visible = false;
                       
                        btn_asign.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                    }


                    if ((type == "Admin" || type == "ArcAdmin" ) && (subtype == "InPatient" && Status == "Closing"))
                    {
                        adminassign.Visible = false;
                        btn_Mang.Visible = true;
                        
                        btn_asign.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                    }


                    if ((type == "Admin" || type == "ArcAdmin")&& (subtype == "OutPatient" && Status == "Closing"))
                    {
                        adminassign.Visible = false;
                        btn_Mang.Visible = true;

                        btn_asign.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                    }





                    else if ((type == "ArcAdmin") && (subtype == "InPatient" ) && Status == "Submitting")
                    {

                        adminassign.Visible = false;
                        btn_Mang.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        btn_asign.Visible = true;
                    }

                    else if ((type == "TPA") && (subtype == "OutPatient") &&( Status != "Closed" && Status == "PendingTPA") && (user==AssignedPerson))
                    {

                        adminassign.Visible = false;
                        btn_Mang.Visible = true;

                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        btn_asign.Visible = false;
                    }

                    else if ((type == "TPA") && (subtype == "OutPatient") && (Status != "Closed" && Status == "PendingTPA") && (user != AssignedPerson))
                    {

                        adminassign.Visible = false;
                        btn_Mang.Visible = false;

                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        btn_asign.Visible = false;
                    }

                    else if ((type == "TPA") && (subtype == "OutPatient") && (Status != "Closed" && Status == "Submitted") && (user == AssignedPerson))
                    {

                        adminassign.Visible = false;
                        btn_Mang.Visible = false;

                        btn_rec.Visible = true;
                        btn_Reject.Visible = true;
                        btn_asign.Visible = false;
                    }

                    else if
                       ((type == "TPAAdmin") && (subtype == "OutPatient") && (Status != "Closed" && Status != "Rejected"))
                    {
                        adminassign.Visible = true;
                        btn_Mang.Visible = false;
                        
                        btn_asign.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                    }

                    else if ((type == "Archiving") && (subtype == "InPatient") && (Status == "Closed" || Status == "Receiving" || Status == "Submitted" || Status == "Closing"))
                      
                    {
                        adminassign.Visible = false;
                        btn_Mang.Visible = false;

                        btn_asign.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                    }

                    else if ((type == "Archiving") &&( subtype == "InPatient" && Status == "Submitting"))
                  
                    {
                        adminassign.Visible = false;
                        btn_Mang.Visible = false;

                        btn_asign.Visible = true;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                    }

                   


                    else if ((type == "TPA") && (subtype == "OutPatient") &&( Status != "Closed"))
                 
                    {
                       
                        adminassign.Visible = true;
                        btn_Mang.Visible = false;
                       
                        btn_rec.Visible = false ;
                        btn_Reject.Visible = false;
                        btn_asign.Visible = true;
                    }


                    if (assigned == "true" && Status != "PendingTPA")
                        {
                            btn_asign.Disabled = true;
                            if (user == AssignedPerson )
                            {
                                btn_rec.Visible = true;
                                btn_Reject.Visible = true;
                            }


                            if ((user != AssignedPerson) && (type == "TPA" || type == "TPAAdmin") &&( Status == "PendingTPA"))
                            {
                                btn_rec.Visible = false;
                                btn_Reject.Visible = false;
                              

                            }


                        }
      
                    if (Status=="Receiving" && user!=AssignedPerson)
                    {
                        btn_Reject.Visible = false;
                        btn_rec.Visible = false;
                        btn_asign.Visible = false;
                    }

                    if (Status == "Receiving" && user == AssignedPerson)
                    {
                        if (type == "TPA")
                        {
                            btn_Reject.Visible = true;
                            btn_rec.Visible = true;
                            btn_asign.Visible = false;
                            Archive.Visible = false;
                        }
                        else
                        {
                            btn_Reject.Visible = true;
                            btn_rec.Visible = true;
                            btn_asign.Visible = false;
                        }
                    }

                   

                    if (Status=="Closing")
                    {
                       
                            btn_rec.Visible = false;
                            btn_Reject.Visible = false;
                            btn_asign.Visible = false;


                    }
                  
                    if (Status == "Closed" || Status=="Rejected")
                    {
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        btn_Mang.Visible = false;
                        btn_issue.Visible = false;
                        

                    }
                    if ((Status == "Submitted" )&& (user == acManger )&& (type == "Data Entry"))
                    {

                        btn_asign.Visible = false;
                        btn_issue.Visible = true;
                        btn_Mang.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                       
                        
                    }



                    if ((Status == "Submitted" )&& (subtype == "InPatient") &&( type == "Data Entry" ))
                    {

                        btn_asign.Visible = false;
                        btn_issue.Visible = true;
                        btn_Mang.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        

                    }

                    if ((subtype == "OutPatient") && (type == "Data Entry"))
                    {

                        btn_asign.Visible = false;
                        btn_issue.Visible = true;
                        btn_Mang.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        Archive.Visible = false;

                    }

                    if (((Status == "Submitting") || (Status == "Receiving")) && (type == "Data Entry") && (subtype == "OutPatient"))
                    {
                        btn_issue.Visible = false;//update
                    }
                    if (subtype == "IntPatient" && type == "Data Entry")
                    {

                        btn_asign.Visible = false;
                        btn_issue.Visible = true;
                        btn_Mang.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        Archive.Visible = true;
                        TPA.Visible = false;


                    }

                    if (Status == "Submitted" && (type == "Admin" || type == "ArcAdmin" || type == "Archiving" || type == "EnterpriseAdmin" || type == "TPA" || type == "TPAAdmin"))
                    {

                        btn_asign.Visible = false;
                        btn_issue.Visible = false;
                        btn_Mang.Visible = false;
                        btn_rec.Visible = false;
                        btn_Reject.Visible = false;
                        

                    }

                    if (Status == "Submitted" && ( type == "TPA") && user==AssignedPerson)
                    {

                        btn_asign.Visible = false;
                        btn_issue.Visible = false;
                        btn_Mang.Visible = false;
                        btn_rec.Visible = true;
                        btn_Reject.Visible = true;


                    }
                    if (confirmed == "true" && subtype == "InPatient")
                    {
                        if (Status=="Closing")
                        {
                            if (type == "Admin")
                            {
                              
                                btn_asign.Visible = false;
                                adminassign.Visible = false;
                                btn_Mang.Visible = true;
                                
                            }
                            
                        }
                       

                    }

                  
                    if (type == "Admin" || type == "ArcAdmin" || type == "Archiving" || type == "EnterpriseAdmin" || type == "TPA" || type=="TPAAdmin")
                    {
                        divclaims.Visible = true;
                       
                    }
                    else
                    {
                        divclaims.Visible = false;
                        
                    }
                    if (Status == "Submitting" && type == "TPA")
                    {
                        
                       
                        adminassign.Visible = false;
                        btn_asign.Visible = true;
                        Archive.Visible = false;
                      
                    }


                    
                 
                    

                    if (type == "EnterpriseAdmin"  )
                    {
                        btn_issue.Visible = false;
                        div_Error.Visible = false;
                        btn_Mang.Visible = false;
                        btn_asign.Visible = false;
                        btn_Reject.Visible = false;
                       
                    }
                }
            }
            catch (Exception ex)
            {


            }

            try
            {

                int id = int.Parse(Request.QueryString["id"]);
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/SubAttached/" + id));
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }

                downloadlist.DataSource = files;
                downloadlist.DataBind();
            }
            catch (Exception ex)
            {

            }
        }



        protected void btn_asign_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateAssignPerson(id, user);
            Server.Transfer("ManageSubRequest.aspx");
        }

        protected void btn_confirmed_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateRembConfirm(id);
            Server.Transfer("ManageSubRequest.aspx");
        }
        protected void btn_Reject_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updaterejected(id,TextBox1.Text,TextBox2.Text);
            Server.Transfer("ManageSubRequest.aspx");
        }

        protected void btn_rec_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            if (RecBatches.Text != "" && RecClaims.Text != "")
            {
                mdb.updaterecived(id, int.Parse(RecBatches.Text), int.Parse(RecClaims.Text),TextBox1.Text,TextBox2.Text);
                Server.Transfer("ManageSubRequest.aspx");
            }
            else
            {
                div_Error.Visible = true;
            }


        }

        protected void btn_Mang_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;

            if (lbl_Batches_Type.InnerText == "OutPatient")
            {
                if (TextBox1.Text != null)
                {
                    if (string.IsNullOrWhiteSpace(TextBox1.Text))
                    {
                        mdb.updateClosed(id, user);


                    }

                    if (TextBox1.Text == "" && (type == "Admin" || type == "ArcAdmin"))
                    {
                        mdb.updateClosed(id, user);
                    }


                    else if (type == "TPA")
                    {
                        mdb.updateClosed(id, user);
                    }


                    else
                    {
                        mdb.updateClosedTPANew(id, user, TextBox1.Text, TextBox2.Text);
                    }

                }


            }




            if (lbl_Batches_Type.InnerText == "InPatient")


          
            {

                mdb.updateClosed(id, user);

            }

            
                

                //if ((TextBox1.Text != string.Empty || Mouse.LeftButton != MouseButtonState.Pressed) && (Status == "Closing") && (type == "Admin" || type == "ArcAdmin"))
                //{
                //    mdb.updateClosedTPANew(id, user, TextBox1.Text);
                //}


                

                //else if (type == "Admin" || type == "ArcAdmin")
                //{
                //    mdb.updateClosedTPANew(id, user, TextBox1.Text);
                //}
                // if (type == "Admin" || type == "ArcAdmin")
                // {
                //     mdb.updateClosedTPANew(id, user, TextBox1.Text, TextBox2.Text);
                //}

                Server.Transfer("ManageSubRequest.aspx");

            }

        protected void btn_Download(object sender, EventArgs e)
        {

            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                string sub = "";
                string body = "";
                string attch = "";
                string reply = "";
                string acManger = "";
                string UserComment = "";
                string AssignedPerson = "";
                string CAsign = "";
                string Status = "";
                string addedbytype = "";
                string rembpend = "";
                int reqesteditems = 0;
                int founditemss = 0;

                mdb.GetDetailByid(id, ref sub, ref body, ref attch, ref acManger, ref UserComment, ref assigned, ref AssignedPerson, ref Status, ref numberofsentbatches, ref numberofrecbatches, ref numberofrecclaims, ref confirmed, ref subtype, ref monthofsub, ref yearofsub, ref TPAcomment);
                string filePath = reply;
                Response.ContentType = ContentType;
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.TransmitFile(filePath);
                Response.End();


            }

            catch (Exception ex)
            {

            }

        }

       
           
        //protected void btn_SendTPA_ServerClick(object sender, EventArgs e)
        //{

        //    int id = int.Parse(Request.QueryString["id"]);
        //    string user = Request.Cookies["nameark"].Value;

        //    mdb.updateClosedTPA(id, user);
        //    Server.Transfer("ManageSubRequest.aspx");

        //}


        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        protected void btn_Transfer(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = drp1.SelectedItem.ToString();
            mdb.updateAssignPersonforAdmin(id, user);
            Server.Transfer("ManageSubRequest.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            Response.Clear();
            Response.ContentType = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml";
            Response.AddHeader("content-disposition", "attachment;    filename="+id+".txt");
            
            Response.Write(txtrBody.InnerText); //byteArray is the mem stream byte array from above
            Response.End();
        }

        
    }

}