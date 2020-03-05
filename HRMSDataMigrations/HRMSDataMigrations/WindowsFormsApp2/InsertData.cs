using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Enums;
using WindowsFormsApp2.Model;
using static WindowsFormsApp2.Enums.StaticEnums;

namespace WindowsFormsApp2
{
    public partial class InsertData : Form
    {

        public string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

        public InsertData()
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
                        MessageBox.Show("File Upload Is not Excel");
                        break;
                }
                //Files has uploaded Must be Excel file and refuse others

                if (extension.Trim() == ".xlsx" || extension.Trim() == ".xls")
                {
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

                                ExcelData.DataSource = datatable1;


                            }
                        }
                    }

                    //Get the name of the First Sheet.


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





        private void Browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                // Add User Data Only

                DataTable FaildTransactionTable = new DataTable();

                FaildTransactionTable.Columns.Add("User Name", typeof(string));
                FaildTransactionTable.Columns.Add("User ID", typeof(string));
                FaildTransactionTable.Columns.Add("HireDate", typeof(string));
                FaildTransactionTable.Columns.Add("Department", typeof(string));
                FaildTransactionTable.Columns.Add("SubDepartment", typeof(string));
                FaildTransactionTable.Columns.Add("Team Manager", typeof(string));
                FaildTransactionTable.Columns.Add("Direct Manager", typeof(string));
                FaildTransactionTable.Columns.Add("Arabic Name", typeof(string));
                FaildTransactionTable.Columns.Add("Position Name", typeof(string));
                FaildTransactionTable.Columns.Add("Gender", typeof(string));
                FaildTransactionTable.Columns.Add("BirthDate", typeof(string));
                FaildTransactionTable.Columns.Add("Phone Number", typeof(string));
                FaildTransactionTable.Columns.Add("Annual vacation", typeof(string));
                FaildTransactionTable.Columns.Add("Casual vacation", typeof(string));
                FaildTransactionTable.Columns.Add("Total Excuse", typeof(string));


                //FaildTransactionTable.Columns.Add("Seniority Before Hire Years", typeof(string));
                //FaildTransactionTable.Columns.Add("Seniority Before Hire Month", typeof(string));
                FaildTransactionTable.Columns.Add("Medical ID", typeof(string));
                FaildTransactionTable.Columns.Add("National ID", typeof(string));
                // FaildTransactionTable.Columns.Add("Address", typeof(string));
                FaildTransactionTable.Columns.Add("Company Name", typeof(string));

                FaildTransactionTable.Columns.Add("Contract Type", typeof(string));
                FaildTransactionTable.Columns.Add("Reason", typeof(string));






                User UserCompletInformations = new User();
                User TeamManager = new User();
                SubDepartment subDepartment = new SubDepartment();
                Department department = new Department();
                Position position = new Position();
                ContractTypeDIM contractType = new ContractTypeDIM();
                GenderTypeDIM genderType = new GenderTypeDIM();
                Manager manager = new Manager();
                //Forign Keys
                int? subDepartmentID = 0;
                int? departmentID = 0;
                int? positionID = 0;
                int? managerID = 0;

                using (ExcelPackage excel = new ExcelPackage())
                {


                    // Target a worksheet

                    // Popular header row data

                    foreach (DataGridViewRow row in ExcelData.Rows)
                    {

                        try
                        {
                            bool IsValid = true;
                            string ErrorMessage = "";
                            //Add Required data For Users
                            string DepartmentName = row.Cells["Department"].Value.ToString();
                            if (HRMS.Departments.Where(x => x.DepartmentName == DepartmentName).Count() > 0)
                            {
                                var z = HRMS.Departments.Where(x => x.DepartmentName == DepartmentName).FirstOrDefault();

                                departmentID = HRMS.Departments.Where(x => x.DepartmentName == DepartmentName).FirstOrDefault().DepartmentID;

                            }
                            else if (DepartmentName == "NULL")
                            {
                                departmentID = null;

                            }
                            else
                            {
                                ErrorMessage = row.Cells["Department"].Value.ToString() + " Department Not Exist ,";
                                IsValid = false;

                            }

                            string SubDepartmentName = row.Cells["SubDepartment"].Value.ToString();
                            if (HRMS.SubDepartments.Where(x => x.SubDepartmentName == SubDepartmentName && x.DepartmentFK == departmentID).Count() > 0)
                            {


                                subDepartmentID = HRMS.SubDepartments.Where(x => x.SubDepartmentName == SubDepartmentName).FirstOrDefault().SubDepartmentID;

                            }
                          else  if (SubDepartmentName == "NULL")
                            {
                                subDepartmentID = null;

                            }
                            else
                            {


                                ErrorMessage = SubDepartmentName + " SubDepartment Not Exist,";
                                IsValid = false;
                            }
                            string PositionName = row.Cells["Position Name"].Value.ToString();
                            if (HRMS.Positions.Where(x => x.PositionName == PositionName).Count() > 0)
                            {
                                positionID = HRMS.Positions.Where(x => x.PositionName == PositionName).FirstOrDefault().PositionID;



                            }
                          else  if (PositionName == "NULL")
                            {
                                positionID = null;

                            }
                            else
                            {
                                ErrorMessage = row.Cells["Position Name"].Value.ToString() + " Postion Not Exist ,";
                                IsValid = false;
                            }


                            string ManagerName = row.Cells["Direct Manager"].Value.ToString();
                            if (HRMS.Users.Where(x => x.LDAPUserName == ManagerName).Count() > 0)
                            {
                                int managerUserID = HRMS.Users.Where(x => x.LDAPUserName == ManagerName).FirstOrDefault().UserID;
                                if (HRMS.Managers.Where(x => x.ManagerUserFK == managerUserID).Count() > 0)
                                {

                                    managerID = HRMS.Managers.Where(x => x.ManagerUserFK == managerUserID).FirstOrDefault().ManagerID;
                                }
                                else
                                {
                                    ErrorMessage = row.Cells["Direct Manager"].Value.ToString() + "This Direct MAnager Is Not Manager";
                                    IsValid = false;

                                }
                            }
                         else   if (ManagerName == "NULL")
                            {
                                managerID = null;

                            }
                            else
                            {

                                ErrorMessage = row.Cells["Direct Manager"].Value.ToString() + "This Direct MAnager Is Not Exist As User";
                                IsValid = false;


                            }



                            //Add Addtiondal Data
                            if (IsValid == true)
                            {
                                string UserName = row.Cells["User Name"].Value.ToString();
                                if (HRMS.Users.Where(x => x.LDAPUserName == UserName).Count() == 1)
                                {
                                    UserCompletInformations = HRMS.Users.Where(x => x.LDAPUserName == UserName).FirstOrDefault();

                                    UserCompletInformations.HireDate = DateTime.ParseExact(row.Cells["HireDate"].Value.ToString(), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                                    UserCompletInformations.BirthDate = DateTime.ParseExact(row.Cells["BirthDate"].Value.ToString(), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                                    UserCompletInformations.MedicalID = row.Cells["Medical ID"].Value.ToString();
                                    UserCompletInformations.PhoneNumber = "0" + row.Cells["Phone Number"].Value.ToString();
                                    UserCompletInformations.ArName = row.Cells["Arabic Name"].Value.ToString();
                                    UserCompletInformations.NationalID = row.Cells["National ID"].Value.ToString();
                                    UserCompletInformations.PositionFK = positionID;
                                    UserCompletInformations.SubDepartmentFK = subDepartmentID;
                                    UserCompletInformations.DirectManagerFK = managerID;
                                    UserCompletInformations.DepartmentFK = departmentID;
                                    UserCompletInformations.PositionFK = positionID;
                                    UserCompletInformations.GenderFK = (int)(Gender)Enum.Parse(typeof(Gender), row.Cells["Gender"].Value.ToString());
                                    UserCompletInformations.ContractTypeFK = (int)(ContractName)Enum.Parse(typeof(ContractName), row.Cells["Contract Type"].Value.ToString());
                                    UserCompletInformations.CompanyFK = (int)(CompanyName)Enum.Parse(typeof(CompanyName), row.Cells["Company Name"].Value.ToString());
                                    UserCompletInformations.AccessControlUserFK = Convert.ToInt32(row.Cells["User ID"].Value.ToString());

                                    HRMS.Users.Add(UserCompletInformations);
                                    HRMS.SaveChanges();



                                    UserEntitlement UserEntitlementAnnual = new UserEntitlement
                                    {
                                        UserFK = UserCompletInformations.UserID,
                                        CreationDate = DateTime.Now,
                                        EntitlementTotal = Convert.ToDecimal(row.Cells["Annual vacation"].Value.ToString()),
                                        LeaveTypeFK = 5,
                                        LeaveDurationUnitFK = 1,
                                        IsActive = true,
                                        IsDeleted = false,
                                    };
                                    HRMS.UserEntitlements.Add(UserEntitlementAnnual);
                                    HRMS.SaveChanges();

                                    UserEntitlementChange UserEntitlementChangeAnnual = new UserEntitlementChange
                                    {
                                        ActionDate = DateTime.Now,
                                        Comment = "Opening Entitlement On System",
                                        CreationDate = DateTime.Now,
                                        EntitlementBefore = 0,
                                        EntitlementAfter = Convert.ToDecimal(row.Cells["Annual vacation"].Value.ToString()),
                                        IsActive = true,
                                        UserEntitlementFK = UserEntitlementAnnual.UserEntitlementID,
                                        DurationFrom = DateTime.Now,
                                        DurationTo = DateTime.Now,
                                        BackToWork = DateTime.Now,
                                        RequestDuration = 0,
                                        UserFK = UserCompletInformations.UserID,
                                        EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                                        LeaveDurationUnitFK = 1

                                    };
                                    HRMS.UserEntitlementChanges.Add(UserEntitlementChangeAnnual);
                                    HRMS.SaveChanges();





                                    UserEntitlement UserEntitlementCasual = new UserEntitlement
                                    {
                                        UserFK = UserCompletInformations.UserID,
                                        CreationDate = DateTime.Now,
                                        EntitlementTotal = Convert.ToDecimal(row.Cells["Casual vacation"].Value.ToString()),
                                        LeaveTypeFK = 6,
                                        LeaveDurationUnitFK = 1,
                                        IsActive = true,
                                        IsDeleted = false,
                                    };
                                    HRMS.UserEntitlements.Add(UserEntitlementCasual);
                                    HRMS.SaveChanges();

                                    UserEntitlementChange UserEntitlementChangeCasual = new UserEntitlementChange
                                    {
                                        ActionDate = DateTime.Now,
                                        Comment = "Opening Entitlement On System",
                                        CreationDate = DateTime.Now,
                                        EntitlementBefore = 0,
                                        EntitlementAfter = Convert.ToDecimal(row.Cells["Casual vacation"].Value.ToString()),
                                        IsActive = true,
                                        UserEntitlementFK = UserEntitlementCasual.UserEntitlementID,
                                        DurationFrom = DateTime.Now,
                                        DurationTo = DateTime.Now,
                                        BackToWork = DateTime.Now,
                                        RequestDuration = 0,
                                        UserFK = UserCompletInformations.UserID,
                                        EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                                        LeaveDurationUnitFK = 1,






                                    };
                                    HRMS.UserEntitlementChanges.Add(UserEntitlementChangeCasual);
                                    HRMS.SaveChanges();



                                    UserEntitlement UserEntitlementTotalExcuse = new UserEntitlement
                                    {
                                        UserFK = UserCompletInformations.UserID,
                                        CreationDate = DateTime.Now,
                                        EntitlementTotal = Convert.ToDecimal(row.Cells["Total Excuse"].Value.ToString()),
                                        LeaveTypeFK = 7,
                                        LeaveDurationUnitFK = 2,
                                        IsActive = true,
                                        IsDeleted = false,
                                    };
                                    HRMS.UserEntitlements.Add(UserEntitlementTotalExcuse);
                                    HRMS.SaveChanges();

                                    UserEntitlementChange UserEntitlementChangeTotalExcuse = new UserEntitlementChange
                                    {
                                        ActionDate = DateTime.Now,
                                        Comment = "Opening Entitlement On System",
                                        CreationDate = DateTime.Now,
                                        EntitlementBefore = 0,
                                        EntitlementAfter = Convert.ToDecimal(row.Cells["Total Excuse"].Value.ToString()),
                                        IsActive = true,
                                        UserEntitlementFK = UserEntitlementAnnual.UserEntitlementID,
                                        DurationFrom = DateTime.Now,
                                        DurationTo = DateTime.Now,
                                        BackToWork = DateTime.Now,
                                        RequestDuration = 0,
                                        UserFK = UserCompletInformations.UserID,
                                        EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                                        LeaveDurationUnitFK = 2

                                    };
                                    HRMS.UserEntitlementChanges.Add(UserEntitlementChangeTotalExcuse);
                                    HRMS.SaveChanges();


                                }


                            }
                            else
                            {
                                row.Cells[19].Value = ErrorMessage;
                                FaildTransactionTable.Rows.Add(
                                    row.Cells[0].Value,
                                    row.Cells[1].Value,
                                    row.Cells[2].Value,
                                    row.Cells[3].Value,
                                    row.Cells[4].Value,
                                     row.Cells[5].Value,
                                    row.Cells[6].Value,
                                       row.Cells[7].Value,
                                     row.Cells[8].Value,
                                    row.Cells[9].Value,
                                    row.Cells[10].Value,
                                     row.Cells[11].Value,
                                    row.Cells[12].Value,
                                       row.Cells[13].Value,
                                     row.Cells[14].Value,
                                    row.Cells[15].Value,
                                    row.Cells[16].Value,
                                     row.Cells[17].Value,
                                    row.Cells[18].Value,
                                       row.Cells[19].Value
                                    // row.Cells[20].Value
                                    //row.Cells[21].Value
                                    //row.Cells[22].Value,
                                    // row.Cells[23].Value,
                                    // row.Cells[24].Value


                                    );


                            }
                        






                        }
                        catch (Exception ex)
                        {


                        }
                     
                    }
                    var workbook = excel.Workbook.Worksheets;

                    workbook.Add("Worksheet1");

                    ExcelWorksheet worksheet = workbook["Worksheet1"];
                    worksheet.Cells[1, 1].LoadFromDataTable(FaildTransactionTable, true);
                    FileInfo excelFile = new FileInfo(@"E:\test.xlsx");
                    excel.SaveAs(excelFile);
                }

            }
        }

       
        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            try
            {

                string filePath = openFileDialog2.FileName;
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

                                ExcelDataOutDomain.DataSource = datatable1;


                            }
                        }
                    }

                    //Get the name of the First Sheet.


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

        private void SaveNotLdap_Click(object sender, EventArgs e)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                // Add User Data Only

                DataTable FaildTransactionTable = new DataTable();

                FaildTransactionTable.Columns.Add("User Name", typeof(string));
                FaildTransactionTable.Columns.Add("User ID", typeof(string));
                FaildTransactionTable.Columns.Add("HireDate", typeof(string));
                FaildTransactionTable.Columns.Add("Department", typeof(string));
                FaildTransactionTable.Columns.Add("SubDepartment", typeof(string));
                FaildTransactionTable.Columns.Add("Team Manager", typeof(string));
                FaildTransactionTable.Columns.Add("Direct Manager", typeof(string));
                FaildTransactionTable.Columns.Add("Arabic Name", typeof(string));
                FaildTransactionTable.Columns.Add("Position Name", typeof(string));
                FaildTransactionTable.Columns.Add("Gender", typeof(string));
                FaildTransactionTable.Columns.Add("BirthDate", typeof(string));
                FaildTransactionTable.Columns.Add("Phone Number", typeof(string));
                FaildTransactionTable.Columns.Add("Annual vacation", typeof(string));
                FaildTransactionTable.Columns.Add("Casual vacation", typeof(string));
                FaildTransactionTable.Columns.Add("Total Excuse", typeof(string));


                //FaildTransactionTable.Columns.Add("Seniority Before Hire Years", typeof(string));
                //FaildTransactionTable.Columns.Add("Seniority Before Hire Month", typeof(string));
                FaildTransactionTable.Columns.Add("Medical ID", typeof(string));
                FaildTransactionTable.Columns.Add("National ID", typeof(string));
                // FaildTransactionTable.Columns.Add("Address", typeof(string));
                FaildTransactionTable.Columns.Add("Company Name", typeof(string));

                FaildTransactionTable.Columns.Add("Contract Type", typeof(string));
                FaildTransactionTable.Columns.Add("Reason", typeof(string));






                User TeamManager = new User();
                SubDepartment subDepartment = new SubDepartment();
                Department department = new Department();
                Position position = new Position();
                ContractTypeDIM contractType = new ContractTypeDIM();
                GenderTypeDIM genderType = new GenderTypeDIM();
                Manager manager = new Manager();
                //Forign Keys
                int? subDepartmentID = 0;
                int? departmentID = 0;
                int? positionID = 0;
                int? managerID = 0;

                using (ExcelPackage excel = new ExcelPackage())
                {


                    // Target a worksheet

                    // Popular header row data

                    foreach (DataGridViewRow row in ExcelDataOutDomain.Rows)
                    {

                        try
                        {
                            bool IsValid = true;
                            string ErrorMessage = "";
                            //Add Required data For Users
                            string DepartmentName = row.Cells["Department"].Value.ToString();
                            if (HRMS.Departments.Where(x => x.DepartmentName == DepartmentName).Count() > 0)
                            {
                                var z = HRMS.Departments.Where(x => x.DepartmentName == DepartmentName).FirstOrDefault();

                                departmentID = HRMS.Departments.Where(x => x.DepartmentName == DepartmentName).FirstOrDefault().DepartmentID;

                            }
                            else if (DepartmentName == "NULL")
                            {
                                departmentID = null;

                            }
                            else
                            {
                                ErrorMessage = row.Cells["Department"].Value.ToString() + " Department Not Exist ,";
                                IsValid = false;

                            }

                            string SubDepartmentName = row.Cells["SubDepartment"].Value.ToString();
                            if (HRMS.SubDepartments.Where(x => x.SubDepartmentName == SubDepartmentName && x.DepartmentFK == departmentID).Count() > 0)
                            {


                                subDepartmentID = HRMS.SubDepartments.Where(x => x.SubDepartmentName == SubDepartmentName).FirstOrDefault().SubDepartmentID;

                            }
                            else if (SubDepartmentName == "NULL")
                            {
                                subDepartmentID = null;

                            }
                            else
                            {


                                ErrorMessage = SubDepartmentName + " SubDepartment Not Exist,";
                                IsValid = false;
                            }
                            string PositionName = row.Cells["Position Name"].Value.ToString();
                            if (HRMS.Positions.Where(x => x.PositionName == PositionName).Count() > 0)
                            {
                                positionID = HRMS.Positions.Where(x => x.PositionName == PositionName).FirstOrDefault().PositionID;



                            }
                            else if (PositionName == "NULL")
                            {
                                positionID = null;

                            }
                            else
                            {
                                ErrorMessage = row.Cells["Position Name"].Value.ToString() + " Postion Not Exist ,";
                                IsValid = false;
                            }


                            string ManagerName = row.Cells["Direct Manager"].Value.ToString();
                            if (HRMS.Users.Where(x => x.LDAPUserName == ManagerName).Count() > 0)
                            {
                                int managerUserID = HRMS.Users.Where(x => x.LDAPUserName == ManagerName).FirstOrDefault().UserID;
                                if (HRMS.Managers.Where(x => x.ManagerUserFK == managerUserID).Count() > 0)
                                {

                                    managerID = HRMS.Managers.Where(x => x.ManagerUserFK == managerUserID).FirstOrDefault().ManagerID;
                                }
                                else
                                {
                                    ErrorMessage = row.Cells["Direct Manager"].Value.ToString() + "This Direct MAnager Is Not Manager";
                                    IsValid = false;

                                }
                            }
                            else if (ManagerName == "NULL")
                            {
                                managerID = null;

                            }
                            else
                            {

                                ErrorMessage = row.Cells["Direct Manager"].Value.ToString() + "This Direct MAnager Is Not Exist As User";
                                IsValid = false;


                            }



                            //Add Addtiondal Data
                            if (IsValid == true)
                            {
                                string UserName = row.Cells["User Name"].Value.ToString();
                                if (HRMS.Users.Where(x => x.LDAPUserName == UserName).Count() == 0)
                                {
                                    User UserCompletInformations = new User();


                                    UserCompletInformations.HireDate = DateTime.ParseExact(row.Cells["HireDate"].Value.ToString(), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                                    UserCompletInformations.BirthDate = DateTime.ParseExact(row.Cells["BirthDate"].Value.ToString(), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                                    UserCompletInformations.MedicalID = row.Cells["Medical ID"].Value.ToString();
                                    UserCompletInformations.PhoneNumber = "0" + row.Cells["Phone Number"].Value.ToString();
                                    UserCompletInformations.ArName = row.Cells["Arabic Name"].Value.ToString();
                                    UserCompletInformations.NationalID = row.Cells["National ID"].Value.ToString();
                                    UserCompletInformations.PositionFK = positionID;
                                    UserCompletInformations.SubDepartmentFK = subDepartmentID;
                                    UserCompletInformations.DirectManagerFK = managerID;
                                    UserCompletInformations.DepartmentFK = departmentID;
                                    UserCompletInformations.PositionFK = positionID;
                                    UserCompletInformations.GenderFK = (int)(Gender)Enum.Parse(typeof(Gender), row.Cells["Gender"].Value.ToString());
                                    UserCompletInformations.ContractTypeFK = (int)(ContractName)Enum.Parse(typeof(ContractName), row.Cells["Contract Type"].Value.ToString());
                                    UserCompletInformations.CompanyFK = (int)(CompanyName)Enum.Parse(typeof(CompanyName), row.Cells["Company Name"].Value.ToString());
                                    UserCompletInformations.AccessControlUserFK = Convert.ToInt32(row.Cells["User ID"].Value.ToString());
                                    UserCompletInformations.LDAPUserName = UserName;
                                    UserCompletInformations.UserName = UserName.Replace(".", " ");
                                    UserCompletInformations.IsOutDomainUser = true;
                                    UserCompletInformations.OutDomainPassword = "prime@123";
                                    UserCompletInformations.IsActive = true;
                                    HRMS.Users.Add(UserCompletInformations);
                                    HRMS.SaveChanges();



                                    UserEntitlement UserEntitlementAnnual = new UserEntitlement
                                    {
                                        UserFK = UserCompletInformations.UserID,
                                        CreationDate = DateTime.Now,
                                        EntitlementTotal = Convert.ToDecimal(row.Cells["Annual vacation"].Value.ToString()),
                                        LeaveTypeFK = 5,
                                        LeaveDurationUnitFK = 1,
                                        IsActive = true,
                                        IsDeleted = false,
                                    };
                                    HRMS.UserEntitlements.Add(UserEntitlementAnnual);
                                    HRMS.SaveChanges();

                                    UserEntitlementChange UserEntitlementChangeAnnual = new UserEntitlementChange
                                    {
                                        ActionDate = DateTime.Now,
                                        Comment = "Opening Entitlement On System",
                                        CreationDate = DateTime.Now,
                                        EntitlementBefore = 0,
                                        EntitlementAfter = Convert.ToDecimal(row.Cells["Annual vacation"].Value.ToString()),
                                        IsActive = true,
                                        UserEntitlementFK = UserEntitlementAnnual.UserEntitlementID,
                                        DurationFrom = DateTime.Now,
                                        DurationTo = DateTime.Now,
                                        BackToWork = DateTime.Now,
                                        RequestDuration = 0,
                                        UserFK = UserCompletInformations.UserID,
                                        EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                                        LeaveDurationUnitFK = 1

                                    };
                                    HRMS.UserEntitlementChanges.Add(UserEntitlementChangeAnnual);
                                    HRMS.SaveChanges();





                                    UserEntitlement UserEntitlementCasual = new UserEntitlement
                                    {
                                        UserFK = UserCompletInformations.UserID,
                                        CreationDate = DateTime.Now,
                                        EntitlementTotal = Convert.ToDecimal(row.Cells["Casual vacation"].Value.ToString()),
                                        LeaveTypeFK = 6,
                                        LeaveDurationUnitFK = 1,
                                        IsActive = true,
                                        IsDeleted = false,
                                    };
                                    HRMS.UserEntitlements.Add(UserEntitlementCasual);
                                    HRMS.SaveChanges();

                                    UserEntitlementChange UserEntitlementChangeCasual = new UserEntitlementChange
                                    {
                                        ActionDate = DateTime.Now,
                                        Comment = "Opening Entitlement On System",
                                        CreationDate = DateTime.Now,
                                        EntitlementBefore = 0,
                                        EntitlementAfter = Convert.ToDecimal(row.Cells["Casual vacation"].Value.ToString()),
                                        IsActive = true,
                                        UserEntitlementFK = UserEntitlementCasual.UserEntitlementID,
                                        DurationFrom = DateTime.Now,
                                        DurationTo = DateTime.Now,
                                        BackToWork = DateTime.Now,
                                        RequestDuration = 0,
                                        UserFK = UserCompletInformations.UserID,
                                        EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                                        LeaveDurationUnitFK = 1,






                                    };
                                    HRMS.UserEntitlementChanges.Add(UserEntitlementChangeCasual);
                                    HRMS.SaveChanges();



                                    UserEntitlement UserEntitlementTotalExcuse = new UserEntitlement
                                    {
                                        UserFK = UserCompletInformations.UserID,
                                        CreationDate = DateTime.Now,
                                        EntitlementTotal = Convert.ToDecimal(row.Cells["Total Excuse"].Value.ToString()),
                                        LeaveTypeFK = 7,
                                        LeaveDurationUnitFK = 2,
                                        IsActive = true,
                                        IsDeleted = false,
                                    };
                                    HRMS.UserEntitlements.Add(UserEntitlementTotalExcuse);
                                    HRMS.SaveChanges();

                                    UserEntitlementChange UserEntitlementChangeTotalExcuse = new UserEntitlementChange
                                    {
                                        ActionDate = DateTime.Now,
                                        Comment = "Opening Entitlement On System",
                                        CreationDate = DateTime.Now,
                                        EntitlementBefore = 0,
                                        EntitlementAfter = Convert.ToDecimal(row.Cells["Total Excuse"].Value.ToString()),
                                        IsActive = true,
                                        UserEntitlementFK = UserEntitlementAnnual.UserEntitlementID,
                                        DurationFrom = DateTime.Now,
                                        DurationTo = DateTime.Now,
                                        BackToWork = DateTime.Now,
                                        RequestDuration = 0,
                                        UserFK = UserCompletInformations.UserID,
                                        EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                                        LeaveDurationUnitFK = 2

                                    };
                                    HRMS.UserEntitlementChanges.Add(UserEntitlementChangeTotalExcuse);
                                    HRMS.SaveChanges();


                                }


                            }
                            else
                            {
                                row.Cells[19].Value = ErrorMessage;
                                FaildTransactionTable.Rows.Add(
                                    row.Cells[0].Value,
                                    row.Cells[1].Value,
                                    row.Cells[2].Value,
                                    row.Cells[3].Value,
                                    row.Cells[4].Value,
                                     row.Cells[5].Value,
                                    row.Cells[6].Value,
                                       row.Cells[7].Value,
                                     row.Cells[8].Value,
                                    row.Cells[9].Value,
                                    row.Cells[10].Value,
                                     row.Cells[11].Value,
                                    row.Cells[12].Value,
                                       row.Cells[13].Value,
                                     row.Cells[14].Value,
                                    row.Cells[15].Value,
                                    row.Cells[16].Value,
                                     row.Cells[17].Value,
                                    row.Cells[18].Value,
                                       row.Cells[19].Value
                                    // row.Cells[20].Value
                                    //row.Cells[21].Value
                                    //row.Cells[22].Value,
                                    // row.Cells[23].Value,
                                    // row.Cells[24].Value


                                    );


                            }







                        }
                        catch (Exception ex)
                        {


                        }

                    }
                    var workbook = excel.Workbook.Worksheets;

                    workbook.Add("Worksheet1");

                    ExcelWorksheet worksheet = workbook["Worksheet1"];
                    worksheet.Cells[1, 1].LoadFromDataTable(FaildTransactionTable, true);
                    FileInfo excelFile = new FileInfo(@"E:\test.xlsx");
                    excel.SaveAs(excelFile);
                }

            }
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void BrowseOutDomain_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();


        }
    }
}
