using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IssuanceMobile
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //search Data By Medical Id
            try
            {
                if (Medical_Input.Text == "")
                {
                    label3.Visible = true;
                    label3.Text = "Please Insert Medical Id";
                    dataGridView1.Visible = false;
                }
                else
                {
                    dataGridView1.Visible = true;
                    SqlConnection sqlconnection = new SqlConnection(@"Data Source=10.1.1.39;Initial Catalog=PhNetwork;Integrated Security=False;User Id=sa;Password=P@ssw0rd;");
                    sqlconnection.Open();
                    SqlCommand searchcommand = new SqlCommand("SELECT * FROM Loginer WHERE Medical_id = '" + Medical_Input.Text.Replace(" ","") + "'", sqlconnection);
                    searchcommand.ExecuteNonQuery();
                    SqlDataAdapter s3 = new SqlDataAdapter(searchcommand);
                    DataTable r4 = new DataTable();
                    s3.Fill(r4);
                    dataGridView1.DataSource = r4;
                }
            }

               catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
