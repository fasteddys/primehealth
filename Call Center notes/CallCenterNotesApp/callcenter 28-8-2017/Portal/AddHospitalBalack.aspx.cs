﻿using System;
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
    public partial class AddHospitalBalack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddHospitalBalack")).Attributes["class"] = "active";
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
                            var x = dc.HospitalTBs.FirstOrDefault(a => a.ID == id);


                            if (x == null)
                            {
                                count++;
                                dc.HospitalTBs.Add(new HospitalTB
                                {
                                    HospName = dr["HospName"].ToString(),
                                    AddressNum = dr["AddressNum"].ToString(),
                                    HospAddress = dr["HospAddress"].ToString(),
                                    Category = dr["Category"].ToString(),
                                    LocID = int.Parse(dr["LocID"].ToString()),
                                    SubLocationName = dr["SubLocationName"].ToString(),
                                    HospPhone = dr["HospPhone"].ToString(),
                                    HospPhone2 = dr["HospPhone2"].ToString(),
                                    HospPhone3 = dr["HospPhone3"].ToString(),
                                    HospPhone4 = dr["HospPhone4"].ToString(),
                                    HospNotes = dr["HospNotes"].ToString(),
                                   // HospLong = dr["HospLong"].ToString(),
                                   // HospLat = dr["HospLat"].ToString(),
                                    QNB = int.Parse(dr["QNB"].ToString()),
                                    Meduim = int.Parse(dr["Meduim"].ToString()),
                                    Large = int.Parse(dr["Large"].ToString()),
                                    Discount = int.Parse(dr["Discount"].ToString()),
                                    HospitalNetwork = dr["HospitalNetwork"].ToString(),
                                    AddByEmp = dr["AddByEmp"].ToString(),
                                    AddedTime = dr["AddedTime"].ToString(),

                                });

                            }
                        }
                        dc.SaveChanges();
                    }
                    div_Success.InnerHtml = "The New Hospital Inserted To Hospital List Is :" + count;
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
                div_Error.InnerHtml = "Error Excel format ";
                div_Error.Visible = true;
            }
        }

    }
}