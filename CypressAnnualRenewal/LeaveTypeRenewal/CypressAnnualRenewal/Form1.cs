using CypressAnnualRenewal.BackUp;
using CypressAnnualRenewal.Excel;
using CypressAnnualRenewal.Reports;
using LeaveTypeRenewal.BLL;
using LeaveTypeRenewal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LeaveTypeRenewal.Helper;

namespace CypressAnnualRenewal
{
    public partial class Form1 : Form
    {
        LeaveTypesBLL leaveTypesBLL = new LeaveTypesBLL();
      
        public Form1()
        {
            InitializeComponent();
            NewYrarTimePicker.Format = DateTimePickerFormat.Custom;
            NewYrarTimePicker.CustomFormat = "yyyy";
            NewYrarTimePicker.ShowUpDown = true;
            EffictiveYear.Format = DateTimePickerFormat.Custom;
            EffictiveYear.CustomFormat = "yyyy";
            EffictiveYear.ShowUpDown = true;
            EffictiveYear.Value.AddYears(-1);
            panel6.Anchor = System.Windows.Forms.AnchorStyles.None;
        }
        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (leaveTypesBLL.CheckPendingLeves())
                {
                    Thread threadInput = new Thread(submit);
                    threadInput.Start();
                }
                else
                {
                    MessageBox.Show("Please Note there is a pending Requests", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {

                DisplayError(ex);
            }


         

        }
        void submit()
        {
            SetLoading(true);

            var context = leaveTypesBLL.AddForLeaveTypesHasEntitlementsForNewYear(TransferRdio.Checked, false, NewYrarTimePicker.Value);
            DataTable Entitlementdata = new DataTable();
            Entitlementdata.Columns.Add("Cypress ID");

            Entitlementdata.Columns.Add("Name");
            Entitlementdata.Columns.Add("LeaveTypeName");
            Entitlementdata.Columns.Add("EntitlementTotal");
            Entitlementdata.Columns.Add("Year");
            Entitlementdata.Columns.Add("Month");



            foreach (var item in context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity.GetType().Name == "UserEntitlement").Select(x => x.Entity as UserEntitlement))
            {
                Entitlementdata.Rows.Add(item.UserFK, item.User.UserName, item.LeaveType.LeaveTypeName, item.EntitlementTotal, item.Year, item.Month);
            }

            DataTable EntitlementChangedata = new DataTable();
            EntitlementChangedata.Columns.Add("Cypress ID");
            EntitlementChangedata.Columns.Add("Comment");
            EntitlementChangedata.Columns.Add("EntitlementBefore");
            EntitlementChangedata.Columns.Add("EntitlementAfter");
            //  EntitlementChangedata.Columns.Add("Month");
            
            foreach (var item in context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity.GetType().Name == "UserEntitlementChange").Select(x => x.Entity as UserEntitlementChange))
            {
                EntitlementChangedata.Rows.Add(item.UserFK, item.Comment, item.EntitlementBefore, item.EntitlementAfter);
            }

