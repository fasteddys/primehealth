using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class OpenCcEmailRequest : System.Web.UI.Page
    {
        Helpers Helper = new Helpers();

        List<string> PersonreceiveEmail = new List<string>();
        List<string> PersonIccEmail = new List<string>();
        List<string> PersonInBccEmail = new List<string>();

        CallcentereMailRequest mdb = new CallcentereMailRequest();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewCCEmailRequestID")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
                SetInitialRow();
            }
            div_Error.Visible = false;
            div_Success.Visible = false;
        }
        private List<string> EmailResult(ListView listview)
        {
            List<string> result = new List<string>();
            //var list = listview.Controls[1];
            for (int i = 0; i < listview.Controls.Count; i++)
            {
                foreach (var item in listview.Controls[i].Controls)
                {
                    try
                    {
                        TextBox txt = (TextBox)item;
                        result.Add(txt.Text);
                    }
                    catch { }


                }
            }

            return result;




        }
        protected void ApvSubmitNewCallcenterEmailApproval(object sender, EventArgs e)
        {

            try
            {
                string AddCCMailRequestName = Request.Cookies["UserName"].Value;
                string OpenCCMailRequestType = Request.Cookies["UserType"].Value;
                //int MID;
                if (FileUpload1.HasFile && CallcenterOpenNote.Text != string.Empty)
                {
                    CallcentereMailRequest md = new BLL.CallcentereMailRequest();
                    string type = Request.Cookies["UserType"].Value.Trim();
                    string Name = Request.Cookies["UserName"].Value.Trim();
                    // MID = md.GetMaxID();
                    int pathID;
                    Helpers helpr = new Helpers();
                    pathID= Convert.ToInt32( mdb.AddCallCenterMailRequest(MailSubject.Value, MemberName.Value, MedicalID.Value, ClientName.Value,
                        ProviderName.Value, CallcenterOpenNote.Text, AddCCMailRequestName, Dropdownlist1.SelectedValue
                        , Category.SelectedValue, Priority.SelectedValue));
                    PersonreceiveEmail.AddRange(EmailResult(AddToList));

                    foreach (var item in PersonIccEmail)
                    {
                        if (item == "")
                        {
                            PersonIccEmail.Remove(item);
                        }

                    }


                    PersonIccEmail.AddRange(EmailResult(AddCcList));
                    PersonInBccEmail.AddRange(EmailResult(AddBccList));
                    List<string> personsinCC = new List<string>();
                    List<string> personsinbcc = new List<string>();
                    foreach (var item in PersonIccEmail)
                    {
                        if (item != "")
                            personsinCC.Add(item);
                    }
                    foreach (var item in PersonInBccEmail)
                    {

                        if (item != "")
                            personsinbcc.Add(item);

                    }


                    Helper.AddEmailRequestMailingDetailsByRequestID(pathID, PersonreceiveEmail, personsinCC, personsinbcc);


                    helpr.EmailRequestFileUpload(FileUpload1.PostedFile, pathID, type, FileUpload1, false);
                    div_Success.Visible = true;
                    Helper.CreateBackup(FileUpload1.PostedFile, pathID, Request.Cookies["UserType"].Value, FileUpload1, false);
                    clean();

                }

                else
                {
                    div_Error.Visible = true;
                }
            }
            catch (Exception ex)
            {
                div_Error.Visible = true;
            }
        }
        private void clean()
        {
            MailSubject.Value = string.Empty;
            MemberName.Value = string.Empty;
            MedicalID.Value = string.Empty;
            ClientName.Value = string.Empty;
            ProviderName.Value = string.Empty;
            CallcenterOpenNote.Text = string.Empty;
            Dropdownlist1.SelectedValue = "-1";
            ViewState["AddToList"] = null;
            ViewState["AddBccList"] = null;
            ViewState["AddCcList"] = null;
            AddToList.DataSource = (DataTable)ViewState["AddToList"];
            AddCcList.DataSource = (DataTable)ViewState["AddBccList"];
            AddBccList.DataSource = (DataTable)ViewState["AddCcList"];

            AddToList.DataBind();
            AddCcList.DataBind();
            AddBccList.DataBind();
            SetInitialRow();


        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            // dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dr = dt.NewRow();
            dr["ID"] = "X";
            dr["Email"] = string.Empty;
            //dr["Column2"] = string.Empty;
            //  dr["Column3"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["AddToList"] = dt;
            AddToList.DataSource = dt;
            AddToList.DataBind();

            ViewState["AddCcList"] = dt;
            AddCcList.DataSource = dt;
            AddCcList.DataBind();

            ViewState["AddBccList"] = dt;

            AddBccList.DataSource = dt;
            AddBccList.DataBind();
        }


        private void AddNewRowToGrid(ListView listview, string NameOfViewState)
        {
            int rowIndex = 0;
            if (ViewState[NameOfViewState] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState[NameOfViewState];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        if (i != 0)
                        { //extract the TextBox values 
                            Button box1 = (Button)listview.Items[rowIndex].FindControl("RowNumber");
                            TextBox box2 = (TextBox)listview.Items[rowIndex].FindControl("TextBox1");
                            box1.Text = "X";
                            drCurrentRow = dtCurrentTable.NewRow();
                            // drCurrentRow["RowNumber"] = i + 1;

                            dtCurrentTable.Rows[i - 1]["ID"] = "X";
                            dtCurrentTable.Rows[i - 1]["Email"] = box2.Text;
                            rowIndex++;
                        }
                    }
                    drCurrentRow["ID"] = "X";
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState[NameOfViewState] = dtCurrentTable;

                    listview.DataSource = dtCurrentTable;
                    listview.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks 
        }
        protected void RowNumberInBCC_Click(object sender, EventArgs e)
        {


            Button DeleteButton = sender as Button;



            int OrderNumber = Convert.ToInt32(DeleteButton.ToString());
            DeleteRowFromGrid(AddBccList, "AddBccList", OrderNumber);
        }

        protected void receiveEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = AddToList.SelectedIndex;
        }
        protected void ADDreceiveofEmail(object sender, EventArgs e)
        {
            AddNewRowToGrid(AddToList, "AddToList");


        }

        protected void ADDInCCofEmailclick(object sender, EventArgs e)
        {
            AddNewRowToGrid(AddCcList, "AddCcList");


        }
        protected void ADDInBCCofEmailclick(object sender, EventArgs e)
        {
            AddNewRowToGrid(AddBccList, "AddBccList");


        }

        protected void InCC_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            AddCcList.SelectedIndex = e.NewSelectedIndex;

            DeleteRowFromGrid(AddCcList, "AddCcList", AddCcList.SelectedIndex);
        }
        protected void InBCC_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {



        }
        protected void InBCC_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            AddBccList.SelectedIndex = e.NewSelectedIndex;

            DeleteRowFromGrid(AddBccList, "AddBccList", AddBccList.SelectedIndex);

        }

        protected void receiveEmail_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            AddToList.SelectedIndex = e.NewSelectedIndex;
            DeleteRowFromGrid(AddToList, "AddToList", AddToList.SelectedIndex);

        }


        private void DeleteRowFromGrid(ListView listview, string NameOfViewState, int OrderOfRemovedRow)
        {
            if (ViewState[NameOfViewState] != null && OrderOfRemovedRow != 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState[NameOfViewState];
                dtCurrentTable.Rows[OrderOfRemovedRow].Delete();


                listview.DataSource = dtCurrentTable;
                listview.DataBind();
            }
        }

    }





}
