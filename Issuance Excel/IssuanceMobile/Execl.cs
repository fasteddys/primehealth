using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;
using System.Data.SqlClient;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
namespace IssuanceMobile
{
    public partial class Execl : Form
    {
        public string connStr = @"Provider=SQLOLEDB;Data Source=(localdb)\MSSQLLocalDB;Initial  Catalog=PhNetwork;Integrated Security=True";
        public OleDbConnection con;
        public string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public static Microsoft.Office.Interop.Excel.Workbook MyBook = null;
        public static Microsoft.Office.Interop.Excel.Application MyApp = null;
        public static Microsoft.Office.Interop.Excel.Worksheet MySheet = null;
        private Thread pThread, sqlThread;
        private MethodInvoker progbarDelegate, progbarDelegateReset, runSQLDelegate;

        private string lockPBVar = "myPBLock";
        private string lockSQLVar = "mySQLock";
        public static string value1;
        public static int CountAllDatabasebeforeinsert;
        public static int Count_Excel;
        int CountDuplicatedMedicalId = 0;

        public Execl()
        {
            InitializeComponent();
            AssignButton.Enabled = false;
            CommandError.Visible = false;
            button1.Enabled = false;
        }
        private void openFileDialog1_FileOk_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            label2.Visible = true;
            label4.Visible = true;
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
                        CommandError.Text = "File Upload Is not Excel";
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
                                DataGridExcelSheet.DataSource = datatable1;
                                DataTable datatable2 = new DataTable();
                                cmd.CommandText = "SELECT  Medical_id,count(Medical_id) as Duplicated From [" + sheetName + "] Group by Medical_id Having count(*)>1";
                                cmd.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmd;
                                oda.Fill(datatable2);
                                con.Close();
                                DataGridMedicalIdDupicated.DataSource = datatable2;
                                int count2 = datatable2.Rows.Count;
                                label6.Text = count2.ToString() + " Row";
                                DataTable datatable3 = new DataTable();
                                cmd.CommandText = "  SELECT Count(Medical_id)  from[" + sheetName + "]  ";
                                cmd.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmd;
                                label2.Text = cmd.ExecuteScalar().ToString();
                                Count_Excel = Convert.ToInt32(label2.Text);
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
            //open ExelSheet From Upload Button
            openFileDialog1.ShowDialog();
            AssignButton.Enabled = true;
            label6.Visible = true;
            label5.Visible = true;
            label2.Visible = true;
            button1.Enabled = true;

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        //Assign Database From Excel To Sql Server By This Button
        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            UploadButton.Enabled = false;
            CountaAllData.Visible = true;
            button1.Enabled = false;
        }

