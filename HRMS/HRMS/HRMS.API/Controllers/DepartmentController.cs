using System.Web;
using HRMS.DTOs;
using HRMS.Utilities;
using HRMS.Entities;
using System.Web.Http;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HRMS.DTOs.Business;
using System;
using System.Reflection;

namespace HRMS.API.Controllers
{
    public class DepartmentController : BaseController
    {
        public ResultViewModel GetAllDepartments()
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetDepartmentsView().ToList(),
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
        public ResultViewModel GetAllUserInSameDeparment(int UserID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetAllUserInSameDeparment(UserID),
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
        public ResultViewModel AddNewDepartment(Department NewDepartment)
        {

            NewDepartment.CreationDate = DateTime.Now;
            NewDepartment.IsActive = true;
            NewDepartment.IsDeleted = false;
            Exception exOutputSubmit;
           

            try
            {
                UnitOfWork.DepartmentBLL.Add(NewDepartment);
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


        //public ResultViewModel GetDepartmentByID(int DepartmentID)
        //{

        //    try
        //    {
        //        return new ResultViewModel
        //        {
        //            Data = UnitOfWork.DepartmentBLL.GetDepartmentByID(DepartmentID)
        //            ,
        //            Message = "",
        //            Success = true

        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultViewModel
        //        {
        //            Data = null
        //             ,
        //            Message = "error",
        //            Success = false

        //        };
        //    }
        //}



        public ResultViewModel GetDepartmentByID(int DepartmentID)
        {
            try
            {
                Department Department = UnitOfWork.DepartmentBLL.Find(p => p.DepartmentID == DepartmentID).FirstOrDefault();

                DepartmentDTO DepartmentDTO = new DepartmentDTO
                {
                    DepartmentID = Department.DepartmentID,
                    DepartmentName = Department.DepartmentName,

                };
                return new ResultViewModel
                {
                    Data = DepartmentDTO
                    ,
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



        //public ResultViewModel GetSubDepartmentByDepartmentID(int DepartmentID)
        //{


        //    try
        //    {
        //        return new ResultViewModel
        //        {
        //            Data = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntID(DepartmentID)
        //            ,
        //            Message = "",
        //            Success = true

        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultViewModel
        //        {
        //            Data = null
        //             ,
        //            Message = "error",
        //            Success = false

        //        };
        //    }
        //}
    }
}