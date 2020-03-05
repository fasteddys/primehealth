using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProviderV2
{
    public partial class Assign : Form
    {
        DataSet dts = new DataSet();
        BindingSource bs = new BindingSource();
        DataView dsview = new DataView();
        public Assign()
        {
            InitializeComponent();
        }

        private void Assign_Load(object sender, EventArgs e)
        {
      
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=ABDELRAHMAN-ALI\\MYSQLSERV;Initial Catalog=Archiving;Integrated Security=True";
                //string sql = "select [id], [ClientName], [rBody] ,[rFrom] ,[rAttach]  ,From [Archiving].[dbo].[Provider]";
                string sql = "select [PId], [PName], [PMonth] ,[PYear] ,[TottalClaims] ,[AttachPath],[Assigned],[ClientName] From [Archiving].[dbo].[Provider] where [ClientName] = '" + comboBox6.Text + "' And [PMonth]= '" + ComboBox4.Text + "' And  [PYear]= '" + ComboBox3.Text + "'  ";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                connection.Open();
                dataadapter.Fill(ds, "[Archiving].[dbo].[Provider]");
                connection.Close();
                dataGridView1.DataSource = ds.Tables["[Archiving].[dbo].[Provider]"];
            }
            catch
            {

               
            }
            removdup("PName", ComboBox1);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            label3.Hide();
            label4.Hide();
            int si = ComboBox1.SelectedIndex;
          //  DataView dv;
          //  dv = new DataView(dts.Tables[0], "PName = '" + textBox7 .Text  + "'", "type Desc", DataViewRowState.CurrentRows);
           // dataGridView1.DataSource = dv;
           // dsview = dts.Tables[0].DefaultView;
         //   bs.DataSource = dsview;
         //   bs.Filter = "PName = '" + ComboBox1.Items[si].ToString() + "'";
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("PName LIKE '*{0}*'", ComboBox1.Text);
           // dataGridView1.DataSource = bs;
            if (dataGridView1 .RowCount > 2 )
            {

                label3.Show();
                label4.Show();

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

            int index = ComboBox1.FindString(textBox7.Text);
            //  int index = ComboBox1.valueMembers.Contains(textBox7.Text);
            ComboBox1.SelectedIndex = index;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
