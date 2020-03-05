using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using Microsoft.Office;
using System.Data.OleDb;

namespace HrReportApprovedRequests
{
    public partial class Form1 : Form
    {
        public static int countcolumns;
        public string connStr = "Provider=SQLOLEDB;Data Source=MOBILE-SYSTEM\\MOBSYSTEMS;Initial  Catalog=PhNetwork;Integrated Security=True";
        public OleDbConnection con;
        public string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public static Microsoft.Office.Interop.Excel.Workbook MyBook = null;
        public static Microsoft.Office.Interop.Excel.Application MyApp = null;
        public static Microsoft.Office.Interop.Excel.Worksheet MySheet = null;
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        public Form1()
        {
          
            InitializeComponent();
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB; Data Source=LIVEDB-1;Initial Catalog=HRMS;Integrated Security=SSPI");
            con.Open();
            OleDbCommand s1 = new OleDbCommand("select * from LeaveTypes", con);
            OleDbDataReader s2 = s1.ExecuteReader();
            if (s2.HasRows)
            {
                while (s2.Read())
                {
                    comboBox3.Items.Add(s2["LeaveTypeName"]);
                    DictionaryViewModels.dicAbsenceType.Add(s2["LeaveTypeName"].ToString(),Convert.ToUInt32(s2["LeaveTypeID"]));
                }
            }

            OleDbCommand s3 = new OleDbCommand("select * from Departments", con);
            OleDbDataReader s4 = s3.ExecuteReader();
            if (s4.HasRows)
            {
                while (s4.Read())
                {
                    comboBox4.Items.Add(s4["DepartmentName"]);
                    DictionaryViewModels.dicDeparetment.Add(s4["DepartmentName"].ToString(), Convert.ToUInt32(s4["DepartmentID"]));
                }
            }

        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                long? leaveTypeId = null;
                long? departmentId = null;
                long? subDepartmentId = null;
                long? userId = null;
                var fromDate = Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy-MM-dd 00:00:00");
                var toDate = Convert.ToDateTime(dateTimePicker2.Text).ToString("yyyy-MM-dd 23:59:59");
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB; Data Source=LIVEDB-1;Initial Catalog=HRMS;Integrated Security=SSPI");
                con.Open();
                OleDbCommand s9 = new OleDbCommand(" select rE.RequestID,U.UserName,U.AccessControlUserFK as Code, lT.LeaveTypeName ,rE.DurationFrom  as FromDate , rE.DurationTo as ToDate , rE.BackToWork , rE.RequestDuration as Duration , apRe.ActionDate as ApprovedDate from Requests as rE inner join ApprovalFlowRequestDetails as apRe on rE.RequestID = apRe.RequestFK inner join Users as U on U.UserID = rE.UserFK  inner join LeaveTypes as lT on  lT.LeaveTypeID = rE.LeaveTypeFK inner join RequestStatus as rS on rS.RequestStatusID = rE.RequesStatusFK where  rS.RequestStatusName = 'Approved' and  rE.IsDeleted = '" + false + "' and apRe.ActionDate >='" + fromDate + "' and apRe.ActionDate<= '" + toDate + "'  ", con);
                if (comboBox3.Text != "")
                {
                    leaveTypeId = DictionaryViewModels.dicAbsenceType[comboBox3.Text];
                    s9.CommandText += " and lT.LeaveTypeId = " + leaveTypeId;
                }
                if (comboBox4.Text != "")
                {
                    departmentId = DictionaryViewModels.dicDeparetment[comboBox4.Text];
                    s9.CommandText += " and Sub.DepartmentFK = " + departmentId;
                }
                if (comboBox5.Text != "")
                {
                    subDepartmentId = DictionaryViewModels.dicSubDeparetment[comboBox5.Text];
                    s9.CommandText += " and Sub.SubDepartmentID = " + subDepartmentId;
                }
                if (comboBox6.Text != "")
                {
                    userId = DictionaryViewModels.dicUser[comboBox6.Text];
                    s9.CommandText += " and U.UserID = " + userId;
                }
                OleDbDataAdapter s10 = new OleDbDataAdapter(s9);
                DataTable d4 = new DataTable();
                s10.Fill(d4);
                var newDt1 = d4.AsEnumerable()
                     .GroupBy(x => x.Field<int>("RequestID"))
                     .Select(y => y.Last())
                     .OrderBy(x => x.Field<DateTime>("ApprovedDate"))
                     .CopyToDataTable();
                dataGridView1.DataSource = newDt1;
                if (dataGridView1.Rows.Count > 0)
                {
                    panel6.Show();
                }
            }
            catch
            {
                MessageBox.Show("Data not found");
            }


        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB; Data Source=LIVEDB-1;Initial Catalog=HRMS;Integrated Security=SSPI");
            con.Open();
            OleDbCommand s5 = new OleDbCommand("select * from SubDepartments as sD inner join Departments as D on D.DepartmentID = sD.DepartmentFK where D.DepartmentName = '" + comboBox4.Text+"';", con);
            OleDbDataReader s6 = s5.ExecuteReader();
            if (s6.HasRows)
            {
                while (s6.Read())
                {
                    comboBox5.Items.Add(s6["SubDepartmentName"]);
                    DictionaryViewModels.dicSubDeparetment.Add(s6["SubDepartmentName"].ToString(), Convert.ToUInt32(s6["SubDepartmentID"]));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB; Data Source=LIVEDB-1;Initial Catalog=HRMS;Integrated Security=SSPI");
            con.Open();
            OleDbCommand s7 = new OleDbCommand("select * from Users as U inner join SubDepartments as sD on sD.SubDepartmentID = U.SubDepartmentFK where sD.SubDepartmentName = '" + comboBox5.Text + "';", con);
            OleDbDataReader s8 = s7.ExecuteReader();
            if (s8.HasRows)
            {
                while (s8.Read())
                {
                    comboBox6.Items.Add(s8["UserName"]);
                    DictionaryViewModels.dicUser.Add(s8["UserName"].ToString(), Convert.ToUInt32(s8["UserID"]));
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            // saveFileDialog1.ShowDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox9.Text = folderBrowserDialog1.SelectedPath + "\\";
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    if (textBox9.Text == "" || textBox8.Text == "")
            //    {
            //        MessageBox.Show("Data Required not Filled");
            //    }
            //    else
            //    {
            //        xlbod(textBox9.Text, textBox8.Text);
                  
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        public void xlbod(string path, string _Name)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;
            int i = 0;
            int j = 0;
            int lRow = dataGridView1.RowCount + 1;
            int ldata = dataGridView1.RowCount + 1;

            xlWorkSheet.Range["A1"].Value = "RequestID";
            xlWorkSheet.Range["B1"].Value = "UserName";
            xlWorkSheet.Range["C1"].Value = "Code";
            xlWorkSheet.Range["D1"].Value = "LeaveType";
            xlWorkSheet.Range["E1"].Value = "DurationFrom";
            xlWorkSheet.Range["F1"].Value = "DurationTo";
            xlWorkSheet.Range["G1"].Value = "BackToWork";
            xlWorkSheet.Range["H1"].Value = "Duration";
            xlWorkSheet.Range["I1"].Value = "ApprovedDate";

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    //xlWorkSheet.Cells[i, j] = "=SUM(B2:B" + i + ")";
                }
            }

            xlWorkSheet.Range["A:G"].EntireColumn.AutoFit();
            //xlWorkSheet.Range["G:H"].Merge();
            xlWorkBook.SaveAs(path + _Name + ".xlsx");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Data Required not Filled");
            }
            else
            {
                xlbod(textBox9.Text, textBox8.Text);
            }
        }
    }
}
