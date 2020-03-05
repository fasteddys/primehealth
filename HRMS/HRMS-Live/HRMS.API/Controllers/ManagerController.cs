using HRMS.DTOs;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace HRMS.API.Controllers
{
    public class ManagerController : BaseController
    {
        // GET: Manager
        public ResultViewModel GetAllManagers()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.ManagerBLL.GetALLManagers(),
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
        public ResultViewModel AddManager(Manager Newmanager)
        {
            Exception exOutputSubmit;

            try
            {
                UnitOfWork.ManagerBLL.Add(Newmanager);
                UnitOfWork.Submit(out exOutputSubmit);
                return new ResultViewModel
                {
                    Data = UnitOfWork.ManagerBLL.GetALLManagers(),
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
        public ResultViewModel UpdateManager(ManagerDTO Updatedmanager)
        {
            Exception exOutputSubmit;

            try
            {
                UnitOfWork.ManagerBLL.EditManager(Updatedmanager);

                UnitOfWork.Submit(out exOutputSubmit);
                return new ResultViewModel
                {
                    Data = null,
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
        public ResultViewModel AddPersonsAndSubDepartmentToManager(ManagerToSubordinatesDTO ManagerToSubordinatesDTO)
        {
            Exception exOutputSubmit;
            try
            {
                string Message = "";
                User User = new User();
                Manager Manager = UnitOfWork.ManagerBLL.Find(x => x.ManagerUserFK == ManagerToSubordinatesDTO.UserID).FirstOrDefault();
                if (Manager == null)
                {
                    User = UnitOfWork.UserBLL.Find(x => x.UserID == ManagerToSubordinatesDTO.UserID).FirstOrDefault();
                    Manager = new Manager
                    {
                        ManagerUserFK = User.UserID,
                        ManagerName = User.UserName,
                        IsActive = true,
                        IsDeleted = false,
                        CreationDate = DateTime.Now

                    };
                    UnitOfWork.ManagerBLL.Add(Manager);
                    UnitOfWork.Submit(out exOutputSubmit);
                    Message = "Add Manager Successfully";
                    ManagerToSubordinatesDTO.ManagerID = Manager.ManagerID;
                }
                else if(Manager != null)
                {
                    ManagerToSubordinatesDTO.ManagerID = UnitOfWork.ManagerBLL.Find(x => x.ManagerUserFK == ManagerToSubordinatesDTO.UserID).FirstOrDefault().ManagerID;
                    Message = "Already Manager Founded";
                }

                if (ManagerToSubordinatesDTO.ListUsersIDs.Count != 0)
                {
                    UnitOfWork.ManagerBLL.AddPersonsToManager(ManagerToSubordinatesDTO);
                    UnitOfWork.Submit(out exOutputSubmit);
                    Message += " & Add Users Successfully";
                }
                if(ManagerToSubordinatesDTO.ListSubDepartmentIDs.Count != 0)
                {
                    UnitOfWork.ManagerBLL.AddSubdepartmentToManager(ManagerToSubordinatesDTO);
                    UnitOfWork.Submit(out exOutputSubmit);
                    Message += " & Add Subdepartment Successfully";
                }

                return new ResultViewModel
                {
                    Data = null,
                    Message = Message,
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

        public ResultViewModel GetALLManagerDetailsByID(int  ManagerID)
        {


            try
            {
                
                return new ResultViewModel
                {
                    Data = UnitOfWork.ManagerBLL.GetALLManagerDetails(ManagerID),
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
        public ResultViewModel CheckUserIsManager(int ID)
        {

            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.ManagerBLL.CheckUserIsManager(ID),
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

        //public ResultViewModel AddUserToManagers(int UserID,List<User> PersonList)
        //{

        //    try
        //    {
        //     User User=   UnitOfWork.UserBLL.GetById(UserID);
        //       Manager AddedManager= UnitOfWork.ManagerBLL.AddUserToManagers(User);
        //        UnitOfWork.ManagerBLL.AddPersonsToManager(PersonList, AddedManager.ManagerID);

        //        UnitOfWork.Submit();
        //        return new ResultViewModel
        //        {
        //            Data = UnitOfWork.ManagerBLL.GetAll().ToList()
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