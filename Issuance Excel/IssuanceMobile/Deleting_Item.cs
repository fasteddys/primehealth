using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Threading;
namespace IssuanceMobile
{
    public partial class Deleting_Item : Form
    {
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private static Microsoft.Office.Interop.Excel.Workbook MyBook = null;
        private static Microsoft.Office.Interop.Excel.Application MyApp = null;
        private static Microsoft.Office.Interop.Excel.Worksheet MySheet = null;
       
        public Deleting_Item()
        {
            InitializeComponent();
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string filePath = openFileDialog1.FileName;
                string extension = Path.GetExtension(filePath);
                string header = "YES";
                string conStr, sheetName;
                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath, header);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath, header);
                        break;
                    default:
                        label3.Text = "File Upload Is not Excel";
                        break;
                }
                //Files has uploaded Must be Excel file and refuse others
                if (extension.Trim() == ".xlsx" || extension.Trim() == ".xls")
                {
                    //Get the name of the First Sheet.
                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            cmd.Connection = con;
                            con.Open();
                            DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            con.Close();
                        }
                    }

                    //Read Data from the First Sheet.
                    using (OleDbConnection con = new OleDbConnection(conStr))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {

                                // Translate All Data From Exel Sheet To Data Table
                                DataTable datatable1 = new DataTable();
                                cmd.CommandText = "SELECT  * From [" + sheetName + "]";
                                cmd.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmd;
                                oda.Fill(datatable1);
                                con.Close();
                                dataGridDeleted.DataSource = datatable1;
                                DataTable datatable3 = new DataTable();
                                cmd.CommandText = "  SELECT Count(Medical_id)  from[" + sheetName + "]  ";
                                cmd.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmd;
                                label6.Text = cmd.ExecuteScalar().ToString();
                                con.Close();

                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Excel File");
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            label4.Visible = true;
            //delete Record by Medical_id Field
            try
            {
               
                if (Medical_id.Text == "")
                {
                    deleteLabel.Visible = true;
                    deleteLabel.Text = "Plaese Fill Field";
                }

                else
                {
                    deleteLabel.Visible = true;
                    SqlConnection sqlcon = new SqlConnection("Data Source=MOBILE-SYSTEM\\MOBSYSTEMS;Initial Catalog=PhNetwork;Integrated Security=True");
                    SqlCommand DeleteCommand = new SqlCommand("delete from Loginer where Medical_id='" + Medical_id.Text + "'", sqlcon);
                    sqlcon.Open();
                    DeleteCommand.ExecuteNonQuery();
                    SqlCommand sqlcmmd = new SqlCommand("select COUNT (*) from Loginer", sqlcon);
                    int alluserscount = (int)sqlcmmd.ExecuteScalar();
                    sqlcon.Close();
                    deleteLabel.Text = "DataBase Has  '" + alluserscount + "' Clients";
                    Medical_id.Clear();
                }
            }
            catch (Exception ex)
            {
                deleteLabel.Visible = true;
                deleteLabel.Text = "You Are insert Error Item";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Delete excel sheet from database sql
            openFileDialog1.ShowDialog();
            UploadButton.Enabled = false;
            label5.Visible = true;
            label6.Visible = true;
           
        }
        private void Deleting_Item_Load(object sender, EventArgs e)
        {
           
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            label3.Visible = true;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int CountSkipedMedicalId = 0,notfoundrecords=0 ,CountDeletedvalues = 0, TottalDBRecords = 0;
            SqlConnection sqlconnection = new SqlConnection("Data Source=MOBILE-SYSTEM\\MOBSYSTEMS;Initial Catalog=PhNetwork;Integrated Security=True");
            sqlconnection.Open();
            SqlCommand GetCountCommand = new SqlCommand("select COUNT (*) from Loginer", sqlconnection);
            TottalDBRecords = (int)GetCountCommand.ExecuteScalar();

            for (int i = 0; i < dataGridDeleted.RowCount; i++)
            {

                try
                {

                    Thread.Sleep(100);
                    SqlCommand GetCountDuplicateCommand = new SqlCommand("select COUNT (Distinct Medical_id) From Loginer  where  Medical_id = '" + dataGridDeleted.Rows[i].Cells[0].Value.ToString() + "'  ", sqlconnection);
                    int DulicatedFound = (int)GetCountDuplicateCommand.ExecuteScalar();
                    //message is seen where there are exists data
                    
                   if (DulicatedFound > 0)
                    {
                        SqlCommand DeleteBulkData = new SqlCommand("delete from Loginer where Medical_id='" + dataGridDeleted.Rows[i].Cells[0].Value.ToString() + "'", sqlconnection);
                        DeleteBulkData.ExecuteNonQuery();
                        CountDeletedvalues++;
                        TottalDBRecords--;
                        dataGridDeleted.Invalidate(true);
                        dataGridDeleted.Update();
                    }
                   else 
                   {
                       notfoundrecords++;
                   }

                    label3.Visible = true;
                    label3.Text = " You Delete " + CountDeletedvalues + " Rows " + " Skiped Rows = " + notfoundrecords + "       Database has " + TottalDBRecords + " Clients Now";

                }
                // If User insert Fake File Or Conflict Data has been accourded 
                catch (Exception ex)
                {
                }
            }

            MessageBox.Show("Delete Operation has completed");    
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

       

       
      
    }
}
