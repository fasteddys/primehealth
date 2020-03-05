using System.Web;
using System.Web.Http;
using HRMS.Entities;
using HRMS.DTOs.Business;
using static HRMS.BLL.StaticData.StaticEnums;
using HRMS.DTOs;
using System;
using HRMS.BLL.UnitOfWork;
using HRMS.Utilities;
using System.Reflection;
using System.Net.Http;
using System.Collections.Generic;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Net.Http.Headers;
using System.Net;

namespace HRMS.API.Controllers
{
    public class UserEntitlementController : BaseController
    {
        public ResultViewModel GetAllEntiltementYears()
        {

            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.UserEntitlementBLL.GetAllEntiltementYears(),
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

        public ResultViewModel GetEntiltementYears(int LeaveTypeID, int UserID)
        {

            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.UserEntitlementBLL.GetEntiltementYears(LeaveTypeID, UserID),
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
                    Message = "Can't get all users entitlements",
                    Success = false

                };
            }


        }
        [HttpGet]
        public ResultViewModel GetAllEntiltementMonths(int LeaveTypeID)
        {

            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserEntitlementBLL.GetAllEntiltementMonths(LeaveTypeID),
                    Message = "Get All Months of Monthly Entitlement Successfully",
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
        public ResultViewModel GetAllEntiltementMonths(int LeaveTypeID, int UserID, int Year)
        {

            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserEntitlementBLL.GetAllEntiltementMonths(LeaveTypeID, UserID, Year),
                    Message = "Get Months of Monthly Entitlement Successfully",
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
        public ResultViewModel GetMonthlyEntitlementQuantity(int LeaveTypeID, int UserID, int Year, int Month)
        {

            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserEntitlementBLL.GetMonthlyEntitlementQuantity(LeaveTypeID, UserID, Year, Month),
                    Message = "Get Monthly Entitlement Qty Successfully",
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
        public ResultViewModel GetAllUserEntitlements(int UserID)
        {

            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.UserEntitlementBLL.GetUserEntitlementByID(UserID),
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

        public ResultViewModel EditUserTotalEntitlement(UserEntitlementsDTO UserEntitlementsDTO)
        {
            try
            {
                UnitOfWork.SharedUserEntitlementBLL.ChangeQuantityOfUserEntitlement(UserEntitlementsDTO);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Entitlement Updated Successfully",
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
        public ResultViewModel FilterUserEntitlementChanges(UserEntitlementChangesFilterDTO UserEntitlementChangesFilterDTO)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserEntitlementBLL.FilterUserEntitlementChanges(UserEntitlementChangesFilterDTO),
                    Message = "Get EntitlementChanges Successfully",
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

        public ResultViewModel HRFilterUserEntitlementChanges(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserEntitlementBLL.HRFilterUserEntitlementChanges(ViewRequestsFilterDTO),
                    Message = "Get EntitlementChanges Successfully",
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

        public ResultViewModel GetAllEntitlementChangedBy()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.EntitlementChangedByDIMBLL.GetAllEntitlementChangedBy(),
                    Message = "Get Data Successfully",
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

        [AcceptVerbs("Post")]
        public HttpResponseMessage HRFilterUserEntitlementChangesExcel(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            List<UserEntitlementChangesSearchResultDTO> UserEntitlementChangesResult = UnitOfWork.SharedUserEntitlementBLL.HRFilterUserEntitlementChanges(ViewRequestsFilterDTO);

            DataTable dtRequest = new DataTable();
            //dtRequest.Columns.Add("Access Control ID");
            dtRequest.Columns.Add("User Name");
            dtRequest.Columns.Add("AC ID");
            dtRequest.Columns.Add("Leave Type Name");
            dtRequest.Columns.Add("Entitlement Before");
            dtRequest.Columns.Add("Entitlement After");
            dtRequest.Columns.Add("Entitlement Changed By");
            dtRequest.Columns.Add("Request Duration");
            dtRequest.Columns.Add("User Change Entitlement");
            dtRequest.Columns.Add("Action Date");
            dtRequest.Columns.Add("Comment");
            foreach (var item in UserEntitlementChangesResult)
            {
                dtRequest.Rows.Add(item.UserName, item.AccessControlID, item.LeaveTypeName, item.EntitlementBefore, item.EntitlementTo, item.EntitlementChangedBy, item.RequestDuration, item.UserChangeEntitlement, item.ActionDate, item.Comment);
            }

            XLWorkbook wb = new XLWorkbook();

            //  byte[] excelData=new byte[10];

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "Cypress EntitlementChanges");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "EntitlementChangeReport" + DateTime.Now.ToString() + ".xls"
            };
            return result;
        }

        public ResultViewModel HRFilterUserEntitlement(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserEntitlementBLL.HRFilterUserEntitlement(ViewRequestsFilterDTO),
                    Message = "Get Entitlement Successfully",
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

        [AcceptVerbs("Post")]
        public HttpResponseMessage HRFilterUserEntitlementExcel(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            List<UserEntitlementSearchResultDTO> UserEntitlementResult = UnitOfWork.SharedUserEntitlementBLL.HRFilterUserEntitlement(ViewRequestsFilterDTO);

            DataTable dtRequest = new DataTable();
            dtRequest.Columns.Add("User Name");
            dtRequest.Columns.Add("AC ID");
            dtRequest.Columns.Add("Leave Type Name");
            dtRequest.Columns.Add("Year");
            dtRequest.Columns.Add("Month");
            dtRequest.Columns.Add("Entitlement Total");
            
            foreach (var item in UserEntitlementResult)
            {
                dtRequest.Rows.Add(item.UserName, item.AccessControlID, item.LeaveTypeName,item.Year, item.Month, item.EntitlementTotal);
            }

            XLWorkbook wb = new XLWorkbook();

            //  byte[] excelData=new byte[10];

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "Cypress Entitlement");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "EntitlementReport" + DateTime.Now.ToString() + ".xls"
            };
            return result;
        }

    }
}