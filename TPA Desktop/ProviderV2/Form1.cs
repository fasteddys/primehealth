using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace ProviderV2
{

    public partial class Form1 : Form
    {
        public bool c1 = true;
        public bool c2 = true;
        public bool c3 = true;
        DataSet dts = new DataSet();
        BindingSource bs = new BindingSource();
        DataView dsview = new DataView();
        Regex regex = new Regex(@"[\\\/:\*\?""'<>|]");
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tabControl1.TabPages.Remove(tabPage1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openfile1 = new OpenFileDialog();
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.textBox1.Text = openfile1.FileName;
                }
                {
                    string pathconn = "Provider = Microsoft.ACE.OLEDB.12.0; Data source=" + textBox1.Text + ";Extended Properties=\"Excel 12.0;HDR= yes;\";";
                    OleDbConnection conn = new OleDbConnection(pathconn);
                    OleDbDataAdapter MyDataAdapter = new OleDbDataAdapter("Select * from [Sheet1$]", conn);

                    MyDataAdapter.Fill(dts, "[Sheet1$]");
                    dataGridView1.DataSource = dts;
                    dataGridView1.DataMember = "[Sheet1$]";
                    conn.Close();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

            }

            label4.Text = "Please Wait While Providers Loading ...";

            if (comboBox6.Text == "Talaat Mostafa")
            {
                removdup("F8", ComboBox1);
            }
            else 
            {

                removdup("F8", comboBox2);

            }



            label4.Text = "you have " + ComboBox1.Items.Count + "provider";
        }



        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == ">")
            {
                this.Width = 1386;
             
                button6.Text = "<";
            }
            else
            {
                this.Width = 640;
              
                button6.Text = ">";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (c1 == true)
            {
                string Path = "\\" + "\\Androiddb\\Attached\\" + comboBox7.Text + "\\" + comboBox6.Text + "\\" + ComboBox3.Text + ComboBox4.Text + "01" + "\\";

                if (!Directory.Exists(Path))
                {

                    Directory.CreateDirectory(Path);

                }

                textBox4.Text = Path;

            }


            if (checkBox1.Checked == true)
            {

                int si = ComboBox1.SelectedIndex;
                dsview = dts.Tables[0].DefaultView;
                bs.DataSource = dsview;
                bs.Filter = "F8 = '" + ComboBox1.Items[si].ToString() + "'";
                dataGridView1.DataSource = bs;
                string Path = "\\" + "\\Androiddb\\Attached\\" + "Tallaat" + "\\" + ComboBox3.Text + ComboBox4.Text + "01";
                string prvider_name = regex.Replace(ComboBox1.Items[si].ToString(), ".");
                if (radioButton8.Checked == true)
                {
                   
                    xlbodArch(textBox4.Text + regex.Replace(ComboBox1.Items[si].ToString(), ".") + ".xlsx");
                   
                }
                else if (radioButton7.Checked == true)
                {
                  
                    T_xlbod(textBox4.Text + regex.Replace(ComboBox1.Items[si].ToString(), ".") + ".xlsx", si, "Talaat Mostafa", (0.043 / 0.95), ComboBox1, Path, prvider_name, ComboBox1.Items[si].ToString());                  

                }
                MessageBox.Show("Done Successfully.");
            }
            else if (checkBox2.Checked == true)
            {
                progressBar1.Maximum = ComboBox1.Items.Count - 1;
                int start = Convert.ToInt32(textBox3.Text.ToString());
                int finish = Convert.ToInt32(TextBox2.Text.ToString());

                for (int si = start - 1; si <= finish - 1; si++)
                {
                    progressBar1.Value = si;
                    dsview = dts.Tables[0].DefaultView;
                    bs.DataSource = dsview;
                    try
                    {
                        bs.Filter = "F8 = '" + ComboBox1.Items[si].ToString() + "'";

                    }
                    catch
                    {

                    }
                    dataGridView1.DataSource = bs;
                    string prvider_name = regex.Replace(ComboBox1.Items[si].ToString(), ".");
                    string Path = "\\" + "\\Androiddb\\Attached\\" + "Tallaat" + "\\" + ComboBox3.Text + ComboBox4.Text + "01";
                    try
                    {
                        if (radioButton8.Checked == true)
                        {
                            xlbodArch(textBox4.Text + regex.Replace(ComboBox1.Items[si].ToString(), ".") + ".xlsx");
                           
                        }
                        else if (radioButton7.Checked == true)
                        {
                          
                            T_xlbod(textBox4.Text + regex.Replace(ComboBox1.Items[si].ToString(), ".") + ".xlsx", si, "Talaat Mostafa", (0.043 / 0.95), ComboBox1, Path, prvider_name, ComboBox1.Items[si].ToString());
                           
                        }
                    }
                    catch
                    {


                    }

                    label4.Text = si + "  From  " + (finish - 1).ToString();
                }
                MessageBox.Show("Done Successfully");
            }
            else if (checkBox5.Checked == true)
            {
                progressBar1.Maximum = ComboBox1.Items.Count - 1;
                for (int si = 0; si <= ComboBox1.Items.Count; si++)
                {

                    try
                    {
                        progressBar1.Value = si;
                        dsview = dts.Tables[0].DefaultView;
                        bs.DataSource = dsview;
                        try
                        {
                            bs.Filter = "F8 = '" + ComboBox1.Items[si].ToString() + "'";

                        }
                        catch
                        {

                        }
                        dataGridView1.DataSource = bs;
                        string prvider_name = regex.Replace(ComboBox1.Items[si].ToString(), ".");
                        string Path = "\\" + "\\Androiddb\\Attached\\" + "Tallaat" + "\\" + ComboBox3.Text + ComboBox4.Text + "01";
                        try
                        {
                            if (radioButton8.Checked == true)
                            {
                               
                                xlbodArch(textBox4.Text + regex.Replace(ComboBox1.Items[si].ToString(), ".") + ".xlsx");
                            }
                            else if (radioButton7.Checked == true)
                            {
                               
                                T_xlbod(textBox4.Text + regex.Replace(ComboBox1.Items[si].ToString(), ".") + ".xlsx", si, "Talaat Mostafa", (0.043 / 0.95), ComboBox1, Path, prvider_name, ComboBox1.Items[si].ToString());
                                
                            }
                        }
                        catch
                        {

                        }

                        label4.Text = si + "  From  " + (ComboBox1.Items.Count - 1).ToString();
                    }
                    catch
                    {

                    }
                }
                MessageBox.Show("Done Successfully");
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////
            else if (checkBox4.Checked == true)
            {
                int si = comboBox2.SelectedIndex;
                int ci = comboBox5.SelectedIndex;
                dsview = dts.Tables[0].DefaultView;
                bs.DataSource = dsview;
                bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "' AND F20 = '" + comboBox5.Items[ci].ToString() + "'";
                dataGridView1.DataSource = bs;
                string Path = "\\" + "\\Androiddb\\Attached\\" + comboBox7.Text + "\\" + comboBox6.Text + "\\" + ComboBox3.Text + ComboBox4.Text + "01";
                string prvider_name = (regex.Replace(comboBox2.Items[si].ToString(), "."));
                if (radioButton8.Checked == true)
                {
                    xlbodArch(textBox4.Text + regex.Replace(comboBox2.Items[si].ToString(), ".") + comboBox5.Items[ci].ToString() + ".xlsx");
                  
                }
                else if (radioButton7.Checked == true)
                {
                    xlbod(textBox4.Text + regex.Replace(comboBox2.Items[si].ToString(), ".") + comboBox5.Items[ci].ToString() + ".xlsx", si, comboBox6.Text, (0.043 / 0.8), comboBox2, Path, prvider_name, comboBox5.Items[ci].ToString(), comboBox2.Items[si].ToString(), comboBox7.Text);
                   
                }
                MessageBox.Show("Done Successfully.");
            }
            else if (checkBox3.Checked == true)
            {

                int start = Int32.Parse(textBox6.Text);
                int finish = Int32.Parse(textBox5.Text);
                for (int si = start - 1; si <= finish - 1; si++)
                {
                    try
                    {
                        progressBar1.Value = si;
                    }
                    catch
                    {

                    }

                    dsview = dts.Tables[0].DefaultView;
                    bs.DataSource = dsview;
                    try
                    {
                        bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "'";
                       
                    }
                    catch
                    {

                    }
                    //Soultion
                    Control.CheckForIllegalCrossThreadCalls = false;
                    dataGridView1.DataSource = bs;
                    removdup("F20", comboBox5);
                    for (int ci = 0; ci <= comboBox5.Items.Count; ci++)
                    {
                        dsview = dts.Tables[0].DefaultView;
                        bs.DataSource = dsview;
                        try
                        {
                            bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "' AND F20 = '" + comboBox5.Items[ci].ToString() + "'";
                            
                        }
                        catch
                        {

                        }
                        //Solution
                        Control.CheckForIllegalCrossThreadCalls = false;
                        dataGridView1.DataSource = bs;
                        string prvider_name = (regex.Replace(comboBox2.Items[si].ToString(), "."));
                        string Path = "\\" + "\\Androiddb\\Attached\\" + comboBox7.Text + "\\" + comboBox6.Text + "\\" + ComboBox3.Text + ComboBox4.Text + "01";
                        try
                        {
                            if (radioButton8.Checked == true)
                            {
                                xlbodArch(textBox4.Text + regex.Replace(comboBox2.Items[si].ToString(), ".") + comboBox5.Items[ci].ToString() + ".xlsx");                            

                            }
                            else if (radioButton7.Checked == true)
                            {
                                xlbod(textBox4.Text + regex.Replace(comboBox2.Items[si].ToString(), ".") + comboBox5.Items[ci].ToString() + ".xlsx", si, comboBox6.Text, (0.043 / 0.8), comboBox2, Path, prvider_name, comboBox5.Items[ci].ToString(), comboBox2.Items[si].ToString(), comboBox7.Text);
                              
                            }
                        }
                        catch
                        {

                        }

                        label4.Text = si + "  From  " + (finish - 1).ToString();
                    }


                }
                MessageBox.Show("Done Successfully.");

            }
            else if (checkBox6.Checked == true)
            {
                for (int si = 0; si <= comboBox2.Items.Count; si++)
                {
                    try
                    {
                        progressBar1.Value = si;
                    }
                    catch
                    {

                    }

                    dsview = dts.Tables[0].DefaultView;
                    bs.DataSource = dsview;
                    try
                    {
                        bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "'";

                    }
                    catch
                    {

                    }
                    //Solution
                    Control.CheckForIllegalCrossThreadCalls = false;
                    dataGridView1.DataSource = bs;
                    removdup("F20", comboBox5);
                    for (int ci = 0; ci <= comboBox5.Items.Count; ci++)
                    {
                        dsview = dts.Tables[0].DefaultView;
                        bs.DataSource = dsview;
                        try
                        {
                            bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "' AND F20 = '" + comboBox5.Items[ci].ToString() + "'";
                           
                        }
                        catch
                        {

                        }
                        //Solution
                        Control.CheckForIllegalCrossThreadCalls = false;
                        dataGridView1.DataSource = bs;
                        string prvider_name = (regex.Replace(comboBox2.Items[si].ToString(), "."));
                        string Path = "\\" + "\\Androiddb\\Attached\\" + comboBox7.Text + "\\" + comboBox6.Text + "\\" + ComboBox3.Text + ComboBox4.Text + "01";
                        if (radioButton8.Checked == true)
                        {
                            try
                            {
                                xlbodArch(textBox4.Text + regex.Replace(comboBox2.Items[si].ToString(), ".") + comboBox5.Items[ci].ToString() + ".xlsx");
                               
                            }
                            catch
                            {

                            }
                        }
                        else if (radioButton7.Checked == true)
                        {
                            try
                            {
                                xlbod(textBox4.Text + regex.Replace(comboBox2.Items[si].ToString(), ".") + comboBox5.Items[ci].ToString() + ".xlsx", si, comboBox6.Text, (0.043 / 0.8), comboBox2, Path, prvider_name, comboBox5.Items[ci].ToString(), comboBox2.Items[si].ToString(), comboBox7.Text);
                               
                            }
                            catch
                            {

                            }
                        }
                    }

                    label4.Text = si + "  From  " + (comboBox2.Items.Count - 1).ToString();
                }
                MessageBox.Show("Done Successfully.");
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Finished");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            backgroundWorker1.RunWorkerAsync();
           
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
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void xlbod(String x, int c_index, string type, Double eza_no, ComboBox co, string path, string provider_name, string policy, string drop1 , string InOutType)
        {
            try
            {

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.DisplayRightToLeft = false;

                //HEADER HEADER--------------------------------------------------------------
                //xlWorkSheet.PageSetup.CenterFooter = "&P/&N";
                xlWorkSheet.PageSetup.CenterFooter = "&B &P of &N &B";
                //xlWorkSheet.PageSetup.LeftHeader = "&F";
                //xlWorkSheet.PageSetup.RightHeader = "&D";
                //------------


                int i = 0;
                int j = 0;
                Excel.Range rng = xlApp.get_Range("A1:A2");
                rng.Style = "Normal";
                rng.EntireRow.Font.Bold = true;
                rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                rng.EntireRow.Font.Size = 12;

                //First and second rows
                xlWorkSheet.Cells[1, 1] = "444444TPA";
                xlWorkSheet.Cells[1, 11] = "رقم المطالبة";
                xlWorkSheet.Cells[1, 13] = "00/00/0000";
                xlWorkSheet.Cells[1, 14] = "تاريخ الإستحقاق";
                xlWorkSheet.Cells[1, 17] = "إسم الجهة";
                //2nd row
                xlWorkSheet.Cells[2, 1] = "رقم الفاتوره";
                xlWorkSheet.Cells[2, 3] = "نوع الجهه ";
                xlWorkSheet.Cells[2, 4] = "كودالماموريه";
                xlWorkSheet.Cells[2, 5] = "رقم الملف الضريبي";
                xlWorkSheet.Cells[2, 6] = "رقم البطاقه الضريبيه";
                xlWorkSheet.Cells[2, 7] = "مسلسل فرد الاسره";
                xlWorkSheet.Cells[2, 8] = "اسم الجهه";
                xlWorkSheet.Cells[2, 9] = "كود بند الصرف";
                xlWorkSheet.Cells[2, 10] = "تاريخ اداء الخدمه";
                xlWorkSheet.Cells[2, 11] = "قيمه الفاتوره بعد الخصم";
                xlWorkSheet.Cells[2, 12] = "قيمه الخصم";
                xlWorkSheet.Cells[2, 13] = "قيمه الفاتوره قبل الخصم و بعد التحمل";
                xlWorkSheet.Cells[2, 14] = "اسم بند الصرف";
                xlWorkSheet.Cells[2, 15] = "تاريخ بدء التامين";
                xlWorkSheet.Cells[2, 16] = "اسم المؤمن له و افراد الاسره";
                xlWorkSheet.Cells[2, 17] = "كود المؤمن عليه";
                xlWorkSheet.Cells[2, 18] = "رقم الوثيقه";

                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dataGridView1[j, i];
                        xlWorkSheet.Cells[i + 3, j + 1] = cell.Value;
                    }
                }

                //   Double  eza_no = (0.043 / 0.95);

                //if (co.Items[c_index].ToString() == "صيدليات العزبي")
                //{

                //    for (int h = 3; h <= dataGridView1.RowCount - 1; h++)
                //    {

                //        xlWorkSheet.Range["L" + h].Formula = "=ROUND(($M" + h + "*" + eza_no + "),2)";
                //        xlWorkSheet.Range["K" + h].Formula = "=ROUND(($M" + h + "-L" + h + "),2)";

                //    }
                //}

                //last row configuration
                int lRow = dataGridView1.RowCount + 3;
                int ldata = dataGridView1.RowCount + 2;
                Excel.Range Lrng = xlApp.get_Range("A" + lRow);
                Lrng.EntireRow.Font.Bold = true;
                Lrng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                Lrng.EntireRow.Font.Size = 12;
                xlWorkSheet.Range["P1"].Value = xlWorkSheet.Range["H3"].Value;
                xlWorkSheet.Range["A" + lRow].Value = "999999";
                xlWorkSheet.Range["C" + lRow].Value = xlWorkSheet.Range["C3"].Value;
                xlWorkSheet.Range["D" + lRow].Value = xlWorkSheet.Range["D3"].Value;
                xlWorkSheet.Range["F" + lRow].Value = xlWorkSheet.Range["F3"].Value;
                xlWorkSheet.Range["H" + lRow].Value = xlWorkSheet.Range["H3"].Value;
                string edate = ComboBox3.Text + ComboBox4.Text + "01";
                xlWorkSheet.Range["J" + lRow].Value = edate;
                xlWorkSheet.Range["K" + lRow].Formula = "=SUBTOTAL(109,$K3:$K" + ldata + ")";
                xlWorkSheet.Range["L" + lRow].Formula = "=SUBTOTAL(109,$L3:$L" + ldata + ")";
                xlWorkSheet.Range["M" + lRow].Formula = "=SUBTOTAL(109,$M3:$M" + ldata + ")";

                xlWorkSheet.Range["J3:J" + ldata].NumberFormat = "YYYYMMDD";
                xlWorkSheet.Range["O3:O" + ldata].NumberFormat = "YYYYMMDD";
                rng = xlWorkSheet.Range["a3:a" + ldata];
                rng.EntireRow.Font.Bold = true;
                rng.EntireRow.Font.Size = 12;
                rng.EntireRow.RowHeight = 35;
                rng = xlWorkSheet.Range["a1:a" + ldata];
                xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();

                for (int u = 1; u <= dataGridView1.RowCount + 3; u++)
                {

                    xlWorkSheet.Range["A" + u].BorderAround2();
                    xlWorkSheet.Range["A" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["A" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["B" + u].BorderAround2();
                    xlWorkSheet.Range["B" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["B" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["C" + u].BorderAround2();
                    xlWorkSheet.Range["C" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["C" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["D" + u].BorderAround2();
                    xlWorkSheet.Range["D" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["D" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["E" + u].BorderAround2();
                    xlWorkSheet.Range["E" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["E" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["F" + u].BorderAround2();
                    xlWorkSheet.Range["F" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["F" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["G" + u].BorderAround2();
                    xlWorkSheet.Range["G" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["G" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["H" + u].BorderAround2();
                    xlWorkSheet.Range["H" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["H" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["I" + u].BorderAround2();
                    xlWorkSheet.Range["I" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["I" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["J" + u].BorderAround2();
                    xlWorkSheet.Range["J" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["J" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["K" + u].BorderAround2();
                    xlWorkSheet.Range["K" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["K" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["L" + u].BorderAround2();
                    xlWorkSheet.Range["L" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["L" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["M" + u].BorderAround2();
                    xlWorkSheet.Range["M" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["M" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["N" + u].BorderAround2();
                    xlWorkSheet.Range["N" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["N" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["O" + u].BorderAround2();
                    xlWorkSheet.Range["O" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["O" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["P" + u].BorderAround2();
                    xlWorkSheet.Range["P" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["P" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["Q" + u].BorderAround2();
                    xlWorkSheet.Range["Q" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["Q" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["R" + u].BorderAround2();
                    xlWorkSheet.Range["R" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["R" + u].VerticalAlignment = -4108;
                }
                //Hide Cols
                //xlWorkSheet.Range["C:H"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["D:E"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["G:H"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["B:B"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["O:O"].EntireColumn.Hidden = false;
                label17.Text = dataGridView1.RowCount.ToString();
                //Lock Cols
                xlWorkSheet.Range["B1"].Style.Locked = true;

                int claim_count = dataGridView1.RowCount - 1;
                string no1 = "9";
                string no2 = "10";
                string pro1 = null;
                string pro2 = null;
                if ((radioButton4.Checked == true) && (drop1 != pro1) && (drop1 != pro2) && (xlWorkSheet.Range["I3"].Value.ToString() != no1) && (xlWorkSheet.Range["I3"].Value.ToString() != no2))
                {
                    for (int z = 3; z <= ldata; z++)
                    {
                        for (int g = z + 1; g <= ldata; g++)
                        {
                            if ((xlWorkSheet.Range["A" + z].Value == xlWorkSheet.Range["A" + g].Value) && (xlWorkSheet.Range["B" + z].Value == xlWorkSheet.Range["B" + g].Value) && (xlWorkSheet.Range["I" + z].Value.ToString() != no1) && (xlWorkSheet.Range["I" + z].Value.ToString() != no2) && (xlWorkSheet.Range["P" + z].Value.ToString() != pro1) && (xlWorkSheet.Range["P" + z].Value.ToString() != pro2) && (xlWorkSheet.Range["H" + z].Value == xlWorkSheet.Range["H" + g].Value) && (xlWorkSheet.Range["N" + z].Value == xlWorkSheet.Range["N" + g].Value))
                            {
                                xlWorkSheet.Range["K" + z].Value = xlWorkSheet.Range["K" + z].Value + xlWorkSheet.Range["K" + g].Value;
                                xlWorkSheet.Range["L" + z].Value = xlWorkSheet.Range["L" + z].Value + xlWorkSheet.Range["L" + g].Value;
                                xlWorkSheet.Range["M" + z].Value = xlWorkSheet.Range["M" + z].Value + xlWorkSheet.Range["M" + g].Value;
                                xlWorkSheet.Range["K" + g].Value = 0;
                                xlWorkSheet.Range["L" + g].Value = 0;
                                xlWorkSheet.Range["M" + g].Value = 0;
                                rng = xlWorkSheet.Range["A" + g];
                                rng.EntireRow.Font.Bold = true;
                                rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }
                    }
                }
                else
                {

                }
                if (radioButton6.Checked == true)
                {
                    string connectionString = @"Data Source=Androiddb;Initial Catalog=TPASys;Integrated Security=True";
                    //string connectionString = (@"Data Source=CCM\SQLEXPRESS;Initial Catalog=Archiving;Integrated Security=True");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string stament = ("insert into [TPASys].[dbo].[Provider] (PName,TottalClaims,AttachPath,PMonth,PYear,ClientName,PolicyNumber,PType) values(N'" + provider_name + "','" + claim_count + "','" + path + "','" + ComboBox4.Text + "','" + ComboBox3.Text + "','" + type + "','" + policy + "','" + InOutType + "')");
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
                else
                {

                }

                xlWorkBook.SaveAs(x);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show("Invalid Client");
            }
            catch (Exception ex)
            {

                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException
                    || ex is ArgumentOutOfRangeException)
                {

                }

            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                this.Height = 749;
                label18.Hide();
            }

            catch
            {


            }
        }


        private void button8_Click(object sender, EventArgs e)
        {

            // saveFileDialog1.ShowDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath + "\\";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text == "Talaat Mostafa")
            {
                tabControl1.SelectedTab = tabPage1;


            }
            else
            {
                tabControl1.SelectedTab = tabPage2;

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int si = comboBox2.SelectedIndex;
            dsview = dts.Tables[0].DefaultView;
            bs.DataSource = dsview;
            bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "'";
            dataGridView1.DataSource = bs;
            removdup("F20", comboBox5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //ComboBox c = new ComboBox();
            //c.Location = new Point(722, 75);
            Remove_hidden re = new Remove_hidden();
            re.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            search re = new search();
            re.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label18.Show();
            this.Height = 566;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                c1 = true;
                button8.Hide();
                return;
            }
            else if (radioButton2.Checked == true)
            {

                c1 = false;
                button8.Show();
                return;
            }

            if (radioButton4.Checked == true)
            {
                c2 = true;
                return;

            }

            else if (radioButton3.Checked == true)
            {

                c2 = false;
                return;
            }

            if (radioButton6.Checked == true)
            {

                c3 = true;
                return;

            }

            else if (radioButton5.Checked == true)
            {
                c3 = false;
                return;
                //MessageBox.Show("Please choose any option", "My Application",
                //MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (comboBox6.Text == "Talaat Mostafa")
            //{
            //    try
            //    {


            //        for (int ind = 0; ind <= ComboBox1.Items.Count; ind++)
            //        {

            //            import(ind, "Talaat Mostafa", ComboBox1);
            //        }
            //    }
            //    catch
            //    {


            //    }
            //}
            //else
            //{
            //    //     try
            //    // {


            //    for (int ind = 0; ind <= comboBox2.Items.Count; ind++)
            //    {
            //        try
            //        {
            //            import(ind, "Elsweedy", comboBox2);
            //        }
            //        catch
            //        {


            //        }
            //    }
            //}
            //catch
            //{


            //}
            importDataBase re = new importDataBase();
            re.Show();
        }

        private void import(string saver, string provider, string type, string policy_no,string Pro_Type)
        {
            //int i = 0;
            //int j = 0;
            //for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            //{
            //    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
            //    {
            //        DataGridViewCell cell = dataGridView1[j, i];
            //        xlWorkSheet.Cells[i + 3, j + 1] = cell.Value;
            //    }
            //}

            int claim_count = dataGridView1.RowCount - 1;

            string connectionString = (@"Data Source=Androiddb;Initial Catalog=TPASys;Integrated Security=True");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string stament = ("insert into [TPASys].[dbo].[Provider] (PName,PolicyNumber,TottalClaims,AttachPath,PMonth,PYear,ClientName,PType) values(N'" + regex.Replace(provider.ToString(), ".") + "','" + policy_no + "','" + claim_count + "','" + saver + "','" + ComboBox4.Text + "','" + ComboBox3.Text + "','" + type + "','" + Pro_Type + "')");
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
        private void T_import(string saver, string provider, string type)
        {
            //int i = 0;
            //int j = 0;
            //for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            //{
            //    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
            //    {
            //        DataGridViewCell cell = dataGridView1[j, i];
            //        xlWorkSheet.Cells[i + 3, j + 1] = cell.Value;
            //    }
            //}

            int claim_count = dataGridView1.RowCount - 1;

            string connectionString = (@"Data Source=Androiddb;Initial Catalog=TPASys;Integrated Security=True");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string stament = ("insert into [TPASys].[dbo].[Provider] (PName,TottalClaims,AttachPath,PMonth,PYear,ClientName) values(N'" + regex.Replace(provider.ToString(), ".") + "','" + claim_count + "','" + saver + "','" + ComboBox4.Text + "','" + ComboBox3.Text + "','" + type + "')");
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
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //  int index = ComboBox1.FindString(textBox7.Text);
            ////  int index = ComboBox1.valueMembers.Contains(textBox7.Text);
            //  ComboBox1.SelectedIndex = index;

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //ThreadStart DisplayThread = new ThreadStart(display);
            //Thread t2 = new Thread(DisplayThread);
            //t2.Start();
            //display();
            ////Assign re = new Assign();
            ////re.Show();
        }
        public void display()
        {
            Assign re = new Assign();
            re.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                dsview = dts.Tables[0].DefaultView;
                bs.DataSource = dsview;
                bs.Filter = "F8 = '" + ComboBox1.Text.ToString() + "'";
                dataGridView1.DataSource = bs;
                string prvider_name = (regex.Replace(ComboBox1.Text.ToString(), "."));
                T_import("\\" + "\\Androiddb\\Attached\\" + "Tallaat" + "\\" + ComboBox3.Text + ComboBox4.Text + "01", prvider_name, "Talaat Mostafa");
                MessageBox.Show("Done Successfully.");
            }
            catch
            {
                MessageBox.Show("Database not avilable !");
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string prvider_name;
           
                if (checkBox6.Checked == true)
                {
                    for (int si = 0; si <= comboBox2.Items.Count; si++)
                    {
                        try
                        {
                            progressBar1.Value = si;
                        }
                        catch
                        {

                        }
                        
                        dsview = dts.Tables[0].DefaultView;
                        bs.DataSource = dsview;
                        try
                        {
                            bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "'";

                        }
                        catch { }
                        dataGridView1.DataSource = bs;
                        removdup("F20", comboBox5);
                        for (int ci = 0; ci <= comboBox5.Items.Count; ci++)
                        {
                            dsview = dts.Tables[0].DefaultView;
                            bs.DataSource = dsview;
                            try
                            {
                                bs.Filter = "F8 = '" + comboBox2.Items[si].ToString() + "' AND F20 = '" + comboBox5.Items[ci].ToString() + "'";

                           
                            dataGridView1.DataSource = bs;
                            prvider_name = (regex.Replace(comboBox2.Items[si].ToString(), ".") );
                            import("\\" + "\\Androiddb\\Attached\\" + comboBox7.Text + "\\" + comboBox6.Text + "\\" + ComboBox3.Text + ComboBox4.Text + "01", prvider_name, comboBox6.Text, comboBox5.Items[ci].ToString(),comboBox7.Text);
                            }
                            catch { }
                            }
                    }
                }
                else
                {
                    dsview = dts.Tables[0].DefaultView;
                    bs.DataSource = dsview;
                    bs.Filter = "F8 = '" + comboBox2.Text.ToString() + "'";
                    dataGridView1.DataSource = bs;
                    removdup("F20", comboBox5);
                    dsview = dts.Tables[0].DefaultView;
                    bs.DataSource = dsview;
                    bs.Filter = "F8 = '" + comboBox2.Text.ToString() + "' AND F20 = '" + comboBox5.Text.ToString() + "'";
                    dataGridView1.DataSource = bs;
                    prvider_name = (regex.Replace(comboBox2.Text.ToString(), ".") );
                    try
                    {
                        import("\\" + "\\Androiddb\\Attached\\" + comboBox7.Text + "\\" + comboBox6.Text + "\\" + ComboBox3.Text + ComboBox4.Text + "01", prvider_name, comboBox6.Text, comboBox5.Text.ToString(),comboBox7.Text);
                    }
                    catch
                    {

                    }
                }

                MessageBox.Show("Done Successfully.");
            
         
          
                        }




        private void xlbodArch(String x)
        {

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = false;
          
            //HEADER HEADER--------------------------------------------------------------
            //xlWorkSheet.PageSetup.CenterFooter = "&P/&N";
            xlWorkSheet.PageSetup.CenterFooter = "&B &P of &N &B";
            //xlWorkSheet.PageSetup.LeftHeader = "&F";
            //xlWorkSheet.PageSetup.RightHeader = "&D";
            //------------

            int i = 0;
            int j = 0;
            Excel.Range rng = xlApp.get_Range("A1:A1");
            rng.Style = "Normal";
            rng.EntireRow.Font.Bold = true;
            rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGreen);
            rng.EntireRow.Font.Size = 14;

            //First and second rows
            xlWorkSheet.Cells[1, 1] = "Claim Set Id";
            xlWorkSheet.Cells[1, 2] = "Claim Code";
            xlWorkSheet.Cells[1, 3] = "Batch Id";
            xlWorkSheet.Cells[1, 4] = "Provider";
            xlWorkSheet.Cells[1, 5] = "Member name";
            xlWorkSheet.Cells[1, 6] = "Policy no";
          

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                }
            }

            //   Double  eza_no = (0.043 / 0.95);

          

            //last row configuration
            int lRow = dataGridView1.RowCount + 3;
            int ldata = dataGridView1.RowCount + 2;
            Excel.Range Lrng = xlApp.get_Range("A" + lRow);

            xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();

            for (int u = 1; u <= dataGridView1.RowCount + 3; u++)
            {

                xlWorkSheet.Range["A" + u].BorderAround2();
                xlWorkSheet.Range["A" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["A" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["B" + u].BorderAround2();
                xlWorkSheet.Range["B" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["B" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["C" + u].BorderAround2();
                xlWorkSheet.Range["C" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["C" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["D" + u].BorderAround2();
                xlWorkSheet.Range["D" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["D" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["E" + u].BorderAround2();
                xlWorkSheet.Range["E" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["E" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["F" + u].BorderAround2();
                xlWorkSheet.Range["F" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["F" + u].VerticalAlignment = -4108;
                
            }
            int claim_count = dataGridView1.RowCount - 1;
            xlWorkBook.SaveAs(x);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        private void T_xlbod(String x, int c_index, string type, Double eza_no, ComboBox co, string path, string provider_name,string drop1)
        {
            try
            {

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.DisplayRightToLeft = false;
                //HEADER HEADER--------------------------------------------------------------
                //xlWorkSheet.PageSetup.CenterFooter = "&P/&N";
                xlWorkSheet.PageSetup.CenterFooter = "&B &P of &N &B";
                //xlWorkSheet.PageSetup.LeftHeader = "&F";
                //xlWorkSheet.PageSetup.RightHeader = "&D";
                //------------

                int i = 0;
                int j = 0;
                Excel.Range rng = xlApp.get_Range("A1:A2");
                rng.Style = "Normal";
                rng.EntireRow.Font.Bold = true;
                rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                rng.EntireRow.Font.Size = 12;
                //First and second rows
                xlWorkSheet.Cells[1, 1] = "444444TPA";
                xlWorkSheet.Cells[1, 11] = "رقم المطالبة";
                xlWorkSheet.Cells[1, 13] = "00/00/0000";
                xlWorkSheet.Cells[1, 14] = "تاريخ الإستحقاق";
                xlWorkSheet.Cells[1, 17] = "إسم الجهة";
                //2nd row
                xlWorkSheet.Cells[2, 1] = "رقم الفاتوره";
                xlWorkSheet.Cells[2, 3] = "نوع الجهه ";
                xlWorkSheet.Cells[2, 4] = "كودالماموريه";
                xlWorkSheet.Cells[2, 5] = "رقم الملف الضريبي";
                xlWorkSheet.Cells[2, 6] = "رقم البطاقه الضريبيه";
                xlWorkSheet.Cells[2, 7] = "مسلسل فرد الاسره";
                xlWorkSheet.Cells[2, 8] = "اسم الجهه";
                xlWorkSheet.Cells[2, 9] = "كود بند الصرف";
                xlWorkSheet.Cells[2, 10] = "تاريخ اداء الخدمه";
                xlWorkSheet.Cells[2, 11] = "قيمه الفاتوره بعد الخصم";
                xlWorkSheet.Cells[2, 12] = "قيمه الخصم";
                xlWorkSheet.Cells[2, 13] = "قيمه الفاتوره قبل الخصم و بعد التحمل";
                xlWorkSheet.Cells[2, 14] = "اسم بند الصرف";
                xlWorkSheet.Cells[2, 15] = "تاريخ بدء التامين";
                xlWorkSheet.Cells[2, 16] = "اسم المؤمن له و افراد الاسره";
                xlWorkSheet.Cells[2, 17] = "كود المؤمن عليه";
                xlWorkSheet.Cells[2, 18] = "رقم الوثيقه";

                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {

                        DataGridViewCell cell = dataGridView1[j, i];
                        xlWorkSheet.Cells[i + 3, j + 1] = cell.Value;
                    }
                }



                //if (co.Items[c_index].ToString() == "صيدليات العزبي")
                //{

                //    for (int h = 3; h <= dataGridView1.RowCount - 1; h++)
                //    {

                //        xlWorkSheet.Range["L" + h].Formula = "=ROUND(($M" + h + "*" + eza_no + "),2)";
                //        xlWorkSheet.Range["K" + h].Formula = "=ROUND(($M" + h + "-L" + h + "),2)";

                //    }
                //}


                int lRow = dataGridView1.RowCount + 3;
                int ldata = dataGridView1.RowCount + 2;
                Excel.Range Lrng = xlApp.get_Range("A" + lRow);
                Lrng.EntireRow.Font.Bold = true;
                Lrng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                Lrng.EntireRow.Font.Size = 12;
                xlWorkSheet.Range["P1"].Value = xlWorkSheet.Range["H3"].Value;
                xlWorkSheet.Range["A" + lRow].Value = "999999";
                xlWorkSheet.Range["C" + lRow].Value = xlWorkSheet.Range["C3"].Value;
                xlWorkSheet.Range["D" + lRow].Value = xlWorkSheet.Range["D3"].Value;
                xlWorkSheet.Range["F" + lRow].Value = xlWorkSheet.Range["F3"].Value;
                xlWorkSheet.Range["H" + lRow].Value = xlWorkSheet.Range["H3"].Value;
                string edate = ComboBox3.Text + ComboBox4.Text + "01";
                xlWorkSheet.Range["J" + lRow].Value = edate;
                xlWorkSheet.Range["K" + lRow].Formula = "=Sum($K3:$K" + ldata + ")";
                xlWorkSheet.Range["L" + lRow].Formula = "=Sum($L3:$L" + ldata + ")";
                xlWorkSheet.Range["M" + lRow].Formula = "=Sum($M3:$M" + ldata + ")";

                xlWorkSheet.Range["J3:J" + ldata].NumberFormat = "YYYYMMDD";
                xlWorkSheet.Range["O3:O" + ldata].NumberFormat = "YYYYMMDD";
                rng = xlWorkSheet.Range["a3:a" + ldata];
                rng.EntireRow.Font.Bold = true;
                rng.EntireRow.Font.Size = 12;
                rng.EntireRow.RowHeight = 35;
                rng = xlWorkSheet.Range["a1:a" + ldata];
                xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();
                label17.Text = dataGridView1.RowCount.ToString();

                for (int u = 1; u <= dataGridView1.RowCount + 3; u++)
                {

                    xlWorkSheet.Range["A" + u].BorderAround2();
                    xlWorkSheet.Range["A" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["A" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["B" + u].BorderAround2();
                    xlWorkSheet.Range["B" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["B" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["C" + u].BorderAround2();
                    xlWorkSheet.Range["C" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["C" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["D" + u].BorderAround2();
                    xlWorkSheet.Range["D" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["D" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["E" + u].BorderAround2();
                    xlWorkSheet.Range["E" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["E" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["F" + u].BorderAround2();
                    xlWorkSheet.Range["F" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["F" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["G" + u].BorderAround2();
                    xlWorkSheet.Range["G" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["G" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["H" + u].BorderAround2();
                    xlWorkSheet.Range["H" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["H" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["I" + u].BorderAround2();
                    xlWorkSheet.Range["I" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["I" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["J" + u].BorderAround2();
                    xlWorkSheet.Range["J" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["J" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["K" + u].BorderAround2();
                    xlWorkSheet.Range["K" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["K" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["L" + u].BorderAround2();
                    xlWorkSheet.Range["L" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["L" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["M" + u].BorderAround2();
                    xlWorkSheet.Range["M" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["M" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["N" + u].BorderAround2();
                    xlWorkSheet.Range["N" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["N" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["O" + u].BorderAround2();
                    xlWorkSheet.Range["O" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["O" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["P" + u].BorderAround2();
                    xlWorkSheet.Range["P" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["P" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["Q" + u].BorderAround2();
                    xlWorkSheet.Range["Q" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["Q" + u].VerticalAlignment = -4108;
                    xlWorkSheet.Range["R" + u].BorderAround2();
                    xlWorkSheet.Range["R" + u].HorizontalAlignment = -4108;
                    xlWorkSheet.Range["R" + u].VerticalAlignment = -4108;
                }

                //Hide Cols
                //xlWorkSheet.Range["C:H"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["D:E"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["G:H"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["B:B"].EntireColumn.Hidden = true;
                xlWorkSheet.Range["O:O"].EntireColumn.Hidden = false;


                //Lock Cols
            //    xlWorkSheet.Range["B1"].Style.Locked = true;
                int claim_count = dataGridView1.RowCount - 1;
                string no1 = "9";
                string no2 = "10";
                string pro1 = null;
                string pro2 = null;
                if ((radioButton4.Checked == true) && (drop1 != pro1) && (drop1 != pro2) && (xlWorkSheet.Range["I3"].Value.ToString() != no1) && (xlWorkSheet.Range["I3"].Value.ToString() != no2))
                {
                    //MessageBox.Show(pro1+","+ drop1);
                    for (int z = 3; z <= ldata; z++)
                    {
                        for (int g = z + 1; g <= ldata; g++)
                        {
                            if (((xlWorkSheet.Range["A" + z].Value == xlWorkSheet.Range["A" + g].Value) && (xlWorkSheet.Range["B" + z].Value == xlWorkSheet.Range["B" + g].Value) && (xlWorkSheet.Range["H" + z].Value == xlWorkSheet.Range["H" + g].Value) && (xlWorkSheet.Range["N" + z].Value == xlWorkSheet.Range["N" + g].Value)))
                            {
                                xlWorkSheet.Range["K" + z].Value = xlWorkSheet.Range["K" + z].Value + xlWorkSheet.Range["K" + g].Value;
                                xlWorkSheet.Range["L" + z].Value = xlWorkSheet.Range["L" + z].Value + xlWorkSheet.Range["L" + g].Value;
                                xlWorkSheet.Range["M" + z].Value = xlWorkSheet.Range["M" + z].Value + xlWorkSheet.Range["M" + g].Value;
                                xlWorkSheet.Range["K" + g].Value = 0;
                                xlWorkSheet.Range["L" + g].Value = 0;
                                xlWorkSheet.Range["M" + g].Value = 0;
                                rng = xlWorkSheet.Range["A" + g];
                                rng.EntireRow.Font.Bold = true;
                                rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                            }
                        }

                    }
                }

                else
                {

                }
                if (radioButton6.Checked == true)
                {
                    string connectionString = @"Data Source=Androiddb;Initial Catalog=TPASys;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string stament = ("insert into [TPASys].[dbo].[Provider] (PName,TottalClaims,AttachPath,PMonth,PYear,ClientName) values(N'" + provider_name + "','" + claim_count + "','" + path + "','" + ComboBox4.Text + "','" + ComboBox3.Text + "','" + type + "')");
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
                else
                {

                }

                xlWorkBook.SaveAs(x);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Invalid Client");
            }
            catch (Exception ex)
            {

                if (ex is FormatException ||
                    ex is OverflowException ||
                    ex is ArgumentNullException
                    || ex is ArgumentOutOfRangeException)
                {

                }

            }

        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                dsview = dts.Tables[0].DefaultView;
                bs.DataSource = dsview;
                bs.Filter = "F8 like '%" + textBox7.Text.ToString() + "%'";
                dataGridView1.DataSource = bs;
            }
            catch
            {

            }
       }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

     

    }
}