using InHouseSendApproval.Mail;
using InHouseSendApproval.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InHouseSendApproval
{
    public partial class ManageSendApproval : Form
    {
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public ManageSendApproval()
        {
            InitializeComponent();
        }

        private void ExcelFileUploadAction(object sender, CancelEventArgs e)
        {
            try
            {
                string filePath = ExcelFileUpload.FileName;
                string extension = Path.GetExtension(filePath);
                string header = "YES";
                string conStr, sheetName;
                conStr = string.Empty;
                UsersExcel.DataSource = null;
                UsersExcel.Columns.Clear();
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath, header);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath, header);
                        break;
                    default:
                        ErrorMessage.Text = "File Upload Is not Excel";
                        break;
                }
                //Files has uploaded Must be Excel file and refuse others
                if (extension.Trim() == ".xlsx" || extension.Trim() == ".xls")
                {
                    //Get the name of the First Sheet.
                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            cmd.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            con.Close();
                        }
                    }

                    //Read Data from the First Sheet.
                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                
                                    // Translate All Data From Exel Sheet To Data Table
                                    DataTable datatable = new DataTable();
                                    cmd.CommandText = "SELECT  Medical_Id ,Name , Email From [" + sheetName + "]";
                                    cmd.Connection = con;
                                    con.Open();
                                    oda.SelectCommand = cmd;
                                try
                                {
                                    oda.Fill(datatable);
                                    UsersExcel.DataSource = datatable;
                                    UsersExcel.Columns["Name"].Width = 150;
                                    UsersExcel.Columns["Email"].Width = 200;
                                    ErrorMessage.Text = "";
                                    con.Close();

                                }
                                catch (Exception ex)
                                {
                                    ErrorMessage.Text = "Columns Medical_Id Or Name Or Email Not Exist";
                                }

                                
                            }
                        }
                    }
                    


                }
                else
                {
                    MessageBox.Show("Please Select Excel File");
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UploadExcelbtn_Click(object sender, EventArgs e)
        {
            UsersExcel.DataSource = null;
            UsersExcel.Columns.Clear();
            ExcelFileUpload.ShowDialog();
        }

        private void OpenPDFLocation_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            OpenPDFLocation.ShowDialog();
           if(OpenPDFLocation.SelectedPath!="")
            {
                PDFFilesLocation.Text = OpenPDFLocation.SelectedPath;
            }
        }

        private void OpenPDFLocation_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (PDFFilesLocation.Text != ""&& UsersExcel.Rows.Count != 0)
            {
                ErrorMessage.Text = "";
                if (MessageBox.Show("Are you sure you Send Approvals ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.DisableAllButtons();

                    SendEmails();
                    this.enableAllButtons();

                }
            }
            else
            {
                MessageBox.Show("You Have To Upload Users Excel And Define PDF Location !!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void SendEmails()
        {
            try
            {
                UsersExcel.Columns.Remove("Message");
                UsersExcel.Columns.Remove("img");
                UsersExcel.Columns.Remove("FaildReason");

                
            }
            catch { }
            Mailling Mail = new Mailling();
            UsersExcel.Columns.Add("Message", "Message");
            Returned ReturnResult = new Returned();
            DataGridViewImageColumn img = new DataGridViewImageColumn();                
            img.Image = null;
            UsersExcel.Columns.Add(img);
            img.Name = "img";
            UsersExcel.Columns.Add("FaildReason", "FaildReason");
            img.HeaderText = "";
            for (int i=0;i< UsersExcel.Rows.Count-1;i++)
            {
                int MedicalID = Convert.ToInt32
                    (UsersExcel.Rows[i].Cells["Medical_Id"].Value);
                ClientData ClientData = new ClientData
                {
                    MedicalID = MedicalID,
                    ClientName = (string)UsersExcel.Rows[i].Cells["Name"].Value,
                    ClientMail = (string)UsersExcel.Rows[i].Cells["Email"].Value,
                    AttachmentsUrl= GetApprovalAttachments(MedicalID)
                };
                if (ClientData.AttachmentsUrl.Count>0)
                {
                    ReturnResult = Mail.SendMail(ClientData);

                }
                else
                {
                    ReturnResult = new Returned
                    {
                        FaildReason = "No Attachment For Client : " + ClientData.MedicalID,
                        Message = "Pending Attachment",
                        Success = false
                    };
                }
                UsersExcel.Rows[i].Cells["Message"].Value = ReturnResult.Message;
                if (ReturnResult.Success)
                {
                    UsersExcel.Rows[i].Cells["Message"].Value = ReturnResult.Message;
                    UsersExcel.Columns["Message"].Width = 150;

                    ((DataGridViewImageCell)UsersExcel.Rows[i].Cells["img"]).Value = Properties.Resources.SuccessIcon;
                }
                else
                {
                    UsersExcel.Rows[i].Cells["Message"].Value = ReturnResult.Message;
                    ((DataGridViewImageCell)UsersExcel.Rows[i].Cells["img"]).Value = Properties.Resources.FailedIcon;
                    UsersExcel.Rows[i].Cells["FaildReason"].Value = ReturnResult.FaildReason;
                    UsersExcel.Rows[i].Cells["FaildReason"].Style.ForeColor = Color.Red;
                    UsersExcel.Columns["FaildReason"].Width = 300;
                    UsersExcel.Columns["Message"].Width = 150;


                }

            }
        }

        public List<string> GetApprovalAttachments(int MedicalID)
        {
            List<string> FilesNames = new List<string>() ;
         
            foreach (string c in Directory.EnumerateFiles(PDFFilesLocation.Text, MedicalID+"*.pdf", SearchOption.AllDirectories))
            {
                FilesNames.Add(c);
            }


            return FilesNames;

        }
        private void DisableAllButtons()
        {
            Browse.Enabled = false;
            UploadExcelbtn.Enabled = false;
            Send.Enabled = false;


        }
        private void enableAllButtons()
        {
            Browse.Enabled = true;
            UploadExcelbtn.Enabled = true;
            Send.Enabled = true;
        }
    }
}
