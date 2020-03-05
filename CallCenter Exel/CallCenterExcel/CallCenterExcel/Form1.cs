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
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Management;
using System.IO;
using WMPLib;
namespace CallCenterExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        DataClasses1DataContext dp = new DataClasses1DataContext();

       

        private void upload_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();


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
                        MessageBox.Show("File Upload Is not Excel");
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

        private void process_btn_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();

        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlConnection sqlconnection = new SqlConnection(@"Data Source=10.1.1.29\MobSystems;Initial Catalog=PhNetwork;Integrated Security=True");
                sqlconnection.Open();
                if (radioButton1.Checked == true)
                {
                    for (int i = 0; i < DataGridExcelSheet.RowCount; i++)
                    {
                        if (DataGridExcelSheet.Rows[i].Cells[21].Value.ToString() == "اضافه")
                        {
                            SqlCommand s2 = new SqlCommand("insert into DoctorTB values(N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() 
                                + "',N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[16].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[17].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[18].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[19].Value.ToString() + "')", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[21].Value.ToString() == "تعديل")
                        {
                            SqlCommand s2 = new SqlCommand("Update  DoctorTB set DoctorName =N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "', DoctorAddressCode= N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "', DoctorAddress = N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "', Doc_cat = N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "', Category = N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "', LocID =  N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "', SubLocationName = N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "', DoctorInfo = N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "', DoctorPhone = N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "', DoctorPhone2 = N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "', DoctorPhone3 = N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "', DoctorPhone4 = N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "', DoctorNotes = N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "', DocLong = N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "', DocLat = N'" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "', AccName = N'" + DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() + "', QNB = N'" + DataGridExcelSheet.Rows[i].Cells[16].Value.ToString() + "', Meduim = N'" + DataGridExcelSheet.Rows[i].Cells[17].Value.ToString() + "', Large =  N'" + DataGridExcelSheet.Rows[i].Cells[18].Value.ToString() + "', Discount = N'" + DataGridExcelSheet.Rows[i].Cells[19].Value.ToString() + "' where ID = '" + DataGridExcelSheet.Rows[i].Cells[20].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[21].Value.ToString() == "حذف")
                        {
                            SqlCommand s2 = new SqlCommand("Delete from  DoctorTB  where ID = '" + DataGridExcelSheet.Rows[i].Cells[20].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    for (int i = 0; i < DataGridExcelSheet.RowCount; i++)
                    {
                        if (DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() == "اضافه")
                        {
                            SqlCommand s2 = new SqlCommand("insert into LaboratoryTB values(N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "')", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() == "تعديل")
                        {
                            SqlCommand s2 = new SqlCommand("Update  LaboratoryTB set LabName=N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "', LabAddressCode= N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "', LabAddress = N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "', Category = N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "', LocID = N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "', LabSublocationName =  N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "', LabPhone = N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "', Labphone2 = N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "', Labphone3 = N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "', Labphone4 = N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "', LabNotes = N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "', LabLong = N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "', LabLat = N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "', Discount = N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "' where ID = '" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() == "حذف")
                        {
                            SqlCommand s2 = new SqlCommand("Delete from  LaboratoryTB  where ID = '" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else if (radioButton3.Checked == true)
                {
                    for (int i = 0; i < DataGridExcelSheet.RowCount; i++)
                    {
                        if (DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() == "اضافه")
                        {
                            SqlCommand s2 = new SqlCommand("insert into OpticalTB values(N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "')", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() == "تعديل")
                        {
                            SqlCommand s2 = new SqlCommand("Update  OpticalTB set OpticalName=N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "', OpticalAddressCode= N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "', OpticalAddress = N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "', Category = N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "', LocID = N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "', OpticalSublocationName =  N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "', OpticalPhone = N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "', Opticalphone2 = N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "', Opticalphone3 = N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "', Opticalphone4 = N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "', OpticalNotes = N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "', OpticalLong = N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "', OpticalLat = N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "', Discount = N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "' where ID = N'" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() == "حذف")
                        {
                            SqlCommand s2 = new SqlCommand("Delete from  OpticalTB  where ID = '" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else if (radioButton4.Checked == true)
                {
                    for (int i = 0; i < DataGridExcelSheet.RowCount; i++)
                    {
                        if (DataGridExcelSheet.Rows[i].Cells[21].Value.ToString() == "اضافه")
                        {
                            SqlCommand s2 = new SqlCommand("insert into HospitalTB values(N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[16].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[17].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[18].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[19].Value.ToString() + "')", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[21].Value.ToString() == "تعديل")
                        {
                            SqlCommand s2 = new SqlCommand("Update  HospitalTB set HospName=N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "', AddressNum= N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "', HospAddress = N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "', Category = N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "', LocID = N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "', SubLocationName =  N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "', HospPhone = N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "', HospPhone2 = N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "', HospPhone3 = N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "', HospPhone4 = N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "', HospNotes = N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "', HospLong = N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "', HospLat = N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "', QNB = N'" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "', Meduim = N'" + DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() + "', Large = N'" + DataGridExcelSheet.Rows[i].Cells[15].Value.ToString() + "', Discount = N'" + DataGridExcelSheet.Rows[i].Cells[16].Value.ToString() + "', HospitalNetwork = N'" + DataGridExcelSheet.Rows[i].Cells[17].Value.ToString() + "', AddByEmp =  N'" + DataGridExcelSheet.Rows[i].Cells[18].Value.ToString() + "', AddedTime = N'" + DataGridExcelSheet.Rows[i].Cells[19].Value.ToString() + "' where ID = '" + DataGridExcelSheet.Rows[i].Cells[20].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[21].Value.ToString() == "حذف")
                        {
                            SqlCommand s2 = new SqlCommand("Delete from  HospitalTB  where ID = '" + DataGridExcelSheet.Rows[i].Cells[20].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else if (radioButton5.Checked == true)
                {
                    for (int i = 0; i < DataGridExcelSheet.RowCount; i++)
                    {
                        if (DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() == "اضافه")
                        {
                            SqlCommand s2 = new SqlCommand("insert into PharmacyTB values(N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "',N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "')", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() == "تعديل")
                        {
                            SqlCommand s2 = new SqlCommand("Update  PharmacyTB set PharmName=N'" + DataGridExcelSheet.Rows[i].Cells[0].Value.ToString() + "', PharmAddressCode= N'" + DataGridExcelSheet.Rows[i].Cells[1].Value.ToString() + "', PharmAddress = N'" + DataGridExcelSheet.Rows[i].Cells[2].Value.ToString() + "', LocID = N'" + DataGridExcelSheet.Rows[i].Cells[3].Value.ToString() + "', PharmSublocationName = N'" + DataGridExcelSheet.Rows[i].Cells[4].Value.ToString() + "', PharmPhone =  N'" + DataGridExcelSheet.Rows[i].Cells[5].Value.ToString() + "', PharmPhone1 = N'" + DataGridExcelSheet.Rows[i].Cells[6].Value.ToString() + "', PharmPhone2 = N'" + DataGridExcelSheet.Rows[i].Cells[7].Value.ToString() + "', PharmPhone3 = N'" + DataGridExcelSheet.Rows[i].Cells[8].Value.ToString() + "', PharmNotes = N'" + DataGridExcelSheet.Rows[i].Cells[9].Value.ToString() + "', PharmLong = N'" + DataGridExcelSheet.Rows[i].Cells[10].Value.ToString() + "', PharmLat = N'" + DataGridExcelSheet.Rows[i].Cells[11].Value.ToString() + "', Discount = N'" + DataGridExcelSheet.Rows[i].Cells[12].Value.ToString() + "' where ID = '" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else if (DataGridExcelSheet.Rows[i].Cells[14].Value.ToString() == "حذف")
                        {
                            SqlCommand s2 = new SqlCommand("Delete from  PharmacyTB  where ID = '" + DataGridExcelSheet.Rows[i].Cells[13].Value.ToString() + "'", sqlconnection);
                            s2.ExecuteNonQuery();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch(NullReferenceException )
            {
                
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Operation is finished");
        }
    }
}
