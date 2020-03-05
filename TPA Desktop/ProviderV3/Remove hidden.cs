using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
namespace ProviderV2
{
    public partial class Remove_hidden : Form
    {
        string strFolder;
        string strFolder2;
        public Remove_hidden()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

            label3.Text = comboBox6.Items.Count.ToString();

        }

        private void button9_Click(object sender, EventArgs e)
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
                    xlWorkSheet.Range["A:R"].EntireColumn.Hidden = false;
                    int rCnt = 0;

                    xlApp.DisplayAlerts = false;
                    xlWorkBook.SaveAs(strFolder2 + "\\" + comboBox6.Items[q].ToString() + ".xlsx", 51); //' 51 == xlsx , 1 == xls
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                }
            }
            catch
            {

            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                folderBrowserDialog1.ShowDialog();
                strFolder2 = folderBrowserDialog1.SelectedPath;
                textBox2.Text = strFolder2;

            }
            catch
            {

            }
        }

        private void Remove_hidden_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {





            //1. Validate folder,
            //2. Instantiate excel object
            //3. Loop through the files
            //4. Add sheets
            //5. Save and enjoy!

            object missing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = false;

            //Create destination object
            Microsoft.Office.Interop.Excel.Workbook objBookDest = ExcelApp.Workbooks.Add(missing);

            string filePath = @"F:\Elsweedy\2014\New folder\أ.-د.-محمد-كمال-عسل1963.xlsx";
            string filePath1 = @"‪F:\Elsweedy\New folder (2)\New Microsoft Excel Worksheet.xlsx";
            //foreach (string filename in Directory.GetFiles(filePath))
            //{
                //if (File.Exists(filename))
                //{
            
                    Microsoft.Office.Interop.Excel.Application xlobj = new Microsoft.Office.Interop.Excel.Application();
                    Workbook w = default(Workbook);
                    Workbook w1 = default(Workbook);
                    Worksheet s = default(Worksheet);
                    Worksheet s1 = default(Worksheet);
                    try
                    {
                        w = xlobj.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        w1 = xlobj.Workbooks.Open(filePath1, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                        s = w.Worksheets.get_Item(1) as Worksheet;
                        s1 = w1.Worksheets.get_Item(1) as Worksheet;

                        // it will copy and paste sheet from one to another with formula
                        s.UsedRange.Copy(missing);
                        s1.Paste(missing, missing);

                        s1.UsedRange.Formula = s.UsedRange.Formula;

                        w.Save();
                        w1.Save();

                        w.Close(false, null, null);
                        w1.Close(false, null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());

                      

                    }
                    w.Save();

                    w1.Save();

                    w.Close(missing, missing, missing);

                    w1.Close(missing, missing, missing);
        }

    }
}