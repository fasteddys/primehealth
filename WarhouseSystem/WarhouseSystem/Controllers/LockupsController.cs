﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.CompanyApp;
using WarhouseSystem.Application.EmployeeRoleApp;
using WarhouseSystem.Application.StatusTypeApp;
using WarhouseSystem.Application.TransactionTypeApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    [RoutePrefix("api/Lockups")]
    public class LockupsController : ApiController
    {
        IManageCompany manageCompany;
        IManageEmployeeRole manageEmployeeRole;
        IManageStatusType manageStatusType;
        IManageTransactionType manageTransactionType;
        public LockupsController()
        {
            manageTransactionType = new ManageTransactionType();
            manageEmployeeRole = new ManageEmployeeRole();
            manageStatusType = new ManageStatusType();
            manageCompany = new ManageCompany();
        }

        [HttpGet]
        [Route("Companies")]
        public IHttpActionResult GetCompanies()
        {
            try
            {
                return Ok(ApiResult.Success(manageCompany.GetAllComapnies()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }

        [HttpGet]
        [Route("StatusTypes")]
        public IHttpActionResult GetStatusTypes()
        {
            try
            {
                return Ok(ApiResult.Success(manageStatusType.GetAllStatusTypes()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }

        [HttpGet]
        [Route("EmployeeRoles")]
        public IHttpActionResult GetEmployeeRoles()
        {
            try
            {
                return Ok(ApiResult.Success(manageEmployeeRole.GetAllEmployeeRoles()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }


        [HttpGet]

        [Route("Transactions")]

        public IHttpActionResult GetTransactionTypes()
        {
            try
            {
                return Ok(ApiResult.Success(manageTransactionType.GetAllTransactionTypes()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }
    }
}
