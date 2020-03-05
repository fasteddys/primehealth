using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;
using Medinisa_TPS.DB;
using Medinisa_TPS;

namespace MediVisa_TPS
{
    public partial class MediVisa : Form
    {
    static  public  bool flagefinsh=false;
        List<string> batch_NO = new List<string>();

SqlConnection con = new SqlConnection(@"Data Source=mohamed-pc\MOHAMEDABDELALEM;Initial Catalog=Egypt_scann; Integrated Security=true");

        //SqlConnection con = new SqlConnection(@"Data Source=10.101.1.12;User ID=sa;Initial Catalog=Egypt_Scann;Integrated Security=true");

        public MediVisa()
        {
            InitializeComponent();

        }
        private void Process_Click(object sender, EventArgs e)
        {

            Process.Enabled = false;
          
            if (checkBox1.Checked == true)
            {

                BackupDataBase();

            }
            DialogResult dialogResult = MessageBox.Show(
               "Batch Number: " + countDVD_NO().ToString() + ",Claims Number: " + CountClaims().ToString()
               , "message"
                , MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                this.Enabled = false;


                try
                {
                    DeleteClaimsByBatchNoANDBatchByDVDNo();


                    MessageBox.Show("Done..........");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Not excute");

                }




            }
            else if (dialogResult == DialogResult.No)
            {

                MessageBox.Show("Canceled");


            }
            Process.Enabled = true;


        }
        public int countDVD_NO()
        {
            int count=0;
            
                if (DVDTXT.Text != "")
                {
                    string commend =
                        "SELECT count(DVD_NO) as DVD_NO from dbo.BATCHER where DVD_NO = " +
                         DVDTXT.Text;
                    string commendbatch_NO =
               "select [BATCH_NO]  from dbo.BATCHER where DVD_NO = " + DVDTXT.Text;
                    SqlCommand com = new SqlCommand(commend, con);
                    con.Open();
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = Convert.ToInt16(reader[0]);
                        }
                    }
                    con.Close();
                    SqlCommand combatchno = new SqlCommand(commendbatch_NO, con);

                    con.Open();
                    using (SqlDataReader reader = combatchno.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            batch_NO.Add( reader["BATCH_NO"].ToString());
                            
                        }
                    }
                    con.Close();



                    return count;
                }
                else
                { return 0; }
            
        }
        public int CountClaims()
        {

            Int32 count = 0;
                if (DVDTXT.Text != "")
                {
                    string batch_NOString = " ";
                    for (int i = 0; i < batch_NO.Count; i++)
                    {
                        if (i == 0)
                        {
                            batch_NOString ="'" + batch_NO[i] + "'";

                        }
                        else
                        {
                            batch_NOString =  batch_NOString + ",'" + batch_NO[i] + "'";
                        }

                    }



                string commend =

                          " select count( [BATCH_NO]) as count from dbo.CLAIM_NO where BATCH_NO in (" + batch_NOString+")";
                    SqlCommand com = new SqlCommand(commend, con);
                    con.Open();
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string x = reader["count"].ToString();

                        count =Convert.ToInt32( reader["count"].ToString());

                    }
                }
                con.Close();
                return count;

                }
                else { return 0; }
            

        }
        private void DVDTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
          
        }
        public void DeleteClaimsByBatchNoANDBatchByDVDNo()

        {

            string batch_NOString = "";
            for (int i = 0; i < batch_NO.Count; i++)
            {
                if (i == 0)
                {
                    batch_NOString = "'" + batch_NO[i] + "'";

                }
                else
                {
                    batch_NOString = batch_NOString + ",'" + batch_NO[i] + "'";
                }

            }



            string commend =
          "BEGIN TRANSACTION [Tran1] BEGIN TRY Delete from dbo.BATCHER where DVD_NO = "+
           DVDTXT .Text+ 
          " Delete from dbo.CLAIM_NO where BATCH_NO in ("+
          batch_NOString 
          + ") COMMIT TRANSACTION[Tran1] END TRY BEGIN CATCH ROLLBACK TRANSACTION[Tran1] END CATCH ";

            SqlCommand com = new SqlCommand(commend, con);
            com.CommandTimeout = 0;

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
             flagefinsh = true;
         

        }
   
        private void DVDTXT_TextChanged(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }
        public void BackupDataBase()
        {
            string database = con.Database.ToString();
            string date = DateTime.Now.ToString("ss-mm-HH-dd-MM-yyyy");
            string location = @"'C:\Program Files\Microsoft SQL Server\MSSQL12.ALSOBKY\MSSQL\Backup\" + date + ".bak'";
            string query = "BACKUP DATABASE MedivisaTps TO DISK ="
                + location;
            SqlCommand cmd = new SqlCommand(
                query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void MediVisa_Load(object sender, EventArgs e)
        {

            
   
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}