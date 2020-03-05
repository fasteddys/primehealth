using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;
using System.Data.SqlClient;

namespace IssuanceMobile
{
    public partial class Add_manual : Form
    {
        public Add_manual()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int CountDuplicatedMedicalId = 0;
                SqlConnection sqlconnection = new SqlConnection(@"Data Source=10.1.1.39;Initial Catalog=PhNetwork;Integrated Security=False;User Id=sa;Password=P@ssw0rd;");
                sqlconnection.Open();
                SqlCommand GetCountDuplicateCommand = new SqlCommand("select COUNT (*) From Loginer  where  Medical_id = '" + textBox1.Text + "'  ", sqlconnection);
                int DulicatedFound = (int)GetCountDuplicateCommand.ExecuteScalar();
                SqlCommand DuplicateMedical = new SqlCommand("select Medical_id From Loginer  where  Medical_id = '" + textBox1.Text + "'  ", sqlconnection);
                string DuplicateMedicalrow = (string)DuplicateMedical.ExecuteScalar();

                //message is seen where there are exists data
                if (DulicatedFound > 0)
                {//doublicate
                    CountDuplicatedMedicalId++;
                    label9.Visible = true;
                    label9.Text = "   Duplicated Medical ID : " + DuplicateMedicalrow + " ... Number Duplicated are : " + CountDuplicatedMedicalId + "";

                }
                else if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
                {
                    label9.Visible = true;
                    label9.Text = "Please Complete Data";
                }
                else
                {//insert is her
                    SqlConnection sqlcon = new SqlConnection(@"Data Source=10.1.1.39;Initial Catalog=PhNetwork;Integrated Security=False;User Id=sa;Password=P@ssw0rd;");
                    SqlCommand sqlcmd = new SqlCommand("insert into Loginer values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + null + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','Activated','"+DateTime.Now.ToString()+"')", sqlcon);
                    sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                    SqlCommand sqlcmmd = new SqlCommand("select COUNT (*) from Loginer", sqlcon);
                    int alluserscount = (int)sqlcmmd.ExecuteScalar();
                    label9.Visible = true;
                    label9.Text = "Member has added Successfully .. " + " Database has " + alluserscount + " Clients";
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
    }
}
