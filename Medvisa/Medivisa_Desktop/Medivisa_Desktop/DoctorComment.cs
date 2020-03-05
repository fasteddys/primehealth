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
    public partial class DoctorComment : Form
    {

        public DoctorComment()
        {
            InitializeComponent();
                         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            if (textBox1.Text == "")
            {
                label4.Visible = true;
                label4.Text = "Please insert comment";

            }
            else
            {
                OleDbCommand cmd = new OleDbCommand("insert into Comments_DR (Comment,Category,Subcategory,Type) values (N'" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + Form1.TYPE + "')", con);
                cmd.ExecuteNonQuery();
                label4.Visible = true;
                label4.Text = "Command is added Sucessfully";
               
            }
        }

        private void DoctorComment_Load(object sender, EventArgs e)
        {
            if (Form1.TYPE == "User")
            {
                label8.Hide();
                comboBox4.Hide();
                label3.Hide();
                comboBox2.Hide();
                tabPage4.Hide();
            }
            else
            {
                

            }
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s2 = new OleDbCommand("SELECT Category FROM CategoryTB where Type ='" + Form1.TYPE + "'", con);
            OleDbDataReader ldr = s2.ExecuteReader();
            if (ldr.HasRows)
            {
                while (ldr.Read())
                {
                    comboBox1.Items.Add(ldr["Category"]);
                }

            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s2 = new OleDbCommand("Select [id] ,[Comment] ,[Category],Subcategory FROM Comments_DR where Type = '" + Form1.TYPE + "'", con);
            OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
            DataTable s4 = new DataTable();
            s3.Fill(s4);
            dataGridView1.DataSource = s4;
            dataGridView1.Columns[1].Width = 600;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[0].Width = 60;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            label5.Text  =   dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

        }

        private void comboBox3_MouseEnter(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s2 = new OleDbCommand("SELECT Category FROM CategoryTB where Type ='" + Form1.TYPE + "'", con);
            OleDbDataReader ldr = s2.ExecuteReader();
            if (ldr.HasRows)
            {
                while (ldr.Read())
                {
                    comboBox3.Items.Add(ldr["Category"]);
                }

            }
        }

        private void comboBox4_MouseEnter(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s2 = new OleDbCommand("SELECT Subcategory FROM Subcategory where Type ='" + Form1.TYPE + "' and  Category ='" + comboBox3.Text + "'", con);
            OleDbDataReader ldr = s2.ExecuteReader();
            if (ldr.HasRows)
            {
                while (ldr.Read())
                {
                    comboBox4.Items.Add(ldr["Subcategory"]);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s5 = new OleDbCommand("update Comments_DR set Comment ='" + textBox2.Text + "', Category ='" + comboBox3.Text + "', Subcategory ='" + comboBox4.Text + "' where id ='" + label5.Text + "'", con);
            s5.ExecuteNonQuery();
            label10.Visible = true;
            label10.Text = "Upadte Sucessfully";
            OleDbCommand s2 = new OleDbCommand("Select [id] ,[Comment] ,[Category],Subcategory FROM Comments_DR where Type = '" + Form1.TYPE + "'", con);
            OleDbDataAdapter s3 = new OleDbDataAdapter(s2);
            DataTable s4 = new DataTable();
            s3.Fill(s4);
            dataGridView1.DataSource = s4;
            dataGridView1.Columns[1].Width = 600;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[0].Width = 60;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                label12.Visible = true;
                label12.Text = "Please Insert This Field";
            }
            else
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand s5 = new OleDbCommand("insert into CategoryTB (Category,Type) values (N'" + textBox3.Text + "','" + Form1.TYPE + "')", con);
                s5.ExecuteNonQuery();
                label12.Visible = true;
                label12.Text = "Add Category Sucessfully";
            }
        }

        private void comboBox5_MouseEnter(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
            con.Open();
            OleDbCommand s2 = new OleDbCommand("SELECT Category FROM CategoryTB where Type ='" + Form1.TYPE + "'", con);
            OleDbDataReader ldr = s2.ExecuteReader();
            if (ldr.HasRows)
            {
                while (ldr.Read())
                {
                    comboBox5.Items.Add(ldr["Category"]);
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                label15.Visible = true;
                label15.Text = "Please Insert This Field";
            }
            else
            {
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand s5 = new OleDbCommand("insert into Subcategory (Subcategory,Category,Type) values (N'" + textBox4.Text + "',N'" + comboBox5.Text + "','" + Form1.TYPE + "')", con);
                s5.ExecuteNonQuery();
                label15.Visible = true;
                label15.Text = "Add Subcategory Sucessfully";
            }
        }
    }
}
