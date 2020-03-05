using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.CompanyApp;
using WarhouseSystem.Application.EmployeeApp;
using WarhouseSystem.Application.StatusTypeApp;
using WarhouseSystem.Application.TransactionTypeApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Validation;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace WarhouseSystem.Controllers
{
    public class EmployeeController : ControllerBase
    {
        IManageEmployee manageEmployee;
        public EmployeeController()
        {
            manageEmployee = new ManageEmployee();
        }

        [HttpPost]
        public IActionResult Login(EmployeeViewModel model)
        {
            try
            {
             var emp=   manageEmployee.Login(model);
                var claims = new[]
                {
                  new  Claim(ClaimTypes.NameIdentifier,emp.Id.ToString()),
                  new  Claim(ClaimTypes.Name,emp.Name.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Hamadaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var securitydescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = cred
                };
                var tokenhandler = new JwtSecurityTokenHandler();
               var token= tokenhandler.CreateToken(securitydescriptor);

               if(emp != null)
                {
                    Utilites.CheckResult = true;
                }

                return Ok(ApiResultFactory.Success(tokenhandler.WriteToken(token)));
            }
            catch(Exception ex)
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Login Is Faild"));
            }
        }

        [HttpPost]
        public IActionResult Register(EmployeeViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageEmployee.AddEmployee(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Registeration Is Faild"));
            }
        }


        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                IEnumerable<EmployeeViewModel> models = manageEmployee.GetAllEmployees();
                return Ok(ApiResultFactory.Success(models));
            }
            catch(Exception ex)
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IActionResult GetEmployeeById(long Id)
        {
            try
            {
                EmployeeViewModel model = manageEmployee.GetEmployee(Id);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(long Id)
        {
            try
            {
                Utilites.CheckResult = manageEmployee.DeleteEmployee(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Employee"));
            }
        }

        [HttpPut]
        public IActionResult UpdateEmployee(long Id,EmployeeViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageEmployee.UpdateEmployee(Id,model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Employee"));
            }
        }
        [HttpPut]
        public IActionResult SetUserPassword( EmployeeViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageEmployee.SetUserPassword(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "New Password dont saved"));
            }
        }

        

        



    }
}
