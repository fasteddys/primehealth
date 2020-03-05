using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using HRMS.Entities;
using HRMS.DTOs.Business;
using static HRMS.BLL.StaticData.StaticEnums;
using HRMS.DTOs;
using System.Reflection;
using HRMS.Utilities;
using System.Configuration;
namespace HRMS.API.Controllers
{
    public class LoginController : BaseController
    {
       [HttpPost]
        public ResultViewModel Login(LoginDTO LoginDTO)
        {
           // Exception exOutputSubmit;
            bool IsSuccess = false;         
            try
            {
               var CheckLDAPAuthentication = ConfigurationManager.AppSettings["CheckLDAPAuthentication"];
                LoginUserDTO LoginUserDTO = UnitOfWork.UserLoginBLL.Login(LoginDTO, out IsSuccess, CheckLDAPAuthentication);
                //  if (UnitOfWork.Submit(out exOutputSubmit))

                if(LoginUserDTO!=null)
                {
                    return new ResultViewModel
                    {
                        Data = LoginUserDTO,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Can Not Login",//exOutputSubmit.Message,
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
        public ResultViewModel LogOut(LoginDTO LoginDTO)
        {
            try
            {
               var Success= UnitOfWork.UserBLL.LogOut(LoginDTO);
                Exception exOutputSubmit;
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
        public ResultViewModel ForceLogOut(LoginDTO LoginDTO)
        {
            try
            { var success = UnitOfWork.UserBLL.ForceLogOut(LoginDTO);
                return new ResultViewModel
                {
                    Data = success,
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



    }
}