        private void Execl_Load(object sender, EventArgs e)
        {
            progressBar1.Step = 1;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 60;

            progbarDelegate = new MethodInvoker(progressBar1.PerformStep);
            progbarDelegateReset = new MethodInvoker(reset);
            runSQLDelegate = new MethodInvoker(data);

            ThreadStart tsP = new ThreadStart(move);
            pThread = new Thread(tsP);

            ThreadStart tsSQL = new ThreadStart(check);
            sqlThread = new Thread(tsSQL);
        }
        private void data()//kkkkkkkkkkkkkkk
        {
            List<loginer> MedicalIDs = new List<loginer>();
            for (int i = 0; i < DataGridExcelSheet.RowCount; i++)
            {

                try
                {
                    SqlConnection sqlconnection = new SqlConnection("Data Source=MOBILE-SYSTEM\\MOBSYSTEMS;Initial Catalog=PhNetwork;Integrated Security=True");
                    sqlconnection.Open();

                    SqlCommand CommandDuplicated = new SqlCommand("SELECT  Medical_id,Password,Name,Email,gender,Type,ClientName,BranchName,Activated FROM Loginer WHERE Medical_id = '" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "'  ", sqlconnection);
                    CommandDuplicated.ExecuteNonQuery();
                    SqlDataAdapter sqladapter1 = new SqlDataAdapter(CommandDuplicated);
                    DataTable dat = new DataTable();
                    sqladapter1.Fill(dat);
                    foreach (DataRow dr in dat.Rows)
                    {

                        MedicalIDs.Add(new loginer
                        {

                            Medical_id = dr["Medical_id"].ToString(),
                            Password = dr["Password"].ToString(),
                            Name = dr["Name"].ToString(),
                            Email = dr["Email"].ToString(),
                            gender = dr["gender"].ToString(),
                            Type = dr["Type"].ToString(),
                            ClientName = dr["ClientName"].ToString(),
                            BranchName = dr["BranchName"].ToString(),
                            Activated = dr["Activated"].ToString(),


                        });
                        ExistsRows.Visible = true;
                        ExistsRows.Text = "Dublicated Data are " + MedicalIDs.Select(m => m.Medical_id).Distinct().Count() + " Rows";


                    }


                    DataGridInvalid.DataSource = dat;



                }
                catch
                {
                    CountaAllData.Visible = true;
                }


            }
            DataGridInvalid.DataSource = MedicalIDs;
        }
        private void move()
        {
            for (int h = 0; h < 8; h++)
            {
                Monitor.Enter(lockPBVar);

                for (int i = 0; i < 60; i++)
                {
                    if (progressBar1.InvokeRequired)
                    {
                        Invoke(progbarDelegate, null);
                    }
                    else
                    {
                        progressBar1.PerformStep();
                    }
                    Thread.Sleep(100);
                }

                if (progressBar1.InvokeRequired)
                {
                    Invoke(progbarDelegateReset, null);
                }
                else
                {
                    reset();
                }

                Monitor.Exit(lockPBVar);

            }
        }
        private void reset()
        {
            progressBar1.Value = progressBar1.Minimum;

        }
        private void check()
        {
            for (int i = 0; i < 18; i++)
            {
                Monitor.Enter(lockSQLVar);

                Invoke(runSQLDelegate);

                Monitor.Exit(lockSQLVar);

                Thread.Sleep(3000000);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ExistsRows.Visible = true;
            sqlThread.Start();


        }
        //her is code..............................................................................................................................
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable newRow = new DataTable();
            for (int i = 0; i < 10; i++)
            { newRow.Columns.Add(); }
            int  TottalDBRecords = 0;
            SqlConnection sqlconnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PhNetwork;Integrated Security=True");
            //  SqlConnection sqlconnection = new SqlConnection(@"Data Source=MOBILE-SYSTEM\\MOBSYSTEMS;Initial Catalog=PhNetwork;Integrated Security=True");

            sqlconnection.Open();
            SqlCommand GetCountCommand = new SqlCommand("select COUNT (*) from Loginer", sqlconnection);
            TottalDBRecords = (int)GetCountCommand.ExecuteScalar();
            Thread.Sleep(100);
            for (int i = 0; i < DataGridExcelSheet.RowCount; i++)
            {

                try
                {



                    SqlCommand GetCountDuplicateCommand = new SqlCommand("select COUNT (Distinct Medical_id) From Loginer  where  Medical_id = '" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "'  ", sqlconnection);
                    int DulicatedFound = (int)GetCountDuplicateCommand.ExecuteScalar();


                   // message is seen where there are exists data
                    if (DulicatedFound > 0)
                    {
                        CountDuplicatedMedicalId++;
                    }

                    //filter null values 
                    //if (DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() == "" || DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() == "")
                    //{
                    //    continue;
                    //}

                    //else
                    //{
                    //    // Insert Bulk Data To Database

                    //    DataGridExcelSheet.Rows[i].Cells[2].Value.ToString().Replace("'", "\'");
                    //    newRow.Rows.Add(DataGridExcelSheet.Rows[i].Cells[0].Value.ToString()
                    //        , DataGridExcelSheet.Rows[i].Cells[1].Value.ToString(),
                    //        DataGridExcelSheet.Rows[i].Cells[2].Value.ToString(), 
                    //        null,
                    //        DataGridExcelSheet.Rows[i].Cells[4].Value.ToString()
                    //        , DataGridExcelSheet.Rows[i].Cells[5].Value.ToString(),
                    //        DataGridExcelSheet.Rows[i].Cells[6].Value.ToString(),
                    //        DataGridExcelSheet.Rows[i].Cells[7].Value.ToString(), "Activated");
                     

                    //}



                }
                catch (Exception ex)
                {
                 
                }

            }
            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconnection))
                {
                    SqlCommand Getcount_before_insert =new SqlCommand("select COUNT (Medical_id) From Loginer ", sqlconnection);
                    int count_before = (int)Getcount_before_insert.ExecuteScalar();
                    bulkCopy.DestinationTableName ="[dbo].[Loginer]";
                    bulkCopy.WriteToServer(newRow);
                    SqlCommand deleteDoublecats = new SqlCommand("WITH cte AS (SELECT * ,row_number() OVER(PARTITION BY Medical_id ORDER BY id) AS[rn]FROM[dbo].[Loginer]) DELETE cte WHERE[rn] > 1", sqlconnection);
                    deleteDoublecats.ExecuteNonQuery();
                    //CountaAllData.Visible = true;
                    SqlCommand Getcount_after_insert = new SqlCommand("select COUNT (Medical_id) From Loginer ", sqlconnection);
                    int count_after = (int)Getcount_after_insert.ExecuteScalar();

                    if (count_after== count_before)
                    {
                        MessageBox.Show("data already exists");
                    }

                    else
                    {
                        MessageBox.Show(count_after-count_before+"  Rows have been Inserted");
                    }
                }
            }
            catch(Exception ex) { }

        }







        //her is code..............................................................................................................................

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //DataGridExcelSheet.DataSource = null;
        }       
    }
}
              
        

       
        

       
    
    

