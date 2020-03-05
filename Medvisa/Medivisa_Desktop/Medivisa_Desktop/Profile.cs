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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                OleDbConnection con = new OleDbConnection(@"Provider=SQLOLEDB;Data Source=10.101.1.12;User ID=sa;Password=123456;Initial Catalog=Egypt_Scann");
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Update User_Login set pass='" + textBox1.Text + "' where Name='" + Form1.NAME + "' and id='" + Form1.ID + "'", con);
                //OleDbDataReader dr = cmd.ExecuteReader();  
                count = (int)(cmd.ExecuteScalar());
                if (count == 1)
                {
                    label2.Visible = true;
                    label2.Text = "Updating Password Sucessfully";
                }
                else
                {
                    label2.Visible = true;
                    label2.Text = "Updateing Password is Failed ";
                }
               
            }
            catch { }
            label2.Visible = true;
           label2.Text = "Updating Password Sucessfully";
            
        }
    }
}
