using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using CallCenterNotesApp.DLL;


namespace CallCenterNotesApp.Portal
{
    public partial class AddBalackOptical : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddBalackOptical")).Attributes["class"] = "active";
            div_Error.Visible = false;
            div_Success.Visible = false;
        }
        static DataSet Parse(string fileName)
        {
            string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0};Extended Properties=Excel 12.0;", fileName);


            DataSet data = new DataSet();

            foreach (var sheetName in GetExcelSheetNames(connectionString))
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    var dataTable = new DataTable();
                    string query = string.Format("SELECT * FROM [{0}]", sheetName);
                    con.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                    adapter.Fill(dataTable);
                    data.Tables.Add(dataTable);

                }
            }

            return data;
        }

        static string[] GetExcelSheetNames(string connectionString)
        {
            OleDbConnection con = null;
            DataTable dt = null;
            con = new OleDbConnection(connectionString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }
        protected void btn_add_balack_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                if (FileUpload1.HasFile)
                {
                    string fileName = Path.Combine(Server.MapPath("~/ImportDocument"), Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.PostedFile.FileName));
                    FileUpload1.PostedFile.SaveAs(fileName);
                    DataSet ds = Parse(fileName);

                    using (PhNetworkEntities dc = new PhNetworkEntities())
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {


                            int id = 0;
                            try
                            {
                                id = int.Parse(dr["ID"].ToString());
                            }
                            catch { }
                            var x = dc.OpticalTBs.FirstOrDefault(a => a.ID == id);



                            if (x == null)
                            {
                                count++;

                                dc.OpticalTBs.Add(new OpticalTB
                                {
                                    OpticalName = dr["OpticalName"].ToString(),
                                    OpticalAddressCode = dr["OpticalAddressCode"].ToString(),
                                    OpticalAddress = dr["OpticalAddress"].ToString(),
                                    Category = dr["Category"].ToString(),
                                    LocID = int.Parse(dr["LocID"].ToString()),

                                    OpticalSublocationName = dr["OpticalSublocationName"].ToString(),
                                    OpticalPhone = dr["OpticalPhone"].ToString(),
                                    Opticalphone2 = dr["Opticalphone2"].ToString(),
                                    Opticalphone3 = dr["Opticalphone4"].ToString(),
                                    Opticalphone4 = dr["Opticalphone4"].ToString(),
                                    OpticalNotes = dr["OpticalNotes"].ToString(),
                                    //OpticalLong = dr["OpticalLong"].ToString(),
                                   // OpticalLat = dr["OpticalLat"].ToString(),
                                    Discount = int.Parse(dr["Discount"].ToString())
                                });
                            }
                        }
                        dc.SaveChanges();
                    }
                    div_Success.InnerHtml = "The New Opticals Inserted To Optical List Is :" + count;
                    div_Success.Visible = true;
                }
                else
                {
                    div_Error.InnerHtml = "Error Please Choose The Excel";
                    div_Error.Visible = true;
                }
            }
            catch (Exception)
            {
                div_Error.InnerHtml = "Error Excel Format Is Not Correct  ";
                div_Error.Visible = true;
            }
        }

    }
}