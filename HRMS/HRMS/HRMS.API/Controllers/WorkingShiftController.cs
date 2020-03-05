using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using HRMS.BLL.UnitOfWork;
using HRMS.DTOs;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static HRMS.BLL.StaticData.StaticEnums;
using Excel = Microsoft.Office.Interop.Excel;

namespace HRMS.API.Controllers
{
    public class WorkingShiftController : BaseController
    {
        [HttpGet]
        public ResultViewModel GetAllWorkingShift()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.WorkingShiftBLL.GetAllWorkingShift().ToList(),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        [HttpGet]
        public ResultViewModel GetAllActiveWorkingShift()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.WorkingShiftBLL.GetAllActiveWorkingShift().ToList(),
                    Message = "Success",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }

        [HttpPost]
        public ResultViewModel AddNewWorkingShift(WorkingShiftDTO workingShiftDTO)
        {
            Exception exOutputSubmit;
            try
            {
                UnitOfWork.WorkingShiftBLL.AddNewWorkingShift(workingShiftDTO);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }

        [HttpPost]
        public ResultViewModel EditWorkingShift(WorkingShiftDTO workingShiftDTO)
        {
            Exception exOutputSubmit;
            try
            {
                UnitOfWork.WorkingShiftUserDailyBLL.CheckAssignedShifts(workingShiftDTO.ShiftID);
                UnitOfWork.WorkingShiftBLL.EditWorkingShift(workingShiftDTO);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        [HttpGet]
        public ResultViewModel GetWorkingShift(int ID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.WorkingShiftBLL.GetWorkingShift(ID),
                    Message = "Success",
                    Success = true

                }; 
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        [HttpPost]
        public ResultViewModel DisapleWorkingShift(int ID)
        {
            Exception exOutputSubmit;
            try
            {
                UnitOfWork.WorkingShiftBLL.DisapleWorkingShift(ID);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        private List<String> CheckUserShiftNotExist(int UserID, DateTime ShiftStartDate, DateTime ShiftEndDate)
        {
            return UnitOfWork.WorkingShiftUserDailyBLL.CheckUserShiftExist(UserID, ShiftStartDate, ShiftEndDate);
        }

        public DataTable ExtractShiftsNameExcel(int LoggedUserID)
        {
            DataTable dataTable = new DataTable();
            List<WorkingShiftDTO> workingShiftList = UnitOfWork.WorkingShiftBLL.GetAllActiveWorkingShift(LoggedUserID);

            dataTable.Columns.Add("Shift Name");
            dataTable.Columns.Add("Shift Start");
            dataTable.Columns.Add("Shift End");
            dataTable.Columns.Add("Shift Grace In");
            dataTable.Columns.Add("Shift Grace Out");

            foreach (var items in workingShiftList)
            {
                dataTable.Rows.Add(items.ShiftName, items.ShiftStart, items.ShiftEnd, items.GraceIn, items.GraceOut);
            }
            return dataTable;
        }

        [HttpPost]
        public HttpResponseMessage ExtractShiftsTemplateExcel(ExtractWorkingShiftDTO extractWorkingShiftDTO)
        {
            List<UserDTO> usersNotHaveShiftsDTO = new List<UserDTO>();

            var shiftFrom = DateTime.ParseExact(extractWorkingShiftDTO.ShiftFrom, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date;
            var shiftTo = DateTime.ParseExact(extractWorkingShiftDTO.ShiftTo, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date;

            List<UserDTO> usersDTO = UnitOfWork.ApprovalFlowBLL.GetUserApprovedByManager(extractWorkingShiftDTO.ManagerID);
            List<UserDTO> activeUsersDTO = usersDTO.Where(x => x.WorkingModeID == (int)WorkingMode.Shift && x.IsActive == true).ToList();

            foreach(var items in activeUsersDTO)
            {
                int count = CheckUserShiftNotExist(items.UserID, shiftFrom, shiftTo).Count;
                //if (CheckUserShiftNotExist(items.UserID, shiftFrom, shiftTo))
                {
                    usersNotHaveShiftsDTO.Add( new UserDTO {
                        UserName = items.UserName,
                        UserID = items.UserID,
                        UserShiftNames = CheckUserShiftNotExist(items.UserID, shiftFrom, shiftTo)
                    });
                }
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Cypress ID");
            dataTable.Columns.Add("User Name");

            while (shiftFrom.CompareTo(shiftTo) <= 0)
            {
                dataTable.Columns.Add(shiftFrom.Date.ToString("dd/MM/yyyy"));
                shiftFrom = shiftFrom.AddDays(1);
            }

            foreach (var items in usersNotHaveShiftsDTO)
            {
                DataRow workRow = dataTable.NewRow();
                workRow[0] = items.UserID;
                workRow[1] = items.UserName;
                for(int i =0; i < items.UserShiftNames.Count; i++)
                {
                    workRow[i+2] = items.UserShiftNames[i];
                }
                dataTable.Rows.Add(workRow);
            }

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dataTable, "Shifts Template");
            wb.AddWorksheet(ExtractShiftsNameExcel(extractWorkingShiftDTO.ManagerID), "Shifts Details");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = "UsersShiftsTemplate" + DateTime.Now.ToString() + ".xlsx"
            //};
            return result;
        }

        [HttpPost]
        public string FileUpload()
        {
            var request = HttpContext.Current.Request;
            var filePath = "C:\\temp\\" + request.Headers["filename"];
            using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                request.InputStream.CopyTo(fs);
            }
            return "uploaded";
        }

        [HttpPost]
        public HttpResponseMessage MyFileUpload()
        {
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                List<string> DbPath = new List<string>();
                string SavePath = ConfigurationManager.AppSettings["RequestAttachment"] + @"\ShiftsAttachmentsUploaded\";
                SavePath = SavePath.Replace(@"\", "/");

                if (!Directory.Exists(SavePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(SavePath);
                }

                var request = HttpContext.Current.Request;
                var filePath = SavePath + request.Headers["filename"];
                using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    request.InputStream.CopyTo(fs);
                }

                result = ExcelParsing(filePath, int.Parse(request.Headers["loggedUserID"]));

                return result;

                //request.InputStream.CopyTo();

                //for (int i = 0; i < Request.Files.Count; i++)
                //{

                //    var file = Request.Files[i];
                //    string Path2 = SavePath + file.FileName;
                //    file.SaveAs(Path2);
                //    DbPath.Add("/RequestsAttachments/" + RequestID + "/" + file.FileName);

                //}
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                result = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                result.Content = new StringContent(ex.Message);

                return result;
            }
        }

        public HttpResponseMessage ExcelParsing(string fullpath, int loggedUserID)
        {
            string data = "";
            Exception exOutputSubmit;
            bool check = false;
            HttpResponseMessage result = new HttpResponseMessage();
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullpath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            List<NewShiftForUserDTO> newShiftForUserList = new List<NewShiftForUserDTO>();

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 2; i <= rowCount; i++)
            {
                int userID = int.Parse(xlRange.Cells[i, 1].Value2.ToString());

                for (int j = 3; j <= colCount; j++)
                {
                    NewShiftForUserDTO newShiftForUser = new NewShiftForUserDTO();
                    var shiftDate = DateTime.ParseExact(xlRange.Cells[1, j].Value2.ToString(), @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date;
                    //string shiftName = "";

                    //either collect data cell by cell or DO you job like insert to DB 
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        string shiftName = xlRange.Cells[i, j].Value2.ToString();

                        //newShiftForUser.UserID = userID;
                        //newShiftForUser.ShiftDate = shiftDate;
                        //newShiftForUser.ShiftName = shiftName;
                        //newShiftForUser.LoggedUserID = loggedUserID;

                        newShiftForUserList.Add(new NewShiftForUserDTO
                        {
                            UserID = userID,
                            ShiftDate = shiftDate,
                            ShiftName = shiftName,
                            LoggedUserID = loggedUserID
                        });
                    }                                          
                }  
            }
            try
            {
                check = UnitOfWork.WorkingShiftUserDailyBLL.CheckIfInsertNewShift(newShiftForUserList);

                if (check)
                {
                    UnitOfWork.WorkingShiftUserDailyBLL.AddNewShiftForUser(newShiftForUserList);

                    xlWorkbook.Close();

                    if (!UnitOfWork.Submit(out exOutputSubmit) && check)
                    {
                        xlWorkbook.Close();

                        throw exOutputSubmit;
                    }

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    xlWorkbook.Close();

                    result = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    result.Content = new StringContent("These users already have Shifts");
                    return result;
                }
            }
            catch (Exception ex)
            {
                xlWorkbook.Close();

                result = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                result.Content = new StringContent(ex.Message);
                return result;
            }
        }

        [HttpGet]
        public ResultViewModel GetUserShiftName(int UserID, string ShiftDate)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.WorkingShiftUserDailyBLL.GetUserShiftName(UserID, ShiftDate),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        [HttpPost]
        public ResultViewModel SwapShift(SwapShiftDTO swapShiftDTO)
        {
            Exception exOutputSubmit;
            try
            {
                UnitOfWork.WorkingShiftUserDailyBLL.SwapShift(swapShiftDTO);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Swap Shift Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
    }
}