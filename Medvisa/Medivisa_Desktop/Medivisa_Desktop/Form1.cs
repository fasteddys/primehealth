using System;
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
    public partial class Form1 : Form
    {
        public static string TYPE;
        public static string NAME;
        public static string ID;
        public static string PASSWORD;
        string _date = DateTime.Today.ToString("yyyy-MM-dd h:mm tt");
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM User_Login WHERE Name='" + textBox1.Text + "' AND Pass='" + textBox2.Text + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                OleDbCommand cmd2 = new OleDbCommand("SELECT Count(*) FROM User_Login WHERE Name='" + textBox1.Text + "' AND Pass='" + textBox2.Text + "'", con);
                int count_login = (int)(cmd2.ExecuteScalar());
                if (count_login == 1)
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if ((dr["Name"].ToString() == textBox1.Text) &&
                                (dr["Pass"].ToString() == textBox2.Text))
                            {

                                TYPE = dr["Type"].ToString();
                                NAME = dr["Name"].ToString();
                                ID = dr["id"].ToString();
                                PASSWORD = dr["Pass"].ToString();                               
                                MainOptionscs main = new MainOptionscs();
                                main.Show();
                                this.Hide();
                            }
                        }
                    }

                    dr.Close();
                    con.Close();
                }
                    
                else if(textBox1.Text=="")
                {
                    if(textBox1.Text!=null)
                    {
                        label5.Visible = false;
                    }
                    label5.Visible = true;
                    dr.Close();
                }
                else if (textBox2.Text == "")
                {
                    if (textBox2.Text != null)
                    {
                        label6.Visible = false;
                    }
                    label6.Visible = true;
                    dr.Close();
                }
                else
                {
                    label7.Visible = true;
                    dr.Close();
                }

               


            }
            catch (Exception ex)
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand s2 = new OleDbCommand("insert into  Logger values('" + Form1.NAME + "' ,'" + _date + "','" + ex + "')", con);
                s2.ExecuteNonQuery();
                //MessageBox.Show(ex.Message);
            }  
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            {
                label5.Visible = false;
            }
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                label6.Visible = false;
            }
        }
    }
}
