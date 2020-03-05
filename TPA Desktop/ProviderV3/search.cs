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

namespace ProviderV2
{
    public partial class search : Form
    {
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        DataView dsview = new DataView();
        public search()
        {
            InitializeComponent();
        }



        private void search_Load(object sender, EventArgs e)
        {
            selection();
            //try
            //{
            //    string connectionString = "Data Source=CCM\\SQLEXPRESS;Initial Catalog=TPASys;Integrated Security=True";
            //    string sql = "select [id], [ClientName], [rAttach] From [TPASys].[dbo].[requestTB]";
            //    SqlConnection connection = new SqlConnection(connectionString);
            //    SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
            //    DataSet ds = new DataSet();
            //    connection.Open();
            //    dataadapter.Fill(ds, "[TPASys].[dbo].[requestTB]");
            //    connection.Close();
            //    dataGridView1.DataSource = ds.Tables["[TPASys].[dbo].[requestTB]"];
            //}
            //catch
            //{

            //}
            //   SqlConnection con;
            //   SqlDataAdapter adapter;
            //   DataSet ds;
            //   SqlCommand cmd;
            //   string connectionString = "Data Source=Androiddb;Initial Catalog=Archiving;Integrated Security=True";
            //   string sql = "SELECT * FROM [requestTB]";
            //   con = new SqlConnection(connectionString);
            //   cmd = new SqlCommand(sql, con);
            //   con.Open();
            //   adapter = new SqlDataAdapter(cmd);
            //   ds = new DataSet();
            //   adapter.Fill(ds, "[requestTB]");
            //   dataGridView1.DataSource = ds.Tables[0];
            ////   dataGridView1.DataBind();
            //   con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //    string connectionString = (@"Data Source=CCM\SQLEXPRESS;Initial Catalog=Archiving;Integrated Security=True");
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        string stament = ("select [id], [ClientName], [rAttach] From [Archiving].[dbo].[Provider] where [id] ='" +textBox1.Text + "')'");
            //        SqlCommand cmd = new SqlCommand(stament);
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = connection;
            //        connection.Open();
            //        try
            //        {
            //            cmd.ExecuteNonQuery();
            //        }
            //        catch 
            //        {

            //        }

            //        connection.Close();
            //    }
            //}

            //     dataGridView1.DataMember = "requestTB";




            //        Dim connectionString As String = "Data Source=CCM\sqlexpress;Initial Catalog=Provider;Integrated Security=True"
            //Dim myConnection As SqlConnection
            //Dim myCommand As SqlCommand
            //Dim myAdapter As SqlDataAdapter

            //myConnection = New SqlConnection(connectionString)
            //myCommand = New SqlCommand(selectCmd, myConnection)
            //myConnection.Open()
            //myDataSet = New DataSet()
            //myAdapter = New SqlDataAdapter()
            //myAdapter.SelectCommand = myCommand
            //myAdapter.Fill(myDataSet, table)

            //If (myDataSet.Tables(table).Rows.Count = 0) Then
            //    MessageBox.Show("Sorry , there are no data to load")
            //Else
            //    d1.DataSource = myDataSet.Tables(table)
            //End If
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            selection();
            //try
            //{
            //    string connectionString = "Data Source=CCM\\SQLEXPRESS;Initial Catalog=Archiving;Integrated Security=True";
            //    string sql = "select [id], [ClientName], [rAttach] From [Archiving].[dbo].[requestTB]";
            //    SqlConnection connection = new SqlConnection(connectionString);
            //    SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
            //    DataSet ds = new DataSet();
            //    connection.Open();
            //    dataadapter.Fill(ds, "[Archiving].[dbo].[requestTB]");
            //    connection.Close();
            //    dataGridView1.DataSource = ds.Tables["[Archiving].[dbo].[requestTB]"];
            //}
            //catch
            //{

            //}
            //       bs.DataSource = dsview;s
            //       bs.Filter = "id like '" + textBox1.Text + "%'";
            //dataGridView1.DataSource = bs;
            try
            {
                ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("id = '" + textBox1.Text + "'");
            }
            catch
            {

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=Androiddb;Initial Catalog=TPASys;Integrated Security=True";
            //string connectionString = (@"Data Source=CCM\SQLEXPRESS;Initial Catalog=Archiving;Integrated Security=True");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string stament = ("update requestTB set rAttach =N'" + textBox2.Text + "'where id = '" + textBox1.Text + "' ");
                SqlCommand cmd = new SqlCommand(stament);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();
                //try
                //{
                cmd.ExecuteNonQuery();
                //}
                //catch
                //{

                //}

                connection.Close();
            }
            selection();
        }
        private void selection()
        {
            try
            {
                string connectionString = "Data Source=Androiddb;Initial Catalog=TPASys;Integrated Security=True";
                string sql = "select [id], [ClientName], [rAttach] From [TPASys].[dbo].[requestTB] where id = '" + textBox1.Text + "' ";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                connection.Open();
                dataadapter.Fill(ds, "[TPASys].[dbo].[requestTB]");
                connection.Close();
                dataGridView1.DataSource = ds.Tables["[TPASys].[dbo].[requestTB]"];
            }
            catch
            {


            }
        }
    }
}
