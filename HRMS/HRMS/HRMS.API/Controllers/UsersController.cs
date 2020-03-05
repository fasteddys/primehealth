using System.Collections.Generic;
using System.Linq;
using HRMS.DTOs.Business;
using System;
using System.Web.Http;
using HRMS.DTOs;
using System.IO;
using System.Web;
using System.Net.Http;
using HRMS.Entities;
using HRMS.Utilities;
using System.Reflection;
using System.Data;
using ClosedXML.Excel;
using System.Net.Http.Headers;
using System.Net;

namespace HRMS.API.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Users
        public ResultViewModel GetAllUsers()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserPositionBLL.SelectUserPosition(),
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
        //unActive Employees
        public ResultViewModel GetAllUserThatWillBeActive()
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetAllUserThatWillBeActive(),
                    Message = "Sucess",
                    Success = true,
                };
            }
            catch(Exception ex)
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
        public ResultViewModel GetNumberOfAllUserThatWillBeActive()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetNumberOfAllUserThatWillBeActive(),
                    Message = "Sucess",
                    Success = true,
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
        //-------------------------------------------------------------------
        public ResultViewModel GetOnBehalfOfUsers(int UserID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserPositionBLL.GetOnBehalfOfUsers(UserID),
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
        public ResultViewModel GetAllUserDetails(int UserID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserDetailsProfileBLL.UserDetails(UserID),
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
        public ResultViewModel GetAllUsersSubDepartment()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.SelectUserSubDepartment(),
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
        public ResultViewModel GetAllUsersDepartment()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.SelectUserDepartment(),
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
        public ResultViewModel GetUserByID(int UserID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetUserByID(UserID),
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
        [System.Web.Http.HttpPost]
        public ResultViewModel GetUserListIDs(List<ListIDDTO> UsersIDs)
        {
            try
            {
                UnitOfWork.UserBLL.GetUserListIDs(UsersIDs);
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
        [System.Web.Http.HttpPost]
        public ResultViewModel SumbitUsersToSubDepartment(List<SubDepartmentIDListDTO> UsersIDs)
        {
            try
            {
                UnitOfWork.UserBLL.SumbitUsersToSubDepartment(UsersIDs);
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
        public ResultViewModel SumbitUsersToDepartment(List<DepartmentIDListDTO> UsersIDs)
        {
            try
            {
                UnitOfWork.UserBLL.SumbitUsersToDepartment(UsersIDs);
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
        public ResultViewModel GetAllACUsers()
        {


            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.AccessControlUserBLL.GetALLAccessControlUser(),
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
        public ResultViewModel GetACUserByID(int ID)
        {


            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.AccessControlUserBLL.GetAll().Where(p => p.AccessControlUserID == ID).FirstOrDefault(),
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
        [System.Web.Http.HttpPost]
        public ResultViewModel UpdateUserAccessControlID(UserDTO user)
        {

            try
            {
                UnitOfWork.UserBLL.LinkUsersToAccessControl(user);
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
        [System.Web.Http.HttpPost]
        public ResultViewModel GetAttendanceByUserID(TimeAttendanceParametersDTO TimeAttendanceParametersDTO)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserTimeAttendanceBLL.GetAttendance(TimeAttendanceParametersDTO).ToList(),
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
        public ResultViewModel GetALLUserDetailsByID(int UserID)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.ManagerBLL.GetALLUserDetails(UserID),
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
        public ResultViewModel GetUserRequests(int UserID, DateTime StartDate, DateTime EndDate, int LeaveTypeID, int Status)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetUserProfileRequests(UserID, StartDate, EndDate, LeaveTypeID, Status),
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
        public ResultViewModel GetAllUserProfileData(int LoggedUserID, int UserID)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserProfileBLL.GetAllUserProfileData(LoggedUserID, UserID),
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

        [System.Web.Http.HttpPost]
        public ResultViewModel DeactivateUser(UserprofileDTO UserData)
        {
            try
            {
                UnitOfWork.SharedUserProfileBLL.DeactivateUser(UserData);
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

        [System.Web.Http.HttpPost]
        public ResultViewModel DeleteUser(UserprofileDTO UserData)
        {
            try
            {
                UnitOfWork.SharedUserProfileBLL.DeleteUser(UserData);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "User Deleted Successfully",
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

        [System.Web.Http.HttpPost]
        public ResultViewModel ReactivateUser(UserprofileDTO UserData)
        {
            try
            {
                UnitOfWork.SharedUserProfileBLL.ReactivateUser(UserData);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "User Reactivated Successfully",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,// "Can't Reactivate User"
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

        [System.Web.Http.HttpPost]
        public ResultViewModel AddProfilePictureURL(ProfilePictureDTO profilePictureDTO)
        {
            //ProfilePictureDTO profilePictureDTO = new ProfilePictureDTO();
            //profilePictureDTO.UserID = ID;
            //profilePictureDTO.Path = Path;
            try
            {
                UnitOfWork.UserBLL.SaveUserProfilePicture(profilePictureDTO);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Profile Picture Updtaed successfully",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,//Sorry..error while updating profile picture, please try again
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
        public ResultViewModel GetUserDropDownData()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserProfileBLL.GetUserDataDropDowns(),
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
        [System.Web.Http.HttpPost]
        public ResultViewModel EditUserData(UserprofileDTO UserprofileDTO)
        {
            try
            {
                UnitOfWork.SharedUserProfileBLL.EditUserData(UserprofileDTO);
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
        [System.Web.Http.HttpPost]
        public ResultViewModel EditAdditionalUserData(UserprofileDTO UserprofileDTO)
        {
            try
            {
                UnitOfWork.SharedUserProfileBLL.EditAdditionalUserData(UserprofileDTO);
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
        [System.Web.Http.HttpPost]
        public ResultViewModel GetUsersByDepartmrntIDs(List< int> ListDepartmentIDs)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetUsersByDepartmrntID(ListDepartmentIDs),
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

        public ResultViewModel GetUserDepartmentUsers(int UserID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserProfileBLL.GetUserDepartmentUsers(UserID),
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

        public ResultViewModel GetOrganizationUsers()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserProfileBLL.GetOrganizationUsers(),
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

        public ResultViewModel GetEmployeesNumberDeserveMedicalInsurance()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetEmployeesNumberDeserveMedicalInsurance(),
                    Message = "Success",
                    Success = true,
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

        public ResultViewModel GetEmployeesDeserveMedicalInsurance()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.UserBLL.GetEmployeesDeserveMedicalInsurance(),
                    Message = "Success",
                    Success = true,
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
        //------------------------------------------------------
        [HttpPost]
        //get filiter users report
        public ResultViewModel UsersInformations(UsersInformationInputDTO usersInformationInputDTO)
        {
            try
            {
              int z=  UnitOfWork.SharedUserProfileBLL.UsersInformations(usersInformationInputDTO).Count();
                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserProfileBLL.UsersInformations(usersInformationInputDTO),
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
        //--------------------------------------------
        [AcceptVerbs("Post")]
        public HttpResponseMessage UsersInformationsExcel(UsersInformationInputDTO usersInformationInputDTO)
        {
            List<UserInformationOutputDTO> userInformationOutputDTOList = UnitOfWork.SharedUserProfileBLL.UsersInformations(usersInformationInputDTO);
            DataTable dtRequest = new DataTable();
            dtRequest.Columns.Add("User ID");//1
            dtRequest.Columns.Add("Access Control UserID");//2
            dtRequest.Columns.Add("User Name");//3
            dtRequest.Columns.Add("Arbic Name");//4
            dtRequest.Columns.Add("Company Name");//23 **
            dtRequest.Columns.Add("ManagerName");//6
            dtRequest.Columns.Add("Team Manager Name");//7
            dtRequest.Columns.Add("Approval Flow Name");//8
            dtRequest.Columns.Add("Sub Department Name");//9
            dtRequest.Columns.Add("Department Name");//10
            dtRequest.Columns.Add("Position Name");//11
            dtRequest.Columns.Add("workingWeekName");//12
            dtRequest.Columns.Add("Contract Type Name");//13
            dtRequest.Columns.Add("Gender");//14
            dtRequest.Columns.Add("HireDate");//15
            dtRequest.Columns.Add("BirthDate");//16
            dtRequest.Columns.Add("ProbationDate");//17
            dtRequest.Columns.Add("TerminationDate");//18
            dtRequest.Columns.Add("CreationDate");//19
            dtRequest.Columns.Add("Last ModificationDate");//20
            dtRequest.Columns.Add("Seniority Before HireMonth");//21
            dtRequest.Columns.Add("Seniority Before HireYears");//22
            dtRequest.Columns.Add("Address");//23
            dtRequest.Columns.Add("Home Address");//24
            dtRequest.Columns.Add("NationalID");//24
            dtRequest.Columns.Add("MedicalID");//25
            dtRequest.Columns.Add("PhoneNumber");//26
            dtRequest.Columns.Add("CustomNote");//27
            dtRequest.Columns.Add("IsActive");//28
            dtRequest.Columns.Add("IsOutDomain");//23

            foreach (var user in userInformationOutputDTOList)
            {
                dtRequest.Rows.Add(user.UserID, user.AccessControlUserID, user.UserName, user.ArbicName,user.CompanyName,
                  user.ManagerName, user.TeamManagerName, user.ApprovalFlowName, user.SubDepartmentName, user.DepartmentName, 
                   user.PositionName,user.workingWeekName, user.ContractTypeName, user.Gender, user.HireDate, user.BirthDate, user.ProbationDate, 
                   user.TerminationDate,user.creationDate,user.ModificationDate, user.SeniorityBeforeHireMonth,user.SeniorityBeforeHireYears,
                   user.Address,user.HomeAddress, user.NationalID, user.MedicalID,user.PhoneNumber, user.CustomNote, user.IsActive,user.IsOutDomainUser);
            }

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "Cypress UsersInformation");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "UsersInformation" + DateTime.Now.ToString() + ".xls"
            };

            return result;
        }
        //------------------------------------------------------------------------------------------------
        //get all class properts
        public ResultViewModel getAllUserInformationProperty()
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.SharedUserProfileBLL.getAllUserInformationProperty(),
                    Message = "Success Get All Propertys Names",
                    Success = true
                };
            }
            catch
            {
                return new ResultViewModel
                {
                    Data = null,
                    Message = "Faild Get All Propertys Names",
                    Success = false
                };
            }
          

        }
        //------------------------------------------------------------------------------------------------
    }
}