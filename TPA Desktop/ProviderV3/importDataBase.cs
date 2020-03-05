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
using Excel = Microsoft.Office.Interop.Excel;
namespace ProviderV2
{
    public partial class importDataBase : Form
    {
        string strFolder;
      
        public importDataBase()
        {
            InitializeComponent();
        }

      
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            try
            {
                for (int q = 0; q <= comboBox6.Items.Count; q++)
                {
                    string ex_file = strFolder + "\\" + comboBox6.Items[q].ToString() + ".xlsx";
                   Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    Excel.Range range;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(ex_file, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    // xlWorkSheet.Range["A:R"].EntireColumn.Hidden = false;
                    int rCnt = 0;
                    range = xlWorkSheet.UsedRange;
                    rCnt = range.Rows.Count - 4;
                    xlApp.DisplayAlerts = false;
                    //xlWorkBook.SaveAs(strFolder2 + "\\" + comboBox6.Items[q].ToString() + ".xlsx", 51); //' 51 == xlsx , 1 == xls
                    //xlWorkBook.Close(true, misValue, misValue);
                    //xlApp.Quit();

                    //releaseObject(xlWorkSheet);
                    //releaseObject(xlWorkBook);
                    //releaseObject(xlApp);
                    string connectionString = @"Data Source=AndroidDB;Initial Catalog=TPASys;Integrated Security=True";
                    string path = @"\\ccm\Attached\Tallaat\20150101";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string stament = ("insert into [TPASys].[dbo].[Provider] (PName,TottalClaims,AttachPath,PMonth,PYear,ClientName) values(N'" + comboBox6.Items[q].ToString() + "','" + rCnt + "','" + path + "','" + "01" + "','" + "2015" + "','" + "Talaat Mostafa" + "')");
                        SqlCommand cmd = new SqlCommand(stament);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        connection.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {

                        }

                        connection.Close();
                    }

                }
            }
            catch
            {


            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {

                folderBrowserDialog1.ShowDialog();
                strFolder = folderBrowserDialog1.SelectedPath;
                textBox1.Text = strFolder;
                foreach (string file in System.IO.Directory.GetFiles(strFolder))
                    comboBox6.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
            }
            catch
            {

            }
        }
    }
}
