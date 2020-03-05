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
    public class PositionController : BaseController
    {
     [HttpPost]
        public ResultViewModel AddNewPosition(Position NewPosition)
        {

            NewPosition.CreationDate = DateTime.Now;
            NewPosition.IsActive = true;
            NewPosition.IsDeleted = false;           
            try
            {
                UnitOfWork.PositionBLL.Add(NewPosition);
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
        public ResultViewModel EditPosition(Position position)
        {
           
            try
            {
                Position Position = UnitOfWork.PositionBLL.Find(x=>x.PositionID== position.PositionID).FirstOrDefault();
                Position.PositionName = Position.PositionName;
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
        public ResultViewModel DeletePosition( Position position)
        {
          
            try
            {

                Position Position = UnitOfWork.PositionBLL.Find(x => x.PositionID == position.PositionID).FirstOrDefault();
                Position.PositionName = Position.PositionName;
                Position.IsDeleted = true;
                Position.IsActive = false;
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

        public ResultViewModel PersonsToPosition(List<User> ListOfUsers, int PositionID)
        {
            try
            {
                UnitOfWork.UserPositionBLL.AddPersonsToPosition(ListOfUsers, PositionID);
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

        public ResultViewModel GetAllPositions()
        {         
        
            try
            {
                return new ResultViewModel
                {
                    Data =UnitOfWork.UserPositionBLL.GetPositionsView().ToList(),
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

        public ResultViewModel GetPersonsByPositionID(int PositionID)
        {
            List<UserDTO> ListUser = new List<UserDTO>();

           foreach(var item in UnitOfWork.UserPositionBLL.GetAllUserByPosition(PositionID))
            {
                ListUser.Add( new UserDTO
                {  
                    UserID=item.UserID,
                    UserName=item.LDAPUserName,

                });

            }
            try
            {
                return new ResultViewModel
                {
                    Data = ListUser,
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

        public ResultViewModel GetPositionByID(int PositionID)
        {
            try
            {
              Position Position=  UnitOfWork.PositionBLL.Find(p => p.PositionID == PositionID).FirstOrDefault();

                PositionDTO PositionDTO = new PositionDTO
                {
                 PositionID = Position.PositionID,
                 PositionName= Position.PositionName,
          
                };


                return new ResultViewModel
                {
                    Data = PositionDTO,
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