using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using Microsoft.Office;
using System.IO;
using System.Text.RegularExpressions;

namespace CallCenterExcel
{
    public partial class Form2 : Form
    {
        public static int countcolumns;
        public string connStr = @"Provider=SQLOLEDB;Data Source=10.1.1.29\MobSystems;Initial  Catalog=PhNetwork;Integrated Security=True";
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
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconnection = new SqlConnection(@"Data Source=10.1.1.29\MobSystems;Initial Catalog=PhNetwork;Integrated Security=True");

            sqlconnection.Open();
            if(radioButton1.Checked==true)
            {
                SqlCommand s2 = new SqlCommand("select * from DoctorTB", sqlconnection);
                SqlDataAdapter s3 = new SqlDataAdapter(s2);
                DataTable dt4 = new DataTable();
                s3.Fill(dt4);
                dataGridView1.DataSource = dt4;
            }
            else if(radioButton2.Checked==true)
            {
                SqlCommand s2 = new SqlCommand("select * from LaboratoryTB", sqlconnection);
                SqlDataAdapter s3 = new SqlDataAdapter(s2);
                DataTable dt4 = new DataTable();
                s3.Fill(dt4);
                dataGridView1.DataSource = dt4;
            }
            else if (radioButton3.Checked == true)
            {
                SqlCommand s2 = new SqlCommand("select * from OpticalTB", sqlconnection);
                SqlDataAdapter s3 = new SqlDataAdapter(s2);
                DataTable dt4 = new DataTable();
                s3.Fill(dt4);
                dataGridView1.DataSource = dt4;
            }
            else if (radioButton4.Checked == true)
            {
                SqlCommand s2 = new SqlCommand("select * from HospitalTB", sqlconnection);
                SqlDataAdapter s3 = new SqlDataAdapter(s2);
                DataTable dt4 = new DataTable();
                s3.Fill(dt4);
                dataGridView1.DataSource = dt4;
            }
            else if (radioButton5.Checked == true)
            {
                SqlCommand s2 = new SqlCommand("select * from PharmacyTB", sqlconnection);
                SqlDataAdapter s3 = new SqlDataAdapter(s2);
                DataTable dt4 = new DataTable();
                s3.Fill(dt4);
                dataGridView1.DataSource = dt4;
            }
            panel6.Visible = true;
           
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

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox9.Text == "" || textBox8.Text == "")
                {
                    MessageBox.Show("Data Required not Filled");
                }
                else
                {
                    if (radioButton1.Checked == true)
                    {
                        xlbod1(textBox9.Text, textBox8.Text);
                        MessageBox.Show("File Saved Sucessfully");
                    }
                    else if(radioButton2.Checked==true)
                    {
                        xlbod2(textBox9.Text, textBox8.Text);
                        MessageBox.Show("File Saved Sucessfully");
                    }
                    else if (radioButton3.Checked == true)
                    {
                        xlbod3(textBox9.Text, textBox8.Text);
                        MessageBox.Show("File Saved Sucessfully");
                    }
                    else if (radioButton4.Checked == true)
                    {
                        xlbod4(textBox9.Text, textBox8.Text);
                        MessageBox.Show("File Saved Sucessfully");
                    }
                    else if (radioButton5.Checked == true)
                    {
                        xlbod5(textBox9.Text, textBox8.Text);
                        MessageBox.Show("File Saved Sucessfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void xlbod1(string path, string _Name)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;
            int i = 0;
            int j = 0;
            int lRow = dataGridView1.RowCount + 1;
            int ldata = dataGridView1.RowCount + 1;

            xlWorkSheet.Range["A1"].Value = "DoctorName";
            xlWorkSheet.Range["B1"].Value = "DoctorAddressCode";
            xlWorkSheet.Range["C1"].Value = "DoctorAddress";
            xlWorkSheet.Range["D1"].Value = "DoctorLoc";
            xlWorkSheet.Range["E1"].Value = "Category";
            xlWorkSheet.Range["F1"].Value = "LocID";
            xlWorkSheet.Range["G1"].Value = "SubLocation Name";
            xlWorkSheet.Range["H1"].Value = "DoctorInfo";
            xlWorkSheet.Range["I1"].Value = "DoctorPhone";
            xlWorkSheet.Range["J1"].Value = "DoctorPhone2";
            xlWorkSheet.Range["K1"].Value = "DoctorPhone3";
            xlWorkSheet.Range["L1"].Value = "DoctorPhone4";
            xlWorkSheet.Range["M1"].Value = "DoctorNotes";
            xlWorkSheet.Range["N1"].Value = "DoctorLong";
            xlWorkSheet.Range["O1"].Value = "DoctorLoc";
            xlWorkSheet.Range["P1"].Value = "AccountName";
            xlWorkSheet.Range["Q1"].Value = "QNB";
            xlWorkSheet.Range["R1"].Value = "Mediam";
            xlWorkSheet.Range["S1"].Value = "Larg";
            xlWorkSheet.Range["T1"].Value = "Discount";
            xlWorkSheet.Range["U1"].Value = "ID";

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    //xlWorkSheet.Cells[i, j] = "=SUM(B2:B" + i + ")";
                }
            }
           
            xlWorkSheet.Range["A:W"].EntireColumn.AutoFit();
            //xlWorkSheet.Range["G:H"].Merge();
            xlWorkBook.SaveAs(path + _Name + ".xlsx");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        public void xlbod2(string path, string _Name)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;
            int i = 0;
            int j = 0;
            int lRow = dataGridView1.RowCount + 1;
            int ldata = dataGridView1.RowCount + 1;

            xlWorkSheet.Range["A1"].Value = "LabName";
            xlWorkSheet.Range["B1"].Value = "LabAddressCode";
            xlWorkSheet.Range["C1"].Value = "LabAddress";
            xlWorkSheet.Range["D1"].Value = "Category";
            xlWorkSheet.Range["E1"].Value = "LocID";
            xlWorkSheet.Range["F1"].Value = "LocSubLocationName";
            xlWorkSheet.Range["G1"].Value = "LabPhone";
            xlWorkSheet.Range["H1"].Value = "LabPhone2";
            xlWorkSheet.Range["I1"].Value = "LabPhone3";
            xlWorkSheet.Range["J1"].Value = "LabPhone4";
            xlWorkSheet.Range["K1"].Value = "LabNotes";
            xlWorkSheet.Range["L1"].Value = "LabLong";
            xlWorkSheet.Range["M1"].Value = "LocLat";
            xlWorkSheet.Range["N1"].Value = "Discount";
            xlWorkSheet.Range["O1"].Value = "ID";
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    //xlWorkSheet.Cells[i, j] = "=SUM(B2:B" + i + ")";
                }
            }

            xlWorkSheet.Range["A:O"].EntireColumn.AutoFit();
            //xlWorkSheet.Range["G:H"].Merge();
            xlWorkBook.SaveAs(path + _Name + ".xlsx");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        public void xlbod3(string path, string _Name)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;
            int i = 0;
            int j = 0;
            int lRow = dataGridView1.RowCount + 1;
            int ldata = dataGridView1.RowCount + 1;

            xlWorkSheet.Range["A1"].Value = "OpticalName";
            xlWorkSheet.Range["B1"].Value = "OpticalAddressCode";
            xlWorkSheet.Range["C1"].Value = "OpticalAddress";
            xlWorkSheet.Range["D1"].Value = "Category";
            xlWorkSheet.Range["E1"].Value = "LocID";
            xlWorkSheet.Range["F1"].Value = "OpticalSubLocationName";
            xlWorkSheet.Range["G1"].Value = "OpticalPhone";
            xlWorkSheet.Range["H1"].Value = "OpticalPhone2";
            xlWorkSheet.Range["I1"].Value = "OpticalPhone3";
            xlWorkSheet.Range["J1"].Value = "OpticalPhone4";
            xlWorkSheet.Range["K1"].Value = "OpticalNotes";
            xlWorkSheet.Range["L1"].Value = "OpticalLong";
            xlWorkSheet.Range["M1"].Value = "OpticicalLat";
            xlWorkSheet.Range["N1"].Value = "Discount";
            xlWorkSheet.Range["O1"].Value = "ID";
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    //xlWorkSheet.Cells[i, j] = "=SUM(B2:B" + i + ")";
                }
            }

            xlWorkSheet.Range["A:O"].EntireColumn.AutoFit();
            //xlWorkSheet.Range["G:H"].Merge();
            xlWorkBook.SaveAs(path + _Name + ".xlsx");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        public void xlbod4(string path, string _Name)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;
            int i = 0;
            int j = 0;
            int lRow = dataGridView1.RowCount + 1;
            int ldata = dataGridView1.RowCount + 1;

            xlWorkSheet.Range["A1"].Value = "HospitalName";
            xlWorkSheet.Range["B1"].Value = "AddressName";
            xlWorkSheet.Range["C1"].Value = "HospitalAddress";
            xlWorkSheet.Range["D1"].Value = "Category";
            xlWorkSheet.Range["E1"].Value = "LocID";
            xlWorkSheet.Range["F1"].Value = "SubLocationName";
            xlWorkSheet.Range["G1"].Value = "HospitalPhone";
            xlWorkSheet.Range["H1"].Value = "HospitalPhone2";
            xlWorkSheet.Range["I1"].Value = "HospitalPhone3";
            xlWorkSheet.Range["J1"].Value = "HospitalPhone4";
            xlWorkSheet.Range["K1"].Value = "HospitalNotes";
            xlWorkSheet.Range["L1"].Value = "HospitalLong";
            xlWorkSheet.Range["M1"].Value = "HospitalLat";
            xlWorkSheet.Range["N1"].Value = "QNB";
            xlWorkSheet.Range["O1"].Value = "Mediam";
            xlWorkSheet.Range["P1"].Value = "Larg";
            xlWorkSheet.Range["Q1"].Value = "Discount";
            xlWorkSheet.Range["R1"].Value = "Hospital Network";
            xlWorkSheet.Range["S1"].Value = "AddByEmp";
            xlWorkSheet.Range["T1"].Value = "AddressTime";
            xlWorkSheet.Range["U1"].Value = "ID";
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    //xlWorkSheet.Cells[i, j] = "=SUM(B2:B" + i + ")";
                }
            }

            xlWorkSheet.Range["A:V"].EntireColumn.AutoFit();
            //xlWorkSheet.Range["G:H"].Merge();
            xlWorkBook.SaveAs(path + _Name + ".xlsx");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        public void xlbod5(string path, string _Name)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;
            int i = 0;
            int j = 0;
            int lRow = dataGridView1.RowCount + 1;
            int ldata = dataGridView1.RowCount + 1;

            xlWorkSheet.Range["A1"].Value = "PharmacyName";
            xlWorkSheet.Range["B1"].Value = "PharmacyAddressCode";
            xlWorkSheet.Range["C1"].Value = "PharmacyAdress";
            xlWorkSheet.Range["D1"].Value = "LocID";
            xlWorkSheet.Range["E1"].Value = "PharmacySubLocationName";
            xlWorkSheet.Range["F1"].Value = "PharmacyPhone";
            xlWorkSheet.Range["G1"].Value = "PharmacyPhone2";
            xlWorkSheet.Range["H1"].Value = "PharmacyPhone3";
            xlWorkSheet.Range["I1"].Value = "PharmacyPhone4";
            xlWorkSheet.Range["J1"].Value = "PharmacyNotes";
            xlWorkSheet.Range["K1"].Value = "PharmacyLong";
            xlWorkSheet.Range["L1"].Value = "PharmacyLat";
            xlWorkSheet.Range["M1"].Value = "Discount";
            xlWorkSheet.Range["N1"].Value = "ID";
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    //xlWorkSheet.Cells[i, j] = "=SUM(B2:B" + i + ")";
                }
            }

            xlWorkSheet.Range["A:N"].EntireColumn.AutoFit();
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
    }
}
