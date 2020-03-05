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
    public partial class AddBalackOfPharm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddBalackOfPharm")).Attributes["class"] = "active";
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
                            var x = dc.PharmacyTBs.FirstOrDefault(a => a.ID== id);


                            if (x == null)
                            {
                                count++;

                                dc.PharmacyTBs.Add(new PharmacyTB
                                {
                                    PharmName = dr["PharmName"].ToString(),
                                    PharmAddressCode = dr["PharmAddressCode"].ToString(),
                                    PharmAddress = dr["PharmAddress"].ToString(),
                                    LocID = int.Parse(dr["LocID"].ToString()),
                                    PharmSublocationName = dr["PharmSublocationName"].ToString(),
                                    PharmPhone = dr["PharmPhone"].ToString(),
                                    PharmPhone1 = dr["PharmPhone1"].ToString(),
                                    PharmPhone2 = dr["PharmPhone2"].ToString(),
                                    PharmPhone3 = dr["PharmPhone3"].ToString(),
                                    PharmNotes = dr["PharmNotes"].ToString(),
                                   // PharmLong = dr["PharmLong"].ToString(),
                                   // PharmLat = dr["PharmLat"].ToString(),
                                    Discount = int.Parse(dr["Discount"].ToString())
                                });
                            }
                        }
                        dc.SaveChanges();
                    }
                    div_Success.InnerHtml = "The New Pharms Inserted To Pharm List Is :" + count;
                    div_Success.Visible = true;
                }
                else
                {
                    div_Error.InnerHtml = "Error Please Choose The Excel";
                    div_Error.Visible = true;
                }
            }
            catch (Exception ex)
            {
                div_Error.InnerHtml = "Error Excel format Is Not Correct  ";
                div_Error.Visible = true;
            }
        }

    }
}