using CardCycle.BLL;
using CardCycle.exel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CardCycle
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        System.Data.DataTable dataTable = new System.Data.DataTable();

        protected void submit_Click(object sender, EventArgs e)
        {

            Request req = new Request();
            string status = "";
          
            foreach (ListItem control in list_of_check_boxs.Items)
            {
                   
                    if (control.Selected == true)
                        status = status + control.Text;

                

            }

            var data = req.daily_report(TextBox1.Text, TextBox2.Text, status);
            foreach(var item in data)
            {if (item.isApproved == "Null")
                    item.isApproved = "";

            }
           
            lstNewReq.DataSource = data;

            lstNewReq.DataBind();

            double? number = 0;
            foreach (var item in data)
            {
                if (item.IssCardsNum != null)
                { number = number + item.IssCardsNum; }
            }
            Label1.Text = number.ToString();
            Label2.Visible = true;
            Label3.Visible = true;
            Label2.Text = Request.Cookies["Name"].Value.Trim();
            Label4.Text = Label4.Text ;
            Label5.Text = TextBox1.Text;
            Label5.Visible = true;
            Label4.Visible = true;
        }

        protected void export_to_exel_Click(object sender, EventArgs e)
        {
            ExcelUtlity exel = new ExcelUtlity();
            Request req = new Request();
            string status="";

            foreach (ListItem control in list_of_check_boxs.Items)
            {

                if (control.Selected == true)
                    status = status + control.Text;



            }



            var data = req.daily_report(TextBox1.Text, TextBox2.Text, status);


            var table = data.Select(x => new { x.id, x.ClientName, x.IssCardsNum, x.rType, x.rdate, x.isApproved, x.States}).ToList();
          
            dataTable = Reportes.ToDataTable(table);
            dataTable.Columns["id"].ColumnName = "ser";
            dataTable.Columns["ClientName"].ColumnName = "Client Name";

            dataTable.Columns["IssCardsNum"].ColumnName = "Count";
            dataTable.Columns["rType"].ColumnName = "request type";
            dataTable.Columns["isApproved"].ColumnName = "close Date";

            dataTable.Columns["States"].ColumnName = "Case";
            dataTable.Columns["rdate"].ColumnName = "Received Request";
            dataTable.Columns.Add("Inception Date", typeof(string));
            dataTable.Columns[7].SetOrdinal(3);
            //chang her
            exel.WriteDataTableToExcel(dataTable, "Person Details", @"C:\inetpub\wwwroot\CCMTest\ccm\daily excel sheet\testPersonExceldata.xlsx", Label1.Text);
            string FileName = TextBox1.Text + ".xlsx"; // It's a file name displayed on downloaded file on client side.

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/daily excel sheet/testPersonExceldata.xlsx"));
            response.Flush();
            response.End();


           
        }



        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }





           










            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected void Select_ALL_CheckedChanged(object sender, EventArgs e)
        {
            //if(Select_ALL.Checked==true)
            //{
            //    foreach (ListItem control in list_of_check_boxs.Items)
            //    {

            //        if (control.Selected == true)
            //        { control.Selected = false; }



            //    }
            //}
            //else
            //{
            //    foreach (ListItem control in list_of_check_boxs.Items)
            //    {

            //        if (control.Selected == false)
            //        { control.Selected = true; }



            //    }
            //}
        }

        protected void list_of_check_boxs_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
            {
                foreach (ListItem item in list_of_check_boxs.Items)
                { item.Selected = true; }
            }
        }

   
    }
}