using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Medivisa_Desktop
{
    public partial class Report : Form
    {
        public static string d_type;
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        public static string calender_month;
        string _date = DateTime.Today.ToString("yyyy-MM-dd h:mm tt");
        public Report()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClaimRevision cr = new ClaimRevision();
            cr.Show();
            this.Close();
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            
                //monthCalendar1.Show();
                calender_month = "from";
                    
        }

        private void textBox5_Click(object sender, EventArgs e)
        {

            //monthCalendar1.Show();
            calender_month = "to";
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if(calender_month=="from")
            {
                textBox4.Text = monthCalendar1.SelectionRange.Start.ToShortDateString();
            }
            if (calender_month == "to")
            {
                textBox5.Text = monthCalendar1.SelectionRange.Start.Date.ToShortDateString();
            }
        }

        private void textBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            monthCalendar1.Hide();
        }

        private void textBox5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            monthCalendar1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();
                 OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                if (Form1.TYPE == "puser")
                {
                    OleDbCommand s5 = new OleDbCommand("Select  Pro_pin,Pro,BATCH_NO,CLAIM_NO,Pcategory,Psub,PComment,PNotes,Pcategory2,Psub2,PComment2,PNotes2,Pcategory3,Psub3,PComment3,PNotes3,Pcategory4,Psub4,PComment4,PNotes4,PJudge,PAssigne_date From Report where PAssigne_date BETWEEN '" + Convert.ToDateTime(textBox4.Text).ToString("yyyy-MM-dd h:mm tt") + "' And '" + Convert.ToDateTime(textBox5.Text).ToString("yyyy-MM-dd h:mm tt") + "'And PAssignee = '" + Form1.NAME + "' ", con);
                    OleDbDataAdapter s6 = new OleDbDataAdapter(s5);
                    DataTable dt4 = new DataTable();
                    s6.Fill(dt4);
                    dataGridView1.DataSource = dt4;


                }
                else
                {
                    OleDbCommand s5 = new OleDbCommand("Select  Doc_id,BATCH_NO,CLAIM_NO,Comment,Judge,date From Report where date BETWEEN '" + Convert.ToDateTime(textBox4.Text).ToString("yyyy-MM-dd h:mm tt") + "' And '" + Convert.ToDateTime(textBox5.Text).ToString("yyyy-MM-dd h:mm tt") + "'And ASSIGNEE = '" + Form1.NAME + "'ORDER BY date ASC", con);
                    OleDbDataAdapter s6 = new OleDbDataAdapter(s5);
                    DataTable dt4 = new DataTable();
                    s6.Fill(dt4);
                    dataGridView1.DataSource = dt4;

                }
                label9.Visible = true;         
                label9.Text = (dataGridView1.RowCount - 1) + " Claims";
            }
            catch (Exception ex)
            {
                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException
                    || ex is ArgumentOutOfRangeException)
                {
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                }

            }     
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Form1.TYPE == "puser")
                {
                    ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("PJudge like '%true%' ");
                }
                else
                {
                    ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Judge like '%true%'  ");
                }
                label9.Visible = true;
                label9.Text = (dataGridView1.RowCount - 1) + " Claims";
            }
            catch
            {

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Form1.TYPE == "puser")
                {
                    ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("PJudge like '%Error%'");
                }
                else
                {
                    ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Judge like '%Error%' ");
                }
                label9.Visible = true;
                label9.Text = (dataGridView1.RowCount - 1) + " Claims";
               
            }
            catch
            {

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    if (Form1.TYPE == "puser")
                    {
                        ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("PJudge like '%true%' And Convert(DOC_ID, 'System.String') like '" + textBox3.Text + "%' ");
                    }
                    else
                    {
                        ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Judge like '%true%' And Convert(DOC_ID, 'System.String') like '" + textBox3.Text + "%' ");
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    if (Form1.TYPE == "puser")
                    {
                        ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("PJudge like '%Error%' And Convert(DOC_ID, 'System.String') like '" + textBox3.Text + "%' ");
                    }
                    else
                    {
                        ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Judge like '%Error%' And Convert(DOC_ID, 'System.String') like '" + textBox3.Text + "%' ");
                    }
                }
                else
                {
                    ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Convert(DOC_ID, 'System.String') like '" + textBox3.Text + "%' ");

                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException
                    || ex is ArgumentOutOfRangeException)
                {

                }

            }      
            label9.Visible = true;
            label9.Text = (dataGridView1.RowCount - 1) + " Claims";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            // saveFileDialog1.ShowDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath + "\\";
            }
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
        public void xlbod(string path, string _Name)
        {

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;

            int i = 0;
            int j = 0;
            Excel.Range rng = xlApp.get_Range("A1:A1");
            rng.Style = "Normal";
            rng.EntireRow.Font.Bold = true;
            rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown);
            rng.EntireRow.Font.Size = 12;

            //First and second rows
            xlWorkSheet.Cells[1, 1] = "DR";
            xlWorkSheet.Cells[1, 2] = "BATCH NO.";
            xlWorkSheet.Cells[1, 3] = "Claims No";
            xlWorkSheet.Cells[1, 4] = "Type Of Error";

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 3; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                }
            }
           
            xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();

            xlWorkBook.SaveAs(path + _Name + ".xlsx");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        public void P_xlbod(string path, string _Name)
        {

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;

            int i = 0;
            int j = 0;
            Excel.Range rng = xlApp.get_Range("A1:A1");
            rng.Style = "Normal";
            rng.EntireRow.Font.Bold = true;
            rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown);
            rng.EntireRow.Font.Size = 12;

            //First and second rows
            xlWorkSheet.Cells[1, 1] = "Pro PIN";
            xlWorkSheet.Cells[1, 2] = "Provider";
            xlWorkSheet.Cells[1, 3] = "Batch #";
            xlWorkSheet.Cells[1, 4] = "Claim No";
            xlWorkSheet.Cells[1, 5] = "Type Of Error";
            xlWorkSheet.Cells[1, 6] = "Sub Type Of ERROR";
            xlWorkSheet.Cells[1, 7] = "Error1";
            xlWorkSheet.Cells[1, 8] = "Notes1";
            xlWorkSheet.Cells[1, 9] = "Type Of Error2";
            xlWorkSheet.Cells[1, 10] = "Sub Type Of ERROR2";
            xlWorkSheet.Cells[1, 11] = "Error2";
            xlWorkSheet.Cells[1, 12] = "Notes2";
            xlWorkSheet.Cells[1, 13] = "Type Of Error3";
            xlWorkSheet.Cells[1, 14] = "Sub Type Of ERROR3";
            xlWorkSheet.Cells[1, 15] = "Error3";
            xlWorkSheet.Cells[1, 16] = "Notes3";
            xlWorkSheet.Cells[1, 17] = "Type Of Error4";
            xlWorkSheet.Cells[1, 18] = "Sub Type Of ERROR4";
            xlWorkSheet.Cells[1, 19] = "Error4";
            xlWorkSheet.Cells[1, 20] = "Notes4";


            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                }
            }

            Excel.Borders borders;
            rng = xlWorkSheet.get_Range("A1", "A1");
            borders = rng.Borders;
            borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Weight = 4d;            
            xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();
            xlWorkBook.SaveAs(path + _Name + ".xlsx");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Form1.TYPE == "User")
            {
                //   P_xlbod
                try
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        MessageBox.Show("Data Required not Filled");
                    }
                    else
                    {
                        xlbod(textBox2.Text, textBox1.Text);
                        MessageBox.Show("File Saved Sucessfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        MessageBox.Show("Data Required not Filled");
                    }
                    else
                    {
                        P_xlbod(textBox2.Text, textBox1.Text);
                        MessageBox.Show("File Saved Sucessfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
               
                    if (Form1.TYPE == "puser")
                    {
                        using (OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann"))
                        {
                            s1.Open();
                            OleDbCommand s7 = new OleDbCommand("Delete from Report where   PAssigne_date='" + _date + "' and PAssignee='" + Form1.NAME + "' ", s1);
                            s7.ExecuteNonQuery();
                            s7.Dispose();
                        }

                    }
                    else
                    {
                        using (OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann"))
                        {
                            s1.Open();
                            OleDbCommand s7 = new OleDbCommand("Delete from Report where   date='" + _date + "' and ASSIGNEE='" + Form1.NAME + "' ", s1);
                            s7.ExecuteNonQuery();
                            s7.Dispose();
                        }
                    }
              
            }
            catch (OleDbException ex)
            {
                if (ex.Message == "timeout")
                {
                    MessageBox.Show("Time Out ");
                }

            }
            catch (Exception ex)
            {

                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException
                    || ex is ArgumentOutOfRangeException)
                {

                }

            }
          
          
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {


                if (Form1.TYPE == "puser")
                {
                    using (OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann"))
                    {
                        s1.Open();
                        OleDbCommand s2 = new OleDbCommand("insert into Report (Pro_pin,Pro,BATCH_NO,CLAIM_NO,Pcategory,Psub,PComment,PNotes,Pcategory2,Psub2,PComment2,PNotes2,Pcategory3,Psub3,PComment3,PNotes3,Pcategory4,Psub4,PComment4,PNotes4,PJudge,PAssigne_date,PAssignee,DOC_ID) select  Pro_pin,Pro,BATCH_NO,CLAIM_NO,Pcategory,Psub,PComment,PNotes,Pcategory2,Psub2,PComment2,PNotes2,Pcategory3,Psub3,PComment3,PNotes3,Pcategory4,Psub4,PComment4,PNotes4,PJudge,PAssigne_date,'" + Form1.NAME + "',Doc_id from CLAIM_NO where PAssigne_date='" + _date + "' and PAssignee='" + Form1.NAME + "' ", s1);
                        s2.ExecuteNonQuery();
                        //MessageBox.Show("INSERT TO REPORT TABLE IS COMPLETED");
                        //s2.Dispose();
                    }
                }
                else
                {
                    OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    s1.Open();
                    OleDbCommand s2 = new OleDbCommand("insert into Report (DOC_ID,BATCH_NO,CLAIM_NO,Comment,Judge,ASSIGNEE,date) select Doc_id,BATCH_NO,CLAIM_NO,Comment,Judge,'" + Form1.NAME + "',date from CLAIM_NO where date='" + _date + "' and ASSIGNEE='" + Form1.NAME + "' ", s1);
                    s2.ExecuteNonQuery();
                    //MessageBox.Show("INSERT TO REPORT TABLE IS COMPLETED");
                    //s2.Dispose();
                }
            }

            catch (OleDbException ex)
            {
                if (ex.ErrorCode == 2147467259)
                {
                    MessageBox.Show("The changes you requested to the table were not successful because they would create duplicate values");
                }

                if (ex.Message == "timeout")
                {
                    MessageBox.Show("Timeout occurred");
                }

            }
           
           
          
        }
    }
}
