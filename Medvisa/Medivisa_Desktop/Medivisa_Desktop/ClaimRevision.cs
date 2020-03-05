using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Management;
using System.Threading;

namespace Medivisa_Desktop
{
    public partial class ClaimRevision : Form
    {
        private readonly SynchronizationContext sys;
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        public static string batch_no;
        public static string claim_no;
        public static string doc_no;
        public static int row_no;
        public static int row_no2;
        public static int count_user_true = 1;
        public static int count_puser_true = 1;
        public static int count_user_error = 1;
        public static int count_puser_error = 1;
        public static int count_finish = 1;
        Regex regex = new Regex(@"[\\\/:\*\?""'<>|]");
        string _date = DateTime.Today.ToString("yyyy-MM-dd h:mm tt");
        public ClaimRevision()
        {
            InitializeComponent();
            sys = SynchronizationContext.Current;
          
        }

       

        private  void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //backgroundWorker1.RunWorkerAsync();
                label2.Visible = true;
                //label7.Visible = true;

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                        if (Form1.TYPE == "puser")
                        {
                            if (radioButton1.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER where DVD_NO = '" + textBox4.Text + "'ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close();

                        dataGridView1.DataSource = dt4;

                        label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else if (radioButton2.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER where PRO_NAME = '" + textBox3.Text.Replace("'", "''") + "'ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close();
                        dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else if (radioButton3.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER where DOC_ID = '" + textBox2.Text + "' ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close();
                        dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER WHERE BATCH_NO ='" + textBox1.Text + "'ORDER BY DVD_NO DESC ", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close(); dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }

                        }
                        else if (Form1.TYPE == "User")
                        {
                            if (radioButton1.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER where DVD_NO = '" + textBox4.Text + "'ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close();
                        dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else if (radioButton2.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER where PRO_NAME = '" + textBox3.Text.Replace("'", "''") + "'ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close();
                        dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else if (radioButton3.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER where DOC_ID = '" + textBox2.Text + "' ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close(); dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else
                            {
                                OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER WHERE BATCH_NO ='" + textBox1.Text + "'ORDER BY DVD_NO DESC ", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close(); dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                        }
                        else
                        {
                            if (radioButton1.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER where DVD_NO = '" + textBox4.Text + "'ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close(); dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";

                            }
                            else if (radioButton2.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER where PRO_NAME = '" + textBox3.Text.Replace("'", "''") + "'ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close(); dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else if (radioButton3.Checked == true)
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER where DOC_ID = '" + textBox2.Text + "' ORDER BY DVD_NO DESC", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close(); dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                            else
                            {
                                OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER WHERE BATCH_NO ='" + textBox1.Text + "'ORDER BY DVD_NO DESC ", s1);
                                OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                                DataTable dt4 = new DataTable();
                        s1.Open();

                        s3.Fill(dt4);
                        s1.Close(); dataGridView1.DataSource = dt4;
                                label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                            }
                        }
                   
                        Col_adjust();
                   
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
            //Col_adjust2();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)//doctor id filter
        {
            filter();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)//batch_number filter
        {
            filter();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)//provider_name filter
        {
            filter();
        }
        private  void filter()
        {
           
                   label3.Visible = true;
                   if (radioButton3.Checked == true)
                   {

                       ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Convert(DVD_NO, 'System.String') like '" + textBox9.Text + "%' and PRO_NAME like '" + textBox7.Text.Replace("'", "''") + "%' and BATCH_NO like '" + textBox6.Text + "%' ");
                       label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";

                   }
                   else if (radioButton1.Checked == true)
                   {

                       ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("DOC_ID like '" + textBox5.Text + "%' and PRO_NAME like '" + textBox7.Text.Replace("'", "''") + "%' and BATCH_NO like '" + textBox6.Text + "%' ");
                       label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                   }
                   else if (radioButton2.Checked == true)
                   {

                       ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("DOC_ID like '" + textBox5.Text + "%' and Convert(DVD_NO, 'System.String') like '" + textBox9.Text + "%'  and BATCH_NO like '" + textBox6.Text + "%' ");
                       label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                   }
                   else
                   {

                       ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("DOC_ID like '" + textBox5.Text + "%' and Convert(DVD_NO, 'System.String') like '" + textBox9.Text + "%' and PRO_NAME like '" + textBox7.Text.Replace("'", "''") + "%' ");
                       label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                   }
              
        }

        private  void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            row_no = e.RowIndex;
            try
            {
                if (Form1.TYPE == "User")
                {
                    batch_no = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    doc_no = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
                else
                {
                    batch_no = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    doc_no = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                }

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                   s1.Open();
                   OleDbCommand s3 = new OleDbCommand("update CLAIM_NO set Doc_id ='" + doc_no + "' where BATCH_NO='" + batch_no + "'", s1);
                   s3.ExecuteNonQuery();
                   SelectComplex();
                   dataGridView1.Rows[e.RowIndex].Selected = true;
                   s1.Close();
               
            }
           
            catch (Exception ex)
            {
                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException
                    || ex is ArgumentOutOfRangeException)
                {
                    //OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    //con.Open();
                    //OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    //s10.ExecuteNonQuery();
                }
               
            }
            

           
        }
      private  void select_batcher()
        {

            OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                     if (Form1.TYPE == "puser")
                     {
                         if (radioButton1.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER where DVD_NO = '" + textBox4.Text + "'ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();

                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else if (radioButton2.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER where PRO_NAME = '" + textBox3.Text.Replace("'", "''") + "'ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close(); dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else if (radioButton3.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER where DOC_ID = '" + textBox2.Text + "' ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close(); dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID,pfinished From BATCHER WHERE BATCH_NO ='" + textBox1.Text + "'ORDER BY DVD_NO DESC ", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }

                     }
                     else if (Form1.TYPE == "User")
                     {
                         if (radioButton1.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER where DVD_NO = '" + textBox4.Text + "'ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else if (radioButton2.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER where PRO_NAME = '" + textBox3.Text.Replace("'", "''") + "'ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else if (radioButton3.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER where DOC_ID = '" + textBox2.Text + "' ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else
                         {
                             OleDbCommand s2 = new OleDbCommand("Select BATCH_NO,PRO_PIN,PRO_NAME ,TOT_CLAIM,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,DVD_NO,USER_ID,finished From BATCHER WHERE BATCH_NO ='" + textBox1.Text + "'ORDER BY DVD_NO DESC ", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                     }
                     else
                     {
                         if (radioButton1.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER where DVD_NO = '" + textBox4.Text + "'ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";

                         }
                         else if (radioButton2.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER where PRO_NAME = '" + textBox3.Text.Replace("'", "''") + "'ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else if (radioButton3.Checked == true)
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER where DOC_ID = '" + textBox2.Text + "' ORDER BY DVD_NO DESC", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                         else
                         {
                             OleDbCommand s2 = new OleDbCommand("Select PRO_PIN,PRO_NAME,BATCH_NO ,TOT_CLAIM,DVD_NO,TOT_AMOUNT,DOC_ID,AUDIT_DATE,FINAL_AUDIT,PUBLISHED,USER_ID From BATCHER WHERE BATCH_NO ='" + textBox1.Text + "'ORDER BY DVD_NO DESC ", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable dt4 = new DataTable();
                    s1.Open();

                    s3.Fill(dt4);
                    s1.Close();
                    dataGridView1.DataSource = dt4;
                             label2.Text = "Number BATCHER : " + (dataGridView1.RowCount - 1) + " Rows";
                         }
                     }
               
        }
       
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row_no = e.RowIndex;
                
               if (Form1.TYPE == "User")
                {
                    batch_no = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    doc_no = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
                else
                {
                    batch_no = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    doc_no = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                }
                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                s1.Open();
                OleDbCommand s3 = new OleDbCommand("update CLAIM_NO set Doc_id ='" + doc_no + "' where BATCH_NO='" + batch_no + "'", s1);
                s3.ExecuteNonQuery();
                SelectComplex();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                s1.Close();
            }
            
            catch (Exception ex)
            {
                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException
                    || ex is ArgumentOutOfRangeException)
                {
                    //OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    //con.Open();
                    //OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    //s10.ExecuteNonQuery();
                }

              
            }
           
        }
 
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            row_no2 = e.RowIndex;
            string location;
            if (e.RowIndex != -1)
            {

                try
                {
                    if (Form1.TYPE == "User")
                    {
                        location = dataGridView2.Rows[e.RowIndex].Cells[9].Value.ToString();
                    }
                    else if (Form1.TYPE == "puser")
                    {
                        location = dataGridView2.Rows[e.RowIndex].Cells[12].Value.ToString();
                    }
                    else
                    {
                        location = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                    }
                    System.Diagnostics.Process.Start(@"\\10.101.1.12\Area_Work\" + location);
                }
                catch
                {
                    MessageBox.Show("This batch is too old and no longer exist");
                }
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.End)
                {
                    //e.Handled = true;
                    dataGridView2.FirstDisplayedScrollingRowIndex = Convert.ToInt32(dataGridView2.Rows.Count - 1);
                    int numofRows = dataGridView2.Rows.Count;
                    dataGridView2.CurrentCell = dataGridView2.Rows[numofRows - 1].Cells[0];

                }
                else if (e.KeyCode == Keys.Home)
                {
                    //e.Handled = true;
                    dataGridView2.FirstDisplayedScrollingRowIndex = 0;
                    dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[0];

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

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView2.Rows[e.RowIndex].Selected = true;
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (Form1.TYPE == "Admin")
                {

                }
                else
                {
                    if (e.RowIndex != -1)
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString() == "finished")
                        {
                            e.CellStyle.BackColor = Color.Red;
                        }

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
   

        private  void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

           
                //try
                //{
                    
                //    OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //    s1.Open();

                //    if (Form1.TYPE == "User")
                //    {
                //        string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                //        string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");
                //        string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                //        OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='"+docid+"', Judge='Error' ,ASSIGNEE ='" + Form1.NAME + "', date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                //        s2.ExecuteNonQuery();

                //    }
                //    else
                //    {
                //        string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                //        string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");
                //        string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                //        OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='" + docid + "', pro_pin='" + pro_pin + "',pro='" + pro + "', PJudge='Error',PAssignee ='" + Form1.NAME + "' , PAssigne_date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                //        s2.ExecuteNonQuery();

                //    }

                //    s1.Close();
           

                //}
                //catch (OleDbException ex)
                //{
                //    if (ex.Message == "timeout")
                //    {
                //        MessageBox.Show("Time Out ");
                //    }

                //}
                //catch (Exception ex)
                //{
                //    if (ex is FormatException ||
                //        ex is OverflowException ||
                //        ex is ArgumentNullException
                //        || ex is ArgumentOutOfRangeException)
                //    {
                //        OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //        con.Open();
                //        OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                //        s10.ExecuteNonQuery();
                //    }

                //}
           


        }
        private void back1()
        {

            try
            {

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                s1.Open();

                if (Form1.TYPE == "User")
                {
                    string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");
                    string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                    OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='" + docid + "', Judge='Error' ,ASSIGNEE ='" + Form1.NAME + "', date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                    s2.ExecuteNonQuery();

                }
                else
                {
                    string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");
                    string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                    OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='" + docid + "', pro_pin='" + pro_pin + "',pro='" + pro + "', PJudge='Error',PAssignee ='" + Form1.NAME + "' , PAssigne_date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                    s2.ExecuteNonQuery();

                }

                s1.Close();


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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                    con.Close();
                }

            }
           
        }

        private  void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            
               
                //   try{
                //    OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //    s1.Open();

                //    if (Form1.TYPE == "User")
                //    {
                //        string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                //        string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                //        string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");
                //        OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='"+doc_no+"' Pro_pin='" + pro_pin + "',pro='" + pro + "', Judge='true',ASSIGNEE ='" + Form1.NAME + "', date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                //        s2.ExecuteNonQuery();

                //    }
                //    else
                //    {
                //        string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                //        string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");
                //        string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                //        OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='"+docid+"', pro_pin='" + pro_pin + "',pro='" + pro + "', PJudge='true',PAssignee ='" + Form1.NAME + "' , PAssigne_date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                //        s2.ExecuteNonQuery();

                //    }

                //    s1.Close();
            

                //}
                //catch (OleDbException ex)
                //{
                //    if (ex.Message == "timeout")
                //    {
                //        MessageBox.Show("Time Out ");
                //    }

                //}
                //catch (Exception ex)
                //{
                //    if (ex is FormatException ||
                //        ex is OverflowException ||
                //        ex is ArgumentNullException
                //        || ex is ArgumentOutOfRangeException)
                //    {
                //        OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //        con.Open();
                //        OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                //        s10.ExecuteNonQuery();
                //    }

                //}
          


        }
        private void back2()
        {


            try
            {
                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                s1.Open();

                if (Form1.TYPE == "User")
                {
                    string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                    string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");
                    OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='" + doc_no + "' Pro_pin='" + pro_pin + "',pro='" + pro + "', Judge='true',ASSIGNEE ='" + Form1.NAME + "', date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                    s2.ExecuteNonQuery();

                }
                else
                {
                    string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");
                    string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                    OleDbCommand s2 = new OleDbCommand("update CLAIM_NO set Doc_id='" + docid + "', pro_pin='" + pro_pin + "',pro='" + pro + "', PJudge='true',PAssignee ='" + Form1.NAME + "' , PAssigne_date ='" + _date + "' where BATCH_NO='" + batch_no + "' And CLAIM_NO='" + claim_no + "'", s1);
                    s2.ExecuteNonQuery();

                }

                s1.Close();


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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                }

            }
          


        }
        private  void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
           
                //try
                //{
                //    string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                //    string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                //    string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                //    string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                //    string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                //    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");
                    
                //    OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //    s1.Open();
                //    OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And ASSIGNEE = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                //    OleDbDataReader ldr = s2.ExecuteReader();
                //    if (ldr.HasRows)
                //    {
                //        OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', Judge = 'Error', [date] = '" + _date + "', ASSIGNEE = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                //        s3.ExecuteNonQuery();
                //    }
                //    else
                //    {
                //        OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,Judge,[date],ASSIGNEE,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','Error','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                //        s3.ExecuteNonQuery();
                //    }
                //    ++count_user_error;
                //    s1.Close();
                   

                //}
                //catch (OleDbException ex)
                //{
                //    if (ex.Message == "timeout")
                //    {
                //        MessageBox.Show("Time Out ");
                //    }

                //}
                //catch (Exception ex)
                //{
                //    if (ex is FormatException ||
                //        ex is OverflowException ||
                //        ex is ArgumentNullException
                //        || ex is ArgumentOutOfRangeException)
                //    {
                //        OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //        con.Open();
                //        OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                //        s10.ExecuteNonQuery();
                //    }

                //}
           
        }
        private void back4()
        {
            try
            {
                string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                s1.Open();
                OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And ASSIGNEE = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                OleDbDataReader ldr = s2.ExecuteReader();
                if (ldr.HasRows)
                {
                    OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', Judge = 'Error', [date] = '" + _date + "', ASSIGNEE = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                    s3.ExecuteNonQuery();
                }
                else
                {
                    OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,Judge,[date],ASSIGNEE,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','Error','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                    s3.ExecuteNonQuery();
                }
                ++count_user_error;
                s1.Close();


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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                    con.Close();

                }

            }
        }
        private  void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)//calim_error_Puser_Report
        {
           
                //try
                //{

                //    string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                //    string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                //    string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                //    string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                //    string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                //    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");
                     
                //    OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //    s1.Open();
                //    OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And PAssignee = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                //    OleDbDataReader ldr = s2.ExecuteReader();
                //    if (ldr.HasRows)
                //    {
                //        OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', PJudge = 'Error', PAssigne_date = '" + _date + "', PAssignee = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                //        s3.ExecuteNonQuery();

                //    }
                //    else
                //    {
                //        OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,PJudge,PAssigne_date,PAssignee,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','Error','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                //        s3.ExecuteNonQuery();

                //    }
                //    ++count_puser_error;
                //    s1.Close();
      

                //}
                //catch (OleDbException ex)
                //{
                //    if (ex.Message == "timeout")
                //    {
                //        MessageBox.Show("Time Out ");
                //    }

                //}
                //catch (Exception ex)
                //{
                //    if (ex is FormatException ||
                //        ex is OverflowException ||
                //        ex is ArgumentNullException
                //        || ex is ArgumentOutOfRangeException)
                //    {
                //        OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //        con.Open();
                //        OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                //        s10.ExecuteNonQuery();
                //    }

                //}
            
        }
        private void back5()
        {
            try
            {

                string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                s1.Open();
                OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And PAssignee = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                OleDbDataReader ldr = s2.ExecuteReader();
                if (ldr.HasRows)
                {
                    OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', PJudge = 'Error', PAssigne_date = '" + _date + "', PAssignee = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                    s3.ExecuteNonQuery();

                }
                else
                {
                    OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,PJudge,PAssigne_date,PAssignee,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','Error','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                    s3.ExecuteNonQuery();

                }
                ++count_puser_error;
                s1.Close();


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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                    con.Close();
                }

            }
            
        }
        private  void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
           
                //try
                //{

                //    string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                //    string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                //    string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                //    string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                //    string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                //    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");
                    
                //    OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //    s1.Open();
                //    OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And ASSIGNEE = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                //    OleDbDataReader ldr = s2.ExecuteReader();
                //    if (ldr.HasRows)
                //    {
                //        OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', Judge = 'true', [date] = '" + _date + "', ASSIGNEE = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                //        s3.ExecuteNonQuery();

                //    }
                //    else
                //    {
                //        OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,Judge,[date],ASSIGNEE,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','true','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                //        s3.ExecuteNonQuery();

                //    }
                //    ++count_user_true;
                //    s1.Close();

            

                //}
                //catch (OleDbException ex)
                //{
                //    if (ex.Message == "timeout")
                //    {
                //        MessageBox.Show("Time Out ");
                //    }

                //}
                //catch (Exception ex)
                //{
                //    if (ex is FormatException ||
                //        ex is OverflowException ||
                //        ex is ArgumentNullException
                //        || ex is ArgumentOutOfRangeException)
                //    {
                //        OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //        con.Open();
                //        OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                //        s10.ExecuteNonQuery();
                //    }
                //    //MessageBox.Show("errorrrrrr");
                //}
            

        }
        private void back6()
        {
            try
            {

                string docid = dataGridView2.Rows[row_no2].Cells[10].Value.ToString();
                string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                string pro_pin = dataGridView1.Rows[row_no].Cells[1].Value.ToString();
                string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[2].Value.ToString(), " ");

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                s1.Open();
                OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And ASSIGNEE = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                OleDbDataReader ldr = s2.ExecuteReader();
                if (ldr.HasRows)
                {
                    OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', Judge = 'true', [date] = '" + _date + "', ASSIGNEE = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                    s3.ExecuteNonQuery();

                }
                else
                {
                    OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,Judge,[date],ASSIGNEE,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','true','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                    s3.ExecuteNonQuery();

                }
                ++count_user_true;
                s1.Close();



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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                    con.Close();

                }
                //MessageBox.Show("errorrrrrr");
            }
        }

        private  void backgroundWorker7_DoWork(object sender, DoWorkEventArgs e)//new update
        {
           
                //try
                //{

                //    string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                //    string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                //    string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                //    string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                //    string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                //    string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");
                  
                //    OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //    s1.Open();
                //    OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And PAssignee = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                //    OleDbDataReader ldr = s2.ExecuteReader();
                //    if (ldr.HasRows)
                //    {
                //        OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', PJudge = 'true', PAssigne_date = '" + _date + "', PAssignee = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                //        s3.ExecuteNonQuery();

                //    }
                //    else
                //    {
                //        OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,PJudge,PAssigne_date,PAssignee,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','true','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                //        s3.ExecuteNonQuery();

                //    }

                //    ++count_puser_true;
                //    s1.Close();
            

                //}
                //catch (OleDbException ex)
                //{
                //    if (ex.Message == "timeout")
                //    {
                //        MessageBox.Show("Time Out ");
                //    }

                //}
                //catch (Exception ex)
                //{
                //    if (ex is FormatException ||
                //        ex is OverflowException ||
                //        ex is ArgumentNullException
                //        || ex is ArgumentOutOfRangeException)
                //    {
                //        OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                //        con.Open();
                //        OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                //        s10.ExecuteNonQuery();
                //    }

                //}
           

        }
        private void back7()
        {
            try
            {

                string docid = dataGridView2.Rows[row_no2].Cells[13].Value.ToString();
                string rej1 = dataGridView2.Rows[row_no2].Cells[5].Value.ToString();
                string rej2 = dataGridView2.Rows[row_no2].Cells[6].Value.ToString();
                string docjdge = dataGridView2.Rows[row_no2].Cells[7].Value.ToString();
                string pro_pin = dataGridView1.Rows[row_no].Cells[0].Value.ToString();
                string pro = regex.Replace(dataGridView1.Rows[row_no].Cells[1].Value.ToString(), " ");

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                s1.Open();
                OleDbCommand s2 = new OleDbCommand(" SELECT BATCH_NO FROM Report WHERE BATCH_NO = '" + batch_no + "' And PAssignee = '" + Form1.NAME + "' And CLAIM_NO = '" + claim_no + "'", s1);
                OleDbDataReader ldr = s2.ExecuteReader();
                if (ldr.HasRows)
                {
                    OleDbCommand s3 = new OleDbCommand("update Report set Pro_pin ='" + pro_pin + "' , Pro = '" + pro + "', DOC_ID ='" + docid + "' , BATCH_NO = '" + batch_no + "', CLAIM_NO = '" + claim_no + "', PJudge = 'true', PAssigne_date = '" + _date + "', PAssignee = '" + Form1.NAME + "', rej1 = '" + rej1 + "', rej2 = '" + rej2 + "' where BATCH_NO = '" + batch_no + "' And CLAIM_NO ='" + claim_no + "'", s1);
                    s3.ExecuteNonQuery();

                }
                else
                {
                    OleDbCommand s3 = new OleDbCommand("insert into Report (Pro_pin,Pro,DOC_ID,BATCH_NO,CLAIM_NO,PJudge,PAssigne_date,PAssignee,rej1,rej2) values ('" + pro_pin + "','" + pro + "','" + docid + "','" + batch_no + "','" + claim_no + "','true','" + _date + "','" + Form1.NAME + "','" + rej1 + "','" + rej2 + "')", s1);
                    s3.ExecuteNonQuery();

                }

                ++count_puser_true;
                s1.Close();


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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                    con.Close();

                }

            }
           
        }
        private  void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            row_no2 = e.RowIndex;
            //OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            //s1.Open();
            if (Form1.TYPE == "User" || Form1.TYPE == "puser")
            {
                try
                {
                    dataGridView2.Rows[e.RowIndex].Selected = true;
                    if (e.RowIndex != -1)
                    {
                        claim_no = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();

                        if (dataGridView2.CurrentCell.ColumnIndex == 0 && dataGridView2.SelectedCells[1].Value.ToString() != "1")
                        {
                            string message = "IS there  error in this claim ?";
                            string caption = "Reminder";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            DialogResult result;
                            result = MessageBox.Show(this, message, caption, buttons,
                                MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                back1();
                               SelectComplex();
                               filter2();
                               Col_adjust2();
                                Errcomment error = new Errcomment();
                                error.Show();
                                if (Form1.TYPE == "User")
                                {
                                    back4();
                                  
                                   //Col_adjust2();
                                    label8.Visible = true;
                                    label8.Text = "Error Claims : " + count_user_error.ToString();
                                }
                                else
                                {
                                    back5();
                                    
                                    label8.Visible = true;
                                    label8.Text = "Error Claims : " + count_puser_error.ToString();
                                }
                               
                                //SelectComplex();
                                dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
                              
                            }
                            else if (result == DialogResult.No)
                            {
                                back2();
                                SelectComplex();
                                filter2();
                                Col_adjust2();
                                if (Form1.TYPE == "User")
                                {
                                    //claim_true_user_report();
                                    back6();
                                   
                                    //Col_adjust2();
                                    label7.Visible = true;
                                    label7.Text ="True Claims : "+ count_user_true.ToString();
                                }
                                else
                                {
                                    //claim_true_puser_report();
                                    back7();
                                   
                                    //Col_adjust2();
                                    label7.Visible = true;
                                    label7.Text = "True Claims : " + count_puser_true.ToString();
                                }
                               
                                //SelectComplex();
                                dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
                              
                            }
                          
                        }
                        if (dataGridView2.CurrentCell.RowIndex == 0 && dataGridView2.CurrentCell.ColumnIndex == 0 && dataGridView2.SelectedCells[1].Value.ToString() == "1")
                        {

                            string message = "Finished this batch ?";
                            string caption = "Reminder";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            DialogResult result;
                            // Displays the MessageBox.
                            result = MessageBox.Show(this, message, caption, buttons,
                                MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)//update batcher to finshed
                            {                               
                                if (Form1.TYPE == "User")
                                {
                                    backgroundWorker8.RunWorkerAsync();
                                    select_batcher();
                                    label9.Visible = true;
                                    label9.Text = "Finished Batches : " + count_finish.ToString();
                                   
                                }
                                else
                                {

                                    backgroundWorker9.RunWorkerAsync();
                                    select_batcher();
                                    //Col_adjust();
                                    label9.Visible = true;
                                    label9.Text = "Finished Batches : " + count_finish.ToString();
                                    
                                }
                                filter();
                                select_batcher();
                                //Col_adjust();
                             
                            }
                            else if (result == DialogResult.No)
                            {
                                try
                                {
                                    dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
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
                                        con.Close();

                                    }

                                }
                                filter();
                                Col_adjust();
                            }
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

            else
            {
            }
         
        }
      
        private void Col_adjust()
        {

            dataGridView1.Columns[0].Width = 135;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[8].Width = 70;
        }

        private void Col_adjust2()
        {

            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[2].Width = 50;
            dataGridView2.Columns[3].Width = 140;
            dataGridView2.Columns[4].Width = 115;
            dataGridView2.Columns[5].Width = 50;
            dataGridView2.Columns[6].Width = 50;
            dataGridView2.Columns[10].Width = 150;
            dataGridView2.Columns[8].Width = 300;
            dataGridView2.Columns[9].Width = 150;
        }
   


        private  void backgroundWorker8_DoWork(object sender, DoWorkEventArgs e)
        {
            
                   try
                   {

                       OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                            s1.Open();
                            OleDbCommand s2 = new OleDbCommand("update BATCHER set finished ='finished' where BATCH_NO='" + batch_no + "'", s1);
                            s2.ExecuteNonQuery();
                            s1.Close();
                            count_finish++;
                        


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
                           OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                           con.Open();
                           OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                           s10.ExecuteNonQuery();
                       }

                   }
               
        }
        private  void backgroundWorker9_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                             s1.Open();
                             OleDbCommand s2 = new OleDbCommand("update BATCHER set pfinished ='finished' where BATCH_NO='" + batch_no + "' ", s1);
                             s2.ExecuteNonQuery();
                             s1.Close();
                             count_finish++;
                        
                

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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
        private void SelectComplex()
        {
            try
            {
                OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                
                label6.Visible = true;
                if (Form1.TYPE == "puser")
                {
                    OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO,CLAIM_PAGE,BATCH_NO,DIAGNO,REJ_CODE,REJ_PRO,PJudge,PComment,Pcomment2,Pcomment3,Pcomment4,SCAN_LOC,Doc_id From CLAIM_NO where BATCH_NO = '" + batch_no + "' ORDER BY CLAIM_NO ASC", s1);
                    OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                    DataTable s4 = new DataTable();
                    s1.Open();
                    s3.Fill(s4);
                    s1.Close();
                    dataGridView2.DataSource = s4;
                    label6.Text = " Claims : " + (dataGridView2.RowCount - 2) + " Rows";
                }
                else if (Form1.TYPE == "User")
                {
                    OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO,CLAIM_PAGE,BATCH_NO,DIAGNO,REJ_CODE,REJ_PRO,Judge,Comment,SCAN_LOC,Doc_id From CLAIM_NO where BATCH_NO = '" + batch_no + "' ORDER BY CLAIM_NO ASC", s1);
                    OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                    DataTable s4 = new DataTable();
                    s1.Open();
                    s3.Fill(s4);
                    s1.Close(); dataGridView2.DataSource = s4;
                    label6.Text = "Claims : " + (dataGridView2.RowCount - 2) + " Rows";
                }
                else
                {
                    OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO,CLAIM_PAGE,BATCH_NO,DIAGNO,REJ_CODE,REJ_PRO,SCAN_LOC,Doc_id From CLAIM_NO where BATCH_NO = '" + batch_no + "' ORDER BY CLAIM_NO ASC", s1);
                    OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                    DataTable s4 = new DataTable();
                    s1.Open();
                    s3.Fill(s4);
                    s1.Close();
                    dataGridView2.DataSource = s4;
                    label6.Text = "Claims : " + (dataGridView2.RowCount - 2) + " Rows";
                }
                s1.Close();
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
                    OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                    con.Open();
                    OleDbCommand s10 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                    s10.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString() == "Error")
                    {
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else if (dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString() == "true")
                    {
                        e.CellStyle.BackColor = Color.Green;
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
        private void filter2()
        {
            try
            {
                ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = string.Format("DIAGNO like '" + textBox8.Text + "%' ");

            }
            catch
            {

            }
        }
        private  void textBox8_TextChanged(object sender, EventArgs e)
        {
            filter2();
        }
         public void P_xlbod(DataGridView dataGridView1)
        {

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;

            int i = 0;
            int j = 0;
            Excel.Range rng = xlApp.get_Range("A1:A1");
            rng.Style = "Normal";       
            rng.EntireRow.Font.Size = 12;
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i +1, j + 1] = cell.Value;
                }
            }

            Excel.Borders borders;
            rng = xlWorkSheet.get_Range("A1", "A1");
            borders = rng.Borders;
            borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Weight = 4d;
            xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();

            xlWorkBook.SaveAs(@"D:\Report For "+Form1.NAME+".xlsx");
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

         

         private void button2_Click(object sender, EventArgs e)
         {
             string message = " Are You Sure To export this excel ?";
             string caption = "Reminder";
             MessageBoxButtons buttons = MessageBoxButtons.YesNo;
             DialogResult result;

             // Displays the MessageBox.

             result = MessageBox.Show(this, message, caption, buttons,
                 MessageBoxIcon.Question);

             if (result == DialogResult.No)
             {
             }

             else
             {
                 P_xlbod(dataGridView2);

             }
         }

         private void button3_Click(object sender, EventArgs e)
         {
             Report rep = new Report();
             rep.Show();
             //this.Close();
         }

        

         private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
         {
             try
             {
                 if (e.KeyCode == Keys.End)
                 {
                     //e.Handled = true;
                     dataGridView1.FirstDisplayedScrollingRowIndex = Convert.ToInt32(dataGridView1.Rows.Count - 1);
                     int numofRows = dataGridView1.Rows.Count;
                     dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                 }
                 else if (e.KeyCode == Keys.Home)
                 {
                     //e.Handled = true;
                     dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                     dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
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
         

         

         private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {
             try
             {
                 textBox3.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                 dataGridView3.Hide();
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

         private void textBox3_TextChanged(object sender, EventArgs e)
         {
             try
             {
               
                 ((DataTable)dataGridView3.DataSource).DefaultView.RowFilter = string.Format(" PRO_NAME like '" + textBox3.Text.Replace("'","''") + "%' ");

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

         private void textBox3_MouseDoubleClick(object sender, MouseEventArgs e)
         {
             dataGridView3.Hide();
         }

         private   void textBox3_KeyPress(object sender, KeyPressEventArgs e)
         {
             dataGridView3.Show();                     
             try
             {

                 OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                             OleDbCommand s2 = new OleDbCommand("select distinct PRO_PIN,PRO_NAME From BATCHER", s1);
                             OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                             DataTable s4 = new DataTable();
                s1.Open();

                s3.Fill(s4);
                           s1.Close();

                dataGridView3.DataSource = s4;
                             dataGridView3.Columns[1].Width = 400;
                             dataGridView3.Columns[0].Width = 70;
                        
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

         private void textBox9_TextChanged(object sender, EventArgs e)
         {
             filter();
         }

        

         private async void backgroundWorker10_DoWork(object sender, DoWorkEventArgs e)
         {
             try
             {
                 await Task.Run(()=>{
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
                 });
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
             GC.Collect();
             GC.WaitForPendingFinalizers();
             GC.Collect();
         }

         private void backgroundWorker10_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                         MessageBox.Show("INSERT TO REPORT TABLE IS COMPLETED");
                         s2.Dispose();
                        s1.Close();

                    }
                }
                 else
                 {
                     OleDbConnection s1 = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                     s1.Open();
                     OleDbCommand s2 = new OleDbCommand("insert into Report (DOC_ID,BATCH_NO,CLAIM_NO,Comment,Judge,ASSIGNEE,date) select Doc_id,BATCH_NO,CLAIM_NO,Comment,Judge,'" + Form1.NAME + "',date from CLAIM_NO where date='" + _date + "' and ASSIGNEE='" + Form1.NAME + "' ", s1);
                     s2.ExecuteNonQuery();
                     MessageBox.Show("INSERT TO REPORT TABLE IS COMPLETED");
                     s2.Dispose();
                    s1.Close();

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
             GC.Collect();
             GC.WaitForPendingFinalizers();
             GC.Collect();
         }


         private void button4_Click(object sender, EventArgs e)
         {
             backgroundWorker10.RunWorkerAsync();
         }
    }
}
