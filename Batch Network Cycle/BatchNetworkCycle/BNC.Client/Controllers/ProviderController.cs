using BNC.DTOs.Business;
using BNC.Entities;
using BNC.Utilities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static BNC.BLL.StaticData.StaticEnums;
using Excel = Microsoft.Office.Interop.Excel;

namespace BNC.Client.Controllers
{
    public class ProviderController : BaseController
    {
        // GET: Receiving

        public ActionResult Index()
        {

            return View();
        }
        
        public ActionResult AddProviderRequest()
        {

            return View();
        }
        public ActionResult SerachRequestsForLocked()
        {
            return View();
        }
        public ActionResult ViewLockRequests()
        {
            return View();
        }
        public ActionResult AddProvidersData()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LockUnLockRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            Exception exOutputSubmit;
            ResultViewModel Result;

            try
            {
                UnitOfWork.SharedProviderBLL.LockUnLockRequest(searchCriteriaDTO);
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true
                    };
                }
                else
                {
                    ExceptionHandlerConstants.CreatBNCException(exOutputSubmit, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    Result = new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRecievingRequestsByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetRecievingRequestsByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetFilterationRequestsByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetFilterationRequestsByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBatchRequestsByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetBatchRequestsByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }       
        [HttpPost]
        public JsonResult GetBatchAuditRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetBatchAuditRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMedicalAuditRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetMedicalAuditRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetFinancialAuditRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetFinancialAuditRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMIAuditRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetMIAuditRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetReconciliationAuditRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetReconciliationAuditRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDataEntryAdminstrationRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetDataEntryAdminstrationRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDataEntryBatchAssigningRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetDataEntryBatchAssigningRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBatchClosingRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetBatchClosingRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetClosedBatchReAdministrationRequestByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetClosedBatchReAdministrationRequestByStatus(searchCriteriaDTO),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetLockRequests()
        {
            ResultViewModel result;
            try
            {
                result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedProviderBLL.GetLockRequests(),
                    Message = "Success Get Data",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false,
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //-------------------------------------------
        public JsonResult CheckLockedRequest(EntityRequestDTO EntityRequestDTO)
        {
            var result=UnitOfWork.SharedBncBLL.CheckLockedRequest(EntityRequestDTO);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------
        public ActionResult searchSpecialApprovalReq()
        {
            ViewBag.ProvidersFK = new SelectList(UnitOfWork.ProvidersBLL.getProvidersData(), "ProviderID", "ProviderName");
            ViewBag.UsersFK = new SelectList(UnitOfWork.UserBLL.GetUsersData(), "UserID", "UserName");
            ViewBag.SpReasonsFK = new SelectList(UnitOfWork.SPReasonBLL.GetSpReasonsData(), "SpReasonsID", "SpReasonsName");
            ViewBag.SpStatutsFK = new SelectList(UnitOfWork.SPStatusBLL.GetSpStatutsData(), "SpStatutsID", "SpStatutsName");
            return View();
        }
        //-----------------------------------------------
        [HttpPost]
        public JsonResult searchSpecialApprovalReq(SpecialApprovalDTO specialApprovalDTO)
        {
            ResultViewModel res;
            try
            {
                res = new ResultViewModel
                {
                    Success = true,
                    Message = "Success Search And Get Data ",
                    Data = UnitOfWork.SPRequestBLL.searchSpecialApprovalReq(specialApprovalDTO),
                };
            }
            catch(Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                res = new ResultViewModel
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null,
                };
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------------------
        [HttpPost]
        public JsonResult GetSpecialApprovalInfoByID(SPReqestDTO spRequest)
        {
            ResultViewModel res=new ResultViewModel();

            if (spRequest.EntrityFK==(int)Entity.FinancialAuditRequest)
            {
                try
                {
                    res = new ResultViewModel
                    {
                        Success = true,
                        Message = "Success Search And Get Data info about special approval Finincial",
                        Data = UnitOfWork.SpecialApprovalFinincalReqBLL.GetSpecialApprovalInfoByID(spRequest.SPReqID),
                    };
                }
                catch (Exception ex)
                {
                    res = new ResultViewModel
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null,
                    };
                }
            }

           else if (spRequest.EntrityFK == (int)Entity.MedicalAuditRequest)
            {
                try
                {
                    res = new ResultViewModel
                    {
                        Success = true,
                        Message = "Success Search And Get Data info about special approval Medical",
                        Data = UnitOfWork.SpecialApprovalMedicalReqBLL.GetSpecialApprovalInfoByID(spRequest.SPReqID),
                    };
                }
                catch (Exception ex)
                {
                    res = new ResultViewModel
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null,
                    };
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);

        }
        //-----------------------------------------------
        [HttpPost]
        public HttpResponseMessage ExtractProvidersTemplateExcel()
        {
            List<UserDTO> usersNotHaveShiftsDTO = new List<UserDTO>();

            //var shiftFrom = DateTime.ParseExact(extractWorkingShiftDTO.ShiftFrom, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date;
            //var shiftTo = DateTime.ParseExact(extractWorkingShiftDTO.ShiftTo, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Provider English Name");
            dataTable.Columns.Add("Provider Arabic Name");
            dataTable.Columns.Add("Provider Type");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Phone");
            dataTable.Columns.Add("MCA Provider Pin");
            dataTable.Columns.Add("IbnSina Provider Pin");            
            dataTable.Columns.Add("Government");
            dataTable.Columns.Add("Effective Date");
            dataTable.Columns.Add("Stopped Date");

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dataTable, "Provider Data Template");
            wb.AddWorksheet(UnitOfWork.ProviderTypeBLL.GetAllProviderTypes(), "Provider Types Details");
            wb.AddWorksheet(UnitOfWork.GovernmentBLL.GetAllGovernmentToDataTable(), "Government Details");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            return result;
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

                var request = System.Web.HttpContext.Current.Request;
                var filePath = SavePath + request.Headers["filename"];
                using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                {
                    request.InputStream.CopyTo(fs);
                }

                //result = ExcelParsing(filePath, int.Parse(request.Headers["loggedUserID"]));

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                result = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                result.Content = new StringContent(ex.Message);

                return result;
            }
        }
        //----------------------------------------------------------------------
        [HttpPost]
        public JsonResult ChangeSpRequestLifeCycle(SpAuditUserRequest spAuditUserRequest)
        {
            UserDTO User = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value);
            Exception outException;
            ResultViewModel res;
            int SpUserFk = UnitOfWork.SpUserBLL.GetSpUserId(User.UserID);
            spAuditUserRequest.SpUserFk = SpUserFk;
            if(SpUserFk!=-1)
            {
                SPRequestUser sPRequestUser = UnitOfWork.SPRequestBLL.ChangeSpRequestLifeCycle(spAuditUserRequest);
                try
                {
                    if (UnitOfWork.Submit(out outException))
                    {
                        res = new ResultViewModel
                        {
                            Success = true,
                            Message = "Success  Add Audit and User Request with Id:" + sPRequestUser.SPReqUserID,
                            Data = null,
                        };
                    }
                    else
                    {
                        ExceptionHandlerConstants.CreatBNCException(outException, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");
                        res = new ResultViewModel
                        {
                            Success = false,
                            Message = outException.Message,
                            Data = null,
                        };
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                    res = new ResultViewModel
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null,
                    };
                }
            }
            else
            {

                res = new ResultViewModel
                {
                    Success = false,
                    Message = "the user with id "+ User.UserID+" Can't have acess to edit claims count",
                    Data = null,
                };
            }

            return Json(res, JsonRequestBehavior.AllowGet);

        }
        //----------------------------------------------------------------------
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

            //List<NewShiftForUserDTO> newShiftForUserList = new List<NewShiftForUserDTO>();

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 2; i <= rowCount; i++)
            {
                int userID = int.Parse(xlRange.Cells[i, 1].Value2.ToString());

                for (int j = 3; j <= colCount; j++)
                {
                    //NewShiftForUserDTO newShiftForUser = new NewShiftForUserDTO();
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

                        //newShiftForUserList.Add(new NewShiftForUserDTO
                        //{
                        //    UserID = userID,
                        //    ShiftDate = shiftDate,
                        //    ShiftName = shiftName,
                        //    LoggedUserID = loggedUserID
                        //});
                    }
                }
            }
            try
            {
                //check = UnitOfWork.WorkingShiftUserDailyBLL.CheckIfInsertNewShift(newShiftForUserList);

                if (check)
                {
                    //UnitOfWork.WorkingShiftUserDailyBLL.AddNewShiftForUser(newShiftForUserList);

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

    }
}