using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medivisa_Desktop
{
    public partial class Errcomment : Form
    {
        public static int status;
        //string SQL_str;
        public Errcomment()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Category like '%" + textBox1.Text.Replace(" ","") + "%' ");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Category like '" + comboBox1.Text + "%' ");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Category like '" + comboBox2.Text + "%' ");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Subcategory like '" + comboBox3.Text + "%' And Category like '" + comboBox2.Text + "%' ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = (@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann"); // making connection  
            con.Open();
            status = status + 1;
            if (status == 1)
            {
                //  prepare();
                if (Form1.TYPE == "puser")
                {
                    OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO, CLAIM_PAGE, BATCH_NO, DIAGNO, REJ_CODE, REJ_PRO, PJudge, PComment, SCAN_LOC,Doc_id From CLAIM_NO where BATCH_NO = '" + ClaimRevision.batch_no + "' ORDER BY CLAIM_NO ASC", con);      
                    OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                    OleDbDataReader s4 = s2.ExecuteReader();
                    if(s4.Read())
                    {
                        OleDbCommand s5 = new OleDbCommand("update CLAIM_NO set PComment ='" + textBox2.Text + "',Pcategory ='" + comboBox2.Text + "',Psub ='" + comboBox3.Text + "',PNotes ='" + textBox3.Text + "',PJudge = 'Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                        s5.ExecuteNonQuery();
                        OleDbCommand s6 = new OleDbCommand("update Report set PComment ='" + textBox2.Text + "',Pcategory ='" + comboBox2.Text + "',Psub ='" + comboBox3.Text + "',PNotes ='" + textBox3.Text + "',PJudge = 'Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'",con);
                        s6.ExecuteNonQuery();
                    }
                                    
                    button1.Text = "Add 2nd comment";
                }

                else
                {
                    OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO,CLAIM_PAGE,BATCH_NO,DIAGNO,REJ_CODE,REJ_PRO,Judge,Comment,SCAN_LOC ,Doc_id  From CLAIM_NO where BATCH_NO = '" + ClaimRevision.batch_no + "' ORDER BY CLAIM_NO ASC", con);              
                    OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                    OleDbDataReader s4 = s2.ExecuteReader();
                    if (s4.Read())
                    {
                        OleDbCommand s5 = new OleDbCommand("update CLAIM_NO set Comment ='" + textBox2.Text + "',Judge = 'Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                        s5.ExecuteNonQuery();
                        OleDbCommand s6 = new OleDbCommand("update Report set Comment ='" + textBox2.Text + "',Judge = 'Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);       
                        s6.ExecuteNonQuery();
                    }
                    
                    this.Close();
                }
                prepare();

            }
            else if (status == 2)
            {

                string message = " Are You Sure To add second comment ?";
                string caption = "Reminder";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(this, message, caption, buttons,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    this.Close();
                    //ClaimRevision cr = new ClaimRevision();
                    //cr.Show();
                }

                else
                {
                    if (Form1.TYPE == "puser")
                    {
                        OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO, CLAIM_PAGE, BATCH_NO, DIAGNO, REJ_CODE, REJ_PRO, PJudge, PComment, SCAN_LOC,Doc_id From CLAIM_NO where BATCH_NO = '" + ClaimRevision.batch_no + "' ORDER BY CLAIM_NO ASC", con);                  
                        OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                        OleDbDataReader s4 = s2.ExecuteReader();
                        if (s4.Read())
                        {
                            OleDbCommand s5 = new OleDbCommand("update CLAIM_NO set PComment2 ='" + textBox2.Text + "',Pcategory2 ='" + comboBox2.Text + "',Psub2 ='" + comboBox3.Text + "',PNotes2 ='" + textBox3.Text + "',PJudge ='Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                            s5.ExecuteNonQuery();
                            OleDbCommand s6 = new OleDbCommand("update Report set PComment2 ='" + textBox2.Text + "',Pcategory2 ='" + comboBox2.Text + "',Psub2 ='" + comboBox3.Text + "',PNotes2 ='" + textBox3.Text + "',PJudge ='Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                            s6.ExecuteNonQuery();
                        }
                                              
                    }
                    prepare();

                    button1.Text = "Add 3rd comment";
                }
            }
            else if (status == 3)
            {
                string message = " Are You Sure To add third comment?";
                string caption = "Reminder";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(this, message, caption, buttons,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    this.Close();
                    //ClaimRevision cr = new ClaimRevision();
                    //cr.Show();
                }

                else
                {
                    //  prepare();
                    if (Form1.TYPE == "puser")
                    {
                        OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO, CLAIM_PAGE, BATCH_NO, DIAGNO, REJ_CODE, REJ_PRO, PJudge, PComment, SCAN_LOC,Doc_id From CLAIM_NO where BATCH_NO = '" + ClaimRevision.batch_no + "' ORDER BY CLAIM_NO ASC", con);
                       
                        OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                        OleDbDataReader s4 = s2.ExecuteReader();
                        if (s4.Read())
                        {
                            OleDbCommand s5 = new OleDbCommand("update CLAIM_NO set PComment3 ='" + textBox2.Text + "',Pcategory3 ='" + comboBox2.Text + "',Psub3 ='" + comboBox3.Text + "',PNotes3 ='" + textBox3.Text + "',PJudge ='Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                            s5.ExecuteNonQuery();
                            OleDbCommand s6 = new OleDbCommand("update Report set PComment3 ='" + textBox2.Text + "',Pcategory3 ='" + comboBox2.Text + "',Psub3 ='" + comboBox3.Text + "',PNotes3 ='" + textBox3.Text + "',PJudge ='Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                            s6.ExecuteNonQuery();
                        }
                       
                    }
                    prepare();
                    button1.Text = "Add 4th comment";
                }
            }
            else if (status == 4)
            {
                string message = " Are You Sure To add forth comment ?";
                string caption = "Reminder";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(this, message, caption, buttons,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    this.Close();
                    //ClaimRevision cr = new ClaimRevision();
                    //cr.Show();
                }

                else
                {
                    // prepare();
                    if (Form1.TYPE == "puser")
                    {
                        OleDbCommand s2 = new OleDbCommand("Select CLAIM_NO, CLAIM_PAGE, BATCH_NO, DIAGNO, REJ_CODE, REJ_PRO, PJudge, PComment, SCAN_LOC,Doc_id From CLAIM_NO where BATCH_NO = '" + ClaimRevision.batch_no + "' ORDER BY CLAIM_NO ASC", con);
                    
                        OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                        OleDbDataReader s4 = s2.ExecuteReader();
                        if (s4.Read())
                        {
                            OleDbCommand s5 = new OleDbCommand("update CLAIM_NO set PComment4 ='" + textBox2.Text + "',Pcategory4 ='" + comboBox2.Text + "',Psub4 ='" + comboBox3.Text + "',PNotes4 ='" + textBox3.Text + "',PJudge = 'Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                            s5.ExecuteNonQuery();
                            OleDbCommand s6 = new OleDbCommand("update Report set PComment4 ='" + textBox2.Text + "',Pcategory4 ='" + comboBox2.Text + "',Psub4 ='" + comboBox3.Text + "',PNotes4 ='" + textBox3.Text + "',PJudge = 'Error' where BATCH_NO='" + ClaimRevision.batch_no + "' And CLAIM_NO ='" + ClaimRevision.claim_no + "'", con);
                            s6.ExecuteNonQuery();
                        }
                      
                    }
                    prepare();
                    button1.Text = "Finish";
                }
            }
            else
            {
                // MessageBox.Show("Maximum comments adding reached please contact system owner for this issue ");
                this.Close();
            }
        }
        private void prepare()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
        }


        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch { }
        }
        private void Col_adjust()
        {

            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[0].Width = 320;
        }

        private void PCol_adjust()
        {

            dataGridView1.Columns[1].Width = 120;

            dataGridView1.Columns[0].Width = 490;
        }
        private void Errcomment_Load(object sender, EventArgs e)
        {
            status = 0;
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = (@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann"); // making connection  
            con.Open();        
            try
            {
                if (Form1.TYPE == "User")
                {
                    OleDbCommand s2 = new OleDbCommand("select Comment ,Category From Comments_DR where Type = '" + Form1.TYPE + "'", con);
                    OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                    DataTable dt4 = new DataTable();
                    s3.Fill(dt4);
                    dataGridView1.DataSource = dt4;
                    PCol_adjust();
                }
                else
                {
                    OleDbCommand s2 = new OleDbCommand("select Comment ,Category,Subcategory From Comments_DR where Type = '" + Form1.TYPE + "'", con);
                    OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
                    DataTable dt4 = new DataTable();
                    s3.Fill(dt4);
                    dataGridView1.DataSource = dt4;
                    Col_adjust();
                }

            }
            catch { }
            con.Close();
            if (Form1.TYPE == "User")
            {

                removdup("Category", comboBox1);
                panel1.Visible = false;
                
            }
            else
            {
                removdup("Category", comboBox2);
                comboBox1.Visible = false;
                label3.Visible = false;
            }
        }
        private void removdup(string col, ComboBox c)
        {

            ArrayList P_list = new ArrayList();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    P_list.Add(row.Cells[col].Value.ToString());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }

            }

            ArrayList Re_Pro = new ArrayList();
            foreach (var x in P_list)
            {
                if (!Re_Pro.Contains(x))
                    Re_Pro.Add(x);
            }


            c.Items.Clear();


            foreach (string line in Re_Pro)
            {
                c.Items.Add(line);
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void comboBox3_MouseEnter(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();

            OleDbCommand cmdo = new OleDbCommand();
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            cmdo.Connection = con;
            cmdo.CommandText = "SELECT Subcategory FROM Subcategory where Category ='" + comboBox2.Text + "'And Type ='" + Form1.TYPE + "'";
            OleDbDataReader ldr = cmdo.ExecuteReader();

            if (ldr.HasRows)
            {
                while (ldr.Read())
                {
                    comboBox3.Items.Add(ldr["Subcategory"]);
                }

            }

            con.Close();
        }

    }
}
