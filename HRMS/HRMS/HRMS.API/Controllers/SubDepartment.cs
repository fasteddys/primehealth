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
    public class SubDepartmentController : BaseController
    {
        public ResultViewModel GetAllSubDepartments()
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetSubDepartmentsView().ToList(),
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
        public ResultViewModel GetSubDepartmentByID(int SubDepartmentID)
        {
                try
                {
                    return new ResultViewModel
                    {
                        Data = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByID(SubDepartmentID),
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
        public ResultViewModel GetPersonsBySubDepartmentID(int SubDepartmentID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetAllUserBySubDepartment(SubDepartmentID),
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
        public ResultViewModel GetPersonsBySubDepartmentIDs(List<int> SubDepartmentIDs)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(SubDepartmentIDs),
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
        public ResultViewModel AddNewSubDepartment(SubDepartment NewSubDepartment)
        {

            NewSubDepartment.CreationDate = DateTime.Now;
            NewSubDepartment.IsActive = true;
            NewSubDepartment.IsDeleted = false;


            try
            {
                UnitOfWork.SubDepartmentBLL.Add(NewSubDepartment);
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
        //public ResultViewModel GetSubDepartmentByDepartmentID(int DepartmentID)
        //{
        //    List<SubDepartmentDTO> ListSubDepartmen = new List<SubDepartmentDTO>();

        //    foreach (var item in UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntID(DepartmentID))
        //    {
        //        ListSubDepartmen.Add(new SubDepartmentDTO
        //        {
        //            UserID = item.UserID,
        //            UserName = item.LDAPUserName,

        //        });

        //    }
        //    try
        //    {
        //        return new ResultViewModel
        //        {
        //            Data = ListSubDepartmen
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
        public ResultViewModel GetSubDepartmentByDepartmentID(int DepartmentID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntID(DepartmentID),
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
        public ResultViewModel GetSubDepartmentByDepartmrntIDs(List<int> ListDepartmentIDs)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntIDs(ListDepartmentIDs),
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