            this.Invoke((MethodInvoker)delegate
            {
                dataGridView1.DataSource = Entitlementdata;

                dataGridView2.DataSource = EntitlementChangedata;

                ExportExcel.Show();
                Start.Hide();
                Brows.Enabled = true;
                SetLoading(false);

            });

        
        }
        private void ExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Thread threadInput = new Thread(ExportExcels);
                threadInput.Start();
            }
            catch (Exception ex)
            {

                DisplayError(ex);
            }

       
        }
        void ExportExcels()
        {
            SetLoading(true);

            if (ExcelLocation.Text != "")
            {
                ExcelUtlity ExcelUtlity = new ExcelUtlity();
                ExcelUtlity.WriteDataTableToExcel((DataTable)(dataGridView1.DataSource), "Entitlments", ExcelLocation.Text + @"\Entitlments.xlsx");
                ExcelUtlity.WriteDataTableToExcel((DataTable)(dataGridView2.DataSource), "EntitlmentsChanges", ExcelLocation.Text + @"\EntitlmentsChanges.xlsx");
                MessageBox.Show(@"Please Find Excel In " + ExcelLocation.Text);
                this.Invoke((MethodInvoker)delegate
                {
                    Save.Show();
                    ExportExcel.Hide();
                    Brows.Enabled = false;
                });

               
            }
            else
            {
                MessageBox.Show("Please Select Excel Saving Location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
            SetLoading(false);


        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                Thread threadInput = new Thread(SaveChanges);
                threadInput.Start();
            }
            catch (Exception ex)
            {

                DisplayError(ex);
            }

         
        }
        void SaveChanges()
        {
            SetLoading(true);

            DialogResult dialogResult = MessageBox.Show("Please Note This Will Save Changes ,Are Sure? :)", "attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                BackupDB BackupDB = new BackupDB();
                BackupDB.TakeBackUp();
                DialogResult dialogBackUpDone = MessageBox.Show("Please Note we Take Backup..", "attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialogBackUpDone == DialogResult.OK)
                {
                    leaveTypesBLL.Save();
                    MessageBox.Show("Done..Please Check And if There is Problem Contact to Development Team .", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
            }
            SetLoading(false);
            this.Invoke((MethodInvoker)delegate
            {
                Save.Hide();

            });
        }
            
        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("pdf", "pdf");
            list.Add("Excel", "xls");
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.DataSource = list.ToList();
            Dictionary<string, string> CompanyList = new Dictionary<string, string>();
            CompanyList.Add("3", "ALL");
            CompanyList.Add("1","Medgulf");
            CompanyList.Add("2", "Prime Health");
            SelectedCompany.DisplayMember = "Value";
            SelectedCompany.ValueMember = "Key";
            SelectedCompany.DataSource = CompanyList.ToList();
            
        }
        private void Brows_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ExcelLocation.Text = fbd.SelectedPath;

                }
            }
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    picLoader.Visible = true;
                    this.Cursor = Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    picLoader.Visible = false;
                    this.Cursor = Cursors.Default;
                });
            }
        }
        private void DisplayError(Exception ex)
        {
            MessageBox.Show("The below error occurred while processing the request: \n\r \n\r" + ex.Message);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ( PrintLocation.Text != "")
                {

                    ExportAnnualYearEntitlmentBLL ExportAnnualYearEntitlment = new ExportAnnualYearEntitlmentBLL();
                    foreach (var item in ExportAnnualYearEntitlment.GetActiveUsers(EffictiveYear.Value.Year,Convert.ToUInt16( SelectedCompany.SelectedValue)))
                    {
                        try
                        {
                            var Logs = ExportAnnualYearEntitlment.GetLeavesLogs(item.CypressID, item.UserEntitlementIDS, Convert.ToInt32(EffictiveYear.Value.Year.ToString()));
                            string path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", "") + " /Reports/UserClosingYear.rdlc";
                            UserClosingYear data = new UserClosingYear();

                            foreach (var LogItem in Logs.LeavesLogs)
                            {
                                data.LeavesLogs.AddLeavesLogsRow(LogItem.Date.ToShortDateString(), LogItem.Period.ToString(), LogItem.LeaveTypeName, LogItem.Note);
                            }
                            foreach (var EndUserEntitlmentItem in Logs.EndEntitlment.LeaveEntitlmentOfEndYear)
                            {
                                data.EndUserEntitlment.AddEndUserEntitlmentRow(EndUserEntitlmentItem.LeaveTypeName, EndUserEntitlmentItem.Entitlment.ToString());
                                Logs.EndEntitlment.TotalEndEntitlment = Logs.EndEntitlment.TotalEndEntitlment + EndUserEntitlmentItem.Entitlment;

                            }
                            foreach (var StartUserEntitlmentItem in Logs.StartEntitlmentDTO.LeaveEntitlmentOfStartYear)
                            {
                                data.StartUserEntitlment.AddStartUserEntitlmentRow(StartUserEntitlmentItem.LeaveTypeName, StartUserEntitlmentItem.Entitlment.ToString());
                                Logs.StartEntitlmentDTO.StartEntitlmentTotal = Logs.StartEntitlmentDTO.StartEntitlmentTotal + StartUserEntitlmentItem.Entitlment;
                            }
                            data.EndEntitlment.AddEndEntitlmentRow(Logs.EndEntitlment.TotalEndEntitlment.ToString());
                            data.StartEntitlment.AddStartEntitlmentRow(Logs.StartEntitlmentDTO.StartEntitlmentTotal.ToString());
                            decimal Expired = Logs.StartEntitlmentDTO.StartEntitlmentTotal - Logs.EndEntitlment.TotalEndEntitlment;
                            if (Expired < 0)
                            {
                                Expired = 0;

                            }

                            data.User.AddUserRow(item.AccID.ToString(), item.UserName, item.BirthDate, item.HireDate, EffictiveYear.Value.Year.ToString(),
                               Expired.ToString(), item.CompanyName
                                );
                            List<string> DataSetsNames = new List<string>();
                            DataSetsNames.Add("DataSet1");
                            DataSetsNames.Add("DataSet2");
                            DataSetsNames.Add("DataSet3");
                            DataSetsNames.Add("DataSet4");
                            DataSetsNames.Add("DataSet5");
                            DataSetsNames.Add("DataSet6");
                            List<string> TablesNames = new List<string>();
                            TablesNames.Add("LeavesLogs");
                            TablesNames.Add("User");
                            TablesNames.Add("StartEntitlment");
                            TablesNames.Add("EndEntitlment");
                            TablesNames.Add("StartUserEntitlment");
                            TablesNames.Add("EndUserEntitlment");
                            FileStream fs = new FileStream(PrintLocation.Text + @"\" + item.AccID + "." + comboBox1.Text.ToString(), FileMode.OpenOrCreate);
                            var bytes = Print.PrintFile(path, data, DataSetsNames, TablesNames, comboBox1.SelectedValue.ToString());
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Close();

                        }
                        catch (Exception ex)
                        {
                            continue;
                        }

                    }
                    MessageBox.Show("Success", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Print Location", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception Ex)
            {
                texterror.Text = texterror.Text + (Ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    PrintLocation.Text = fbd.SelectedPath;

                }
            }
        }

        private void PrintOne_Click(object sender, EventArgs e)
        {
            try
            {
                if (AccUserID.Text != "" && PrintLocation.Text != "")
                {
                    ExportAnnualYearEntitlmentBLL ExportAnnualYearEntitlment = new ExportAnnualYearEntitlmentBLL();
                    foreach (var item in ExportAnnualYearEntitlment.GetActiveUserByID(EffictiveYear.Value.Year, Convert.ToInt32(AccUserID.Text)))
                    {
                        var Logs = ExportAnnualYearEntitlment.GetLeavesLogs(item.CypressID, item.UserEntitlementIDS, Convert.ToInt32(EffictiveYear.Value.Year.ToString()));
                        string path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", "") + " /Reports/UserClosingYear.rdlc";
                        texterror.Text=path;
                        UserClosingYear data = new UserClosingYear();


                        foreach (var LogItem in Logs.LeavesLogs)
                        {
                            data.LeavesLogs.AddLeavesLogsRow(LogItem.Date.ToShortDateString(), LogItem.Period.ToString(), LogItem.LeaveTypeName, LogItem.Note);
                        }
                        foreach (var EndUserEntitlmentItem in Logs.EndEntitlment.LeaveEntitlmentOfEndYear)
                        {
                            data.EndUserEntitlment.AddEndUserEntitlmentRow(EndUserEntitlmentItem.LeaveTypeName, EndUserEntitlmentItem.Entitlment.ToString());
                            Logs.EndEntitlment.TotalEndEntitlment = Logs.EndEntitlment.TotalEndEntitlment + EndUserEntitlmentItem.Entitlment;

                        }
                        foreach (var StartUserEntitlmentItem in Logs.StartEntitlmentDTO.LeaveEntitlmentOfStartYear)
                        {
                            data.StartUserEntitlment.AddStartUserEntitlmentRow(StartUserEntitlmentItem.LeaveTypeName, StartUserEntitlmentItem.Entitlment.ToString());
                            Logs.StartEntitlmentDTO.StartEntitlmentTotal = Logs.StartEntitlmentDTO.StartEntitlmentTotal + StartUserEntitlmentItem.Entitlment;
                        }
                        data.EndEntitlment.AddEndEntitlmentRow(Logs.EndEntitlment.TotalEndEntitlment.ToString());
                        data.StartEntitlment.AddStartEntitlmentRow(Logs.StartEntitlmentDTO.StartEntitlmentTotal.ToString());
                        decimal Expired = Logs.StartEntitlmentDTO.StartEntitlmentTotal - Logs.EndEntitlment.TotalEndEntitlment;
                        if (Expired < 0)
                        {
                            Expired = 0;

                        }
                        data.User.AddUserRow(item.AccID.ToString(), item.UserName, item.BirthDate, item.HireDate, EffictiveYear.Value.Year.ToString()
                           , Expired.ToString(), item.CompanyName
                           );

                        List<string> DataSetsNames = new List<string>();
                        DataSetsNames.Add("DataSet1");
                        DataSetsNames.Add("DataSet2");
                        DataSetsNames.Add("DataSet3");
                        DataSetsNames.Add("DataSet4");
                        DataSetsNames.Add("DataSet5");
                        DataSetsNames.Add("DataSet6");
                        List<string> TablesNames = new List<string>();
                        TablesNames.Add("LeavesLogs");
                        TablesNames.Add("User");
                        TablesNames.Add("StartEntitlment");
                        TablesNames.Add("EndEntitlment");
                        TablesNames.Add("StartUserEntitlment");
                        TablesNames.Add("EndUserEntitlment");



                        FileStream fs = new FileStream(PrintLocation.Text + @"\" + item.AccID + "." + comboBox1.Text.ToString(), FileMode.OpenOrCreate);
                        var bytes = Print.PrintFile(path, data, DataSetsNames, TablesNames, comboBox1.SelectedValue.ToString());
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }
                    MessageBox.Show("Success", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Please Select Print Location and AccID", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception Ex)
            {
                texterror.Text = texterror.Text+(Ex.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
