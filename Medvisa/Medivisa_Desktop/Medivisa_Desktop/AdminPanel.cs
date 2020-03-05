using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Medivisa_Desktop
{
    public partial class AdminPanel : Form
    {
        public static string _type;
        public static string clander_type;
         Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        public AdminPanel()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            //comboBox3.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                label7.Visible = true;
                
            }
           else if (textBox2.Text == "")
            {
                label8.Visible = true;
            }
           else if (textBox3.Text == "")
            {
                label9.Visible = true;
                label9.Text = "Confirm Password Required";
            }
            else if (textBox3.Text != textBox2.Text)
            {
                label9.Visible = true;
                label9.Text = "Confirm Password is not match with password";
            }
            else
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand s5 = new OleDbCommand("insert into User_Login (Name,Pass,Type) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')", con);
                s5.ExecuteNonQuery();
                label6.Visible = true;
                label6.Text = "User is addded sucessfully";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s5 = new OleDbCommand("Select * from User_Login", con);
            OleDbDataAdapter s6 = new OleDbDataAdapter(s5);
            DataTable s7 = new DataTable();
            s6.Fill(s7);
            dataGridView1.DataSource = s7;
            label12.Visible = true;
            label12.Text = dataGridView1.RowCount.ToString()+ " Users";
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            label16.Visible = true;
            label16.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox4.Text=="")
            {
                label17.Visible = true;
                label7.Text = "User is updated Faild";

            }
            else
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand s5 = new OleDbCommand("Update User_Login set Name ='" + textBox4.Text + "' , Pass='" + textBox5.Text + "', Type='" + comboBox2.Text + "' where id='" + label16.Text + "'", con);
                s5.ExecuteNonQuery();
                label17.Visible = true;
                label17.Text = "User is updated sucessfully";
                dataGridView1.Refresh();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            if (textBox6.Text == "" || textBox7.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Data is not completed !");
            }


           else if (_type == "puser")
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand s5 = new OleDbCommand("Select  BATCH_NO,CLAIM_NO,PJudge,PComment,PAssigne_date,Pro,Pro_pin,DOC_ID,rej1,rej2,REPLACE(BATCH_NO+'\\'+'claim_'+CLAIM_NO+'.tif',' ','') As SCAN_LOC  From Report where PAssigne_date BETWEEN '" + Convert.ToDateTime(textBox6.Text).ToString("yyyy-MM-dd h:mm tt") + "' And '" + Convert.ToDateTime(textBox7.Text).ToString("yyyy-MM-dd h:mm tt") + "' And PAssignee= '" + comboBox3.Text.ToString() + "'", con);
                OleDbDataAdapter s6 = new OleDbDataAdapter(s5);
                DataTable dt4 = new DataTable();
                s6.Fill(dt4);
                dataGridView2.DataSource = dt4;
            }
            else
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand s5 = new OleDbCommand("Select  BATCH_NO,CLAIM_NO,Judge,Comment,date,Pro,Pro_pin,DOC_ID,rej1,rej2,REPLACE(BATCH_NO+'\\'+'claim_'+CLAIM_NO+'.tif',' ','') As SCAN_LOC From Report where date BETWEEN '" + Convert.ToDateTime(textBox6.Text).ToString("yyyy-MM-dd h:mm tt") + "' And '" + Convert.ToDateTime(textBox7.Text).ToString("yyyy-MM-dd h:mm tt") + "' And  ASSIGNEE = '" + comboBox3.Text.ToString() + "'", con);
                OleDbDataAdapter s6 = new OleDbDataAdapter(s5);
                DataTable dt4 = new DataTable();
                s6.Fill(dt4);
                dataGridView2.DataSource = dt4;
               
            }
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            label24.Visible = true;
            label24.Text = "Claims : " + (dataGridView2.RowCount-1);
           
        }

        private void comboBox3_MouseEnter(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s2 = new OleDbCommand("Select Name from User_Login", con);
            OleDbDataReader s3 = s2.ExecuteReader();
            if(s3.HasRows)
            {
                while(s3.Read())
                {
                    comboBox3.Items.Add(s3["Name"]);
                }
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s2 = new OleDbCommand("Select Type from User_Login where Name='" + comboBox3.Text + "' ", con);
            OleDbDataReader s3 = s2.ExecuteReader();
            if (s3.HasRows)
            {
                while (s3.Read())
                {
                    _type = (s3["Type"]).ToString();
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (clander_type == "from")
            {
                textBox6.Text = monthCalendar1.SelectionRange.Start.Date.ToShortDateString();
                
            }
            else
            {
                textBox7.Text = monthCalendar1.SelectionRange.Start.Date.ToShortDateString();
               
            }
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            //monthCalendar1.Show();
            clander_type = "from";
            
        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            //monthCalendar1.Show();
            clander_type = "to";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (_type == "puser")
            {
                ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = string.Format("PJudge like '%true%'");
                label24.Visible = true;
                label24.Text = "True Claims : " + (dataGridView2.RowCount-1);
            }
            else
            {
                ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = string.Format("Judge like '%true%'");
                label24.Visible = true;
                label24.Text = "True Claims : " + (dataGridView2.RowCount-1);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (_type == "puser")
            {
                ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = string.Format("PJudge like '%Error%'");
                label24.Visible = true;
                label24.Text = "Error Claims : " + (dataGridView2.RowCount-1);
            }
            else
            {
                ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = string.Format("Judge like '%Error%'");
                label24.Visible = true;
                label24.Text = "Error Claims : " + (dataGridView2.RowCount-1);
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
                    xlbod(textBox9.Text, textBox8.Text);
                    MessageBox.Show("File Saved Sucessfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void xlbod(string path, string _Name)
        {

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;

            int i = 0;
            int j = 0;
            Excel.Range rng = xlApp.get_Range("C2");
            xlWorkSheet.Range["C2"].Value = "Report for user (" + comboBox3.Text + " )";
            rng.Style = "Normal";
            rng.EntireRow.Font.Bold = true;
            rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown);
            rng.EntireRow.Font.Size = 36;

            Excel.Range rng1 = xlApp.get_Range("C2");
            // xlWorkSheet.Range["C2"].Value = "Report for user (" + users_cmbo.Text + " )";
            rng1.Style = "Normal";
            rng1.EntireRow.Font.Bold = true;
            rng1.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
            rng1.EntireRow.Font.Size = 12;

            Excel.Range rng2 = xlApp.get_Range("A4");
            // xlWorkSheet.Range["C2"].Value = "Report for user (" + users_cmbo.Text + " )";
            rng2.Style = "Normal";
            rng2.EntireRow.Font.Bold = true;
            rng2.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
            rng2.EntireRow.Font.Size = 12;
            xlWorkSheet.Range["A4"].Value = "BATCH N.";
            xlWorkSheet.Range["B4"].Value = "Claims No";
            xlWorkSheet.Range["C4"].Value = "Judge";
            xlWorkSheet.Range["D4"].Value = "Comments";
            xlWorkSheet.Range["E4"].Value = "Date";
            xlWorkSheet.Range["F4"].Value = "ProviderName";
            xlWorkSheet.Range["G4"].Value = "ProPin";
            xlWorkSheet.Range["H4"].Value = "DocID";
            xlWorkSheet.Range["I4"].Value = "RejCode";
            xlWorkSheet.Range["J4"].Value = "RejPro";
            xlWorkSheet.Range["K4"].Value = "SCAN_LOC";
            for (i = 0; i <= dataGridView2.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView2.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView2[j, i];
                    xlWorkSheet.Cells[i + 6, j + 1] = cell.Value;
                }
            }
           
            xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();
            xlWorkBook.SaveAs(path + _Name + ".csv");
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

        private void textBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            monthCalendar1.Hide();
        }

        private void textBox7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            monthCalendar1.Hide();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string location;
            if (e.RowIndex != -1)
            {

                try
                {
                    if (Form1.TYPE == "User")
                    {
                        location = dataGridView2.Rows[e.RowIndex].Cells[10].Value.ToString();
                    }
                    else
                    {
                        location = dataGridView2.Rows[e.RowIndex].Cells[10].Value.ToString();
                    }

                    System.Diagnostics.Process.Start(@"\\10.101.1.12\Area_Work\" + location);
                }
                catch
                {
                    MessageBox.Show("This batch is too old and no longer exist");
                }
            }
        }

    }
}
